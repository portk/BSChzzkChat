using BSChzzkChat.Structs;
using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSChzzkChat.Src
{
    class ChatClient
    {
        ClientWebSocket client = new ClientWebSocket();
        Uri uri;
        string pingMsg;
        string pongMsg;
        Random rand = new Random();
        //string connectedMsg;

        public RequestListControl requestListControl = new RequestListControl();

        public ChatClient()
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(5, 5);

            int id = rand.Next(1, 11);

            uri = new Uri($"wss://kr-ss{id}.chat.naver.com/chat");

            PingPongObj tmp = new PingPongObj("2", 0);
            pingMsg = JsonSerializer.Serialize(tmp);
            tmp = new PingPongObj("2", 10000);
            pongMsg = JsonSerializer.Serialize(tmp);
            //tmp = new PingPongObj("2", 10100);
            //connectedMsg = JsonSerializer.Serialize(tmp);
        }

        public async Task Init()
        {
            GetInformation userInformation = new GetInformation();
            userInformation.SendRequest();

            try
            {
                await client.ConnectAsync(uri, CancellationToken.None);

                ConnectBdyObj connectOptBdy = new ConnectBdyObj(userInformation.uId, 2001, userInformation.accessToken);
                ConnectObj connectOpt = new ConnectObj("2", 100, "game", userInformation.chatChannelId, connectOptBdy, 1);

                // first touch
                string jsonString = JsonSerializer.Serialize(connectOpt);
                ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonString));
                await client.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);

                ThreadPool.QueueUserWorkItem(Listen);
                //ThreadPool.QueueUserWorkItem(Send);
            }
            catch (Exception e)
            {
                WriteLog(e.Message);
            }
        }

        private async void Listen(Object obj)
        {
            ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[8192]);

            if (client.State == WebSocketState.Open)
            {
                WriteLog("연결에 성공하였습니다");
            }
            else
            {
                WriteLog("연결에 실패하였습니다");
            }

            while (client.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await client.ReceiveAsync(bytesReceived, CancellationToken.None);
                string serverMsg = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                if (serverMsg == pingMsg) Send(pongMsg);
                else
                {
                    ParseChat(serverMsg);
                }
            }
        }

        // 미구현
        // 사용자가 채팅을 입력해서 보내는 부분
        // 따로 만들고 떼온거라 서식이 이 프로그램에 알맞지 않다.
        // 폼에서 입력받고 전송의사를 받아서 발송하는 형태로 변경해야함
        //private void Send(Object obj)
        //{
        //    string msg = (string)obj;
        //    if (msg != null)
        //    {
        //        ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
        //        client.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        //    }
        //}

        public async void CloseClient()
        {
            await client.CloseOutputAsync(WebSocketCloseStatus.Empty, null, CancellationToken.None);
        }

        private async void Send(string msg)
        {
            ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
            await client.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // 끝까지 받기 전에 파싱이 시작되서 지금은 인덱싱 방식을 사용했음
        // 기다리게 바꾸고, Json 파싱으로 바꾸면 지금처럼 딮스를 파고 들어가지 않고 리스트에서 하나씩 꺼내면 됨
        private void ParseChat(string str)
        {
            int idxNicknameStart;
            int idxNicknameEnd;
            string nickname;
            int idxChatMsgStart;
            int idxChatMsgEnd;
            string chatMsg;
            int idxUidStart;
            int idxUidEnd;
            string uid;
            int idxPayAmountStart;
            int idxPayAmountEnd;
            string payAmount = null;

            // 닉네임 찾기
            idxNicknameStart = str.IndexOf("nickname");
            if (idxNicknameStart != -1)
            {
                idxNicknameStart += 13;
                idxNicknameEnd = str.IndexOf("\\\"", idxNicknameStart);

                nickname = (idxNicknameEnd != -1) ? str.Substring(idxNicknameStart, idxNicknameEnd - idxNicknameStart) : "";
            }
            else
            {
                nickname = "";
            }

            // 채팅 내용 찾기
            idxChatMsgStart = str.IndexOf("msg\"");
            if (idxChatMsgStart != -1)
            {
                idxChatMsgStart += 6;
                idxChatMsgEnd = str.IndexOf("\"", idxChatMsgStart);

                if (idxChatMsgEnd != -1)
                {
                    chatMsg = str.Substring(idxChatMsgStart, idxChatMsgEnd - idxChatMsgStart);
                    ParseChat(str.Substring(idxChatMsgEnd));
                }
                else
                {
                    chatMsg = "";
                }

            }
            else
            {
                chatMsg = "";
            }


            // 도네이션 확인
            idxPayAmountStart = str.IndexOf("\"payAmount\\\"");
            if (idxPayAmountStart != -1)
            {
                idxPayAmountStart += 13;
                idxPayAmountEnd = str.IndexOf(",", idxPayAmountStart);

                if (idxPayAmountEnd != -1) payAmount = str.Substring(idxPayAmountStart, idxPayAmountEnd - idxPayAmountStart);

                // 익명 후원시 닉네임 자리에 익명넣기
                idxUidStart = str.IndexOf("\"uid\"");
                if (idxUidStart != -1)
                {
                    idxUidStart += 7;
                    idxUidEnd = str.IndexOf("\"", idxUidStart);

                    uid = (idxUidEnd != -1) ? str.Substring(idxUidStart, idxUidEnd - idxUidStart) : "";

                    if (uid == "anonymous") nickname = "익명";
                }
            }

            // 출력부
            if (nickname != "" && chatMsg != "")
            {
                if (chatMsg.IndexOf(GetSettings.RequestWord) == 0)
                {
                    ThreadPool.QueueUserWorkItem(ThreadingGetRequest, chatMsg.Substring(GetSettings.RequestWord.Length));
                }

                if (payAmount == null)
                {
                    ChatWrite(nickname, chatMsg);
                }
                else
                {
                    ChatWrite(nickname, chatMsg, payAmount);
                }
            }
            else
            {
                return;
            }
        }

        private void ThreadingGetRequest(object songCode)
        {
            requestListControl.GetRequest((string)songCode);
        }

        // 일반채팅
        private void ChatWrite(string nickname, string chatMsg)
        {
            if (Form1._chatBox.InvokeRequired)
            {
                Form1._chatBox.Invoke(new MethodInvoker(delegate
                {
                    if (Form1._chatBox.Text.Length != 0)
                    {
                        Form1._chatBox.SelectionStart = Form1._chatBox.TextLength;
                    }
                    Form1._chatBox.SelectionColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    Form1._chatBox.AppendText($"{nickname} : ");
                    Form1._chatBox.SelectionColor = Color.Black;
                    Form1._chatBox.AppendText($"{chatMsg}" + "\r\n");
                }));
            }
            else
            {
                if (Form1._chatBox.Text.Length != 0)
                {
                    Form1._chatBox.SelectionStart = Form1._chatBox.TextLength;
                }
                Form1._chatBox.SelectionColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                Form1._chatBox.AppendText($"{nickname} : ");
                Form1._chatBox.SelectionColor = Color.Black;
                Form1._chatBox.AppendText($"{chatMsg}" + "\r\n");
            }
        }

        // 도네이션
        private void ChatWrite(string nickname, string chatMsg, string payAmount)
        {
            if (Form1._chatBox.InvokeRequired)
            {
                Form1._chatBox.Invoke(new MethodInvoker(delegate
                {
                    if (Form1._chatBox.Text.Length != 0)
                    {
                        Form1._chatBox.SelectionStart = Form1._chatBox.TextLength;
                    }
                    Form1._chatBox.SelectionColor = Color.Green;
                    Form1._chatBox.AppendText($"({payAmount} 후원) ");
                }));
                ChatWrite(nickname, chatMsg);
            }
            else
            {
                if (Form1._chatBox.Text.Length != 0)
                {
                    Form1._chatBox.SelectionStart = Form1._chatBox.TextLength;
                }
                Form1._chatBox.SelectionColor = Color.Green;
                Form1._chatBox.AppendText($"({payAmount} 후원) ");
                ChatWrite(nickname, chatMsg);
            }
        }

        private void WriteLog(string msg)
        {
            if (Form1._logBox.InvokeRequired)
            {
                Form1._logBox.Invoke(new MethodInvoker(delegate
                {
                    if (Form1._logBox.Text.Length != 0)
                    {
                        Form1._logBox.SelectionStart = Form1._chatBox.TextLength;
                    }
                    Form1._logBox.AppendText(msg);
                    Form1._logBox.AppendText("\r\n");
                }));
            }
            else
            {
                if (Form1._logBox.Text.Length != 0)
                {
                    Form1._logBox.SelectionStart = Form1._chatBox.TextLength;
                }
                Form1._logBox.AppendText(msg);
                Form1._logBox.AppendText("\r\n");
            }
        }
    }
}

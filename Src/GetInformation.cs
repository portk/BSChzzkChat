using BSChzzkChat.Structs;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace BSChzzkChat.Src
{
    class GetInformation
    {
        WebClient client = new WebClient();
        public string uId = "";
        public string chatChannelId = "";
        public string accessToken = "";
        public string extraToken = "";

        // 리퀘스트 헤더 설정
        public GetInformation()
        {
            client.Headers.Clear();
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.Encoding = Encoding.UTF8;
        }

        public void SendRequest()
        {
            // 채널 id 받아오기
            string channelId = GetSettings.ChannelId;

            try
            {
                // 응답 임시 저장용
                string response = "";

                // 사용자 정보 받기
                response = client.DownloadString("https://comm-api.game.naver.com/nng_main/v1/user/getUserStatus");
                MyInfo myInfo = JsonSerializer.Deserialize<MyInfo>(response);
                uId = myInfo.content.userIdHash;
                if (uId == null) uId = "";

                // 채널 정보 받기
                response = client.DownloadString($"https://api.chzzk.naver.com/polling/v2/channels/{channelId}/live-status");
                LiveStatus liveStatus = JsonSerializer.Deserialize<LiveStatus>(response);
                chatChannelId = liveStatus.content.chatChannelId;

                // 접근 토큰 받기
                response = client.DownloadString($"https://comm-api.game.naver.com/nng_main/v1/chats/access-token?channelId={chatChannelId}&chatType=STREAMING");
                GetAccessToken getAccessToken = JsonSerializer.Deserialize<GetAccessToken>(response);
                accessToken = getAccessToken.content.accessToken;
                extraToken = getAccessToken.content.extraToken;
            }
            catch (Exception ex)
            {
                if (Form1._logBox.InvokeRequired)
                {
                    Form1._logBox.Invoke(new MethodInvoker(delegate
                    {
                        if (Form1._logBox.Text.Length != 0)
                        {
                            Form1._logBox.SelectionStart = Form1._chatBox.TextLength;
                        }
                        Form1._logBox.AppendText(ex.Message);
                        Form1._logBox.AppendText("\r\n");
                    }));
                }
                else
                {
                    if (Form1._logBox.Text.Length != 0)
                    {
                        Form1._logBox.SelectionStart = Form1._chatBox.TextLength;
                    }
                    Form1._logBox.AppendText(ex.Message);
                    Form1._logBox.AppendText("\r\n");
                }
                return;
            }
        }
    }
}

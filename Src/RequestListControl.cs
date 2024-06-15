using BSChzzkChat.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace BSChzzkChat.Src
{
    class RequestListControl
    {
        public static List<Request> _songList = new List<Request>();
        public static bool listState = false;
        public RequestListControl()
        {
            ThreadPool.SetMaxThreads(2, 2);
        }

        // 기신청 여부 조회
        public bool CheckList(string songCode)
        {
            foreach (Request song in _songList)
            {
                if (song.SongCode == songCode) return true;
            }

            return false;
        }

        // 요청받은 내용 등록 혹은 자동 거절
        public void GetRequest(string songCode)
        {
            if (CheckList(songCode))
            {
                WriteLog(String.Format($"{songCode} 곡은 신청 상태 입니다."));

            }
            else if (listState)
            {
                WriteLog(String.Format("리스트가 가득차 받을 수 없습니다."));
            }
            else
            {
                if (_songList.Count >= GetSettings.RequestMaxCount) listState = true;

                Request request = new Request(songCode, "곡 정보 검색 중");

                _songList.Add(request);

                // item add to listBox
                if (Form1._requestList.InvokeRequired)
                {
                    Form1._requestList.Invoke(new MethodInvoker(delegate
                    {
                        Form1._requestList.Items.Add(request.ToString());
                    }));
                }
                else
                {
                    Form1._requestList.Items.Add(request.ToString());
                }

                ThreadPool.QueueUserWorkItem(CheckInfo, songCode);
            }
        }

        public void CheckInfo(object objSongCode)
        {
            string songCode = (string)objSongCode;
            GetSongData getSongData = new GetSongData(songCode);
            string songName = getSongData.GetFileData();

            int idx = 0;
            foreach (Request item in _songList)
            {
                if (item.SongCode == songCode)
                {
                    break;
                }
                idx += 1;
            }

            // 검색결과가 있을 때
            if (songName != "")
            {
                songName = songName.Substring(0, songName.IndexOf(".zip"));
                Request temp = _songList[idx];
                temp.SongName = songName;
                _songList[idx] = temp;
                // logBox control
                WriteLog($"{songCode} 곡의 파일 정보를 찾았습니다.");
                // requestList control
                if (Form1._requestList.InvokeRequired)
                {
                    Form1._requestList.Invoke(new MethodInvoker(delegate
                    {
                        Form1._requestList.Items[idx] = songName;
                    }));
                }
                else
                {
                    Form1._requestList.Items[idx] = songName;
                }
            }
            else
            {
                WriteLog($"{songCode} 곡의 정보를 찾을 수 없어 자동 거절 되었습니다.");
                DeclineRequest(idx);
            }

            getSongData = null;
        }

        // 요청 수락
        public void AcceptRequest(int idx)
        {
            // 파일검사
            string directoryPath = GetSettings.CustomSongPath;

            string[] dirArray = Directory.GetDirectories(directoryPath, String.Format($"{_songList[idx].SongCode} *"));
            // 있으면 파일 파일 있다고 표시
            if (dirArray.Length > 0)
            {
                GetFileInfo(dirArray[0]);
            }
            // 없으면 압축파일 존재여부 검사
            else
            {
                string[] files = Directory.GetFiles(directoryPath, String.Format($"{_songList[idx].SongCode} *.zip"));
                // 압축파일이 존재하면 압축 해제
                if (files.Length > 0)
                {
                    WriteLog("압축파일을 해제 합니다");

                    // 압축 해제
                    _ = new FileUnZip(_songList[idx].SongCode);
                }
                else
                {
                    //파일 다운로드 및 압축 해제
                    WriteLog("파일을 다운로드 합니다");

                    _ = new SongDownloader(idx);
                }
            }
        }

        // 요청 삭제
        public void DeclineRequest(int idx)
        {
            _songList.RemoveAt(idx);
            if (Form1._requestList.InvokeRequired)
            {
                Form1._requestList.Invoke(new MethodInvoker(delegate
                {
                    Form1._requestList.Items.RemoveAt(idx);
                }));
            }
            else
            {
                Form1._requestList.Items.RemoveAt(idx);
            }

            if (_songList.Count <= GetSettings.RequestMaxCount) listState = false;
        }

        private void GetFileInfo(string fileDirectory)
        {
            string fileInfo = File.ReadAllText($@"{fileDirectory}\info.dat");
            string[] needInfo = { "_songName\":", "_songSubName\":", "_songAuthorName\":", "_levelAuthorName\":" };
            string[] songInfo = { null, null, null, null };
            int idxStart = -1;
            int idxEnd = -1;

            for (int idx = 0; idx < needInfo.Length; idx++)
            {
                idxStart = fileInfo.IndexOf(needInfo[idx]);

                if (idxStart != -1)
                {
                    idxStart = fileInfo.IndexOf("\"", idxStart + needInfo[idx].Length);
                    idxEnd = fileInfo.IndexOf("\",", idxStart);
                }
                
                if (idxStart != -1 && idxEnd != -1)
                {
                    songInfo[idx] = fileInfo.Substring(idxStart + 1, idxEnd - idxStart - 1);
                }
            }

            WriteLog($"{songInfo[0]} {songInfo[1]}\r\n{songInfo[2]} [{songInfo[3]}]");
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

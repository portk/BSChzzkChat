using System;
using System.Net;
using System.Windows.Forms;

namespace BSChzzkChat.Src
{
    class SongDownloader
    {
        private WebClient webClient = new WebClient();
        private string baseUrl = "https://api.beatsaver.com/download/key/";
        Uri downloadUri;
        public int progress = 0;
        string directoryPath = GetSettings.CustomSongPath;
        string fileName = "";
        string songCode = "";

        public SongDownloader(int idx)
        {
            // 다운로드 할 곡 코드 받기
            this.songCode = RequestListControl._songList[idx].SongCode;
            // 다운로드 할 곡 이름 받기
            this.fileName = RequestListControl._songList[idx].SongName + ".zip";
            // 다운로드 uri 지정
            downloadUri = new Uri(String.Format($"{baseUrl}{songCode}"));

            SetWebClientEvents();
            FileDownload();
        }

        // 웹 클라이언트에 이벤트 등록하기
        private void SetWebClientEvents()
        {
            // 진행도 변화 이벤트
            webClient.DownloadProgressChanged += (s, e) =>
            {
                // 진행도 받기 용
                progress = e.ProgressPercentage;
            };

            // 다운로드 완료 이벤트
            webClient.DownloadFileCompleted += (s, e) =>
            {
                // 압축해제 실행
                _ = new FileUnZip(songCode);
            };
        }

        // 다운로드 실행
        private void FileDownload()
        {
            try
            {
                // 파일 다운로드
                webClient.DownloadFileAsync(downloadUri, String.Format($@"{directoryPath}/{fileName}"));
            }
            catch (Exception ex)
            {
                if (Form1._logBox.InvokeRequired)
                {
                    if (Form1._logBox.Text.Length != 0)
                    {
                        Form1._logBox.SelectionStart = Form1._chatBox.TextLength;
                    }
                    Form1._logBox.Invoke(new MethodInvoker(delegate
                    {
                        Form1._logBox.AppendText(ex.Message + " 다운로드 에러\r\n");
                    }));
                }
                else
                {
                    if (Form1._logBox.Text.Length != 0)
                    {
                        Form1._logBox.SelectionStart = Form1._chatBox.TextLength;
                    }
                    Form1._logBox.AppendText(ex.Message + " 다운로드 에러\r\n");
                }
            }
        }
    }
}

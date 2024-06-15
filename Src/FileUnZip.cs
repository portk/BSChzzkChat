using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace BSChzzkChat.Src
{
    class FileUnZip
    {
        // 저장 폴더
        string DirectoryPath = GetSettings.CustomSongPath;
        // 압축 해제할 파일 경로
        string filePath = "";

        public FileUnZip(string songCode)
        {
            Run(songCode);
        }

        // 파일 압축 해제 후 파일 삭제
        private void UnZipFile()
        {
            try
            {
                // 압축 해제 경로. 해당 파일명을 이용하여 디렉토리 생성
                string unZipPath = String.Format($"{filePath.Substring(0, filePath.LastIndexOf(".zip"))}");
                // 압축 해제
                ZipFile.ExtractToDirectory(filePath, unZipPath);
                // 파일 삭제
                File.Delete(filePath);
                GetFileInfo(unZipPath);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void Run(string songCode)
        {
            // 파일명 가져오기
            string[] files = Directory.GetFiles(DirectoryPath, String.Format($"{songCode} *.zip"));

            // 파일 유무 검사 후 파일 지정 및 압축해제 실행
            if (files.Length > 0)
            {
                // 파일 지정
                filePath = files[0];
                UnZipFile();

                WriteLog("커스텀 곡 새로고침 후 검색하세요.");
            }
            else
            {
                WriteLog("파일을 찾을 수 없습니다.");
            }
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

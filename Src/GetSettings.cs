using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BSChzzkChat.Src
{
    class GetSettings
    {
        private string filePath = ".\\Settings.ini";

        public static string ChannelId { get; set; } = "";
        private string beatSaberPath = "";
        public static string RequestWord { get; set; } = "!bsr";
        public static int RequestMaxCount { get; set; } = 5;

        public static string CustomSongPath { get; set; } = "";

        // ini파일 사용 준비
        // string
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder reVal, int size, string filePath);
        //int
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string section, string key, int def, string filePath);

        public GetSettings()
        {
            if (File.Exists(filePath))
            {
                ReadSettings();
            }
            else
            {
                MakeSettingFile();
            }
        }

        public void MakeSettingFile()
        {
            // 설정파일 폴더 제작용. 지금은 프로그램과 동일 위치로 되어있어서 필요하지 않다.
            //string dirPath = ".";
            //if (!Directory.Exists(dirPath)) Directory.CreateDirectory(".\\");

            WritePrivateProfileString("Channel", "ChannelId", ChannelId, filePath);
            WritePrivateProfileString("BeatSaber", "BeatSaberFolder", beatSaberPath, filePath);
            WritePrivateProfileString("Requester", "RequestWord", RequestWord, filePath);
            WritePrivateProfileString("Requester", "ReqeustMaxCount", "5", filePath);

            if (Form1._logBox.InvokeRequired)
            {
                Form1._logBox.Invoke(new MethodInvoker(delegate
                {
                    Form1._logBox.AppendText($"{Path.GetFullPath(filePath)} 경로에 세팅파일을 만들었습니다.\r\n세팅 파일을 채워주세요\r\n");
                }));
            }
            else
            {
                Form1._logBox.AppendText($"{Path.GetFullPath(filePath)} 경로에 세팅파일을 만들었습니다.\r\n세팅 파일을 채워주세요\r\n");
            }
        }

        // 설정 값 받기
        public void ReadSettings()
        {
            StringBuilder tmp = new StringBuilder
            {
                Capacity = 300
            };

            // 채널 id
            GetPrivateProfileString("Channel", "ChannelId", "", tmp, tmp.Capacity, filePath);
            ChannelId = tmp.ToString();

            if (ChannelId == "")
            {
                if (Form1._logBox.InvokeRequired)
                {
                    Form1._logBox.Invoke(new MethodInvoker(delegate
                    {
                        Form1._logBox.AppendText("채널아이디를 입력해 주세요\r\n");
                        Form1._logBox.AppendText($"{Path.GetFullPath(filePath)} 파일에서 입력할 수 있습니다.\r\n");
                    }));
                }
                else
                {
                    Form1._logBox.AppendText("채널아이디를 입력해 주세요\r\n");
                    Form1._logBox.AppendText($"{Path.GetFullPath(filePath)} 파일에서 입력할 수 있습니다.\r\n");
                }
            }

            // 비트세이버 경로
            GetPrivateProfileString("BeatSaber", "BeatSaberFolder", "", tmp, tmp.Capacity, filePath);
            beatSaberPath = tmp.ToString();

            if (beatSaberPath == "")
            {
                if (Form1._logBox.InvokeRequired)
                {
                    Form1._logBox.Invoke(new MethodInvoker(delegate
                    {
                        Form1._logBox.AppendText("비트세이버 경로를 입력해 주세요\r\n");
                        Form1._logBox.AppendText($"{Path.GetFullPath(filePath)} 파일에서 입력할 수 있습니다.\r\n");
                    }));
                }
                else
                {
                    Form1._logBox.AppendText("비트세이버 경로를 입력해 주세요\r\n");
                    Form1._logBox.AppendText($"{Path.GetFullPath(filePath)} 파일에서 입력할 수 있습니다.\r\n");
                }
            }
            else
            {
                // 커스텀곡 경로
                CustomSongPath = string.Format($@"{beatSaberPath}\\Beat Saber_Data\\CustomLevels");
            }

            // 신청용 매크로
            GetPrivateProfileString("Requester", "RequestWord", "!bsr", tmp, tmp.Capacity, filePath);
            RequestWord = tmp.ToString() + " ";

            // 곡신청 허용 수
            RequestMaxCount = GetPrivateProfileInt("Requester", "ReqeustMaxCount", 5, filePath);
            // 리스트와 연동하기 편하게 하기 위한 작업
            // list.Count >= RequestMaxCount => close
            // list.Count < RequestMaxCount => open
            RequestMaxCount -= 1;
        }
    }
}

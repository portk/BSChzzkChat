using BSChzzkChat.Src;
using System;
using System.IO;
using System.Windows.Forms;

namespace BSChzzkChat
{
    public partial class Form1 : Form
    {
        public static RichTextBox _chatBox;
        public static ListBox _requestList;
        public static RichTextBox _logBox;
        private ChatClient chatClient = null;

        public Form1()
        {
            InitializeComponent();

            _chatBox = ChatBox;
            _requestList = RequestListBox;
            _logBox = LogBox;

            _ = new GetSettings();

            if (GetSettings.ChannelId != "" && GetSettings.CustomSongPath != "")
            {
                if (Directory.Exists(GetSettings.CustomSongPath))
                {
                    this.chatClient = new ChatClient();
                    _ = chatClient.Init();
                }
                else
                {
                    LogBox.AppendText("비트세이버 폴더 경로를 확인해주세요\r\n경로가 맞더라도 CustomLevels 폴더가 없다면 실행되지 않습니다\r\n");
                }
            }
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            if (RequestListBox.SelectedIndex != -1)
                chatClient.requestListControl.AcceptRequest(RequestListBox.SelectedIndex);
        }

        private void DeclineBtn_Click(object sender, EventArgs e)
        {
            if (RequestListBox.SelectedIndex != -1)
                chatClient.requestListControl.DeclineRequest(RequestListBox.SelectedIndex);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chatClient != null)
                chatClient.CloseClient();
        }

        private void ChatBox_TextChanged(object sender, EventArgs e)
        {
            ChatBox.ScrollToCaret();
        }

        private void LogBox_TextChanged(object sender, EventArgs e)
        {
            LogBox.ScrollToCaret();
        }
    }
}

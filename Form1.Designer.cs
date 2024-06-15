
namespace BSChzzkChat
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.RequestListBox = new System.Windows.Forms.ListBox();
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.DeclineBtn = new System.Windows.Forms.Button();
            this.AcceptBtn = new System.Windows.Forms.Button();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RequestListBox
            // 
            this.RequestListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RequestListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RequestListBox.Font = new System.Drawing.Font("굴림", 15F);
            this.RequestListBox.FormattingEnabled = true;
            this.RequestListBox.ItemHeight = 20;
            this.RequestListBox.Location = new System.Drawing.Point(552, 9);
            this.RequestListBox.Name = "RequestListBox";
            this.RequestListBox.Size = new System.Drawing.Size(443, 282);
            this.RequestListBox.TabIndex = 0;
            // 
            // ChatBox
            // 
            this.ChatBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChatBox.Font = new System.Drawing.Font("굴림", 11F);
            this.ChatBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChatBox.Location = new System.Drawing.Point(4, 9);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ChatBox.ShortcutsEnabled = false;
            this.ChatBox.Size = new System.Drawing.Size(542, 551);
            this.ChatBox.TabIndex = 1;
            this.ChatBox.TabStop = false;
            this.ChatBox.Text = "";
            this.ChatBox.TextChanged += new System.EventHandler(this.ChatBox_TextChanged);
            // 
            // DeclineBtn
            // 
            this.DeclineBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DeclineBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeclineBtn.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold);
            this.DeclineBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DeclineBtn.Location = new System.Drawing.Point(1001, 193);
            this.DeclineBtn.Name = "DeclineBtn";
            this.DeclineBtn.Size = new System.Drawing.Size(75, 75);
            this.DeclineBtn.TabIndex = 13;
            this.DeclineBtn.Text = "삭제";
            this.DeclineBtn.UseVisualStyleBackColor = true;
            this.DeclineBtn.Click += new System.EventHandler(this.DeclineBtn_Click);
            // 
            // AcceptBtn
            // 
            this.AcceptBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AcceptBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AcceptBtn.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold);
            this.AcceptBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AcceptBtn.Location = new System.Drawing.Point(1001, 50);
            this.AcceptBtn.Name = "AcceptBtn";
            this.AcceptBtn.Size = new System.Drawing.Size(75, 75);
            this.AcceptBtn.TabIndex = 12;
            this.AcceptBtn.Text = "수락";
            this.AcceptBtn.UseVisualStyleBackColor = true;
            this.AcceptBtn.Click += new System.EventHandler(this.AcceptBtn_Click);
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.Font = new System.Drawing.Font("굴림", 11F);
            this.LogBox.Location = new System.Drawing.Point(552, 310);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(528, 250);
            this.LogBox.TabIndex = 14;
            this.LogBox.Text = "";
            this.LogBox.TextChanged += new System.EventHandler(this.LogBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.DeclineBtn);
            this.Controls.Add(this.AcceptBtn);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.RequestListBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.RichTextBox ChatBox;
        public System.Windows.Forms.ListBox RequestListBox;
        public System.Windows.Forms.Button DeclineBtn;
        public System.Windows.Forms.Button AcceptBtn;
        public System.Windows.Forms.RichTextBox LogBox;
    }
}


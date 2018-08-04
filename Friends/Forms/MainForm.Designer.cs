namespace Friends.Forms
{
	partial class MainForm
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
			this.label_title = new System.Windows.Forms.Label();
			this.text_userpw = new System.Windows.Forms.TextBox();
			this.text_userid = new System.Windows.Forms.TextBox();
			this.button_login = new System.Windows.Forms.Button();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// label_title
			// 
			this.label_title.AutoSize = true;
			this.label_title.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
			this.label_title.ForeColor = System.Drawing.Color.White;
			this.label_title.Location = new System.Drawing.Point(52, 50);
			this.label_title.Name = "label_title";
			this.label_title.Size = new System.Drawing.Size(281, 30);
			this.label_title.TabIndex = 0;
			this.label_title.Text = "Facebook Friend Grapher";
			// 
			// text_userpw
			// 
			this.text_userpw.Location = new System.Drawing.Point(57, 145);
			this.text_userpw.Name = "text_userpw";
			this.text_userpw.PasswordChar = '*';
			this.text_userpw.Size = new System.Drawing.Size(276, 21);
			this.text_userpw.TabIndex = 1;
			this.text_userpw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_userpw_KeyPress);
			// 
			// text_userid
			// 
			this.text_userid.Location = new System.Drawing.Point(57, 118);
			this.text_userid.Name = "text_userid";
			this.text_userid.Size = new System.Drawing.Size(276, 21);
			this.text_userid.TabIndex = 0;
			this.text_userid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_userid_KeyPress);
			// 
			// button_login
			// 
			this.button_login.Location = new System.Drawing.Point(57, 172);
			this.button_login.Name = "button_login";
			this.button_login.Size = new System.Drawing.Size(276, 23);
			this.button_login.TabIndex = 2;
			this.button_login.Text = "Login";
			this.button_login.UseVisualStyleBackColor = true;
			this.button_login.Click += new System.EventHandler(this.button_login_Click);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
			this.ClientSize = new System.Drawing.Size(384, 261);
			this.Controls.Add(this.button_login);
			this.Controls.Add(this.text_userid);
			this.Controls.Add(this.text_userpw);
			this.Controls.Add(this.label_title);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Facebook Friend Grapher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_title;
		private System.Windows.Forms.TextBox text_userpw;
		private System.Windows.Forms.TextBox text_userid;
		private System.Windows.Forms.Button button_login;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
	}
}


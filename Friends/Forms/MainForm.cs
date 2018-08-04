using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Friends.Dialogs;
using Friends.Library;
using OpenQA.Selenium.Chrome;

namespace Friends.Forms
{
	public partial class MainForm : Form
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

		private Facebook facebook;

		public MainForm()
		{
			InitializeComponent();
			SendMessage(text_userid.Handle, 0x1501, 1, "ID / Email");
			SendMessage(text_userpw.Handle, 0x1501, 1, "Password");
		}

		private void button_login_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(text_userid.Text) || String.IsNullOrWhiteSpace(text_userid.Text))
			{
				text_userid.Focus();
				return;
			}

			if (String.IsNullOrEmpty(text_userpw.Text) || String.IsNullOrWhiteSpace(text_userpw.Text))
			{
				text_userpw.Focus();
				return;
			}

			button_login.Enabled = false;
			text_userid.Enabled = false;
			text_userpw.Enabled = false;
			backgroundWorker1.RunWorkerAsync();
			
		}

		private void text_userid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				button_login.PerformClick();
			}
		}

		private void text_userpw_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				button_login.PerformClick();
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			ChromeDriverService service = ChromeDriverService.CreateDefaultService();
			service.HideCommandPromptWindow = true;
			
			ChromeOptions options = new ChromeOptions();
			options.AddArgument("headless");

			facebook = new Facebook(new ChromeDriver(service, options), true);
			facebook.onBrowserSelectRequest = () =>
			{
				DialogResult result = MessageBox.Show("Save Browser?", "", MessageBoxButtons.YesNo);
				return result == DialogResult.Yes;
			};
			facebook.onSecondAuthCodeRequest = () =>
			{
				SecondAuthDialog dialog = new SecondAuthDialog();
				dialog.ShowInTaskbar = false;
				dialog.ShowDialog();
				return dialog.Code;
			};
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = facebook.Login(text_userid.Text, text_userpw.Text);
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if ((Boolean)e.Result == true)
			{
				GraphForm graph = new GraphForm(facebook);
				Hide();
				graph.ShowDialog(this);
				Show();
			}
			else
			{
				MessageBox.Show("Login Failed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			button_login.Enabled = true;
			text_userid.Enabled = true;
			text_userpw.Enabled = true;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			facebook.Logout();
		}
	}
}

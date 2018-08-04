using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Friends.Dialogs;
using Friends.Library;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Friends.Forms
{
	public partial class GraphForm : Form
	{
		private delegate void SetStatusText(String Text);
		private delegate void SetProgress(int progress, int total);
		private delegate void ReleaseButton();

		private SetStatusText setStatusText;
		private SetProgress setProgress;
		private ReleaseButton releaseButton;

		private Facebook _facebook;

		private ChromiumWebBrowser cefBrowser;

		public GraphForm(Facebook facebook)
		{
			InitializeComponent();
			InitBrowser();
			
			setStatusText = new SetStatusText(setStatusTextMethod);
			setProgress = new SetProgress(setProgressMethod);
			releaseButton = new ReleaseButton(releaseButtonMethod);

			_facebook = facebook;

			SimpleServer server = new SimpleServer();
			server.Run();
		}

		private void InitBrowser()
		{
			Cef.Initialize(new CefSettings());
			cefBrowser = new ChromiumWebBrowser("about:blank");
			panel_view.Controls.Add(cefBrowser);
			cefBrowser.Dock = DockStyle.Fill;
		}

		private void button_start_Click(object sender, EventArgs e)
		{
			button_start.Enabled = false;

			new Thread(() =>
			{
				Invoke(setStatusText, "Loading Friend List");

				List<User> friendList = _facebook.GetFriendsList();
				List<Tuple<User, User>> link = new List<Tuple<User, User>>();

				foreach (User user in friendList)
				{
					int index = friendList.IndexOf(user);
					Invoke(setStatusText,
						String.Format("Loading Mutal Friend : {0}/{1}", index, friendList.Count + 1));
					Invoke(setProgress, index, friendList.Count + 1);
					link.Add(new Tuple<User, User>(_facebook.User, user));
					foreach (User muser in _facebook.GetMutualFriendList(user))
					{
						if (link.Contains(new Tuple<User, User>(_facebook.User, muser)))
						{
							continue;
						}

						link.Add(new Tuple<User, User>(user, muser));
					}
				}

				String jsonPath = Path.Combine(Environment.CurrentDirectory, "src/result.json");

				if (File.Exists(jsonPath))
				{
					Invoke(setStatusText, "Existing File Found, Deleting");
					File.Delete(jsonPath);
				}

				Invoke(setStatusText, "Writing Result to JSON");
				StreamWriter writer = File.CreateText(jsonPath);
				writer.Write("{");
				writer.Write("\"nodes\": [");
				writer.Write("{\"id\":\"" + _facebook.User.ID + "\",\"name\": \"Me\"}");
				foreach (User user in friendList) {
					if (user.ID.Equals("-1")) {
						continue;
					}
					writer.Write(",{\"id\":\"" + user.ID + "\",\"name\":\"" + user.Name + "\"}");
				}
				writer.Write("],\"links\": [");
				foreach (Tuple<User, User> pair in link) {
					if (link.IndexOf(pair) != 0) {
						writer.Write(",");
					}
					writer.Write("{\"source\":\""
						+ pair.Item1.ID
						+ "\",\"target\":\""
						+ pair.Item2.ID
						+ "\"}");
				}
				writer.Write("]}");
				writer.Flush();
				writer.Close();
				
				Invoke(setStatusText, "Loding to Web Browser");
				cefBrowser.Load("http://localhost:30113/src/index.html");
				Invoke(setStatusText, "Finished");
				Invoke(setProgress, 0, 1);
				Invoke(releaseButton);
			}).Start();
		}

		private void setStatusTextMethod(String text)
		{
			label_status.Text = text;
		}

		private void setProgressMethod(int progress, int total)
		{
			progress_total.Maximum = total;
			progress_total.Value = progress;
			progress_total.Step = 1;
		}

		private void releaseButtonMethod()
		{
			button_start.Enabled = true;
		}

		private void progress_total_Click(object sender, EventArgs e)
		{

		}

		private void button_load_Click(object sender, EventArgs e)
		{
			cefBrowser.Load("http://localhost:30113/src/index.html");

		}
	}
}

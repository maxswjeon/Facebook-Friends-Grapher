using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Friends.Forms;

namespace Friends.Dialogs
{
	public partial class SettingsDialog : Form
	{
		public Settings setting;

		public SettingsDialog(Settings setting)
		{
			InitializeComponent();
			this.setting = setting;
		}

		private void SettingsDialog_Load(object sender, EventArgs e)
		{
			numericUpDown1.Value = setting.threadCount;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			setting.threadCount = (int)numericUpDown1.Value;
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}

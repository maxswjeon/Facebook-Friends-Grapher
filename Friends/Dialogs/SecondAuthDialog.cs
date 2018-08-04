using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Friends.Forms
{
	public partial class SecondAuthDialog : Form
	{
		public String Code = "";
		public SecondAuthDialog()
		{
			InitializeComponent();
		}

		private void SecondAuthDialog_Load(object sender, EventArgs e)
		{

		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			Code = textBox1.Text;
			Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			Code = "";
			Close();
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				button_ok.PerformClick();
			}
		}
	}
}

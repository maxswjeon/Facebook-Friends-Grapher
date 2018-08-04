using System.Windows.Forms;

namespace Friends.Forms
{
	partial class GraphForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.progress_total = new System.Windows.Forms.ProgressBar();
			this.panel_title = new System.Windows.Forms.Panel();
			this.button_load = new System.Windows.Forms.Button();
			this.button_start = new System.Windows.Forms.Button();
			this.label_title = new System.Windows.Forms.Label();
			this.panel_view = new System.Windows.Forms.Panel();
			this.label_status = new System.Windows.Forms.Label();
			this.panel_status = new System.Windows.Forms.Panel();
			this.panel_title.SuspendLayout();
			this.panel_status.SuspendLayout();
			this.SuspendLayout();
			// 
			// progress_total
			// 
			this.progress_total.Location = new System.Drawing.Point(367, 0);
			this.progress_total.Name = "progress_total";
			this.progress_total.Size = new System.Drawing.Size(367, 23);
			this.progress_total.TabIndex = 0;
			this.progress_total.Click += new System.EventHandler(this.progress_total_Click);
			// 
			// panel_title
			// 
			this.panel_title.Controls.Add(this.button_load);
			this.panel_title.Controls.Add(this.button_start);
			this.panel_title.Controls.Add(this.label_title);
			this.panel_title.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel_title.Location = new System.Drawing.Point(0, 0);
			this.panel_title.Name = "panel_title";
			this.panel_title.Size = new System.Drawing.Size(734, 30);
			this.panel_title.TabIndex = 1;
			// 
			// button_load
			// 
			this.button_load.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.button_load.Location = new System.Drawing.Point(656, 0);
			this.button_load.Name = "button_load";
			this.button_load.Size = new System.Drawing.Size(75, 30);
			this.button_load.TabIndex = 4;
			this.button_load.Text = "Load";
			this.button_load.UseVisualStyleBackColor = true;
			this.button_load.Click += new System.EventHandler(this.button_load_Click);
			// 
			// button_start
			// 
			this.button_start.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.button_start.Location = new System.Drawing.Point(575, 0);
			this.button_start.Name = "button_start";
			this.button_start.Size = new System.Drawing.Size(75, 30);
			this.button_start.TabIndex = 3;
			this.button_start.Text = "Start";
			this.button_start.UseVisualStyleBackColor = true;
			this.button_start.Click += new System.EventHandler(this.button_start_Click);
			// 
			// label_title
			// 
			this.label_title.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label_title.Location = new System.Drawing.Point(0, 0);
			this.label_title.Name = "label_title";
			this.label_title.Size = new System.Drawing.Size(252, 30);
			this.label_title.TabIndex = 2;
			this.label_title.Text = "Facebook Friend Grapher";
			// 
			// panel_view
			// 
			this.panel_view.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_view.Location = new System.Drawing.Point(0, 30);
			this.panel_view.Name = "panel_view";
			this.panel_view.Size = new System.Drawing.Size(734, 408);
			this.panel_view.TabIndex = 2;
			// 
			// label_status
			// 
			this.label_status.Dock = System.Windows.Forms.DockStyle.Left;
			this.label_status.Font = new System.Drawing.Font("맑은 고딕", 11F);
			this.label_status.Location = new System.Drawing.Point(0, 0);
			this.label_status.Name = "label_status";
			this.label_status.Size = new System.Drawing.Size(364, 23);
			this.label_status.TabIndex = 3;
			this.label_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel_status
			// 
			this.panel_status.Controls.Add(this.progress_total);
			this.panel_status.Controls.Add(this.label_status);
			this.panel_status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel_status.Location = new System.Drawing.Point(0, 438);
			this.panel_status.Name = "panel_status";
			this.panel_status.Size = new System.Drawing.Size(734, 23);
			this.panel_status.TabIndex = 4;
			// 
			// GraphForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(734, 461);
			this.Controls.Add(this.panel_view);
			this.Controls.Add(this.panel_title);
			this.Controls.Add(this.panel_status);
			this.Name = "GraphForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GraphForm";
			this.panel_title.ResumeLayout(false);
			this.panel_status.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar progress_total;
		private System.Windows.Forms.Panel panel_title;
		private System.Windows.Forms.Button button_start;
		private System.Windows.Forms.Label label_title;
		private System.Windows.Forms.Panel panel_view;
		private System.Windows.Forms.Label label_status;
		private System.Windows.Forms.Panel panel_status;
		private Button button_load;
	}
}
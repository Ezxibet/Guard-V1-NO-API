namespace DragonGuard
{
	// Token: 0x02000003 RID: 3
	public partial class MainFrm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000045F8 File Offset: 0x000027F8
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004630 File Offset: 0x00002830
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DragonGuard.MainFrm));
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			this.label3 = new global::System.Windows.Forms.Label();
			this.timer2 = new global::System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.kapatToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new global::System.Windows.Forms.Label();
			this.timer3 = new global::System.Windows.Forms.Timer(this.components);
			this.timer10 = new global::System.Windows.Forms.Timer(this.components);
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Lucida Sans Unicode", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = global::System.Drawing.SystemColors.ControlLightLight;
			this.label2.Location = new global::System.Drawing.Point(178, 115);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(204, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "(PointBlank) Anti HACK System!";
			this.label2.TextChanged += new global::System.EventHandler(this.label2_TextChanged);
			this.label2.Click += new global::System.EventHandler(this.label2_Click);
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(31, 39, 46);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Location = new global::System.Drawing.Point(-4, 150);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(529, 11);
			this.panel1.TabIndex = 4;
			this.panel2.BackColor = global::System.Drawing.Color.Blue;
			this.panel2.Location = new global::System.Drawing.Point(3, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(226, 11);
			this.panel2.TabIndex = 5;
			this.timer1.Interval = 50;
			this.timer1.Tick += new global::System.EventHandler(this.Timer1_Tick_1);
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Lucida Sans Unicode", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.ForeColor = global::System.Drawing.SystemColors.ControlLightLight;
			this.label3.Location = new global::System.Drawing.Point(211, 131);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(84, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Loading... 0%";
			this.timer2.Interval = 10;
			this.timer2.Tick += new global::System.EventHandler(this.Timer2_Tick);
			this.contextMenuStrip1.BackColor = global::System.Drawing.Color.White;
			this.contextMenuStrip1.Font = new global::System.Drawing.Font("Lucida Sans Unicode", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.kapatToolStripMenuItem
			});
			this.contextMenuStrip1.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.RenderMode = global::System.Windows.Forms.ToolStripRenderMode.Professional;
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(111, 28);
			this.kapatToolStripMenuItem.BackColor = global::System.Drawing.SystemColors.Control;
			this.kapatToolStripMenuItem.Font = new global::System.Drawing.Font("Lucida Sans Unicode", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.kapatToolStripMenuItem.ForeColor = global::System.Drawing.Color.FromArgb(11, 29, 36);
			this.kapatToolStripMenuItem.Image = global::DragonGuard.Properties.Resources.mark;
			this.kapatToolStripMenuItem.ImageAlign = global::System.Drawing.ContentAlignment.TopRight;
			this.kapatToolStripMenuItem.ImageTransparentColor = global::System.Drawing.Color.Transparent;
			this.kapatToolStripMenuItem.Margin = new global::System.Windows.Forms.Padding(1);
			this.kapatToolStripMenuItem.Name = "kapatToolStripMenuItem";
			this.kapatToolStripMenuItem.Size = new global::System.Drawing.Size(110, 22);
			this.kapatToolStripMenuItem.Text = "Close";
			this.kapatToolStripMenuItem.Click += new global::System.EventHandler(this.KapatToolStripMenuItem_Click);
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.SystemColors.Control;
			this.label1.Location = new global::System.Drawing.Point(95, 174);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(284, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Copyright © 2022 PointBlank - ALL RIGHTS RESERVED. ";
			this.timer3.Enabled = true;
			this.timer3.Interval = 2300;
			this.timer3.Tick += new global::System.EventHandler(this.timer3_Tick);
			this.timer10.Tick += new global::System.EventHandler(this.timer10_Tick);
			this.pictureBox1.BackgroundImage = global::DragonGuard.Properties.Resources.main1;
			this.pictureBox1.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Image = (global::System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(-4, -1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(529, 95);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new global::System.EventHandler(this.pictureBox1_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			this.BackColor = global::System.Drawing.Color.FromArgb(11, 29, 36);
			base.ClientSize = new global::System.Drawing.Size(524, 187);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.pictureBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (global::System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "MainFrm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
			base.Load += new global::System.EventHandler(this.Form1_Load);
			base.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
			base.Resize += new global::System.EventHandler(this.MainFrm_Resize);
			this.panel1.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000009 RID: 9
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400000B RID: 11
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400000C RID: 12
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x0400000D RID: 13
		private global::System.Windows.Forms.Timer timer1;

		// Token: 0x0400000E RID: 14
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.Timer timer2;

		// Token: 0x04000010 RID: 16
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000011 RID: 17
		private global::System.Windows.Forms.ToolStripMenuItem kapatToolStripMenuItem;

		// Token: 0x04000012 RID: 18
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000013 RID: 19
		private global::System.Windows.Forms.Timer timer3;

		// Token: 0x04000014 RID: 20
		private global::System.Windows.Forms.Timer timer10;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.PictureBox pictureBox1;
	}
}

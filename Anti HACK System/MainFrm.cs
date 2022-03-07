using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DragonGuard.classes;
using DragonGuard.Classes;
using DragonGuard.Properties;
using Microsoft.Win32;

namespace DragonGuard
{
	// Token: 0x02000003 RID: 3
	public partial class MainFrm : Form
	{
		// Token: 0x06000006 RID: 6
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		private void AddHostsFileLine(string url)
		{
			try
			{
				foreach (string value in new WebClient().DownloadString(url).Split(new string[]
				{
					"<br>",
					"</br>"
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					FileStream fileStream = new FileStream("C:\\\\Windows\\\\System32\\\\drivers\\\\etc\\\\hosts", FileMode.OpenOrCreate, FileAccess.Write);
					StreamWriter streamWriter = new StreamWriter(fileStream);
					streamWriter.WriteLine(value);
					streamWriter.Flush();
					streamWriter.Close();
					fileStream.Close();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E0 File Offset: 0x000003E0
		private string PerformRequest(string url, string RegHWID)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			CookieContainer cookieContainer = null;
			httpWebRequest.CookieContainer = cookieContainer;
			string s = "&RegHWID=" + RegHWID;
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
			httpWebRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
			}
			return new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream()).ReadToEnd();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002298 File Offset: 0x00000498
		public static string RegHWID()
		{
			string result;
			try
			{
				bool flag = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ubisofty\\XD") == null;
				if (flag)
				{
					bool flag2 = Process.GetProcessesByName("PointBlank").Length < 1;
					if (flag2)
					{
						KillProtection.onDisable();
						Application.Exit();
					}
					else
					{
						foreach (Process process in Process.GetProcesses())
						{
							bool flag3 = process.ProcessName == "PointBlank";
							if (flag3)
							{
								process.Kill();
								KillProtection.onDisable();
								Application.Exit();
								Environment.Exit(1);
							}
						}
					}
				}
				result = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ubisofty\\XD", true).GetValue("HardwareID", true).ToString();
			}
			catch
			{
				bool flag4 = Process.GetProcessesByName("PointBlank").Length < 1;
				if (flag4)
				{
					KillProtection.onDisable();
					Application.Exit();
				}
				else
				{
					foreach (Process process2 in Process.GetProcesses())
					{
						bool flag5 = process2.ProcessName == "PointBlank";
						if (flag5)
						{
							process2.Kill();
							KillProtection.onDisable();
							Application.Exit();
							Environment.Exit(1);
						}
					}
				}
				result = "null";
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002408 File Offset: 0x00000608
		public MainFrm()
		{
			KillProtection.onEnable();
			this.AddHostsFileLine(Modul.WEB + "launcher/Hosts.php");
			this.Text = "";
			base.CreateParams.ExStyle |= 0;
			base.TopMost = true;
			ProcessModule mainModule = Process.GetCurrentProcess().MainModule;
			this.objKeyboardProcess = new MainFrm.LowLevelKeyboardProc(this.CaptureKey);
			this.ptrHook = MainFrm.SetWindowsHookEx(13, this.objKeyboardProcess, MainFrm.GetModuleHandle(mainModule.ModuleName), 0U);
			this.MyIcon.Icon = Resources.icon;
			base.ShowInTaskbar = false;
			this.InitializeComponent();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000024C8 File Offset: 0x000006C8
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 128;
				return createParams;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024F8 File Offset: 0x000006F8
		private IntPtr CaptureKey(int nCode, IntPtr wp, IntPtr lp)
		{
			bool flag = nCode >= 0;
			if (flag)
			{
				MainFrm.KeyboardDLLStruct keyboardDLLStruct = (MainFrm.KeyboardDLLStruct)Marshal.PtrToStructure(lp, typeof(MainFrm.KeyboardDLLStruct));
				bool flag2 = keyboardDLLStruct.key == Keys.F1 || keyboardDLLStruct.key == Keys.F5 || keyboardDLLStruct.key == Keys.F6 || keyboardDLLStruct.key == Keys.F7 || keyboardDLLStruct.key == Keys.F9 || keyboardDLLStruct.key == Keys.F10 || keyboardDLLStruct.key == Keys.F11 || keyboardDLLStruct.key == Keys.F12 || keyboardDLLStruct.key == Keys.Insert || keyboardDLLStruct.key == Keys.Delete || keyboardDLLStruct.key == Keys.Alt || keyboardDLLStruct.key == Keys.Home || keyboardDLLStruct.key == Keys.End || keyboardDLLStruct.key == Keys.Right;
				if (flag2)
				{
					return (IntPtr)1;
				}
			}
			return MainFrm.CallNextHookEx(this.ptrHook, nCode, wp, lp);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025E1 File Offset: 0x000007E1
		[STAThread]
		private static void Main()
		{
			Application.Run(new MainFrm());
		}

		// Token: 0x0600000E RID: 14
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int id, MainFrm.LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);

		// Token: 0x0600000F RID: 15
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool UnhookWindowsHookEx(IntPtr hook);

		// Token: 0x06000010 RID: 16
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);

		// Token: 0x06000011 RID: 17
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string name);

		// Token: 0x06000012 RID: 18
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern short GetAsyncKeyState(Keys key);

		// Token: 0x06000013 RID: 19 RVA: 0x000025F0 File Offset: 0x000007F0
		private void Form1_Load(object sender, EventArgs e)
		{
			foreach (Process process in Process.GetProcesses())
			{
				bool flag = process.ProcessName == "Guard";
				if (flag)
				{
					process.Kill();
				}
			}
			this.Text = "";
			Listener.SKeyEventHandler += this.KeyboardListener_s_KeyEventHandler;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Region = Region.FromHrgn(MainFrm.CreateRoundRectRgn(0, 0, base.Width, base.Height, 20, 20));
			base.StartPosition = FormStartPosition.Manual;
			base.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - base.Width, Screen.PrimaryScreen.WorkingArea.Height - base.Height);
			base.Opacity = 0.1;
			this.timer10.Start();
			this.timer2.Start();
			this.timer1.Start();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026FB File Offset: 0x000008FB
		private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026FB File Offset: 0x000008FB
		private void Form1_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002700 File Offset: 0x00000900
		private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
			KillProtection.onDisable();
			bool flag = Process.GetProcessesByName("PointBlank").Length < 1;
			if (flag)
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				KillProtection.onDisable();
				Application.Exit();
			}
			else
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				foreach (Process process in Process.GetProcesses())
				{
					bool flag2 = process.ProcessName == "PointBlank";
					if (flag2)
					{
						process.Kill();
						KillProtection.onDisable();
						Application.Exit();
						Environment.Exit(1);
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027D4 File Offset: 0x000009D4
		private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
			KillProtection.onDisable();
			bool flag = Process.GetProcessesByName("PointBlank").Length < 1;
			if (flag)
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				KillProtection.onDisable();
				Application.Exit();
			}
			else
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				foreach (Process process in Process.GetProcesses())
				{
					bool flag2 = process.ProcessName == "PointBlank";
					if (flag2)
					{
						process.Kill();
						KillProtection.onDisable();
						Application.Exit();
						Environment.Exit(1);
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000028A8 File Offset: 0x00000AA8
		private void MainFrm_Resize(object sender, EventArgs e)
		{
			bool flag = base.WindowState == FormWindowState.Minimized;
			if (flag)
			{
				base.Hide();
				this.MyIcon.Visible = true;
				this.MyIcon.Text = "PointBlankGuardV1";
				this.MyIcon.BalloonTipTitle = "PointBlankGuard Information";
				this.MyIcon.BalloonTipText = "The program continues to run in the background.";
				this.MyIcon.BalloonTipIcon = ToolTipIcon.Info;
				this.MyIcon.ShowBalloonTip(5000);
				this.MyIcon.MouseDoubleClick += this.MyIcon_MouseDoubleClick;
				this.MyIcon.ContextMenuStrip = this.contextMenuStrip1;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002958 File Offset: 0x00000B58
		public void MyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.MyIcon.Text = "PointBlankGuardV1";
			this.MyIcon.BalloonTipTitle = "PointBlank Information";
			this.MyIcon.BalloonTipText = "[PointBlankGuard] is our active game protection in an optimized way...";
			this.MyIcon.BalloonTipIcon = ToolTipIcon.Info;
			this.MyIcon.ShowBalloonTip(5000);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029B8 File Offset: 0x00000BB8
		private void StartPBEXE(string url)
		{
			try
			{
				string text = new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream()).ReadToEnd();
				int startIndex = text.IndexOf("<Key>") + 5;
				int length = text.Substring(startIndex).IndexOf("</Key>");
				string text2 = text.Substring(startIndex, length);
				this.LauncherKey = text2.ToString();
				bool flag = File.Exists("Guard.exe");
				if (flag)
				{
					Process[] processesByName = Process.GetProcessesByName("Guard");
					bool flag2 = processesByName.Length >= 1;
					if (flag2)
					{
						Process[] array = processesByName;
						for (int i = 0; i < array.Length; i++)
						{
							array[i].Kill();
							this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
							MessageBox.Show("Guard.exe Not Found! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							KillProtection.onDisable();
							Application.Exit();
						}
					}
					Process.Start(Application.StartupPath + "\\Guard.exe", this.LauncherKey);
					this.PerformRequest(Modul.WEB + "launcher/add.php", MainFrm.RegHWID());
					this.DizinIceriginiListeyeEkle(string.Concat(new string[]
					{
						Application.StartupPath + "\\Pack"
					}));
				}
				else
				{
					this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
					Process[] processesByName2 = Process.GetProcessesByName("PointBlank");
					bool flag3 = processesByName2.Length >= 1;
					if (flag3)
					{
						Process[] array2 = processesByName2;
						for (int j = 0; j < array2.Length; j++)
						{
							array2[j].Kill();
							this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
							MessageBox.Show("Guard.exe Not Found! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							KillProtection.onDisable();
							Application.Exit();
						}
					}
					else
					{
						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
						MessageBox.Show("Guard.exe not found! ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						KillProtection.onDisable();
						Application.Exit();
					}
				}
			}
			catch (Exception ex)
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				Process[] processesByName3 = Process.GetProcessesByName("Point Blank");
				bool flag4 = processesByName3.Length >= 1;
				if (flag4)
				{
					foreach (Process process in processesByName3)
					{
						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
						process.Kill();
						MessageBox.Show(string.Format("An unknown error has occurred.\nERROR: {0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						KillProtection.onDisable();
						Application.Exit();
					}
				}
				else
				{
					this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
					MessageBox.Show(string.Format("An unknown error has occurred.\nERROR: {0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					KillProtection.onDisable();
					Application.Exit();
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D04 File Offset: 0x00000F04
		private void Timer1_Tick_1(object sender, EventArgs e)
		{
			this.counter++;
			this.panel2.Left += 4;
			bool flag = this.panel2.Left > 510;
			if (flag)
			{
				this.panel2.Left = 176;
			}
			bool flag2 = this.counter > 0;
			if (flag2)
			{
				bool flag3 = this.counter < 100;
				if (flag3)
				{
					this.label3.Text = string.Format("LOADING... {0}%", this.counter);
				}
				bool flag4 = this.counter == 100;
				if (flag4)
				{
					this.label3.Text = string.Format("Completed! {0}%", this.counter);
					this.StartPBEXE(Modul.WEB + "launcher/keygennX.php");
					base.WindowState = FormWindowState.Minimized;
					this.timer1.Stop();
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002DFC File Offset: 0x00000FFC
		private void Timer2_Tick(object sender, EventArgs e)
		{
			bool flag = base.Opacity <= 1.0;
			if (flag)
			{
				base.Opacity += 0.025;
			}
			else
			{
				this.timer2.Stop();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E48 File Offset: 0x00001048
		private void KapatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = MessageBox.Show("Are you sure you want to exit? The game will be closed when you exit.", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
			if (flag)
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				KillProtection.onDisable();
				Process[] array = Process.GetProcessesByName("Guard");
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Kill();
				}
				this.check = true;
				bool flag2 = Process.GetProcessesByName("PointBlank").Length < 1;
				if (flag2)
				{
					Application.Exit();
				}
				else
				{
					foreach (Process process in Process.GetProcesses())
					{
						bool flag3 = process.ProcessName == "PointBlank";
						if (flag3)
						{
							process.Kill();
							KillProtection.onDisable();
							Application.Exit();
							Environment.Exit(1);
						}
					}
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026FB File Offset: 0x000008FB
		private void label2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026FB File Offset: 0x000008FB
		private void label2_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002F3C File Offset: 0x0000113C
		private void GetProcName()
		{
			Process[] processesByName = Process.GetProcessesByName("taskmgr");
			bool flag = processesByName.Length >= 1;
			if (flag)
			{
				Process[] array = processesByName;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Kill();
				}
			}
			try
			{
				Process[] processes = Process.GetProcesses();
				string machineName = Environment.MachineName;
				string userName = Environment.UserName;
				foreach (Process process in processes)
				{
					bool flag2 = !string.IsNullOrEmpty(process.MainWindowTitle);
					if (flag2)
					{
						string text = process.MainWindowTitle.Replace("Process", "@#$");
						string text2 = process.MainWindowTitle.Replace("Suspender", "@#$");
						string text3 = process.MainWindowTitle.Replace("Leheman1 Suspender", "@#$");
						string text4 = process.MainWindowTitle.Replace("(Administrator)", "@#$");
						string text5 = process.MainWindowTitle.Replace("Window Title Changer by MurGee.com", "@#$");
						string text6 = process.MainWindowTitle.Replace("MPGH ForeverRed Universal PB's Hack ", "@#$");
						string text7 = process.MainWindowTitle.Replace(" MPGH ForeverRed Universal PB's Hack", "@#$");
						string text8 = process.MainWindowTitle.Replace("Cheat Engine", "@#$");
						string text9 = process.MainWindowTitle.Replace("Memory Viewer", "@#$");
						string text10 = process.MainWindowTitle.Replace("Hack", "@#$");
						string text11 = process.MainWindowTitle.Replace("Hacker", "@#$");
						string text12 = process.MainWindowTitle.Replace(string.Concat(new string[]
						{
							"Process Hacker [",
							machineName,
							"\\",
							userName,
							"]"
						}), "@#$");
						string text13 = process.MainWindowTitle.Replace("Resource Hacker ", "@#$");
						string text14 = process.MainWindowTitle.Replace("NetLimiter 4 ", "@#$");
						string text15 = process.MainWindowTitle.Replace("Task Manager", "@#$");
						string text16 = process.MainWindowTitle.Replace("Görev Yöneticisi", "@#$");
						string text17 = process.MainWindowTitle.Replace("Process Hacker", "@#$");
						string text18 = process.MainWindowTitle.Replace("Manager", "@#$");
						string text19 = process.MainWindowTitle.Replace("Title", "@#$");
						string text20 = process.MainWindowTitle.Replace("Changer", "@#$");
						string text21 = process.MainWindowTitle.Replace("MPGH ForeverRed Universal PB's Hack", "@#$");
						string text22 = process.MainWindowTitle.Replace("injector", "@#$");
						string text23 = process.MainWindowTitle.Replace(" Engine", "@#$");
						string text24 = process.MainWindowTitle.Replace(" Engine ", "@#$");
						string text25 = process.MainWindowTitle.Replace("Engine ", "@#$");
						string text26 = process.MainWindowTitle.Replace(string.Concat(new string[]
						{
							"Process Hacker [",
							machineName,
							"\\",
							userName,
							"]+"
						}), "@#$");
						string text27 = process.MainWindowTitle.Replace("System Explorer 6.4.3.5352", "@#$");
						string text28 = process.MainWindowTitle.Replace("System Explorer ", "@#$");
						string text29 = process.MainWindowTitle.Replace("AnVir", "@#$");
						string text30 = process.MainWindowTitle.Replace("AnVir ", "@#$");
						string text31 = process.MainWindowTitle.Replace("N-Jector by NOOB", "@#$");
						string text32 = process.MainWindowTitle.Replace("N-Jector by NOOB ", "@#$");
						string text33 = process.MainWindowTitle.Replace(string.Concat(new string[]
						{
							"Process Explorer - Sysinternals: www.sysinternals.com [",
							machineName,
							"\\",
							userName,
							"]"
						}), "@#$");
						string text34 = process.MainWindowTitle.Replace("Resource Monitor", "@#$");
						string text35 = process.MainWindowTitle.Replace("Resource Monitor ", "@#$");
						string text36 = process.MainWindowTitle.Replace("X-Mouse Button Control - ", "@#$");
						string text37 = process.MainWindowTitle.Replace("X-Mouse Button Control ", "@#$");
						string text38 = process.MainWindowTitle.Replace("X-Mouse Button Control - Setup ", "@#$");
						string text39 = process.MainWindowTitle.Replace("X-Mouse Button Control - Setup v2.19.1", "@#$");
						string text40 = process.MainWindowTitle.Replace("Speed AutoClicker", "@#$");
						string text41 = process.MainWindowTitle.Replace("SpeedAutoClicker", "@#$");
						string a = text.Substring(0, 3);
						string a2 = text2.Substring(0, 3);
						string a3 = text3.Substring(0, 3);
						string a4 = text4.Substring(0, 3);
						string a5 = text5.Substring(0, 3);
						string a6 = text6.Substring(0, 3);
						string a7 = text7.Substring(0, 3);
						string a8 = text8.Substring(0, 3);
						string a9 = text9.Substring(0, 3);
						string a10 = text10.Substring(0, 3);
						string a11 = text11.Substring(0, 3);
						string a12 = text12.Substring(0, 3);
						string a13 = text13.Substring(0, 3);
						string a14 = text14.Substring(0, 3);
						string a15 = text15.Substring(0, 3);
						string a16 = text16.Substring(0, 3);
						string a17 = text17.Substring(0, 3);
						string a18 = text18.Substring(0, 3);
						string a19 = text19.Substring(0, 3);
						string a20 = text20.Substring(0, 3);
						string a21 = text21.Substring(0, 3);
						string a22 = text22.Substring(0, 3);
						string a23 = text23.Substring(0, 3);
						string a24 = text24.Substring(0, 3);
						string a25 = text25.Substring(0, 3);
						string a26 = text26.Substring(0, 3);
						string a27 = text27.Substring(0, 3);
						string a28 = text28.Substring(0, 3);
						string a29 = text29.Substring(0, 3);
						string a30 = text30.Substring(0, 3);
						string a31 = text31.Substring(0, 3);
						string a32 = text32.Substring(0, 3);
						string a33 = text33.Substring(0, 3);
						string a34 = text34.Substring(0, 3);
						string a35 = text35.Substring(0, 3);
						string a36 = text36.Substring(0, 3);
						string a37 = text37.Substring(0, 3);
						string a38 = text38.Substring(0, 3);
						string a39 = text39.Substring(0, 3);
						string a40 = text40.Substring(0, 3);
						string a41 = text41.Substring(0, 3);
						bool flag3 = a == "@#$" || a2 == "@#$" || a3 == "@#$" || a4 == "@#$" || a5 == "@#$" || a6 == "@#$" || a7 == "@#$" || a8 == "@#$" || a9 == "@#$" || a10 == "@#$" || a11 == "@#$" || a12 == "@#$" || a13 == "@#$" || a14 == "@#$" || a15 == "@#$" || a16 == "@#$" || a17 == "@#$" || a18 == "@#$" || a19 == "@#$" || a20 == "@#$" || a21 == "@#$" || a22 == "@#$" || a23 == "@#$" || a24 == "@#$" || a25 == "@#$" || a26 == "@#$" || a27 == "@#$" || a28 == "@#$" || a29 == "@#$" || a30 == "@#$" || a31 == "@#$" || a32 == "@#$" || a33 == "@#$" || a34 == "@#$" || a35 == "@#$" || a36 == "@#$" || a37 == "@#$" || a38 == "@#$" || a39 == "@#$" || a40 == "@#$" || a41 == "@#$";
						if (flag3)
						{
							bool flag4 = Process.GetProcessesByName("PointBlank").Length < 1;
							if (flag4)
							{
								this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
								KillProtection.onDisable();
								Application.Exit();
							}
							else
							{
								this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
								foreach (Process process2 in Process.GetProcesses())
								{
									bool flag5 = process2.ProcessName == "PointBlank";
									if (flag5)
									{
										process2.Kill();
										KillProtection.onDisable();
										Application.Exit();
										Environment.Exit(1);
									}
								}
							}
						}
					}
				}
			}
			catch
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				KillProtection.onDisable();
				foreach (Process process3 in Process.GetProcesses())
				{
					bool flag6 = process3.ProcessName == "PointBlank";
					if (flag6)
					{
						process3.Kill();
						KillProtection.onDisable();
						Application.Exit();
						Environment.Exit(1);
					}
					else
					{
						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
						KillProtection.onDisable();
						Application.Exit();
						Environment.Exit(1);
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003A20 File Offset: 0x00001C20
		private void DizinIceriginiListeyeEkle(string dizin)
		{
			try
			{
				string[] files = Directory.GetFiles(dizin);
				for (int i = 0; i < files.Length; i++)
				{
					FileInfo fileInfo = new FileInfo(files[i]);
					long length = fileInfo.Length;
					string name = fileInfo.Name;
					bool flag = length == 8142852L;
					if (flag)
					{
						bool flag2 = Process.GetProcessesByName("PointBlank").Length < 1;
						if (flag2)
						{
							this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
							Application.Exit();
						}
						else
						{
							this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
							foreach (Process process in Process.GetProcesses())
							{
								bool flag3 = process.ProcessName == "PointBlank";
								if (flag3)
								{
									process.Kill();
									Application.Exit();
									Environment.Exit(1);
								}
							}
						}
					}
				}
			}
			catch
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				KillProtection.onDisable();
				foreach (Process process2 in Process.GetProcesses())
				{
					bool flag4 = process2.ProcessName == "PointBlank";
					if (flag4)
					{
						process2.Kill();
						KillProtection.onDisable();
						Application.Exit();
						Environment.Exit(1);
					}
					else
					{
						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
						KillProtection.onDisable();
						Application.Exit();
						Environment.Exit(1);
					}
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003BF0 File Offset: 0x00001DF0
		private void timer3_Tick(object sender, EventArgs e)
		{
			this.GetProcName();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003BFC File Offset: 0x00001DFC
		private void timer10_Tick(object sender, EventArgs e)
		{
			string path = "C:\\Program Files\\IDA Freeware 7.0\\Qt5Core.dll";
			string path2 = "C:\\Program Files(x86)\\Cheat Engine 6.3\\luaclient - x86_64.dll";
			string path3 = "C:\\Program Files(x86)\\Cheat Engine 7.2\\luaclient - x86_64.dll";
			string path4 = "C:\\Program Files(x86)\\Cheat Engine 7.1\\luaclient - x86_64.dll";
			string path5 = "C:\\Program Files(x86)\\Cheat Engine 7.0\\luaclient - x86_64.dll";
			string path6 = "C:\\Program Files(x86)\\Cheat Engine 6.4\\luaclient - x86_64.dll";
			string path7 = "C:\\Program Files(x86)\\Cheat Engine 6.2\\luaclient - x86_64.dll";
			string path8 = "C:\\Program Files\\BreakPoint Software\\Hex Workshop v6.8\\pe932d.dll";
			string path9 = "C:\\Program Files\\BreakPoint Software\\Hex Workshop v6.8\\HWorks64.exe";
			string path10 = "C:\\Program Files\\Process Hacker 2\\unins000.dat";
			string path11 = "C:\\Program Files\\HxD\\HxD.exe";
			string path12 = "C:\\Program Files\\Disk Pulse\\bin\\libmon.dll";
			bool flag = File.Exists(path);
			if (flag)
			{
				this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
				File.Delete("\\Pack");
				Application.Exit();
			}
			else
			{
				bool flag2 = File.Exists(path2);
				if (flag2)
				{
					bool flag3 = Process.GetProcessesByName("PointBlank").Length < 1;
					if (flag3)
					{
						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
						KillProtection.onDisable();
						File.Delete("\\Pack");
						Application.Exit();
					}
					else
					{
						File.Delete("\\Pack");
						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
						foreach (Process process in Process.GetProcesses())
						{
							bool flag4 = process.ProcessName == "PointBlank";
							if (flag4)
							{
								process.Kill();
								KillProtection.onDisable();
								Application.Exit();
								Environment.Exit(1);
							}
						}
					}
				}
				else
				{
					bool flag5 = File.Exists(path3);
					if (flag5)
					{
						File.Delete("\\Pack");
						bool flag6 = Process.GetProcessesByName("PointBlank").Length < 1;
						if (flag6)
						{
							this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
							KillProtection.onDisable();
							Application.Exit();
						}
						else
						{
							this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
							foreach (Process process2 in Process.GetProcesses())
							{
								bool flag7 = process2.ProcessName == "PointBlank";
								if (flag7)
								{
									process2.Kill();
									KillProtection.onDisable();
									Application.Exit();
									Environment.Exit(1);
								}
							}
						}
					}
					else
					{
						bool flag8 = File.Exists(path4);
						if (flag8)
						{
							File.Delete("\\Pack");
							bool flag9 = Process.GetProcessesByName("PointBlank").Length < 1;
							if (flag9)
							{
								this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
								KillProtection.onDisable();
								Application.Exit();
							}
							else
							{
								this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
								foreach (Process process3 in Process.GetProcesses())
								{
									bool flag10 = process3.ProcessName == "PointBlank";
									if (flag10)
									{
										process3.Kill();
										KillProtection.onDisable();
										Application.Exit();
										Environment.Exit(1);
									}
								}
							}
						}
						else
						{
							bool flag11 = File.Exists(path5);
							if (flag11)
							{
								bool flag12 = Process.GetProcessesByName("PointBlank").Length < 1;
								if (flag12)
								{
									this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
									KillProtection.onDisable();
									Application.Exit();
								}
								else
								{
									this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
									foreach (Process process4 in Process.GetProcesses())
									{
										bool flag13 = process4.ProcessName == "PointBlank";
										if (flag13)
										{
											process4.Kill();
											KillProtection.onDisable();
											Application.Exit();
											Environment.Exit(1);
										}
									}
								}
							}
							else
							{
								bool flag14 = File.Exists(path6);
								if (flag14)
								{
									bool flag15 = Process.GetProcessesByName("PointBlank").Length < 1;
									if (flag15)
									{
										this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
										KillProtection.onDisable();
										Application.Exit();
									}
									else
									{
										this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
										foreach (Process process5 in Process.GetProcesses())
										{
											bool flag16 = process5.ProcessName == "PointBlank";
											if (flag16)
											{
												process5.Kill();
												KillProtection.onDisable();
												Application.Exit();
												Environment.Exit(1);
											}
										}
									}
								}
								else
								{
									bool flag17 = File.Exists(path7);
									if (flag17)
									{
										File.Delete("\\Pack");
										bool flag18 = Process.GetProcessesByName("PointBlank").Length < 1;
										if (flag18)
										{
											this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
											KillProtection.onDisable();
											Application.Exit();
										}
										else
										{
											this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
											foreach (Process process6 in Process.GetProcesses())
											{
												bool flag19 = process6.ProcessName == "PointBlank";
												if (flag19)
												{
													process6.Kill();
													KillProtection.onDisable();
													Application.Exit();
													Environment.Exit(1);
												}
											}
										}
									}
									else
									{
										bool flag20 = File.Exists(path8);
										if (flag20)
										{
											this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
											Application.Exit();
										}
										else
										{
											bool flag21 = File.Exists(path9);
											if (flag21)
											{
												this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
												Application.Exit();
											}
											else
											{
												bool flag22 = File.Exists(path10);
												if (flag22)
												{
													bool flag23 = Process.GetProcessesByName("PointBlank").Length < 1;
													if (flag23)
													{
														this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
														KillProtection.onDisable();
														Application.Exit();
													}
													else
													{
														this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
														foreach (Process process7 in Process.GetProcesses())
														{
															bool flag24 = process7.ProcessName == "PointBlank";
															if (flag24)
															{
																process7.Kill();
																KillProtection.onDisable();
																Application.Exit();
																Environment.Exit(1);
															}
														}
													}
												}
												else
												{
													bool flag25 = File.Exists(path11);
													if (flag25)
													{
														this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
														Application.Exit();
													}
													else
													{
														bool flag26 = File.Exists(path12);
														if (flag26)
														{
															this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
															Application.Exit();
														}
														else
														{
															bool flag27 = File.Exists(Application.StartupPath + "\\hid2.dll");
															if (flag27)
															{
																this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
																Application.Exit();
															}
															else
															{
																bool flag28 = File.Exists(Application.StartupPath + "\\hid.dll");
																if (flag28)
																{
																	this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
																	Application.Exit();
																}
																else
																{
																	bool flag29 = File.Exists(Application.StartupPath + "\\hid1.dll");
																	if (flag29)
																	{
																		this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
																		Application.Exit();
																	}
																	else
																	{
																		bool flag30 = File.Exists(Application.StartupPath + "\\zlib1.dll");
																		if (flag30)
																		{
																			bool flag31 = new FileInfo(Application.StartupPath + "\\zlib1.dll").Length != 68440L;
																			if (flag31)
																			{
																				bool flag32 = Process.GetProcessesByName("PointBlank").Length < 1;
																				if (flag32)
																				{
																					this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
																					Application.Exit();
																				}
																				else
																				{
																					this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
																					foreach (Process process8 in Process.GetProcesses())
																					{
																						bool flag33 = process8.ProcessName == "PointBlank";
																						if (flag33)
																						{
																							process8.Kill();
																							Application.Exit();
																							Environment.Exit(1);
																						}
																					}
																				}
																			}
																		}
																		else
																		{
																			bool flag34 = File.Exists(Application.StartupPath + "\\fmodex.dll");
																			if (flag34)
																			{
																				bool flag35 = new FileInfo(Application.StartupPath + "\\fmodex.dll").Length != 323416L;
																				if (flag35)
																				{
																					bool flag36 = Process.GetProcessesByName("PointBlank").Length < 1;
																					if (flag36)
																					{
																						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
																						Application.Exit();
																					}
																					else
																					{
																						this.PerformRequest(Modul.WEB + "launcher/delete.php", MainFrm.RegHWID());
																						foreach (Process process9 in Process.GetProcesses())
																						{
																							bool flag37 = process9.ProcessName == "PointBlank";
																							if (flag37)
																							{
																								process9.Kill();
																								Application.Exit();
																								Environment.Exit(1);
																							}
																						}
																					}
																				}
																			}
																			else
																			{
																				this.timer10.Stop();
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026FB File Offset: 0x000008FB
		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x04000003 RID: 3
		private MainFrm.LowLevelKeyboardProc objKeyboardProcess;

		// Token: 0x04000004 RID: 4
		private int counter;

		// Token: 0x04000005 RID: 5
		private readonly NotifyIcon MyIcon = new NotifyIcon();

		// Token: 0x04000006 RID: 6
		private IntPtr ptrHook;

		// Token: 0x04000007 RID: 7
		public bool check;

		// Token: 0x04000008 RID: 8
		public string LauncherKey;

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x0600003C RID: 60
		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x0200000B RID: 11
		private struct KeyboardDLLStruct
		{
			// Token: 0x04000026 RID: 38
			public Keys key;

			// Token: 0x04000027 RID: 39
			public int scanCode;

			// Token: 0x04000028 RID: 40
			public int flags;

			// Token: 0x04000029 RID: 41
			public int time;

			// Token: 0x0400002A RID: 42
			public IntPtr extra;
		}
	}
}

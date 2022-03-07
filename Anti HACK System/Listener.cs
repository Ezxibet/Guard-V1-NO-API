using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace DragonGuard
{
	// Token: 0x02000002 RID: 2
	[PermissionSet(SecurityAction.Demand, Name = "SkipVerification")]
	public class Listener
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000021C0 File Offset: 0x000003C0
		private static void KeyHandler(ushort key, uint msg)
		{
			if (Listener.SKeyEventHandler == null)
			{
				return;
			}
			foreach (EventHandler eventHandler in Listener.SKeyEventHandler.GetInvocationList())
			{
				try
				{
					eventHandler(null, new Listener.UniversalKeyEventArgs(key, msg));
				}
				catch
				{
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x0000221C File Offset: 0x0000041C
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x00002250 File Offset: 0x00000450
		public static event EventHandler SKeyEventHandler;

		// Token: 0x04000001 RID: 1
		private static readonly Listener.ListeningWindow s_Listener = new Listener.ListeningWindow(new Listener.ListeningWindow.KeyDelegate(Listener.KeyHandler));

		// Token: 0x02000003 RID: 3
		public class UniversalKeyEventArgs : KeyEventArgs
		{
			// Token: 0x06000007 RID: 7 RVA: 0x00002072 File Offset: 0x00000272
			public UniversalKeyEventArgs(ushort aKey, uint aMsg) : base((Keys)aKey)
			{
				this.m_Msg = aMsg;
				this.m_Key = aKey;
			}

			// Token: 0x04000003 RID: 3
			public readonly uint m_Msg;

			// Token: 0x04000004 RID: 4
			public readonly ushort m_Key;
		}

		// Token: 0x02000004 RID: 4
		private class ListeningWindow : NativeWindow
		{
			// Token: 0x06000008 RID: 8 RVA: 0x00002284 File Offset: 0x00000484
			public unsafe ListeningWindow(Listener.ListeningWindow.KeyDelegate keyHandlerFunction)
			{
				this.m_KeyHandler = keyHandlerFunction;
				this.cp.Caption = "Hidden window";
				this.cp.ClassName = null;
				this.cp.X = int.MaxValue;
				this.cp.Y = int.MaxValue;
				this.cp.Height = 0;
				this.cp.Width = 0;
				this.cp.Style = 33554432;
				this.CreateHandle(this.cp);
				Listener.ListeningWindow.RAWINPUTDEV rawinputdev = default(Listener.ListeningWindow.RAWINPUTDEV);
				try
				{
					rawinputdev.usUsagePage = 1;
					rawinputdev.usUsage = 6;
					rawinputdev.dwFlags = 256u;
					rawinputdev.hwndTarget = base.Handle.ToPointer();
					if (!Listener.ListeningWindow.RegisterRawInputDevices(&rawinputdev, 1u, (uint)sizeof(Listener.ListeningWindow.RAWINPUTDEV)))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error(), "ListeningWindow::RegisterRawInputDevices");
					}
				}
				catch
				{
					throw;
				}
			}

			// Token: 0x06000009 RID: 9 RVA: 0x00002388 File Offset: 0x00000588
			protected unsafe override void WndProc(ref Message m)
			{
				if (m.Msg == 255)
				{
					try
					{
						uint cbSizeHeader = (uint)sizeof(Listener.ListeningWindow.RAWINPUTHEADER);
						uint num;
						int rawInputData = Listener.ListeningWindow.GetRawInputData(m.LParam.ToPointer(), 268435459u, null, &num, cbSizeHeader);
						if (rawInputData != 0)
						{
							throw new Exception(string.Format("WndProc::GetRawInputData (1) returned non zero value ({0})", rawInputData));
						}
						byte* ptr = stackalloc byte[(int)(UIntPtr)num];
						uint rawInputData2 = (uint)Listener.ListeningWindow.GetRawInputData(m.LParam.ToPointer(), 268435459u, ptr, &num, cbSizeHeader);
						if (rawInputData2 != num)
						{
							throw new Exception(string.Format("WndProc::GetRawInputData (2) received {0} bytes while expected {1} bytes", rawInputData2, num));
						}
						Listener.ListeningWindow.RAWINPUTHKEYBOARD* ptr2 = (Listener.ListeningWindow.RAWINPUTHKEYBOARD*)ptr;
						if (ptr2->header.dwType == 1u && (this.m_PrevControlKey != ptr2->VKey || this.m_PrevMessage != ptr2->Message))
						{
							this.m_PrevControlKey = ptr2->VKey;
							this.m_PrevMessage = ptr2->Message;
							this.m_KeyHandler(ptr2->VKey, ptr2->Message);
						}
					}
					catch
					{
						throw;
					}
				}
				base.WndProc(ref m);
			}

			// Token: 0x0600000A RID: 10
			[DllImport("User32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal unsafe static extern bool RegisterRawInputDevices(Listener.ListeningWindow.RAWINPUTDEV* rawInputDevices, uint numDevices, uint size);

			// Token: 0x0600000B RID: 11
			[DllImport("User32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.I4)]
			internal unsafe static extern int GetRawInputData(void* hRawInput, uint uiCommand, byte* pData, uint* pcbSize, uint cbSizeHeader);

			// Token: 0x04000005 RID: 5
			private uint m_PrevMessage;

			// Token: 0x04000006 RID: 6
			private ushort m_PrevControlKey;

			// Token: 0x04000007 RID: 7
			private readonly Listener.ListeningWindow.KeyDelegate m_KeyHandler;

			// Token: 0x04000008 RID: 8
			public readonly CreateParams cp = new CreateParams();

			// Token: 0x04000009 RID: 9
			private const int WS_CLIPCHILDREN = 33554432;

			// Token: 0x0400000A RID: 10
			private const int WM_INPUT = 255;

			// Token: 0x0400000B RID: 11
			private const int RIDEV_INPUTSINK = 256;

			// Token: 0x0400000C RID: 12
			private const int RID_INPUT = 268435459;

			// Token: 0x0400000D RID: 13
			private const int RIM_TYPEKEYBOARD = 1;

			// Token: 0x02000005 RID: 5
			// (Invoke) Token: 0x0600000D RID: 13
			public delegate void KeyDelegate(ushort key, uint msg);

			// Token: 0x02000006 RID: 6
			internal struct RAWINPUTDEV
			{
				// Token: 0x0400000E RID: 14
				public ushort usUsagePage;

				// Token: 0x0400000F RID: 15
				public ushort usUsage;

				// Token: 0x04000010 RID: 16
				public uint dwFlags;

				// Token: 0x04000011 RID: 17
				public unsafe void* hwndTarget;
			}

			// Token: 0x02000007 RID: 7
			private struct RAWINPUTHEADER
			{
				// Token: 0x04000012 RID: 18
				public uint dwType;

				// Token: 0x04000013 RID: 19
				public uint dwSize;

				// Token: 0x04000014 RID: 20
				public unsafe void* hDevice;

				// Token: 0x04000015 RID: 21
				public unsafe void* wParam;
			}

			// Token: 0x02000008 RID: 8
			private struct RAWINPUTHKEYBOARD
			{
				// Token: 0x04000016 RID: 22
				public Listener.ListeningWindow.RAWINPUTHEADER header;

				// Token: 0x04000017 RID: 23
				public ushort MakeCode;

				// Token: 0x04000018 RID: 24
				public ushort Flags;

				// Token: 0x04000019 RID: 25
				public ushort Reserved;

				// Token: 0x0400001A RID: 26
				public ushort VKey;

				// Token: 0x0400001B RID: 27
				public uint Message;

				// Token: 0x0400001C RID: 28
				public uint ExtraInformation;
			}
		}
	}
}

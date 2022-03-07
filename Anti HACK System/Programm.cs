using System;
using System.Windows.Forms;
using DragonGuard.Classes;

namespace DragonGuard
{
	// Token: 0x02000004 RID: 4
	internal static class Programm
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00004DEC File Offset: 0x00002FEC
		[STAThread]
		private static void Main(string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				bool flag = args[i] != "a key you want";
				if (flag)
				{
					KillProtection.onDisable();
					Environment.Exit(0);
					Application.Exit();
					break;
				}
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainFrm());
			}
		}
	}
}

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace DragonGuard.Classes
{
	// Token: 0x02000007 RID: 7
	public static class KillProtection
	{
		// Token: 0x06000031 RID: 49
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern void RtlSetProcessIsCritical(uint v1, uint v2, uint v3);

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00004F5C File Offset: 0x0000315C
		private static bool IsProtected
		{
			get
			{
				bool result;
				try
				{
					KillProtection.s_isProtectedLock.EnterReadLock();
					result = KillProtection.s_isProtected;
				}
				finally
				{
					KillProtection.s_isProtectedLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004FA4 File Offset: 0x000031A4
		public static void onEnable()
		{
			try
			{
				KillProtection.s_isProtectedLock.EnterWriteLock();
				bool flag = !KillProtection.s_isProtected;
				if (flag)
				{
					Process.EnterDebugMode();
					KillProtection.RtlSetProcessIsCritical(1U, 0U, 0U);
					KillProtection.s_isProtected = true;
				}
			}
			finally
			{
				KillProtection.s_isProtectedLock.ExitWriteLock();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00005008 File Offset: 0x00003208
		public static void onDisable()
		{
			try
			{
				KillProtection.s_isProtectedLock.EnterWriteLock();
				bool flag = KillProtection.s_isProtected;
				if (flag)
				{
					KillProtection.RtlSetProcessIsCritical(0U, 0U, 0U);
					KillProtection.s_isProtected = false;
				}
			}
			finally
			{
				KillProtection.s_isProtectedLock.ExitWriteLock();
			}
		}

		// Token: 0x04000019 RID: 25
		private static volatile bool s_isProtected = false;

		// Token: 0x0400001A RID: 26
		private static ReaderWriterLockSlim s_isProtectedLock = new ReaderWriterLockSlim();
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DragonGuard.Properties
{
	// Token: 0x02000005 RID: 5
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class Resources
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00004E4D File Offset: 0x0000304D
		internal Resources()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00004E58 File Offset: 0x00003058
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager temp = new ResourceManager("DragonGuard.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00004EA0 File Offset: 0x000030A0
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00004EB7 File Offset: 0x000030B7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00004EC0 File Offset: 0x000030C0
		public static Icon icon
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("icon", Resources.resourceCulture);
				return (Icon)obj;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00004EF0 File Offset: 0x000030F0
		public static Bitmap main1
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("main1", Resources.resourceCulture);
				return (Bitmap)obj;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00004F20 File Offset: 0x00003120
		public static Bitmap mark
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("mark", Resources.resourceCulture);
				return (Bitmap)obj;
			}
		}

		// Token: 0x04000016 RID: 22
		private static ResourceManager resourceMan;

		// Token: 0x04000017 RID: 23
		private static CultureInfo resourceCulture;
	}
}

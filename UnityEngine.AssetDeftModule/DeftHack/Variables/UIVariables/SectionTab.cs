using System;

namespace DeftHack.Variables.UIVariables
{
	// Token: 0x0200000E RID: 14
	public class SectionTab
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000022B2 File Offset: 0x000004B2
		public SectionTab(string name, Action code)
		{
			this.name = name;
			this.code = code;
		}

		// Token: 0x04000033 RID: 51
		public static SectionTab CurrentSectionTab;

		// Token: 0x04000034 RID: 52
		public Action code;

		// Token: 0x04000035 RID: 53
		public string name;
	}
}

using System;

namespace DeftHack.Misc.Enums
{
	// Token: 0x02000063 RID: 99
	[Flags]
	public enum AllocationType : uint
	{
		// Token: 0x04000156 RID: 342
		COMMIT = 4096u,
		// Token: 0x04000157 RID: 343
		RESERVE = 8192u,
		// Token: 0x04000158 RID: 344
		RESET = 524288u,
		// Token: 0x04000159 RID: 345
		LARGE_PAGES = 536870912u,
		// Token: 0x0400015A RID: 346
		PHYSICAL = 4194304u,
		// Token: 0x0400015B RID: 347
		TOP_DOWN = 1048576u,
		// Token: 0x0400015C RID: 348
		WRITE_WATCH = 2097152u
	}
}

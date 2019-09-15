using System;

namespace DeftHack.Misc.Enums
{
	// Token: 0x02000067 RID: 103
	[Flags]
	public enum MemoryProtection : uint
	{
		// Token: 0x04000193 RID: 403
		EXECUTE = 16u,
		// Token: 0x04000194 RID: 404
		EXECUTE_READ = 32u,
		// Token: 0x04000195 RID: 405
		EXECUTE_READWRITE = 64u,
		// Token: 0x04000196 RID: 406
		EXECUTE_WRITECOPY = 128u,
		// Token: 0x04000197 RID: 407
		NOACCESS = 1u,
		// Token: 0x04000198 RID: 408
		READONLY = 2u,
		// Token: 0x04000199 RID: 409
		READWRITE = 4u,
		// Token: 0x0400019A RID: 410
		WRITECOPY = 8u,
		// Token: 0x0400019B RID: 411
		GUARD_Modifierflag = 256u,
		// Token: 0x0400019C RID: 412
		NOCACHE_Modifierflag = 512u,
		// Token: 0x0400019D RID: 413
		WRITECOMBINE_Modifierflag = 1024u
	}
}

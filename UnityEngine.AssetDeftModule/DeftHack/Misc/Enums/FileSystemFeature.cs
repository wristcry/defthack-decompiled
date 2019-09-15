using System;

namespace DeftHack.Misc.Enums
{
	// Token: 0x02000065 RID: 101
	[Flags]
	public enum FileSystemFeature : uint
	{
		// Token: 0x04000174 RID: 372
		CasePreservedNames = 2u,
		// Token: 0x04000175 RID: 373
		CaseSensitiveSearch = 1u,
		// Token: 0x04000176 RID: 374
		DaxVolume = 536870912u,
		// Token: 0x04000177 RID: 375
		FileCompression = 16u,
		// Token: 0x04000178 RID: 376
		NamedStreams = 262144u,
		// Token: 0x04000179 RID: 377
		PersistentACLS = 8u,
		// Token: 0x0400017A RID: 378
		ReadOnlyVolume = 524288u,
		// Token: 0x0400017B RID: 379
		SequentialWriteOnce = 1048576u,
		// Token: 0x0400017C RID: 380
		SupportsEncryption = 131072u,
		// Token: 0x0400017D RID: 381
		SupportsExtendedAttributes = 8388608u,
		// Token: 0x0400017E RID: 382
		SupportsHardLinks = 4194304u,
		// Token: 0x0400017F RID: 383
		SupportsObjectIDs = 65536u,
		// Token: 0x04000180 RID: 384
		SupportsOpenByFileId = 16777216u,
		// Token: 0x04000181 RID: 385
		SupportsReparsePoints = 128u,
		// Token: 0x04000182 RID: 386
		SupportsSparseFiles = 64u,
		// Token: 0x04000183 RID: 387
		SupportsTransactions = 2097152u,
		// Token: 0x04000184 RID: 388
		SupportsUsnJournal = 33554432u,
		// Token: 0x04000185 RID: 389
		UnicodeOnDisk = 4u,
		// Token: 0x04000186 RID: 390
		VolumeIsCompressed = 32768u,
		// Token: 0x04000187 RID: 391
		VolumeQuotas = 32u
	}
}

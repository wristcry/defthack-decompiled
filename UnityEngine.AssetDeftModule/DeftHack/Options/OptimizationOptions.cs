using System;
using DeftHack.Attributes;

namespace DeftHack.Options
{
	// Token: 0x02000053 RID: 83
	public static class OptimizationOptions
	{
		// Token: 0x040000EE RID: 238
		[Save]
		public static int PacketRefreshRate = 50;

		// Token: 0x040000EF RID: 239
		[Save]
		public static int InputSamples;
	}
}

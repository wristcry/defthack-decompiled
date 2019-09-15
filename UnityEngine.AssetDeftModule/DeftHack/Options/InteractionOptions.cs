using System;
using DeftHack.Attributes;

namespace DeftHack.Options
{
	// Token: 0x02000050 RID: 80
	public static class InteractionOptions
	{
		// Token: 0x040000AC RID: 172
		[Save]
		public static bool InteractThroughWalls = false;

		// Token: 0x040000AD RID: 173
		[Save]
		public static bool NoHitStructures = false;

		// Token: 0x040000AE RID: 174
		[Save]
		public static bool NoHitBarricades = false;

		// Token: 0x040000AF RID: 175
		[Save]
		public static bool NoHitItems = false;

		// Token: 0x040000B0 RID: 176
		[Save]
		public static bool NoHitVehicles = false;

		// Token: 0x040000B1 RID: 177
		[Save]
		public static bool NoHitResources = false;

		// Token: 0x040000B2 RID: 178
		[Save]
		public static bool NoHitEnvironment = false;
	}
}

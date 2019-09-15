using System;
using DeftHack.Attributes;

namespace DeftHack.Options.AimOptions
{
	// Token: 0x0200005C RID: 92
	public static class SphereOptions
	{
		// Token: 0x0400013B RID: 315
		[Save]
		public static float SphereRadius = 15f;

		// Token: 0x0400013C RID: 316
		[Save]
		public static int RecursionLevel = 2;

		// Token: 0x0400013D RID: 317
		[Save]
		public static bool SpherePrediction = true;
	}
}

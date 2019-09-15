using System;
using DeftHack.Attributes;
using DeftHack.Misc.Enums;
using SDG.Unturned;

namespace DeftHack.Options.AimOptions
{
	// Token: 0x0200005A RID: 90
	public static class AimbotOptions
	{
		// Token: 0x0400011C RID: 284
		[Save]
		public static bool Enabled = false;

		// Token: 0x0400011D RID: 285
		[Save]
		public static bool UseGunDistance = false;

		// Token: 0x0400011E RID: 286
		[Save]
		public static bool Smooth = false;

		// Token: 0x0400011F RID: 287
		[Save]
		public static bool OnKey = false;

		// Token: 0x04000120 RID: 288
		[Save]
		public static bool UseFovAim = true;

		// Token: 0x04000121 RID: 289
		public static float MaxSpeed = 20f;

		// Token: 0x04000122 RID: 290
		[Save]
		public static float AimSpeed = 5f;

		// Token: 0x04000123 RID: 291
		[Save]
		public static float Distance = 300f;

		// Token: 0x04000124 RID: 292
		[Save]
		public static float FOV = 15f;

		// Token: 0x04000125 RID: 293
		[Save]
		public static ELimb TargetLimb = ELimb.SKULL;

		// Token: 0x04000126 RID: 294
		[Save]
		public static TargetMode TargetMode = TargetMode.Distance;

		// Token: 0x04000127 RID: 295
		[Save]
		public static bool NoAimbotDrop = false;
	}
}

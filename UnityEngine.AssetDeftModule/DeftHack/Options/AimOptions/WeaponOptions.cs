using System;
using DeftHack.Attributes;
using DeftHack.Misc.Serializables;

namespace DeftHack.Options.AimOptions
{
	// Token: 0x0200005E RID: 94
	public static class WeaponOptions
	{
		// Token: 0x04000140 RID: 320
		[Save]
		public static bool ShowWeaponInfo = false;

		// Token: 0x04000141 RID: 321
		[Save]
		public static bool CustomCrosshair = false;

		// Token: 0x04000142 RID: 322
		[Save]
		public static SerializableColor CrosshairColor = new SerializableColor(255, 0, 0);

		// Token: 0x04000143 RID: 323
		[Save]
		public static bool NoRecoil = false;

		// Token: 0x04000144 RID: 324
		[Save]
		public static bool NoSpread = false;

		// Token: 0x04000145 RID: 325
		[Save]
		public static bool NoSway = false;

		// Token: 0x04000146 RID: 326
		[Save]
		public static bool NoDrop = false;

		// Token: 0x04000147 RID: 327
		[Save]
		public static bool OofOnDeath = false;

		// Token: 0x04000148 RID: 328
		[Save]
		public static bool AutoReload = false;

		// Token: 0x04000149 RID: 329
		[Save]
		public static bool Tracers = false;

		// Token: 0x0400014A RID: 330
		[Save]
		public static bool EnableBulletDropPrediction = false;

		// Token: 0x0400014B RID: 331
		[Save]
		public static bool HighlightBulletDropPredictionTarget = false;
	}
}

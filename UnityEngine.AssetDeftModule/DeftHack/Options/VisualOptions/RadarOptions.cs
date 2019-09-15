using System;
using DeftHack.Attributes;

namespace DeftHack.Options.VisualOptions
{
	// Token: 0x02000058 RID: 88
	public static class RadarOptions
	{
		// Token: 0x0400010B RID: 267
		[Save]
		public static bool Enabled = false;

		// Token: 0x0400010C RID: 268
		[Save]
		public static bool TrackPlayer = false;

		// Token: 0x0400010D RID: 269
		[Save]
		public static bool ShowPlayers = false;

		// Token: 0x0400010E RID: 270
		[Save]
		public static bool ShowVehicles = false;

		// Token: 0x0400010F RID: 271
		[Save]
		public static bool ShowVehiclesUnlocked = false;

		// Token: 0x04000110 RID: 272
		[Save]
		public static bool ShowDeathPosition = false;

		// Token: 0x04000111 RID: 273
		public static float RadarZoom = 1f;

		// Token: 0x04000112 RID: 274
		[Save]
		public static float RadarSize = 300f;
	}
}

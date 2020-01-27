using System;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Attributes;
using DeftHack.Misc;
using DeftHack.Misc.Classes.ESP;
using DeftHack.Misc.Enums;
using DeftHack.Misc.Serializables;
using UnityEngine;

namespace DeftHack.Options.VisualOptions
{
	// Token: 0x02000055 RID: 85
	public static class ESPOptions
	{
		// Token: 0x040000F6 RID: 246
		[Save]
		public static bool Enabled = true;

		// Token: 0x040000F7 RID: 247
		[Save]
		public static bool ChamsEnabled = false;

		// Token: 0x040000F8 RID: 248
		[Save]
		public static bool ChamsFlat = false;

		// Token: 0x040000F9 RID: 249
		[Save]
		public static bool ShowVanishPlayers = false;

		// Token: 0x040000FA RID: 250
		[Save]
		public static ESPVisual[] VisualOptions = Enumerable.Repeat<ESPVisual>(new ESPVisual
		{
			Enabled = false,
			Labels = false,
			Boxes = false,
			ShowName = false,
			ShowDistance = false,
			ShowAngle = false,
			TwoDimensional = false,
			Glow = false,
			InfiniteDistance = false,
			LineToObject = false,
			TextScaling = false,
			UseObjectCap = false,
			CustomTextColor = false,
			Distance = 250f,
			Location = LabelLocation.BottomMiddle,
			FixedTextSize = 11,
			MinTextSize = 8,
			MaxTextSize = 11,
			MinTextSizeDistance = 800f,
			BorderStrength = 2,
			ObjectCap = 24
		}, Enum.GetValues(typeof(ESPTarget)).Length).ToArray<ESPVisual>();

		// Token: 0x040000FB RID: 251
		[Save]
		public static Dictionary<ESPTarget, int> PriorityTable = Enum.GetValues(typeof(ESPTarget)).Cast<ESPTarget>().ToDictionary((ESPTarget x) => x, (ESPTarget x) => (int)x);

		// Token: 0x040000FC RID: 252
		[Save]
		public static bool ShowPlayerWeapon = false;

		// Token: 0x040000FD RID: 253
		[Save]
		public static bool ShowPlayerVehicle = false;

		// Token: 0x040000FE RID: 254
		[Save]
		public static bool UsePlayerGroup = false;

		// Token: 0x04000100 RID: 256
		[Save]
		public static bool FilterItems = false;

		// Token: 0x04000101 RID: 257
		[Save]
		public static bool ShowVehicleFuel;

		// Token: 0x04000102 RID: 258
		[Save]
		public static bool ShowVehicleHealth;

		// Token: 0x04000103 RID: 259
		[Save]
		public static bool ShowVehicleLocked;

		// Token: 0x04000104 RID: 260
		[Save]
		public static bool FilterVehicleLocked;

		// Token: 0x04000105 RID: 261
		[Save]
		public static bool ShowSentryItem;

		// Token: 0x04000106 RID: 262
		[Save]
		public static bool ShowClaimed;

		// Token: 0x04000107 RID: 263
		[Save]
		public static bool ShowGeneratorFuel;

		// Token: 0x04000108 RID: 264
		[Save]
		public static bool ShowGeneratorPowered;
	}
}

using System;
using System.Collections.Generic;
using DeftHack.Attributes;
using DeftHack.Misc.Enums;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Options
{
	// Token: 0x02000052 RID: 82
	public static class MiscOptions
	{
		// Token: 0x040000B7 RID: 183
		public static Vector3 pos;

		// Token: 0x040000B8 RID: 184
		public static bool PanicMode = false;

		// Token: 0x040000B9 RID: 185
		[Save]
		public static bool hang = false;

		// Token: 0x040000BA RID: 186
		[Save]
		public static bool PunchSilentAim = false;

		// Token: 0x040000BB RID: 187
		[Save]
		public static bool EnVehicle = false;

		// Token: 0x040000BC RID: 188
		[Save]
		public static bool BuildinObstacles = false;

		// Token: 0x040000BD RID: 189
		[Save]
		public static bool NoFlash = false;

		// Token: 0x040000BE RID: 190
		[Save]
		public static bool PunchAura = false;

		// Token: 0x040000BF RID: 191
		[Save]
		public static bool NoSnow = false;

		// Token: 0x040000C0 RID: 192
		[Save]
		public static bool NoRain = false;

		// Token: 0x040000C1 RID: 193
		[Save]
		public static Language lang = Language.Russian;

		// Token: 0x040000C2 RID: 194
		[Save]
		public static bool banbypass = false;

		// Token: 0x040000C3 RID: 195
		[Save]
		public static bool NoFlinch = false;

		// Token: 0x040000C4 RID: 196
		[Save]
		public static bool NoGrayscale = false;

		// Token: 0x040000C5 RID: 197
		[Save]
		public static bool CustomSalvageTime = false;

		// Token: 0x040000C6 RID: 198
		[Save]
		public static float SalvageTime = 1f;

		// Token: 0x040000C7 RID: 199
		[Save]
		public static bool SetTimeEnabled = false;

		// Token: 0x040000C8 RID: 200
		[Save]
		public static float Time = 0f;

		// Token: 0x040000C9 RID: 201
		[Save]
		public static bool SlowFall = false;

		// Token: 0x040000CA RID: 202
		[Save]
		public static bool AirStick = false;

		// Token: 0x040000CB RID: 203
		[Save]
		public static bool Compass = false;

		// Token: 0x040000CC RID: 204
		[Save]
		public static bool GPS = false;

		// Token: 0x040000CD RID: 205
		[Save]
		public static bool Bones = false;

		// Token: 0x040000CE RID: 206
		[Save]
		public static bool ShowPlayersOnMap = false;

		// Token: 0x040000CF RID: 207
		[Save]
		public static bool NightVision = false;

		// Token: 0x040000D0 RID: 208
		public static bool WasNightVision = false;

		// Token: 0x040000D1 RID: 209
		[Save]
		public static string SpamText = "https://vk.com/beyondcheat";

		// Token: 0x040000D2 RID: 210
		[Save]
		public static bool SpammerEnabled = false;

		// Token: 0x040000D3 RID: 211
		[Save]
		public static int SpammerDelay = 0;

		// Token: 0x040000D4 RID: 212
		[Save]
		public static bool VehicleFly = false;

		// Token: 0x040000D5 RID: 213
		[Save]
		public static bool VehicleUseMaxSpeed = false;

		// Token: 0x040000D6 RID: 214
		[Save]
		public static float SpeedMultiplier = 1f;

		// Token: 0x040000D7 RID: 215
		[Save]
		public static bool ExtendMeleeRange = false;

		// Token: 0x040000D8 RID: 216
		[Save]
		public static float MeleeRangeExtension = 7.5f;

		// Token: 0x040000D9 RID: 217
		public static bool NoMovementVerification = false;

		// Token: 0x040000DA RID: 218
		[Save]
		public static bool AlwaysCheckMovementVerification = false;

		// Token: 0x040000DB RID: 219
		public static Player SpectatedPlayer;

		// Token: 0x040000DC RID: 220
		[Save]
		public static bool PlayerFlight = false;

		// Token: 0x040000DD RID: 221
		[Save]
		public static float FlightSpeedMultiplier = 1f;

		// Token: 0x040000DE RID: 222
		public static bool Freecam = false;

		// Token: 0x040000DF RID: 223
		[Save]
		public static HashSet<ulong> Friends = new HashSet<ulong>();

		// Token: 0x040000E0 RID: 224
		[Save]
		public static int SCrashMethod = 1;

		// Token: 0x040000E1 RID: 225
		[Save]
		public static int AntiSpyMethod = 0;

		// Token: 0x040000E2 RID: 226
		[Save]
		public static string AntiSpyPath = "";

		// Token: 0x040000E3 RID: 227
		[Save]
		public static bool AlertOnSpy = false;

		// Token: 0x040000E4 RID: 228
		[Save]
		public static bool EnableDistanceCrash = false;

		// Token: 0x040000E5 RID: 229
		[Save]
		public static float CrashDistance = 100f;

		// Token: 0x040000E6 RID: 230
		[Save]
		public static bool CrashByName = false;

		// Token: 0x040000E7 RID: 231
		[Save]
		public static List<string> CrashWords = new List<string>();

		// Token: 0x040000E8 RID: 232
		[Save]
		public static List<string> CrashIDs = new List<string>();

		// Token: 0x040000E9 RID: 233
		[Save]
		public static bool NearbyItemRaycast = false;

		// Token: 0x040000EA RID: 234
		[Save]
		public static bool IncreaseNearbyItemDistance = false;

		// Token: 0x040000EB RID: 235
		[Save]
		public static float NearbyItemDistance = 15f;

		// Token: 0x040000EC RID: 236
		public static bool epos;

		// Token: 0x040000ED RID: 237
		public static float Altitude;
	}
}

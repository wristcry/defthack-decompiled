using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options;
using DeftHack.Utilities;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x02000040 RID: 64
	public static class OV_LevelLighting
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000091CC File Offset: 0x000073CC
		[OnSpy]
		public static void Disable()
		{
			bool flag = !DrawUtilities.ShouldRun();
			bool flag2 = !flag;
			if (flag2)
			{
				OV_LevelLighting.WasEnabled = MiscOptions.ShowPlayersOnMap;
				MiscOptions.ShowPlayersOnMap = false;
				OV_LevelLighting.OV_updateLighting();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00009204 File Offset: 0x00007404
		[OffSpy]
		public static void Enable()
		{
			bool flag = !DrawUtilities.ShouldRun();
			bool flag2 = !flag;
			if (flag2)
			{
				MiscOptions.ShowPlayersOnMap = OV_LevelLighting.WasEnabled;
				OV_LevelLighting.OV_updateLighting();
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000026A0 File Offset: 0x000008A0
		[Initializer]
		public static void Init()
		{
			OV_LevelLighting.Time = typeof(LevelLighting).GetField("_time", BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00009238 File Offset: 0x00007438
		[Override(typeof(LevelLighting), "updateLighting", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_updateLighting()
		{
			float time = LevelLighting.time;
			bool flag = !DrawUtilities.ShouldRun() || !MiscOptions.SetTimeEnabled || PlayerCoroutines.IsSpying;
			bool flag2 = flag;
			if (flag2)
			{
				OverrideUtilities.CallOriginal(null, Array.Empty<object>());
			}
			else
			{
				OV_LevelLighting.Time.SetValue(null, MiscOptions.Time);
				OverrideUtilities.CallOriginal(null, Array.Empty<object>());
				OV_LevelLighting.Time.SetValue(null, time);
			}
		}

		// Token: 0x04000084 RID: 132
		public static FieldInfo Time;

		// Token: 0x04000085 RID: 133
		public static bool WasEnabled;
	}
}

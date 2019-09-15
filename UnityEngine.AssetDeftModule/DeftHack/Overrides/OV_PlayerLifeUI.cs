using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Options;
using DeftHack.Utilities;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x02000048 RID: 72
	public static class OV_PlayerLifeUI
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000AF20 File Offset: 0x00009120
		[Override(typeof(PlayerLifeUI), "hasCompassInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static bool OV_hasCompassInInventory()
		{
			return MiscOptions.Compass || (bool)OverrideUtilities.CallOriginal(null, Array.Empty<object>());
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000AF50 File Offset: 0x00009150
		[Override(typeof(PlayerLifeUI), "updateGrayscale", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_updateGrayscale()
		{
			bool noGrayscale = MiscOptions.NoGrayscale;
			bool flag = !noGrayscale;
			if (flag)
			{
				OverrideUtilities.CallOriginal(null, Array.Empty<object>());
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000AF7C File Offset: 0x0000917C
		[OnSpy]
		public static void Disable()
		{
			bool flag = !DrawUtilities.ShouldRun();
			bool flag2 = !flag;
			if (flag2)
			{
				OV_PlayerLifeUI.WasCompassEnabled = MiscOptions.Compass;
				MiscOptions.Compass = false;
				PlayerLifeUI.updateCompass();
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000AFB4 File Offset: 0x000091B4
		[OffSpy]
		public static void Enable()
		{
			bool flag = !DrawUtilities.ShouldRun();
			bool flag2 = !flag;
			if (flag2)
			{
				MiscOptions.Compass = OV_PlayerLifeUI.WasCompassEnabled;
				PlayerLifeUI.updateCompass();
			}
		}

		// Token: 0x040000A7 RID: 167
		public static bool WasCompassEnabled;
	}
}

using System;
using System.Reflection;
using DeftHack.Attributes;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x0200004B RID: 75
	public class OV_PlayerUI
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000B01C File Offset: 0x0000921C
		[Override(typeof(PlayerUI), "updateCrosshair", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_updateCrosshair(float spread)
		{
			bool flag = !Provider.modeConfigData.Gameplay.Crosshair;
			bool flag2 = !flag;
			if (flag2)
			{
				PlayerLifeUI.crosshairLeftImage.positionOffset_X = (int)(-spread * 400f) - 4;
				PlayerLifeUI.crosshairLeftImage.positionOffset_Y = -4;
				PlayerLifeUI.crosshairRightImage.positionOffset_X = (int)(spread * 400f) - 4;
				PlayerLifeUI.crosshairRightImage.positionOffset_Y = -4;
				PlayerLifeUI.crosshairUpImage.positionOffset_X = -4;
				PlayerLifeUI.crosshairUpImage.positionOffset_Y = (int)(-spread * 400f) - 4;
				PlayerLifeUI.crosshairDownImage.positionOffset_X = -4;
				PlayerLifeUI.crosshairDownImage.positionOffset_Y = (int)(spread * 400f) - 4;
			}
		}
	}
}

using System;
using System.Reflection;
using DeftHack.Attributes;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x0200004A RID: 74
	public static class OV_PlayerPauseUI
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000276E File Offset: 0x0000096E
		[Override(typeof(PlayerPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_onClickedExitButton(SleekButton button)
		{
			Provider.disconnect();
		}
	}
}

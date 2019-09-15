using System;
using System.Reflection;
using DeftHack.Attributes;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x02000041 RID: 65
	public static class OV_MenuPauseUI
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000026BE File Offset: 0x000008BE
		[Override(typeof(MenuPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_onClickedExitButton(SleekButton button)
		{
			Application.Quit();
		}
	}
}

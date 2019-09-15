using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Components.UI.Menu;
using DeftHack.Coroutines;
using DeftHack.Utilities;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x0200003C RID: 60
	public static class OV_Cursor
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00008F4C File Offset: 0x0000714C
		[Override(typeof(Cursor), "set_lockState", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_set_lockState(CursorLockMode rMode)
		{
			bool flag = MenuComponent.IsInMenu && !PlayerCoroutines.IsSpying && (rMode == CursorLockMode.Confined || rMode == CursorLockMode.Locked);
			bool flag2 = !flag;
			if (flag2)
			{
				OverrideUtilities.CallOriginal(null, new object[]
				{
					rMode
				});
			}
		}
	}
}

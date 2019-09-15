using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Options;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x0200003E RID: 62
	public static class OV_Input
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00002686 File Offset: 0x00000886
		[OnSpy]
		public static void OnSpied()
		{
			OV_Input.InputEnabled = false;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000268F File Offset: 0x0000088F
		[OffSpy]
		public static void OnEndSpy()
		{
			OV_Input.InputEnabled = true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009090 File Offset: 0x00007290
		[Override(typeof(Input), "GetKey", BindingFlags.Static | BindingFlags.Public, 1)]
		public static bool OV_GetKey(KeyCode key)
		{
			bool flag = !DrawUtilities.ShouldRun() || !OV_Input.InputEnabled;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = (bool)OverrideUtilities.CallOriginal(null, new object[]
				{
					key
				});
			}
			else
			{
				bool flag3 = key == ControlsSettings.primary && TriggerbotOptions.IsFiring;
				bool flag4 = flag3;
				result = (flag4 || (((key != ControlsSettings.left && key != ControlsSettings.right && key != ControlsSettings.up && key != ControlsSettings.down) || !(MiscOptions.SpectatedPlayer != null)) && (bool)OverrideUtilities.CallOriginal(null, new object[]
				{
					key
				})));
			}
			return result;
		}

		// Token: 0x04000083 RID: 131
		public static bool InputEnabled = true;
	}
}

using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Options;
using DeftHack.Utilities;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x02000049 RID: 73
	public class OV_PlayerLook
	{
		// Token: 0x0600010E RID: 270 RVA: 0x0000AFE8 File Offset: 0x000091E8
		[Override(typeof(PlayerLook), "onDamaged", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public static void OV_onDamaged(byte damage)
		{
			bool noFlinch = MiscOptions.NoFlinch;
			bool flag = !noFlinch;
			if (flag)
			{
				OverrideUtilities.CallOriginal(null, new object[]
				{
					damage
				});
			}
		}
	}
}

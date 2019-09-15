using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x02000045 RID: 69
	public class OV_PlayerEquipment
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00009E64 File Offset: 0x00008064
		[Override(typeof(PlayerEquipment), "punch", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public void OV_punch(EPlayerPunch p)
		{
			bool punchSilentAim = MiscOptions.PunchSilentAim;
			bool flag = punchSilentAim;
			if (flag)
			{
				OV_DamageTool.OVType = OverrideType.PlayerHit;
			}
			OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment, new object[]
			{
				p
			});
			OV_DamageTool.OVType = OverrideType.None;
		}

		// Token: 0x0400008B RID: 139
		public static bool WasPunching;

		// Token: 0x0400008C RID: 140
		public static uint CurrSim;
	}
}

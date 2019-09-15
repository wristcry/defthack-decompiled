using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x0200004E RID: 78
	public class OV_UseableMelee
	{
		// Token: 0x0600011B RID: 283 RVA: 0x0000BB60 File Offset: 0x00009D60
		[Override(typeof(UseableMelee), "fire", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public static void OV_fire()
		{
			OV_DamageTool.OVType = OverrideType.None;
			bool flag = RaycastOptions.Enabled && MiscOptions.ExtendMeleeRange;
			bool flag2 = flag;
			if (flag2)
			{
				OV_DamageTool.OVType = OverrideType.SilentAimMelee;
			}
			else
			{
				bool enabled = RaycastOptions.Enabled;
				bool flag3 = enabled;
				if (flag3)
				{
					OV_DamageTool.OVType = OverrideType.SilentAim;
				}
				else
				{
					bool extendMeleeRange = MiscOptions.ExtendMeleeRange;
					bool flag4 = extendMeleeRange;
					if (flag4)
					{
						OV_DamageTool.OVType = OverrideType.Extended;
					}
				}
			}
			OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment.useable, Array.Empty<object>());
			OV_DamageTool.OVType = OverrideType.None;
		}
	}
}

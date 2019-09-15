using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x0200003D RID: 61
	public static class OV_DamageTool
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00008F98 File Offset: 0x00007198
		[Override(typeof(DamageTool), "raycast", BindingFlags.Static | BindingFlags.Public, 1)]
		public static RaycastInfo OV_raycast(Ray ray, float range, int mask)
		{
			switch (OV_DamageTool.OVType)
			{
			case OverrideType.Extended:
				return RaycastUtilities.GenerateOriginalRaycast(ray, MiscOptions.MeleeRangeExtension, mask);
			case OverrideType.PlayerHit:
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					bool flag = VectorUtilities.GetDistance(Player.player.transform.position, Provider.clients[i].player.transform.position) > 15.5;
					bool flag2 = !flag;
					if (flag2)
					{
						RaycastInfo result;
						RaycastUtilities.GenerateRaycast(out result);
						return result;
					}
				}
				break;
			case OverrideType.SilentAim:
			{
				RaycastInfo raycastInfo;
				return RaycastUtilities.GenerateRaycast(out raycastInfo) ? raycastInfo : RaycastUtilities.GenerateOriginalRaycast(ray, range, mask);
			}
			case OverrideType.SilentAimMelee:
			{
				RaycastInfo raycastInfo2;
				return RaycastUtilities.GenerateRaycast(out raycastInfo2) ? raycastInfo2 : RaycastUtilities.GenerateOriginalRaycast(ray, MiscOptions.MeleeRangeExtension, mask);
			}
			}
			return RaycastUtilities.GenerateOriginalRaycast(ray, range, mask);
		}

		// Token: 0x04000082 RID: 130
		public static OverrideType OVType = OverrideType.None;
	}
}

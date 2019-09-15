using System;
using System.Collections.Generic;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Options;
using DeftHack.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x0200003F RID: 63
	public static class OV_ItemManager
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00009150 File Offset: 0x00007350
		[Override(typeof(ItemManager), "getItemsInRadius", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_getItemsInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<InteractableItem> result)
		{
			bool increaseNearbyItemDistance = MiscOptions.IncreaseNearbyItemDistance;
			bool flag = increaseNearbyItemDistance;
			if (flag)
			{
				OverrideUtilities.CallOriginal(null, new object[]
				{
					center,
					Mathf.Pow(MiscOptions.NearbyItemDistance, 2f),
					search,
					result
				});
			}
			else
			{
				OverrideUtilities.CallOriginal(null, new object[]
				{
					center,
					sqrRadius,
					search,
					result
				});
			}
		}
	}
}

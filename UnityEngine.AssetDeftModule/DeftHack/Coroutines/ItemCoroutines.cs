using System;
using System.Collections;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Coroutines
{
	// Token: 0x02000088 RID: 136
	public static class ItemCoroutines
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00002C14 File Offset: 0x00000E14
		public static IEnumerator PickupItems()
		{
			for (;;)
			{
				bool flag = !DrawUtilities.ShouldRun() || !ItemOptions.AutoItemPickup;
				bool flag4 = flag;
				if (flag4)
				{
					yield return new WaitForSeconds(0.5f);
				}
				else
				{
					Collider[] array = Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, 19f, RayMasks.ITEM);
					int num;
					for (int i = 0; i < array.Length; i = num + 1)
					{
						Collider col = array[i];
						bool flag2 = col == null || col.GetComponent<InteractableItem>() == null || col.GetComponent<InteractableItem>().asset == null;
						bool flag5 = !flag2;
						if (flag5)
						{
							InteractableItem item = col.GetComponent<InteractableItem>();
							bool flag3 = !ItemUtilities.Whitelisted(item.asset, ItemOptions.ItemFilterOptions);
							bool flag6 = !flag3;
							if (flag6)
							{
								item.use();
								col = null;
								item = null;
							}
							item = null;
						}
						num = i;
						col = null;
					}
					yield return new WaitForSeconds((float)(ItemOptions.ItemPickupDelay / 1000));
					array = null;
					array = null;
				}
			}
			yield break;
		}
	}
}

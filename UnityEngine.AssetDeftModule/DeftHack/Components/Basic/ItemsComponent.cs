using System;
using System.Collections.Generic;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000BB RID: 187
	[Component]
	public class ItemsComponent : MonoBehaviour
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public static void RefreshItems()
		{
			ItemsComponent.items.Clear();
			for (ushort num = 0; num < 65535; num += 1)
			{
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
				bool flag = !string.IsNullOrEmpty((itemAsset != null) ? itemAsset.itemName : null) && !ItemsComponent.items.Contains(itemAsset);
				bool flag2 = flag;
				if (flag2)
				{
					ItemsComponent.items.Add(itemAsset);
				}
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00003159 File Offset: 0x00001359
		public void Start()
		{
			CoroutineComponent.ItemPickupCoroutine = base.StartCoroutine(ItemCoroutines.PickupItems());
		}

		// Token: 0x040003CA RID: 970
		public static List<ItemAsset> items = new List<ItemAsset>();
	}
}

using System;
using System.Collections.Generic;
using DeftHack.Components.Basic;
using DeftHack.Components.UI.Menu;
using DeftHack.Misc.Classes.Misc;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x02000019 RID: 25
	public static class ItemUtilities
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00005890 File Offset: 0x00003A90
		public static bool Whitelisted(ItemAsset asset, ItemOptionList OptionList)
		{
			bool flag = OptionList.ItemfilterCustom && OptionList.AddedItems.Contains(asset.id);
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = OptionList.ItemfilterGun && asset is ItemGunAsset;
				bool flag4 = flag3;
				if (flag4)
				{
					result = true;
				}
				else
				{
					bool flag5 = OptionList.ItemfilterAmmo && asset is ItemMagazineAsset;
					bool flag6 = flag5;
					if (flag6)
					{
						result = true;
					}
					else
					{
						bool flag7 = OptionList.ItemfilterMedical && asset is ItemMedicalAsset;
						bool flag8 = flag7;
						if (flag8)
						{
							result = true;
						}
						else
						{
							bool flag9 = OptionList.ItemfilterFoodAndWater && (asset is ItemFoodAsset || asset is ItemWaterAsset);
							bool flag10 = flag9;
							if (flag10)
							{
								result = true;
							}
							else
							{
								bool flag11 = OptionList.ItemfilterBackpack && asset is ItemBackpackAsset;
								bool flag12 = flag11;
								if (flag12)
								{
									result = true;
								}
								else
								{
									bool flag13 = OptionList.ItemfilterCharges && asset is ItemChargeAsset;
									bool flag14 = flag13;
									if (flag14)
									{
										result = true;
									}
									else
									{
										bool flag15 = OptionList.ItemfilterFuel && asset is ItemFuelAsset;
										bool flag16 = flag15;
										if (flag16)
										{
											result = true;
										}
										else
										{
											bool flag17 = OptionList.ItemfilterClothing && asset is ItemClothingAsset;
											result = flag17;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005A0C File Offset: 0x00003C0C
		public static void DrawItemButton(ItemAsset asset, HashSet<ushort> AddedItems)
		{
			string text = asset.itemName;
			bool flag = asset.itemName.Length > 60;
			bool flag2 = flag;
			if (flag2)
			{
				text = asset.itemName.Substring(0, 60) + "..";
			}
			bool flag3 = Prefab.Button(text, 490f, 25f, Array.Empty<GUILayoutOption>());
			bool flag4 = flag3;
			if (flag4)
			{
				bool flag5 = AddedItems.Contains(asset.id);
				bool flag6 = flag5;
				if (flag6)
				{
					AddedItems.Remove(asset.id);
				}
				else
				{
					AddedItems.Add(asset.id);
				}
			}
			GUILayout.Space(3f);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005AB4 File Offset: 0x00003CB4
		public static void DrawFilterTab(ItemOptionList OptionList)
		{
			System.Action two = null;
			System.Action tri = null;
			System.Action one = null;
			Prefab.SectionTabButton("ФИЛЬТР ПРЕДМЕТОВ", delegate
			{
				Prefab.Toggle("Оружие", ref OptionList.ItemfilterGun, 17);
				Prefab.Toggle("Боеприпасы", ref OptionList.ItemfilterAmmo, 17);
				Prefab.Toggle("Медикаменты", ref OptionList.ItemfilterMedical, 17);
				Prefab.Toggle("Рюкзаки", ref OptionList.ItemfilterBackpack, 17);
				Prefab.Toggle("Charges", ref OptionList.ItemfilterCharges, 17);
				Prefab.Toggle("Топливо", ref OptionList.ItemfilterFuel, 17);
				Prefab.Toggle("Одежда", ref OptionList.ItemfilterClothing, 17);
				Prefab.Toggle("Провизия", ref OptionList.ItemfilterFoodAndWater, 17);
				Prefab.Toggle("Настройка фильтра", ref OptionList.ItemfilterCustom, 17);
				bool itemfilterCustom = OptionList.ItemfilterCustom;
				bool flag = itemfilterCustom;
				if (flag)
				{
					GUILayout.Space(5f);
					string text = "Кастомизация фильтра";
					System.Action code;
					bool flag2 = (code = one) == null;
					if (flag2)
					{
						code = (one = delegate()
						{
							GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
							GUILayout.Space(55f);
							OptionList.searchstring = Prefab.TextField(OptionList.searchstring, "Поиск:", 200);
							GUILayout.Space(5f);
							bool flag3 = Prefab.Button("Обновить", 276f, 25f, Array.Empty<GUILayoutOption>());
							bool flag4 = flag3;
							if (flag4)
							{
								ItemsComponent.RefreshItems();
							}
							GUILayout.FlexibleSpace();
							GUILayout.EndHorizontal();
							Rect area = new Rect(70f, 50f, 540f, 190f);
							string title = "Добавить";
							ItemOptionList optionList = OptionList;
							System.Action code2;
							bool flag5 = (code2 = two) == null;
							if (flag5)
							{
								code2 = (two = delegate()
								{
									GUILayout.Space(5f);
									for (int i = 0; i < ItemsComponent.items.Count; i++)
									{
										ItemAsset itemAsset = ItemsComponent.items[i];
										bool flag7 = false;
										bool flag8 = itemAsset.itemName.ToLower().Contains(OptionList.searchstring.ToLower());
										bool flag9 = flag8;
										if (flag9)
										{
											flag7 = true;
										}
										bool flag10 = OptionList.searchstring.Length < 2;
										bool flag11 = flag10;
										if (flag11)
										{
											flag7 = false;
										}
										bool flag12 = OptionList.AddedItems.Contains(itemAsset.id);
										bool flag13 = flag12;
										if (flag13)
										{
											flag7 = false;
										}
										bool flag14 = flag7;
										bool flag15 = flag14;
										if (flag15)
										{
											ItemUtilities.DrawItemButton(itemAsset, OptionList.AddedItems);
										}
									}
									GUILayout.Space(2f);
								});
							}
							Prefab.ScrollView(area, title, ref optionList.additemscroll, code2, 20, Array.Empty<GUILayoutOption>());
							Rect area2 = new Rect(70f, 245f, 540f, 191f);
							string title2 = "Удалить";
							ItemOptionList optionList2 = OptionList;
							System.Action code3;
							bool flag6 = (code3 = tri) == null;
							if (flag6)
							{
								code3 = (tri = delegate()
								{
									GUILayout.Space(5f);
									for (int i = 0; i < ItemsComponent.items.Count; i++)
									{
										ItemAsset itemAsset = ItemsComponent.items[i];
										bool flag7 = false;
										bool flag8 = itemAsset.itemName.ToLower().Contains(OptionList.searchstring.ToLower());
										bool flag9 = flag8;
										if (flag9)
										{
											flag7 = true;
										}
										bool flag10 = !OptionList.AddedItems.Contains(itemAsset.id);
										bool flag11 = flag10;
										if (flag11)
										{
											flag7 = false;
										}
										bool flag12 = flag7;
										bool flag13 = flag12;
										if (flag13)
										{
											ItemUtilities.DrawItemButton(itemAsset, OptionList.AddedItems);
										}
									}
									GUILayout.Space(2f);
								});
							}
							Prefab.ScrollView(area2, title2, ref optionList2.removeitemscroll, code3, 20, Array.Empty<GUILayoutOption>());
						});
					}
					Prefab.SectionTabButton(text, code, 0f, 20);
				}
			}, 0f, 20);
		}
	}
}

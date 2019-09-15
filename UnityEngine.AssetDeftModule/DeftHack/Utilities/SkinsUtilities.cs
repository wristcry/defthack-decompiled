using System;
using System.Collections.Generic;
using DeftHack.Components.UI.Menu;
using DeftHack.Misc.Classes.Skins;
using DeftHack.Misc.Enums;
using DeftHack.Options.VisualOptions;
using DeftHack.Variables;
using SDG.Provider;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x02000026 RID: 38
	public static class SkinsUtilities
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00006EE4 File Offset: 0x000050E4
		private static HumanClothes CharacterClothes
		{
			get
			{
				return OptimizationVariables.MainPlayer.clothing.characterClothes;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00006F08 File Offset: 0x00005108
		private static HumanClothes FirstClothes
		{
			get
			{
				return OptimizationVariables.MainPlayer.clothing.firstClothes;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00006F2C File Offset: 0x0000512C
		private static HumanClothes ThirdClothes
		{
			get
			{
				return OptimizationVariables.MainPlayer.clothing.thirdClothes;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006F50 File Offset: 0x00005150
		public static void Apply(Skin skin, SkinType skinType)
		{
			bool flag = skinType == SkinType.Weapons;
			bool flag2 = flag;
			if (flag2)
			{
				Dictionary<ushort, int> itemSkins = OptimizationVariables.MainPlayer.channel.owner.itemSkins;
				bool flag3 = itemSkins == null;
				bool flag4 = !flag3;
				if (flag4)
				{
					ushort inventoryItemID = Provider.provider.economyService.getInventoryItemID(skin.ID);
					SkinOptions.SkinConfig.WeaponSkins.Clear();
					int num;
					bool flag5 = itemSkins.TryGetValue(inventoryItemID, out num);
					bool flag6 = flag5;
					if (flag6)
					{
						itemSkins[inventoryItemID] = skin.ID;
					}
					else
					{
						itemSkins.Add(inventoryItemID, skin.ID);
					}
					OptimizationVariables.MainPlayer.equipment.applySkinVisual();
					OptimizationVariables.MainPlayer.equipment.applyMythicVisual();
					foreach (KeyValuePair<ushort, int> keyValuePair in itemSkins)
					{
						SkinOptions.SkinConfig.WeaponSkins.Add(new WeaponSave(keyValuePair.Key, keyValuePair.Value));
					}
				}
			}
			else
			{
				SkinsUtilities.ApplyClothing(skin, skinType);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007084 File Offset: 0x00005284
		private static void ApplyClothing(Skin skin, SkinType type)
		{
			switch (type)
			{
			case SkinType.Shirts:
				SkinsUtilities.CharacterClothes.visualShirt = skin.ID;
				SkinsUtilities.FirstClothes.visualShirt = skin.ID;
				SkinsUtilities.ThirdClothes.visualShirt = skin.ID;
				SkinOptions.SkinConfig.ShirtID = skin.ID;
				break;
			case SkinType.Pants:
				SkinsUtilities.CharacterClothes.visualPants = skin.ID;
				SkinsUtilities.FirstClothes.visualPants = skin.ID;
				SkinsUtilities.ThirdClothes.visualPants = skin.ID;
				SkinOptions.SkinConfig.PantsID = skin.ID;
				break;
			case SkinType.Backpacks:
				SkinsUtilities.CharacterClothes.visualBackpack = skin.ID;
				SkinsUtilities.FirstClothes.visualBackpack = skin.ID;
				SkinsUtilities.ThirdClothes.visualBackpack = skin.ID;
				SkinOptions.SkinConfig.BackpackID = skin.ID;
				break;
			case SkinType.Vests:
				SkinsUtilities.CharacterClothes.visualVest = skin.ID;
				SkinsUtilities.FirstClothes.visualVest = skin.ID;
				SkinsUtilities.ThirdClothes.visualVest = skin.ID;
				SkinOptions.SkinConfig.VestID = skin.ID;
				break;
			case SkinType.Hats:
				SkinsUtilities.CharacterClothes.visualHat = skin.ID;
				SkinsUtilities.FirstClothes.visualHat = skin.ID;
				SkinsUtilities.ThirdClothes.visualHat = skin.ID;
				SkinOptions.SkinConfig.HatID = skin.ID;
				break;
			case SkinType.Masks:
				SkinsUtilities.CharacterClothes.visualMask = skin.ID;
				SkinsUtilities.FirstClothes.visualMask = skin.ID;
				SkinsUtilities.ThirdClothes.visualMask = skin.ID;
				SkinOptions.SkinConfig.MaskID = skin.ID;
				break;
			case SkinType.Glasses:
				SkinsUtilities.CharacterClothes.visualGlasses = skin.ID;
				SkinsUtilities.FirstClothes.visualGlasses = skin.ID;
				SkinsUtilities.ThirdClothes.visualGlasses = skin.ID;
				SkinOptions.SkinConfig.GlassesID = skin.ID;
				break;
			}
			SkinsUtilities.CharacterClothes.apply();
			SkinsUtilities.FirstClothes.apply();
			SkinsUtilities.ThirdClothes.apply();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000072D0 File Offset: 0x000054D0
		public static void ApplyFromConfig()
		{
			Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
			foreach (WeaponSave weaponSave in SkinOptions.SkinConfig.WeaponSkins)
			{
				dictionary[weaponSave.WeaponID] = weaponSave.SkinID;
			}
			bool flag = OptimizationVariables.MainPlayer == null;
			bool flag2 = !flag;
			if (flag2)
			{
				OptimizationVariables.MainPlayer.channel.owner.itemSkins = dictionary;
				bool flag3 = SkinOptions.SkinConfig.ShirtID != 0;
				bool flag4 = flag3;
				if (flag4)
				{
					SkinsUtilities.CharacterClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
					SkinsUtilities.FirstClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
					SkinsUtilities.ThirdClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
				}
				bool flag5 = SkinOptions.SkinConfig.PantsID != 0;
				bool flag6 = flag5;
				if (flag6)
				{
					SkinsUtilities.CharacterClothes.visualPants = SkinOptions.SkinConfig.PantsID;
					SkinsUtilities.FirstClothes.visualPants = SkinOptions.SkinConfig.PantsID;
					SkinsUtilities.ThirdClothes.visualPants = SkinOptions.SkinConfig.PantsID;
				}
				bool flag7 = SkinOptions.SkinConfig.BackpackID != 0;
				bool flag8 = flag7;
				if (flag8)
				{
					SkinsUtilities.CharacterClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
					SkinsUtilities.FirstClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
					SkinsUtilities.ThirdClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
				}
				bool flag9 = SkinOptions.SkinConfig.VestID != 0;
				bool flag10 = flag9;
				if (flag10)
				{
					SkinsUtilities.CharacterClothes.visualVest = SkinOptions.SkinConfig.VestID;
					SkinsUtilities.FirstClothes.visualVest = SkinOptions.SkinConfig.VestID;
					SkinsUtilities.ThirdClothes.visualVest = SkinOptions.SkinConfig.VestID;
				}
				bool flag11 = SkinOptions.SkinConfig.HatID != 0;
				bool flag12 = flag11;
				if (flag12)
				{
					SkinsUtilities.CharacterClothes.visualHat = SkinOptions.SkinConfig.HatID;
					SkinsUtilities.FirstClothes.visualHat = SkinOptions.SkinConfig.HatID;
					SkinsUtilities.ThirdClothes.visualHat = SkinOptions.SkinConfig.HatID;
				}
				bool flag13 = SkinOptions.SkinConfig.MaskID != 0;
				bool flag14 = flag13;
				if (flag14)
				{
					SkinsUtilities.CharacterClothes.visualMask = SkinOptions.SkinConfig.MaskID;
					SkinsUtilities.FirstClothes.visualMask = SkinOptions.SkinConfig.MaskID;
					SkinsUtilities.ThirdClothes.visualMask = SkinOptions.SkinConfig.MaskID;
				}
				bool flag15 = SkinOptions.SkinConfig.GlassesID != 0;
				bool flag16 = flag15;
				if (flag16)
				{
					SkinsUtilities.CharacterClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
					SkinsUtilities.FirstClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
					SkinsUtilities.ThirdClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
				}
				SkinsUtilities.CharacterClothes.apply();
				SkinsUtilities.FirstClothes.apply();
				SkinsUtilities.ThirdClothes.apply();
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000075F8 File Offset: 0x000057F8
		public static void DrawSkins(SkinOptionList OptionList)
		{
			System.Action one = null;
			Prefab.SectionTabButton(OptionList.Type.ToString(), delegate
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.Space(60f);
				SkinsUtilities.SearchString = Prefab.TextField(SkinsUtilities.SearchString, "Search:", 480);
				GUILayout.EndHorizontal();
				Rect area = new Rect(70f, 40f, 540f, 395f);
				string title = OptionList.Type.ToString();
				System.Action code;
				bool flag = (code = one) == null;
				if (flag)
				{
					code = (one = delegate()
					{
						foreach (Skin skin in OptionList.Skins)
						{
							bool flag2 = skin.Name.ToLower().Contains(SkinsUtilities.SearchString.ToLower());
							bool flag3 = flag2;
							bool flag4 = flag3;
							if (flag4)
							{
								bool flag5 = Prefab.Button(skin.Name, 495f, 25f, Array.Empty<GUILayoutOption>());
								bool flag6 = flag5;
								if (flag6)
								{
									SkinsUtilities.Apply(skin, OptionList.Type);
								}
							}
						}
					});
				}
				Prefab.ScrollView(area, title, ref SkinsUtilities.ScrollPos, code, 20, Array.Empty<GUILayoutOption>());
			}, 0f, 20);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000764C File Offset: 0x0000584C
		public static void RefreshEconInfo()
		{
			bool flag = SkinOptions.SkinWeapons.Skins.Count > 5;
			bool flag2 = !flag;
			if (flag2)
			{
				foreach (UnturnedEconInfo unturnedEconInfo in TempSteamworksEconomy.econInfo)
				{
					bool flag3 = unturnedEconInfo.type.Contains("Skin");
					bool flag4 = flag3;
					if (flag4)
					{
						SkinOptions.SkinWeapons.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
					bool flag5 = unturnedEconInfo.type.Contains("Shirt");
					bool flag6 = flag5;
					if (flag6)
					{
						SkinOptions.SkinClothesShirts.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
					bool flag7 = unturnedEconInfo.type.Contains("Pants");
					bool flag8 = flag7;
					if (flag8)
					{
						SkinOptions.SkinClothesPants.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
					bool flag9 = unturnedEconInfo.type.Contains("Backpack");
					bool flag10 = flag9;
					if (flag10)
					{
						SkinOptions.SkinClothesBackpack.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
					bool flag11 = unturnedEconInfo.type.Contains("Vest");
					bool flag12 = flag11;
					if (flag12)
					{
						SkinOptions.SkinClothesVest.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
					bool flag13 = unturnedEconInfo.type.Contains("Hat");
					bool flag14 = flag13;
					if (flag14)
					{
						SkinOptions.SkinClothesHats.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
					bool flag15 = unturnedEconInfo.type.Contains("Mask");
					bool flag16 = flag15;
					if (flag16)
					{
						SkinOptions.SkinClothesMask.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
					bool flag17 = unturnedEconInfo.type.Contains("Glass");
					bool flag18 = flag17;
					if (flag18)
					{
						SkinOptions.SkinClothesGlasses.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
					}
				}
			}
		}

		// Token: 0x0400005C RID: 92
		public static Vector2 ScrollPos;

		// Token: 0x0400005D RID: 93
		private static string SearchString = "";
	}
}

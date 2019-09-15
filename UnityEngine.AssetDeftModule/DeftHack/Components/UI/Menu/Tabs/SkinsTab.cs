using System;
using DeftHack.Options.VisualOptions;
using DeftHack.Utilities;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000B1 RID: 177
	public static class SkinsTab
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0001916C File Offset: 0x0001736C
		public static void Tab()
		{
			SkinsUtilities.DrawSkins(SkinOptions.SkinWeapons);
			SkinsUtilities.DrawSkins(SkinOptions.SkinClothesShirts);
			SkinsUtilities.DrawSkins(SkinOptions.SkinClothesPants);
			SkinsUtilities.DrawSkins(SkinOptions.SkinClothesBackpack);
			SkinsUtilities.DrawSkins(SkinOptions.SkinClothesHats);
			SkinsUtilities.DrawSkins(SkinOptions.SkinClothesMask);
			SkinsUtilities.DrawSkins(SkinOptions.SkinClothesVest);
			SkinsUtilities.DrawSkins(SkinOptions.SkinClothesGlasses);
			GUILayout.Label("СКИНЫ ВИДНЫ ТОЛЬКО ВАМ!", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
		}
	}
}

using System;
using DeftHack.Attributes;
using DeftHack.Misc.Classes.Skins;
using DeftHack.Misc.Enums;

namespace DeftHack.Options.VisualOptions
{
	// Token: 0x02000059 RID: 89
	public static class SkinOptions
	{
		// Token: 0x04000113 RID: 275
		[Save]
		public static SkinConfig SkinConfig = new SkinConfig();

		// Token: 0x04000114 RID: 276
		public static SkinOptionList SkinWeapons = new SkinOptionList(SkinType.Weapons);

		// Token: 0x04000115 RID: 277
		public static SkinOptionList SkinClothesShirts = new SkinOptionList(SkinType.Shirts);

		// Token: 0x04000116 RID: 278
		public static SkinOptionList SkinClothesPants = new SkinOptionList(SkinType.Pants);

		// Token: 0x04000117 RID: 279
		public static SkinOptionList SkinClothesBackpack = new SkinOptionList(SkinType.Backpacks);

		// Token: 0x04000118 RID: 280
		public static SkinOptionList SkinClothesVest = new SkinOptionList(SkinType.Vests);

		// Token: 0x04000119 RID: 281
		public static SkinOptionList SkinClothesHats = new SkinOptionList(SkinType.Hats);

		// Token: 0x0400011A RID: 282
		public static SkinOptionList SkinClothesMask = new SkinOptionList(SkinType.Masks);

		// Token: 0x0400011B RID: 283
		public static SkinOptionList SkinClothesGlasses = new SkinOptionList(SkinType.Glasses);
	}
}

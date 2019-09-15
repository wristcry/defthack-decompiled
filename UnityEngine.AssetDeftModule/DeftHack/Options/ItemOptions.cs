using System;
using DeftHack.Attributes;
using DeftHack.Misc.Classes.Misc;

namespace DeftHack.Options
{
	// Token: 0x02000051 RID: 81
	public static class ItemOptions
	{
		// Token: 0x040000B3 RID: 179
		[Save]
		public static bool AutoItemPickup = false;

		// Token: 0x040000B4 RID: 180
		[Save]
		public static int ItemPickupDelay = 1000;

		// Token: 0x040000B5 RID: 181
		[Save]
		public static ItemOptionList ItemFilterOptions = new ItemOptionList();

		// Token: 0x040000B6 RID: 182
		[Save]
		public static ItemOptionList ItemESPOptions = new ItemOptionList();
	}
}

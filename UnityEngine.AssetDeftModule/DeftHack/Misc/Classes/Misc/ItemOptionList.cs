using System;
using System.Collections.Generic;
using DeftHack.Misc.Serializables;

namespace DeftHack.Misc.Classes.Misc
{
	// Token: 0x02000071 RID: 113
	public class ItemOptionList
	{
		// Token: 0x040001CA RID: 458
		public HashSet<ushort> AddedItems = new HashSet<ushort>();

		// Token: 0x040001CB RID: 459
		public bool ItemfilterGun = false;

		// Token: 0x040001CC RID: 460
		public bool ItemfilterAmmo = false;

		// Token: 0x040001CD RID: 461
		public bool ItemfilterMedical = false;

		// Token: 0x040001CE RID: 462
		public bool ItemfilterBackpack = false;

		// Token: 0x040001CF RID: 463
		public bool ItemfilterCharges = false;

		// Token: 0x040001D0 RID: 464
		public bool ItemfilterFuel = false;

		// Token: 0x040001D1 RID: 465
		public bool ItemfilterClothing = false;

		// Token: 0x040001D2 RID: 466
		public bool ItemfilterFoodAndWater = false;

		// Token: 0x040001D3 RID: 467
		public bool ItemfilterCustom = true;

		// Token: 0x040001D4 RID: 468
		public string searchstring = "";

		// Token: 0x040001D5 RID: 469
		public SerializableVector2 additemscroll = new SerializableVector2(0f, 0f);

		// Token: 0x040001D6 RID: 470
		public SerializableVector2 removeitemscroll = new SerializableVector2(0f, 0f);
	}
}

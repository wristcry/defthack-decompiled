using System;

namespace DeftHack.Misc.Classes.Skins
{
	// Token: 0x02000070 RID: 112
	public class WeaponSave
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000296C File Offset: 0x00000B6C
		public WeaponSave(ushort WeaponID, int SkinID)
		{
			this.WeaponID = WeaponID;
			this.SkinID = SkinID;
		}

		// Token: 0x040001C8 RID: 456
		public ushort WeaponID;

		// Token: 0x040001C9 RID: 457
		public int SkinID;
	}
}

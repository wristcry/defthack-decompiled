using System;
using System.Collections.Generic;
using DeftHack.Misc.Enums;

namespace DeftHack.Misc.Classes.Skins
{
	// Token: 0x0200006F RID: 111
	public class SkinOptionList
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00002949 File Offset: 0x00000B49
		public SkinOptionList(SkinType Type)
		{
			this.Type = Type;
		}

		// Token: 0x040001C6 RID: 454
		public SkinType Type = SkinType.Weapons;

		// Token: 0x040001C7 RID: 455
		public HashSet<Skin> Skins = new HashSet<Skin>();
	}
}

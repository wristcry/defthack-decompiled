using System;
using DeftHack.Misc.Enums;
using UnityEngine;

namespace DeftHack.Misc.Classes.ESP
{
	// Token: 0x02000075 RID: 117
	public class ESPObject
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000299C File Offset: 0x00000B9C
		public ESPObject(ESPTarget t, object o, GameObject go)
		{
			this.Target = t;
			this.Object = o;
			this.GObject = go;
		}

		// Token: 0x040001DD RID: 477
		public ESPTarget Target;

		// Token: 0x040001DE RID: 478
		public object Object;

		// Token: 0x040001DF RID: 479
		public GameObject GObject;
	}
}

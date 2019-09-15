using System;
using UnityEngine;

namespace DeftHack.Misc.Classes.Misc
{
	// Token: 0x02000072 RID: 114
	public class LocalBounds
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00002984 File Offset: 0x00000B84
		public LocalBounds(Vector3 po, Vector3 e)
		{
			this.PosOffset = po;
			this.Extents = e;
		}

		// Token: 0x040001D7 RID: 471
		public Vector3 PosOffset;

		// Token: 0x040001D8 RID: 472
		public Vector3 Extents;
	}
}

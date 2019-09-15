using System;
using UnityEngine;

namespace DeftHack.Misc.Serializables
{
	// Token: 0x02000062 RID: 98
	public class SerializableVector2
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00002905 File Offset: 0x00000B05
		public SerializableVector2(float nx, float ny)
		{
			this.x = nx;
			this.y = ny;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000C294 File Offset: 0x0000A494
		public Vector2 ToVector2()
		{
			return new Vector2(this.x, this.y);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		public static implicit operator Vector2(SerializableVector2 vector)
		{
			return vector.ToVector2();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public static implicit operator SerializableVector2(Vector2 vector)
		{
			return new SerializableVector2(vector.x, vector.y);
		}

		// Token: 0x04000153 RID: 339
		public float x;

		// Token: 0x04000154 RID: 340
		public float y;
	}
}

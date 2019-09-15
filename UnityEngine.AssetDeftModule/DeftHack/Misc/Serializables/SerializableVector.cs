using System;
using UnityEngine;

namespace DeftHack.Misc.Serializables
{
	// Token: 0x02000061 RID: 97
	public class SerializableVector
	{
		// Token: 0x0600013B RID: 315 RVA: 0x000028E6 File Offset: 0x00000AE6
		public SerializableVector(float nx, float ny, float nz)
		{
			this.x = nx;
			this.y = ny;
			this.z = nz;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000C250 File Offset: 0x0000A450
		public Vector3 ToVector()
		{
			return new Vector3(this.x, this.y, this.z);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000C27C File Offset: 0x0000A47C
		public static implicit operator Vector3(SerializableVector vector)
		{
			return vector.ToVector();
		}

		// Token: 0x04000150 RID: 336
		public float x;

		// Token: 0x04000151 RID: 337
		public float y;

		// Token: 0x04000152 RID: 338
		public float z;
	}
}

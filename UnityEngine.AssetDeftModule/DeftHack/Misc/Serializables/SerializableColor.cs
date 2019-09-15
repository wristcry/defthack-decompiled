using System;
using UnityEngine;

namespace DeftHack.Misc.Serializables
{
	// Token: 0x02000060 RID: 96
	public class SerializableColor
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000288B File Offset: 0x00000A8B
		public SerializableColor()
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002895 File Offset: 0x00000A95
		public SerializableColor(int nr, int ng, int nb, int na)
		{
			this.r = nr;
			this.g = ng;
			this.b = nb;
			this.a = na;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000028BC File Offset: 0x00000ABC
		public SerializableColor(int nr, int ng, int nb)
		{
			this.r = nr;
			this.g = ng;
			this.b = nb;
			this.a = 255;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public static implicit operator Color32(SerializableColor color)
		{
			return color.ToColor();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		public static implicit operator Color(SerializableColor color)
		{
			return color.ToColor();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000C204 File Offset: 0x0000A404
		public static implicit operator SerializableColor(Color32 color)
		{
			return color.ToSerializableColor();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000C21C File Offset: 0x0000A41C
		public Color32 ToColor()
		{
			return new Color32((byte)this.r, (byte)this.g, (byte)this.b, (byte)this.a);
		}

		// Token: 0x0400014C RID: 332
		public int r;

		// Token: 0x0400014D RID: 333
		public int g;

		// Token: 0x0400014E RID: 334
		public int b;

		// Token: 0x0400014F RID: 335
		public int a;
	}
}

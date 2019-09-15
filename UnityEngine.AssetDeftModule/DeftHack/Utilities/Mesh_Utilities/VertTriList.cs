using System;
using UnityEngine;

namespace DeftHack.Utilities.Mesh_Utilities
{
	// Token: 0x0200002E RID: 46
	public class VertTriList
	{
		// Token: 0x1700000D RID: 13
		public int[] this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000024E1 File Offset: 0x000006E1
		public VertTriList(int[] tri, int numVerts)
		{
			this.Init(tri, numVerts);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000024F4 File Offset: 0x000006F4
		public VertTriList(Mesh mesh)
		{
			this.Init(mesh.triangles, mesh.vertexCount);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00008744 File Offset: 0x00006944
		public void Init(int[] tri, int numVerts)
		{
			int[] array = new int[numVerts];
			for (int i = 0; i < tri.Length; i++)
			{
				array[tri[i]]++;
			}
			this.list = new int[numVerts][];
			for (int j = 0; j < array.Length; j++)
			{
				this.list[j] = new int[array[j]];
			}
			for (int k = 0; k < tri.Length; k++)
			{
				int num = tri[k];
				int[] array2 = this.list[num];
				int[] array3 = array;
				int num2 = num;
				int num3 = array3[num2] - 1;
				array3[num2] = num3;
				array2[num3] = k / 3;
			}
		}

		// Token: 0x04000069 RID: 105
		public int[][] list;
	}
}

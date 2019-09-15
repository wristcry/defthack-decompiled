using System;
using UnityEngine;

namespace DeftHack.Utilities.Mesh_Utilities
{
	// Token: 0x0200002D RID: 45
	public static class NearestPointTest
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000083DC File Offset: 0x000065DC
		private static Vector3 NearestPointOnMesh(Vector3 pt, Vector3[] verts, int[] tri, VertTriList vt)
		{
			int num = -1;
			float num2 = 1E+08f;
			for (int i = 0; i < verts.Length; i++)
			{
				float sqrMagnitude = (verts[i] - pt).sqrMagnitude;
				bool flag = sqrMagnitude >= num2;
				bool flag2 = !flag;
				if (flag2)
				{
					num = i;
					num2 = sqrMagnitude;
				}
			}
			bool flag3 = num == -1;
			bool flag4 = flag3;
			Vector3 result;
			if (flag4)
			{
				result = Vector3.zero;
			}
			else
			{
				int[] array = vt[num];
				Vector3 vector = Vector3.zero;
				num2 = 1E+08f;
				for (int j = 0; j < array.Length; j++)
				{
					int num3 = array[j] * 3;
					Vector3 vector2 = verts[tri[num3]];
					Vector3 vector3 = verts[tri[num3 + 1]];
					Vector3 vector4 = verts[tri[num3 + 2]];
					Vector3 vector5 = NearestPointTest.NearestPointOnTri(pt, vector2, vector3, vector4);
					float sqrMagnitude2 = (pt - vector5).sqrMagnitude;
					bool flag5 = sqrMagnitude2 >= num2;
					bool flag6 = !flag5;
					if (flag6)
					{
						vector = vector5;
						num2 = sqrMagnitude2;
					}
				}
				result = vector;
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000850C File Offset: 0x0000670C
		public static Vector3 NearestPointOnTri(Vector3 pt, Vector3 a, Vector3 b, Vector3 c)
		{
			Vector3 lhs = b - a;
			Vector3 vector = c - a;
			Vector3 lhs2 = c - b;
			float magnitude = lhs.magnitude;
			float magnitude2 = vector.magnitude;
			float magnitude3 = lhs2.magnitude;
			Vector3 vector2 = pt - a;
			Vector3 rhs = pt - b;
			Vector3 vector3 = pt - c;
			Vector3 vector4 = lhs / magnitude;
			Vector3 normalized = Vector3.Cross(lhs, vector).normalized;
			Vector3 rhs2 = Vector3.Cross(normalized, vector4);
			Vector3 lhs3 = Vector3.Cross(lhs, vector2);
			Vector3 lhs4 = Vector3.Cross(vector, -vector3);
			Vector3 lhs5 = Vector3.Cross(lhs2, rhs);
			bool flag = Vector3.Dot(lhs3, normalized) > 0f;
			bool flag2 = Vector3.Dot(lhs4, normalized) > 0f;
			bool flag3 = Vector3.Dot(lhs5, normalized) > 0f;
			bool flag4 = flag && flag2 && flag3;
			bool flag5 = flag4;
			Vector3 result;
			if (flag5)
			{
				float d = Vector3.Dot(vector2, vector4);
				float d2 = Vector3.Dot(vector2, rhs2);
				result = a + vector4 * d + rhs2 * d2;
			}
			else
			{
				Vector3 lhs6 = vector4;
				Vector3 normalized2 = vector.normalized;
				Vector3 normalized3 = lhs2.normalized;
				float d3 = Mathf.Clamp(Vector3.Dot(lhs6, vector2), 0f, magnitude);
				float d4 = Mathf.Clamp(Vector3.Dot(normalized2, vector2), 0f, magnitude2);
				float d5 = Mathf.Clamp(Vector3.Dot(normalized3, rhs), 0f, magnitude3);
				Vector3 vector5 = a + d3 * lhs6;
				Vector3 vector6 = a + d4 * normalized2;
				Vector3 vector7 = b + d5 * normalized3;
				float sqrMagnitude = (pt - vector5).sqrMagnitude;
				float sqrMagnitude2 = (pt - vector6).sqrMagnitude;
				float sqrMagnitude3 = (pt - vector7).sqrMagnitude;
				result = ((sqrMagnitude < sqrMagnitude2) ? ((sqrMagnitude < sqrMagnitude3) ? vector5 : vector7) : ((sqrMagnitude2 < sqrMagnitude3) ? vector6 : vector7));
			}
			return result;
		}

		// Token: 0x04000065 RID: 101
		public static Vector3 a;

		// Token: 0x04000066 RID: 102
		public static Vector3 b;

		// Token: 0x04000067 RID: 103
		public static Vector3 c;

		// Token: 0x04000068 RID: 104
		public static Vector3 pt;
	}
}

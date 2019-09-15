using System;
using System.Collections.Generic;
using DeftHack.Attributes;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x0200001D RID: 29
	public static class MathUtilities
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000023AB File Offset: 0x000005AB
		[Initializer]
		public static void GenerateRandom()
		{
			MathUtilities.Random = new System.Random();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006010 File Offset: 0x00004210
		public static bool Intersect(Vector3 p0, Vector3 p1, Vector3 oVector, Vector3 bCenter, out Vector3 intersection)
		{
			intersection = Vector3.zero;
			Vector3 vector = p1 - p0;
			float num = Vector3.Dot(vector, oVector);
			bool flag = num == 0f;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				float num2 = Vector3.Dot(p0 - bCenter, oVector);
				float d = -(num2 / num);
				intersection = p0 + d * vector;
				result = true;
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006088 File Offset: 0x00004288
		public static Vector3 GetOrthogonalVector(Vector3 vCenter, Vector3 vPoint)
		{
			Vector3 a = vCenter - vPoint;
			double distance = VectorUtilities.GetDistance(vCenter, vPoint);
			return a / (float)distance;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000060B4 File Offset: 0x000042B4
		public static Vector3[] GetRectanglePoints(Vector3 playerPos, Vector3[] bCorners, Bounds bound)
		{
			Vector3 orthogonalVector = MathUtilities.GetOrthogonalVector(bound.center, playerPos);
			List<Vector3> list = new List<Vector3>();
			Vector3[] array = new Vector3[]
			{
				bCorners[0],
				bCorners[1],
				bCorners[1],
				bCorners[3],
				bCorners[3],
				bCorners[2],
				bCorners[2],
				bCorners[0],
				bCorners[4],
				bCorners[5],
				bCorners[5],
				bCorners[7],
				bCorners[7],
				bCorners[6],
				bCorners[6],
				bCorners[4],
				bCorners[0],
				bCorners[4],
				bCorners[1],
				bCorners[5],
				bCorners[2],
				bCorners[6],
				bCorners[3],
				bCorners[7]
			};
			for (int i = 0; i < 24; i += 2)
			{
				Vector3 p = array[i];
				Vector3 p2 = array[i + 1];
				Vector3 item;
				bool flag = MathUtilities.Intersect(p, p2, orthogonalVector, bound.center, out item);
				bool flag2 = flag;
				if (flag2)
				{
					list.Add(item);
				}
			}
			Bounds bounds = new Bounds(bound.center, bound.size * 1.2f);
			for (int j = list.Count - 1; j > -1; j--)
			{
				bool flag3 = !bounds.Contains(list[j]);
				bool flag4 = flag3;
				if (flag4)
				{
					list.RemoveAt(j);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400004A RID: 74
		public static System.Random Random;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeftHack.Utilities.Mesh_Utilities
{
	// Token: 0x0200002B RID: 43
	public static class IcoSphere
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00007CE4 File Offset: 0x00005EE4
		private static int getMiddlePoint(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius)
		{
			bool flag = p1 < p2;
			long num = (long)(flag ? p1 : p2);
			long num2 = (long)(flag ? p2 : p1);
			long key = (num << 32) + num2;
			int num3;
			bool flag2 = cache.TryGetValue(key, out num3);
			bool flag3 = flag2;
			int result;
			if (flag3)
			{
				result = num3;
			}
			else
			{
				Vector3 vector = vertices[p1];
				Vector3 vector2 = vertices[p2];
				Vector3 vector3 = new Vector3((vector.x + vector2.x) / 2f, (vector.y + vector2.y) / 2f, (vector.z + vector2.z) / 2f);
				int count = vertices.Count;
				vertices.Add(vector3.normalized * radius);
				cache.Add(key, count);
				result = count;
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public static GameObject Create(string name, float radius, float recursionLevel)
		{
			GameObject gameObject = new GameObject(name);
			Mesh mesh = new Mesh
			{
				name = name
			};
			List<Vector3> list = new List<Vector3>();
			Dictionary<long, int> dictionary = new Dictionary<long, int>();
			float num = (1f + Mathf.Sqrt(5f)) / 2f;
			list.Add(new Vector3(-1f, num, 0f).normalized * radius);
			list.Add(new Vector3(1f, num, 0f).normalized * radius);
			list.Add(new Vector3(-1f, -num, 0f).normalized * radius);
			list.Add(new Vector3(1f, -num, 0f).normalized * radius);
			list.Add(new Vector3(0f, -1f, num).normalized * radius);
			list.Add(new Vector3(0f, 1f, num).normalized * radius);
			list.Add(new Vector3(0f, -1f, -num).normalized * radius);
			list.Add(new Vector3(0f, 1f, -num).normalized * radius);
			list.Add(new Vector3(num, 0f, -1f).normalized * radius);
			list.Add(new Vector3(num, 0f, 1f).normalized * radius);
			list.Add(new Vector3(-num, 0f, -1f).normalized * radius);
			list.Add(new Vector3(-num, 0f, 1f).normalized * radius);
			List<IcoSphere.TriangleIndices> list2 = new List<IcoSphere.TriangleIndices>
			{
				new IcoSphere.TriangleIndices(0, 11, 5),
				new IcoSphere.TriangleIndices(0, 5, 1),
				new IcoSphere.TriangleIndices(0, 1, 7),
				new IcoSphere.TriangleIndices(0, 7, 10),
				new IcoSphere.TriangleIndices(0, 10, 11),
				new IcoSphere.TriangleIndices(1, 5, 9),
				new IcoSphere.TriangleIndices(5, 11, 4),
				new IcoSphere.TriangleIndices(11, 10, 2),
				new IcoSphere.TriangleIndices(10, 7, 6),
				new IcoSphere.TriangleIndices(7, 1, 8),
				new IcoSphere.TriangleIndices(3, 9, 4),
				new IcoSphere.TriangleIndices(3, 4, 2),
				new IcoSphere.TriangleIndices(3, 2, 6),
				new IcoSphere.TriangleIndices(3, 6, 8),
				new IcoSphere.TriangleIndices(3, 8, 9),
				new IcoSphere.TriangleIndices(4, 9, 5),
				new IcoSphere.TriangleIndices(2, 4, 11),
				new IcoSphere.TriangleIndices(6, 2, 10),
				new IcoSphere.TriangleIndices(8, 6, 7),
				new IcoSphere.TriangleIndices(9, 8, 1)
			};
			int num2 = 0;
			while ((float)num2 < recursionLevel)
			{
				List<IcoSphere.TriangleIndices> list3 = new List<IcoSphere.TriangleIndices>();
				for (int i = 0; i < list2.Count; i++)
				{
					IcoSphere.TriangleIndices triangleIndices = list2[i];
					int middlePoint = IcoSphere.getMiddlePoint(triangleIndices.v1, triangleIndices.v2, ref list, ref dictionary, radius);
					int middlePoint2 = IcoSphere.getMiddlePoint(triangleIndices.v2, triangleIndices.v3, ref list, ref dictionary, radius);
					int middlePoint3 = IcoSphere.getMiddlePoint(triangleIndices.v3, triangleIndices.v1, ref list, ref dictionary, radius);
					list3.Add(new IcoSphere.TriangleIndices(triangleIndices.v1, middlePoint, middlePoint3));
					list3.Add(new IcoSphere.TriangleIndices(triangleIndices.v2, middlePoint2, middlePoint));
					list3.Add(new IcoSphere.TriangleIndices(triangleIndices.v3, middlePoint3, middlePoint2));
					list3.Add(new IcoSphere.TriangleIndices(middlePoint, middlePoint2, middlePoint3));
				}
				list2 = list3;
				num2++;
			}
			mesh.vertices = list.ToArray();
			List<int> list4 = new List<int>();
			for (int j = 0; j < list2.Count; j++)
			{
				list4.Add(list2[j].v1);
				list4.Add(list2[j].v2);
				list4.Add(list2[j].v3);
			}
			mesh.triangles = list4.ToArray();
			Vector3[] vertices = mesh.vertices;
			Vector2[] array = new Vector2[vertices.Length];
			for (int k = 0; k < vertices.Length; k++)
			{
				Vector3 normalized = vertices[k].normalized;
				Vector2 vector = new Vector2(0f, 0f);
				vector.x = (Mathf.Atan2(normalized.x, normalized.z) + 3.14159274f) / 3.14159274f / 2f;
				vector.y = (Mathf.Acos(normalized.y) + 3.14159274f) / 3.14159274f - 1f;
				array[k] = new Vector2(vector.x, vector.y);
			}
			mesh.uv = array;
			Vector3[] array2 = new Vector3[list.Count];
			for (int l = 0; l < array2.Length; l++)
			{
				array2[l] = list[l].normalized;
			}
			mesh.normals = array2;
			mesh.RecalculateBounds();
			gameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
			return gameObject;
		}

		// Token: 0x0200002C RID: 44
		private struct TriangleIndices
		{
			// Token: 0x060000AF RID: 175 RVA: 0x000024C9 File Offset: 0x000006C9
			public TriangleIndices(int v1, int v2, int v3)
			{
				this.v1 = v1;
				this.v2 = v2;
				this.v3 = v3;
			}

			// Token: 0x04000062 RID: 98
			public int v1;

			// Token: 0x04000063 RID: 99
			public int v2;

			// Token: 0x04000064 RID: 100
			public int v3;
		}
	}
}

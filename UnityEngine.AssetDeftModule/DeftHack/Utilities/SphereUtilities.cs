﻿using System;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Components.Basic;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x02000028 RID: 40
	public static class SphereUtilities
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00007A20 File Offset: 0x00005C20
		public static bool GetRaycast(GameObject Target, Vector3 StartPos, out Vector3 Point)
		{
			Point = Vector3.zero;
			bool flag = Target == null;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int layer = Target.layer;
				Target.layer = LayerMasks.AGENT;
				RaycastComponent Component = Target.GetComponent<RaycastComponent>();
				bool flag3 = VectorUtilities.GetDistance(Target.transform.position, StartPos) <= 15.5;
				bool flag4 = flag3;
				if (flag4)
				{
					Point = OptimizationVariables.MainPlayer.transform.position;
					result = true;
				}
				else
				{
					Vector3[] vertices = Component.Sphere.GetComponent<MeshCollider>().sharedMesh.vertices;
					IEnumerable<Vector3> source = vertices;
					Func<Vector3, Vector3> selector;
					Func<Vector3, Vector3> <>9__0;
					if ((selector = <>9__0) == null)
					{
						selector = (<>9__0 = ((Vector3 v) => Component.Sphere.transform.TransformPoint(v)));
					}
					foreach (Vector3 vector in source.Select(selector).ToArray<Vector3>())
					{
						Vector3 direction = VectorUtilities.Normalize(vector - StartPos);
						double distance = VectorUtilities.GetDistance(StartPos, vector);
						bool flag5 = Physics.Raycast(StartPos, direction, (float)distance + 0.5f, RayMasks.DAMAGE_CLIENT);
						bool flag6 = !flag5;
						if (flag6)
						{
							Target.layer = layer;
							Point = vector;
							return true;
						}
					}
					Target.layer = layer;
					result = false;
				}
			}
			return result;
		}
	}
}

using System;
using DeftHack.Variables;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x0200002A RID: 42
	public static class VectorUtilities
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00007B94 File Offset: 0x00005D94
		public static double GetDistance(Vector3 point)
		{
			return VectorUtilities.GetDistance(OptimizationVariables.MainCam.transform.position, point);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007BBC File Offset: 0x00005DBC
		public static double GetDistance(Vector3 start, Vector3 point)
		{
			Vector3 vector;
			vector.x = start.x - point.x;
			vector.y = start.y - point.y;
			vector.z = start.z - point.z;
			return Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007C3C File Offset: 0x00005E3C
		public static double GetMagnitude(Vector3 vector)
		{
			return Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007C80 File Offset: 0x00005E80
		public static Vector3 Normalize(Vector3 vector)
		{
			return vector / (float)VectorUtilities.GetMagnitude(vector);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public static double GetAngleDelta(Vector3 mainPos, Vector3 forward, Vector3 target)
		{
			Vector3 lhs = VectorUtilities.Normalize(target - mainPos);
			return Math.Atan2(VectorUtilities.GetMagnitude(Vector3.Cross(lhs, forward)), (double)Vector3.Dot(lhs, forward)) * 57.295779513082323;
		}
	}
}

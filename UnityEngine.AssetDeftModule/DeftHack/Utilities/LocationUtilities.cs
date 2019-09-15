using System;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x0200001B RID: 27
	public static class LocationUtilities
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00005F60 File Offset: 0x00004160
		public static LocationNode GetClosestLocation(Vector3 pos)
		{
			double num = 1337420.0;
			LocationNode result = null;
			foreach (LocationNode locationNode in (from n in LevelNodes.nodes
			where n.type == ENodeType.LOCATION
			select (LocationNode)n).ToArray<LocationNode>())
			{
				double distance = VectorUtilities.GetDistance(pos, locationNode.point);
				bool flag = distance < num;
				bool flag2 = flag;
				if (flag2)
				{
					num = distance;
					result = locationNode;
				}
			}
			return result;
		}
	}
}

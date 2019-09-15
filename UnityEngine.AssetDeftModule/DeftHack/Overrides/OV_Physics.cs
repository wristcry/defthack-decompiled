using System;
using DeftHack.Utilities;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x02000042 RID: 66
	public static class OV_Physics
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000092B0 File Offset: 0x000074B0
		public static bool OV_Linecast(Vector3 start, Vector3 end, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return !OV_Physics.ForceReturnFalse && (bool)OverrideUtilities.CallOriginal(null, new object[]
			{
				start,
				end,
				layerMask,
				queryTriggerInteraction
			});
		}

		// Token: 0x04000086 RID: 134
		public static bool ForceReturnFalse = false;
	}
}

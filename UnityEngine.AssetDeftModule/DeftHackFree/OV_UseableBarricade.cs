using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHackFree
{
	// Token: 0x020000D5 RID: 213
	public class OV_UseableBarricade
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		[Override(typeof(UseableBarricade), "checkSpace", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public bool OV_checkSpace()
		{
			bool flag = !MiscOptions.BuildinObstacles || PlayerCoroutines.IsSpying;
			bool result;
			if (flag)
			{
				result = (bool)OverrideUtilities.CallOriginal(this, new object[0]);
			}
			else
			{
				OverrideUtilities.CallOriginal(this, new object[0]);
				bool flag2 = (Vector3)OV_UseableBarricade.pointField.GetValue(this) != Vector3.zero && !MiscOptions.Freecam;
				if (flag2)
				{
					bool epos = MiscOptions.epos;
					if (epos)
					{
						OV_UseableBarricade.pointField.SetValue(this, (Vector3)OV_UseableBarricade.pointField.GetValue(this) + MiscOptions.pos);
					}
					result = true;
				}
				else
				{
					RaycastHit raycastHit;
					bool flag3 = Physics.Raycast(new Ray(OptimizationVariables.MainCam.transform.position, OptimizationVariables.MainCam.transform.forward), out raycastHit, 20f, RayMasks.DAMAGE_CLIENT);
					if (flag3)
					{
						Vector3 vector = raycastHit.point;
						bool epos2 = MiscOptions.epos;
						if (epos2)
						{
							vector += MiscOptions.pos;
						}
						OV_UseableBarricade.pointField.SetValue(this, vector);
						result = true;
					}
					else
					{
						Vector3 vector2 = OptimizationVariables.MainCam.transform.position + OptimizationVariables.MainCam.transform.forward * 7f;
						bool epos3 = MiscOptions.epos;
						if (epos3)
						{
							vector2 += MiscOptions.pos;
						}
						OV_UseableBarricade.pointField.SetValue(this, vector2);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0400042B RID: 1067
		private static FieldInfo pointField = typeof(UseableBarricade).GetField("point", ReflectionVariables.PrivateInstance);
	}
}

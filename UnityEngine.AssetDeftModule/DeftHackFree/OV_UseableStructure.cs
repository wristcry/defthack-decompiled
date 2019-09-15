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
	// Token: 0x020000D6 RID: 214
	public class OV_UseableStructure
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0001E784 File Offset: 0x0001C984
		[Override(typeof(UseableStructure), "checkSpace", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
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
				bool flag2 = (Vector3)OV_UseableStructure.pointField.GetValue(this) != Vector3.zero && !MiscOptions.Freecam;
				if (flag2)
				{
					bool epos = MiscOptions.epos;
					if (epos)
					{
						OV_UseableStructure.pointField.SetValue(this, (Vector3)OV_UseableStructure.pointField.GetValue(this) + MiscOptions.pos);
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
						OV_UseableStructure.pointField.SetValue(this, vector);
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
						OV_UseableStructure.pointField.SetValue(this, vector2);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0400042C RID: 1068
		public static FieldInfo pointField = typeof(UseableStructure).GetField("point", ReflectionVariables.PrivateInstance);
	}
}

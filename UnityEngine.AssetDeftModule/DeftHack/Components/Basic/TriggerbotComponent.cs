using System;
using System.Collections;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options.AimOptions;
using DeftHack.Overrides;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000C6 RID: 198
	[Component]
	public class TriggerbotComponent : MonoBehaviour
	{
		// Token: 0x06000304 RID: 772 RVA: 0x000033F9 File Offset: 0x000015F9
		[Initializer]
		public static void Init()
		{
			TriggerbotComponent.CurrentFiremode = typeof(UseableGun).GetField("firemode", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00003417 File Offset: 0x00001617
		public void Start()
		{
			base.StartCoroutine(TriggerbotComponent.CheckTrigger());
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00003426 File Offset: 0x00001626
		public static IEnumerator CheckTrigger()
		{
			for (;;)
			{
				yield return new WaitForSeconds(0.1f);
				bool flag = !TriggerbotOptions.Enabled || !DrawUtilities.ShouldRun() || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SPRINT || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.CLIMB || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING;
				bool flag7 = flag;
				if (flag7)
				{
					TriggerbotOptions.IsFiring = false;
				}
				else
				{
					PlayerLook look = OptimizationVariables.MainPlayer.look;
					Useable u = OptimizationVariables.MainPlayer.equipment.useable;
					Useable useable = u;
					Useable useable2 = useable;
					bool flag8 = useable2 == null;
					if (flag8)
					{
						TriggerbotOptions.IsFiring = false;
					}
					else
					{
						UseableGun useableGun;
						bool flag9 = (useableGun = (useable2 as UseableGun)) != null;
						UseableGun gun;
						if (flag9)
						{
							gun = useableGun;
							ItemGunAsset PAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
							RaycastInfo ri = RaycastUtilities.GenerateOriginalRaycast(new Ray(look.aim.position, look.aim.forward), PAsset.range, RayMasks.DAMAGE_CLIENT);
							bool flag2 = AimbotCoroutines.LockedObject != null && AimbotCoroutines.IsAiming;
							bool flag10 = flag2;
							if (flag10)
							{
								Ray r = OV_UseableGun.GetAimRay(look.aim.position, AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull"));
								ri = RaycastUtilities.GenerateOriginalRaycast(new Ray(r.origin, r.direction), PAsset.range, RayMasks.DAMAGE_CLIENT);
								r = default(Ray);
								r = default(Ray);
							}
							bool Valid = ri.player != null;
							bool enabled = RaycastOptions.Enabled;
							bool flag11 = enabled;
							if (flag11)
							{
								Valid = RaycastUtilities.GenerateRaycast(out ri);
							}
							bool flag3 = !Valid;
							bool flag12 = flag3;
							if (flag12)
							{
								TriggerbotOptions.IsFiring = false;
								continue;
							}
							EFiremode fire = (EFiremode)TriggerbotComponent.CurrentFiremode.GetValue(gun);
							bool flag4 = fire == EFiremode.AUTO;
							bool flag13 = flag4;
							if (flag13)
							{
								TriggerbotOptions.IsFiring = true;
								continue;
							}
							TriggerbotOptions.IsFiring = !TriggerbotOptions.IsFiring;
							PAsset = null;
							ri = null;
						}
						else
						{
							UseableMelee x = useable2 as UseableMelee;
							bool flag14 = x != null;
							if (flag14)
							{
								ItemMeleeAsset MAsset = (ItemMeleeAsset)OptimizationVariables.MainPlayer.equipment.asset;
								RaycastInfo ri2 = RaycastUtilities.GenerateOriginalRaycast(new Ray(look.aim.position, look.aim.forward), MAsset.range, RayMasks.DAMAGE_CLIENT);
								bool flag5 = AimbotCoroutines.LockedObject != null && AimbotCoroutines.IsAiming;
								bool flag15 = flag5;
								if (flag15)
								{
									Ray r2 = OV_UseableGun.GetAimRay(look.aim.position, AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull"));
									ri2 = RaycastUtilities.GenerateOriginalRaycast(new Ray(r2.origin, r2.direction), MAsset.range, RayMasks.DAMAGE_CLIENT);
									r2 = default(Ray);
									r2 = default(Ray);
								}
								bool Valid2 = ri2.player != null;
								bool enabled2 = RaycastOptions.Enabled;
								bool flag16 = enabled2;
								if (flag16)
								{
									Valid2 = RaycastUtilities.GenerateRaycast(out ri2);
								}
								bool flag6 = !Valid2;
								bool flag17 = flag6;
								if (flag17)
								{
									TriggerbotOptions.IsFiring = false;
									continue;
								}
								bool isRepeated = MAsset.isRepeated;
								bool flag18 = isRepeated;
								if (flag18)
								{
									TriggerbotOptions.IsFiring = true;
									continue;
								}
								TriggerbotOptions.IsFiring = !TriggerbotOptions.IsFiring;
								MAsset = null;
								ri2 = null;
							}
						}
						useable2 = null;
						useableGun = null;
						gun = null;
						look = null;
						u = null;
						useableGun = null;
						gun = null;
					}
					look = null;
					u = null;
					useable = null;
					useable2 = null;
				}
			}
			yield break;
		}

		// Token: 0x04000400 RID: 1024
		public static FieldInfo CurrentFiremode;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x0200004D RID: 77
	public class OV_UseableGun
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00002785 File Offset: 0x00000985
		[Initializer]
		public static void Load()
		{
			OV_UseableGun.BulletsField = typeof(UseableGun).GetField("bullets", ReflectionVariables.PrivateInstance);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000B19C File Offset: 0x0000939C
		public static bool IsRaycastInvalid(RaycastInfo info)
		{
			return info.player == null && info.zombie == null && info.animal == null && info.vehicle == null && info.transform == null;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000B1F8 File Offset: 0x000093F8
		[Override(typeof(UseableGun), "ballistics", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public void OV_ballistics()
		{
			Useable useable = OptimizationVariables.MainPlayer.equipment.useable;
			bool isServer = Provider.isServer;
			bool flag = isServer;
			if (flag)
			{
				OverrideUtilities.CallOriginal(useable, Array.Empty<object>());
			}
			else
			{
				bool flag2 = Time.realtimeSinceStartup - PlayerLifeUI.hitmarkers[0].lastHit > PlayerUI.HIT_TIME;
				bool flag3 = flag2;
				if (flag3)
				{
					PlayerLifeUI.hitmarkers[0].hitBuildImage.isVisible = false;
					PlayerLifeUI.hitmarkers[0].hitCriticalImage.isVisible = false;
					PlayerLifeUI.hitmarkers[0].hitEntitiyImage.isVisible = false;
				}
				ItemGunAsset itemGunAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
				PlayerLook look = OptimizationVariables.MainPlayer.look;
				bool flag4 = itemGunAsset.projectile != null;
				bool flag5 = !flag4;
				if (flag5)
				{
					List<BulletInfo> list = (List<BulletInfo>)OV_UseableGun.BulletsField.GetValue(useable);
					bool flag6 = list.Count == 0;
					bool flag7 = !flag6;
					if (flag7)
					{
						RaycastInfo raycastInfo = null;
						bool enabled = RaycastOptions.Enabled;
						bool flag8 = enabled;
						if (flag8)
						{
							RaycastUtilities.GenerateRaycast(out raycastInfo);
						}
						bool ballistics = Provider.modeConfigData.Gameplay.Ballistics;
						bool flag9 = ballistics;
						if (flag9)
						{
							bool flag10 = raycastInfo == null;
							bool flag11 = flag10;
							if (flag11)
							{
								bool noAimbotDrop = AimbotOptions.NoAimbotDrop;
								bool flag12 = noAimbotDrop;
								if (flag12)
								{
									bool flag13 = AimbotCoroutines.IsAiming && AimbotCoroutines.LockedObject != null;
									bool flag14 = flag13;
									if (flag14)
									{
										Vector3 aimPosition = AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull");
										Ray aimRay = OV_UseableGun.GetAimRay(look.aim.position, aimPosition);
										float maxDistance = (float)VectorUtilities.GetDistance(look.aim.position, aimPosition);
										RaycastHit raycastHit;
										bool flag15 = !Physics.Raycast(aimRay, out raycastHit, maxDistance, RayMasks.DAMAGE_SERVER);
										bool flag16 = flag15;
										if (flag16)
										{
											raycastInfo = RaycastUtilities.GenerateOriginalRaycast(aimRay, itemGunAsset.range, RayMasks.ENEMY);
										}
									}
								}
								bool flag17 = WeaponOptions.NoDrop && raycastInfo == null;
								bool flag18 = flag17;
								if (flag18)
								{
									for (int i = 0; i < list.Count; i++)
									{
										BulletInfo bulletInfo = list[i];
										Ray ray = new Ray(bulletInfo.pos, bulletInfo.dir);
										RaycastInfo info = DamageTool.raycast(ray, itemGunAsset.ballisticTravel, RayMasks.DAMAGE_CLIENT);
										bool flag19 = OV_UseableGun.IsRaycastInvalid(info);
										bool flag20 = flag19;
										if (flag20)
										{
											bulletInfo.pos += bulletInfo.dir * itemGunAsset.ballisticTravel;
										}
										else
										{
											EPlayerHit newHit = OV_UseableGun.CalcHitMarker(itemGunAsset, ref info);
											PlayerUI.hitmark(0, Vector3.zero, false, newHit);
											OptimizationVariables.MainPlayer.input.sendRaycast(info);
											bulletInfo.steps = 254;
										}
									}
									for (int j = list.Count - 1; j >= 0; j--)
									{
										BulletInfo bulletInfo2 = list[j];
										BulletInfo bulletInfo3 = bulletInfo2;
										BulletInfo bulletInfo4 = bulletInfo3;
										bulletInfo4.steps += 1;
										bool flag21 = bulletInfo2.steps >= itemGunAsset.ballisticSteps;
										bool flag22 = flag21;
										if (flag22)
										{
											list.RemoveAt(j);
										}
									}
									return;
								}
								bool flag23 = raycastInfo == null;
								bool flag24 = flag23;
								if (flag24)
								{
									OverrideUtilities.CallOriginal(useable, Array.Empty<object>());
									return;
								}
							}
							for (int k = 0; k < list.Count; k++)
							{
								BulletInfo bulletInfo5 = list[k];
								double distance = VectorUtilities.GetDistance(OptimizationVariables.MainPlayer.transform.position, raycastInfo.point);
								bool flag25 = (double)((float)bulletInfo5.steps * itemGunAsset.ballisticTravel) < distance;
								bool flag26 = !flag25;
								if (flag26)
								{
									EPlayerHit newHit2 = OV_UseableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
									PlayerUI.hitmark(0, Vector3.zero, false, newHit2);
									OptimizationVariables.MainPlayer.input.sendRaycast(raycastInfo);
									bulletInfo5.steps = 254;
								}
							}
							for (int l = list.Count - 1; l >= 0; l--)
							{
								BulletInfo bulletInfo6 = list[l];
								BulletInfo bulletInfo7 = bulletInfo6;
								BulletInfo bulletInfo8 = bulletInfo7;
								bulletInfo8.steps += 1;
								bool flag27 = bulletInfo6.steps >= itemGunAsset.ballisticSteps;
								bool flag28 = flag27;
								if (flag28)
								{
									list.RemoveAt(l);
								}
							}
						}
						else
						{
							bool flag29 = raycastInfo != null;
							bool flag30 = flag29;
							if (flag30)
							{
								for (int m = 0; m < list.Count; m++)
								{
									EPlayerHit newHit3 = OV_UseableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
									PlayerUI.hitmark(0, Vector3.zero, false, newHit3);
									OptimizationVariables.MainPlayer.input.sendRaycast(raycastInfo);
								}
								list.Clear();
							}
							else
							{
								OverrideUtilities.CallOriginal(useable, Array.Empty<object>());
							}
						}
					}
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000B6FC File Offset: 0x000098FC
		public static EPlayerHit CalcHitMarker(ItemGunAsset PAsset, ref RaycastInfo ri)
		{
			EPlayerHit eplayerHit = EPlayerHit.NONE;
			bool flag = ri == null || PAsset == null;
			bool flag2 = flag;
			EPlayerHit result;
			if (flag2)
			{
				result = eplayerHit;
			}
			else
			{
				bool flag3 = ri.animal || ri.player || ri.zombie;
				bool flag4 = flag3;
				if (flag4)
				{
					eplayerHit = EPlayerHit.ENTITIY;
					bool flag5 = ri.limb == ELimb.SKULL;
					bool flag6 = flag5;
					if (flag6)
					{
						eplayerHit = EPlayerHit.CRITICAL;
					}
				}
				else
				{
					bool flag7 = ri.transform;
					bool flag8 = flag7;
					if (flag8)
					{
						bool flag9 = ri.transform.CompareTag("Barricade") && PAsset.barricadeDamage > 1f;
						bool flag10 = flag9;
						if (flag10)
						{
							InteractableDoorHinge component = ri.transform.GetComponent<InteractableDoorHinge>();
							bool flag11 = component != null;
							bool flag12 = flag11;
							if (flag12)
							{
								ri.transform = component.transform.parent.parent;
							}
							ushort id;
							bool flag13 = !ushort.TryParse(ri.transform.name, out id);
							bool flag14 = flag13;
							if (flag14)
							{
								return eplayerHit;
							}
							ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id);
							bool flag15 = itemBarricadeAsset == null || (!itemBarricadeAsset.isVulnerable && !PAsset.isInvulnerable);
							bool flag16 = flag15;
							if (flag16)
							{
								return eplayerHit;
							}
							bool flag17 = eplayerHit == EPlayerHit.NONE;
							bool flag18 = flag17;
							if (flag18)
							{
								eplayerHit = EPlayerHit.BUILD;
							}
						}
						else
						{
							bool flag19 = ri.transform.CompareTag("Structure") && PAsset.structureDamage > 1f;
							bool flag20 = flag19;
							if (flag20)
							{
								ushort id2;
								bool flag21 = !ushort.TryParse(ri.transform.name, out id2);
								bool flag22 = flag21;
								if (flag22)
								{
									return eplayerHit;
								}
								ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id2);
								bool flag23 = itemStructureAsset == null || (!itemStructureAsset.isVulnerable && !PAsset.isInvulnerable);
								bool flag24 = flag23;
								if (flag24)
								{
									return eplayerHit;
								}
								bool flag25 = eplayerHit == EPlayerHit.NONE;
								bool flag26 = flag25;
								if (flag26)
								{
									eplayerHit = EPlayerHit.BUILD;
								}
							}
							else
							{
								bool flag27 = ri.transform.CompareTag("Resource") && PAsset.resourceDamage > 1f;
								bool flag28 = flag27;
								if (flag28)
								{
									byte x;
									byte y;
									ushort index;
									bool flag29 = !ResourceManager.tryGetRegion(ri.transform, out x, out y, out index);
									bool flag30 = flag29;
									if (flag30)
									{
										return eplayerHit;
									}
									ResourceSpawnpoint resourceSpawnpoint = ResourceManager.getResourceSpawnpoint(x, y, index);
									bool flag31 = resourceSpawnpoint == null || resourceSpawnpoint.isDead || resourceSpawnpoint.asset.bladeID != PAsset.bladeID;
									bool flag32 = flag31;
									if (flag32)
									{
										return eplayerHit;
									}
									bool flag33 = eplayerHit == EPlayerHit.NONE;
									bool flag34 = flag33;
									if (flag34)
									{
										eplayerHit = EPlayerHit.BUILD;
									}
								}
								else
								{
									bool flag35 = PAsset.objectDamage > 1f;
									bool flag36 = flag35;
									if (flag36)
									{
										InteractableObjectRubble component2 = ri.transform.GetComponent<InteractableObjectRubble>();
										bool flag37 = component2 == null;
										bool flag38 = flag37;
										if (flag38)
										{
											return eplayerHit;
										}
										ri.section = component2.getSection(ri.collider.transform);
										bool flag39 = component2.isSectionDead(ri.section) || (!component2.asset.rubbleIsVulnerable && !PAsset.isInvulnerable);
										bool flag40 = flag39;
										if (flag40)
										{
											return eplayerHit;
										}
										bool flag41 = eplayerHit == EPlayerHit.NONE;
										bool flag42 = flag41;
										if (flag42)
										{
											eplayerHit = EPlayerHit.BUILD;
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag43 = ri.vehicle && !ri.vehicle.isDead && PAsset.vehicleDamage > 1f;
						bool flag44 = flag43;
						if (flag44)
						{
							bool flag45 = ri.vehicle.asset != null && (ri.vehicle.asset.isVulnerable || PAsset.isInvulnerable);
							bool flag46 = flag45;
							if (flag46)
							{
								bool flag47 = eplayerHit == EPlayerHit.NONE;
								bool flag48 = flag47;
								if (flag48)
								{
									eplayerHit = EPlayerHit.BUILD;
								}
							}
						}
					}
				}
				result = eplayerHit;
			}
			return result;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000BB38 File Offset: 0x00009D38
		public static Ray GetAimRay(Vector3 origin, Vector3 pos)
		{
			Vector3 direction = VectorUtilities.Normalize(pos - origin);
			return new Ray(pos, direction);
		}

		// Token: 0x040000A9 RID: 169
		private static FieldInfo BulletsField;
	}
}

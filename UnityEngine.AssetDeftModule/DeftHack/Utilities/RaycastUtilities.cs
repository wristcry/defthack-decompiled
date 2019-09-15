using System;
using System.Collections.Generic;
using DeftHack.Components.Basic;
using DeftHack.Coroutines;
using DeftHack.Options.AimOptions;
using DeftHack.Variables;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x02000025 RID: 37
	public static class RaycastUtilities
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00006864 File Offset: 0x00004A64
		public static bool NoShootthroughthewalls(Transform transform)
		{
			Vector3 direction = AimbotCoroutines.GetAimPosition(transform, "Skull") - Player.player.look.aim.position;
			RaycastHit raycastHit;
			return PhysicsUtility.raycast(new Ray(Player.player.look.aim.position, direction), out raycastHit, direction.magnitude, RayMasks.DAMAGE_CLIENT, QueryTriggerInteraction.UseGlobal) && raycastHit.transform.IsChildOf(transform);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000068DC File Offset: 0x00004ADC
		public static RaycastInfo GenerateOriginalRaycast(Ray ray, float range, int mask)
		{
			RaycastHit hit;
			PhysicsUtility.raycast(ray, out hit, range, mask, QueryTriggerInteraction.UseGlobal);
			RaycastInfo raycastInfo = new RaycastInfo(hit);
			raycastInfo.direction = ray.direction;
			bool flag = !(raycastInfo.transform == null);
			RaycastInfo result;
			if (flag)
			{
				bool flag2 = raycastInfo.transform.CompareTag("Barricade");
				if (flag2)
				{
					raycastInfo.transform = DamageTool.getBarricadeRootTransform(raycastInfo.transform);
				}
				else
				{
					bool flag3 = raycastInfo.transform.CompareTag("Structure");
					if (flag3)
					{
						raycastInfo.transform = DamageTool.getStructureRootTransform(raycastInfo.transform);
					}
				}
				bool flag4 = raycastInfo.transform.CompareTag("Enemy");
				if (flag4)
				{
					raycastInfo.player = DamageTool.getPlayer(raycastInfo.transform);
				}
				bool flag5 = raycastInfo.transform.CompareTag("Zombie");
				if (flag5)
				{
					raycastInfo.zombie = DamageTool.getZombie(raycastInfo.transform);
				}
				bool flag6 = raycastInfo.transform.CompareTag("Animal");
				if (flag6)
				{
					raycastInfo.animal = DamageTool.getAnimal(raycastInfo.transform);
				}
				raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				bool flag7 = !RaycastOptions.UseRandomLimb;
				if (flag7)
				{
					bool useCustomLimb = RaycastOptions.UseCustomLimb;
					if (useCustomLimb)
					{
						raycastInfo.limb = RaycastOptions.TargetLimb;
					}
				}
				else
				{
					ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
					raycastInfo.limb = array[MathUtilities.Random.Next(0, array.Length)];
				}
				bool flag8 = raycastInfo.transform.CompareTag("Vehicle");
				if (flag8)
				{
					raycastInfo.vehicle = DamageTool.getVehicle(raycastInfo.transform);
				}
				else
				{
					bool flag9 = raycastInfo.zombie != null && raycastInfo.zombie.isRadioactive;
					if (flag9)
					{
						raycastInfo.material = EPhysicsMaterial.ALIEN_DYNAMIC;
					}
					else
					{
						raycastInfo.material = DamageTool.getMaterial(hit.point, raycastInfo.transform, raycastInfo.collider);
					}
				}
				bool alwaysHitHead = RaycastOptions.AlwaysHitHead;
				if (alwaysHitHead)
				{
					raycastInfo.limb = ELimb.SKULL;
				}
				result = raycastInfo;
			}
			else
			{
				result = raycastInfo;
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public static bool GenerateRaycast(out RaycastInfo info)
		{
			ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
			float num = (itemGunAsset != null) ? itemGunAsset.range : 15.5f;
			info = RaycastUtilities.GenerateOriginalRaycast(new Ray(OptimizationVariables.MainPlayer.look.aim.position, OptimizationVariables.MainPlayer.look.aim.forward), num, RayMasks.DAMAGE_CLIENT);
			bool flag = RaycastOptions.EnablePlayerSelection && RaycastUtilities.TargetedPlayer != null;
			if (flag)
			{
				GameObject gameObject = RaycastUtilities.TargetedPlayer.gameObject;
				bool flag2 = true;
				Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
				bool flag3 = Vector3.Distance(position, gameObject.transform.position) > num;
				if (flag3)
				{
					flag2 = false;
				}
				Vector3 point;
				bool flag4 = !SphereUtilities.GetRaycast(gameObject, position, out point);
				if (flag4)
				{
					flag2 = false;
				}
				bool flag5 = flag2;
				if (flag5)
				{
					info = RaycastUtilities.GenerateRaycast(gameObject, point, info.collider);
					return true;
				}
				bool onlyShootAtSelectedPlayer = RaycastOptions.OnlyShootAtSelectedPlayer;
				if (onlyShootAtSelectedPlayer)
				{
					return false;
				}
			}
			GameObject @object;
			Vector3 point2;
			bool targetObject = RaycastUtilities.GetTargetObject(RaycastUtilities.Objects, out @object, out point2, (double)num);
			bool result;
			if (targetObject)
			{
				info = RaycastUtilities.GenerateRaycast(@object, point2, info.collider);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006C48 File Offset: 0x00004E48
		public static RaycastInfo GenerateRaycast(GameObject Object, Vector3 Point, Collider col)
		{
			ELimb limb = RaycastOptions.TargetLimb;
			bool useRandomLimb = RaycastOptions.UseRandomLimb;
			if (useRandomLimb)
			{
				ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
				limb = array[MathUtilities.Random.Next(0, array.Length)];
			}
			EPhysicsMaterial material = (col == null) ? EPhysicsMaterial.NONE : DamageTool.getMaterial(Point, Object.transform, col);
			return new RaycastInfo(Object.transform)
			{
				point = Point,
				direction = OptimizationVariables.MainPlayer.look.aim.forward,
				limb = limb,
				material = material,
				player = Object.GetComponent<Player>(),
				zombie = Object.GetComponent<Zombie>(),
				vehicle = Object.GetComponent<InteractableVehicle>()
			};
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006D0C File Offset: 0x00004F0C
		public static bool GetTargetObject(GameObject[] Objects, out GameObject Object, out Vector3 Point, double Range)
		{
			double num = Range + 1.0;
			double num2 = 180.0;
			Object = null;
			Point = Vector3.zero;
			Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
			Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
			foreach (GameObject gameObject in Objects)
			{
				bool flag = !(gameObject == null);
				if (flag)
				{
					Vector3 position2 = gameObject.transform.position;
					Player component = gameObject.GetComponent<Player>();
					bool flag2 = !component || (!component.life.isDead && !FriendUtilities.IsFriendly(component) && (!RaycastOptions.NoShootthroughthewalls || RaycastUtilities.NoShootthroughthewalls(gameObject.transform)));
					if (flag2)
					{
						Zombie component2 = gameObject.GetComponent<Zombie>();
						bool flag3 = !component2 || !component2.isDead;
						if (flag3)
						{
							bool flag4 = gameObject.GetComponent<RaycastComponent>() == null;
							if (flag4)
							{
								gameObject.AddComponent<RaycastComponent>();
							}
							else
							{
								double distance = VectorUtilities.GetDistance(position, position2);
								bool flag5 = distance <= Range;
								if (flag5)
								{
									bool silentAimUseFOV = RaycastOptions.SilentAimUseFOV;
									if (silentAimUseFOV)
									{
										double angleDelta = VectorUtilities.GetAngleDelta(position, forward, position2);
										bool flag6 = angleDelta > (double)RaycastOptions.SilentAimFOV || angleDelta > num2;
										if (flag6)
										{
											goto IL_1A6;
										}
										num2 = angleDelta;
									}
									else
									{
										bool flag7 = distance > num;
										if (flag7)
										{
											goto IL_1A6;
										}
									}
									Vector3 vector;
									bool raycast = SphereUtilities.GetRaycast(gameObject, position, out vector);
									if (raycast)
									{
										Object = gameObject;
										num = distance;
										Point = vector;
									}
								}
							}
						}
					}
				}
				IL_1A6:;
			}
			return Object != null;
		}

		// Token: 0x04000059 RID: 89
		public static GameObject[] Objects = new GameObject[0];

		// Token: 0x0400005A RID: 90
		public static List<GameObject> AttachedObjects = new List<GameObject>();

		// Token: 0x0400005B RID: 91
		public static Player TargetedPlayer;
	}
}

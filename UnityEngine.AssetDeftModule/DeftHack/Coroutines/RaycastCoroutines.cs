using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Misc.Enums;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Coroutines
{
	// Token: 0x0200008F RID: 143
	public class RaycastCoroutines
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public static IEnumerator UpdateObjects()
		{
			for (;;)
			{
				bool flag = !DrawUtilities.ShouldRun();
				if (flag)
				{
					RaycastUtilities.Objects = new GameObject[0];
					yield return new WaitForSeconds(1f);
				}
				else
				{
					try
					{
						ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
						float num = (itemGunAsset != null) ? itemGunAsset.range : 15.5f;
						num += 10f;
						GameObject[] array = (from c in Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, num)
						select c.gameObject).ToArray<GameObject>();
						switch (RaycastOptions.Target)
						{
						case TargetPriority.Players:
						{
							RaycastCoroutines.CachedPlayers.Clear();
							GameObject[] array2 = array;
							int num2;
							for (int i = 0; i < array2.Length; i = num2 + 1)
							{
								Player player = DamageTool.getPlayer(array2[i].transform);
								bool flag2 = !(player == null) && !RaycastCoroutines.CachedPlayers.Contains(player) && !(player == OptimizationVariables.MainPlayer) && !player.life.isDead;
								if (flag2)
								{
									RaycastCoroutines.CachedPlayers.Add(player);
								}
								player = null;
								num2 = i;
							}
							RaycastUtilities.Objects = (from c in RaycastCoroutines.CachedPlayers
							select c.gameObject).ToArray<GameObject>();
							break;
						}
						case TargetPriority.Zombies:
							RaycastUtilities.Objects = (from g in array
							where g.GetComponent<Zombie>() != null
							select g).ToArray<GameObject>();
							break;
						case TargetPriority.Sentries:
							RaycastUtilities.Objects = (from g in array
							where g.GetComponent<InteractableSentry>() != null
							select g).ToArray<GameObject>();
							break;
						case TargetPriority.Beds:
							RaycastUtilities.Objects = (from g in array
							where g.GetComponent<InteractableBed>() != null
							select g).ToArray<GameObject>();
							break;
						case TargetPriority.ClaimFlags:
							RaycastUtilities.Objects = (from g in array
							where g.GetComponent<InteractableClaim>() != null
							select g).ToArray<GameObject>();
							break;
						case TargetPriority.Storage:
							RaycastUtilities.Objects = (from g in array
							where g.GetComponent<InteractableStorage>() != null
							select g).ToArray<GameObject>();
							break;
						case TargetPriority.Vehicles:
							RaycastUtilities.Objects = (from g in array
							where g.GetComponent<InteractableVehicle>() != null
							select g).ToArray<GameObject>();
							break;
						}
						itemGunAsset = null;
						array = null;
					}
					catch (Exception ex)
					{
						Exception exception = ex;
						DebugUtilities.LogException(exception);
					}
					yield return new WaitForSeconds(2f);
				}
			}
			yield break;
		}

		// Token: 0x040002F5 RID: 757
		public static List<Player> CachedPlayers = new List<Player>();
	}
}

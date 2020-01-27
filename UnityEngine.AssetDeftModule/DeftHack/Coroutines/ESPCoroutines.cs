using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Attributes;
using DeftHack.Misc;
using DeftHack.Misc.Classes.ESP;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Options.VisualOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Coroutines
{
	// Token: 0x02000083 RID: 131
	public static class ESPCoroutines
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00002B74 File Offset: 0x00000D74
		public static IEnumerator DoChams()
		{
			DebugUtilities.Log("Starting Chams Coroutine");
			for (;;)
			{
				bool flag = !DrawUtilities.ShouldRun() || ESPCoroutines.UnlitChams == null;
				bool flag2 = flag;
				if (flag2)
				{
					yield return new WaitForSeconds(1f);
				}
				else
				{
					try
					{
						bool chamsEnabled = ESPOptions.ChamsEnabled;
						bool flag3 = chamsEnabled;
						if (flag3)
						{
							ESPCoroutines.EnableChams();
						}
						else
						{
							ESPCoroutines.DisableChams();
						}
					}
					catch (Exception ex2)
					{
						Exception ex = ex2;
						Exception e = ex;
						Debug.LogException(e);
						e = null;
					}
					yield return new WaitForSeconds(5f);
				}
			}
			yield break;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000D774 File Offset: 0x0000B974
		public static void DoChamsGameObject(GameObject pgo, Color32 front, Color32 behind)
		{
			bool flag = ESPCoroutines.UnlitChams == null;
			bool flag2 = !flag;
			if (flag2)
			{
				Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					bool flag3 = !(componentsInChildren[i].material.shader != ESPCoroutines.LitChams | ESPCoroutines.UnlitChams);
					bool flag4 = !flag3;
					if (flag4)
					{
						Material[] materials = componentsInChildren[i].materials;
						for (int j = 0; j < materials.Length; j++)
						{
							materials[j].shader = (ESPOptions.ChamsFlat ? ESPCoroutines.UnlitChams : ESPCoroutines.LitChams);
							materials[j].SetColor("_ColorVisible", new Color32(front.r, front.g, front.b, front.a));
							materials[j].SetColor("_ColorBehind", new Color32(behind.r, behind.g, behind.b, behind.a));
						}
					}
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		[OffSpy]
		public static void EnableChams()
		{
			bool flag = !ESPOptions.ChamsEnabled;
			bool flag2 = !flag;
			if (flag2)
			{
				Color32 color = ColorUtilities.getColor("_ChamsFriendVisible");
				Color32 color2 = ColorUtilities.getColor("_ChamsFriendInvisible");
				Color32 color3 = ColorUtilities.getColor("_ChamsEnemyVisible");
				Color32 color4 = ColorUtilities.getColor("_ChamsEnemyInvisible");
				foreach (SteamPlayer steamPlayer in Provider.clients.ToArray())
				{
					Color32 front = FriendUtilities.IsFriendly(steamPlayer.player) ? color : color3;
					Color32 behind = FriendUtilities.IsFriendly(steamPlayer.player) ? color2 : color4;
					Player player = steamPlayer.player;
					bool flag3 = player == null || player == OptimizationVariables.MainPlayer || player.gameObject == null || player.life == null || player.life.isDead;
					bool flag4 = !flag3;
					if (flag4)
					{
						GameObject gameObject = player.gameObject;
						ESPCoroutines.DoChamsGameObject(gameObject, front, behind);
					}
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		[OnSpy]
		public static void DisableChams()
		{
			bool flag = ESPCoroutines.Normal == null;
			bool flag2 = !flag;
			if (flag2)
			{
				for (int i = 0; i < Provider.clients.ToArray().Length; i++)
				{
					Player player = Provider.clients.ToArray()[i].player;
					bool flag3 = player == null || player == OptimizationVariables.MainPlayer || player.life == null || player.life.isDead;
					bool flag4 = !flag3;
					if (flag4)
					{
						GameObject gameObject = player.gameObject;
						Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
						for (int j = 0; j < componentsInChildren.Length; j++)
						{
							Material[] materials = componentsInChildren[j].materials;
							for (int k = 0; k < materials.Length; k++)
							{
								bool flag5 = materials[k].shader != ESPCoroutines.Normal;
								bool flag6 = flag5;
								if (flag6)
								{
									materials[k].shader = ESPCoroutines.Normal;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00002B7C File Offset: 0x00000D7C
		public static IEnumerator UpdateObjectList()
		{
			for (;;)
			{
				bool flag = !DrawUtilities.ShouldRun();
				bool flag5 = flag;
				if (flag5)
				{
					yield return new WaitForSeconds(2f);
				}
				else
				{
					List<ESPObject> objects = ESPVariables.Objects;
					objects.Clear();
					List<ESPTarget> targets = (from k in ESPOptions.PriorityTable.Keys
					orderby ESPOptions.PriorityTable[k] descending
					select k).ToList<ESPTarget>();
					int num;
					for (int i = 0; i < targets.Count; i = num + 1)
					{
						ESPTarget target = targets[i];
						ESPVisual vis = ESPOptions.VisualOptions[(int)target];
						bool flag2 = !vis.Enabled;
						bool flag6 = !flag2;
						if (flag6)
						{
							Vector3 pPos = OptimizationVariables.MainPlayer.transform.position;
							switch (target)
							{
							case ESPTarget.Игроки:
							{
								SteamPlayer[] objarray = (from p in Provider.clients
								orderby VectorUtilities.GetDistance(pPos, p.player.transform.position) descending
								select p).ToArray<SteamPlayer>();
								bool useObjectCap = vis.UseObjectCap;
								bool flag7 = useObjectCap;
								if (flag7)
								{
									objarray = objarray.TakeLast(vis.ObjectCap).ToArray<SteamPlayer>();
								}
								for (int j = 0; j < objarray.Length; j = num + 1)
								{
									SteamPlayer sPlayer = objarray[j];
									Player plr = sPlayer.player;
									bool flag3 = plr.life.isDead || plr == OptimizationVariables.MainPlayer;
									bool flag8 = !flag3;
									if (flag8)
									{
										objects.Add(new ESPObject(target, plr, plr.gameObject));
										sPlayer = null;
										plr = null;
									}
									num = j;
									sPlayer = null;
									plr = null;
								}
								break;
							}
							case ESPTarget.Зомби:
							{
								Zombie[] objarr = (from obj in ZombieManager.regions.SelectMany((ZombieRegion r) => r.zombies)
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<Zombie>();
								bool useObjectCap2 = vis.UseObjectCap;
								bool flag9 = useObjectCap2;
								if (flag9)
								{
									objarr = objarr.TakeLast(vis.ObjectCap).ToArray<Zombie>();
								}
								for (int k2 = 0; k2 < objarr.Length; k2 = num + 1)
								{
									Zombie obj9 = objarr[k2];
									objects.Add(new ESPObject(target, obj9, obj9.gameObject));
									obj9 = null;
									num = k2;
									obj9 = null;
								}
								break;
							}
							case ESPTarget.Предметы:
							{
								InteractableItem[] objarr2 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableItem>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableItem>();
								bool useObjectCap3 = vis.UseObjectCap;
								bool flag10 = useObjectCap3;
								if (flag10)
								{
									objarr2 = objarr2.TakeLast(vis.ObjectCap).ToArray<InteractableItem>();
								}
								for (int l = 0; l < objarr2.Length; l = num + 1)
								{
									InteractableItem obj10 = objarr2[l];
									bool flag4 = ItemUtilities.Whitelisted(obj10.asset, ItemOptions.ItemESPOptions) || !ESPOptions.FilterItems;
									bool flag11 = flag4;
									if (flag11)
									{
										objects.Add(new ESPObject(target, obj10, obj10.gameObject));
									}
									obj10 = null;
									num = l;
									obj10 = null;
								}
								break;
							}
							case ESPTarget.Турели:
							{
								InteractableSentry[] objarr3 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSentry>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableSentry>();
								bool useObjectCap4 = vis.UseObjectCap;
								bool flag12 = useObjectCap4;
								if (flag12)
								{
									objarr3 = objarr3.TakeLast(vis.ObjectCap).ToArray<InteractableSentry>();
								}
								for (int m = 0; m < objarr3.Length; m = num + 1)
								{
									InteractableSentry obj11 = objarr3[m];
									objects.Add(new ESPObject(target, obj11, obj11.gameObject));
									obj11 = null;
									num = m;
									obj11 = null;
								}
								break;
							}
							case ESPTarget.Кровати:
							{
								InteractableBed[] objarr4 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableBed>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableBed>();
								bool useObjectCap5 = vis.UseObjectCap;
								bool flag13 = useObjectCap5;
								if (flag13)
								{
									objarr4 = objarr4.TakeLast(vis.ObjectCap).ToArray<InteractableBed>();
								}
								for (int n = 0; n < objarr4.Length; n = num + 1)
								{
									InteractableBed obj12 = objarr4[n];
									objects.Add(new ESPObject(target, obj12, obj12.gameObject));
									obj12 = null;
									num = n;
									obj12 = null;
								}
								break;
							}
							case ESPTarget.КлеймФлаги:
							{
								InteractableClaim[] objarr5 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableClaim>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableClaim>();
								bool useObjectCap6 = vis.UseObjectCap;
								bool flag14 = useObjectCap6;
								if (flag14)
								{
									objarr5 = objarr5.TakeLast(vis.ObjectCap).ToArray<InteractableClaim>();
								}
								for (int j2 = 0; j2 < objarr5.Length; j2 = num + 1)
								{
									InteractableClaim obj13 = objarr5[j2];
									objects.Add(new ESPObject(target, obj13, obj13.gameObject));
									obj13 = null;
									num = j2;
									obj13 = null;
								}
								break;
							}
							case ESPTarget.Транспорт:
							{
								InteractableVehicle[] objarr6 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableVehicle>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableVehicle>();
								bool useObjectCap7 = vis.UseObjectCap;
								bool flag15 = useObjectCap7;
								if (flag15)
								{
									objarr6 = objarr6.TakeLast(vis.ObjectCap).ToArray<InteractableVehicle>();
								}
								for (int j3 = 0; j3 < objarr6.Length; j3 = num + 1)
								{
									InteractableVehicle obj14 = objarr6[j3];
									bool isDead = obj14.isDead;
									bool flag16 = !isDead;
									if (flag16)
									{
										objects.Add(new ESPObject(target, obj14, obj14.gameObject));
										obj14 = null;
									}
									num = j3;
									obj14 = null;
								}
								break;
							}
							case ESPTarget.Ящики:
							{
								InteractableStorage[] objarr7 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableStorage>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableStorage>();
								bool useObjectCap8 = vis.UseObjectCap;
								bool flag17 = useObjectCap8;
								if (flag17)
								{
									objarr7 = objarr7.TakeLast(vis.ObjectCap).ToArray<InteractableStorage>();
								}
								for (int j4 = 0; j4 < objarr7.Length; j4 = num + 1)
								{
									InteractableStorage obj15 = objarr7[j4];
									objects.Add(new ESPObject(target, obj15, obj15.gameObject));
									obj15 = null;
									num = j4;
									obj15 = null;
								}
								break;
							}
							case ESPTarget.Генераторы:
							{
								InteractableGenerator[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableGenerator>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableGenerator>();
								bool useObjectCap9 = vis.UseObjectCap;
								bool flag18 = useObjectCap9;
								if (flag18)
								{
									objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableGenerator>();
								}
								for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
								{
									InteractableGenerator obj16 = objarr8[j5];
									objects.Add(new ESPObject(target, obj16, obj16.gameObject));
									obj16 = null;
									num = j5;
									obj16 = null;
								}
								break;
							}
							case ESPTarget.Животные:
							{
								Animal[] objarr9 = (from obj in UnityEngine.Object.FindObjectsOfType<Animal>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<Animal>();
								bool useObjectCap10 = vis.UseObjectCap;
								bool flag19 = useObjectCap10;
								if (flag19)
								{
									objarr9 = objarr9.TakeLast(vis.ObjectCap).ToArray<Animal>();
								}
								for (int j6 = 0; j6 < objarr9.Length; j6 = num + 1)
								{
									Animal obj17 = objarr9[j6];
									objects.Add(new ESPObject(target, obj17, obj17.gameObject));
									obj17 = null;
									num = j6;
									obj17 = null;
								}
								break;
							}
							case ESPTarget.Ловшуки:
							{
								InteractableTrap[] objarr10 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableTrap>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableTrap>();
								bool useObjectCap11 = vis.UseObjectCap;
								bool flag20 = useObjectCap11;
								if (flag20)
								{
									objarr10 = objarr10.TakeLast(vis.ObjectCap).ToArray<InteractableTrap>();
								}
								for (int j7 = 0; j7 < objarr10.Length; j7 = num + 1)
								{
									InteractableTrap obj18 = objarr10[j7];
									objects.Add(new ESPObject(target, obj18, obj18.gameObject));
									obj18 = null;
									num = j7;
									obj18 = null;
								}
								break;
							}
							case ESPTarget.Аирдропы:
							{
								Carepackage[] objarr11 = (from obj in UnityEngine.Object.FindObjectsOfType<Carepackage>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<Carepackage>();
								bool useObjectCap12 = vis.UseObjectCap;
								bool flag21 = useObjectCap12;
								if (flag21)
								{
									objarr11 = objarr11.TakeLast(vis.ObjectCap).ToArray<Carepackage>();
								}
								for (int j8 = 0; j8 < objarr11.Length; j8 = num + 1)
								{
									Carepackage obj19 = objarr11[j8];
									objects.Add(new ESPObject(target, obj19, obj19.gameObject));
									obj19 = null;
									num = j8;
									obj19 = null;
								}
								break;
							}
							case ESPTarget.Двери:
							{
								InteractableDoorHinge[] objarr12 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableDoorHinge>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableDoorHinge>();
								bool useObjectCap13 = vis.UseObjectCap;
								bool flag22 = useObjectCap13;
								if (flag22)
								{
									objarr12 = objarr12.TakeLast(vis.ObjectCap).ToArray<InteractableDoorHinge>();
								}
								for (int j9 = 0; j9 < objarr12.Length; j9 = num + 1)
								{
									InteractableDoorHinge obj20 = objarr12[j9];
									objects.Add(new ESPObject(target, obj20, obj20.gameObject));
									obj20 = null;
									num = j9;
									obj20 = null;
								}
								break;
							}
							case ESPTarget.Ягоды:
							{
								InteractableForage[] objarr13 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableForage>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableForage>();
								bool useObjectCap14 = vis.UseObjectCap;
								bool flag23 = useObjectCap14;
								if (flag23)
								{
									objarr13 = objarr13.TakeLast(vis.ObjectCap).ToArray<InteractableForage>();
								}
								for (int j10 = 0; j10 < objarr13.Length; j10 = num + 1)
								{
									InteractableForage obj21 = objarr13[j10];
									objects.Add(new ESPObject(target, obj21, obj21.gameObject));
									obj21 = null;
									num = j10;
									obj21 = null;
								}
								break;
							}
							case ESPTarget.Растения:
							{
								InteractableFarm[] objarr14 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableFarm>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableFarm>();
								bool useObjectCap15 = vis.UseObjectCap;
								bool flag24 = useObjectCap15;
								if (flag24)
								{
									objarr14 = objarr14.TakeLast(vis.ObjectCap).ToArray<InteractableFarm>();
								}
								for (int j11 = 0; j11 < objarr14.Length; j11 = num + 1)
								{
									InteractableFarm obj22 = objarr14[j11];
									objects.Add(new ESPObject(target, obj22, obj22.gameObject));
									obj22 = null;
									num = j11;
									obj22 = null;
								}
								break;
							}
							case ESPTarget.C4:
							{
								InteractableCharge[] objarr15 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableCharge>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableCharge>();
								bool useObjectCap16 = vis.UseObjectCap;
								bool flag25 = useObjectCap16;
								if (flag25)
								{
									objarr15 = objarr15.TakeLast(vis.ObjectCap).ToArray<InteractableCharge>();
								}
								for (int j12 = 0; j12 < objarr15.Length; j12 = num + 1)
								{
									InteractableCharge obj23 = objarr15[j12];
									objects.Add(new ESPObject(target, obj23, obj23.gameObject));
									obj23 = null;
									num = j12;
									obj23 = null;
								}
								break;
							}
							case ESPTarget.Fire:
							{
								InteractableFire[] objarr16 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableFire>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableFire>();
								bool useObjectCap17 = vis.UseObjectCap;
								bool flag26 = useObjectCap17;
								if (flag26)
								{
									objarr16 = objarr16.TakeLast(vis.ObjectCap).ToArray<InteractableFire>();
								}
								for (int j13 = 0; j13 < objarr16.Length; j13 = num + 1)
								{
									InteractableFire obj24 = objarr16[j13];
									objects.Add(new ESPObject(target, obj24, obj24.gameObject));
									obj24 = null;
									num = j13;
									obj24 = null;
								}
								break;
							}
							case ESPTarget.Лампы:
							{
								InteractableSpot[] objarr17 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSpot>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableSpot>();
								bool useObjectCap18 = vis.UseObjectCap;
								bool flag27 = useObjectCap18;
								if (flag27)
								{
									objarr17 = objarr17.TakeLast(vis.ObjectCap).ToArray<InteractableSpot>();
								}
								for (int j14 = 0; j14 < objarr17.Length; j14 = num + 1)
								{
									InteractableSpot obj25 = objarr17[j14];
									objects.Add(new ESPObject(target, obj25, obj25.gameObject));
									obj25 = null;
									num = j14;
									obj25 = null;
								}
								break;
							}
							case ESPTarget.Топливо:
							{
								InteractableObjectResource[] objarr18 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableObjectResource>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableObjectResource>();
								bool useObjectCap19 = vis.UseObjectCap;
								bool flag28 = useObjectCap19;
								if (flag28)
								{
									objarr18 = objarr18.TakeLast(vis.ObjectCap).ToArray<InteractableObjectResource>();
								}
								for (int j15 = 0; j15 < objarr18.Length; j15 = num + 1)
								{
									InteractableObjectResource obj26 = objarr18[j15];
									objects.Add(new ESPObject(target, obj26, obj26.gameObject));
									obj26 = null;
									num = j15;
									obj26 = null;
								}
								break;
							}
							case ESPTarget.Генератор_безопасной_зоны:
							{
								InteractableSafezone[] objarr19 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSafezone>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableSafezone>();
								bool useObjectCap20 = vis.UseObjectCap;
								bool flag29 = useObjectCap20;
								if (flag29)
								{
									objarr19 = objarr19.TakeLast(vis.ObjectCap).ToArray<InteractableSafezone>();
								}
								for (int j16 = 0; j16 < objarr19.Length; j16 = num + 1)
								{
									InteractableSafezone obj27 = objarr19[j16];
									objects.Add(new ESPObject(target, obj27, obj27.gameObject));
									obj27 = null;
									num = j16;
									obj27 = null;
								}
								break;
							}
							case ESPTarget.Генератор_Воздуха:
							{
								InteractableOxygenator[] objarr20 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableOxygenator>()
								orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
								select obj).ToArray<InteractableOxygenator>();
								bool useObjectCap21 = vis.UseObjectCap;
								bool flag30 = useObjectCap21;
								if (flag30)
								{
									objarr20 = objarr20.TakeLast(vis.ObjectCap).ToArray<InteractableOxygenator>();
								}
								for (int j17 = 0; j17 < objarr20.Length; j17 = num + 1)
								{
									InteractableOxygenator obj28 = objarr20[j17];
									objects.Add(new ESPObject(target, obj28, obj28.gameObject));
									obj28 = null;
									num = j17;
									obj28 = null;
								}
								break;
							}
							}
						}
						num = i;
						vis = null;
					}
					yield return new WaitForSeconds(5f);
					objects = null;
					targets = null;
					objects = null;
					targets = null;
				}
			}
			yield break;
		}

		// Token: 0x0400021F RID: 543
		public static Shader LitChams;

		// Token: 0x04000220 RID: 544
		public static Shader UnlitChams;

		// Token: 0x04000221 RID: 545
		public static Shader Normal;
	}
}

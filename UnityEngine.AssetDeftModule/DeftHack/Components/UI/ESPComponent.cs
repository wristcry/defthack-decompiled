using System;
using System.Collections.Generic;
using System.Linq;
using Assembly_CSharp;
using DeftHack.Attributes;
using DeftHack.Components.Basic;
using DeftHack.Coroutines;
using DeftHack.Misc.Classes.ESP;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Options.AimOptions;
using DeftHack.Options.VisualOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using DeftHack.Variables.UIVariables;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI
{
	// Token: 0x02000092 RID: 146
	[SpyComponent]
	[Component]
	public class ESPComponent : MonoBehaviour
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0001094C File Offset: 0x0000EB4C
		[Initializer]
		public static void Initialize()
		{
			for (int i = 0; i < ESPOptions.VisualOptions.Length; i++)
			{
				ESPTarget esptarget = (ESPTarget)i;
				Color32 color = Color.red;
				Color32 color2 = Color.white;
				switch (esptarget)
				{
				case ESPTarget.Зомби:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Предметы:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Турели:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Кровати:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.КлеймФлаги:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Транспорт:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Ящики:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Генераторы:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Животные:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Ловшуки:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Аирдропы:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Двери:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Ягоды:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Растения:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.C4:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Fire:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Лампы:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Топливо:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Генератор_безопасной_зоны:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				case ESPTarget.Генератор_Воздуха:
					color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
					break;
				}
				ColorUtilities.addColor(new ColorVariable(string.Format("_{0}", esptarget), string.Format("ВХ - {0}", esptarget), color, false));
				ColorUtilities.addColor(new ColorVariable(string.Format("_{0}_Outline", esptarget), string.Format("ВХ - {0} (Контур)", esptarget), Color.black, false));
				ColorUtilities.addColor(new ColorVariable(string.Format("_{0}_Glow", esptarget), string.Format("ВХ - {0} (ОБВОДКА)", esptarget), Color.yellow, false));
			}
			ColorUtilities.addColor(new ColorVariable("_ESPFriendly", "Дружелюбные игроки", Color.green, false));
			ColorUtilities.addColor(new ColorVariable("_ChamsFriendVisible", "Чамсы - Видимый друг", Color.green, false));
			ColorUtilities.addColor(new ColorVariable("_ChamsFriendInisible", "Чамсы - Невидимый друг", Color.blue, false));
			ColorUtilities.addColor(new ColorVariable("_ChamsEnemyVisible", "Чамсы - Видимый враг", new Color32(byte.MaxValue, 165, 0, byte.MaxValue), false));
			ColorUtilities.addColor(new ColorVariable("_ChamsEnemyInvisible", "Чамсы - Невидимый враг", Color.red, false));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00002D4D File Offset: 0x00000F4D
		public void Start()
		{
			CoroutineComponent.ESPCoroutine = base.StartCoroutine(ESPCoroutines.UpdateObjectList());
			CoroutineComponent.ChamsCoroutine = base.StartCoroutine(ESPCoroutines.DoChams());
			ESPComponent.MainCamera = OptimizationVariables.MainCam;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00010D04 File Offset: 0x0000EF04
		public void Update()
		{
			bool noRain = MiscOptions.NoRain;
			bool flag = noRain;
			if (flag)
			{
				Main.NR();
			}
			bool noSnow = MiscOptions.NoSnow;
			bool flag2 = noSnow;
			if (flag2)
			{
				Main.NS();
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00010D38 File Offset: 0x0000EF38
		public void OnGUI()
		{
			bool flag = Event.current.type != EventType.Repaint || !ESPOptions.Enabled;
			bool flag2 = !flag;
			if (flag2)
			{
				bool flag3 = !DrawUtilities.ShouldRun();
				bool flag4 = !flag3;
				if (flag4)
				{
					GUI.depth = 1;
					bool flag5 = ESPComponent.MainCamera == null;
					bool flag6 = flag5;
					if (flag6)
					{
						ESPComponent.MainCamera = OptimizationVariables.MainCam;
					}
					Vector3 position = OptimizationVariables.MainPlayer.transform.position;
					Vector3 position2 = OptimizationVariables.MainPlayer.look.aim.position;
					Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
					for (int i = 0; i < ESPVariables.Objects.Count; i++)
					{
						ESPObject espobject = ESPVariables.Objects[i];
						ESPVisual espvisual = ESPOptions.VisualOptions[(int)espobject.Target];
						GameObject gobject = espobject.GObject;
						bool flag7 = !espvisual.Enabled;
						bool flag8 = flag7;
						if (flag8)
						{
							Highlighter component = gobject.GetComponent<Highlighter>();
							bool flag9 = component != null && component != TrajectoryComponent.Highlighted;
							bool flag10 = flag9;
							if (flag10)
							{
								component.ConstantOffImmediate();
							}
						}
						else
						{
							bool flag11 = espobject.Target == ESPTarget.Предметы && ESPOptions.FilterItems;
							bool flag12 = flag11;
							if (flag12)
							{
								bool flag13 = !ItemUtilities.Whitelisted(((InteractableItem)espobject.Object).asset, ItemOptions.ItemESPOptions);
								bool flag14 = flag13;
								if (flag14)
								{
									goto IL_1783;
								}
							}
							Color color = ColorUtilities.getColor(string.Format("_{0}", espobject.Target));
							LabelLocation location = espvisual.Location;
							bool flag15 = gobject == null;
							bool flag16 = !flag15;
							if (flag16)
							{
								Vector3 position3 = gobject.transform.position;
								double distance = VectorUtilities.GetDistance(position3, position);
								bool flag17 = distance < 0.5 || (distance > (double)espvisual.Distance && !espvisual.InfiniteDistance);
								bool flag18 = !flag17;
								if (flag18)
								{
									Vector3 vector = ESPComponent.MainCamera.WorldToScreenPoint(position3);
									bool flag19 = vector.z <= 0f;
									bool flag20 = !flag19;
									if (flag20)
									{
										Vector3 localScale = gobject.transform.localScale;
										ESPTarget target = espobject.Target;
										bool flag21 = target > ESPTarget.Зомби;
										Bounds bounds;
										if (flag21)
										{
											bool flag22 = target != ESPTarget.Транспорт;
											if (flag22)
											{
												bounds = gobject.GetComponent<Collider>().bounds;
											}
											else
											{
												bounds = gobject.transform.Find("Model_0").GetComponent<MeshRenderer>().bounds;
												Transform transform = gobject.transform.Find("Model_1");
												bool flag23 = transform != null;
												bool flag24 = flag23;
												if (flag24)
												{
													bounds.Encapsulate(transform.GetComponent<MeshRenderer>().bounds);
												}
											}
										}
										else
										{
											bounds = new Bounds(new Vector3(position3.x, position3.y + 1f, position3.z), new Vector3(localScale.x * 2f, localScale.y * 3f, localScale.z * 2f));
										}
										int textSize = DrawUtilities.GetTextSize(espvisual, distance);
										double num = Math.Round(distance);
										string text = string.Format("<size={0}>", textSize);
										string text2 = string.Format("<size={0}>", textSize);
										switch (espobject.Target)
										{
										case ESPTarget.Игроки:
										{
											Player player = (Player)espobject.Object;
											bool isDead = player.life.isDead;
											bool flag25 = isDead;
											if (flag25)
											{
												goto IL_1783;
											}
											bool showName = espvisual.ShowName;
											bool flag26 = showName;
											if (flag26)
											{
												text2 = text2 + ESPComponent.GetSteamPlayer(player).playerID.characterName + "\n";
											}
											bool flag27 = RaycastUtilities.TargetedPlayer == player && RaycastOptions.EnablePlayerSelection;
											bool flag28 = flag27;
											if (flag28)
											{
												text2 += "[Цель]\n";
											}
											bool showPlayerWeapon = ESPOptions.ShowPlayerWeapon;
											bool flag29 = showPlayerWeapon;
											if (flag29)
											{
												text2 = text2 + ((player.equipment.asset != null) ? player.equipment.asset.itemName : "Руки") + "\n";
											}
											bool showPlayerVehicle = ESPOptions.ShowPlayerVehicle;
											bool flag30 = showPlayerVehicle;
											if (flag30)
											{
												text2 += ((player.movement.getVehicle() != null) ? (player.movement.getVehicle().asset.name + "\n") : "Нет транспорта\n");
											}
											bounds.size /= 2f;
											bounds.size = new Vector3(bounds.size.x, bounds.size.y * 1.25f, bounds.size.z);
											bool flag31 = FriendUtilities.IsFriendly(player) && ESPOptions.UsePlayerGroup;
											bool flag32 = flag31;
											if (flag32)
											{
												color = ColorUtilities.getColor("_ESPFriendly");
											}
											break;
										}
										case ESPTarget.Зомби:
										{
											bool isDead2 = ((Zombie)espobject.Object).isDead;
											bool flag33 = isDead2;
											if (flag33)
											{
												goto IL_1783;
											}
											bool showName2 = espvisual.ShowName;
											bool flag34 = showName2;
											if (flag34)
											{
												text2 += "Зомби\n";
											}
											break;
										}
										case ESPTarget.Предметы:
										{
											InteractableItem interactableItem = (InteractableItem)espobject.Object;
											bool showName3 = espvisual.ShowName;
											bool flag35 = showName3;
											if (flag35)
											{
												text2 = text2 + interactableItem.asset.itemName + "\n";
											}
											break;
										}
										case ESPTarget.Турели:
										{
											InteractableSentry interactableSentry = (InteractableSentry)espobject.Object;
											bool showName4 = espvisual.ShowName;
											bool flag36 = showName4;
											if (flag36)
											{
												text2 += "Турели\n";
												text += "Турели\n";
											}
											bool showSentryItem = ESPOptions.ShowSentryItem;
											bool flag37 = showSentryItem;
											if (flag37)
											{
												text = text + ESPComponent.SentryName(interactableSentry.displayItem, false) + "\n";
												text2 = text2 + ESPComponent.SentryName(interactableSentry.displayItem, true) + "\n";
											}
											break;
										}
										case ESPTarget.Кровати:
										{
											InteractableBed bed = (InteractableBed)espobject.Object;
											bool showName5 = espvisual.ShowName;
											bool flag38 = showName5;
											if (flag38)
											{
												text2 += "Кровать\n";
												text += "Кровать\n";
											}
											bool showClaimed = ESPOptions.ShowClaimed;
											bool flag39 = showClaimed;
											if (flag39)
											{
												text2 = text2 + ESPComponent.GetOwned(bed, true) + "\n";
												text = text + ESPComponent.GetOwned(bed, false) + "\n";
											}
											break;
										}
										case ESPTarget.КлеймФлаги:
										{
											bool showName6 = espvisual.ShowName;
											bool flag40 = showName6;
											if (flag40)
											{
												text2 += "Клейм Флаг\n";
											}
											break;
										}
										case ESPTarget.Транспорт:
										{
											InteractableVehicle interactableVehicle = (InteractableVehicle)espobject.Object;
											bool flag41 = interactableVehicle.health == 0;
											bool flag42 = flag41;
											if (flag42)
											{
												goto IL_1783;
											}
											bool flag43 = ESPOptions.FilterVehicleLocked && interactableVehicle.isLocked;
											bool flag44 = flag43;
											if (flag44)
											{
												goto IL_1783;
											}
											ushort num2;
											ushort num3;
											interactableVehicle.getDisplayFuel(out num2, out num3);
											float num4 = Mathf.Round(100f * ((float)interactableVehicle.health / (float)interactableVehicle.asset.health));
											float num5 = Mathf.Round(100f * ((float)num2 / (float)num3));
											bool showName7 = espvisual.ShowName;
											bool flag45 = showName7;
											if (flag45)
											{
												text2 = text2 + interactableVehicle.asset.name + "\n";
												text = text + interactableVehicle.asset.name + "\n";
											}
											bool showVehicleHealth = ESPOptions.ShowVehicleHealth;
											bool flag46 = showVehicleHealth;
											if (flag46)
											{
												text2 += string.Format("Прочность: {0}%\n", num4);
												text += string.Format("Прочность: {0}%\n", num4);
											}
											bool showVehicleFuel = ESPOptions.ShowVehicleFuel;
											bool flag47 = showVehicleFuel;
											if (flag47)
											{
												text2 += string.Format("Топливо: {0}%\n", num5);
												text += string.Format("Топливо: {0}%\n", num5);
											}
											bool showVehicleLocked = ESPOptions.ShowVehicleLocked;
											bool flag48 = showVehicleLocked;
											if (flag48)
											{
												text2 = text2 + ESPComponent.GetLocked(interactableVehicle, true) + "\n";
												text = text + ESPComponent.GetLocked(interactableVehicle, false) + "\n";
											}
											break;
										}
										case ESPTarget.Ящики:
										{
											InteractableStorage interactableStorage = (InteractableStorage)espobject.Object;
											bool showName8 = espvisual.ShowName;
											if (showName8)
											{
												text2 += "Ящик\n";
												text2 = text2 + "ID: " + interactableStorage.name + "\n";
												bool flag49 = interactableStorage.name == "1374";
												if (flag49)
												{
													text2 += "Аирдроп\n";
												}
												else
												{
													bool flag50 = interactableStorage.name == "366";
													if (flag50)
													{
														text2 += "ИМЯ: Кленовый Ящик\n";
													}
													else
													{
														bool flag51 = interactableStorage.name == "1202";
														if (flag51)
														{
															text2 += "ИМЯ: Кленовая Стойка для оружия\n";
														}
														else
														{
															bool flag52 = interactableStorage.name == "1205";
															if (flag52)
															{
																text2 += "ИМЯ: Кленовый Стенд\n";
															}
															else
															{
																bool flag53 = interactableStorage.name == "1245";
																if (flag53)
																{
																	text2 += "ИМЯ: Кленовый кухонный шкаф\n";
																}
																else
																{
																	bool flag54 = interactableStorage.name == "1251";
																	if (flag54)
																	{
																		text2 += "ИМЯ: Кленовая Кухонная раковина\n";
																	}
																	else
																	{
																		bool flag55 = interactableStorage.name == "1278";
																		if (flag55)
																		{
																			text2 += "ИМЯ: Кленовый шкаф\n";
																		}
																		else
																		{
																			bool flag56 = interactableStorage.name == "1410";
																			if (flag56)
																			{
																				text2 += "ИМЯ: Кленовая подставка для трофеев\n";
																			}
																			else
																			{
																				bool flag57 = interactableStorage.name == "1410";
																				if (flag57)
																				{
																					text2 += "ИМЯ: Кленовая подставка для трофеев\n";
																				}
																				else
																				{
																					bool flag58 = interactableStorage.name == "367";
																					if (flag58)
																					{
																						text2 += "ИМЯ: Берёзовый ящик\n";
																					}
																					else
																					{
																						bool flag59 = interactableStorage.name == "1203";
																						if (flag59)
																						{
																							text2 += "ИМЯ: Берёзовая стойка для оружия\n";
																						}
																						else
																						{
																							bool flag60 = interactableStorage.name == "1206";
																							if (flag60)
																							{
																								text2 += "ИМЯ: Берёзовый стенд\n";
																							}
																							else
																							{
																								bool flag61 = interactableStorage.name == "1246";
																								if (flag61)
																								{
																									text2 += "ИМЯ: Берёзовый кухонный шкаф\n";
																								}
																								else
																								{
																									bool flag62 = interactableStorage.name == "1252";
																									if (flag62)
																									{
																										text2 += "ИМЯ: Берёзовая раковина\n";
																									}
																									else
																									{
																										bool flag63 = interactableStorage.name == "1411";
																										if (flag63)
																										{
																											text2 += "ИМЯ: Берёзовая подставка для трофеев\n";
																										}
																										else
																										{
																											bool flag64 = interactableStorage.name == "368";
																											if (flag64)
																											{
																												text2 += "ИМЯ: Сосновый ящик\n";
																											}
																											else
																											{
																												bool flag65 = interactableStorage.name == "1204";
																												if (flag65)
																												{
																													text2 += "ИМЯ: Сосновая стойка для оружия\n";
																												}
																												else
																												{
																													bool flag66 = interactableStorage.name == "1207";
																													if (flag66)
																													{
																														text2 += "ИМЯ: Сосновый стенд\n";
																													}
																													else
																													{
																														bool flag67 = interactableStorage.name == "1247";
																														if (flag67)
																														{
																															text2 += "ИМЯ: Сосновый кухонный шкаф\n";
																														}
																														else
																														{
																															bool flag68 = interactableStorage.name == "1253";
																															if (flag68)
																															{
																																text2 += "ИМЯ: Сосновая раковина\n";
																															}
																															else
																															{
																																bool flag69 = interactableStorage.name == "1280";
																																if (flag69)
																																{
																																	text2 += "ИМЯ: Сосновый шкаф\n";
																																}
																																else
																																{
																																	bool flag70 = interactableStorage.name == "1412";
																																	if (flag70)
																																	{
																																		text2 += "ИМЯ: Сосновая подставка для трофеев\n";
																																	}
																																	else
																																	{
																																		bool flag71 = interactableStorage.name == "328";
																																		if (flag71)
																																		{
																																			text2 += "ИМЯ: Сейф\n";
																																		}
																																		else
																																		{
																																			bool flag72 = interactableStorage.name == "1220";
																																			if (flag72)
																																			{
																																				text2 += "ИМЯ: Железная стойка для оружия\n";
																																			}
																																			else
																																			{
																																				bool flag73 = interactableStorage.name == "1221";
																																				if (flag73)
																																				{
																																					text2 += "ИМЯ: Железный стенд\n";
																																				}
																																				else
																																				{
																																					bool flag74 = interactableStorage.name == "1248";
																																					if (flag74)
																																					{
																																						text2 += "ИМЯ: Железный кухонный шкаф\n";
																																					}
																																					else
																																					{
																																						bool flag75 = interactableStorage.name == "1254";
																																						if (flag75)
																																						{
																																							text2 += "ИМЯ: Железная раковина\n";
																																						}
																																						else
																																						{
																																							bool flag76 = interactableStorage.name == "1281";
																																							if (flag76)
																																							{
																																								text2 += "ИМЯ: Железный шкаф\n";
																																							}
																																							else
																																							{
																																								bool flag77 = interactableStorage.name == "1413";
																																								if (flag77)
																																								{
																																									text2 += "ИМЯ: Железная подставка для трофеев\n";
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
											break;
										}
										case ESPTarget.Генераторы:
										{
											bool showName9 = espvisual.ShowName;
											if (showName9)
											{
												text2 += "Генератор\n";
											}
											InteractableGenerator interactableGenerator = (InteractableGenerator)espobject.Object;
											float num6 = Mathf.Round(100f * ((float)interactableGenerator.fuel / (float)interactableGenerator.capacity));
											bool showGeneratorFuel = ESPOptions.ShowGeneratorFuel;
											bool flag78 = showGeneratorFuel;
											if (flag78)
											{
												text2 += string.Format("Топливо: {0}%\n", num6);
												text += string.Format("Топливо: {0}%\n", num6);
											}
											bool showGeneratorPowered = ESPOptions.ShowGeneratorPowered;
											bool flag79 = showGeneratorPowered;
											if (flag79)
											{
												text2 = text2 + ESPComponent.GetPowered(interactableGenerator, true) + "\n";
												text = text + ESPComponent.GetPowered(interactableGenerator, false) + "\n";
											}
											break;
										}
										case ESPTarget.Животные:
										{
											Animal animal = (Animal)espobject.Object;
											bool showName10 = espvisual.ShowName;
											if (showName10)
											{
												text2 += "Животное\n";
												text2 = text2 + animal.asset.animalName + "\n";
											}
											break;
										}
										case ESPTarget.Ловшуки:
										{
											InteractableTrap interactableTrap = (InteractableTrap)espobject.Object;
											bool showName11 = espvisual.ShowName;
											if (showName11)
											{
												text2 += "Ловушка\n";
											}
											break;
										}
										case ESPTarget.Аирдропы:
										{
											Carepackage carepackage = (Carepackage)espobject.Object;
											bool showName12 = espvisual.ShowName;
											if (showName12)
											{
												text2 += "Аирдроп\n";
											}
											break;
										}
										case ESPTarget.Двери:
										{
											bool showName13 = espvisual.ShowName;
											if (showName13)
											{
												text2 += "Дверь\n";
											}
											break;
										}
										case ESPTarget.Ягоды:
										{
											bool showName14 = espvisual.ShowName;
											if (showName14)
											{
												text2 += "Ягода\n";
											}
											break;
										}
										case ESPTarget.Растения:
										{
											InteractableFarm interactableFarm = (InteractableFarm)espobject.Object;
											bool showName15 = espvisual.ShowName;
											if (showName15)
											{
												text2 += "Растение\n";
												bool flag80 = interactableFarm.name == "330";
												if (flag80)
												{
													text2 += "Саженец моркови\n";
												}
												else
												{
													bool flag81 = interactableFarm.name == "336";
													if (flag81)
													{
														text2 += "Саженец кукурузы\n";
													}
													else
													{
														bool flag82 = interactableFarm.name == "339";
														if (flag82)
														{
															text2 += "Саженец капусты\n";
														}
														else
														{
															bool flag83 = interactableFarm.name == "341";
															if (flag83)
															{
																text2 += "Саженец помидора\n";
															}
															else
															{
																bool flag84 = interactableFarm.name == "343";
																if (flag84)
																{
																	text2 += "Саженец картошки\n";
																}
																else
																{
																	bool flag85 = interactableFarm.name == "345";
																	if (flag85)
																	{
																		text2 += "Саженец пшеницы\n";
																	}
																	else
																	{
																		bool flag86 = interactableFarm.name == "1104";
																		if (flag86)
																		{
																			text2 += "Саженец янтаря\n";
																		}
																		else
																		{
																			bool flag87 = interactableFarm.name == "1105";
																			if (flag87)
																			{
																				text2 += "Саженец синих ягод\n";
																			}
																			else
																			{
																				bool flag88 = interactableFarm.name == "1106";
																				if (flag88)
																				{
																					text2 += "Саженец зелёных ягод\n";
																				}
																				else
																				{
																					bool flag89 = interactableFarm.name == "1107";
																					if (flag89)
																					{
																						text2 += "Саженец лиловых ягод\n";
																					}
																					else
																					{
																						bool flag90 = interactableFarm.name == "1108";
																						if (flag90)
																						{
																							text2 += "Саженец алых ягод\n";
																						}
																						else
																						{
																							bool flag91 = interactableFarm.name == "1109";
																							if (flag91)
																							{
																								text2 += "Саженец бирюзовых ягод\n";
																							}
																							else
																							{
																								bool flag92 = interactableFarm.name == "1110";
																								if (flag92)
																								{
																									text2 += "Саженец жёлтых ягод\n";
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
											break;
										}
										case ESPTarget.C4:
										{
											InteractableCharge interactableCharge = (InteractableCharge)espobject.Object;
											bool showName16 = espvisual.ShowName;
											if (showName16)
											{
												text2 += "C4\n";
												bool flag93 = interactableCharge.name == "1241";
												if (flag93)
												{
													text2 += "Breaching Charge\n";
												}
												else
												{
													bool flag94 = interactableCharge.name == "1393";
													if (flag94)
													{
														text2 += "Precision Charge\n";
													}
												}
											}
											break;
										}
										case ESPTarget.Fire:
										{
											bool showName17 = espvisual.ShowName;
											if (showName17)
											{
												text2 += "Источник огня\n";
											}
											break;
										}
										case ESPTarget.Лампы:
										{
											bool showName18 = espvisual.ShowName;
											if (showName18)
											{
												text2 += "Лампа\n";
											}
											break;
										}
										case ESPTarget.Топливо:
										{
											bool showName19 = espvisual.ShowName;
											if (showName19)
											{
												text2 += "Топливная бочка\n";
											}
											break;
										}
										case ESPTarget.Генератор_безопасной_зоны:
										{
											InteractableSafezone interactableSafezone = (InteractableSafezone)espobject.Object;
											bool showName20 = espvisual.ShowName;
											if (showName20)
											{
												text2 += "Генератор сейф зоны\n";
											}
											break;
										}
										case ESPTarget.Генератор_Воздуха:
										{
											bool showName21 = espvisual.ShowName;
											if (showName21)
											{
												text2 += "Генератор воздуха\n";
											}
											break;
										}
										}
										bool flag95 = text == string.Format("<size={0}>", textSize);
										bool flag96 = flag95;
										if (flag96)
										{
											text = null;
										}
										bool showDistance = espvisual.ShowDistance;
										bool flag97 = showDistance;
										if (flag97)
										{
											text2 += string.Format("{0}m\n", num);
											bool flag98 = text != null;
											bool flag99 = flag98;
											if (flag99)
											{
												text += string.Format("{0}m\n", num);
											}
										}
										bool showAngle = espvisual.ShowAngle;
										bool flag100 = showAngle;
										if (flag100)
										{
											double num7 = Math.Round(VectorUtilities.GetAngleDelta(position2, forward, position3), 2);
											text2 += string.Format("Угол: {0}°\n", num7);
											bool flag101 = text != null;
											bool flag102 = flag101;
											if (flag102)
											{
												text += string.Format("{0}°\n", num7);
											}
										}
										bool bones = MiscOptions.Bones;
										if (bones)
										{
											this.DrawBones();
										}
										text2 += "</size>";
										bool flag103 = text != null;
										bool flag104 = flag103;
										if (flag104)
										{
											text += "</size>";
										}
										Vector3[] boxVectors = DrawUtilities.GetBoxVectors(bounds);
										Vector2[] rectangleLines = DrawUtilities.GetRectangleLines(ESPComponent.MainCamera, bounds, color);
										Vector3 v2 = DrawUtilities.Get2DW2SVector(ESPComponent.MainCamera, rectangleLines, location);
										bool enabled = MirrorCameraOptions.Enabled;
										bool flag105;
										if (enabled)
										{
											flag105 = rectangleLines.Any((Vector2 v) => MirrorCameraComponent.viewport.Contains(v));
										}
										else
										{
											flag105 = false;
										}
										bool flag106 = flag105;
										bool flag107 = flag106;
										if (flag107)
										{
											Highlighter component2 = gobject.GetComponent<Highlighter>();
											bool flag108 = component2 != null;
											bool flag109 = flag108;
											if (flag109)
											{
												component2.ConstantOffImmediate();
											}
										}
										else
										{
											bool boxes = espvisual.Boxes;
											bool flag110 = boxes;
											if (flag110)
											{
												bool twoDimensional = espvisual.TwoDimensional;
												bool flag111 = twoDimensional;
												if (flag111)
												{
													DrawUtilities.PrepareRectangleLines(rectangleLines, color);
												}
												else
												{
													DrawUtilities.PrepareBoxLines(boxVectors, color);
													v2 = DrawUtilities.Get3DW2SVector(ESPComponent.MainCamera, bounds, location);
												}
											}
											bool glow = espvisual.Glow;
											bool flag112 = glow;
											if (flag112)
											{
												Highlighter highlighter = gobject.GetComponent<Highlighter>() ?? gobject.AddComponent<Highlighter>();
												highlighter.occluder = true;
												highlighter.overlay = true;
												highlighter.ConstantOnImmediate(ColorUtilities.getColor(string.Format("_{0}_Glow", espobject.Target)));
												ESPComponent.Highlighters.Add(highlighter);
											}
											else
											{
												Highlighter component3 = gobject.GetComponent<Highlighter>();
												bool flag113 = component3 != null && component3 != TrajectoryComponent.Highlighted;
												bool flag114 = flag113;
												if (flag114)
												{
													component3.ConstantOffImmediate();
												}
											}
											bool labels = espvisual.Labels;
											bool flag115 = labels;
											if (flag115)
											{
												DrawUtilities.DrawLabel(ESPComponent.ESPFont, location, v2, text2, espvisual.CustomTextColor ? ColorUtilities.getColor(string.Format("_{0}_Text", espobject.Target)) : color, ColorUtilities.getColor(string.Format("_{0}_Outline", espobject.Target)), espvisual.BorderStrength, text, 12);
											}
											bool lineToObject = espvisual.LineToObject;
											bool flag116 = lineToObject;
											if (flag116)
											{
												ESPVariables.DrawBuffer2.Enqueue(new ESPBox2
												{
													Color = color,
													Vertices = new Vector2[]
													{
														new Vector2((float)(Screen.width / 2), (float)Screen.height),
														new Vector2(vector.x, (float)Screen.height - vector.y)
													}
												});
											}
										}
									}
								}
							}
						}
						IL_1783:;
					}
					ESPComponent.GLMat.SetPass(0);
					GL.PushMatrix();
					GL.LoadProjectionMatrix(ESPComponent.MainCamera.projectionMatrix);
					GL.modelview = ESPComponent.MainCamera.worldToCameraMatrix;
					GL.Begin(1);
					for (int j = 0; j < ESPVariables.DrawBuffer.Count; j++)
					{
						ESPBox espbox = ESPVariables.DrawBuffer.Dequeue();
						GL.Color(espbox.Color);
						Vector3[] vertices = espbox.Vertices;
						for (int k = 0; k < vertices.Length; k++)
						{
							GL.Vertex(vertices[k]);
						}
					}
					GL.End();
					GL.PopMatrix();
					GL.PushMatrix();
					GL.Begin(1);
					for (int l = 0; l < ESPVariables.DrawBuffer2.Count; l++)
					{
						ESPBox2 espbox2 = ESPVariables.DrawBuffer2.Dequeue();
						GL.Color(espbox2.Color);
						Vector2[] vertices2 = espbox2.Vertices;
						bool flag117 = true;
						for (int m = 0; m < vertices2.Length; m++)
						{
							bool flag118 = m < vertices2.Length - 1;
							bool flag119 = flag118;
							if (flag119)
							{
								Vector2 b = vertices2[m];
								Vector2 a = vertices2[m + 1];
								bool flag120 = Vector2.Distance(a, b) > (float)(Screen.width / 2);
								bool flag121 = flag120;
								if (flag121)
								{
									flag117 = false;
									break;
								}
							}
						}
						bool flag122 = !flag117;
						bool flag123 = !flag122;
						if (flag123)
						{
							for (int n = 0; n < vertices2.Length; n++)
							{
								GL.Vertex3(vertices2[n].x, vertices2[n].y, 0f);
							}
						}
					}
					GL.End();
					GL.PopMatrix();
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000126C4 File Offset: 0x000108C4
		private void DrawBones()
		{
			Color red = Color.red;
			DrawUtilities.DrawLine(HumanBodyBones.Head, HumanBodyBones.Spine, red, 1.2f, true);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000126E8 File Offset: 0x000108E8
		[OnSpy]
		public static void DisableHighlighters()
		{
			foreach (Highlighter highlighter in ESPComponent.Highlighters)
			{
				highlighter.occluder = false;
				highlighter.overlay = false;
				highlighter.ConstantOffImmediate();
			}
			ESPComponent.Highlighters.Clear();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0001275C File Offset: 0x0001095C
		public static string SentryName(Item DisplayItem, bool color)
		{
			return (DisplayItem != null) ? Assets.find(EAssetType.ITEM, DisplayItem.id).name : (color ? "<color=#ff0000ff>Нет предмета</color>" : "Нет предмета");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00012794 File Offset: 0x00010994
		public static string GetLocked(InteractableVehicle Vehicle, bool color)
		{
			return Vehicle.isLocked ? (color ? "<color=#ff0000ff>Закрыто</color>" : "Закрыто") : (color ? "<color=#00ff00ff>Открыто</color>" : "Открыто");
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000127D0 File Offset: 0x000109D0
		public static string GetPowered(InteractableGenerator Generator, bool color)
		{
			return Generator.isPowered ? (color ? "<color=#00ff00ff>Работает</color>" : "Работает") : (color ? "<color=#ff0000ff>Не работает</color>" : "Не работает");
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0001280C File Offset: 0x00010A0C
		public static string GetOwned(InteractableBed bed, bool color)
		{
			return bed.isClaimed ? (color ? "<color=$00ff00ff>Занята</color>" : "Занята") : (color ? "<color=#ff0000ff>Свободна</color>" : "Свободна");
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00012848 File Offset: 0x00010A48
		public static SteamPlayer GetSteamPlayer(Player player)
		{
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				bool flag = steamPlayer.player == player;
				bool flag2 = flag;
				if (flag2)
				{
					return steamPlayer;
				}
			}
			return null;
		}

		// Token: 0x04000308 RID: 776
		public static Material GLMat;

		// Token: 0x04000309 RID: 777
		public static Font ESPFont;

		// Token: 0x0400030A RID: 778
		public static List<Highlighter> Highlighters = new List<Highlighter>();

		// Token: 0x0400030B RID: 779
		public static Camera MainCamera;
	}
}

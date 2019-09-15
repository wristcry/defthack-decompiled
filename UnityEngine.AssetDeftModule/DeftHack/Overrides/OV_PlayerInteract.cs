using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using HighlightingSystem;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x02000047 RID: 71
	public class OV_PlayerInteract
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000A0D8 File Offset: 0x000082D8
		[Initializer]
		public static void Init()
		{
			OV_PlayerInteract.FocusField = typeof(PlayerInteract).GetField("focus", ReflectionVariables.PrivateStatic);
			OV_PlayerInteract.TargetField = typeof(PlayerInteract).GetField("target", ReflectionVariables.PrivateStatic);
			OV_PlayerInteract.InteractableField = typeof(PlayerInteract).GetField("_interactable", ReflectionVariables.PrivateStatic);
			OV_PlayerInteract.Interactable2Field = typeof(PlayerInteract).GetField("_interactable2", ReflectionVariables.PrivateStatic);
			OV_PlayerInteract.PurchaseAssetField = typeof(PlayerInteract).GetField("purchaseAsset", ReflectionVariables.PrivateStatic);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000A17C File Offset: 0x0000837C
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002714 File Offset: 0x00000914
		public static Transform focus
		{
			get
			{
				return (Transform)OV_PlayerInteract.FocusField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.FocusField.SetValue(null, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000A1A0 File Offset: 0x000083A0
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00002724 File Offset: 0x00000924
		public static Transform target
		{
			get
			{
				return (Transform)OV_PlayerInteract.TargetField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.TargetField.SetValue(null, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000A1C4 File Offset: 0x000083C4
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00002734 File Offset: 0x00000934
		public static Interactable interactable
		{
			get
			{
				return (Interactable)OV_PlayerInteract.InteractableField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.InteractableField.SetValue(null, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000A1E8 File Offset: 0x000083E8
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00002744 File Offset: 0x00000944
		public static Interactable2 interactable2
		{
			get
			{
				return (Interactable2)OV_PlayerInteract.Interactable2Field.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.Interactable2Field.SetValue(null, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000A20C File Offset: 0x0000840C
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00002754 File Offset: 0x00000954
		public static ItemAsset purchaseAsset
		{
			get
			{
				return (ItemAsset)OV_PlayerInteract.PurchaseAssetField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.PurchaseAssetField.SetValue(null, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000A230 File Offset: 0x00008430
		private float salvageTime
		{
			get
			{
				bool customSalvageTime = MiscOptions.CustomSalvageTime;
				float result;
				if (customSalvageTime)
				{
					result = MiscOptions.SalvageTime;
				}
				else
				{
					bool flag = !OptimizationVariables.MainPlayer.channel.owner.isAdmin;
					if (flag)
					{
						result = 8f;
					}
					else
					{
						result = 1f;
					}
				}
				return result;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002764 File Offset: 0x00000964
		private void hotkey(byte button)
		{
			VehicleManager.swapVehicle(button);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000A280 File Offset: 0x00008480
		private void onPurchaseUpdated(PurchaseNode node)
		{
			bool flag = node == null;
			if (flag)
			{
				OV_PlayerInteract.purchaseAsset = null;
			}
			else
			{
				OV_PlayerInteract.purchaseAsset = (ItemAsset)Assets.find(EAssetType.ITEM, node.id);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000A2BC File Offset: 0x000084BC
		public static void highlight(Transform target, Color color)
		{
			bool flag = !target.CompareTag("Player") || target.CompareTag("Enemy") || target.CompareTag("Zombie") || target.CompareTag("Animal") || target.CompareTag("Agent");
			if (flag)
			{
				Highlighter highlighter = target.GetComponent<Highlighter>();
				bool flag2 = highlighter == null;
				if (flag2)
				{
					highlighter = target.gameObject.AddComponent<Highlighter>();
				}
				highlighter.ConstantOn(color, 0.25f);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000A340 File Offset: 0x00008540
		[OnSpy]
		public static void OnSpied()
		{
			Transform transform = OptimizationVariables.MainCam.transform;
			PhysicsUtility.raycast(new Ray(transform.position, transform.forward), out OV_PlayerInteract.hit, (float)((OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? 6 : 4), RayMasks.PLAYER_INTERACT, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000A394 File Offset: 0x00008594
		[Override(typeof(PlayerInteract), "Update", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public void OV_Update()
		{
			bool flag = !DrawUtilities.ShouldRun();
			if (!flag)
			{
				bool flag2 = OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.DRIVING && OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.SITTING && !OptimizationVariables.MainPlayer.life.isDead && !OptimizationVariables.MainPlayer.workzone.isBuilding;
				if (flag2)
				{
					bool flag3 = Time.realtimeSinceStartup - OV_PlayerInteract.lastInteract > 0.1f;
					if (flag3)
					{
						int num = 0;
						bool flag4 = InteractionOptions.InteractThroughWalls && !PlayerCoroutines.IsSpying;
						if (flag4)
						{
							bool flag5 = !InteractionOptions.NoHitBarricades;
							if (flag5)
							{
								num |= RayMasks.BARRICADE;
							}
							bool flag6 = !InteractionOptions.NoHitItems;
							if (flag6)
							{
								num |= RayMasks.ITEM;
							}
							bool flag7 = !InteractionOptions.NoHitResources;
							if (flag7)
							{
								num |= RayMasks.RESOURCE;
							}
							bool flag8 = !InteractionOptions.NoHitStructures;
							if (flag8)
							{
								num |= RayMasks.STRUCTURE;
							}
							bool flag9 = !InteractionOptions.NoHitVehicles;
							if (flag9)
							{
								num |= RayMasks.VEHICLE;
							}
							bool flag10 = !InteractionOptions.NoHitEnvironment;
							if (flag10)
							{
								num |= (RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND);
							}
						}
						else
						{
							num = RayMasks.PLAYER_INTERACT;
						}
						OV_PlayerInteract.lastInteract = Time.realtimeSinceStartup;
						float num2 = (InteractionOptions.InteractThroughWalls && !PlayerCoroutines.IsSpying) ? 20f : 4f;
						PhysicsUtility.raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out OV_PlayerInteract.hit, (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? (num2 + 2f) : num2, num, QueryTriggerInteraction.UseGlobal);
					}
					Transform transform = (!(OV_PlayerInteract.hit.collider != null)) ? null : OV_PlayerInteract.hit.collider.transform;
					bool flag11 = transform != OV_PlayerInteract.focus;
					if (flag11)
					{
						bool flag12 = OV_PlayerInteract.focus != null && PlayerInteract.interactable != null;
						if (flag12)
						{
							InteractableDoorHinge componentInParent = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
							bool flag13 = componentInParent != null;
							if (flag13)
							{
								HighlighterTool.unhighlight(componentInParent.door.transform);
							}
							else
							{
								HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
							}
						}
						OV_PlayerInteract.focus = null;
						OV_PlayerInteract.target = null;
						OV_PlayerInteract.interactable = null;
						OV_PlayerInteract.interactable2 = null;
						bool flag14 = transform != null;
						if (flag14)
						{
							OV_PlayerInteract.focus = transform;
							OV_PlayerInteract.interactable = OV_PlayerInteract.focus.GetComponentInParent<Interactable>();
							OV_PlayerInteract.interactable2 = OV_PlayerInteract.focus.GetComponentInParent<Interactable2>();
							bool flag15 = PlayerInteract.interactable != null;
							if (flag15)
							{
								OV_PlayerInteract.target = PlayerInteract.interactable.transform.FindChildRecursive("Target");
								bool flag16 = PlayerInteract.interactable.checkInteractable();
								if (flag16)
								{
									bool isEnabled = PlayerUI.window.isEnabled;
									if (isEnabled)
									{
										bool flag17 = PlayerInteract.interactable.checkUseable();
										if (flag17)
										{
											Color green;
											bool flag18 = !PlayerInteract.interactable.checkHighlight(out green);
											if (flag18)
											{
												green = Color.green;
											}
											InteractableDoorHinge componentInParent2 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
											bool flag19 = componentInParent2 != null;
											if (flag19)
											{
												HighlighterTool.highlight(componentInParent2.door.transform, green);
											}
											else
											{
												HighlighterTool.highlight(PlayerInteract.interactable.transform, green);
											}
										}
										else
										{
											Color red = Color.red;
											InteractableDoorHinge componentInParent3 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
											bool flag20 = componentInParent3 != null;
											if (flag20)
											{
												HighlighterTool.highlight(componentInParent3.door.transform, red);
											}
											else
											{
												HighlighterTool.highlight(PlayerInteract.interactable.transform, red);
											}
										}
									}
								}
								else
								{
									OV_PlayerInteract.target = null;
									OV_PlayerInteract.interactable = null;
								}
							}
						}
					}
				}
				else
				{
					bool flag21 = OV_PlayerInteract.focus != null && PlayerInteract.interactable != null;
					if (flag21)
					{
						InteractableDoorHinge componentInParent4 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoorHinge>();
						bool flag22 = componentInParent4 != null;
						if (flag22)
						{
							HighlighterTool.unhighlight(componentInParent4.door.transform);
						}
						else
						{
							HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
						}
					}
					OV_PlayerInteract.focus = null;
					OV_PlayerInteract.target = null;
					OV_PlayerInteract.interactable = null;
					OV_PlayerInteract.interactable2 = null;
				}
				bool isDead = OptimizationVariables.MainPlayer.life.isDead;
				if (!isDead)
				{
					bool flag23 = PlayerInteract.interactable != null;
					if (flag23)
					{
						EPlayerMessage message;
						string text;
						Color color;
						bool flag24 = PlayerInteract.interactable.checkHint(out message, out text, out color) && !PlayerUI.window.showCursor;
						if (flag24)
						{
							bool flag25 = PlayerInteract.interactable.CompareTag("Item");
							if (flag25)
							{
								PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, message, text, color, new object[]
								{
									((InteractableItem)PlayerInteract.interactable).item,
									((InteractableItem)PlayerInteract.interactable).asset
								});
							}
							else
							{
								PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, message, text, color, new object[0]);
							}
						}
					}
					else
					{
						bool flag26 = OV_PlayerInteract.purchaseAsset != null && OptimizationVariables.MainPlayer.movement.purchaseNode != null && !PlayerUI.window.showCursor;
						if (flag26)
						{
							PlayerUI.hint(null, EPlayerMessage.PURCHASE, string.Empty, Color.white, new object[]
							{
								OV_PlayerInteract.purchaseAsset.itemName,
								OptimizationVariables.MainPlayer.movement.purchaseNode.cost
							});
						}
						else
						{
							bool flag27 = OV_PlayerInteract.focus != null && OV_PlayerInteract.focus.CompareTag("Enemy");
							if (flag27)
							{
								Player player = DamageTool.getPlayer(OV_PlayerInteract.focus);
								bool flag28 = player != null && player != Player.player && !PlayerUI.window.showCursor;
								if (flag28)
								{
									PlayerUI.hint(null, EPlayerMessage.ENEMY, string.Empty, Color.white, new object[]
									{
										player.channel.owner
									});
								}
							}
						}
					}
					EPlayerMessage message2;
					float data;
					bool flag29 = PlayerInteract.interactable2 != null && PlayerInteract.interactable2.checkHint(out message2, out data) && !PlayerUI.window.showCursor;
					if (flag29)
					{
						PlayerUI.hint2(message2, (!OV_PlayerInteract.isHoldingKey) ? 0f : ((Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown) / this.salvageTime), data);
					}
					bool flag30 = (OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SITTING) && !Input.GetKey(KeyCode.LeftShift);
					if (flag30)
					{
						bool keyDown = Input.GetKeyDown(KeyCode.F1);
						if (keyDown)
						{
							this.hotkey(0);
						}
						bool keyDown2 = Input.GetKeyDown(KeyCode.F2);
						if (keyDown2)
						{
							this.hotkey(1);
						}
						bool keyDown3 = Input.GetKeyDown(KeyCode.F3);
						if (keyDown3)
						{
							this.hotkey(2);
						}
						bool keyDown4 = Input.GetKeyDown(KeyCode.F4);
						if (keyDown4)
						{
							this.hotkey(3);
						}
						bool keyDown5 = Input.GetKeyDown(KeyCode.F5);
						if (keyDown5)
						{
							this.hotkey(4);
						}
						bool keyDown6 = Input.GetKeyDown(KeyCode.F6);
						if (keyDown6)
						{
							this.hotkey(5);
						}
						bool keyDown7 = Input.GetKeyDown(KeyCode.F7);
						if (keyDown7)
						{
							this.hotkey(6);
						}
						bool keyDown8 = Input.GetKeyDown(KeyCode.F8);
						if (keyDown8)
						{
							this.hotkey(7);
						}
						bool keyDown9 = Input.GetKeyDown(KeyCode.F9);
						if (keyDown9)
						{
							this.hotkey(8);
						}
						bool keyDown10 = Input.GetKeyDown(KeyCode.F10);
						if (keyDown10)
						{
							this.hotkey(9);
						}
					}
					bool keyDown11 = Input.GetKeyDown(ControlsSettings.interact);
					if (keyDown11)
					{
						OV_PlayerInteract.lastKeyDown = Time.realtimeSinceStartup;
						OV_PlayerInteract.isHoldingKey = true;
					}
					bool flag31 = Input.GetKeyDown(ControlsSettings.inspect) && ControlsSettings.inspect != ControlsSettings.interact && OptimizationVariables.MainPlayer.equipment.canInspect;
					if (flag31)
					{
						OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
					}
					bool flag32 = OV_PlayerInteract.isHoldingKey;
					if (flag32)
					{
						bool keyUp = Input.GetKeyUp(ControlsSettings.interact);
						if (keyUp)
						{
							OV_PlayerInteract.isHoldingKey = false;
							bool showCursor = PlayerUI.window.showCursor;
							if (showCursor)
							{
								bool flag33 = OptimizationVariables.MainPlayer.inventory.isStoring && OptimizationVariables.MainPlayer.inventory.shouldInteractCloseStorage;
								if (flag33)
								{
									PlayerDashboardUI.close();
									PlayerLifeUI.open();
								}
								else
								{
									bool active = PlayerBarricadeSignUI.active;
									if (active)
									{
										PlayerBarricadeSignUI.close();
										PlayerLifeUI.open();
									}
									else
									{
										bool active2 = PlayerBarricadeStereoUI.active;
										if (active2)
										{
											PlayerBarricadeStereoUI.close();
											PlayerLifeUI.open();
										}
										else
										{
											bool active3 = PlayerBarricadeLibraryUI.active;
											if (active3)
											{
												PlayerBarricadeLibraryUI.close();
												PlayerLifeUI.open();
											}
											else
											{
												bool active4 = PlayerBarricadeMannequinUI.active;
												if (active4)
												{
													PlayerBarricadeMannequinUI.close();
													PlayerLifeUI.open();
												}
												else
												{
													bool active5 = PlayerNPCDialogueUI.active;
													if (active5)
													{
														bool dialogueAnimating = PlayerNPCDialogueUI.dialogueAnimating;
														if (dialogueAnimating)
														{
															PlayerNPCDialogueUI.skipText();
														}
														else
														{
															bool dialogueHasNextPage = PlayerNPCDialogueUI.dialogueHasNextPage;
															if (dialogueHasNextPage)
															{
																PlayerNPCDialogueUI.nextPage();
															}
															else
															{
																PlayerNPCDialogueUI.close();
																PlayerLifeUI.open();
															}
														}
													}
													else
													{
														bool active6 = PlayerNPCQuestUI.active;
														if (active6)
														{
															PlayerNPCQuestUI.closeNicely();
														}
														else
														{
															bool active7 = PlayerNPCVendorUI.active;
															if (active7)
															{
																PlayerNPCVendorUI.closeNicely();
															}
														}
													}
												}
											}
										}
									}
								}
							}
							else
							{
								bool flag34 = OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SITTING;
								if (flag34)
								{
									VehicleManager.exitVehicle();
								}
								else
								{
									bool flag35 = OV_PlayerInteract.focus != null && PlayerInteract.interactable != null;
									if (flag35)
									{
										bool flag36 = PlayerInteract.interactable.checkUseable();
										if (flag36)
										{
											PlayerInteract.interactable.use();
										}
									}
									else
									{
										bool flag37 = OV_PlayerInteract.purchaseAsset != null;
										if (flag37)
										{
											bool flag38 = OptimizationVariables.MainPlayer.skills.experience >= OptimizationVariables.MainPlayer.movement.purchaseNode.cost;
											if (flag38)
											{
												OptimizationVariables.MainPlayer.skills.sendPurchase(OptimizationVariables.MainPlayer.movement.purchaseNode);
											}
										}
										else
										{
											bool flag39 = ControlsSettings.inspect == ControlsSettings.interact && OptimizationVariables.MainPlayer.equipment.canInspect;
											if (flag39)
											{
												OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
											}
										}
									}
								}
							}
						}
						else
						{
							bool flag40 = Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown > this.salvageTime;
							if (flag40)
							{
								OV_PlayerInteract.isHoldingKey = false;
								bool flag41 = !PlayerUI.window.showCursor && PlayerInteract.interactable2 != null;
								if (flag41)
								{
									PlayerInteract.interactable2.use();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0400009E RID: 158
		private static FieldInfo FocusField;

		// Token: 0x0400009F RID: 159
		private static FieldInfo TargetField;

		// Token: 0x040000A0 RID: 160
		private static FieldInfo InteractableField;

		// Token: 0x040000A1 RID: 161
		private static FieldInfo Interactable2Field;

		// Token: 0x040000A2 RID: 162
		private static FieldInfo PurchaseAssetField;

		// Token: 0x040000A3 RID: 163
		private static bool isHoldingKey;

		// Token: 0x040000A4 RID: 164
		private static float lastInteract;

		// Token: 0x040000A5 RID: 165
		private static float lastKeyDown;

		// Token: 0x040000A6 RID: 166
		private static RaycastHit hit;
	}
}

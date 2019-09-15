using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options;
using DeftHack.Options.AimOptions;
using DeftHack.Threads;
using DeftHack.Utilities;
using DeftHack.Variables;
using DeftHackFree;
using SDG.Provider;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000BE RID: 190
	[Component]
	public class MiscComponent : MonoBehaviour
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0001C030 File Offset: 0x0001A230
		[Initializer]
		public static void Initialize()
		{
			HotkeyComponent.ActionDict.Add("_VFToggle", delegate
			{
				MiscOptions.VehicleFly = !MiscOptions.VehicleFly;
			});
			HotkeyComponent.ActionDict.Add("_ToggleAimbot", delegate
			{
				AimbotOptions.Enabled = !AimbotOptions.Enabled;
			});
			HotkeyComponent.ActionDict.Add("_AimbotOnKey", delegate
			{
				AimbotOptions.OnKey = !AimbotOptions.OnKey;
			});
			HotkeyComponent.ActionDict.Add("_ToggleFreecam", delegate
			{
				MiscOptions.Freecam = !MiscOptions.Freecam;
			});
			HotkeyComponent.ActionDict.Add("_PanicButton", delegate
			{
				MiscOptions.PanicMode = !MiscOptions.PanicMode;
				bool panicMode = MiscOptions.PanicMode;
				bool flag = panicMode;
				if (flag)
				{
					PlayerCoroutines.DisableAllVisuals();
				}
				else
				{
					PlayerCoroutines.EnableAllVisuals();
				}
			});
			HotkeyComponent.ActionDict.Add("_SelectPlayer", delegate
			{
				Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
				Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
				bool enablePlayerSelection = RaycastOptions.EnablePlayerSelection;
				bool flag = enablePlayerSelection;
				if (flag)
				{
					foreach (GameObject gameObject in RaycastUtilities.Objects)
					{
						Player component = gameObject.GetComponent<Player>();
						bool flag2 = component != null;
						bool flag3 = flag2;
						if (flag3)
						{
							bool flag4 = VectorUtilities.GetAngleDelta(position, forward, gameObject.transform.position) < (double)RaycastOptions.SelectedFOV;
							bool flag5 = flag4;
							if (flag5)
							{
								RaycastUtilities.TargetedPlayer = component;
								break;
							}
						}
					}
				}
			});
			HotkeyComponent.ActionDict.Add("_InstantDisconnect", delegate
			{
				Provider.disconnect();
			});
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001C188 File Offset: 0x0001A388
		[OnSpy]
		public static void Disable()
		{
			bool wasNightVision = MiscOptions.WasNightVision;
			bool flag = wasNightVision;
			if (flag)
			{
				MiscComponent.NightvisionBeforeSpy = true;
				MiscOptions.NightVision = false;
			}
			bool freecam = MiscOptions.Freecam;
			bool flag2 = freecam;
			if (flag2)
			{
				MiscComponent.FreecamBeforeSpy = true;
				MiscOptions.Freecam = false;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		[OffSpy]
		public static void Enable()
		{
			bool nightvisionBeforeSpy = MiscComponent.NightvisionBeforeSpy;
			bool flag = nightvisionBeforeSpy;
			if (flag)
			{
				MiscComponent.NightvisionBeforeSpy = false;
				MiscOptions.NightVision = true;
			}
			bool freecamBeforeSpy = MiscComponent.FreecamBeforeSpy;
			bool flag2 = freecamBeforeSpy;
			if (flag2)
			{
				MiscComponent.FreecamBeforeSpy = false;
				MiscOptions.Freecam = true;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001C208 File Offset: 0x0001A408
		private void Start()
		{
			MiscComponent.Instance = this;
			Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(delegate()
			{
				bool alwaysCheckMovementVerification = MiscOptions.AlwaysCheckMovementVerification;
				bool flag = alwaysCheckMovementVerification;
				if (flag)
				{
					MiscComponent.CheckMovementVerification();
				}
				else
				{
					MiscOptions.NoMovementVerification = false;
				}
			}));
			SkinsUtilities.RefreshEconInfo();
			HotkeyComponent.ActionDict.Add("_Com1", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com1);
			});
			HotkeyComponent.ActionDict.Add("_Com2", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com2);
			});
			HotkeyComponent.ActionDict.Add("_Com3", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com3);
			});
			HotkeyComponent.ActionDict.Add("_Com4", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com4);
			});
			HotkeyComponent.ActionDict.Add("_Com5", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com5);
			});
			HotkeyComponent.ActionDict.Add("_AutoPickUp", delegate
			{
				ItemOptions.AutoItemPickup = !ItemOptions.AutoItemPickup;
			});
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001C370 File Offset: 0x0001A570
		public void Update()
		{
			bool hang = MiscOptions.hang;
			if (hang)
			{
				Player.player.movement.pluginGravityMultiplier = 0f;
			}
			else
			{
				Player.player.movement.pluginGravityMultiplier = 1f;
			}
			bool flag = Camera.main != null && OptimizationVariables.MainCam == null;
			bool flag2 = flag;
			if (flag2)
			{
				OptimizationVariables.MainCam = Camera.main;
			}
			bool flag3 = !OptimizationVariables.MainPlayer;
			bool flag4 = !flag3;
			if (flag4)
			{
				bool flag5 = !DrawUtilities.ShouldRun();
				bool flag6 = !flag5;
				if (flag6)
				{
					int num;
					Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num);
					bool oofOnDeath = WeaponOptions.OofOnDeath;
					bool flag7 = oofOnDeath;
					if (flag7)
					{
						bool flag8 = num != this.currentKills;
						bool flag9 = flag8;
						if (flag9)
						{
							bool flag10 = this.currentKills != -1;
							bool flag11 = flag10;
							if (flag11)
							{
								OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetVariables.Audio["oof"], 3f);
							}
							this.currentKills = num;
						}
					}
					else
					{
						this.currentKills = num;
					}
					bool nightVision = MiscOptions.NightVision;
					bool flag12 = nightVision;
					if (flag12)
					{
						LevelLighting.vision = ELightingVision.MILITARY;
						LevelLighting.updateLighting();
						PlayerLifeUI.updateGrayscale();
						MiscOptions.WasNightVision = true;
					}
					else
					{
						bool wasNightVision = MiscOptions.WasNightVision;
						bool flag13 = wasNightVision;
						if (flag13)
						{
							LevelLighting.vision = ELightingVision.NONE;
							LevelLighting.updateLighting();
							PlayerLifeUI.updateGrayscale();
							MiscOptions.WasNightVision = false;
						}
					}
					bool enableDistanceCrash = MiscOptions.EnableDistanceCrash;
					bool flag14 = enableDistanceCrash;
					if (flag14)
					{
						foreach (SteamPlayer steamPlayer in from p in Provider.clients
						where p.player != OptimizationVariables.MainPlayer && VectorUtilities.GetDistance(p.player.transform.position, OptimizationVariables.MainPlayer.transform.position) < (double)MiscOptions.CrashDistance
						select p)
						{
							bool flag15 = !PlayerCrashThread.CrashTargets.Contains(steamPlayer.playerID.steamID);
							bool flag16 = flag15;
							if (flag16)
							{
								PlayerCrashThread.CrashTargets.Add(steamPlayer.playerID.steamID);
							}
						}
					}
					bool isDead = OptimizationVariables.MainPlayer.life.isDead;
					bool flag17 = isDead;
					if (flag17)
					{
						MiscComponent.LastDeath = OptimizationVariables.MainPlayer.transform.position;
					}
					bool crashByName = MiscOptions.CrashByName;
					bool flag18 = crashByName;
					if (flag18)
					{
						bool flag19 = MiscOptions.CrashWords.ToArray().Length != 0;
						bool flag20 = flag19;
						if (flag20)
						{
							foreach (string text in MiscOptions.CrashWords)
							{
								foreach (SteamPlayer steamPlayer2 in Provider.clients)
								{
									bool flag21 = FriendUtilities.IsFriendly(steamPlayer2.player);
									bool flag22 = !flag21;
									if (flag22)
									{
										bool flag23 = !steamPlayer2.playerID.characterName.ToLower().Contains(text.ToLower());
										bool flag24 = !flag23;
										if (flag24)
										{
											PlayerCrashThread.CrashTargets.Add(steamPlayer2.playerID.steamID);
											break;
										}
									}
								}
							}
						}
						bool flag25 = MiscOptions.CrashIDs.ToArray().Length != 0;
						bool flag26 = flag25;
						if (flag26)
						{
							foreach (string b in MiscOptions.CrashIDs)
							{
								foreach (SteamPlayer steamPlayer3 in Provider.clients)
								{
									bool flag27 = FriendUtilities.IsFriendly(steamPlayer3.player);
									bool flag28 = !flag27;
									if (flag28)
									{
										bool flag29 = steamPlayer3.playerID.steamID.ToString() != b;
										bool flag30 = !flag29;
										if (flag30)
										{
											PlayerCrashThread.CrashTargets.Add(steamPlayer3.playerID.steamID);
											break;
										}
									}
								}
							}
						}
						bool noFlash = MiscOptions.NoFlash;
						if (noFlash)
						{
							bool flag31 = MiscOptions.NoFlash && ((Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).a > 0f;
							if (flag31)
							{
								Color color = (Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
								color.a = 0f;
								typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, color);
							}
						}
					}
				}
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001C8AC File Offset: 0x0001AAAC
		public void FixedUpdate()
		{
			bool flag = !OptimizationVariables.MainPlayer;
			bool flag2 = !flag;
			if (flag2)
			{
				MiscComponent.VehicleFlight();
				MiscComponent.PlayerFlight();
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001C8E0 File Offset: 0x0001AAE0
		public static void PlayerFlight()
		{
			Player mainPlayer = OptimizationVariables.MainPlayer;
			bool flag = !MiscOptions.PlayerFlight;
			bool flag2 = flag;
			if (flag2)
			{
				ItemCloudAsset itemCloudAsset = mainPlayer.equipment.asset as ItemCloudAsset;
				mainPlayer.movement.itemGravityMultiplier = ((itemCloudAsset != null) ? itemCloudAsset.gravity : 1f);
			}
			else
			{
				mainPlayer.movement.itemGravityMultiplier = 0f;
				float flightSpeedMultiplier = MiscOptions.FlightSpeedMultiplier;
				bool flag3 = HotkeyUtilities.IsHotkeyHeld("_FlyUp");
				bool flag4 = flag3;
				if (flag4)
				{
					mainPlayer.transform.position += mainPlayer.transform.up / 5f * flightSpeedMultiplier;
				}
				bool flag5 = HotkeyUtilities.IsHotkeyHeld("_FlyDown");
				bool flag6 = flag5;
				if (flag6)
				{
					mainPlayer.transform.position -= mainPlayer.transform.up / 5f * flightSpeedMultiplier;
				}
				bool flag7 = HotkeyUtilities.IsHotkeyHeld("_FlyLeft");
				bool flag8 = flag7;
				if (flag8)
				{
					mainPlayer.transform.position -= mainPlayer.transform.right / 5f * flightSpeedMultiplier;
				}
				bool flag9 = HotkeyUtilities.IsHotkeyHeld("_FlyRight");
				bool flag10 = flag9;
				if (flag10)
				{
					mainPlayer.transform.position += mainPlayer.transform.right / 5f * flightSpeedMultiplier;
				}
				bool flag11 = HotkeyUtilities.IsHotkeyHeld("_FlyForward");
				bool flag12 = flag11;
				if (flag12)
				{
					mainPlayer.transform.position += mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
				}
				bool flag13 = HotkeyUtilities.IsHotkeyHeld("_FlyBackward");
				bool flag14 = flag13;
				if (flag14)
				{
					mainPlayer.transform.position -= mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001CB04 File Offset: 0x0001AD04
		public static void VehicleFlight()
		{
			InteractableVehicle vehicle = OptimizationVariables.MainPlayer.movement.getVehicle();
			bool flag = vehicle == null;
			bool flag2 = !flag;
			if (flag2)
			{
				Rigidbody component = vehicle.GetComponent<Rigidbody>();
				bool flag3 = component == null;
				bool flag4 = !flag3;
				if (flag4)
				{
					bool vehicleFly = MiscOptions.VehicleFly;
					bool flag5 = vehicleFly;
					if (flag5)
					{
						float num = MiscOptions.VehicleUseMaxSpeed ? (vehicle.asset.speedMax * Time.fixedDeltaTime) : (MiscOptions.SpeedMultiplier / 3f);
						component.useGravity = false;
						component.isKinematic = true;
						Transform transform = vehicle.transform;
						bool flag6 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeUp");
						bool flag7 = flag6;
						if (flag7)
						{
							transform.position += new Vector3(0f, num * 0.65f, 0f);
						}
						bool flag8 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeDown");
						bool flag9 = flag8;
						if (flag9)
						{
							transform.position -= new Vector3(0f, num * 0.65f, 0f);
						}
						bool flag10 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeLeft");
						bool flag11 = flag10;
						if (flag11)
						{
							component.MovePosition(transform.position - transform.right * num);
						}
						bool flag12 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeRight");
						bool flag13 = flag12;
						if (flag13)
						{
							component.MovePosition(transform.position + transform.right * num);
						}
						bool flag14 = HotkeyUtilities.IsHotkeyHeld("_VFMoveForward");
						bool flag15 = flag14;
						if (flag15)
						{
							component.MovePosition(transform.position + transform.forward * num);
						}
						bool flag16 = HotkeyUtilities.IsHotkeyHeld("_VFMoveBackward");
						bool flag17 = flag16;
						if (flag17)
						{
							component.MovePosition(transform.position - transform.forward * num);
						}
						bool flag18 = HotkeyUtilities.IsHotkeyHeld("_VFRotateRight");
						bool flag19 = flag18;
						if (flag19)
						{
							transform.Rotate(0f, 1f, 0f);
						}
						bool flag20 = HotkeyUtilities.IsHotkeyHeld("_VFRotateLeft");
						bool flag21 = flag20;
						if (flag21)
						{
							transform.Rotate(0f, -1f, 0f);
						}
						bool flag22 = HotkeyUtilities.IsHotkeyHeld("_VFRollLeft");
						bool flag23 = flag22;
						if (flag23)
						{
							transform.Rotate(0f, 0f, 2f);
						}
						bool flag24 = HotkeyUtilities.IsHotkeyHeld("_VFRollRight");
						bool flag25 = flag24;
						if (flag25)
						{
							transform.Rotate(0f, 0f, -2f);
						}
						bool flag26 = HotkeyUtilities.IsHotkeyHeld("_VFRotateUp");
						bool flag27 = flag26;
						if (flag27)
						{
							vehicle.transform.Rotate(-2f, 0f, 0f);
						}
						bool flag28 = HotkeyUtilities.IsHotkeyHeld("_VFRotateDown");
						bool flag29 = flag28;
						if (flag29)
						{
							vehicle.transform.Rotate(2f, 0f, 0f);
						}
					}
					else
					{
						component.useGravity = true;
						component.isKinematic = false;
					}
				}
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000031D5 File Offset: 0x000013D5
		public static void CheckMovementVerification()
		{
			MiscComponent.Instance.StartCoroutine(MiscComponent.CheckVerification(OptimizationVariables.MainPlayer.transform.position));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001CE30 File Offset: 0x0001B030
		public static void incrementStatTrackerValue(ushort itemID, int newValue)
		{
			bool flag = Player.player == null;
			if (!flag)
			{
				SteamPlayer owner = Player.player.channel.owner;
				bool flag2 = owner == null;
				if (!flag2)
				{
					int num;
					bool flag3 = !owner.getItemSkinItemDefID(itemID, out num);
					if (!flag3)
					{
						string tags;
						string dynamic_props;
						bool flag4 = !owner.getTagsAndDynamicPropsForItem(num, out tags, out dynamic_props);
						if (!flag4)
						{
							DynamicEconDetails dynamicEconDetails = new DynamicEconDetails(tags, dynamic_props);
							EStatTrackerType type;
							int num2;
							bool statTrackerValue = dynamicEconDetails.getStatTrackerValue(out type, out num2);
							if (statTrackerValue)
							{
								bool flag5 = !owner.modifiedItems.Contains(itemID);
								if (flag5)
								{
									owner.modifiedItems.Add(itemID);
								}
								int i = 0;
								while (i < owner.skinItems.Length)
								{
									bool flag6 = owner.skinItems[i] != num;
									if (flag6)
									{
										i++;
									}
									else
									{
										bool flag7 = i < owner.skinDynamicProps.Length;
										if (flag7)
										{
											owner.skinDynamicProps[i] = dynamicEconDetails.getPredictedDynamicPropsJsonForStatTracker(type, newValue);
											break;
										}
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000031F7 File Offset: 0x000013F7
		public static IEnumerator CheckVerification(Vector3 LastPos)
		{
			bool flag = Time.realtimeSinceStartup - MiscComponent.LastMovementCheck < 0.8f;
			bool flag3 = flag;
			if (flag3)
			{
				yield break;
			}
			MiscComponent.LastMovementCheck = Time.realtimeSinceStartup;
			OptimizationVariables.MainPlayer.transform.position = new Vector3(0f, -1337f, 0f);
			yield return new WaitForSeconds(3f);
			bool flag2 = VectorUtilities.GetDistance(OptimizationVariables.MainPlayer.transform.position, LastPos) < 10.0;
			bool flag4 = flag2;
			if (flag4)
			{
				MiscOptions.NoMovementVerification = false;
			}
			else
			{
				MiscOptions.NoMovementVerification = true;
				OptimizationVariables.MainPlayer.transform.position = LastPos + new Vector3(0f, 5f, 0f);
			}
			yield break;
		}

		// Token: 0x040003D0 RID: 976
		public static Vector3 LastDeath;

		// Token: 0x040003D1 RID: 977
		public static MiscComponent Instance;

		// Token: 0x040003D2 RID: 978
		public static float LastMovementCheck;

		// Token: 0x040003D3 RID: 979
		public static bool FreecamBeforeSpy;

		// Token: 0x040003D4 RID: 980
		public static bool NightvisionBeforeSpy;

		// Token: 0x040003D5 RID: 981
		public static List<PlayerInputPacket> ClientsidePackets;

		// Token: 0x040003D6 RID: 982
		public static FieldInfo Primary = typeof(PlayerEquipment).GetField("_primary", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040003D7 RID: 983
		public static FieldInfo Sequence = typeof(PlayerInput).GetField("sequence", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040003D8 RID: 984
		public static FieldInfo CPField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040003D9 RID: 985
		private int currentKills = -1;

		// Token: 0x040003DA RID: 986
		private bool _isBroken;
	}
}

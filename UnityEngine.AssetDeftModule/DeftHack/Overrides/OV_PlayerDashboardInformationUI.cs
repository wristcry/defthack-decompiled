using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x02000044 RID: 68
	public static class OV_PlayerDashboardInformationUI
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00009338 File Offset: 0x00007538
		private static Sleek mapDynamicContainer
		{
			get
			{
				return (Sleek)OV_PlayerDashboardInformationUI.DynamicContainerInfo.GetValue(null);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000935C File Offset: 0x0000755C
		public static int GetMap()
		{
			PlayerInventory inventory = OptimizationVariables.MainPlayer.inventory;
			bool flag = MiscOptions.GPS || inventory.has(1176) != null;
			bool flag2 = flag;
			int result;
			if (flag2)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000026D8 File Offset: 0x000008D8
		[Initializer]
		public static void Init()
		{
			OV_PlayerDashboardInformationUI.DynamicContainerInfo = typeof(PlayerDashboardInformationUI).GetField("mapDynamicContainer", ReflectionVariables.PrivateStatic);
			OV_PlayerDashboardInformationUI.RefreshStaticMap = typeof(PlayerDashboardInformationUI).GetMethod("refreshStaticMap", BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000093A4 File Offset: 0x000075A4
		[OnSpy]
		public static void Disable()
		{
			bool flag = !DrawUtilities.ShouldRun();
			bool flag2 = !flag;
			if (flag2)
			{
				OV_PlayerDashboardInformationUI.WasEnabled = MiscOptions.ShowPlayersOnMap;
				OV_PlayerDashboardInformationUI.WasGPSEnabled = MiscOptions.Compass;
				MiscOptions.ShowPlayersOnMap = false;
				MiscOptions.GPS = false;
				OV_PlayerDashboardInformationUI.RefreshStaticMap.Invoke(OptimizationVariables.MainPlayer.inventory, new object[]
				{
					OV_PlayerDashboardInformationUI.GetMap()
				});
				OV_PlayerDashboardInformationUI.OV_refreshDynamicMap();
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009414 File Offset: 0x00007614
		[OffSpy]
		public static void Enable()
		{
			bool flag = !DrawUtilities.ShouldRun();
			bool flag2 = !flag;
			if (flag2)
			{
				MiscOptions.ShowPlayersOnMap = OV_PlayerDashboardInformationUI.WasEnabled;
				MiscOptions.GPS = OV_PlayerDashboardInformationUI.WasGPSEnabled;
				OV_PlayerDashboardInformationUI.RefreshStaticMap.Invoke(OptimizationVariables.MainPlayer.inventory, new object[]
				{
					OV_PlayerDashboardInformationUI.GetMap()
				});
				OV_PlayerDashboardInformationUI.OV_refreshDynamicMap();
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009478 File Offset: 0x00007678
		[Override(typeof(PlayerDashboardInformationUI), "searchForMapsInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_searchForMapsInInventory(ref bool enableChart, ref bool enableMap)
		{
			bool gps = MiscOptions.GPS;
			bool flag = gps;
			if (flag)
			{
				enableMap = true;
				enableChart = true;
			}
			else
			{
				OverrideUtilities.CallOriginal(null, Array.Empty<object>());
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000094AC File Offset: 0x000076AC
		[Override(typeof(PlayerDashboardInventoryUI), "updateNearbyDrops", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_updateNearbyDrops()
		{
			bool nearbyItemRaycast = MiscOptions.NearbyItemRaycast;
			bool flag = nearbyItemRaycast;
			if (flag)
			{
				OV_Physics.ForceReturnFalse = true;
			}
			OverrideUtilities.CallOriginal(null, Array.Empty<object>());
			OV_Physics.ForceReturnFalse = false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000094E0 File Offset: 0x000076E0
		[Override(typeof(PlayerDashboardInformationUI), "refreshDynamicMap", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_refreshDynamicMap()
		{
			OV_PlayerDashboardInformationUI.mapDynamicContainer.remove();
			bool flag = !PlayerDashboardInformationUI.active;
			bool flag2 = !flag;
			if (flag2)
			{
				bool flag3 = PlayerDashboardInformationUI.noLabel.isVisible || !Provider.modeConfigData.Gameplay.Group_Map;
				bool flag4 = !flag3;
				if (flag4)
				{
					bool flag5 = LevelManager.levelType == ELevelType.ARENA;
					bool flag6 = flag5;
					if (flag6)
					{
						SleekImageTexture sleekImageTexture = new SleekImageTexture(PlayerDashboardInformationUI.icons.load<Texture2D>("Arena_Area"));
						sleekImageTexture.positionScale_X = LevelManager.arenaTargetCenter.x / (float)(Level.size - Level.border * 2) + 0.5f - LevelManager.arenaTargetRadius / (float)(Level.size - Level.border * 2);
						sleekImageTexture.positionScale_Y = 0.5f - LevelManager.arenaTargetCenter.z / (float)(Level.size - Level.border * 2) - LevelManager.arenaTargetRadius / (float)(Level.size - Level.border * 2);
						sleekImageTexture.sizeScale_X = LevelManager.arenaTargetRadius * 2f / (float)(Level.size - Level.border * 2);
						sleekImageTexture.sizeScale_Y = LevelManager.arenaTargetRadius * 2f / (float)(Level.size - Level.border * 2);
						sleekImageTexture.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture);
						SleekImageTexture sleekImageTexture2 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture2.positionScale_Y = sleekImageTexture.positionScale_Y;
						sleekImageTexture2.sizeScale_X = sleekImageTexture.positionScale_X;
						sleekImageTexture2.sizeScale_Y = sleekImageTexture.sizeScale_Y;
						sleekImageTexture2.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture2);
						SleekImageTexture sleekImageTexture3 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture3.positionScale_X = sleekImageTexture.positionScale_X + sleekImageTexture.sizeScale_X;
						sleekImageTexture3.positionScale_Y = sleekImageTexture.positionScale_Y;
						sleekImageTexture3.sizeScale_X = 1f - sleekImageTexture.positionScale_X - sleekImageTexture.sizeScale_X;
						sleekImageTexture3.sizeScale_Y = sleekImageTexture.sizeScale_Y;
						sleekImageTexture3.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture3);
						SleekImageTexture sleekImageTexture4 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture4.sizeScale_X = 1f;
						sleekImageTexture4.sizeScale_Y = sleekImageTexture.positionScale_Y;
						sleekImageTexture4.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture4);
						SleekImageTexture sleekImageTexture5 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture5.positionScale_Y = sleekImageTexture.positionScale_Y + sleekImageTexture.sizeScale_Y;
						sleekImageTexture5.sizeScale_X = 1f;
						sleekImageTexture5.sizeScale_Y = 1f - sleekImageTexture.positionScale_Y - sleekImageTexture.sizeScale_Y;
						sleekImageTexture5.backgroundColor = new Color(1f, 1f, 0f, 0.5f);
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture5);
						SleekImageTexture sleekImageTexture6 = new SleekImageTexture(PlayerDashboardInformationUI.icons.load<Texture2D>("Arena_Area"));
						sleekImageTexture6.positionScale_X = LevelManager.arenaCurrentCenter.x / (float)(Level.size - Level.border * 2) + 0.5f - LevelManager.arenaCurrentRadius / (float)(Level.size - Level.border * 2);
						sleekImageTexture6.positionScale_Y = 0.5f - LevelManager.arenaCurrentCenter.z / (float)(Level.size - Level.border * 2) - LevelManager.arenaCurrentRadius / (float)(Level.size - Level.border * 2);
						sleekImageTexture6.sizeScale_X = LevelManager.arenaCurrentRadius * 2f / (float)(Level.size - Level.border * 2);
						sleekImageTexture6.sizeScale_Y = LevelManager.arenaCurrentRadius * 2f / (float)(Level.size - Level.border * 2);
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture6);
						SleekImageTexture sleekImageTexture7 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture7.positionScale_Y = sleekImageTexture6.positionScale_Y;
						sleekImageTexture7.sizeScale_X = sleekImageTexture6.positionScale_X;
						sleekImageTexture7.sizeScale_Y = sleekImageTexture6.sizeScale_Y;
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture7);
						SleekImageTexture sleekImageTexture8 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture8.positionScale_X = sleekImageTexture6.positionScale_X + sleekImageTexture6.sizeScale_X;
						sleekImageTexture8.positionScale_Y = sleekImageTexture6.positionScale_Y;
						sleekImageTexture8.sizeScale_X = 1f - sleekImageTexture6.positionScale_X - sleekImageTexture6.sizeScale_X;
						sleekImageTexture8.sizeScale_Y = sleekImageTexture6.sizeScale_Y;
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture8);
						SleekImageTexture sleekImageTexture9 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture9.sizeScale_X = 1f;
						sleekImageTexture9.sizeScale_Y = sleekImageTexture6.positionScale_Y;
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture9);
						SleekImageTexture sleekImageTexture10 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"));
						sleekImageTexture10.positionScale_Y = sleekImageTexture6.positionScale_Y + sleekImageTexture6.sizeScale_Y;
						sleekImageTexture10.sizeScale_X = 1f;
						sleekImageTexture10.sizeScale_Y = 1f - sleekImageTexture6.positionScale_Y - sleekImageTexture6.sizeScale_Y;
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture10);
					}
					foreach (SteamPlayer steamPlayer in Provider.clients)
					{
						bool flag7 = steamPlayer.model == null;
						bool flag8 = !flag7;
						if (flag8)
						{
							PlayerQuests quests = steamPlayer.player.quests;
							bool flag9 = steamPlayer.playerID.steamID != Provider.client && !quests.isMemberOfSameGroupAs(OptimizationVariables.MainPlayer) && (!MiscOptions.ShowPlayersOnMap || !DrawUtilities.ShouldRun() || PlayerCoroutines.IsSpying);
							bool flag10 = !flag9;
							if (flag10)
							{
								SleekImageTexture sleekImageTexture11 = new SleekImageTexture();
								sleekImageTexture11.positionOffset_X = -10;
								sleekImageTexture11.positionOffset_Y = -10;
								sleekImageTexture11.positionScale_X = steamPlayer.player.transform.position.x / (float)(Level.size - Level.border * 2) + 0.5f;
								sleekImageTexture11.positionScale_Y = 0.5f - steamPlayer.player.transform.position.z / (float)(Level.size - Level.border * 2);
								sleekImageTexture11.sizeOffset_X = 20;
								sleekImageTexture11.sizeOffset_Y = 20;
								bool flag11 = !OptionsSettings.streamer;
								bool flag12 = flag11;
								if (flag12)
								{
									sleekImageTexture11.texture = Provider.provider.communityService.getIcon(steamPlayer.playerID.steamID, false);
								}
								sleekImageTexture11.addLabel(steamPlayer.playerID.characterName, ESleekSide.RIGHT);
								sleekImageTexture11.shouldDestroyTexture = true;
								OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture11);
								bool flag13 = !quests.isMarkerPlaced;
								bool flag14 = !flag13;
								if (flag14)
								{
									SleekImageTexture sleekImageTexture12 = new SleekImageTexture(PlayerDashboardInformationUI.icons.load<Texture2D>("Marker"));
									sleekImageTexture12.positionScale_X = quests.markerPosition.x / (float)(Level.size - Level.border * 2) + 0.5f;
									sleekImageTexture12.positionScale_Y = 0.5f - quests.markerPosition.z / (float)(Level.size - Level.border * 2);
									sleekImageTexture12.positionOffset_X = -10;
									sleekImageTexture12.positionOffset_Y = -10;
									sleekImageTexture12.sizeOffset_X = 20;
									sleekImageTexture12.sizeOffset_Y = 20;
									sleekImageTexture12.backgroundColor = steamPlayer.markerColor;
									sleekImageTexture12.addLabel(steamPlayer.playerID.characterName, ESleekSide.RIGHT);
									OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture12);
								}
							}
						}
					}
					bool flag15 = OptimizationVariables.MainPlayer == null;
					bool flag16 = !flag15;
					if (flag16)
					{
						SleekImageTexture sleekImageTexture13 = new SleekImageTexture();
						sleekImageTexture13.positionOffset_X = -10;
						sleekImageTexture13.positionOffset_Y = -10;
						sleekImageTexture13.positionScale_X = OptimizationVariables.MainPlayer.transform.position.x / (float)(Level.size - Level.border * 2) + 0.5f;
						sleekImageTexture13.positionScale_Y = 0.5f - OptimizationVariables.MainPlayer.transform.position.z / (float)(Level.size - Level.border * 2);
						sleekImageTexture13.sizeOffset_X = 20;
						sleekImageTexture13.sizeOffset_Y = 20;
						sleekImageTexture13.isAngled = true;
						sleekImageTexture13.angle = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
						sleekImageTexture13.texture = PlayerDashboardInformationUI.icons.load<Texture2D>("Player");
						sleekImageTexture13.backgroundTint = ESleekTint.FOREGROUND;
						sleekImageTexture13.addLabel(string.IsNullOrEmpty(Characters.active.nick) ? Characters.active.name : Characters.active.nick, ESleekSide.RIGHT);
						OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture13);
					}
				}
			}
		}

		// Token: 0x04000087 RID: 135
		public static bool WasGPSEnabled;

		// Token: 0x04000088 RID: 136
		public static bool WasEnabled;

		// Token: 0x04000089 RID: 137
		public static MethodInfo RefreshStaticMap;

		// Token: 0x0400008A RID: 138
		public static FieldInfo DynamicContainerInfo;
	}
}

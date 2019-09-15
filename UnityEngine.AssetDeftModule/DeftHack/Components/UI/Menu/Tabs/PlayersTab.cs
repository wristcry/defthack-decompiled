using System;
using System.Linq;
using DeftHack.Options;
using DeftHack.Threads;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000AD RID: 173
	public static class PlayersTab
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00012848 File Offset: 0x00010A48
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

		// Token: 0x06000279 RID: 633 RVA: 0x000188B4 File Offset: 0x00016AB4
		public static void Tab()
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(5f);
			PlayersTab.SearchString = Prefab.TextField(PlayersTab.SearchString, "Поиск: ", 466);
			GUILayout.EndHorizontal();
			Prefab.ScrollView(new Rect(0f, 30f, 466f, 215f), "Игроки", ref PlayersTab.PlayersScroll, delegate()
			{
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					Player player = Provider.clients[i].player;
					bool flag = player == OptimizationVariables.MainPlayer || player == null || (PlayersTab.SearchString != "" && player.name.IndexOf(PlayersTab.SearchString, StringComparison.OrdinalIgnoreCase) == -1);
					bool flag2 = !flag;
					if (flag2)
					{
						bool flag3 = FriendUtilities.IsFriendly(player);
						bool flag4 = MiscOptions.SpectatedPlayer == player;
						bool flag5 = PlayerCrashThread.CrashTarget == player.channel.owner.playerID.steamID;
						bool flag6 = player == PlayersTab.SelectedPlayer;
						string text = flag5 ? "<color=#ff0000ff>" : (flag3 ? "<color=#00ff00ff>" : "");
						bool flag7 = Prefab.Button(string.Concat(new string[]
						{
							flag6 ? "<b>" : "",
							flag4 ? "<color=#0000ffff>[НАБЛЮДЕНИЕ]</color> " : "",
							text,
							player.name,
							(flag3 || flag5) ? "</color>" : "",
							flag6 ? "</b>" : ""
						}), 400f, 25f, Array.Empty<GUILayoutOption>());
						bool flag8 = flag7;
						if (flag8)
						{
							PlayersTab.SelectedPlayer = player;
						}
						GUILayout.Space(2f);
					}
				}
			}, 20, Array.Empty<GUILayoutOption>());
			Prefab.MenuArea(new Rect(0f, 260f, 190f, 175f), "ОПЦИИ", delegate
			{
				bool flag = PlayersTab.SelectedPlayer == null;
				bool flag2 = !flag;
				if (flag2)
				{
					CSteamID steamID = PlayersTab.SelectedPlayer.channel.owner.playerID.steamID;
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
					bool flag3 = FriendUtilities.IsFriendly(PlayersTab.SelectedPlayer);
					bool flag4 = flag3;
					if (flag4)
					{
						bool flag5 = Prefab.Button("Убрать из друзей", 150f, 25f, Array.Empty<GUILayoutOption>());
						bool flag6 = flag5;
						if (flag6)
						{
							FriendUtilities.RemoveFriend(PlayersTab.SelectedPlayer);
						}
					}
					else
					{
						bool flag7 = Prefab.Button("Добавить в друзья", 150f, 25f, Array.Empty<GUILayoutOption>());
						bool flag8 = flag7;
						if (flag8)
						{
							FriendUtilities.AddFriend(PlayersTab.SelectedPlayer);
						}
					}
					bool flag9 = Prefab.Button("Наблюдаль", 150f, 25f, Array.Empty<GUILayoutOption>());
					bool flag10 = flag9;
					if (flag10)
					{
						MiscOptions.SpectatedPlayer = PlayersTab.SelectedPlayer;
					}
					bool flag11 = MiscOptions.SpectatedPlayer != null && MiscOptions.SpectatedPlayer == PlayersTab.SelectedPlayer;
					bool flag12 = flag11;
					if (flag12)
					{
						bool flag13 = Prefab.Button("Не наблюдать", 150f, 25f, Array.Empty<GUILayoutOption>());
						bool flag14 = flag13;
						if (flag14)
						{
							MiscOptions.SpectatedPlayer = null;
						}
					}
					bool noMovementVerification = MiscOptions.NoMovementVerification;
					bool flag15 = noMovementVerification;
					if (flag15)
					{
						bool flag16 = Prefab.Button("Телепортироваться", 150f, 25f, Array.Empty<GUILayoutOption>());
						bool flag17 = flag16;
						if (flag17)
						{
							OptimizationVariables.MainPlayer.transform.position = PlayersTab.SelectedPlayer.transform.position;
						}
					}
					GUILayout.EndVertical();
					GUILayout.EndHorizontal();
				}
			});
			Prefab.MenuArea(new Rect(196f, 260f, 270f, 175f), "Информация", delegate
			{
				bool flag = PlayersTab.SelectedPlayer == null;
				bool flag2 = !flag;
				if (flag2)
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
					GUILayout.Label("SteamID:", Array.Empty<GUILayoutOption>());
					GUILayout.TextField(PlayersTab.SelectedPlayer.channel.owner.playerID.steamID.ToString(), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Space(2f);
					GUILayout.TextField("Локация: " + LocationUtilities.GetClosestLocation(PlayersTab.SelectedPlayer.transform.position).name, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Label("Оружие: " + ((PlayersTab.SelectedPlayer.equipment.asset != null) ? PlayersTab.SelectedPlayer.equipment.asset.itemName : "Fists"), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Label("Транспорт: " + ((PlayersTab.SelectedPlayer.movement.getVehicle() != null) ? PlayersTab.SelectedPlayer.movement.getVehicle().asset.name : "No Vehicle"), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Label("Кол-во в группе: " + Provider.clients.Count((SteamPlayer c) => c.player != PlayersTab.SelectedPlayer && c.player.quests.isMemberOfSameGroupAs(PlayersTab.SelectedPlayer)), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.EndVertical();
					GUILayout.EndHorizontal();
				}
			});
		}

		// Token: 0x0400038C RID: 908
		public static Vector2 PlayersScroll;

		// Token: 0x0400038D RID: 909
		public static Player SelectedPlayer;

		// Token: 0x0400038E RID: 910
		public static string SearchString = "";
	}
}

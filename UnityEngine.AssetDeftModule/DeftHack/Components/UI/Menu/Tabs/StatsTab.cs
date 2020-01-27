using System;

using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000B2 RID: 178
	public static class StatsTab
	{
		// Token: 0x06000287 RID: 647 RVA: 0x000191E8 File Offset: 0x000173E8
		public static void Tab()
		{
			Prefab.ScrollView(new Rect(0f, 0f, 250f, 436f), "Статистика", ref StatsTab.ScrollPos, delegate()
			{
				for (int i = 0; i < StatsTab.StatLabels.Length; i++)
				{
					string text = StatsTab.StatLabels[i];
					bool flag = Prefab.Button(text, 205f, 25f, Array.Empty<GUILayoutOption>());
					bool flag2 = flag;
					if (flag2)
					{
						StatsTab.Selected = i;
					}
					GUILayout.Space(3f);
				}
				GUILayout.Label("Получение достижений", Prefab._TextStyle, new GUILayoutOption[0]);
				bool flag3 = Prefab.Button("Welcome to PEI", 205f, 25f, new GUILayoutOption[0]);
				if (flag3)
				{
					Provider.provider.achievementsService.setAchievement("pei");
				}
				GUILayout.Space(3f);
				bool flag4 = Prefab.Button("A Bridge Too Far", 205f, 25f, new GUILayoutOption[0]);
				if (flag4)
				{
					Provider.provider.achievementsService.setAchievement("bridge");
				}
				GUILayout.Space(3f);
				bool flag5 = Prefab.Button("Mastermind", 205f, 25f, new GUILayoutOption[0]);
				if (flag5)
				{
					Provider.provider.achievementsService.setAchievement("mastermind");
				}
				GUILayout.Space(3f);
				bool flag6 = Prefab.Button("Offense", 205f, 25f, new GUILayoutOption[0]);
				if (flag6)
				{
					Provider.provider.achievementsService.setAchievement("offense");
				}
				GUILayout.Space(3f);
				bool flag7 = Prefab.Button("Defense", 205f, 25f, new GUILayoutOption[0]);
				if (flag7)
				{
					Provider.provider.achievementsService.setAchievement("defense");
				}
				GUILayout.Space(3f);
				bool flag8 = Prefab.Button("Support", 205f, 25f, new GUILayoutOption[0]);
				if (flag8)
				{
					Provider.provider.achievementsService.setAchievement("support");
				}
				GUILayout.Space(3f);
				bool flag9 = Prefab.Button("Experienced + Schooled", 205f, 25f, new GUILayoutOption[0]);
				if (flag9)
				{
					Provider.provider.achievementsService.setAchievement("experienced");
				}
				GUILayout.Space(3f);
				bool flag10 = Prefab.Button("Hoarder + Scavenger", 205f, 25f, new GUILayoutOption[0]);
				if (flag10)
				{
					Provider.provider.achievementsService.setAchievement("hoarder");
				}
				GUILayout.Space(3f);
				bool flag11 = Prefab.Button("Outdoors + Camper", 205f, 25f, new GUILayoutOption[0]);
				if (flag11)
				{
					Provider.provider.achievementsService.setAchievement("outdoors");
				}
				GUILayout.Space(3f);
				bool flag12 = Prefab.Button("Psychopath + Murderer", 205f, 25f, new GUILayoutOption[0]);
				if (flag12)
				{
					Provider.provider.achievementsService.setAchievement("psychopath");
				}
				GUILayout.Space(3f);
				bool flag13 = Prefab.Button("Survivor", 205f, 25f, new GUILayoutOption[0]);
				if (flag13)
				{
					Provider.provider.achievementsService.setAchievement("survivor");
				}
				GUILayout.Space(3f);
				bool flag14 = Prefab.Button("Berries", 205f, 25f, new GUILayoutOption[0]);
				if (flag14)
				{
					Provider.provider.achievementsService.setAchievement("berries");
				}
				GUILayout.Space(3f);
				bool flag15 = Prefab.Button("Accident Prone", 205f, 25f, new GUILayoutOption[0]);
				if (flag15)
				{
					Provider.provider.achievementsService.setAchievement("accident_prone");
				}
				GUILayout.Space(3f);
				bool flag16 = Prefab.Button("Behind the Wheel", 205f, 25f, new GUILayoutOption[0]);
				if (flag16)
				{
					Provider.provider.achievementsService.setAchievement("wheel");
				}
				GUILayout.Space(3f);
				bool flag17 = Prefab.Button("Welcome to the Yukon", 205f, 25f, new GUILayoutOption[0]);
				if (flag17)
				{
					Provider.provider.achievementsService.setAchievement("yukon");
				}
				GUILayout.Space(3f);
				bool flag18 = Prefab.Button("Welcome to Washington", 205f, 25f, new GUILayoutOption[0]);
				if (flag18)
				{
					Provider.provider.achievementsService.setAchievement("washington");
				}
				GUILayout.Space(3f);
				bool flag19 = Prefab.Button("Fishing", 205f, 25f, new GUILayoutOption[0]);
				if (flag19)
				{
					Provider.provider.achievementsService.setAchievement("fishing");
				}
				GUILayout.Space(3f);
				bool flag20 = Prefab.Button("Crafting", 205f, 25f, new GUILayoutOption[0]);
				if (flag20)
				{
					Provider.provider.achievementsService.setAchievement("crafting");
				}
				GUILayout.Space(3f);
				bool flag21 = Prefab.Button("Farming", 205f, 25f, new GUILayoutOption[0]);
				if (flag21)
				{
					Provider.provider.achievementsService.setAchievement("farming");
				}
				GUILayout.Space(3f);
				bool flag22 = Prefab.Button("Headshot", 205f, 25f, new GUILayoutOption[0]);
				if (flag22)
				{
					Provider.provider.achievementsService.setAchievement("headshot");
				}
				GUILayout.Space(3f);
				bool flag23 = Prefab.Button("Sharpshooter", 205f, 25f, new GUILayoutOption[0]);
				if (flag23)
				{
					Provider.provider.achievementsService.setAchievement("sharpshooter");
				}
				GUILayout.Space(3f);
				bool flag24 = Prefab.Button("Hiking", 205f, 25f, new GUILayoutOption[0]);
				if (flag24)
				{
					Provider.provider.achievementsService.setAchievement("hiking");
				}
				GUILayout.Space(3f);
				bool flag25 = Prefab.Button("Roadtrip", 205f, 25f, new GUILayoutOption[0]);
				if (flag25)
				{
					Provider.provider.achievementsService.setAchievement("roadtrip");
				}
				GUILayout.Space(3f);
				bool flag26 = Prefab.Button("Champion", 205f, 25f, new GUILayoutOption[0]);
				if (flag26)
				{
					Provider.provider.achievementsService.setAchievement("champion");
				}
				GUILayout.Space(3f);
				bool flag27 = Prefab.Button("Fortified", 205f, 25f, new GUILayoutOption[0]);
				if (flag27)
				{
					Provider.provider.achievementsService.setAchievement("fortified");
				}
				GUILayout.Space(3f);
				bool flag28 = Prefab.Button("Welcome to Russia", 205f, 25f, new GUILayoutOption[0]);
				if (flag28)
				{
					Provider.provider.achievementsService.setAchievement("russia");
				}
				GUILayout.Space(3f);
				bool flag29 = Prefab.Button("Villain", 205f, 25f, new GUILayoutOption[0]);
				if (flag29)
				{
					Provider.provider.achievementsService.setAchievement("villain");
				}
				GUILayout.Space(3f);
				bool flag30 = Prefab.Button("Unturned", 205f, 25f, new GUILayoutOption[0]);
				if (flag30)
				{
					Provider.provider.achievementsService.setAchievement("unturned");
				}
				GUILayout.Space(3f);
				bool flag31 = Prefab.Button("Forged + Hardened", 205f, 25f, new GUILayoutOption[0]);
				if (flag31)
				{
					Provider.provider.achievementsService.setAchievement("forged");
				}
				GUILayout.Space(3f);
				bool flag32 = Prefab.Button("Soulcrystal", 205f, 25f, new GUILayoutOption[0]);
				if (flag32)
				{
					Provider.provider.achievementsService.setAchievement("soulcrystal");
				}
				GUILayout.Space(3f);
				bool flag33 = Prefab.Button("Paragon", 205f, 25f, new GUILayoutOption[0]);
				if (flag33)
				{
					Provider.provider.achievementsService.setAchievement("paragon");
				}
				GUILayout.Space(3f);
				bool flag34 = Prefab.Button("Mk. II", 205f, 25f, new GUILayoutOption[0]);
				if (flag34)
				{
					Provider.provider.achievementsService.setAchievement("mk2");
				}
				GUILayout.Space(3f);
				bool flag35 = Prefab.Button("Ensign", 205f, 25f, new GUILayoutOption[0]);
				if (flag35)
				{
					Provider.provider.achievementsService.setAchievement("ensign");
				}
				GUILayout.Space(3f);
				bool flag36 = Prefab.Button("Lieutenant", 205f, 25f, new GUILayoutOption[0]);
				if (flag36)
				{
					Provider.provider.achievementsService.setAchievement("lieutenant");
				}
				GUILayout.Space(3f);
				bool flag37 = Prefab.Button("Major", 205f, 25f, new GUILayoutOption[0]);
				if (flag37)
				{
					Provider.provider.achievementsService.setAchievement("major");
				}
				GUILayout.Space(3f);
				bool flag38 = Prefab.Button("Welcome to Hawaii", 205f, 25f, new GUILayoutOption[0]);
				if (flag38)
				{
					Provider.provider.achievementsService.setAchievement("hawaii");
				}
				GUILayout.Space(3f);
				bool flag39 = Prefab.Button("Получить все достижения", 205f, 25f, new GUILayoutOption[0]);
				if (flag39)
				{
					Provider.provider.achievementsService.setAchievement("pei");
					Provider.provider.achievementsService.setAchievement("bridge");
					Provider.provider.achievementsService.setAchievement("mastermind");
					Provider.provider.achievementsService.setAchievement("offense");
					Provider.provider.achievementsService.setAchievement("defense");
					Provider.provider.achievementsService.setAchievement("support");
					Provider.provider.achievementsService.setAchievement("experienced");
					Provider.provider.achievementsService.setAchievement("hoarder");
					Provider.provider.achievementsService.setAchievement("outdoors");
					Provider.provider.achievementsService.setAchievement("psychopath");
					Provider.provider.achievementsService.setAchievement("survivor");
					Provider.provider.achievementsService.setAchievement("berries");
					Provider.provider.achievementsService.setAchievement("accident_prone");
					Provider.provider.achievementsService.setAchievement("wheel");
					Provider.provider.achievementsService.setAchievement("yukon");
					Provider.provider.achievementsService.setAchievement("fishing");
					Provider.provider.achievementsService.setAchievement("washington");
					Provider.provider.achievementsService.setAchievement("crafting");
					Provider.provider.achievementsService.setAchievement("farming");
					Provider.provider.achievementsService.setAchievement("headshot");
					Provider.provider.achievementsService.setAchievement("sharpshooter");
					Provider.provider.achievementsService.setAchievement("hiking");
					Provider.provider.achievementsService.setAchievement("roadtrip");
					Provider.provider.achievementsService.setAchievement("champion");
					Provider.provider.achievementsService.setAchievement("fortified");
					Provider.provider.achievementsService.setAchievement("russia");
					Provider.provider.achievementsService.setAchievement("villain");
					Provider.provider.achievementsService.setAchievement("unturned");
					Provider.provider.achievementsService.setAchievement("forged");
					Provider.provider.achievementsService.setAchievement("soulcrystal");
					Provider.provider.achievementsService.setAchievement("paragon");
					Provider.provider.achievementsService.setAchievement("mk2");
					Provider.provider.achievementsService.setAchievement("ensign");
					Provider.provider.achievementsService.setAchievement("major");
					Provider.provider.achievementsService.setAchievement("lieutenant");
					Provider.provider.achievementsService.setAchievement("hawaii");
				}
				GUILayout.Space(3f);
			}, 20, Array.Empty<GUILayoutOption>());
			Rect area = new Rect(260f, 0f, 196f, 250f);
			Prefab.MenuArea(area, "Модиффикатор", delegate
			{
				bool flag = StatsTab.Selected == 0;
				bool flag2 = !flag;
				if (flag2)
				{
					string text = StatsTab.StatLabels[StatsTab.Selected];
					int num;
					Provider.provider.statisticsService.userStatisticsService.getStatistic(StatsTab.StatNames[StatsTab.Selected], out num);
					GUILayout.Label(text, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Space(4f);
					GUILayout.Label(string.Format("Текущий: {0}", num), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Space(3f);
					StatsTab.Amount = Prefab.TextField(StatsTab.Amount, "Модиффицировать: ", 50);
					GUILayout.Space(2f);
					int num2;
					bool flag3 = !int.TryParse(StatsTab.Amount, out num2);
					bool flag4 = !flag3;
					if (flag4)
					{
						bool flag5 = Prefab.Button("Принять", 75f, 25f, Array.Empty<GUILayoutOption>());
						bool flag6 = flag5;
						if (flag6)
						{
							for (int i = 1; i <= num2; i++)
							{
								Provider.provider.statisticsService.userStatisticsService.setStatistic(StatsTab.StatNames[StatsTab.Selected], num + i);
							}
						}
					}
				}
			});
		}

		// Token: 0x04000396 RID: 918
		public static string id;

		// Token: 0x04000397 RID: 919
		public static string count;

		// Token: 0x04000398 RID: 920
		public static string name = "Оружие: ";

		// Token: 0x04000399 RID: 921
		public static string labels;

		// Token: 0x0400039A RID: 922
		public static int Selected = 0;

		// Token: 0x0400039B RID: 923
		public static Vector2 ScrollPos;

		// Token: 0x0400039C RID: 924
		public static string Amount = "";

		// Token: 0x0400039D RID: 925
		public static string[] StatLabels = new string[]
		{
			"None",
			"Normal Zombie Kills",
			"Player Kills",
			"Items Found",
			"Resources Found",
			"Experience Found",
			"Mega Zombie Kills",
			"Player Deaths",
			"Animal Kills",
			"Blueprints Found",
			"Fishies Found",
			"Plants Taken",
			"Accuracy",
			"Headshots",
			"Foot Traveled",
			"Vehicle Traveled",
			"Arena Wins",
			"Buildables Taken",
			"Throwables Found"
		};

		// Token: 0x0400039E RID: 926
		public static string[] StatNames = new string[]
		{
			"None",
			"Kills_Zombies_Normal",
			"Kills_Players",
			"Found_Items",
			"Found_Resources",
			"Found_Experience",
			"Kills_Zombies_Mega",
			"Deaths_Players",
			"Kills_Animals",
			"Found_Crafts",
			"Found_Fishes",
			"Found_Plants",
			"Accuracy_Shot",
			"Headshots",
			"Travel_Foot",
			"Travel_Vehicle",
			"Arena_Wins",
			"Found_Buildables",
			"Found_Throwables"
		};
	}
}

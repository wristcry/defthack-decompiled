using System;
using System.IO;
using System.Reflection;

using DeftHack.Coroutines;
using DeftHack.Managers.Main;
using DeftHack.Managers.Submanagers;
using DeftHack.Options;
using DeftHack.Threads;
using DeftHack.Utilities;
using DeftHack.Variables.UIVariables;
using DeftHackFree;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000AB RID: 171
	public static class MoreMiscTab
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00018260 File Offset: 0x00016460
		public static void Language1()
		{
			bool flag = !MoreMiscTab.lb;
			MoreMiscTab.lb = flag;
			bool flag2 = MoreMiscTab.language(MoreMiscTab.lb) == "English";
			if (flag2)
			{
				MenuTabs.visual = "visual";
				MenuTabs.aim = "Aimbot";
				MenuTabs.weapon = "weapons";
				MenuTabs.stast = "stats";
				MenuTabs.player = "player";
				MenuTabs.skins = "skins";
				MenuTabs.misc = "misc";
				MenuTabs.option = "misc2";
				MenuTabs.ifo = "info";
				MenuTabs.colors = "colors";
				MenuTabs.hotkeys = "hotkeys";
				MenuTabs.bindtab1 = "bind";
			}
			else
			{
				MenuTabs.visual = "визуал";
				MenuTabs.aim = "аим бот";
				MenuTabs.weapon = "оружие";
				MenuTabs.stast = "стата";
				MenuTabs.player = "игроки";
				MenuTabs.skins = "скины";
				MenuTabs.misc = "прочее";
				MenuTabs.option = "опции";
				MenuTabs.ifo = "инфо";
				MenuTabs.colors = "цвета";
				MenuTabs.hotkeys = "Кнопки";
				MenuTabs.bindtab1 = "Бинды";
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001838C File Offset: 0x0001658C
		internal static string a(bool bool_0)
		{
			return ": " + (bool_0 ? "Вкл" : "Выкл");
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000183B8 File Offset: 0x000165B8
		internal static string language(bool bool_0)
		{
			return ": " + (bool_0 ? "Russian" : "English");
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000183E4 File Offset: 0x000165E4
		public static string GetRandomIpAddress()
		{
			System.Random random = new System.Random();
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				random.Next(1, 255),
				random.Next(0, 255),
				random.Next(0, 255),
				random.Next(0, 255)
			});
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0001845C File Offset: 0x0001665C
		public static void Tab()
		{
			Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "Опции", delegate
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(new GUILayoutOption[]
				{
					GUILayout.Width(230f)
				});
				GUILayout.Space(2f);
				Prefab.Toggle("Авто подбор вещей", ref ItemOptions.AutoItemPickup, 17);
				GUILayout.Space(5f);
				GUILayout.Label("Задержка: " + ItemOptions.ItemPickupDelay + "мс", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
				GUILayout.Space(2f);
				ItemOptions.ItemPickupDelay = (int)Prefab.Slider(0f, 3000f, (float)ItemOptions.ItemPickupDelay, 175);
				GUILayout.Space(5f);
				ItemUtilities.DrawFilterTab(ItemOptions.ItemFilterOptions);
				GUILayout.Label("________________", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
				GUILayout.Space(5f);
				GUILayout.Label(string.Format("Метод краша сервера: {0}", MiscOptions.SCrashMethod), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
				GUILayout.Space(2f);
				MiscOptions.SCrashMethod = (int)Prefab.Slider(1f, 3f, (float)MiscOptions.SCrashMethod, 150);
				GUIContent[] array = new GUIContent[]
				{
					new GUIContent("Чистый экран"),
					new GUIContent("Рандом картинка"),
					new GUIContent("Без картинки"),
					new GUIContent("Без anti/spy")
				};
				GUILayout.Space(5f);
				GUILayout.Label("Anti/spy метод:", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
				bool flag = Prefab.List(200f, "_SpyMethods", new GUIContent(array[MiscOptions.AntiSpyMethod].text), array, Array.Empty<GUILayoutOption>());
				bool flag2 = flag;
				if (flag2)
				{
					MiscOptions.AntiSpyMethod = DropDown.Get("_SpyMethods").ListIndex;
				}
				bool flag3 = MiscOptions.AntiSpyMethod == 1;
				bool flag4 = flag3;
				if (flag4)
				{
					GUILayout.Space(2f);
					GUILayout.Label("Anti/spy папка:", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					MiscOptions.AntiSpyPath = Prefab.TextField(MiscOptions.AntiSpyPath, "", 225);
				}
				GUILayout.Space(5f);
				Prefab.Toggle("Предупреждать при /spy", ref MiscOptions.AlertOnSpy, 17);
				GUILayout.Space(5f);
				GUILayout.Space(5f);
				bool flag5 = Prefab.Button("Мгновенный дисконнект", 200f, 25f, Array.Empty<GUILayoutOption>());
				bool flag6 = flag5;
				if (flag6)
				{
					Provider.disconnect();
				}
				GUILayout.Space(5f);
				bool flag7 = Prefab.Button("Очистить авто краш", 200f, 25f, Array.Empty<GUILayoutOption>());
				bool flag8 = flag7;
				if (flag8)
				{
					PlayerCrashThread.CrashTargets.Clear();
				}
				GUILayout.Space(5f);
				bool flag14 = Prefab.Button("Отключить чит", 200f, 25f, Array.Empty<GUILayoutOption>());
				if (flag14)
				{
					File.Delete(ConfigManager.ConfigPath);
					File.Delete(LoaderCoroutines.AssetPath);
					bool flag15 = File.Exists("df.log");
					if (flag15)
					{
						File.Delete("df.log");
					}
					PlayerCoroutines.DisableAllVisuals();
					OverrideManager.OffHook();
					UnityEngine.Object.DestroyImmediate(abc.HookObject);
				}
				GUILayout.Space(5f);
				GUILayout.EndVertical();
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			});
		}

		// Token: 0x04000383 RID: 899
		internal static bool lb;

		// Token: 0x04000384 RID: 900
		private static Vector2 Scroll;

		// Token: 0x04000385 RID: 901
		private static Vector2 Scroll1;

		// Token: 0x04000386 RID: 902
		private static string text;

		// Token: 0x04000387 RID: 903
		private static string text1;

		// Token: 0x04000388 RID: 904
		public static FieldInfo hwidfield = typeof(LocalHwid).GetField("cachedHwid", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000389 RID: 905
		public static FieldInfo ippfietd = typeof(P2PSessionState_t).GetField("m_nRemoteIP", BindingFlags.Static | BindingFlags.NonPublic);
	}
}

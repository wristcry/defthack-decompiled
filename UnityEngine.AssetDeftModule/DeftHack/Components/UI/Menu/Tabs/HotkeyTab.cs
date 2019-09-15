using System;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Components.Basic;
using DeftHack.Options;
using DeftHack.Variables;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000A3 RID: 163
	public static class HotkeyTab
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000171DC File Offset: 0x000153DC
		public static void Tab()
		{
			Prefab.ScrollView(new Rect(0f, 0f, 466f, 400f), "Горячие клавиши", ref HotkeyTab.HotkeyScroll, delegate()
			{
				foreach (KeyValuePair<string, Dictionary<string, Hotkey>> keyValuePair in HotkeyOptions.HotkeyDict)
				{
					bool isFirst = HotkeyTab.IsFirst;
					bool flag = isFirst;
					if (flag)
					{
						HotkeyTab.IsFirst = false;
						HotkeyTab.DrawSpacer(keyValuePair.Key, true);
					}
					else
					{
						HotkeyTab.DrawSpacer(keyValuePair.Key, false);
					}
					foreach (KeyValuePair<string, Hotkey> keyValuePair2 in keyValuePair.Value)
					{
						HotkeyTab.DrawButton(keyValuePair2.Value.Name, keyValuePair2.Key);
					}
				}
			}, 20, Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0001723C File Offset: 0x0001543C
		public static void DrawSpacer(string Text, bool First)
		{
			bool flag = !First;
			bool flag2 = flag;
			if (flag2)
			{
				GUILayout.Space(10f);
			}
			Prefab._TextStyle.fontStyle = FontStyle.Bold;
			GUILayout.Label(Text, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
			Prefab._TextStyle.fontStyle = FontStyle.Normal;
			GUILayout.Space(8f);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00017298 File Offset: 0x00015498
		public static void DrawButton(string Option, string Identifier)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(Option, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
			bool flag = HotkeyTab.ClickedOption == Identifier;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = Prefab.Button("Убрать", 75f, 25f, Array.Empty<GUILayoutOption>());
				bool flag4 = flag3;
				if (flag4)
				{
					HotkeyComponent.Clear();
					HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = new KeyCode[0];
					HotkeyTab.ClickedOption = "";
				}
				bool flag5 = !HotkeyComponent.StopKeys;
				bool flag6 = flag5;
				if (flag6)
				{
					bool flag7 = HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0;
					bool flag8 = flag7;
					string text;
					if (flag8)
					{
						text = string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
						{
							KeyCode keyCode = k;
							return keyCode.ToString();
						}).ToArray<string>());
					}
					else
					{
						text = "Не назначена";
					}
					Prefab.Button(text, 200f, 25f, Array.Empty<GUILayoutOption>());
				}
				else
				{
					HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = HotkeyComponent.CurrentKeys.ToArray();
					HotkeyComponent.Clear();
					Prefab.Button(string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
					{
						KeyCode keyCode = k;
						return keyCode.ToString();
					}).ToArray<string>()), 200f, 25f, Array.Empty<GUILayoutOption>());
					HotkeyTab.ClickedOption = "";
				}
			}
			else
			{
				bool flag9 = HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0;
				bool flag10 = flag9;
				string text2;
				if (flag10)
				{
					text2 = string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
					{
						KeyCode keyCode = k;
						return keyCode.ToString();
					}).ToArray<string>());
				}
				else
				{
					text2 = "Не назначена";
				}
				bool flag11 = Prefab.Button(text2, 200f, 25f, Array.Empty<GUILayoutOption>());
				bool flag12 = flag11;
				if (flag12)
				{
					HotkeyComponent.Clear();
					HotkeyTab.ClickedOption = Identifier;
					HotkeyComponent.NeedsKeys = true;
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(2f);
		}

		// Token: 0x04000366 RID: 870
		public static Vector2 HotkeyScroll;

		// Token: 0x04000367 RID: 871
		public static string ClickedOption;

		// Token: 0x04000368 RID: 872
		public static bool IsFirst = true;
	}
}

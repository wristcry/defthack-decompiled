using System;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Options.VisualOptions;
using DeftHack.Utilities;
using DeftHack.Variables.UIVariables;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000A0 RID: 160
	public static class ColorsTab
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00016AB8 File Offset: 0x00014CB8
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00002F52 File Offset: 0x00001152
		public static Color LastColorPreview1
		{
			get
			{
				return ColorsTab.LastColorPreview;
			}
			set
			{
				ColorsTab.LastColorPreview = value;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00016AD0 File Offset: 0x00014CD0
		public static void Tab()
		{
			bool flag = ColorOptions.selectedOption == null;
			bool flag2 = flag;
			if (flag2)
			{
				ColorOptions.previewselected = ColorOptions.preview;
			}
			Prefab.ScrollView(new Rect(0f, 0f, 250f, 436f), "Цвета", ref ColorsTab.ColorScroll, delegate()
			{
				GUILayout.Space(10f);
				List<KeyValuePair<string, ColorVariable>> list = ColorOptions.ColorDict.ToList<KeyValuePair<string, ColorVariable>>();
				list.Sort((KeyValuePair<string, ColorVariable> pair1, KeyValuePair<string, ColorVariable> pair2) => string.Compare(pair1.Value.name, pair2.Value.name, StringComparison.Ordinal));
				for (int i = 0; i < list.Count; i++)
				{
					ColorVariable color = ColorUtilities.getColor(list[i].Value.identity);
					bool flag3 = Prefab.ColorButton(205f, color, 25f, Array.Empty<GUILayoutOption>());
					bool flag4 = flag3;
					if (flag4)
					{
						ColorOptions.selectedOption = color.identity;
						ColorOptions.previewselected = new ColorVariable(color);
						ColorsTab.LastColorPreview1 = color.color;
					}
					GUILayout.Space(3f);
				}
				bool flag5 = Prefab.Button("Восстановить по умолчанию", 205f, 25f, Array.Empty<GUILayoutOption>());
				bool flag6 = flag5;
				if (flag6)
				{
					for (int j = 0; j < list.Count; j++)
					{
						ColorVariable color2 = ColorUtilities.getColor(list[j].Value.identity);
						color2.color = color2.origColor;
						MenuComponent.SetGUIColors();
						ColorOptions.selectedOption = null;
						ColorsTab.LastColorPreview1 = ColorOptions.preview.color;
					}
				}
				GUILayout.Space(10f);
			}, 20, Array.Empty<GUILayoutOption>());
			Rect previewRect = new Rect(255f, 0f, 211f, 120f);
			Prefab.MenuArea(previewRect, "Предпросмотр", delegate
			{
				Rect rect = new Rect(5f, 20f, previewRect.width - 10f, previewRect.height - 25f);
				Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
				Drawing.DrawRect(MenuUtilities.Inline(rect, 1), MenuComponent._OutlineBorderLightGray, null);
				Rect rect2 = MenuUtilities.Inline(rect, 2);
				Drawing.DrawRect(new Rect(rect2.x, rect2.y, rect2.width / 2f, rect2.height), ColorsTab.LastColorPreview1, null);
				Drawing.DrawRect(new Rect(rect2.x + rect2.width / 2f, rect2.y, rect2.width / 2f, rect2.height), ColorOptions.previewselected, null);
			});
			Prefab.MenuArea(new Rect(previewRect.x, previewRect.y + previewRect.height + 5f, previewRect.width, 436f - previewRect.height - 5f), ColorOptions.previewselected.name, delegate
			{
				GUILayout.BeginArea(new Rect(10f, 20f, previewRect.width - 10f, 205f));
				ColorOptions.previewselected.color.r = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.r, "R: ", 30, 0, 255));
				ColorOptions.previewselected.color.r = (int)((byte)Mathf.Round(Prefab.Slider(0f, 255f, (float)ColorOptions.previewselected.color.r, 185)));
				GUILayout.FlexibleSpace();
				ColorOptions.previewselected.color.g = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.g, "G: ", 30, 0, 255));
				ColorOptions.previewselected.color.g = (int)((byte)Mathf.Round(Prefab.Slider(0f, 255f, (float)ColorOptions.previewselected.color.g, 185)));
				GUILayout.FlexibleSpace();
				ColorOptions.previewselected.color.b = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.b, "B: ", 30, 0, 255));
				ColorOptions.previewselected.color.b = (int)((byte)Mathf.Round(Prefab.Slider(0f, 255f, (float)ColorOptions.previewselected.color.b, 185)));
				GUILayout.FlexibleSpace();
				bool flag3 = !ColorOptions.previewselected.disableAlpha;
				bool flag4 = flag3;
				if (flag4)
				{
					ColorOptions.previewselected.color.a = (int)((byte)Prefab.TextField(ColorOptions.previewselected.color.a, "A: ", 30, 0, 255));
					ColorOptions.previewselected.color.a = (int)((byte)Mathf.Round(Prefab.Slider(0f, 255f, (float)ColorOptions.previewselected.color.a, 185)));
				}
				else
				{
					Prefab.TextField(ColorOptions.previewselected.color.a, "A: ", 30, 0, 255);
					Prefab.Slider(0f, 255f, (float)ColorOptions.previewselected.color.a, 185);
				}
				GUILayout.Space(100f);
				GUILayout.EndArea();
				GUILayout.Space(160f);
				GUILayout.FlexibleSpace();
				bool flag5 = Prefab.Button("Отменить", 180f, 25f, Array.Empty<GUILayoutOption>());
				bool flag6 = flag5;
				if (flag6)
				{
					ColorOptions.selectedOption = null;
					ColorsTab.LastColorPreview1 = ColorOptions.preview.color;
				}
				GUILayout.Space(3f);
				bool flag7 = Prefab.Button("Восстановить", 180f, 25f, Array.Empty<GUILayoutOption>());
				bool flag8 = flag7;
				if (flag8)
				{
					ColorUtilities.setColor(ColorOptions.previewselected.identity, ColorOptions.previewselected.origColor);
					ColorOptions.previewselected.color = ColorOptions.previewselected.origColor;
					MenuComponent.SetGUIColors();
				}
				GUILayout.Space(3f);
				bool flag9 = Prefab.Button("Применить", 180f, 25f, Array.Empty<GUILayoutOption>());
				bool flag10 = flag9;
				if (flag10)
				{
					ColorUtilities.setColor(ColorOptions.previewselected.identity, ColorOptions.previewselected.color);
					MenuComponent.SetGUIColors();
					ColorsTab.LastColorPreview1 = ColorOptions.previewselected.color;
				}
				GUILayout.Space(30f);
			});
		}

		// Token: 0x04000360 RID: 864
		private static Vector2 ColorScroll;

		// Token: 0x04000361 RID: 865
		private static Color LastColorPreview = ColorOptions.preview.color;
	}
}

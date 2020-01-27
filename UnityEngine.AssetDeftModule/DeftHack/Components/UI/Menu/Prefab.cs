using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Misc.Serializables;
using DeftHack.Utilities;
using DeftHack.Variables.UIVariables;
using UnityEngine;

namespace DeftHack.Components.UI.Menu
{
	// Token: 0x0200009D RID: 157
	public static class Prefab
	{
		// Token: 0x0600022B RID: 555 RVA: 0x000146D0 File Offset: 0x000128D0
		[Initializer]
		public static void Initialize()
		{
			ColorUtilities.addColor(new ColorVariable("_MenuTabOff", "Меню - Неактив", new Color32(160, 160, 160, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_MenuTabOn", "Меню - Актив", new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_MenuTabHover", "Меню - Заливка", new Color32(210, 210, 210, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_TextStyleOff", "Меню надписи - Неактив", new Color32(160, 160, 160, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_TextStyleOn", "Меню надписи - Актив", new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_TextStyleHover", "Меню Надписи - Заливка", new Color32(210, 210, 210, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_HeaderStyle", "Меню - Заголовки", new Color32(210, 210, 210, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_ToggleBoxBG", "Меню - Фон элемент", new Color32(71, 70, 71, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_ButtonBG", "Меню Кнопки - Фон", new Color32(130, 130, 130, byte.MaxValue), true));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0001487C File Offset: 0x00012A7C
		public static void CheckStyles()
		{
			bool flag = Prefab._MenuTabStyle != null || !LoaderCoroutines.IsLoaded;
			bool flag2 = !flag;
			if (flag2)
			{
				Prefab._MenuTabStyle = new GUIStyle();
				Prefab._MenuTabStyle.font = MenuComponent._TabFont;
				Prefab._MenuTabStyle.fontSize = 29;
				Prefab._HeaderStyle = new GUIStyle();
				Prefab._HeaderStyle.font = MenuComponent._TabFont;
				Prefab._HeaderStyle.fontSize = 15;
				Prefab._HeaderStyle.alignment = TextAnchor.MiddleCenter;
				Prefab._WaterMarkStyle = new GUIStyle();
				Prefab._WaterMarkStyle.font = MenuComponent._TextFont;
				Prefab._WaterMarkStyle.fontSize = 15;
				Prefab._TextStyle = new GUIStyle();
				Prefab._TextStyle.font = MenuComponent._TextFont;
				Prefab._TextStyle.fontSize = 17;
				Prefab._sliderStyle = new GUIStyle();
				Prefab._sliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
				Prefab._sliderThumbStyle.fixedWidth = 7f;
				Prefab._sliderVThumbStyle = new GUIStyle(GUI.skin.verticalSliderThumb);
				Prefab._sliderVThumbStyle.fixedHeight = 7f;
				Prefab._listStyle = new GUIStyle();
				Prefab._listStyle.padding.left = (Prefab._listStyle.padding.right = (Prefab._listStyle.padding.top = (Prefab._listStyle.padding.bottom = 4)));
				Prefab._listStyle.alignment = TextAnchor.MiddleLeft;
				Prefab._listStyle.font = MenuComponent._TextFont;
				Prefab._listStyle.fontSize = 15;
				Prefab._ButtonStyle = new GUIStyle();
				Prefab._ButtonStyle.alignment = TextAnchor.MiddleLeft;
				Prefab._ButtonStyle.font = MenuComponent._TextFont;
				Prefab._ButtonStyle.fontSize = 15;
				Prefab._ButtonStyle.padding.left = (Prefab._ButtonStyle.padding.right = (Prefab._ButtonStyle.padding.top = (Prefab._ButtonStyle.padding.bottom = 4)));
				MenuUtilities.FixGUIStyleColor(Prefab._sliderStyle);
				MenuUtilities.FixGUIStyleColor(Prefab._MenuTabStyle);
				MenuUtilities.FixGUIStyleColor(Prefab._TextStyle);
				MenuComponent.SetGUIColors();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00002F1E File Offset: 0x0000111E
		internal static bool ListNoid(float v, GUIContent gUIContent, string[] array1, GUILayoutOption[] gUILayoutOption)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00014ACC File Offset: 0x00012CCC
		public static void UpdateColors()
		{
			Prefab._WaterMarkStyle.normal.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._WaterMarkStyle.onNormal.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._WaterMarkStyle.hover.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._WaterMarkStyle.onHover.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._WaterMarkStyle.active.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._WaterMarkStyle.onActive.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._WaterMarkStyle.focused.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._WaterMarkStyle.onFocused.textColor = new Color(MenuComponent.Water_rF, MenuComponent.Water_gF, MenuComponent.Water_bF);
			Prefab._MenuTabStyle.normal.textColor = ColorUtilities.getColor("_MenuTabOff");
			Prefab._MenuTabStyle.onNormal.textColor = ColorUtilities.getColor("_MenuTabOn");
			Prefab._MenuTabStyle.hover.textColor = ColorUtilities.getColor("_MenuTabHover");
			Prefab._MenuTabStyle.onHover.textColor = ColorUtilities.getColor("_MenuTabOn");
			Prefab._MenuTabStyle.active.textColor = ColorUtilities.getColor("_MenuTabOn");
			Prefab._MenuTabStyle.onActive.textColor = ColorUtilities.getColor("_MenuTabOn");
			Prefab._MenuTabStyle.focused.textColor = ColorUtilities.getColor("_MenuTabOff");
			Prefab._MenuTabStyle.onFocused.textColor = ColorUtilities.getColor("_MenuTabOff");
			Prefab._TextStyle.normal.textColor = ColorUtilities.getColor("_TextStyleOff");
			Prefab._TextStyle.onNormal.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._TextStyle.hover.textColor = ColorUtilities.getColor("_TextStyleHover");
			Prefab._TextStyle.onHover.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._TextStyle.active.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._TextStyle.onActive.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._TextStyle.focused.textColor = ColorUtilities.getColor("_TextStyleOff");
			Prefab._TextStyle.onFocused.textColor = ColorUtilities.getColor("_TextStyleOff");
			Prefab._HeaderStyle.normal.textColor = ColorUtilities.getColor("_HeaderStyle");
			Prefab._listStyle.normal.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._listStyle.onNormal.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._listStyle.hover.textColor = ColorUtilities.getColor("_OutlineBorderBlack");
			Prefab._ButtonStyle.normal.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._ButtonStyle.onNormal.textColor = ColorUtilities.getColor("_TextStyleOn");
			Prefab._ButtonStyle.hover.textColor = ColorUtilities.getColor("_OutlineBorderBlack");
			Prefab._ButtonStyle.onHover.textColor = ColorUtilities.getColor("_OutlineBorderBlack");
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(0, 0, ColorUtilities.getColor("_TextStyleHover"));
			texture2D.Apply();
			Prefab._ButtonStyle.hover.background = texture2D;
			Texture2D texture2D2 = new Texture2D(1, 1);
			texture2D2.SetPixel(0, 0, ColorUtilities.getColor("_ButtonBG"));
			texture2D2.Apply();
			Prefab._ButtonStyle.normal.background = texture2D2;
			Texture2D texture2D3 = new Texture2D(1, 1);
			texture2D3.SetPixel(0, 0, ColorUtilities.getColor("_TextStyleOn"));
			texture2D3.Apply();
			Prefab._ButtonStyle.active.background = texture2D3;
			Texture2D texture2D4 = new Texture2D(1, 1);
			texture2D4.SetPixel(0, 0, ColorUtilities.getColor("_TextStyleOn"));
			texture2D4.Apply();
			Prefab._listStyle.hover.background = texture2D4;
			Prefab._listStyle.onHover.background = texture2D4;
			Texture2D texture2D5 = new Texture2D(1, 1);
			texture2D5.SetPixel(0, 0, ColorUtilities.getColor("_ButtonBG"));
			texture2D5.Apply();
			Prefab._listStyle.normal.background = texture2D5;
			Prefab._listStyle.onNormal.background = texture2D5;
			Prefab._ToggleBoxBG = ColorUtilities.getColor("_ToggleBoxBG");
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00015038 File Offset: 0x00013238
		public static bool MenuTab(string text, ref bool state, int fontsize = 29)
		{
			bool result = false;
			bool flag = state;
			int fontSize = Prefab._MenuTabStyle.fontSize;
			Prefab._MenuTabStyle.fontSize = fontsize;
			bool flag2 = GUILayout.Toggle(flag, text.ToUpper(), Prefab._MenuTabStyle, Array.Empty<GUILayoutOption>());
			bool flag3 = flag != flag2;
			bool flag4 = flag3;
			if (flag4)
			{
				state = !state;
				result = true;
			}
			Prefab._MenuTabStyle.fontSize = fontSize;
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000150A8 File Offset: 0x000132A8
		public static bool MenuTabAbsolute(Vector2 pos, string text, ref bool state, int fontsize = 29)
		{
			bool result = false;
			bool flag = state;
			int fontSize = Prefab._MenuTabStyle.fontSize;
			Prefab._MenuTabStyle.fontSize = fontsize;
			Vector2 size = Prefab._MenuTabStyle.CalcSize(new GUIContent(text));
			Rect position = new Rect(pos, size);
			bool flag2 = GUI.Toggle(position, flag, text.ToUpper(), Prefab._MenuTabStyle);
			bool flag3 = flag != flag2;
			bool flag4 = flag3;
			if (flag4)
			{
				state = !state;
				result = true;
			}
			Prefab._MenuTabStyle.fontSize = fontSize;
			return result;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00015134 File Offset: 0x00013334
		public static void MenuArea(Rect area, string header, Action code)
		{
			Rect rect = new Rect(area.x, area.y + 5f, area.width, area.height - 5f);
			Rect rect2 = MenuUtilities.Inline(rect, 1);
			Rect position = MenuUtilities.Inline(rect2, 1);
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(rect2, MenuComponent._OutlineBorderLightGray, null);
			Drawing.DrawRect(position, MenuComponent._FillLightBlack, null);
			Vector2 vector = Prefab._HeaderStyle.CalcSize(new GUIContent(header));
			Drawing.DrawRect(new Rect(area.x + 18f, area.y, vector.x + 4f, vector.y), MenuComponent._FillLightBlack, null);
			GUI.Label(new Rect(area.x + 20f, area.y - 3f, vector.x, vector.y), header, Prefab._HeaderStyle);
			GUILayout.BeginArea(area);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(15f);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			try
			{
				code();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000152AC File Offset: 0x000134AC
		public static void SectionTabButton(string text, Action code, float space = 0f, int fontsize = 20)
		{
			bool flag = false;
			GUILayout.Space(space);
			bool flag2 = Prefab.MenuTab(text, ref flag, fontsize);
			bool flag3 = flag2;
			if (flag3)
			{
				SectionTab.CurrentSectionTab = new SectionTab(text, code);
			}
			GUILayout.Space(space);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000152E8 File Offset: 0x000134E8
		public static bool Toggle(string text, ref bool state, int fontsize = 17)
		{
			bool result = false;
			int num = 1;
			int num2 = 15;
			int fontSize = Prefab._TextStyle.fontSize;
			Prefab._TextStyle.fontSize = fontsize;
			GUILayout.Space(3f);
			Rect rect = GUILayoutUtility.GetRect(150f, 15f);
			Rect rect2 = new Rect(rect.x + (float)num, rect.y + (float)num, (float)(num2 - num * 2), (float)(num2 - num * 2));
			Rect position = MenuUtilities.Inline(rect2, 1);
			Drawing.DrawRect(rect2, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(position, Prefab._ToggleBoxBG, null);
			bool flag = GUI.Button(rect, GUIContent.none, Prefab._TextStyle);
			bool flag2 = flag;
			if (flag2)
			{
				state = !state;
				result = true;
			}
			bool flag3 = Event.current.type == EventType.Repaint;
			bool flag4 = flag3;
			if (flag4)
			{
				bool flag5 = rect.Contains(Event.current.mousePosition);
				bool isActive = flag5 && Input.GetMouseButton(0);
				Prefab._TextStyle.Draw(new Rect(rect.x + 20f, rect.y, 130f, (float)num2), text, flag5, isActive, false, false);
			}
			Prefab._TextStyle.fontSize = fontSize;
			bool flag6 = state;
			bool flag7 = flag6;
			if (flag7)
			{
				Drawing.DrawRect(position, MenuComponent._Accent2, null);
			}
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00015450 File Offset: 0x00013650
		public static void Slider(float left, float right, ref float value, int size)
		{
			GUIStyle sliderThumbStyle = Prefab._sliderThumbStyle;
			GUIStyle sliderStyle = Prefab._sliderStyle;
			float num = (sliderThumbStyle.fixedWidth != 0f) ? sliderThumbStyle.fixedWidth : ((float)sliderThumbStyle.padding.horizontal);
			value = GUILayout.HorizontalSlider(value, left, right, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, new GUILayoutOption[]
			{
				GUILayout.Width((float)size)
			});
			Rect lastRect = GUILayoutUtility.GetLastRect();
			float num2 = (lastRect.width - (float)sliderStyle.padding.horizontal - num) / (right - left);
			Rect rect = new Rect((value - left) * num2 + lastRect.x + (float)sliderStyle.padding.left, lastRect.y + (float)sliderStyle.padding.top, num, lastRect.height - (float)sliderStyle.padding.vertical);
			Drawing.DrawRect(lastRect, MenuComponent._FillLightBlack, null);
			Drawing.DrawRect(new Rect(lastRect.x, lastRect.y + lastRect.height / 2f - 2f, lastRect.width, 4f), Prefab._ToggleBoxBG, null);
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(rect, 1), Prefab._MenuTabStyle.onNormal.textColor, null);
			GUILayout.Space(5f);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000155BC File Offset: 0x000137BC
		public static float Slider(float left, float right, float value, int size)
		{
			GUIStyle sliderThumbStyle = Prefab._sliderThumbStyle;
			GUIStyle sliderStyle = Prefab._sliderStyle;
			float num = (sliderThumbStyle.fixedWidth != 0f) ? sliderThumbStyle.fixedWidth : ((float)sliderThumbStyle.padding.horizontal);
			value = GUILayout.HorizontalSlider(value, left, right, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, new GUILayoutOption[]
			{
				GUILayout.Width((float)size)
			});
			Rect lastRect = GUILayoutUtility.GetLastRect();
			float num2 = (lastRect.width - (float)sliderStyle.padding.horizontal - num) / (right - left);
			Rect rect = new Rect((value - left) * num2 + lastRect.x + (float)sliderStyle.padding.left, lastRect.y + (float)sliderStyle.padding.top, num, lastRect.height - (float)sliderStyle.padding.vertical);
			Drawing.DrawRect(lastRect, MenuComponent._FillLightBlack, null);
			Drawing.DrawRect(new Rect(lastRect.x, lastRect.y + lastRect.height / 2f - 2f, lastRect.width, 4f), Prefab._ToggleBoxBG, null);
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(rect, 1), Prefab._MenuTabStyle.onNormal.textColor, null);
			GUILayout.Space(5f);
			return value;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001572C File Offset: 0x0001392C
		public static void VerticalSlider(Rect pos, float top, float bottom, ref float value)
		{
			GUIStyle sliderVThumbStyle = Prefab._sliderVThumbStyle;
			GUIStyle sliderStyle = Prefab._sliderStyle;
			float num = (sliderVThumbStyle.fixedHeight != 0f) ? sliderVThumbStyle.fixedHeight : ((float)sliderVThumbStyle.padding.vertical);
			value = GUI.VerticalSlider(pos, value, top, bottom, Prefab._sliderStyle, GUI.skin.verticalSliderThumb);
			Rect position = pos;
			float num2 = (position.height - (float)sliderStyle.padding.vertical - num) / (bottom - top);
			Rect rect = new Rect(position.x + (float)sliderStyle.padding.left, (value - top) * num2 + position.y + (float)sliderStyle.padding.top, position.width - (float)sliderStyle.padding.horizontal, num);
			Drawing.DrawRect(position, MenuComponent._FillLightBlack, null);
			Drawing.DrawRect(new Rect(position.x + position.width / 2f - 2f, position.y, 4f, position.height), Prefab._ToggleBoxBG, null);
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(rect, 1), Prefab._MenuTabStyle.onNormal.textColor, null);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00015874 File Offset: 0x00013A74
		public static void ScrollView(Rect area, string title, ref Vector2 scrollpos, Action code, int padding = 20, params GUILayoutOption[] options)
		{
			Drawing.DrawRect(area, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(area, 1), MenuComponent._OutlineBorderLightGray, null);
			Rect rect = MenuUtilities.Inline(area, 2);
			Drawing.DrawRect(rect, MenuComponent._FillLightBlack, null);
			Color textColor = Prefab._MenuTabStyle.normal.textColor;
			int fontSize = Prefab._MenuTabStyle.fontSize;
			Prefab._MenuTabStyle.normal.textColor = Prefab._MenuTabStyle.onNormal.textColor;
			Prefab._MenuTabStyle.fontSize = 15;
			Drawing.DrawRect(new Rect(rect.x, rect.y, rect.width, Prefab._MenuTabStyle.CalcSize(new GUIContent(title)).y + 2f), MenuComponent._OutlineBorderLightGray, null);
			GUILayout.BeginArea(rect);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label(title, Prefab._MenuTabStyle, Array.Empty<GUILayoutOption>());
			Prefab._MenuTabStyle.normal.textColor = textColor;
			Prefab._MenuTabStyle.fontSize = fontSize;
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.Space(2f);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			scrollpos = GUILayout.BeginScrollView(scrollpos, false, true, Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space((float)padding);
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.MinHeight(rect.height)
			});
			try
			{
				code();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			GUILayout.EndVertical();
			Rect lastRect = GUILayoutUtility.GetLastRect();
			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();
			Rect lastRect2 = GUILayoutUtility.GetLastRect();
			GUILayout.Space(1f);
			GUILayout.EndHorizontal();
			GUILayout.Space(1f);
			Drawing.DrawRect(new Rect(lastRect2.x + lastRect2.width - 16f, lastRect2.y, 16f, lastRect2.height), MenuComponent._FillLightBlack, null);
			bool flag = lastRect.height - lastRect2.height > 0f;
			bool flag2 = flag;
			if (flag2)
			{
				Prefab.VerticalSlider(new Rect(lastRect2.x + 4f, lastRect2.y + 8f, 12f, lastRect2.height - 14f), 0f, lastRect.height - lastRect2.height, ref scrollpos.y);
			}
			GUILayout.EndArea();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00015B20 File Offset: 0x00013D20
		public static void ScrollView(Rect area, string title, ref SerializableVector2 scrollpos, Action code, int padding = 20, params GUILayoutOption[] options)
		{
			Drawing.DrawRect(area, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(area, 1), MenuComponent._OutlineBorderLightGray, null);
			Rect rect = MenuUtilities.Inline(area, 2);
			Drawing.DrawRect(rect, MenuComponent._FillLightBlack, null);
			Color textColor = Prefab._MenuTabStyle.normal.textColor;
			int fontSize = Prefab._MenuTabStyle.fontSize;
			Prefab._MenuTabStyle.normal.textColor = Prefab._MenuTabStyle.onNormal.textColor;
			Prefab._MenuTabStyle.fontSize = 15;
			Drawing.DrawRect(new Rect(rect.x, rect.y, rect.width, Prefab._MenuTabStyle.CalcSize(new GUIContent(title)).y + 2f), MenuComponent._OutlineBorderLightGray, null);
			GUILayout.BeginArea(rect);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label(title, Prefab._MenuTabStyle, Array.Empty<GUILayoutOption>());
			Prefab._MenuTabStyle.normal.textColor = textColor;
			Prefab._MenuTabStyle.fontSize = fontSize;
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.Space(2f);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			scrollpos = GUILayout.BeginScrollView(scrollpos.ToVector2(), false, true, Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space((float)padding);
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.MinHeight(rect.height)
			});
			try
			{
				code();
			}
			catch (Exception ex)
			{
			}
			GUILayout.EndVertical();
			Rect lastRect = GUILayoutUtility.GetLastRect();
			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();
			Rect lastRect2 = GUILayoutUtility.GetLastRect();
			GUILayout.Space(1f);
			GUILayout.EndHorizontal();
			GUILayout.Space(1f);
			Drawing.DrawRect(new Rect(lastRect2.x + lastRect2.width - 16f, lastRect2.y, 16f, lastRect2.height), MenuComponent._FillLightBlack, null);
			bool flag = lastRect.height - lastRect2.height > 0f;
			bool flag2 = flag;
			if (flag2)
			{
				Prefab.VerticalSlider(new Rect(lastRect2.x + 4f, lastRect2.y + 8f, 12f, lastRect2.height - 14f), 0f, lastRect.height - lastRect2.height, ref scrollpos.y);
			}
			GUILayout.EndArea();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public static bool List(float width, string identifier, GUIContent buttonContent, GUIContent[] listContent, params GUILayoutOption[] options)
		{
			Vector2 vector = Prefab._listStyle.CalcSize(buttonContent);
			List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
			list.Add(GUILayout.Height(vector.y));
			list.Add(GUILayout.Width(width));
			Rect rect = GUILayoutUtility.GetRect(width, vector.y, list.ToArray());
			DropDown dropDown = DropDown.Get(identifier);
			return Prefab.List(rect, ref dropDown.IsEnabled, ref dropDown.ListIndex, ref dropDown.ScrollView, buttonContent, listContent, "button", "box", Prefab._listStyle);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00015E58 File Offset: 0x00014058
		public static bool ListNoid(float width, GUIContent buttonContent, GUIContent[] listContent, params GUILayoutOption[] options)
		{
			Vector2 vector = Prefab._listStyle.CalcSize(buttonContent);
			List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
			list.Add(GUILayout.Height(vector.y));
			list.Add(GUILayout.Width(width));
			Rect rect = GUILayoutUtility.GetRect(width, vector.y, list.ToArray());
			DropDown dropDown = DropDown.Get(null);
			return Prefab.List(rect, ref dropDown.IsEnabled, ref dropDown.ListIndex, ref dropDown.ScrollView, buttonContent, listContent, "button", "box", Prefab._listStyle);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00015EEC File Offset: 0x000140EC
		public static bool List(string identifier, GUIContent buttonContent, GUIContent[] listContent, params GUILayoutOption[] options)
		{
			Vector2 vector = Prefab._listStyle.CalcSize(buttonContent);
			List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
			list.Add(GUILayout.Height(vector.y));
			list.Add(GUILayout.Width(vector.x + 5f));
			Rect rect = GUILayoutUtility.GetRect(vector.x + 5f, vector.y, list.ToArray());
			DropDown dropDown = DropDown.Get(identifier);
			return Prefab.List(rect, ref dropDown.IsEnabled, ref dropDown.ListIndex, ref dropDown.ScrollView, buttonContent, listContent, "button", "box", Prefab._listStyle);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00015F98 File Offset: 0x00014198
		public static bool List(Rect position, ref bool showList, ref int listEntry, ref Vector2 scrollPos, GUIContent buttonContent, GUIContent[] listContent)
		{
			return Prefab.List(position, ref showList, ref listEntry, ref scrollPos, buttonContent, listContent, "button", "box", Prefab._listStyle);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00015FD0 File Offset: 0x000141D0
		public static bool List(Rect position, ref bool showList, ref int listEntry, ref Vector2 scrollPos, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
		{
			Drawing.DrawRect(position, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(position, 1), MenuComponent._OutlineBorderDarkGray, null);
			int fontSize = Prefab._TextStyle.fontSize;
			Color textColor = Prefab._TextStyle.normal.textColor;
			Prefab._TextStyle.fontSize = 15;
			Prefab._TextStyle.normal.textColor = Prefab._TextStyle.onNormal.textColor;
			Prefab._TextStyle.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(position.x + position.height + 4f, position.y, position.width - position.height * 2f, position.height), buttonContent, Prefab._TextStyle);
			bool result = false;
			bool flag = Prefab.AbsButton(new Rect(position.x, position.y, position.height, position.height), "<", Array.Empty<GUILayoutOption>());
			bool flag2 = flag;
			if (flag2)
			{
				result = true;
				listEntry = System.Math.Max(0, listEntry - 1);
			}
			bool flag3 = Prefab.AbsButton(new Rect(position.x + position.width - position.height, position.y, position.height, position.height), ">", Array.Empty<GUILayoutOption>());
			bool flag4 = flag3;
			if (flag4)
			{
				result = true;
				listEntry = System.Math.Min(listContent.Length - 1, listEntry + 1);
			}
			Prefab._TextStyle.alignment = TextAnchor.UpperLeft;
			Prefab._TextStyle.fontSize = fontSize;
			Prefab._TextStyle.normal.textColor = textColor;
			return result;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00016180 File Offset: 0x00014380
		public static bool AbsButton(Rect area, string text, params GUILayoutOption[] options)
		{
			Drawing.DrawRect(area, MenuComponent._OutlineBorderBlack, null);
			return GUI.Button(MenuUtilities.Inline(area, 1), text, Prefab._ButtonStyle);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000161B8 File Offset: 0x000143B8
		public static bool Button(string text, float width, float height = 25f, params GUILayoutOption[] options)
		{
			List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
			list.Add(GUILayout.Height(height));
			list.Add(GUILayout.Width(width));
			Rect rect = GUILayoutUtility.GetRect(width, height, list.ToArray());
			return Prefab.AbsButton(rect, text, options);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00016204 File Offset: 0x00014404
		public static bool ColorButton(float width, ColorVariable color, float height = 25f, params GUILayoutOption[] options)
		{
			List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
			list.Add(GUILayout.Height(height));
			list.Add(GUILayout.Width(width));
			Rect rect = GUILayoutUtility.GetRect(width, height, list.ToArray());
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			Rect rect2 = new Rect(rect.x + 4f, rect.y + 4f, rect.height - 8f, rect.height - 8f);
			bool result = GUI.Button(MenuUtilities.Inline(rect, 1), "      " + color.name, Prefab._ButtonStyle);
			Drawing.DrawRect(rect2, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(rect2, 1), MenuComponent._OutlineBorderLightGray, null);
			Drawing.DrawRect(MenuUtilities.Inline(rect2, 2), color.color, null);
			return result;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000162FC File Offset: 0x000144FC
		public static string TextField(string text, string label, int width)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
			int fontSize = Prefab._TextStyle.fontSize;
			Prefab._TextStyle.fontSize = 13;
			float y = Prefab._TextStyle.CalcSize(new GUIContent("asdf")).y;
			Rect rect = GUILayoutUtility.GetRect((float)width, y);
			Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height + 1f), MenuComponent._OutlineBorderLightGray, null);
			Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height), MenuComponent._FillLightBlack, null);
			text = GUI.TextField(new Rect(rect.x + 4f, rect.y + 2f, rect.width, rect.height), text, Prefab._TextStyle);
			GUILayout.FlexibleSpace();
			Prefab._TextStyle.fontSize = fontSize;
			GUILayout.EndHorizontal();
			return text;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00016430 File Offset: 0x00014630
		public static int TextField(int text, string label, int width, int min = 0, int max = 255)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
			int fontSize = Prefab._TextStyle.fontSize;
			Prefab._TextStyle.fontSize = 13;
			float y = Prefab._TextStyle.CalcSize(new GUIContent("asdf")).y;
			Rect rect = GUILayoutUtility.GetRect((float)width, y);
			Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height + 1f), MenuComponent._OutlineBorderLightGray, null);
			Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height), MenuComponent._FillLightBlack, null);
			try
			{
				int num = int.Parse(Prefab.digitsOnly.Replace(GUI.TextField(new Rect(rect.x + 4f, rect.y + 2f, rect.width, rect.height), text.ToString(), Prefab._TextStyle), ""));
				bool flag = num >= min && num <= max;
				bool flag2 = flag;
				if (flag2)
				{
					text = num;
				}
			}
			catch (Exception exception)
			{
				DebugUtilities.LogException(exception);
			}
			GUILayout.FlexibleSpace();
			Prefab._TextStyle.fontSize = fontSize;
			GUILayout.EndHorizontal();
			return text;
		}

		// Token: 0x04000352 RID: 850
		public static GUIStyle _MenuTabStyle;

		// Token: 0x04000353 RID: 851
		public static GUIStyle _HeaderStyle;

		// Token: 0x04000354 RID: 852
		public static GUIStyle _TextStyle;

		// Token: 0x04000355 RID: 853
		public static GUIStyle _WaterMarkStyle;

		// Token: 0x04000356 RID: 854
		public static GUIStyle _sliderStyle;

		// Token: 0x04000357 RID: 855
		public static GUIStyle _sliderThumbStyle;

		// Token: 0x04000358 RID: 856
		public static GUIStyle _sliderVThumbStyle;

		// Token: 0x04000359 RID: 857
		public static GUIStyle _listStyle;

		// Token: 0x0400035A RID: 858
		public static GUIStyle _ButtonStyle;

		// Token: 0x0400035B RID: 859
		public static Color32 _ToggleBoxBG;

		// Token: 0x0400035C RID: 860
		private static int popupListHash = "PopupList".GetHashCode();

		// Token: 0x0400035D RID: 861
		public static Regex digitsOnly = new Regex("[^\\d]");
	}
}

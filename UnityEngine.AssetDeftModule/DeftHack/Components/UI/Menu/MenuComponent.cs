using System;
using DeftHack.Attributes;
using DeftHack.Managers.Main;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables.UIVariables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI.Menu
{
	// Token: 0x02000099 RID: 153
	[SpyComponent]
	[Component]
	public class MenuComponent : MonoBehaviour
	{
		// Token: 0x06000213 RID: 531 RVA: 0x000138E0 File Offset: 0x00011AE0
		[Initializer]
		public static void Initialize()
		{
			ColorUtilities.addColor(new ColorVariable("_OutlineBorderBlack", "Меню - черный контур", new Color32(0, 0, 0, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_OutlineBorderLightGray", "Меню - контур 1", new Color32(65, 65, 65, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_OutlineBorderDarkGray", "Меню -  контур 2", new Color32(65, 65, 65, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_FillLightBlack", "Меню - Фон", new Color32(MenuComponent.r, MenuComponent.g, MenuComponent.b, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_Water", "", new Color32(MenuComponent.Water_r, MenuComponent.Water_g, MenuComponent.Water_b, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_Accent1", "Меню - Акцент 1", new Color32(0, 0, 0, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_Accent2", "Меню - Акцент 2", new Color32(byte.MaxValue, 95, 0, byte.MaxValue), true));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00002EA4 File Offset: 0x000010A4
		private void Start()
		{
			MenuTabs.AddTabs();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00013A08 File Offset: 0x00011C08
		private void Update()
		{
			HotkeyUtilities.Initialize();
			bool flag = !HotkeyOptions.UnorganizedHotkeys.ContainsKey("_Menu");
			if (flag)
			{
				HotkeyUtilities.AddHotkey("Прочее", "Активация меню", "_MenuComponent", new KeyCode[]
				{
					KeyCode.F1
				});
			}
			bool flag2 = (HotkeyOptions.UnorganizedHotkeys["_MenuComponent"].Keys.Length == 0 && Input.GetKeyDown(MenuComponent.MenuKey)) || HotkeyUtilities.IsHotkeyDown("_MenuComponent");
			if (flag2)
			{
				MenuComponent.IsInMenu = !MenuComponent.IsInMenu;
				bool isInMenu = MenuComponent.IsInMenu;
				if (isInMenu)
				{
					SectionTab.CurrentSectionTab = null;
				}
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00013AAC File Offset: 0x00011CAC
		private void OnGUI()
		{
			GUI.contentColor = Color.magenta;
			GUI.Label(new Rect((float)Screen.width - 437f, 12f, 427f, 21f), "Owned by OwO | yanderehook owns you and all | vk.com/yanderehook");
			Language lang = MiscOptions.lang;
			if (lang != Language.Russian)
			{
				if (lang == Language.En)
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
			Prefab.CheckStyles();
			if (MenuComponent.IsInMenu && MenuComponent._LogoTexLarge != null)
			{
				if (this._cursorTexture == null)
				{
					this._cursorTexture = (Resources.Load("UI/Cursor") as Texture);
				}
				GUI.depth = -1;
				MenuComponent.MenuRect = GUI.Window(0, MenuComponent.MenuRect, new GUI.WindowFunction(this.DoMenu), "DeftHack");
				GUI.depth = -2;
				this._cursor.x = Input.mousePosition.x;
				this._cursor.y = (float)Screen.height - Input.mousePosition.y;
				GUI.DrawTexture(this._cursor, this._cursorTexture);
				Cursor.lockState = CursorLockMode.None;
				if (PlayerUI.window != null)
				{
					PlayerUI.window.showCursor = true;
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00013CBC File Offset: 0x00011EBC
		private void DoMenu(int id)
		{
			bool flag = SectionTab.CurrentSectionTab == null;
			bool flag2 = flag;
			if (flag2)
			{
				this.DoBorder();
				this.DoTabs();
				this.DrawTabs();
				this.DoConfigButtons();
			}
			else
			{
				this.DoSectionTab();
			}
			GUI.DragWindow(new Rect(0f, 0f, MenuComponent.MenuRect.width, 25f));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00013D28 File Offset: 0x00011F28
		private void DoBorder()
		{
			Rect rect = new Rect(0f, 0f, MenuComponent.MenuRect.width, MenuComponent.MenuRect.height);
			Rect rect2 = MenuUtilities.Inline(rect, 1);
			Rect rect3 = MenuUtilities.Inline(rect2, 1);
			Rect rect4 = MenuUtilities.Inline(rect3, 3);
			Rect position = MenuUtilities.Inline(rect4, 1);
			Rect position2 = new Rect(position.x + 2f, position.y + 2f, position.width - 4f, 2f);
			Rect position3 = new Rect(position.x + 2f, position.y + 4f, position.width - 4f, 2f);
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(rect2, MenuComponent._OutlineBorderLightGray, null);
			Drawing.DrawRect(rect3, MenuComponent._OutlineBorderDarkGray, null);
			Drawing.DrawRect(rect4, MenuComponent._OutlineBorderLightGray, null);
			Drawing.DrawRect(position, MenuComponent._FillLightBlack, null);
			Drawing.DrawRect(position2, MenuComponent._Accent1, null);
			Drawing.DrawRect(position3, MenuComponent._Accent2, null);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00013E64 File Offset: 0x00012064
		private void DoTabs()
		{
			GUILayout.BeginArea(new Rect(15f, 25f, 130f, 325f));
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			for (int i = 0; i < MenuTabOption.tabs[this._pIndex].Count; i++)
			{
				bool flag = Prefab.MenuTab(MenuTabOption.tabs[this._pIndex][i].name, ref MenuTabOption.tabs[this._pIndex][i].enabled, 29);
				bool flag2 = flag;
				if (flag2)
				{
					MenuTabOption.CurrentTab = (MenuTabOption.tabs[this._pIndex][i].enabled ? MenuTabOption.tabs[this._pIndex][i] : null);
				}
				GUILayout.Space(-11f);
				bool flag3 = MenuTabOption.tabs[this._pIndex][i] != MenuTabOption.CurrentTab;
				bool flag4 = flag3;
				if (flag4)
				{
					MenuTabOption.tabs[this._pIndex][i].enabled = false;
				}
			}
			GUILayout.Space(20f);
			GUILayout.EndVertical();
			bool flag5 = false;
			bool flag6 = Prefab.MenuTabAbsolute(new Vector2(0f, 292f), "пред", ref flag5, 19) && this._pIndex > 0;
			bool flag7 = flag6;
			if (flag7)
			{
				this._pIndex--;
			}
			bool flag8 = Prefab.MenuTabAbsolute(new Vector2(76f, 292f), "след", ref flag5, 19) && this._pIndex < MenuTabOption.tabs.Length - 1;
			bool flag9 = flag8;
			if (flag9)
			{
				this._pIndex++;
			}
			GUILayout.EndArea();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001402C File Offset: 0x0001222C
		private void DrawTabs()
		{
			GUILayout.BeginArea(new Rect(160f, 25f, 466f, 436f));
			bool flag = MenuTabOption.CurrentTab != null;
			bool flag2 = flag;
			if (flag2)
			{
				MenuTabOption.CurrentTab.tab();
			}
			else
			{
				MenuComponent.LogoTab();
			}
			GUILayout.EndArea();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001408C File Offset: 0x0001228C
		private void DoSectionTab()
		{
			bool flag = SectionTab.CurrentSectionTab != null;
			bool flag2 = flag;
			if (flag2)
			{
				this.DoBorder();
				Prefab.MenuArea(new Rect(10f, 20f, MenuComponent.MenuRect.width - 20f, MenuComponent.MenuRect.height - 20f - 10f), SectionTab.CurrentSectionTab.name.ToUpper(), SectionTab.CurrentSectionTab.code);
				bool flag3 = false;
				bool flag4 = Prefab.MenuTabAbsolute(new Vector2(17f, 428f), "Назад", ref flag3, 19);
				bool flag5 = flag4;
				if (flag5)
				{
					SectionTab.CurrentSectionTab = null;
				}
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00014138 File Offset: 0x00012338
		private void DoConfigButtons()
		{
			Prefab.MenuArea(new Rect(18f, 370f, 125f, 91f), "КОНФИГ", delegate
			{
				GUILayout.Space(2f);
				if (Prefab.Button("Сохранить", 90f, 25f, Array.Empty<GUILayoutOption>()))
				{
					ConfigManager.SaveConfig(ConfigManager.CollectConfig());
				}
				GUILayout.Space(2f);
				if (Prefab.Button("Загрузить", 90f, 25f, Array.Empty<GUILayoutOption>()))
				{
					ConfigManager.Init();
					MenuComponent.SetGUIColors();
					SkinsUtilities.ApplyFromConfig();
				}
				GUILayout.Label("owned", Prefab._WaterMarkStyle, new GUILayoutOption[0]);
			});
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00002EAD File Offset: 0x000010AD
		private static void LogoTab()
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00014188 File Offset: 0x00012388
		public static void UpdateColors()
		{
			MenuComponent._OutlineBorderBlack = ColorUtilities.getColor("_OutlineBorderBlack");
			MenuComponent._OutlineBorderLightGray = ColorUtilities.getColor("_OutlineBorderLightGray");
			MenuComponent._OutlineBorderDarkGray = ColorUtilities.getColor("_OutlineBorderDarkGray");
			MenuComponent._FillLightBlack = ColorUtilities.getColor("_FillLightBlack");
			MenuComponent._Water = ColorUtilities.getColor("_Water");
			MenuComponent._Accent1 = ColorUtilities.getColor("_Accent1");
			MenuComponent._Accent2 = ColorUtilities.getColor("_Accent2");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00002EB0 File Offset: 0x000010B0
		public static void SetGUIColors()
		{
			MenuComponent.UpdateColors();
			Prefab.UpdateColors();
		}

		// Token: 0x04000328 RID: 808
		private string appdata = Environment.ExpandEnvironmentVariables("%appdata%");

		// Token: 0x04000329 RID: 809
		public static Font _TabFont;

		// Token: 0x0400032A RID: 810
		public static Font _TextFont;

		// Token: 0x0400032B RID: 811
		public static Texture2D _LogoTexLarge;

		// Token: 0x0400032C RID: 812
		public static bool IsInMenu;

		// Token: 0x0400032D RID: 813
		public static KeyCode MenuKey = KeyCode.F1;

		// Token: 0x0400032E RID: 814
		public static Rect MenuRect = new Rect(29f, 29f, 640f, 480f);

		// Token: 0x0400032F RID: 815
		public static Color32 _OutlineBorderBlack;

		// Token: 0x04000330 RID: 816
		public static Color32 _OutlineBorderLightGray;

		// Token: 0x04000331 RID: 817
		public static byte r = 65;

		// Token: 0x04000332 RID: 818
		public static byte g = 65;

		// Token: 0x04000333 RID: 819
		public static byte b = 65;

		// Token: 0x04000334 RID: 820
		public static byte Water_r = (byte)(MenuComponent.r - 9);

		// Token: 0x04000335 RID: 821
		public static byte Water_g = (byte)(MenuComponent.g - 9);

		// Token: 0x04000336 RID: 822
		public static byte Water_b = (byte)(MenuComponent.b - 5);

		// Token: 0x04000337 RID: 823
		public static float Water_rF = (float)(MenuComponent.r - 9);

		// Token: 0x04000338 RID: 824
		public static float Water_gF = (float)(MenuComponent.g - 9);

		// Token: 0x04000339 RID: 825
		public static float Water_bF = (float)(MenuComponent.b - 5);

		// Token: 0x0400033A RID: 826
		public static Color32 _Water;

		// Token: 0x0400033B RID: 827
		public static Color32 _OutlineBorderDarkGray;

		// Token: 0x0400033C RID: 828
		public static Color32 _FillLightBlack;

		// Token: 0x0400033D RID: 829
		public static Color32 _Accent1;

		// Token: 0x0400033E RID: 830
		public static Color32 _Accent2;

		// Token: 0x0400033F RID: 831
		private Rect _cursor = new Rect(0f, 0f, 20f, 20f);

		// Token: 0x04000340 RID: 832
		private Texture _cursorTexture;

		// Token: 0x04000341 RID: 833
		private int _pIndex = 0;
	}
}

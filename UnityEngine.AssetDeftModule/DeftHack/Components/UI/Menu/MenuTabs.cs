using System;
using System.Net;
using DeftHack.Components.UI.Menu.Tabs;
using DeftHack.Variables.UIVariables;
using DeftHackFree;
using DeftHackFree.DebugTab;

namespace DeftHack.Components.UI.Menu
{
	// Token: 0x0200009C RID: 156
	public static class MenuTabs
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000144A8 File Offset: 0x000126A8
		public static void AddTabs()
		{
			MenuTabOption.Add(new MenuTabOption(MenuTabs.visual, new MenuTabOption.MenuTab(VisualsTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.aim, new MenuTabOption.MenuTab(AimbotTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.weapon, new MenuTabOption.MenuTab(WeaponsTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.stast, new MenuTabOption.MenuTab(StatsTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.player, new MenuTabOption.MenuTab(PlayersTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.skins, new MenuTabOption.MenuTab(SkinsTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.misc, new MenuTabOption.MenuTab(MiscTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.option, new MenuTabOption.MenuTab(MoreMiscTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.ifo, new MenuTabOption.MenuTab(InfoTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.colors, new MenuTabOption.MenuTab(ColorsTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.hotkeys, new MenuTabOption.MenuTab(HotkeyTab.Tab)));
			MenuTabOption.Add(new MenuTabOption(MenuTabs.bindtab1, new MenuTabOption.MenuTab(BindTab.Tab)));
			bool flag = new WebClient().DownloadString("http://defthack.ru/_DeftLoader9/debugfuncunturned.txt").Trim() == "1";
			if (flag)
			{
				MenuTabOption.Add(new MenuTabOption("Debug", new MenuTabOption.MenuTab(DebugTab.Tab)));
			}
		}

		// Token: 0x04000346 RID: 838
		public static string visual = "визуал";

		// Token: 0x04000347 RID: 839
		public static string aim = "аим бот";

		// Token: 0x04000348 RID: 840
		public static string weapon = "оружие";

		// Token: 0x04000349 RID: 841
		public static string stast = "стата";

		// Token: 0x0400034A RID: 842
		public static string player = "игроки";

		// Token: 0x0400034B RID: 843
		public static string skins = "скины";

		// Token: 0x0400034C RID: 844
		public static string misc = "прочее";

		// Token: 0x0400034D RID: 845
		public static string option = "опции";

		// Token: 0x0400034E RID: 846
		public static string ifo = "инфо";

		// Token: 0x0400034F RID: 847
		public static string colors = "цвета";

		// Token: 0x04000350 RID: 848
		public static string hotkeys = "Кнопки";

		// Token: 0x04000351 RID: 849
		public static string bindtab1 = "Бинды";
	}
}

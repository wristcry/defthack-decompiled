using System;
using System.Collections.Generic;
using System.Linq;

namespace DeftHack.Variables.UIVariables
{
	// Token: 0x0200000B RID: 11
	public class MenuTabOption
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00003840 File Offset: 0x00001A40
		public static void Add(MenuTabOption tab)
		{
			bool flag = !MenuTabOption.Contains(tab);
			bool flag2 = flag;
			if (flag2)
			{
				MenuTabOption.tabs[MenuTabOption.cPageIndex].Add(tab);
				tab.page = MenuTabOption.cPageIndex;
				MenuTabOption.cTabIndex++;
				bool flag3 = MenuTabOption.cTabIndex % 9 == 0;
				bool flag4 = flag3;
				if (flag4)
				{
					MenuTabOption.cTabIndex = 0;
					MenuTabOption.cPageIndex++;
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000038B0 File Offset: 0x00001AB0
		public static bool Contains(MenuTabOption tab)
		{
			bool result = false;
			foreach (MenuTabOption menuTabOption in MenuTabOption.tabs.SelectMany((List<MenuTabOption> t) => t))
			{
				bool flag = tab.name == menuTabOption.name;
				bool flag2 = flag;
				if (flag2)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000225B File Offset: 0x0000045B
		public MenuTabOption(string name, MenuTabOption.MenuTab tab)
		{
			this.tab = tab;
			this.name = name;
		}

		// Token: 0x04000029 RID: 41
		public MenuTabOption.MenuTab tab;

		// Token: 0x0400002A RID: 42
		public string name;

		// Token: 0x0400002B RID: 43
		public bool enabled = false;

		// Token: 0x0400002C RID: 44
		public static MenuTabOption CurrentTab;

		// Token: 0x0400002D RID: 45
		public int page;

		// Token: 0x0400002E RID: 46
		private static int cTabIndex = 0;

		// Token: 0x0400002F RID: 47
		private static int cPageIndex = 0;

		// Token: 0x04000030 RID: 48
		public static List<MenuTabOption>[] tabs = new List<MenuTabOption>[]
		{
			new List<MenuTabOption>(),
			new List<MenuTabOption>()
		};

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x0600002A RID: 42
		public delegate void MenuTab();
	}
}

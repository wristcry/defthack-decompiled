using System;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000A5 RID: 165
	public class InfoTab
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00017604 File Offset: 0x00015804
		public static void Tab()
		{
			Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "ИНФО", delegate
			{
                GUILayout.Label("Next time use something better than Enigma protector :^)", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                GUILayout.Label("Cracked by OwO | vk.com/yanderehook", Prefab._TextStyle, new GUILayoutOption[0]);
				GUILayout.Space(2f);
                GUILayout.Label("hi yougame.bz ;)", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                GUILayout.Label("hi yanderehook.pw ;)", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(4f);
				if (Provider.isConnected)
				{
					GUILayout.Label("Ваш SteamID64: ", Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Space(2f);
					GUILayout.TextField(string.Format("{0}", Provider.user), Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Space(8f);
					GUILayout.Label("Данные о сервере: ", Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Space(2f);
					GUILayout.TextField(string.Format("{0}:{1}", Parser.getIPFromUInt32(Provider.currentServerInfo.ip), Provider.currentServerInfo.port), Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Space(4f);
				}
				GUILayout.Label("Пожелания и жалобы по читу сюда:", Prefab._TextStyle, new GUILayoutOption[0]);
				GUILayout.TextField("http://vk.me/beyondcheat (owned)", Prefab._TextStyle, new GUILayoutOption[0]);
				GUILayout.Space(4f);
				if (Prefab.Button("OwO <3", 200f, 25f, new GUILayoutOption[0]))
				{
					Application.OpenURL("https://vk.com/yanderehook");
				}
			});
		}

		// Token: 0x0400036E RID: 878
		public static string ipjfserver = "81.88.215.148";

		// Token: 0x0400036F RID: 879
		public static string validsteam = "";

		// Token: 0x04000370 RID: 880
		public static string youst = string.Format("{0}", Provider.user);
	}
}

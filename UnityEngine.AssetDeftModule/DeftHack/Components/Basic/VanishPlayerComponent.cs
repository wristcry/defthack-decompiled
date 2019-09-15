using System;
using DeftHack.Attributes;
using DeftHack.Components.UI.Menu;
using DeftHack.Options.VisualOptions;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000C8 RID: 200
	[Component]
	public class VanishPlayerComponent : MonoBehaviour
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00003446 File Offset: 0x00001646
		[OnSpy]
		public static void Disable()
		{
			VanishPlayerComponent.WasEnabled = ESPOptions.ShowVanishPlayers;
			ESPOptions.ShowVanishPlayers = false;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00003459 File Offset: 0x00001659
		[OffSpy]
		public static void Enable()
		{
			ESPOptions.ShowVanishPlayers = VanishPlayerComponent.WasEnabled;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001E16C File Offset: 0x0001C36C
		private void OnGUI()
		{
			bool showVanishPlayers = ESPOptions.ShowVanishPlayers;
			bool flag = showVanishPlayers;
			if (flag)
			{
				GUI.color = new Color(1f, 1f, 1f, 0f);
				VanishPlayerComponent.vew = GUILayout.Window(350, VanishPlayerComponent.vew, new GUI.WindowFunction(this.PlayersMenu), "Игроки в вашине", Array.Empty<GUILayoutOption>());
				GUI.color = Color.white;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001E1DC File Offset: 0x0001C3DC
		private void PlayersMenu(int windowID)
		{
			Drawing.DrawRect(new Rect(0f, 0f, VanishPlayerComponent.vew.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 20f, VanishPlayerComponent.vew.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 25f, VanishPlayerComponent.vew.width, VanishPlayerComponent.vew.height + 25f), new Color32(64, 64, 64, byte.MaxValue), null);
			GUILayout.Space(-19f);
			GUILayout.Label("Vanish Players", Array.Empty<GUILayoutOption>());
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				bool flag = Vector3.Distance(steamPlayer.player.transform.position, Vector3.zero) < 10f;
				bool flag2 = flag;
				if (flag2)
				{
					GUILayout.Label(steamPlayer.playerID.characterName, Array.Empty<GUILayoutOption>());
				}
			}
			GUI.DragWindow();
		}

		// Token: 0x0400041C RID: 1052
		private static bool WasEnabled;

		// Token: 0x0400041D RID: 1053
		public static Rect vew = new Rect(1075f, 10f, 200f, 300f);
	}
}

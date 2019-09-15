using System;
using DeftHack.Attributes;
using DeftHack.Components.UI.Menu;
using DeftHack.Options.VisualOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000C1 RID: 193
	[Component]
	public class RadarComponent : MonoBehaviour
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00003307 File Offset: 0x00001507
		[OnSpy]
		public static void Disable()
		{
			RadarComponent.WasEnabled = RadarOptions.Enabled;
			RadarOptions.Enabled = false;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000331A File Offset: 0x0000151A
		[OffSpy]
		public static void Enable()
		{
			RadarOptions.Enabled = RadarComponent.WasEnabled;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001D234 File Offset: 0x0001B434
		private void OnGUI()
		{
			bool flag = RadarOptions.Enabled && Provider.isConnected && !Provider.isLoading;
			bool flag2 = flag;
			if (flag2)
			{
				RadarComponent.vew.width = (RadarComponent.vew.height = RadarOptions.RadarSize + 10f);
				GUI.color = new Color(1f, 1f, 1f, 0f);
				RadarComponent.veww = GUILayout.Window(345, RadarComponent.vew, new GUI.WindowFunction(this.RadarMenu), "Radar", Array.Empty<GUILayoutOption>());
				RadarComponent.vew.x = RadarComponent.veww.x;
				RadarComponent.vew.y = RadarComponent.veww.y;
				GUI.color = Color.white;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001D308 File Offset: 0x0001B508
		private void RadarMenu(int windowID)
		{
			Drawing.DrawRect(new Rect(0f, 0f, RadarComponent.vew.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 20f, RadarComponent.vew.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 25f, RadarComponent.vew.width, RadarComponent.vew.height + 25f), new Color32(64, 64, 64, byte.MaxValue), null);
			GUILayout.Space(-19f);
			GUILayout.Label("Radar", Array.Empty<GUILayoutOption>());
			Vector2 vector = new Vector2(RadarComponent.vew.width / 2f, (RadarComponent.vew.height + 25f) / 2f);
			RadarComponent.radarcenter = new Vector2(RadarComponent.vew.width / 2f, (RadarComponent.vew.height + 25f) / 2f);
			Vector2 vector2 = RadarComponent.GameToRadarPosition(Player.player.transform.position);
			bool trackPlayer = RadarOptions.TrackPlayer;
			bool flag = trackPlayer;
			if (flag)
			{
				RadarComponent.radarcenter.x = RadarComponent.radarcenter.x - vector2.x;
				RadarComponent.radarcenter.y = RadarComponent.radarcenter.y + vector2.y;
			}
			Drawing.DrawRect(new Rect(vector.x, 25f, 1f, RadarComponent.vew.height), Color.gray, null);
			Drawing.DrawRect(new Rect(0f, vector.y, RadarComponent.vew.width, 1f), Color.gray, null);
			this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector2.x, RadarComponent.radarcenter.y - vector2.y), Color.black, 4f);
			this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector2.x, RadarComponent.radarcenter.y - vector2.y), Color.white, 3f);
			bool showVehicles = RadarOptions.ShowVehicles;
			bool flag2 = showVehicles;
			if (flag2)
			{
				foreach (InteractableVehicle interactableVehicle in VehicleManager.vehicles)
				{
					bool showVehiclesUnlocked = RadarOptions.ShowVehiclesUnlocked;
					bool flag3 = showVehiclesUnlocked;
					if (flag3)
					{
						bool flag4 = !interactableVehicle.isLocked;
						bool flag5 = flag4;
						if (flag5)
						{
							Vector2 vector3 = RadarComponent.GameToRadarPosition(interactableVehicle.transform.position);
							this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector3.x, RadarComponent.radarcenter.y - vector3.y), Color.black, 3f);
							this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector3.x, RadarComponent.radarcenter.y - vector3.y), ColorUtilities.getColor("_Vehicles"), 2f);
						}
					}
					else
					{
						Vector2 vector4 = RadarComponent.GameToRadarPosition(interactableVehicle.transform.position);
						this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector4.x, RadarComponent.radarcenter.y - vector4.y), Color.black, 3f);
						this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector4.x, RadarComponent.radarcenter.y - vector4.y), ColorUtilities.getColor("_Vehicles"), 2f);
					}
				}
			}
			bool showPlayers = RadarOptions.ShowPlayers;
			bool flag6 = showPlayers;
			if (flag6)
			{
				foreach (SteamPlayer steamPlayer in Provider.clients)
				{
					bool flag7 = steamPlayer.player != OptimizationVariables.MainPlayer;
					bool flag8 = flag7;
					if (flag8)
					{
						Vector2 vector5 = RadarComponent.GameToRadarPosition(steamPlayer.player.transform.position);
						this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector5.x, RadarComponent.radarcenter.y - vector5.y), Color.black, 3f);
						this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector5.x, RadarComponent.radarcenter.y - vector5.y), ColorUtilities.getColor("_Players"), 2f);
					}
				}
			}
			bool flag9 = MiscComponent.LastDeath != new Vector3(0f, 0f, 0f);
			bool flag10 = flag9;
			if (flag10)
			{
				Vector2 vector6 = RadarComponent.GameToRadarPosition(MiscComponent.LastDeath);
				this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector6.x, RadarComponent.radarcenter.y - vector6.y), Color.black, 4f);
				this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector6.x, RadarComponent.radarcenter.y - vector6.y), Color.grey, 3f);
			}
			GUI.DragWindow();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00003327 File Offset: 0x00001527
		private void DrawRadarDot(Vector2 pos, Color color, float size = 2f)
		{
			Drawing.DrawRect(new Rect(pos.x - size, pos.y - size, size * 2f, size * 2f), color, null);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public static Vector2 GameToRadarPosition(Vector3 pos)
		{
			Vector2 result;
			result.x = pos.x / ((float)Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
			result.y = pos.z / ((float)Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
			return result;
		}

		// Token: 0x040003F0 RID: 1008
		public static Rect veww;

		// Token: 0x040003F1 RID: 1009
		public static Rect vew = new Rect((float)Screen.width - RadarOptions.RadarSize - 20f, 10f, RadarOptions.RadarSize + 10f, RadarOptions.RadarSize + 10f);

		// Token: 0x040003F2 RID: 1010
		public static Vector2 radarcenter;

		// Token: 0x040003F3 RID: 1011
		public static bool WasEnabled;
	}
}

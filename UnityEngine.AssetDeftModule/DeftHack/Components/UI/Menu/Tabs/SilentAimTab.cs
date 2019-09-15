using System;
using DeftHack.Options.AimOptions;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000AF RID: 175
	public class SilentAimTab
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00018EC0 File Offset: 0x000170C0
		public static void Tab()
		{
			Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "SILENT AIM", delegate
			{
				GUILayout.Space(2f);
				Prefab.Toggle("Enabled", ref RaycastOptions.Enabled, 17);
				GUILayout.Space(10f);
				bool enabled = RaycastOptions.Enabled;
				bool flag = enabled;
				if (flag)
				{
					Prefab.Toggle("Sphere position prediction", ref SphereOptions.SpherePrediction, 17);
					GUILayout.Space(5f);
					bool flag2 = !SphereOptions.SpherePrediction;
					bool flag3 = flag2;
					if (flag3)
					{
						GUILayout.Label("Sphere Radius: " + Math.Round((double)SphereOptions.SphereRadius, 2) + "m", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						Prefab.Slider(0f, 16f, ref SphereOptions.SphereRadius, 200);
					}
					GUILayout.Label("Recursion Level: " + SphereOptions.RecursionLevel, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					SphereOptions.RecursionLevel = (int)Prefab.Slider(0f, 4f, (float)SphereOptions.RecursionLevel, 200);
					GUILayout.Space(10f);
					string[] array = new string[]
					{
						"Players",
						"Zombies",
						"Sentries",
						"Beds",
						"Claim Flags",
						"Storage",
						"Vehicles"
					};
					GUILayout.Space(15f);
					Prefab.Toggle("Select Player", ref RaycastOptions.EnablePlayerSelection, 17);
					bool enablePlayerSelection = RaycastOptions.EnablePlayerSelection;
					bool flag4 = enablePlayerSelection;
					if (flag4)
					{
						GUILayout.Space(2f);
						GUILayout.Label("Selection FOV: " + RaycastOptions.SelectedFOV, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						RaycastOptions.SelectedFOV = Prefab.Slider(1f, 180f, RaycastOptions.SelectedFOV, 200);
						Prefab.Toggle("Only Shoot Selected", ref RaycastOptions.OnlyShootAtSelectedPlayer, 17);
					}
					GUILayout.Space(2f);
					Prefab.Toggle("Use FOV", ref RaycastOptions.SilentAimUseFOV, 17);
					bool silentAimUseFOV = RaycastOptions.SilentAimUseFOV;
					bool flag5 = silentAimUseFOV;
					if (flag5)
					{
						GUILayout.Space(2f);
						GUILayout.Label("Aim FOV: " + RaycastOptions.SilentAimFOV, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						RaycastOptions.SilentAimFOV = Prefab.Slider(1f, 180f, RaycastOptions.SilentAimFOV, 200);
					}
				}
			});
		}
	}
}

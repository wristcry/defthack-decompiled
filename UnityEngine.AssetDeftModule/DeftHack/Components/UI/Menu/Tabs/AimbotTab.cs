using System;
using DeftHack.Misc.Enums;
using DeftHack.Options.AimOptions;
using DeftHack.Variables.UIVariables;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x0200009E RID: 158
	public static class AimbotTab
	{
		// Token: 0x06000244 RID: 580 RVA: 0x000165C0 File Offset: 0x000147C0
		public static void Tab()
		{
			Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "АИМ", delegate
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(new GUILayoutOption[]
				{
					GUILayout.Width(230f)
				});
				GUILayout.Space(2f);
				Prefab.Toggle("Тихий АИМ", ref RaycastOptions.Enabled, 17);
				GUILayout.Space(10f);
				bool enabled = RaycastOptions.Enabled;
				bool flag = enabled;
				if (flag)
				{
					Prefab.Toggle("Авто радиус сферы", ref SphereOptions.SpherePrediction, 17);
					GUILayout.Space(5f);
					bool flag2 = !SphereOptions.SpherePrediction;
					bool flag3 = flag2;
					if (flag3)
					{
						GUILayout.Label("Радиус сферы: " + System.Math.Round((double)SphereOptions.SphereRadius, 2) + "m", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						Prefab.Slider(0f, 16f, ref SphereOptions.SphereRadius, 200);
					}
					GUILayout.Space(5f);
					GUIContent[] array = new GUIContent[]
					{
						new GUIContent("Игроки"),
						new GUIContent("Зомби"),
						new GUIContent("Турели"),
						new GUIContent("Кровати"),
						new GUIContent("Клейм флаг"),
						new GUIContent("Ящики"),
						new GUIContent("Транспорт")
					};
					Prefab.Toggle("Не стрелять через стены", ref RaycastOptions.NoShootthroughthewalls, 17);
					GUILayout.Space(2f);
					Prefab.Toggle("Использовать FOV", ref RaycastOptions.SilentAimUseFOV, 17);
					bool silentAimUseFOV = RaycastOptions.SilentAimUseFOV;
					bool flag4 = silentAimUseFOV;
					if (flag4)
					{
						Prefab.Toggle("Отображать FOV", ref RaycastOptions.ShowSilentAimUseFOV, 17);
						GUILayout.Space(2f);
						GUILayout.Label("FOV: " + RaycastOptions.SilentAimFOV, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						RaycastOptions.SilentAimFOV = Prefab.Slider(1f, 180f, RaycastOptions.SilentAimFOV, 200);
						bool flag5 = RaycastOptions.SilentAimFOV == 1f;
						if (flag5)
						{
							RaycastOptions.SilentAimFOV = 2f;
						}
					}
					else
					{
						RaycastOptions.ShowSilentAimUseFOV = false;
					}
					bool flag6 = Prefab.List(200f, "_TargetPriority", new GUIContent("Приоритет: " + array[(int)RaycastOptions.Target].text), array, Array.Empty<GUILayoutOption>());
					if (flag6)
					{
						RaycastOptions.Target = (TargetPriority)DropDown.Get("_TargetPriority").ListIndex;
					}
					GUILayout.Space(5f);
				}
				GUILayout.EndVertical();
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				Prefab.Toggle("АИМ", ref AimbotOptions.Enabled, 17);
				Prefab.Toggle("Нет падения пули", ref AimbotOptions.NoAimbotDrop, 17);
				Prefab.Toggle("Дистанция по оружию", ref AimbotOptions.UseGunDistance, 17);
				Prefab.Toggle("Плавность", ref AimbotOptions.Smooth, 17);
				Prefab.Toggle("По кнопке(F)", ref AimbotOptions.OnKey, 17);
				GUILayout.Space(3f);
				bool smooth = AimbotOptions.Smooth;
				bool flag7 = smooth;
				if (flag7)
				{
					GUILayout.Label("Скорость аима: " + AimbotOptions.AimSpeed, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					AimbotOptions.AimSpeed = (float)((int)Prefab.Slider(1f, AimbotOptions.MaxSpeed, AimbotOptions.AimSpeed, 200));
				}
				Prefab.Toggle("Использовать FOV", ref AimbotOptions.UseFovAim, 17);
				bool useFovAim = AimbotOptions.UseFovAim;
				if (useFovAim)
				{
					Prefab.Toggle("Отображать FOV", ref RaycastOptions.ShowAimUseFOV, 17);
					AimbotOptions.TargetMode = TargetMode.FOV;
					GUILayout.Label("FOV: " + AimbotOptions.FOV, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					AimbotOptions.FOV = (float)((int)Prefab.Slider(1f, 180f, AimbotOptions.FOV, 200));
					bool flag8 = AimbotOptions.FOV == 1f;
					if (flag8)
					{
						AimbotOptions.FOV = 3f;
					}
				}
				else
				{
					RaycastOptions.ShowAimUseFOV = false;
				}
				GUILayout.Label("Дистанция: " + AimbotOptions.Distance, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
				AimbotOptions.Distance = (float)((int)Prefab.Slider(50f, 1000f, AimbotOptions.Distance, 200));
				GUIContent[] array2 = new GUIContent[]
				{
					new GUIContent("Дистанция"),
					new GUIContent("FOV")
				};
				bool flag9 = Prefab.List(200f, "_TargetMode", new GUIContent("Наводится: " + array2[(int)AimbotOptions.TargetMode].text), array2, Array.Empty<GUILayoutOption>());
				bool flag10 = flag9;
				if (flag10)
				{
					AimbotOptions.TargetMode = (TargetMode)DropDown.Get("_TargetMode").ListIndex;
				}
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			});
		}
	}
}

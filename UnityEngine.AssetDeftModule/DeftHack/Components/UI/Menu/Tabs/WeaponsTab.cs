using System;
using DeftHack.Options.AimOptions;
using DeftHack.Variables.UIVariables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000B6 RID: 182
	public static class WeaponsTab
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0001B778 File Offset: 0x00019978
		public static void Tab()
		{
			Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "ДЛЯ ОРУЖИЯ", delegate
			{
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(new GUILayoutOption[]
				{
					GUILayout.Width(230f)
				});
				Prefab.Toggle("Нет отдачи", ref WeaponOptions.NoRecoil, 17);
				Prefab.Toggle("Нет разброса", ref WeaponOptions.NoSpread, 17);
				Prefab.Toggle("Нет увода", ref WeaponOptions.NoSway, 17);
				Prefab.Toggle("Нет баллистики", ref WeaponOptions.NoDrop, 17);
				Prefab.Toggle("Триггербот", ref TriggerbotOptions.Enabled, 17);
				Prefab.Toggle("Звук после убийства", ref WeaponOptions.OofOnDeath, 17);
				Prefab.Toggle("Автоперезарядка", ref WeaponOptions.AutoReload, 17);
				Prefab.Toggle("Показывать инфу о оружии", ref WeaponOptions.ShowWeaponInfo, 17);
				bool flag = Prefab.Toggle("Стрелять всегда в голову", ref RaycastOptions.AlwaysHitHead, 17);
				if (flag)
				{
					RaycastOptions.UseCustomLimb = false;
					RaycastOptions.UseRandomLimb = false;
				}
				bool flag2 = Prefab.Toggle("Случайная конечность", ref RaycastOptions.UseRandomLimb, 17);
				if (flag2)
				{
					RaycastOptions.UseCustomLimb = false;
					RaycastOptions.AlwaysHitHead = false;
				}
				bool flag3 = !RaycastOptions.UseRandomLimb;
				if (flag3)
				{
					bool flag4 = Prefab.Toggle("Кастомная конечность", ref RaycastOptions.UseCustomLimb, 17);
					if (flag4)
					{
						RaycastOptions.UseRandomLimb = false;
						RaycastOptions.AlwaysHitHead = false;
					}
				}
				GUILayout.Space(2f);
				GUIContent[] array = new GUIContent[]
				{
					new GUIContent("Left Foot"),
					new GUIContent("Left Leg"),
					new GUIContent("Right Foot"),
					new GUIContent("Right Leg"),
					new GUIContent("Left Hand"),
					new GUIContent("Left Arm"),
					new GUIContent("Right Hand"),
					new GUIContent("Right Arm"),
					new GUIContent("Left Back"),
					new GUIContent("Right Back"),
					new GUIContent("Left Front"),
					new GUIContent("Right Front"),
					new GUIContent("Spine"),
					new GUIContent("Skull")
				};
				GUILayout.Space(2f);
				bool flag5 = RaycastOptions.UseCustomLimb && !RaycastOptions.UseRandomLimb;
				bool flag6 = flag5;
				if (flag6)
				{
					bool flag7 = Prefab.List(230f, "_TargetLimb", new GUIContent("Конечность: " + array[(int)RaycastOptions.TargetLimb].text), array, Array.Empty<GUILayoutOption>());
					bool flag8 = flag7;
					if (flag8)
					{
						RaycastOptions.TargetLimb = (ELimb)DropDown.Get("_TargetLimb").ListIndex;
					}
				}
				GUILayout.Space(2f);
				GUILayout.EndVertical();
				GUILayout.BeginVertical(new GUILayoutOption[]
				{
					GUILayout.Width(230f)
				});
				GUILayout.Space(2f);
				GUILayout.Space(2f);
				GUILayout.EndVertical();
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			});
		}
	}
}

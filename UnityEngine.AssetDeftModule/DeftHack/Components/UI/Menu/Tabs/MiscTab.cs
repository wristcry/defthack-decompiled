using System;
using Assembly_CSharp;
using DeftHack.Components.Basic;
using DeftHack.Options;
using DeftHack.Threads;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000A9 RID: 169
	public static class MiscTab
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00017A0C File Offset: 0x00015C0C
		public static void Tab()
		{
			Prefab.ScrollView(new Rect(0f, 0f, 466f, 436f), "Прочее", ref StatsTab.ScrollPos, delegate()
			{
				bool flag = MiscTab.cb == EEngine.CAR;
				if (flag)
				{
					MiscTab.car = "Машина";
				}
				else
				{
					bool flag2 = MiscTab.cb == EEngine.PLANE;
					if (flag2)
					{
						MiscTab.car = "Самолёт";
					}
					else
					{
						bool flag3 = MiscTab.cb == EEngine.HELICOPTER;
						if (flag3)
						{
							MiscTab.car = "Вертолёт";
						}
						else
						{
							bool flag4 = MiscTab.cb == EEngine.BLIMP;
							if (flag4)
							{
								MiscTab.car = "Дирижабль";
							}
							else
							{
								bool flag5 = MiscTab.cb == EEngine.TRAIN;
								if (flag5)
								{
									MiscTab.car = "Поезд";
								}
							}
						}
					}
				}
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				GUILayout.BeginVertical(new GUILayoutOption[]
				{
					GUILayout.Width(230f)
				});
				Prefab.Toggle("Для транспорта", ref MiscOptions.EnVehicle, 17);
				bool enVehicle = MiscOptions.EnVehicle;
				if (enVehicle)
				{
					Prefab.Toggle("Полёт транспорта", ref MiscOptions.VehicleFly, 17);
					bool vehicleFly = MiscOptions.VehicleFly;
					bool flag6 = vehicleFly;
					if (flag6)
					{
						Prefab.Toggle("Максимальная скорость", ref MiscOptions.VehicleUseMaxSpeed, 17);
						bool flag7 = !MiscOptions.VehicleUseMaxSpeed;
						bool flag8 = flag7;
						if (flag8)
						{
							GUILayout.Space(2f);
							GUILayout.Label("Множитель скорости: " + MiscOptions.SpeedMultiplier + "x", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
							GUILayout.Space(2f);
							MiscOptions.SpeedMultiplier = (float)Math.Round((double)Prefab.Slider(0f, 10f, MiscOptions.SpeedMultiplier, 175), 2);
							GUILayout.Space(4f);
						}
					}
					GUILayout.Space(2f);
					bool flag9 = Prefab.Button("Заправить машину", 200f, 25f, new GUILayoutOption[0]);
					if (flag9)
					{
						Main.Fuel();
					}
					GUILayout.Space(2f);
					bool flag10 = Prefab.Button("Проезд сквозь объекты" + MoreMiscTab.a(MiscTab.lb), 200f, 25f, new GUILayoutOption[0]);
					if (flag10)
					{
						Main.ColliderVehicle();
					}
					GUILayout.Label("_________________________", Prefab._TextStyle, new GUILayoutOption[0]);
				}
				Prefab.Toggle("Быстрое снятия строений", ref MiscOptions.CustomSalvageTime, 17);
				Prefab.Toggle("Постройка в препядствиях", ref MiscOptions.BuildinObstacles, 17);
				Prefab.Toggle("Время суток", ref MiscOptions.SetTimeEnabled, 17);
				Prefab.Toggle("Зависание", ref MiscOptions.hang, 17);
				bool noMovementVerification = MiscOptions.NoMovementVerification;
				bool flag11 = noMovementVerification;
				if (flag11)
				{
					Prefab.Toggle("Полёт игрока", ref MiscOptions.PlayerFlight, 17);
					bool playerFlight = MiscOptions.PlayerFlight;
					bool flag12 = playerFlight;
					if (flag12)
					{
						GUILayout.Label("Множитель скорости: " + MiscOptions.FlightSpeedMultiplier + "x", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						GUILayout.Space(2f);
						MiscOptions.FlightSpeedMultiplier = (float)Math.Round((double)Prefab.Slider(0f, 10f, MiscOptions.FlightSpeedMultiplier, 175), 2);
					}
				}
				Prefab.Toggle("Дальность удара", ref MiscOptions.PunchSilentAim, 17);
				bool punchSilentAim = MiscOptions.PunchSilentAim;
				if (punchSilentAim)
				{
					MiscOptions.ExtendMeleeRange = true;
				}
				else
				{
					MiscOptions.ExtendMeleeRange = false;
				}
				GUILayout.EndVertical();
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				bool flag13 = Provider.isConnected && OptimizationVariables.MainPlayer != null;
				bool flag14 = flag13;
				if (flag14)
				{
					bool flag15 = !OptimizationVariables.MainPlayer.look.isOrbiting;
					bool flag16 = flag15;
					if (flag16)
					{
						OptimizationVariables.MainPlayer.look.orbitPosition = Vector3.zero;
					}
					Prefab.Toggle("Свободная камера", ref MiscOptions.Freecam, 17);
					bool isOrbiting = OptimizationVariables.MainPlayer.look.isOrbiting;
					bool flag17 = isOrbiting;
					if (flag17)
					{
						bool flag18 = Prefab.Button("Вернуть камеру", 150f, 25f, Array.Empty<GUILayoutOption>());
						bool flag19 = flag18;
						if (flag19)
						{
							Main.ResetCam();
						}
					}
				}
				Prefab.Toggle("Краш сервера", ref ServerCrashThread.ServerCrashEnabled, 17);
				Prefab.Toggle("Авто краш сервера", ref ServerCrashThread.AlwaysCrash, 17);
				Prefab.Toggle("Автопроверка движения", ref MiscOptions.AlwaysCheckMovementVerification, 17);
				bool isConnected = Provider.isConnected;
				bool flag20 = isConnected;
				if (flag20)
				{
					bool flag21 = Prefab.Button("Проверить движение", 150f, 25f, Array.Empty<GUILayoutOption>());
					bool flag22 = flag21;
					if (flag22)
					{
						MiscComponent.CheckMovementVerification();
					}
				}
				bool extendMeleeRange = MiscOptions.ExtendMeleeRange;
				bool flag23 = extendMeleeRange;
				if (flag23)
				{
					GUILayout.Space(2f);
					GUILayout.Label("Расстояние удара: " + MiscOptions.MeleeRangeExtension, Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					GUILayout.Space(2f);
					MiscOptions.MeleeRangeExtension = (float)Math.Round((double)Prefab.Slider(0f, 7.5f, MiscOptions.MeleeRangeExtension, 175), 1);
				}
				GUILayout.Space(5f);
				bool setTimeEnabled = MiscOptions.SetTimeEnabled;
				if (setTimeEnabled)
				{
					GUILayout.Label("ТЕКУЩЕЕ ВРЕМЯ", Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Label("Время: " + MiscOptions.Time, Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Space(2f);
					MiscOptions.Time = (float)Math.Round((double)Prefab.Slider(0f, 0.9f, MiscOptions.Time, 175), 2);
					GUILayout.Space(8f);
				}
				GUILayout.Space(5f);
				bool customSalvageTime = MiscOptions.CustomSalvageTime;
				if (customSalvageTime)
				{
					GUILayout.Label("ВРЕМЯ СНЯТИЯ ПОСТРОЕК", Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Label("Время снятия: " + MiscOptions.SalvageTime + " секунда", Prefab._TextStyle, new GUILayoutOption[0]);
					GUILayout.Space(2f);
					MiscOptions.SalvageTime = (float)Math.Round((double)Prefab.Slider(0f, 10f, MiscOptions.SalvageTime, 175));
					bool flag24 = MiscOptions.SalvageTime == 0f;
					if (flag24)
					{
						MiscOptions.SalvageTime = 1f;
					}
					GUILayout.Space(8f);
				}
				GUILayout.Space(5f);
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}, 20, Array.Empty<GUILayoutOption>());
			Prefab.MenuArea(new Rect(17f, 291f, 215f, 135f), "СПАМЕР", delegate
			{
				Prefab.Toggle("Включить", ref MiscOptions.SpammerEnabled, 17);
				GUILayout.Space(5f);
				MiscOptions.SpamText = Prefab.TextField(MiscOptions.SpamText, "Текст: ", 150);
				GUILayout.Space(10f);
				GUILayout.Label("Задержка: " + MiscOptions.SpammerDelay + "ms", Prefab._TextStyle, Array.Empty<GUILayoutOption>());
				GUILayout.Space(5f);
				MiscOptions.SpammerDelay = (int)Prefab.Slider(0f, 10000f, (float)MiscOptions.SpammerDelay, 175);
			});
			Prefab.MenuArea(new Rect(235f, 271f, 221f, 155f), "Взаимодействия", delegate
			{
				Prefab.Toggle("Взаимодейвие через:", ref InteractionOptions.InteractThroughWalls, 17);
				bool flag = !InteractionOptions.InteractThroughWalls;
				bool flag2 = !flag;
				if (flag2)
				{
					Prefab.Toggle("Стены/Полы/т.д.", ref InteractionOptions.NoHitStructures, 17);
					Prefab.Toggle("Сейфы/Двери/т.д.", ref InteractionOptions.NoHitBarricades, 17);
					Prefab.Toggle("Предметы", ref InteractionOptions.NoHitItems, 17);
					Prefab.Toggle("Транспорт", ref InteractionOptions.NoHitVehicles, 17);
					Prefab.Toggle("Ресурсы", ref InteractionOptions.NoHitResources, 17);
					Prefab.Toggle("Землю/Здания", ref InteractionOptions.NoHitEnvironment, 17);
				}
			});
		}

		// Token: 0x04000377 RID: 887
		public static int i;

		// Token: 0x04000378 RID: 888
		public static int o;

		// Token: 0x04000379 RID: 889
		public static bool lb;

		// Token: 0x0400037A RID: 890
		public static int db;

		// Token: 0x0400037B RID: 891
		public static int nnX;

		// Token: 0x0400037C RID: 892
		public static EEngine znI;

		// Token: 0x0400037D RID: 893
		private static string car;

		// Token: 0x0400037E RID: 894
		public static EEngine cb;
	}
}

using System;

using DeftHack.Misc.Classes.ESP;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Options.VisualOptions;
using DeftHack.Utilities;
using DeftHackFree;
using UnityEngine;

namespace DeftHack.Components.UI.Menu.Tabs
{
	// Token: 0x020000B4 RID: 180
	public static class VisualsTab
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0001A360 File Offset: 0x00018560
		public static void Tab()
		{
			Prefab.ScrollView(new Rect(0f, 0f, 225f, 436f), "ВХ", ref StatsTab.ScrollPos, delegate()
			{
				Prefab.SectionTabButton("Игроки", delegate
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Игроки);
					bool flag = !ESPOptions.VisualOptions[0].Enabled;
					bool flag2 = !flag;
					if (flag2)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
						Prefab.Toggle("Показывать оружие", ref ESPOptions.ShowPlayerWeapon, 17);
						Prefab.Toggle("Показывать транспорт", ref ESPOptions.ShowPlayerVehicle, 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Зомби", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Зомби);
				}, 0f, 20);
				Prefab.SectionTabButton("Транспорт", delegate
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Транспорт);
					bool flag = !ESPOptions.VisualOptions[6].Enabled;
					bool flag2 = !flag;
					if (flag2)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
						Prefab.Toggle("Кол-во топлива", ref ESPOptions.ShowVehicleFuel, 17);
						Prefab.Toggle("Кол-во прочности", ref ESPOptions.ShowVehicleHealth, 17);
						Prefab.Toggle("Показывать закрытые", ref ESPOptions.ShowVehicleLocked, 17);
						Prefab.Toggle("Фильтровать закрытые", ref ESPOptions.FilterVehicleLocked, 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Предметы", delegate
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Предметы);
					bool flag = !ESPOptions.VisualOptions[2].Enabled;
					bool flag2 = !flag;
					if (flag2)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
						Prefab.Toggle("Фильтр предметов", ref ESPOptions.FilterItems, 17);
						bool filterItems = ESPOptions.FilterItems;
						bool flag3 = filterItems;
						if (flag3)
						{
							GUILayout.Space(5f);
							ItemUtilities.DrawFilterTab(ItemOptions.ItemESPOptions);
						}
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Ящики", delegate
				{
					VisualsTab.BasicControls(ESPTarget.Ящики);
				}, 0f, 20);
				Prefab.SectionTabButton("Кровати", delegate
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Кровати);
					bool flag = !ESPOptions.VisualOptions[4].Enabled;
					bool flag2 = !flag;
					if (flag2)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
						Prefab.Toggle("Показать занятые", ref ESPOptions.ShowClaimed, 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Генераторы", delegate
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Генераторы);
					bool flag = !ESPOptions.VisualOptions[8].Enabled;
					bool flag2 = !flag;
					if (flag2)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
						Prefab.Toggle("Кол-во топлива", ref ESPOptions.ShowGeneratorFuel, 17);
						Prefab.Toggle("Статус работы", ref ESPOptions.ShowGeneratorPowered, 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Турели", delegate
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Турели);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					bool flag2 = !flag;
					if (flag2)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
						Prefab.Toggle("Показывать оружие", ref ESPOptions.ShowSentryItem, 17);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Клейм флаги", delegate
				{
					VisualsTab.BasicControls(ESPTarget.КлеймФлаги);
				}, 0f, 20);
				Prefab.SectionTabButton("Животные", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Животные);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Ловушки", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Ловшуки);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Двери", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Двери);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Аирдропы", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Аирдропы);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Ягоды", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Ягоды);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Растения", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Растения);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Взрывчатка", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.C4);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Источники огня", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Fire);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Лампы", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Лампы);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Топливо", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Топливо);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Ген. СЗ", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Генератор_безопасной_зоны);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
				Prefab.SectionTabButton("Ген. воздух", delegate
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.BeginVertical(new GUILayoutOption[]
					{
						GUILayout.Width(240f)
					});
					VisualsTab.BasicControls(ESPTarget.Генератор_Воздуха);
					bool flag = !ESPOptions.VisualOptions[3].Enabled;
					if (!flag)
					{
						GUILayout.EndVertical();
						GUILayout.BeginVertical(new GUILayoutOption[0]);
						GUILayout.EndVertical();
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
					}
				}, 0f, 20);
			}, 20, Array.Empty<GUILayoutOption>());
			Prefab.MenuArea(new Rect(230f, 0f, 236f, 180f), "ДРУГОЕ", delegate
			{
				Prefab.SectionTabButton("Радар", delegate
				{
					Prefab.Toggle("Радар", ref RadarOptions.Enabled, 17);
					bool enabled2 = RadarOptions.Enabled;
					bool flag2 = enabled2;
					if (flag2)
					{
						Prefab.Toggle("Центрирование игрока", ref RadarOptions.TrackPlayer, 17);
						Prefab.Toggle("Показывать игроков", ref RadarOptions.ShowPlayers, 17);
						Prefab.Toggle("Показывать машины", ref RadarOptions.ShowVehicles, 17);
						bool showVehicles = RadarOptions.ShowVehicles;
						bool flag3 = showVehicles;
						if (flag3)
						{
							Prefab.Toggle("Только открытые", ref RadarOptions.ShowVehiclesUnlocked, 17);
						}
						GUILayout.Space(5f);
						GUILayout.Label("Зум радара: " + Mathf.Round(RadarOptions.RadarZoom), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						Prefab.Slider(0f, 10f, ref RadarOptions.RadarZoom, 200);
						bool flag4 = Prefab.Button("По умолчанию", 105f, 25f, Array.Empty<GUILayoutOption>());
						bool flag5 = flag4;
						if (flag5)
						{
							RadarOptions.RadarZoom = 1f;
						}
						GUILayout.Space(5f);
						GUILayout.Label("Размер радара: " + Mathf.RoundToInt(RadarOptions.RadarSize), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
						Prefab.Slider(50f, 1000f, ref RadarOptions.RadarSize, 200);
					}
				}, 0f, 20);
				Prefab.Toggle("Игроки в ванише", ref ESPOptions.ShowVanishPlayers, 17);
				Prefab.Toggle("Камера заднего вида", ref MirrorCameraOptions.Enabled, 17);
				GUILayout.Space(5f);
			});
			Prefab.MenuArea(new Rect(230f, 185f, 236f, 250f), "Переключатели", delegate
			{
				bool flag = Prefab.Toggle("ВХ", ref ESPOptions.Enabled, 17);
				bool flag2 = flag;
				if (flag2)
				{
					bool flag3 = !ESPOptions.Enabled;
					bool flag4 = flag3;
					if (flag4)
					{
						for (int i = 0; i < ESPOptions.VisualOptions.Length; i++)
						{
							ESPOptions.VisualOptions[i].Glow = false;
						}
						abc.HookObject.GetComponent<ESPComponent>().OnGUI();
					}
				}
				Prefab.Toggle("Чамсы", ref ESPOptions.ChamsEnabled, 17);
				bool chamsEnabled = ESPOptions.ChamsEnabled;
				bool flag5 = chamsEnabled;
				if (flag5)
				{
					Prefab.Toggle("Плоские чамсы", ref ESPOptions.ChamsFlat, 17);
				}
				Prefab.Toggle("Без дождя", ref MiscOptions.NoRain, 17);
				Prefab.Toggle("Без снега", ref MiscOptions.NoSnow, 17);
				Prefab.Toggle("No Flash", ref MiscOptions.NoFlash, 17);
				Prefab.Toggle("ПНВ", ref MiscOptions.NightVision, 17);
				Prefab.Toggle("Компасс", ref MiscOptions.Compass, 17);
				Prefab.Toggle("Карта(GPS)", ref MiscOptions.GPS, 17);
				Prefab.Toggle("Показ игроков на карте", ref MiscOptions.ShowPlayersOnMap, 17);
			});
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001A444 File Offset: 0x00018644
		private static void BasicControls(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			Prefab.Toggle("Активировать", ref espvisual.Enabled, 17);
			bool flag = !espvisual.Enabled;
			bool flag2 = !flag;
			if (flag2)
			{
				Prefab.Toggle("Надписи", ref espvisual.Labels, 17);
				bool labels = espvisual.Labels;
				bool flag3 = labels;
				if (flag3)
				{
					Prefab.Toggle("Показывать имя", ref espvisual.ShowName, 17);
					Prefab.Toggle("Показывать дистанцию", ref espvisual.ShowDistance, 17);
					Prefab.Toggle("Показывать угол", ref espvisual.ShowAngle, 17);
				}
				Prefab.Toggle("Вох", ref espvisual.Boxes, 17);
				bool boxes = espvisual.Boxes;
				bool flag4 = boxes;
				if (flag4)
				{
					Prefab.Toggle("2D Вох", ref espvisual.TwoDimensional, 17);
				}
				Prefab.Toggle("Обводка", ref espvisual.Glow, 17);
				Prefab.Toggle("Линия до объекта", ref espvisual.LineToObject, 17);
				Prefab.Toggle("Масштаб текста", ref espvisual.TextScaling, 17);
				bool textScaling = espvisual.TextScaling;
				bool flag5 = textScaling;
				if (flag5)
				{
					espvisual.MinTextSize = Prefab.TextField(espvisual.MinTextSize, "Минимальный размер текста: ", 30, 0, 255);
					espvisual.MaxTextSize = Prefab.TextField(espvisual.MaxTextSize, "Максимальный размер текста: ", 30, 0, 255);
					GUILayout.Space(3f);
					GUILayout.Label("Масштабирование текста по расстоянию: " + Mathf.RoundToInt(espvisual.MinTextSizeDistance), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					Prefab.Slider(0f, 1000f, ref espvisual.MinTextSizeDistance, 200);
					GUILayout.Space(3f);
				}
				else
				{
					espvisual.FixedTextSize = Prefab.TextField(espvisual.FixedTextSize, "Фиксированный размер текста: ", 30, 0, 255);
				}
				Prefab.Toggle("Дистанция на всю карту", ref espvisual.InfiniteDistance, 17);
				bool flag6 = !espvisual.InfiniteDistance;
				bool flag7 = flag6;
				if (flag7)
				{
					GUILayout.Label("ESP Расстояние: " + Mathf.RoundToInt(espvisual.Distance), Prefab._TextStyle, Array.Empty<GUILayoutOption>());
					Prefab.Slider(0f, 4000f, ref espvisual.Distance, 200);
					GUILayout.Space(3f);
				}
				Prefab.Toggle("Лимит объектов", ref espvisual.UseObjectCap, 17);
				bool useObjectCap = espvisual.UseObjectCap;
				bool flag8 = useObjectCap;
				if (flag8)
				{
					espvisual.ObjectCap = Prefab.TextField(espvisual.ObjectCap, "Object cap:", 30, 0, 255);
				}
				espvisual.BorderStrength = Prefab.TextField(espvisual.BorderStrength, "Border Strength:", 30, 0, 255);
			}
		}
	}
}

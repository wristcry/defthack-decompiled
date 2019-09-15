using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Components.Basic;
using DeftHack.Coroutines;
using DeftHack.Misc.Enums;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using DeftHack.Variables.UIVariables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI
{
	// Token: 0x02000095 RID: 149
	[Component]
	[SpyComponent]
	public class WeaponComponent : MonoBehaviour
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00012E3C File Offset: 0x0001103C
		public static byte Ammo()
		{
			return (byte)WeaponComponent.AmmoInfo.GetValue(OptimizationVariables.MainPlayer.equipment.useable);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00012E6C File Offset: 0x0001106C
		[Initializer]
		public static void Initialize()
		{
			ColorUtilities.addColor(new ColorVariable("_BulletTracersHitColor", "Оружие - пули трассеры (Hit)", new Color32(byte.MaxValue, 0, 0, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_BulletTracersColor", "Оружие - пули трассеры", new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_WeaponInfoColor", "Оружие - Информация", new Color32(0, byte.MaxValue, 0, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_ShowFOVAim", "Отрисовка FOV Aim", new Color32(0, byte.MaxValue, 0, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_ShowFOV", "Отрисовка FOV SilentAim", new Color32(byte.MaxValue, 0, 0, byte.MaxValue), true));
			ColorUtilities.addColor(new ColorVariable("_WeaponInfoBorder", "Оружие - Информация (Граница)", new Color32(0, 0, 0, byte.MaxValue), true));
			HotkeyComponent.ActionDict.Add("_ToggleTriggerbot", delegate
			{
				TriggerbotOptions.Enabled = !TriggerbotOptions.Enabled;
			});
			HotkeyComponent.ActionDict.Add("_ToggleNoRecoil", delegate
			{
				WeaponOptions.NoRecoil = !WeaponOptions.NoRecoil;
			});
			HotkeyComponent.ActionDict.Add("_ToggleNoSpread", delegate
			{
				WeaponOptions.NoSpread = !WeaponOptions.NoSpread;
			});
			HotkeyComponent.ActionDict.Add("_ToggleNoSway", delegate
			{
				WeaponOptions.NoSway = !WeaponOptions.NoSway;
			});
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public void Start()
		{
			base.StartCoroutine(WeaponComponent.UpdateWeapon());
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00013024 File Offset: 0x00011224
		public void OnGUI()
		{
			bool flag = WeaponComponent.MainCamera == null;
			bool flag2 = flag;
			if (flag2)
			{
				WeaponComponent.MainCamera = Camera.main;
			}
			bool noSway = WeaponOptions.NoSway;
			bool flag3 = noSway;
			if (flag3)
			{
				bool flag4 = OptimizationVariables.MainPlayer != null && OptimizationVariables.MainPlayer.animator != null;
				bool flag5 = flag4;
				if (flag5)
				{
					OptimizationVariables.MainPlayer.animator.viewSway = Vector3.zero;
				}
			}
			bool flag6 = Event.current.type != EventType.Repaint;
			bool flag7 = !flag6;
			if (flag7)
			{
				bool flag8 = !DrawUtilities.ShouldRun();
				bool flag9 = !flag8;
				if (flag9)
				{
					bool tracers = WeaponOptions.Tracers;
					bool flag10 = tracers;
					if (flag10)
					{
						ESPComponent.GLMat.SetPass(0);
						GL.PushMatrix();
						GL.LoadProjectionMatrix(WeaponComponent.MainCamera.projectionMatrix);
						GL.modelview = WeaponComponent.MainCamera.worldToCameraMatrix;
						GL.Begin(1);
						for (int i = WeaponComponent.Tracers.Count - 1; i > -1; i--)
						{
							TracerLine tracerLine = WeaponComponent.Tracers[i];
							bool flag11 = DateTime.Now - tracerLine.CreationTime > TimeSpan.FromSeconds(5.0);
							bool flag12 = flag11;
							if (flag12)
							{
								WeaponComponent.Tracers.Remove(tracerLine);
							}
							else
							{
								GL.Color(tracerLine.Hit ? ColorUtilities.getColor("_BulletTracersHitColor") : ColorUtilities.getColor("_BulletTracersColor"));
								GL.Vertex(tracerLine.StartPosition);
								GL.Vertex(tracerLine.EndPosition);
							}
						}
						GL.End();
						GL.PopMatrix();
					}
					bool showWeaponInfo = WeaponOptions.ShowWeaponInfo;
					bool flag13 = showWeaponInfo;
					if (flag13)
					{
						bool flag14 = !(OptimizationVariables.MainPlayer.equipment.asset is ItemGunAsset);
						bool flag15 = !flag14;
						if (flag15)
						{
							GUI.depth = 0;
							ItemGunAsset itemGunAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
							string content = string.Format("<size=15>{0}\nДальность: {1}\nУрон игрокам: {2}</size>", itemGunAsset.itemName, itemGunAsset.range, itemGunAsset.playerDamageMultiplier.damage);
							DrawUtilities.DrawLabel(ESPComponent.ESPFont, LabelLocation.MiddleLeft, new Vector2((float)(Screen.width - 20), (float)(Screen.height / 2)), content, ColorUtilities.getColor("_WeaponInfoColor"), ColorUtilities.getColor("_WeaponInfoBorder"), 1, null, 12);
						}
					}
					float radius = RaycastOptions.SilentAimFOV * 7f + 20f;
					float radius2 = AimbotOptions.FOV * 7f + 20f;
					bool showSilentAimUseFOV = RaycastOptions.ShowSilentAimUseFOV;
					if (showSilentAimUseFOV)
					{
						DrawUtilities.DrawCircle(AssetVariables.Materials["ESP"], ColorUtilities.getColor("_ShowFOV"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), radius);
					}
					bool showAimUseFOV = RaycastOptions.ShowAimUseFOV;
					if (showAimUseFOV)
					{
						DrawUtilities.DrawCircle(AssetVariables.Materials["ESP"], ColorUtilities.getColor("_ShowFOVAim"), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), radius2);
					}
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00002DE3 File Offset: 0x00000FE3
		public static IEnumerator UpdateWeapon()
		{
			for (;;)
			{
				yield return new WaitForSeconds(0.1f);
				bool flag = !DrawUtilities.ShouldRun();
				bool flag6 = !flag;
				if (flag6)
				{
					ItemGunAsset PAsset;
					bool flag2 = (PAsset = (OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset)) == null;
					bool flag7 = !flag2;
					if (flag7)
					{
						bool flag3 = !WeaponComponent.AssetBackups.ContainsKey(PAsset.id);
						bool flag8 = flag3;
						if (flag8)
						{
							float[] Backups = new float[]
							{
								PAsset.recoilAim,
								PAsset.recoilMax_x,
								PAsset.recoilMax_y,
								PAsset.recoilMin_x,
								PAsset.recoilMin_y,
								PAsset.spreadAim,
								PAsset.spreadHip
							};
							Backups[6] = PAsset.spreadHip;
							WeaponComponent.AssetBackups.Add(PAsset.id, Backups);
							Backups = null;
							Backups = null;
						}
						bool flag4 = WeaponOptions.NoRecoil && !PlayerCoroutines.IsSpying;
						bool flag9 = flag4;
						if (flag9)
						{
							PAsset.recoilAim = 0f;
							PAsset.recoilMax_x = 0f;
							PAsset.recoilMax_y = 0f;
							PAsset.recoilMin_x = 0f;
							PAsset.recoilMin_y = 0f;
						}
						else
						{
							PAsset.recoilAim = WeaponComponent.AssetBackups[PAsset.id][0];
							PAsset.recoilMax_x = WeaponComponent.AssetBackups[PAsset.id][1];
							PAsset.recoilMax_y = WeaponComponent.AssetBackups[PAsset.id][2];
							PAsset.recoilMin_x = WeaponComponent.AssetBackups[PAsset.id][3];
							PAsset.recoilMin_y = WeaponComponent.AssetBackups[PAsset.id][4];
						}
						bool flag5 = WeaponOptions.NoSpread && !PlayerCoroutines.IsSpying;
						bool flag10 = flag5;
						if (flag10)
						{
							PAsset.spreadAim = 0f;
							PAsset.spreadHip = 0f;
							PlayerUI.updateCrosshair(0f);
						}
						else
						{
							PAsset.spreadAim = WeaponComponent.AssetBackups[PAsset.id][5];
							PAsset.spreadHip = WeaponComponent.AssetBackups[PAsset.id][6];
							WeaponComponent.UpdateCrosshair.Invoke(OptimizationVariables.MainPlayer.equipment.useable, null);
						}
						WeaponComponent.Reload();
						PAsset = null;
					}
					PAsset = null;
				}
			}
			yield break;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00013378 File Offset: 0x00011578
		public static void Reload()
		{
			bool flag = !WeaponOptions.AutoReload || WeaponComponent.Ammo() > 0;
			bool flag2 = !flag;
			if (flag2)
			{
				IEnumerable<InventorySearch> source = from i in OptimizationVariables.MainPlayer.inventory.search(EItemType.MAGAZINE, ((ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset).magazineCalibers)
				where i.jar.item.amount > 0
				select i;
				List<InventorySearch> list = source.ToList<InventorySearch>();
				bool flag3 = list.Count == 0;
				bool flag4 = !flag3;
				if (flag4)
				{
					InventorySearch inventorySearch = (from i in list
					orderby i.jar.item.amount descending
					select i).First<InventorySearch>();
					OptimizationVariables.MainPlayer.channel.send("askAttachMagazine", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						inventorySearch.page,
						inventorySearch.jar.x,
						inventorySearch.jar.y
					});
				}
			}
		}

		// Token: 0x04000311 RID: 785
		public static Dictionary<ushort, float[]> AssetBackups = new Dictionary<ushort, float[]>();

		// Token: 0x04000312 RID: 786
		public static List<TracerLine> Tracers = new List<TracerLine>();

		// Token: 0x04000313 RID: 787
		public static Camera MainCamera;

		// Token: 0x04000314 RID: 788
		public static FieldInfo AmmoInfo = typeof(UseableGun).GetField("ammo", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000315 RID: 789
		public static MethodInfo UpdateCrosshair = typeof(UseableGun).GetMethod("updateCrosshair", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}

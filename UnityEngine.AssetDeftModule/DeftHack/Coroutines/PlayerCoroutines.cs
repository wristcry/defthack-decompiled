using System;
using System.Collections;
using System.IO;
using DeftHack.Components.UI;
using DeftHack.Managers.Submanagers;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Coroutines
{
	// Token: 0x0200008C RID: 140
	public static class PlayerCoroutines
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00002C6A File Offset: 0x00000E6A
		public static IEnumerator TakeScreenshot()
		{
			Player plr = OptimizationVariables.MainPlayer;
			SteamChannel channel = plr.channel;
			switch (MiscOptions.AntiSpyMethod)
			{
			case 0:
			{
				bool flag = Time.realtimeSinceStartup - PlayerCoroutines.LastSpy < 0.5f || PlayerCoroutines.IsSpying;
				bool flag7 = flag;
				if (flag7)
				{
					yield break;
				}
				PlayerCoroutines.IsSpying = true;
				PlayerCoroutines.LastSpy = Time.realtimeSinceStartup;
				bool flag2 = !MiscOptions.PanicMode;
				bool flag8 = flag2;
				if (flag8)
				{
					PlayerCoroutines.DisableAllVisuals();
				}
				yield return new WaitForFixedUpdate();
				yield return new WaitForEndOfFrame();
				Texture2D screenshotRaw = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
				{
					name = "Screenshot_Raw",
					hideFlags = HideFlags.HideAndDontSave
				};
				screenshotRaw.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0, false);
				Texture2D screenshotFinal = new Texture2D(640, 480, TextureFormat.RGB24, false)
				{
					name = "Screenshot_Final",
					hideFlags = HideFlags.HideAndDontSave
				};
				Color[] oldColors = screenshotRaw.GetPixels();
				Color[] newColors = new Color[screenshotFinal.width * screenshotFinal.height];
				float widthRatio = (float)screenshotRaw.width / (float)screenshotFinal.width;
				float heightRatio = (float)screenshotRaw.height / (float)screenshotFinal.height;
				int num4;
				for (int i = 0; i < screenshotFinal.height; i = num4 + 1)
				{
					int num = (int)((float)i * heightRatio) * screenshotRaw.width;
					int num2 = i * screenshotFinal.width;
					for (int j = 0; j < screenshotFinal.width; j = num4 + 1)
					{
						int num3 = (int)((float)j * widthRatio);
						newColors[num2 + j] = oldColors[num + num3];
						num4 = j;
					}
					num4 = i;
				}
				screenshotFinal.SetPixels(newColors);
				byte[] data = screenshotFinal.EncodeToJPG(33);
				bool flag3 = data.Length < 30000;
				bool flag9 = flag3;
				if (flag9)
				{
					channel.longBinaryData = true;
					channel.openWrite();
					channel.write(data);
					channel.closeWrite("tellScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
					channel.longBinaryData = false;
				}
				yield return new WaitForFixedUpdate();
				yield return new WaitForEndOfFrame();
				PlayerCoroutines.IsSpying = false;
				bool flag4 = !MiscOptions.PanicMode;
				bool flag10 = flag4;
				if (flag10)
				{
					PlayerCoroutines.EnableAllVisuals();
				}
				break;
			}
			case 1:
			{
				System.Random r = new System.Random();
				string[] files = Directory.GetFiles(MiscOptions.AntiSpyPath);
				byte[] dataRaw = File.ReadAllBytes(files[r.Next(files.Length)]);
				Texture2D texRaw = new Texture2D(2, 2);
				texRaw.LoadImage(dataRaw);
				Texture2D screenshotFinal2 = new Texture2D(640, 480, TextureFormat.RGB24, false)
				{
					name = "Screenshot_Final",
					hideFlags = HideFlags.HideAndDontSave
				};
				Color[] oldColors2 = texRaw.GetPixels();
				Color[] newColors2 = new Color[screenshotFinal2.width * screenshotFinal2.height];
				float widthRatio2 = (float)texRaw.width / (float)screenshotFinal2.width;
				float heightRatio2 = (float)texRaw.height / (float)screenshotFinal2.height;
				int num8;
				for (int k = 0; k < screenshotFinal2.height; k = num8 + 1)
				{
					int num5 = (int)((float)k * heightRatio2) * texRaw.width;
					int num6 = k * screenshotFinal2.width;
					for (int l = 0; l < screenshotFinal2.width; l = num8 + 1)
					{
						int num7 = (int)((float)l * widthRatio2);
						newColors2[num6 + l] = oldColors2[num5 + num7];
						num8 = l;
					}
					num8 = k;
				}
				screenshotFinal2.SetPixels(newColors2);
				byte[] data2 = screenshotFinal2.EncodeToJPG(33);
				bool flag5 = data2.Length < 30000;
				bool flag11 = flag5;
				if (flag11)
				{
					channel.longBinaryData = true;
					channel.openWrite();
					channel.write(data2);
					channel.closeWrite("tellScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
					channel.longBinaryData = false;
				}
				break;
			}
			case 3:
			{
				yield return new WaitForFixedUpdate();
				yield return new WaitForEndOfFrame();
				Texture2D screenshotRaw2 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
				{
					name = "Screenshot_Raw",
					hideFlags = HideFlags.HideAndDontSave
				};
				screenshotRaw2.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0, false);
				Texture2D screenshotFinal3 = new Texture2D(640, 480, TextureFormat.RGB24, false)
				{
					name = "Screenshot_Final",
					hideFlags = HideFlags.HideAndDontSave
				};
				Color[] oldColors3 = screenshotRaw2.GetPixels();
				Color[] newColors3 = new Color[screenshotFinal3.width * screenshotFinal3.height];
				float widthRatio3 = (float)screenshotRaw2.width / (float)screenshotFinal3.width;
				float heightRatio3 = (float)screenshotRaw2.height / (float)screenshotFinal3.height;
				int num12;
				for (int m = 0; m < screenshotFinal3.height; m = num12 + 1)
				{
					int num9 = (int)((float)m * heightRatio3) * screenshotRaw2.width;
					int num10 = m * screenshotFinal3.width;
					for (int n = 0; n < screenshotFinal3.width; n = num12 + 1)
					{
						int num11 = (int)((float)n * widthRatio3);
						newColors3[num10 + n] = oldColors3[num9 + num11];
						num12 = n;
					}
					num12 = m;
				}
				screenshotFinal3.SetPixels(newColors3);
				byte[] data3 = screenshotFinal3.EncodeToJPG(33);
				bool flag6 = data3.Length < 30000;
				bool flag12 = flag6;
				if (flag12)
				{
					channel.longBinaryData = true;
					channel.openWrite();
					channel.write(data3);
					channel.closeWrite("tellScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
					channel.longBinaryData = false;
				}
				yield return new WaitForFixedUpdate();
				yield return new WaitForEndOfFrame();
				break;
			}
			}
			bool alertOnSpy = MiscOptions.AlertOnSpy;
			bool flag13 = alertOnSpy;
			if (flag13)
			{
				OptimizationVariables.MainPlayer.StartCoroutine(PlayerCoroutines.ScreenShotMessageCoroutine());
			}
			yield break;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00002C72 File Offset: 0x00000E72
		public static IEnumerator ScreenShotMessageCoroutine()
		{
			float started = Time.realtimeSinceStartup;
			bool flag2;
			do
			{
				yield return new WaitForEndOfFrame();
				bool flag = !PlayerCoroutines.IsSpying;
				bool flag3 = flag;
				if (flag3)
				{
					PlayerUI.hint(null, EPlayerMessage.INTERACT, "Тебя проверяют на /spy", Color.red, Array.Empty<object>());
				}
				flag2 = (Time.realtimeSinceStartup - started > 3f);
			}
			while (!flag2);
			yield break;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public static void DisableAllVisuals()
		{
			SpyManager.InvokePre();
			bool flag = DrawUtilities.ShouldRun();
			bool flag2 = flag;
			if (flag2)
			{
				ItemGunAsset itemGunAsset;
				bool flag3 = (itemGunAsset = (OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset)) != null;
				bool flag4 = flag3;
				if (flag4)
				{
					UseableGun useableGun = OptimizationVariables.MainPlayer.equipment.useable as UseableGun;
					PlayerUI.updateCrosshair(useableGun.isAiming ? WeaponComponent.AssetBackups[itemGunAsset.id][5] : WeaponComponent.AssetBackups[itemGunAsset.id][6]);
				}
			}
			bool flag5 = LevelLighting.seaLevel == 0f;
			if (flag5)
			{
				LevelLighting.seaLevel = MiscOptions.Altitude;
			}
			SpyManager.DestroyComponents();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00002C7A File Offset: 0x00000E7A
		public static void EnableAllVisuals()
		{
			SpyManager.AddComponents();
			SpyManager.InvokePost();
		}

		// Token: 0x040002B8 RID: 696
		public static float LastSpy;

		// Token: 0x040002B9 RID: 697
		public static bool IsSpying;

		// Token: 0x040002BA RID: 698
		public static Player SpecPlayer;
	}
}

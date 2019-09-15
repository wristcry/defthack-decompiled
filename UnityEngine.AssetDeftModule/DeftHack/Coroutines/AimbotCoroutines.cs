using System;
using System.Collections;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Components.UI;
using DeftHack.Misc.Enums;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Coroutines
{
	// Token: 0x02000080 RID: 128
	public static class AimbotCoroutines
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000CD20 File Offset: 0x0000AF20
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00002A95 File Offset: 0x00000C95
		public static float Pitch
		{
			get
			{
				return OptimizationVariables.MainPlayer.look.pitch;
			}
			set
			{
				AimbotCoroutines.PitchInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000CD44 File Offset: 0x0000AF44
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00002AB3 File Offset: 0x00000CB3
		public static float Yaw
		{
			get
			{
				return OptimizationVariables.MainPlayer.look.yaw;
			}
			set
			{
				AimbotCoroutines.YawInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00002AD1 File Offset: 0x00000CD1
		[Initializer]
		public static void Init()
		{
			AimbotCoroutines.PitchInfo = typeof(PlayerLook).GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic);
			AimbotCoroutines.YawInfo = typeof(PlayerLook).GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00002B0A File Offset: 0x00000D0A
		public static IEnumerator SetLockedObject()
		{
			for (;;)
			{
				bool flag = !DrawUtilities.ShouldRun() || !AimbotOptions.Enabled;
				bool flag9 = flag;
				if (flag9)
				{
					yield return new WaitForSeconds(0.1f);
				}
				else
				{
					Player p = null;
					Vector3 aimPos = OptimizationVariables.MainPlayer.look.aim.position;
					Vector3 aimForward = OptimizationVariables.MainPlayer.look.aim.forward;
					SteamPlayer[] players = Provider.clients.ToArray();
					int num;
					for (int i = 0; i < players.Length; i = num + 1)
					{
						SteamPlayer cPlayer = players[i];
						bool flag2 = cPlayer == null || cPlayer.player == OptimizationVariables.MainPlayer || cPlayer.player.life == null || cPlayer.player.life.isDead || FriendUtilities.IsFriendly(cPlayer.player);
						bool flag10 = !flag2;
						if (flag10)
						{
							TargetMode targetMode = AimbotOptions.TargetMode;
							bool flag11 = targetMode > TargetMode.Distance;
							if (flag11)
							{
								bool flag12 = targetMode == TargetMode.FOV;
								if (flag12)
								{
									bool flag3 = VectorUtilities.GetAngleDelta(aimPos, aimForward, players[i].player.transform.position) < (double)AimbotOptions.FOV;
									bool flag13 = flag3;
									if (flag13)
									{
										bool flag4 = p == null;
										bool flag14 = flag4;
										if (flag14)
										{
											p = players[i].player;
										}
										else
										{
											bool flag5 = VectorUtilities.GetAngleDelta(aimPos, aimForward, players[i].player.transform.position) < VectorUtilities.GetAngleDelta(aimPos, aimForward, p.transform.position);
											bool flag15 = flag5;
											if (flag15)
											{
												p = players[i].player;
											}
										}
									}
								}
							}
							else
							{
								bool flag6 = p == null;
								bool flag16 = flag6;
								if (flag16)
								{
									p = players[i].player;
								}
								else
								{
									bool flag7 = VectorUtilities.GetDistance(p.transform.position) > VectorUtilities.GetDistance(players[i].player.transform.position);
									bool flag17 = flag7;
									if (flag17)
									{
										p = players[i].player;
									}
								}
							}
							cPlayer = null;
						}
						num = i;
						cPlayer = null;
					}
					bool flag8 = !AimbotCoroutines.IsAiming;
					bool flag18 = flag8;
					if (flag18)
					{
						AimbotCoroutines.LockedObject = ((p != null) ? p.gameObject : null);
					}
					yield return new WaitForEndOfFrame();
					p = null;
					aimPos = default(Vector3);
					aimForward = default(Vector3);
					players = null;
					p = null;
					aimPos = default(Vector3);
					aimForward = default(Vector3);
					players = null;
				}
			}
			yield break;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002B12 File Offset: 0x00000D12
		public static IEnumerator AimToObject()
		{
			for (;;)
			{
				bool flag = !DrawUtilities.ShouldRun() || !AimbotOptions.Enabled;
				bool flag4 = flag;
				if (flag4)
				{
					yield return new WaitForSeconds(0.1f);
				}
				else
				{
					bool flag2 = AimbotCoroutines.LockedObject != null && AimbotCoroutines.LockedObject.transform != null && ESPComponent.MainCamera != null;
					bool flag5 = flag2;
					if (flag5)
					{
						bool flag3 = HotkeyUtilities.IsHotkeyHeld("_AimbotKey") || !AimbotOptions.OnKey;
						bool flag6 = flag3;
						if (flag6)
						{
							AimbotCoroutines.IsAiming = true;
							bool smooth = AimbotOptions.Smooth;
							bool flag7 = smooth;
							if (flag7)
							{
								AimbotCoroutines.SmoothAim(AimbotCoroutines.LockedObject);
							}
							else
							{
								AimbotCoroutines.Aim(AimbotCoroutines.LockedObject);
							}
						}
						else
						{
							AimbotCoroutines.IsAiming = false;
						}
					}
					else
					{
						AimbotCoroutines.IsAiming = false;
					}
					yield return new WaitForEndOfFrame();
				}
			}
			yield break;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public static void Aim(GameObject obj)
		{
			Camera mainCam = OptimizationVariables.MainCam;
			Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
			bool flag = aimPosition == AimbotCoroutines.PiVector;
			bool flag2 = !flag;
			if (flag2)
			{
				OptimizationVariables.MainPlayer.transform.LookAt(aimPosition);
				OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
				mainCam.transform.LookAt(aimPosition);
				float num = mainCam.transform.localRotation.eulerAngles.x;
				bool flag3 = num <= 90f && num <= 270f;
				bool flag4 = flag3;
				if (flag4)
				{
					num = mainCam.transform.localRotation.eulerAngles.x + 90f;
				}
				else
				{
					bool flag5 = num >= 270f && num <= 360f;
					bool flag6 = flag5;
					if (flag6)
					{
						num = mainCam.transform.localRotation.eulerAngles.x - 270f;
					}
				}
				AimbotCoroutines.Pitch = num;
				AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		public static void SmoothAim(GameObject obj)
		{
			Camera mainCam = OptimizationVariables.MainCam;
			Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
			bool flag = aimPosition == AimbotCoroutines.PiVector;
			bool flag2 = !flag;
			if (flag2)
			{
				OptimizationVariables.MainPlayer.transform.rotation = Quaternion.Slerp(OptimizationVariables.MainPlayer.transform.rotation, Quaternion.LookRotation(aimPosition - OptimizationVariables.MainPlayer.transform.position), Time.deltaTime * AimbotOptions.AimSpeed);
				OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
				mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, Quaternion.LookRotation(aimPosition - mainCam.transform.position), Time.deltaTime * AimbotOptions.AimSpeed);
				float num = mainCam.transform.localRotation.eulerAngles.x;
				bool flag3 = num <= 90f && num <= 270f;
				bool flag4 = flag3;
				if (flag4)
				{
					num = mainCam.transform.localRotation.eulerAngles.x + 90f;
				}
				else
				{
					bool flag5 = num >= 270f && num <= 360f;
					bool flag6 = flag5;
					if (flag6)
					{
						num = mainCam.transform.localRotation.eulerAngles.x - 270f;
					}
				}
				AimbotCoroutines.Pitch = num;
				AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		private static Vector2 CalcAngle(GameObject obj)
		{
			Vector3 vector = ESPComponent.MainCamera.WorldToScreenPoint(AimbotCoroutines.GetAimPosition(obj.transform, "Skull"));
			return Vector2.zero;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		public static void AimMouseTo(float x, float y)
		{
			DebugUtilities.Log(string.Format("yaw:{0}|pitch:{1}|x:{2}|y:{3}", new object[]
			{
				AimbotCoroutines.Yaw,
				AimbotCoroutines.Pitch,
				x,
				y
			}));
			AimbotCoroutines.Yaw = x;
			AimbotCoroutines.Pitch = y;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000D13C File Offset: 0x0000B33C
		public static Vector3 GetAimPosition(Transform parent, string name)
		{
			Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
			bool flag = componentsInChildren == null;
			bool flag2 = flag;
			Vector3 piVector;
			if (flag2)
			{
				piVector = AimbotCoroutines.PiVector;
			}
			else
			{
				foreach (Transform transform in componentsInChildren)
				{
					bool flag3 = transform.name.Trim() == name;
					bool flag4 = flag3;
					if (flag4)
					{
						return transform.position + new Vector3(0f, 0.4f, 0f);
					}
				}
				piVector = AimbotCoroutines.PiVector;
			}
			return piVector;
		}

		// Token: 0x04000202 RID: 514
		public static Vector3 PiVector = new Vector3(0f, 3.14159274f, 0f);

		// Token: 0x04000203 RID: 515
		public static GameObject LockedObject;

		// Token: 0x04000204 RID: 516
		public static bool IsAiming = false;

		// Token: 0x04000205 RID: 517
		public static FieldInfo PitchInfo;

		// Token: 0x04000206 RID: 518
		public static FieldInfo YawInfo;
	}
}

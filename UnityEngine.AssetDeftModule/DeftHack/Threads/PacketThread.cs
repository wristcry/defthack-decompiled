using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DeftHack.Components.Basic;
using DeftHack.Options;
using DeftHack.Overrides;
using SDG.Provider.Services.Community;
using SDG.SteamworksProvider.Services.Community;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace DeftHack.Threads
{
	// Token: 0x0200002F RID: 47
	public class PacketThread
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00002511 File Offset: 0x00000711
		public static void Reset()
		{
			PacketThread.Receivers.Clear();
			PacketThread.ReceiverCount = 0;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000087F8 File Offset: 0x000069F8
		public static void InitReceivers()
		{
			PacketThread.Reset();
			foreach (SteamChannel item in UnityEngine.Object.FindObjectsOfType<SteamChannel>())
			{
				PacketThread.Receivers.Add(item);
				PacketThread.ReceiverCount++;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008840 File Offset: 0x00006A40
		public static void Start()
		{
			Provider.onClientDisconnected = (Provider.ClientDisconnected)Delegate.Combine(Provider.onClientDisconnected, new Provider.ClientDisconnected(PacketThread.Reset));
			for (;;)
			{
				Thread.Sleep(OptimizationOptions.PacketRefreshRate);
				PacketThread.Listen(0);
				for (int i = 0; i < PacketThread.ReceiverCount; i++)
				{
					PacketThread.Listen(PacketThread.Receivers[i].id);
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000088B4 File Offset: 0x00006AB4
		public static void Listen(int channel)
		{
			ulong size;
			ICommunityEntity communityEntity;
			bool flag = !Provider.provider.multiplayerService.clientMultiplayerService.read(out communityEntity, PacketThread.PacketBuffer, out size, channel);
			bool flag2 = !flag;
			if (flag2)
			{
				bool flag3 = size > 1UL || size < 2UL;
				bool flag4 = !flag3;
				if (flag4)
				{
					CSteamID SteamID = ((SteamworksCommunityEntity)communityEntity).steamID;
					byte b = PacketThread.PacketBuffer[0];
					bool flag5 = b > 25;
					bool flag6 = !flag5;
					if (flag6)
					{
						ESteamPacket esteamPacket = (ESteamPacket)b;
						bool flag7 = SteamID != Provider.server;
						bool flag8 = flag7;
						if (flag8)
						{
							bool flag9 = esteamPacket != ESteamPacket.UPDATE_VOICE;
							bool flag10 = !flag9;
							if (flag10)
							{
								SteamChannel c = PacketThread.Receivers[channel];
								MainThreadDispatcherComponent.InvokeOnMain(delegate
								{
									c.receive(SteamID, PacketThread.PacketBuffer, 0, (int)size);
								});
							}
						}
						else
						{
							ESteamPacket esteamPacket2 = esteamPacket;
							bool flag11 = esteamPacket2 - ESteamPacket.UPDATE_RELIABLE_BUFFER > 7;
							if (flag11)
							{
								MainThreadDispatcherComponent.InvokeOnMain(delegate
								{
									OV_Provider.OV_receiveClient(SteamID, PacketThread.PacketBuffer, 0, (int)size, channel);
								});
							}
							else
							{
								SteamChannel c = PacketThread.Receivers.First((SteamChannel r) => r.id == channel);
								MainThreadDispatcherComponent.InvokeOnMain(delegate
								{
									c.receive(SteamID, PacketThread.PacketBuffer, 0, (int)size);
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x0400006A RID: 106
		public static byte[] PacketBuffer = new byte[65535];

		// Token: 0x0400006B RID: 107
		public static List<SteamChannel> Receivers = new List<SteamChannel>();

		// Token: 0x0400006C RID: 108
		public static int ReceiverCount = 0;
	}
}

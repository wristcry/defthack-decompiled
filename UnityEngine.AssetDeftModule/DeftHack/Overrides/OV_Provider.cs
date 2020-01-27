using System;
using System.Diagnostics;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Threads;
using DeftHack.Utilities;
using SDG.Unturned;
using Steamworks;

namespace DeftHack.Overrides
{
	// Token: 0x0200004C RID: 76
	public static class OV_Provider
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000B0D4 File Offset: 0x000092D4
		[Override(typeof(Provider), "receiveClient", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_receiveClient(CSteamID steamID, byte[] packet, int offset, int size, int channel)
		{
			bool flag = !OV_Provider.IsConnected;
			bool flag2 = flag;
			if (flag2)
			{
				OV_Provider.IsConnected = true;
				bool flag3 = Parser.getIPFromUInt32(Provider.currentServerInfo.ip) == "0.0.0.0";
				if (flag3)
				{
					Provider.disconnect();
				}
			}
			bool flag4 = ServerCrashThread.ServerCrashEnabled && packet[0] == 1;
			bool flag5 = !flag4;
			if (flag5)
			{
				bool flag6 = steamID != Provider.server && packet[0] != 23;
				bool flag7 = !flag6;
				if (flag7)
				{
					OverrideUtilities.CallOriginal(null, new object[]
					{
						steamID,
						packet,
						offset,
						size,
						channel
					});
				}
			}
		}

		// Token: 0x040000A8 RID: 168
		public static bool IsConnected;
	}
}

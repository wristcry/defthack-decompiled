using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Variables;
using SDG.Unturned;

namespace DeftHack.Overrides
{
	// Token: 0x0200003B RID: 59
	public class OV_ChatManager
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00008E68 File Offset: 0x00007068
		[Override(typeof(ChatManager), "sendChat", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_sendChat(EChatMode mode, string text)
		{
			bool flag = text.Contains("@tp");
			bool flag2 = flag;
			if (flag2)
			{
				string[] array = text.Split(new char[]
				{
					' '
				});
				bool flag3 = array.Length > 1;
				bool flag4 = flag3;
				if (flag4)
				{
					OptimizationVariables.MainPlayer.movement.transform.position = PlayerTool.getPlayer(array[1]).transform.position;
				}
			}
			else
			{
				bool flag5 = text.Contains("@day");
				bool flag6 = flag5;
				if (flag6)
				{
					LightingManager.time = 900u;
				}
				else
				{
					bool flag7 = text.Contains("@night");
					bool flag8 = flag7;
					if (flag8)
					{
						LightingManager.time = 0u;
					}
					else
					{
						ChatManager.instance.channel.send("askChat", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
						{
							(byte)mode,
							text
						});
					}
				}
			}
		}
	}
}

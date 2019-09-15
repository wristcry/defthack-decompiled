using System;
using DeftHack.Options;
using DeftHack.Options.VisualOptions;
using DeftHack.Variables;
using SDG.Unturned;

namespace DeftHack.Utilities
{
	// Token: 0x02000012 RID: 18
	public static class FriendUtilities
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00004E68 File Offset: 0x00003068
		public static bool IsFriendly(Player player)
		{
			return (player.quests.isMemberOfSameGroupAs(OptimizationVariables.MainPlayer) && ESPOptions.UsePlayerGroup) || MiscOptions.Friends.Contains(player.channel.owner.playerID.steamID.m_SteamID);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004EBC File Offset: 0x000030BC
		public static void AddFriend(Player Friend)
		{
			ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
			bool flag = !MiscOptions.Friends.Contains(steamID);
			bool flag2 = flag;
			if (flag2)
			{
				MiscOptions.Friends.Add(steamID);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004F08 File Offset: 0x00003108
		public static void RemoveFriend(Player Friend)
		{
			ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
			bool flag = MiscOptions.Friends.Contains(steamID);
			bool flag2 = flag;
			if (flag2)
			{
				MiscOptions.Friends.Remove(steamID);
			}
		}
	}
}

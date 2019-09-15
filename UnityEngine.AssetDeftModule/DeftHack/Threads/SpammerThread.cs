using System;
using System.Threading;
using DeftHack.Attributes;
using DeftHack.Options;
using SDG.Unturned;

namespace DeftHack.Threads
{
	// Token: 0x0200003A RID: 58
	public static class SpammerThread
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00008E30 File Offset: 0x00007030
		[Thread]
		public static void Spammer()
		{
			for (;;)
			{
				Thread.Sleep(MiscOptions.SpammerDelay);
				bool spammerEnabled = MiscOptions.SpammerEnabled;
				if (spammerEnabled)
				{
					ChatManager.sendChat(EChatMode.GLOBAL, MiscOptions.SpamText);
				}
			}
		}
	}
}

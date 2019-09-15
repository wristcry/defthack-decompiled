using System;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Variables;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace DeftHack.Overrides
{
	// Token: 0x02000043 RID: 67
	public class OV_Player : MonoBehaviour
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00009304 File Offset: 0x00007504
		[Override(typeof(Player), "askScreenshot", BindingFlags.Instance | BindingFlags.Public, 0)]
		public void OV_askScreenshot(CSteamID steamid)
		{
			bool flag = OptimizationVariables.MainPlayer.channel.checkServer(steamid);
			bool flag2 = flag;
			if (flag2)
			{
				base.StartCoroutine(PlayerCoroutines.TakeScreenshot());
			}
		}
	}
}

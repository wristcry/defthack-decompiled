using System;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using DeftHack.Options;
using DeftHack.Utilities;
using DeftHack.Variables;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000C5 RID: 197
	[Component]
	public class SpectatorComponent : MonoBehaviour
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public void FixedUpdate()
		{
			bool flag = !DrawUtilities.ShouldRun();
			bool flag2 = !flag;
			if (flag2)
			{
				bool flag3 = MiscOptions.SpectatedPlayer != null && !PlayerCoroutines.IsSpying;
				bool flag4 = flag3;
				if (flag4)
				{
					OptimizationVariables.MainPlayer.look.isOrbiting = true;
					OptimizationVariables.MainPlayer.look.orbitPosition = MiscOptions.SpectatedPlayer.transform.position - OptimizationVariables.MainPlayer.transform.position;
					OptimizationVariables.MainPlayer.look.orbitPosition += new Vector3(0f, 3f, 0f);
				}
				else
				{
					OptimizationVariables.MainPlayer.look.isOrbiting = MiscOptions.Freecam;
				}
			}
		}
	}
}

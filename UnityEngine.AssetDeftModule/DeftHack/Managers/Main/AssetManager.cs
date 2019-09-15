using System;
using DeftHack.Components.Basic;
using DeftHack.Coroutines;
using DeftHackFree;

namespace DeftHack.Managers.Main
{
	// Token: 0x0200007A RID: 122
	public static class AssetManager
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000029E5 File Offset: 0x00000BE5
		public static void Init()
		{
			abc.HookObject.GetComponent<CoroutineComponent>().StartCoroutine(LoaderCoroutines.LoadAssets());
		}
	}
}

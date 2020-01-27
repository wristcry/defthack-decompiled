using System;
using System.IO;
using System.Threading;
using DeftHack.Components.UI.Menu;
using DeftHack.Managers.Main;
using DeftHack.Utilities;
using UnityEngine;

namespace DeftHackFree
{
	// Token: 0x020000D7 RID: 215
	public static class abc
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0001E90C File Offset: 0x0001CB0C
		public static void bc()
		{
			if (File.Exists("Unturned_Data\\Managed\\Pathfinding.CSharpFx.xml"))
			{
				File.Delete("Unturned_Data\\Managed\\Pathfinding.CSharpFx.xml");
			}
			abc.HookObject = new GameObject();
			UnityEngine.Object.DontDestroyOnLoad(abc.HookObject);
			try
			{
				ConfigManager.Init();
				AttributeManager.Init();
				AssetManager.Init();
				ConfigManager.Init();
				//MenuComponent.SetGUIColors();
				SkinsUtilities.ApplyFromConfig();
			}
			catch (Exception exception)
			{
				DebugUtilities.LogException(exception);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001E97C File Offset: 0x0001CB7C
		public static void HookThread()
		{
			for (;;)
			{
				bool flag = abc.HookObject == null;
				if (flag)
				{
					abc.bc();
				}
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00003577 File Offset: 0x00001777
		public static void Thread()
		{
			new Thread(new ThreadStart(abc.HookThread)).Start();
		}

		// Token: 0x0400042D RID: 1069
		public static GameObject HookObject;
	}
}

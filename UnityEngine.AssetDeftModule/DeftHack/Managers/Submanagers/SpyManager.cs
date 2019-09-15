using System;
using System.Collections.Generic;
using System.Reflection;
using DeftHackFree;
using UnityEngine;

namespace DeftHack.Managers.Submanagers
{
	// Token: 0x02000079 RID: 121
	public class SpyManager
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000C510 File Offset: 0x0000A710
		public static void InvokePre()
		{
			foreach (MethodInfo methodInfo in SpyManager.PreSpy)
			{
				methodInfo.Invoke(null, null);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000C564 File Offset: 0x0000A764
		public static void InvokePost()
		{
			foreach (MethodInfo methodInfo in SpyManager.PostSpy)
			{
				methodInfo.Invoke(null, null);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		public static void DestroyComponents()
		{
			foreach (Type type in SpyManager.Components)
			{
				UnityEngine.Object.Destroy(abc.HookObject.GetComponent(type));
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C614 File Offset: 0x0000A814
		public static void AddComponents()
		{
			foreach (Type componentType in SpyManager.Components)
			{
				abc.HookObject.AddComponent(componentType);
			}
		}

		// Token: 0x040001F7 RID: 503
		public static IEnumerable<MethodInfo> PreSpy;

		// Token: 0x040001F8 RID: 504
		public static IEnumerable<Type> Components;

		// Token: 0x040001F9 RID: 505
		public static IEnumerable<MethodInfo> PostSpy;
	}
}

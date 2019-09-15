using System;
using System.Collections.Generic;
using DeftHack.Attributes;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000BC RID: 188
	[Component]
	public class MainThreadDispatcherComponent : MonoBehaviour
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0001BC50 File Offset: 0x00019E50
		public void Update()
		{
			Queue<Action> methodQueue = MainThreadDispatcherComponent.MethodQueue;
			Queue<Action> obj = methodQueue;
			lock (obj)
			{
				while (MainThreadDispatcherComponent.MethodQueue.Count > 0)
				{
					MainThreadDispatcherComponent.MethodQueue.Dequeue()();
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001BCB8 File Offset: 0x00019EB8
		public static void InvokeOnMain(Action a)
		{
			Queue<Action> methodQueue = MainThreadDispatcherComponent.MethodQueue;
			Queue<Action> obj = methodQueue;
			lock (obj)
			{
				MainThreadDispatcherComponent.MethodQueue.Enqueue(a);
			}
		}

		// Token: 0x040003CB RID: 971
		private static readonly Queue<Action> MethodQueue = new Queue<Action>();
	}
}

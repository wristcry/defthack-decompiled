using System;
using DeftHack.Attributes;
using DeftHack.Coroutines;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000B8 RID: 184
	[Component]
	public class AimbotComponent : MonoBehaviour
	{
		// Token: 0x060002AF RID: 687 RVA: 0x000030E6 File Offset: 0x000012E6
		public void Start()
		{
			CoroutineComponent.LockCoroutine = base.StartCoroutine(AimbotCoroutines.SetLockedObject());
			CoroutineComponent.AimbotCoroutine = base.StartCoroutine(AimbotCoroutines.AimToObject());
			base.StartCoroutine(RaycastCoroutines.UpdateObjects());
		}
	}
}

using System;
using System.Collections;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities.Mesh_Utilities;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000C2 RID: 194
	[DisallowMultipleComponent]
	public class RaycastComponent : MonoBehaviour
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0000338E File Offset: 0x0000158E
		private void Awake()
		{
			base.StartCoroutine(this.RedoSphere());
			base.StartCoroutine(this.CalcSphere());
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000033AB File Offset: 0x000015AB
		private IEnumerator CalcSphere()
		{
			for (;;)
			{
				yield return new WaitForSeconds(0.1f);
				bool flag = this.Sphere;
				bool flag3 = flag;
				if (flag3)
				{
					Rigidbody rb = base.gameObject.GetComponent<Rigidbody>();
					bool flag2 = rb;
					bool flag4 = flag2;
					if (flag4)
					{
						float sizeBias = 1f - Provider.ping * rb.velocity.magnitude * 2f;
						this.Sphere.transform.localScale = new Vector3(sizeBias, sizeBias, sizeBias);
					}
					rb = null;
					rb = null;
				}
			}
			yield break;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000033BA File Offset: 0x000015BA
		private IEnumerator RedoSphere()
		{
			for (;;)
			{
				GameObject tmp = this.Sphere;
				this.Sphere = IcoSphere.Create("HitSphere", SphereOptions.SpherePrediction ? 15.5f : SphereOptions.SphereRadius, (float)SphereOptions.RecursionLevel);
				this.Sphere.layer = LayerMasks.AGENT;
				this.Sphere.transform.parent = base.transform;
				this.Sphere.transform.localPosition = Vector3.zero;
				UnityEngine.Object.Destroy(tmp);
				yield return new WaitForSeconds(1f);
				tmp = null;
				tmp = null;
			}
			yield break;
		}

		// Token: 0x040003F4 RID: 1012
		public GameObject Sphere;
	}
}

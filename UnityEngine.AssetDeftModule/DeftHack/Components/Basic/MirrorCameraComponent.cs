using System;
using DeftHack.Attributes;
using DeftHack.Components.UI.Menu;
using DeftHack.Options.VisualOptions;
using DeftHack.Variables;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000BD RID: 189
	[Component]
	public class MirrorCameraComponent : MonoBehaviour
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00003184 File Offset: 0x00001384
		[OnSpy]
		public static void Disable()
		{
			MirrorCameraComponent.WasEnabled = MirrorCameraOptions.Enabled;
			MirrorCameraOptions.Enabled = false;
			UnityEngine.Object.Destroy(MirrorCameraComponent.cam_obj);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000031A2 File Offset: 0x000013A2
		[OffSpy]
		public static void Enable()
		{
			MirrorCameraOptions.Enabled = MirrorCameraComponent.WasEnabled;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0001BD04 File Offset: 0x00019F04
		public void Update()
		{
			bool flag = !MirrorCameraComponent.cam_obj || !MirrorCameraComponent.subCam;
			bool flag2 = !flag;
			if (flag2)
			{
				bool enabled = MirrorCameraOptions.Enabled;
				bool flag3 = enabled;
				if (flag3)
				{
					MirrorCameraComponent.subCam.enabled = true;
				}
				else
				{
					MirrorCameraComponent.subCam.enabled = false;
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00002EAD File Offset: 0x000010AD
		private void Start()
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001BD64 File Offset: 0x00019F64
		private void OnGUI()
		{
			bool enabled = MirrorCameraOptions.Enabled;
			bool flag = enabled;
			if (flag)
			{
				GUI.color = new Color(1f, 1f, 1f, 0f);
				MirrorCameraComponent.viewport = GUILayout.Window(99, MirrorCameraComponent.viewport, new GUI.WindowFunction(this.DoMenu), "Mirror Camera", Array.Empty<GUILayoutOption>());
				GUI.color = Color.white;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		private void DoMenu(int windowID)
		{
			bool flag = MirrorCameraComponent.cam_obj == null || MirrorCameraComponent.subCam == null;
			bool flag2 = flag;
			if (flag2)
			{
				MirrorCameraComponent.cam_obj = new GameObject();
				bool flag3 = MirrorCameraComponent.subCam != null;
				bool flag4 = flag3;
				if (flag4)
				{
					UnityEngine.Object.Destroy(MirrorCameraComponent.subCam);
				}
				MirrorCameraComponent.subCam = MirrorCameraComponent.cam_obj.AddComponent<Camera>();
				MirrorCameraComponent.subCam.CopyFrom(OptimizationVariables.MainCam);
				MirrorCameraComponent.cam_obj.AddComponent<GUILayer>();
				MirrorCameraComponent.cam_obj.transform.position = OptimizationVariables.MainCam.gameObject.transform.position;
				MirrorCameraComponent.cam_obj.transform.rotation = OptimizationVariables.MainCam.gameObject.transform.rotation;
				MirrorCameraComponent.cam_obj.transform.Rotate(0f, 180f, 0f);
				MirrorCameraComponent.subCam.transform.SetParent(OptimizationVariables.MainCam.transform, true);
				MirrorCameraComponent.subCam.enabled = true;
				MirrorCameraComponent.subCam.rect = new Rect(0.6f, 0.6f, 0.4f, 0.4f);
				MirrorCameraComponent.subCam.depth = 99f;
				UnityEngine.Object.DontDestroyOnLoad(MirrorCameraComponent.cam_obj);
			}
			float x = MirrorCameraComponent.viewport.x / (float)Screen.width;
			float num = (MirrorCameraComponent.viewport.y + 25f) / (float)Screen.height;
			float width = MirrorCameraComponent.viewport.width / (float)Screen.width;
			float num2 = MirrorCameraComponent.viewport.height / (float)Screen.height;
			num = 1f - num;
			num -= num2;
			MirrorCameraComponent.subCam.rect = new Rect(x, num, width, num2);
			Drawing.DrawRect(new Rect(0f, 0f, MirrorCameraComponent.viewport.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 20f, MirrorCameraComponent.viewport.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
			GUILayout.Space(-19f);
			GUILayout.Label("Mirror Camera", Array.Empty<GUILayoutOption>());
			GUI.DragWindow();
		}

		// Token: 0x040003CC RID: 972
		public static Rect viewport = new Rect(1075f, 10f, (float)(Screen.width / 4), (float)(Screen.height / 4));

		// Token: 0x040003CD RID: 973
		public static GameObject cam_obj;

		// Token: 0x040003CE RID: 974
		public static Camera subCam;

		// Token: 0x040003CF RID: 975
		public static bool WasEnabled;
	}
}

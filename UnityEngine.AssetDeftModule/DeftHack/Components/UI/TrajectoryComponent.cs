using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Options.AimOptions;
using DeftHack.Utilities;
using DeftHack.Variables;
using DeftHack.Variables.UIVariables;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Components.UI
{
	// Token: 0x02000094 RID: 148
	[Component]
	public class TrajectoryComponent : MonoBehaviour
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00002D9F File Offset: 0x00000F9F
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public static Highlighter Highlighted { get; private set; }

		// Token: 0x060001F2 RID: 498 RVA: 0x000128B8 File Offset: 0x00010AB8
		[Initializer]
		public static void Initialize()
		{
			ColorUtilities.addColor(new ColorVariable("_TrajectoryPredictionInRange", "B.D. Predict (In Range)", Color.cyan, true));
			ColorUtilities.addColor(new ColorVariable("_TrajectoryPredictionOutOfRange", "B.D. Predict (Out of Range)", Color.red, true));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00012908 File Offset: 0x00010B08
		public void OnGUI()
		{
			Player mainPlayer = OptimizationVariables.MainPlayer;
			bool flag = mainPlayer == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				PlayerEquipment equipment = mainPlayer.equipment;
				obj = ((equipment != null) ? equipment.useable : null);
			}
			UseableGun useableGun = obj as UseableGun;
			bool flag2 = useableGun == null || TrajectoryComponent.spying || !WeaponOptions.EnableBulletDropPrediction || !Provider.modeConfigData.Gameplay.Ballistics;
			bool flag3 = flag2;
			if (flag3)
			{
				bool flag4 = TrajectoryComponent.Highlighted != null;
				bool flag5 = flag4;
				if (flag5)
				{
					TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
					TrajectoryComponent.Highlighted = null;
				}
			}
			else
			{
				RaycastHit raycastHit;
				List<Vector3> list = TrajectoryComponent.PlotTrajectory(useableGun, out raycastHit, 255);
				bool flag6 = Vector3.Distance(list.Last<Vector3>(), OptimizationVariables.MainPlayer.look.aim.position) > useableGun.equippedGunAsset.range;
				ColorVariable color = ColorUtilities.getColor("_TrajectoryPredictionInRange");
				ColorVariable color2 = ColorUtilities.getColor("_TrajectoryPredictionOutOfRange");
				bool flag7 = WeaponOptions.HighlightBulletDropPredictionTarget && raycastHit.collider != null;
				bool flag8 = flag7;
				if (flag8)
				{
					Transform transform = raycastHit.transform;
					GameObject gameObject = null;
					bool flag9 = DamageTool.getPlayer(transform) != null;
					bool flag10 = flag9;
					if (flag10)
					{
						gameObject = DamageTool.getPlayer(transform).gameObject;
					}
					else
					{
						bool flag11 = DamageTool.getZombie(transform) != null;
						bool flag12 = flag11;
						if (flag12)
						{
							gameObject = DamageTool.getZombie(transform).gameObject;
						}
						else
						{
							bool flag13 = DamageTool.getAnimal(transform) != null;
							bool flag14 = flag13;
							if (flag14)
							{
								gameObject = DamageTool.getAnimal(transform).gameObject;
							}
							else
							{
								bool flag15 = DamageTool.getVehicle(transform) != null;
								bool flag16 = flag15;
								if (flag16)
								{
									gameObject = DamageTool.getVehicle(transform).gameObject;
								}
							}
						}
					}
					bool flag17 = gameObject != null;
					bool flag18 = flag17;
					if (flag18)
					{
						Highlighter highlighter = gameObject.GetComponent<Highlighter>() ?? gameObject.AddComponent<Highlighter>();
						bool flag19 = !highlighter.enabled;
						bool flag20 = flag19;
						if (flag20)
						{
							highlighter.occluder = true;
							highlighter.overlay = true;
							highlighter.ConstantOnImmediate(flag6 ? color2 : color);
						}
						bool flag21 = TrajectoryComponent.Highlighted != null && highlighter != TrajectoryComponent.Highlighted;
						bool flag22 = flag21;
						if (flag22)
						{
							TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
						}
						TrajectoryComponent.Highlighted = highlighter;
					}
					else
					{
						bool flag23 = TrajectoryComponent.Highlighted != null;
						bool flag24 = flag23;
						if (flag24)
						{
							TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
							TrajectoryComponent.Highlighted = null;
						}
					}
				}
				else
				{
					bool flag25 = !WeaponOptions.HighlightBulletDropPredictionTarget && TrajectoryComponent.Highlighted != null;
					bool flag26 = flag25;
					if (flag26)
					{
						TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
						TrajectoryComponent.Highlighted = null;
					}
				}
				ESPComponent.GLMat.SetPass(0);
				GL.PushMatrix();
				GL.LoadProjectionMatrix(OptimizationVariables.MainCam.projectionMatrix);
				GL.modelview = OptimizationVariables.MainCam.worldToCameraMatrix;
				GL.Begin(2);
				GL.Color(flag6 ? color2 : color);
				foreach (Vector3 v in list)
				{
					GL.Vertex(v);
				}
				GL.End();
				GL.PopMatrix();
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00012C98 File Offset: 0x00010E98
		private static void RemoveHighlight(Highlighter h)
		{
			bool flag = h == null;
			bool flag2 = !flag;
			if (flag2)
			{
				h.occluder = false;
				h.overlay = false;
				h.ConstantOffImmediate();
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public static List<Vector3> PlotTrajectory(UseableGun gun, out RaycastHit hit, int maxSteps = 255)
		{
			hit = default(RaycastHit);
			Transform transform = (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.FIRST) ? OptimizationVariables.MainPlayer.look.aim : OptimizationVariables.MainCam.transform;
			Vector3 vector = transform.position;
			Vector3 forward = transform.forward;
			ItemGunAsset equippedGunAsset = gun.equippedGunAsset;
			float num = equippedGunAsset.ballisticDrop;
			Attachments attachments = (Attachments)TrajectoryComponent.thirdAttachments.GetValue(gun);
			List<Vector3> list = new List<Vector3>
			{
				vector
			};
			bool flag = ((attachments != null) ? attachments.barrelAsset : null) != null;
			bool flag2 = flag;
			if (flag2)
			{
				num *= attachments.barrelAsset.ballisticDrop;
			}
			for (int i = 1; i < maxSteps; i++)
			{
				vector += forward * equippedGunAsset.ballisticTravel;
				forward.y -= num;
				forward.Normalize();
				bool flag3 = Physics.Linecast(list[i - 1], vector, out hit, RayMasks.DAMAGE_CLIENT);
				bool flag4 = flag3;
				if (flag4)
				{
					list.Add(hit.point);
					break;
				}
				list.Add(vector);
			}
			return list;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00012E08 File Offset: 0x00011008
		[OnSpy]
		public static void OnSpy()
		{
			bool flag = TrajectoryComponent.Highlighted != null;
			bool flag2 = flag;
			if (flag2)
			{
				TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
			}
			TrajectoryComponent.spying = true;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00002DAE File Offset: 0x00000FAE
		[OffSpy]
		public static void OffSpy()
		{
			TrajectoryComponent.spying = false;
		}

		// Token: 0x0400030F RID: 783
		private static readonly FieldInfo thirdAttachments = typeof(UseableGun).GetField("thirdAttachments", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000310 RID: 784
		private static bool spying;
	}
}

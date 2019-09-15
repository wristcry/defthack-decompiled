using System;
using DeftHack.Components.UI.Menu;
using DeftHack.Misc.Classes.ESP;
using DeftHack.Misc.Enums;
using DeftHack.Variables;
using SDG.Unturned;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x02000011 RID: 17
	public static class DrawUtilities
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00003B84 File Offset: 0x00001D84
		public static bool ShouldRun()
		{
			return Provider.isConnected && !Provider.isLoading && !LoadingUI.isBlocked && !(OptimizationVariables.MainPlayer == null);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003BBC File Offset: 0x00001DBC
		public static void DrawLine(HumanBodyBones pointA, HumanBodyBones pointB, Color color, float width, bool antiAlias)
		{
			Texture2D image;
			if (antiAlias)
			{
				width *= 3f;
				image = DrawUtilities.aaLineTex;
				Material material = DrawUtilities.blendMaterial;
			}
			else
			{
				image = DrawUtilities.lineTex;
				Material material2 = DrawUtilities.blitMaterial;
			}
			Matrix4x4 identity = Matrix4x4.identity;
			GL.PushMatrix();
			GL.MultMatrix(identity);
			GUI.color = color;
			GUI.DrawTexture(DrawUtilities.lineRect, image);
			GL.PopMatrix();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003C28 File Offset: 0x00001E28
		public static int GetTextSize(ESPVisual vis, double dist)
		{
			bool flag = !vis.TextScaling;
			bool flag2 = flag;
			int result;
			if (flag2)
			{
				result = vis.FixedTextSize;
			}
			else
			{
				bool flag3 = dist > (double)vis.MinTextSizeDistance;
				bool flag4 = flag3;
				if (flag4)
				{
					result = vis.MinTextSize;
				}
				else
				{
					int num = vis.MaxTextSize - vis.MinTextSize;
					double num2 = (double)(vis.MinTextSizeDistance / (float)num);
					result = vis.MaxTextSize - (int)(dist / num2);
				}
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public static Vector2[] GetRectangleLines(Camera cam, Bounds b, Color c)
		{
			Vector3[] array = new Vector3[]
			{
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z))
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].y = (float)Screen.height - array[i].y;
			}
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int j = 1; j < array.Length; j++)
			{
				vector = Vector3.Min(vector, array[j]);
				vector2 = Vector3.Max(vector2, array[j]);
			}
			return new Vector2[]
			{
				new Vector2(vector.x, vector.y),
				new Vector2(vector2.x, vector.y),
				new Vector2(vector.x, vector2.y),
				new Vector2(vector2.x, vector2.y)
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000408C File Offset: 0x0000228C
		public static Bounds GetBoundsRecursively(GameObject go)
		{
			Bounds result = default(Bounds);
			Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				result.Encapsulate(componentsInChildren[i].bounds);
			}
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000040D4 File Offset: 0x000022D4
		public static Bounds TransformBounds(Transform _transform, Bounds _localBounds)
		{
			Vector3 center = _transform.TransformPoint(_localBounds.center);
			Vector3 extents = _localBounds.extents;
			Vector3 vector = _transform.TransformVector(extents.x, 0f, 0f);
			Vector3 vector2 = _transform.TransformVector(0f, extents.y, 0f);
			Vector3 vector3 = _transform.TransformVector(0f, 0f, extents.z);
			extents.x = Mathf.Abs(vector.x) + Mathf.Abs(vector2.x) + Mathf.Abs(vector3.x);
			extents.y = Mathf.Abs(vector.y) + Mathf.Abs(vector2.y) + Mathf.Abs(vector3.y);
			extents.z = Mathf.Abs(vector.z) + Mathf.Abs(vector2.z) + Mathf.Abs(vector3.z);
			return new Bounds
			{
				center = center,
				extents = extents
			};
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000041E4 File Offset: 0x000023E4
		public static void DrawTextWithOutline(Rect centerRect, string text, GUIStyle style, Color innerColor, Color borderColor, int borderWidth, string outlineText = null)
		{
			bool flag = outlineText == null;
			bool flag2 = flag;
			if (flag2)
			{
				outlineText = text;
			}
			style.normal.textColor = borderColor;
			Rect position = centerRect;
			position.x -= (float)borderWidth;
			position.y -= (float)borderWidth;
			GUI.Label(position, text, style);
			while (position.x <= centerRect.x + (float)borderWidth)
			{
				float x = position.x;
				position.x = x + 1f;
				GUI.Label(position, outlineText, style);
			}
			while (position.y <= centerRect.y + (float)borderWidth)
			{
				float y = position.y;
				position.y = y + 1f;
				GUI.Label(position, outlineText, style);
			}
			while (position.x >= centerRect.x - (float)borderWidth)
			{
				float x2 = position.x;
				position.x = x2 - 1f;
				GUI.Label(position, outlineText, style);
			}
			while (position.y >= centerRect.y - (float)borderWidth)
			{
				float y2 = position.y;
				position.y = y2 - 1f;
				GUI.Label(position, outlineText, style);
			}
			style.normal.textColor = innerColor;
			GUI.Label(centerRect, text, style);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004364 File Offset: 0x00002564
		public static Vector2 InvertScreenSpace(Vector2 dim)
		{
			return new Vector2(dim.x, (float)Screen.height - dim.y);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004390 File Offset: 0x00002590
		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000043EC File Offset: 0x000025EC
		public static void DrawLabel(Font Font, LabelLocation Location, Vector2 W2SVector, string Content, Color BorderColor, Color InnerColor, int BorderWidth, string outerContent = null, int fontSize = 12)
		{
			GUIContent guicontent = new GUIContent(Content);
			GUIStyle guistyle = new GUIStyle
			{
				font = Font,
				fontSize = fontSize
			};
			Vector2 vector = guistyle.CalcSize(guicontent);
			float x = vector.x;
			float y = vector.y;
			Rect centerRect = new Rect(0f, 0f, x, y);
			switch (Location)
			{
			case LabelLocation.TopRight:
				centerRect.x = W2SVector.x;
				centerRect.y = W2SVector.y - y;
				guistyle.alignment = TextAnchor.UpperLeft;
				break;
			case LabelLocation.TopMiddle:
				centerRect.x = W2SVector.x - x / 2f;
				centerRect.y = W2SVector.y - y;
				guistyle.alignment = TextAnchor.LowerCenter;
				break;
			case LabelLocation.TopLeft:
				centerRect.x = W2SVector.x - x;
				centerRect.y = W2SVector.y - y;
				guistyle.alignment = TextAnchor.UpperRight;
				break;
			case LabelLocation.MiddleRight:
				centerRect.x = W2SVector.x;
				centerRect.y = W2SVector.y - y / 2f;
				guistyle.alignment = TextAnchor.MiddleLeft;
				break;
			case LabelLocation.Center:
				centerRect.x = W2SVector.x - x / 2f;
				centerRect.y = W2SVector.y - y / 2f;
				guistyle.alignment = TextAnchor.MiddleCenter;
				break;
			case LabelLocation.MiddleLeft:
				centerRect.x = W2SVector.x - x;
				centerRect.y = W2SVector.y - y / 2f;
				guistyle.alignment = TextAnchor.MiddleRight;
				break;
			case LabelLocation.BottomRight:
				centerRect.x = W2SVector.x;
				centerRect.y = W2SVector.y;
				guistyle.alignment = TextAnchor.LowerLeft;
				break;
			case LabelLocation.BottomMiddle:
				centerRect.x = W2SVector.x - x / 2f;
				centerRect.y = W2SVector.y;
				guistyle.alignment = TextAnchor.UpperCenter;
				break;
			case LabelLocation.BottomLeft:
				centerRect.x = W2SVector.x - x;
				centerRect.y = W2SVector.y;
				guistyle.alignment = TextAnchor.LowerRight;
				break;
			}
			bool flag = centerRect.x - 10f < 0f || centerRect.y - 10f < 0f;
			bool flag2 = !flag;
			if (flag2)
			{
				bool flag3 = centerRect.x + 10f > (float)Screen.width || centerRect.y + 10f > (float)Screen.height;
				bool flag4 = !flag3;
				if (flag4)
				{
					DrawUtilities.DrawTextWithOutline(centerRect, guicontent.text, guistyle, BorderColor, InnerColor, BorderWidth, outerContent);
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000046AC File Offset: 0x000028AC
		public static Vector2 Get3DW2SVector(Camera cam, Bounds b, LabelLocation location)
		{
			Vector2 result;
			switch (location)
			{
			case LabelLocation.TopRight:
			case LabelLocation.TopMiddle:
			case LabelLocation.TopLeft:
				result = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(new Vector3(b.center.x, b.center.y + b.extents.y, b.center.z)));
				break;
			case LabelLocation.MiddleRight:
			case LabelLocation.Center:
			case LabelLocation.MiddleLeft:
				result = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(b.center));
				break;
			case LabelLocation.BottomRight:
			case LabelLocation.BottomMiddle:
			case LabelLocation.BottomLeft:
				result = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(new Vector3(b.center.x, b.center.y - b.extents.y, b.center.z)));
				break;
			default:
				result = Vector2.zero;
				break;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000047A4 File Offset: 0x000029A4
		public static Vector2 Get2DW2SVector(Camera cam, Vector2[] Corners, LabelLocation location)
		{
			Vector2 result;
			switch (location)
			{
			case LabelLocation.TopRight:
				result = Corners[1];
				break;
			case LabelLocation.TopMiddle:
				result = new Vector2((Corners[0].x + Corners[1].x) / 2f, Corners[0].y);
				break;
			case LabelLocation.TopLeft:
				result = Corners[0];
				break;
			case LabelLocation.MiddleRight:
				result = new Vector2(Corners[0].x, (Corners[1].y + Corners[2].y) / 2f);
				break;
			case LabelLocation.Center:
				result = new Vector2(Corners[2].x, (Corners[1].y + Corners[2].y) / 2f);
				break;
			case LabelLocation.MiddleLeft:
				result = new Vector2((Corners[2].x + Corners[3].x) / 2f, (Corners[1].y + Corners[2].y) / 2f);
				break;
			case LabelLocation.BottomRight:
				result = Corners[2];
				break;
			case LabelLocation.BottomMiddle:
				result = new Vector2((Corners[2].x + Corners[3].x) / 2f, Corners[2].y);
				break;
			case LabelLocation.BottomLeft:
				result = Corners[3];
				break;
			default:
				result = Vector2.zero;
				break;
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004940 File Offset: 0x00002B40
		public static Vector3[] GetBoxVectors(Bounds b)
		{
			Vector3 center = b.center;
			Vector3 extents = b.extents;
			return new Vector3[]
			{
				new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z),
				new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z),
				new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z),
				new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z),
				new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z),
				new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z),
				new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z),
				new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z)
			};
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004B00 File Offset: 0x00002D00
		public static void PrepareRectangleLines(Vector2[] nvectors, Color c)
		{
			ESPVariables.DrawBuffer2.Enqueue(new ESPBox2
			{
				Color = c,
				Vertices = new Vector2[]
				{
					nvectors[0],
					nvectors[1],
					nvectors[1],
					nvectors[3],
					nvectors[3],
					nvectors[2],
					nvectors[2],
					nvectors[0]
				}
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public static void PrepareBoxLines(Vector3[] vectors, Color c)
		{
			ESPVariables.DrawBuffer.Enqueue(new ESPBox
			{
				Color = c,
				Vertices = new Vector3[]
				{
					vectors[0],
					vectors[1],
					vectors[1],
					vectors[3],
					vectors[3],
					vectors[2],
					vectors[2],
					vectors[0],
					vectors[4],
					vectors[5],
					vectors[5],
					vectors[7],
					vectors[7],
					vectors[6],
					vectors[6],
					vectors[4],
					vectors[0],
					vectors[4],
					vectors[1],
					vectors[5],
					vectors[2],
					vectors[6],
					vectors[3],
					vectors[7]
				}
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004D38 File Offset: 0x00002F38
		public static void DrawCircle(Material Mat, Color Col, Vector2 Center, float Radius)
		{
			GL.PushMatrix();
			Mat.SetPass(0);
			GL.Begin(1);
			GL.Color(Col);
			for (float num = 0f; num < 6.28318548f; num += 0.05f)
			{
				Vector2 v = new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y);
				GL.Vertex(v);
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public static void DrawMenuRect(float x, float y, float width, float height, Color fillcolor)
		{
			Color black = Color.black;
			Drawing.DrawRect(new Rect(x, y, width, 5f), black, null);
			Drawing.DrawRect(new Rect(x, y, 5f, height), black, null);
			Drawing.DrawRect(new Rect(x, y + (height - 5f), width, 5f), black, null);
			Drawing.DrawRect(new Rect(x + (width - 5f), 0f, 5f, height), black, null);
			Drawing.DrawRect(new Rect(5f, 5f, width - 10f, height - 10f), fillcolor, null);
		}

		// Token: 0x04000037 RID: 55
		private static Texture2D aaLineTex = null;

		// Token: 0x04000038 RID: 56
		private static Material blendMaterial = null;

		// Token: 0x04000039 RID: 57
		private static Texture2D lineTex = null;

		// Token: 0x0400003A RID: 58
		private static Material blitMaterial = null;

		// Token: 0x0400003B RID: 59
		private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);
	}
}

using System;
using UnityEngine;

namespace DeftHack.Components.UI.Menu
{
	// Token: 0x02000098 RID: 152
	public static class Drawing
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0001386C File Offset: 0x00011A6C
		public static void DrawRect(Rect position, Color color, GUIContent content = null)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUI.Box(position, content ?? GUIContent.none, Drawing.textureStyle);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000138A4 File Offset: 0x00011AA4
		public static void LayoutBox(Color color, GUIContent content = null)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUILayout.Box(content ?? GUIContent.none, Drawing.textureStyle, Array.Empty<GUILayoutOption>());
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x04000326 RID: 806
		private static readonly Texture2D backgroundTexture = Texture2D.whiteTexture;

		// Token: 0x04000327 RID: 807
		private static readonly GUIStyle textureStyle = new GUIStyle
		{
			normal = new GUIStyleState
			{
				background = Drawing.backgroundTexture
			}
		};
	}
}

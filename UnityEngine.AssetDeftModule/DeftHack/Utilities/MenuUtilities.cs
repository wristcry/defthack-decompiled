using System;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x0200001E RID: 30
	public static class MenuUtilities
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000023B8 File Offset: 0x000005B8
		static MenuUtilities()
		{
			MenuUtilities.TexClear.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			MenuUtilities.TexClear.Apply();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006308 File Offset: 0x00004508
		public static void FixGUIStyleColor(GUIStyle style)
		{
			style.normal.background = MenuUtilities.TexClear;
			style.onNormal.background = MenuUtilities.TexClear;
			style.hover.background = MenuUtilities.TexClear;
			style.onHover.background = MenuUtilities.TexClear;
			style.active.background = MenuUtilities.TexClear;
			style.onActive.background = MenuUtilities.TexClear;
			style.focused.background = MenuUtilities.TexClear;
			style.onFocused.background = MenuUtilities.TexClear;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000063A0 File Offset: 0x000045A0
		public static Rect Inline(Rect rect, int border = 1)
		{
			Rect result = new Rect(rect.x + (float)border, rect.y + (float)border, rect.width - (float)(border * 2), rect.height - (float)(border * 2));
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000063E8 File Offset: 0x000045E8
		public static Rect AbsRect(Vector2 pos1, Vector2 pos2)
		{
			return MenuUtilities.AbsRect(pos1.x, pos1.y, pos2.x, pos2.y);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006418 File Offset: 0x00004618
		public static Rect AbsRect(float x1, float y1, float x2, float y2)
		{
			float width = y2 - y1;
			float height = x2 - x1;
			return new Rect(x1, y1, width, height);
		}

		// Token: 0x0400004B RID: 75
		public static Texture2D TexClear = new Texture2D(1, 1);
	}
}

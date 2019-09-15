using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeftHack.Variables
{
	// Token: 0x02000003 RID: 3
	public static class AssetVariables
	{
		// Token: 0x0400000A RID: 10
		public static AssetBundle ABundle;

		// Token: 0x0400000B RID: 11
		public static Dictionary<string, Material> Materials = new Dictionary<string, Material>();

		// Token: 0x0400000C RID: 12
		public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

		// Token: 0x0400000D RID: 13
		public static Dictionary<string, AudioClip> Audio = new Dictionary<string, AudioClip>();

		// Token: 0x0400000E RID: 14
		public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

		// Token: 0x0400000F RID: 15
		public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();
	}
}

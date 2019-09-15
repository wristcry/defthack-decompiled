using System;
using System.Collections.Generic;
using DeftHack.Attributes;
using DeftHack.Variables.UIVariables;
using UnityEngine;

namespace DeftHack.Options.VisualOptions
{
	// Token: 0x02000054 RID: 84
	public static class ColorOptions
	{
		// Token: 0x040000F0 RID: 240
		[Save]
		public static Dictionary<string, ColorVariable> ColorDict = new Dictionary<string, ColorVariable>();

		// Token: 0x040000F1 RID: 241
		public static Dictionary<string, ColorVariable> DefaultColorDict = new Dictionary<string, ColorVariable>();

		// Token: 0x040000F2 RID: 242
		public static ColorVariable errorColor = new ColorVariable("errorColor", "#ERROR.NOTFOUND", Color.magenta, true);

		// Token: 0x040000F3 RID: 243
		public static ColorVariable preview = new ColorVariable("preview", "Цвет не выбран", Color.white, true);

		// Token: 0x040000F4 RID: 244
		public static ColorVariable previewselected;

		// Token: 0x040000F5 RID: 245
		public static string selectedOption;
	}
}

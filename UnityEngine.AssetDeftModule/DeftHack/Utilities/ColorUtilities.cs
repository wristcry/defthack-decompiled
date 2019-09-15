using System;
using System.Globalization;
using System.Linq;
using DeftHack.Misc;
using DeftHack.Options.VisualOptions;
using DeftHack.Variables.UIVariables;
using UnityEngine;

namespace DeftHack.Utilities
{
	// Token: 0x0200000F RID: 15
	public static class ColorUtilities
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003944 File Offset: 0x00001B44
		public static void addColor(ColorVariable ColorVariable)
		{
			bool flag = !ColorOptions.DefaultColorDict.ContainsKey(ColorVariable.identity);
			bool flag2 = flag;
			if (flag2)
			{
				ColorOptions.DefaultColorDict.Add(ColorVariable.identity, ColorVariable);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003980 File Offset: 0x00001B80
		public static ColorVariable getColor(string identifier)
		{
			ColorVariable colorVariable;
			bool flag = ColorOptions.ColorDict.TryGetValue(identifier, out colorVariable);
			bool flag2 = flag;
			ColorVariable result;
			if (flag2)
			{
				result = colorVariable;
			}
			else
			{
				result = ColorOptions.errorColor;
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000039B8 File Offset: 0x00001BB8
		public static string getHex(string identifier)
		{
			ColorVariable color;
			bool flag = ColorOptions.ColorDict.TryGetValue(identifier, out color);
			bool flag2 = flag;
			string result;
			if (flag2)
			{
				result = ColorUtilities.ColorToHex(color);
			}
			else
			{
				result = ColorUtilities.ColorToHex(ColorOptions.errorColor);
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003A04 File Offset: 0x00001C04
		public static void setColor(string identifier, Color32 color)
		{
			ColorVariable colorVariable;
			bool flag = ColorOptions.ColorDict.TryGetValue(identifier, out colorVariable);
			bool flag2 = flag;
			if (flag2)
			{
				colorVariable.color = color.ToSerializableColor();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003A34 File Offset: 0x00001C34
		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + "FF";
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003A84 File Offset: 0x00001C84
		public static ColorVariable[] getArray()
		{
			return ColorOptions.ColorDict.Values.ToArray<ColorVariable>();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public static Color32 HexToColor(string hex)
		{
			byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
			return new Color32(r, g, b, byte.MaxValue);
		}
	}
}

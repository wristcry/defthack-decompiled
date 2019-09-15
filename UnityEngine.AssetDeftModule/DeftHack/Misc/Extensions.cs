using System;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Misc.Serializables;
using UnityEngine;

namespace DeftHack.Misc
{
	// Token: 0x0200005F RID: 95
	public static class Extensions
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public static Color Invert(this Color32 color)
		{
			return new Color((float)(byte.MaxValue - color.r), (float)(byte.MaxValue - color.g), (float)(byte.MaxValue - color.b));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000C124 File Offset: 0x0000A324
		public static SerializableColor ToSerializableColor(this Color32 c)
		{
			return new SerializableColor((int)c.r, (int)c.g, (int)c.b, (int)c.a);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000C154 File Offset: 0x0000A354
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
		{
			return source.Skip(Math.Max(0, source.Count<T>() - N));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000C17C File Offset: 0x0000A37C
		public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> target)
		{
			foreach (T item in target)
			{
				source.Add(item);
			}
		}
	}
}

using System;
using DeftHack.Misc.Serializables;
using Newtonsoft.Json;
using UnityEngine;

namespace DeftHack.Variables.UIVariables
{
	// Token: 0x02000009 RID: 9
	public class ColorVariable
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000021BB File Offset: 0x000003BB
		[JsonConstructor]
		public ColorVariable(string identity, string name, Color32 color, Color32 origColor, bool disableAlpha)
		{
			this.identity = identity;
			this.name = name;
			this.color = color;
			this.origColor = origColor;
			this.disableAlpha = disableAlpha;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021F4 File Offset: 0x000003F4
		public ColorVariable(string identity, string name, Color32 color, bool disableAlpha = true)
		{
			this.identity = identity;
			this.name = name;
			this.color = color;
			this.origColor = color;
			this.disableAlpha = disableAlpha;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003760 File Offset: 0x00001960
		public ColorVariable(ColorVariable option)
		{
			this.identity = option.identity;
			this.name = option.name;
			this.color = option.color;
			this.origColor = option.origColor;
			this.disableAlpha = option.disableAlpha;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000037B4 File Offset: 0x000019B4
		public static implicit operator Color(ColorVariable color)
		{
			return color.color.ToColor();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000037D8 File Offset: 0x000019D8
		public static implicit operator Color32(ColorVariable color)
		{
			return color.color;
		}

		// Token: 0x04000020 RID: 32
		public SerializableColor color;

		// Token: 0x04000021 RID: 33
		public SerializableColor origColor;

		// Token: 0x04000022 RID: 34
		public string name;

		// Token: 0x04000023 RID: 35
		public string identity;

		// Token: 0x04000024 RID: 36
		public bool disableAlpha;
	}
}

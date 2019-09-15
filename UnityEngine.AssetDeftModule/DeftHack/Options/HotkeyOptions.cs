using System;
using System.Collections.Generic;
using DeftHack.Attributes;
using DeftHack.Variables;

namespace DeftHack.Options
{
	// Token: 0x0200004F RID: 79
	public static class HotkeyOptions
	{
		// Token: 0x040000AA RID: 170
		[Save]
		public static Dictionary<string, Dictionary<string, Hotkey>> HotkeyDict = new Dictionary<string, Dictionary<string, Hotkey>>();

		// Token: 0x040000AB RID: 171
		[Save]
		public static Dictionary<string, Hotkey> UnorganizedHotkeys = new Dictionary<string, Hotkey>();
	}
}

using System;
using System.Collections.Generic;
using DeftHack.Misc.Classes.ESP;

namespace DeftHack.Variables
{
	// Token: 0x02000004 RID: 4
	public class ESPVariables
	{
		// Token: 0x04000010 RID: 16
		public static List<ESPObject> Objects = new List<ESPObject>();

		// Token: 0x04000011 RID: 17
		public static Queue<ESPBox> DrawBuffer = new Queue<ESPBox>();

		// Token: 0x04000012 RID: 18
		public static Queue<ESPBox2> DrawBuffer2 = new Queue<ESPBox2>();
	}
}

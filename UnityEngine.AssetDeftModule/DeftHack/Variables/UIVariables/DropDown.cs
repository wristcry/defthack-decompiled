using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeftHack.Variables.UIVariables
{
	// Token: 0x0200000A RID: 10
	public class DropDown
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000222C File Offset: 0x0000042C
		public DropDown()
		{
			this.IsEnabled = false;
			this.ListIndex = 0;
			this.ScrollView = Vector2.zero;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000037F8 File Offset: 0x000019F8
		public static DropDown Get(string identifier)
		{
			DropDown dropDown;
			bool flag = DropDown.DropDownManager.TryGetValue(identifier, out dropDown);
			bool flag2 = flag;
			DropDown result;
			if (flag2)
			{
				result = dropDown;
			}
			else
			{
				dropDown = new DropDown();
				DropDown.DropDownManager.Add(identifier, dropDown);
				result = dropDown;
			}
			return result;
		}

		// Token: 0x04000025 RID: 37
		public static Dictionary<string, DropDown> DropDownManager = new Dictionary<string, DropDown>();

		// Token: 0x04000026 RID: 38
		public bool IsEnabled;

		// Token: 0x04000027 RID: 39
		public int ListIndex;

		// Token: 0x04000028 RID: 40
		public Vector2 ScrollView;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using DeftHack.Attributes;
using DeftHack.Components.UI.Menu;
using DeftHack.Utilities;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace DeftHack.Components.Basic
{
	// Token: 0x020000BA RID: 186
	[Component]
	public class HotkeyComponent : MonoBehaviour
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0001BAA0 File Offset: 0x00019CA0
		public void Update()
		{
			bool needsKeys = HotkeyComponent.NeedsKeys;
			bool flag = needsKeys;
			if (flag)
			{
				List<KeyCode> currentKeys = HotkeyComponent.CurrentKeys.ToList<KeyCode>();
				HotkeyComponent.CurrentKeys.Clear();
				foreach (KeyCode keyCode in HotkeyComponent.Keys)
				{
					bool key = Input.GetKey(keyCode);
					bool flag2 = key;
					if (flag2)
					{
						HotkeyComponent.CurrentKeys.Add(keyCode);
					}
				}
				bool flag3 = HotkeyComponent.CurrentKeys.Count < HotkeyComponent.CurrentKeyCount && HotkeyComponent.CurrentKeyCount > 0;
				bool flag4 = flag3;
				if (flag4)
				{
					HotkeyComponent.CurrentKeys = currentKeys;
					HotkeyComponent.StopKeys = true;
				}
				HotkeyComponent.CurrentKeyCount = HotkeyComponent.CurrentKeys.Count;
			}
			bool isInMenu = MenuComponent.IsInMenu;
			bool flag5 = !isInMenu;
			if (flag5)
			{
				foreach (KeyValuePair<string, Newtonsoft.Json.Serialization.Action> keyValuePair in HotkeyComponent.ActionDict)
				{
					bool flag6 = HotkeyUtilities.IsHotkeyDown(keyValuePair.Key);
					bool flag7 = flag6;
					if (flag7)
					{
						keyValuePair.Value();
					}
				}
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00003115 File Offset: 0x00001315
		public static void Clear()
		{
			HotkeyComponent.NeedsKeys = false;
			HotkeyComponent.StopKeys = false;
			HotkeyComponent.CurrentKeyCount = 0;
			HotkeyComponent.CurrentKeys = new List<KeyCode>();
		}

		// Token: 0x040003C4 RID: 964
		public static bool NeedsKeys;

		// Token: 0x040003C5 RID: 965
		public static bool StopKeys;

		// Token: 0x040003C6 RID: 966
		public static int CurrentKeyCount;

		// Token: 0x040003C7 RID: 967
		public static List<KeyCode> CurrentKeys;

		// Token: 0x040003C8 RID: 968
		public static Dictionary<string, Newtonsoft.Json.Serialization.Action> ActionDict = new Dictionary<string, Newtonsoft.Json.Serialization.Action>();

		// Token: 0x040003C9 RID: 969
		public static KeyCode[] Keys = (KeyCode[])Enum.GetValues(typeof(KeyCode));
	}
}

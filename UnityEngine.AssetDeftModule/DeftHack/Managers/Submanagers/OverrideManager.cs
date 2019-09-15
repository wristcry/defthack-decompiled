using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Wrappers;

namespace DeftHack.Managers.Submanagers
{
	// Token: 0x02000077 RID: 119
	public static class OverrideManager
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000C388 File Offset: 0x0000A588
		public static Dictionary<OverrideAttribute, OverrideWrapper> Overrides
		{
			get
			{
				return OverrideManager._overrides;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public static void OffHook()
		{
			foreach (OverrideWrapper overrideWrapper in OverrideManager.Overrides.Values)
			{
				overrideWrapper.Revert();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		public static void LoadOverride(MethodInfo method)
		{
			OverrideAttribute attribute = (OverrideAttribute)Attribute.GetCustomAttribute(method, typeof(OverrideAttribute));
			bool flag = OverrideManager.Overrides.Count((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Key.Method == attribute.Method) > 0;
			bool flag2 = !flag;
			if (flag2)
			{
				OverrideWrapper overrideWrapper = new OverrideWrapper(attribute.Method, method, attribute, null);
				overrideWrapper.Override();
				OverrideManager.Overrides.Add(attribute, overrideWrapper);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000C484 File Offset: 0x0000A684
		public static void InitHook()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				foreach (MethodInfo methodInfo in type.GetMethods())
				{
					bool flag = methodInfo.Name == "OV_GetKey" && methodInfo.IsDefined(typeof(OverrideAttribute), false);
					bool flag2 = flag;
					if (flag2)
					{
						OverrideManager.LoadOverride(methodInfo);
					}
				}
			}
		}

		// Token: 0x040001F5 RID: 501
		private static Dictionary<OverrideAttribute, OverrideWrapper> _overrides = new Dictionary<OverrideAttribute, OverrideWrapper>();
	}
}

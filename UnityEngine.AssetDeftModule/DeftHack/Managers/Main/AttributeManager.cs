using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using DeftHack.Attributes;
using DeftHack.Managers.Submanagers;
using DeftHackFree;

namespace DeftHack.Managers.Main
{
	// Token: 0x0200007B RID: 123
	public static class AttributeManager
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000C66C File Offset: 0x0000A86C
		public static void Init()
		{
			List<Type> list = new List<Type>();
			List<MethodInfo> list2 = new List<MethodInfo>();
			List<MethodInfo> list3 = new List<MethodInfo>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				bool flag = type.IsDefined(typeof(ComponentAttribute), false);
				bool flag2 = flag;
				if (flag2)
				{
					abc.HookObject.AddComponent(type);
				}
				bool flag3 = type.IsDefined(typeof(SpyComponentAttribute), false);
				bool flag4 = flag3;
				if (flag4)
				{
					list.Add(type);
				}
				MethodInfo[] methods = type.GetMethods();
				for (int j = 0; j < methods.Length; j++)
				{
					MethodInfo M = methods[j];
					bool flag5 = M.IsDefined(typeof(InitializerAttribute), false);
					bool flag6 = flag5;
					if (flag6)
					{
						M.Invoke(null, null);
					}
					bool flag7 = M.IsDefined(typeof(OverrideAttribute), false);
					bool flag8 = flag7;
					if (flag8)
					{
						OverrideManager.LoadOverride(M);
					}
					bool flag9 = M.IsDefined(typeof(OnSpyAttribute), false);
					bool flag10 = flag9;
					if (flag10)
					{
						list2.Add(M);
					}
					bool flag11 = M.IsDefined(typeof(OffSpyAttribute), false);
					bool flag12 = flag11;
					if (flag12)
					{
						list3.Add(M);
					}
					bool flag13 = M.IsDefined(typeof(ThreadAttribute), false);
					bool flag14 = flag13;
					if (flag14)
					{
						new Thread(delegate()
						{
							try
							{
								M.Invoke(null, null);
							}
							catch (Exception ex)
							{
							}
						}).Start();
					}
				}
			}
			SpyManager.Components = list;
			SpyManager.PostSpy = list3;
			SpyManager.PreSpy = list2;
		}
	}
}

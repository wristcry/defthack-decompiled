using System;
using System.Linq;
using System.Reflection;

namespace DeftHack.Attributes
{
	// Token: 0x020000CD RID: 205
	[AttributeUsage(AttributeTargets.Method)]
	public class OverrideAttribute : Attribute
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000348F File Offset: 0x0000168F
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00003497 File Offset: 0x00001697
		public Type Class { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000034A0 File Offset: 0x000016A0
		// (set) Token: 0x0600031B RID: 795 RVA: 0x000034A8 File Offset: 0x000016A8
		public string MethodName { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000034B1 File Offset: 0x000016B1
		// (set) Token: 0x0600031D RID: 797 RVA: 0x000034B9 File Offset: 0x000016B9
		public MethodInfo Method { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000034C2 File Offset: 0x000016C2
		// (set) Token: 0x0600031F RID: 799 RVA: 0x000034CA File Offset: 0x000016CA
		public BindingFlags Flags { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000034D3 File Offset: 0x000016D3
		// (set) Token: 0x06000321 RID: 801 RVA: 0x000034DB File Offset: 0x000016DB
		public bool MethodFound { get; private set; }

		// Token: 0x06000322 RID: 802 RVA: 0x0001E348 File Offset: 0x0001C548
		public OverrideAttribute(Type tClass, string method, BindingFlags flags, int index = 0)
		{
			this.Class = tClass;
			this.MethodName = method;
			this.Flags = flags;
			try
			{
				this.Method = (from a in this.Class.GetMethods(flags)
				where a.Name == method
				select a).ToArray<MethodInfo>()[index];
				this.MethodFound = true;
			}
			catch (Exception)
			{
				this.MethodFound = false;
			}
		}
	}
}

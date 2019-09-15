using System;
using System.Reflection;

namespace DeftHack.Variables
{
	// Token: 0x02000007 RID: 7
	public static class ReflectionVariables
	{
		// Token: 0x04000017 RID: 23
		public static BindingFlags PublicInstance = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x04000018 RID: 24
		public static BindingFlags PrivateInstance = BindingFlags.Instance | BindingFlags.NonPublic;

		// Token: 0x04000019 RID: 25
		public static BindingFlags PublicStatic = BindingFlags.Static | BindingFlags.Public;

		// Token: 0x0400001A RID: 26
		public static BindingFlags PrivateStatic = BindingFlags.Static | BindingFlags.NonPublic;

		// Token: 0x0400001B RID: 27
		public static BindingFlags Everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}

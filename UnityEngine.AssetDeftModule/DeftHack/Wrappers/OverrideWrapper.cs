using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using DeftHack.Attributes;
using DeftHack.Utilities;

namespace DeftHack.Wrappers
{
	// Token: 0x02000002 RID: 2
	public class OverrideWrapper
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002090 File Offset: 0x00000290
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002098 File Offset: 0x00000298
		public MethodInfo Original { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020A1 File Offset: 0x000002A1
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020A9 File Offset: 0x000002A9
		public MethodInfo Modified { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020B2 File Offset: 0x000002B2
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020BA File Offset: 0x000002BA
		public IntPtr PtrOriginal { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020C3 File Offset: 0x000002C3
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020CB File Offset: 0x000002CB
		public IntPtr PtrModified { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020D4 File Offset: 0x000002D4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020DC File Offset: 0x000002DC
		public OverrideUtilities.OffsetBackup OffsetBackup { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020E5 File Offset: 0x000002E5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020ED File Offset: 0x000002ED
		public OverrideAttribute Attribute { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020F6 File Offset: 0x000002F6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020FE File Offset: 0x000002FE
		public bool Detoured { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002107 File Offset: 0x00000307
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000210F File Offset: 0x0000030F
		public object Instance { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002120 File Offset: 0x00000320
		public bool Local { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x000035A0 File Offset: 0x000017A0
		public OverrideWrapper(MethodInfo original, MethodInfo modified, OverrideAttribute attribute, object instance = null)
		{
			this.Original = original;
			this.Modified = modified;
			this.Instance = instance;
			this.Attribute = attribute;
			this.Local = (this.Modified.DeclaringType.Assembly == Assembly.GetExecutingAssembly());
			RuntimeHelpers.PrepareMethod(original.MethodHandle);
			RuntimeHelpers.PrepareMethod(modified.MethodHandle);
			this.PtrOriginal = this.Original.MethodHandle.GetFunctionPointer();
			this.PtrModified = this.Modified.MethodHandle.GetFunctionPointer();
			this.OffsetBackup = new OverrideUtilities.OffsetBackup(this.PtrOriginal);
			this.Detoured = false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003660 File Offset: 0x00001860
		public bool Override()
		{
			bool detoured = this.Detoured;
			bool flag = detoured;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = OverrideUtilities.OverrideFunction(this.PtrOriginal, this.PtrModified);
				bool flag3 = flag2;
				bool flag4 = flag3;
				if (flag4)
				{
					this.Detoured = true;
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000036B4 File Offset: 0x000018B4
		public bool Revert()
		{
			bool flag = !this.Detoured;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = OverrideUtilities.RevertOverride(this.OffsetBackup);
				bool flag4 = flag3;
				bool flag5 = flag4;
				if (flag5)
				{
					this.Detoured = false;
				}
				result = flag3;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003704 File Offset: 0x00001904
		public object CallOriginal(object[] args, object instance = null)
		{
			this.Revert();
			object result = null;
			try
			{
				result = this.Original.Invoke(instance ?? this.Instance, args);
			}
			catch (Exception exception)
			{
				DebugUtilities.LogException(exception);
			}
			this.Override();
			return result;
		}
	}
}

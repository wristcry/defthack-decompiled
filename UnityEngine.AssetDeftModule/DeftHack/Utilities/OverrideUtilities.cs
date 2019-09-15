using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Managers.Submanagers;
using DeftHack.Wrappers;

namespace DeftHack.Utilities
{
	// Token: 0x0200001F RID: 31
	public static class OverrideUtilities
	{
		// Token: 0x06000082 RID: 130 RVA: 0x0000643C File Offset: 0x0000463C
		public static object CallOriginalFunc(MethodInfo method, object instance = null, params object[] args)
		{
			bool flag = OverrideManager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != method);
			bool flag2 = flag;
			if (flag2)
			{
				throw new Exception("The Override specified was not found!");
			}
			OverrideWrapper value = OverrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
			return value.CallOriginal(args, instance);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000064B0 File Offset: 0x000046B0
		public static object CallOriginal(object instance = null, params object[] args)
		{
			StackTrace stackTrace = new StackTrace(false);
			bool flag = stackTrace.FrameCount < 1;
			bool flag2 = flag;
			if (flag2)
			{
				throw new Exception("Invalid trace back to the original method! Please provide the methodinfo instead!");
			}
			MethodBase method = stackTrace.GetFrame(1).GetMethod();
			MethodInfo original = null;
			bool flag3 = !Attribute.IsDefined(method, typeof(OverrideAttribute));
			bool flag4 = flag3;
			if (flag4)
			{
				method = stackTrace.GetFrame(2).GetMethod();
			}
			OverrideAttribute overrideAttribute = (OverrideAttribute)Attribute.GetCustomAttribute(method, typeof(OverrideAttribute));
			bool flag5 = overrideAttribute == null;
			bool flag6 = flag5;
			if (flag6)
			{
				throw new Exception("This method can only be called from an overwritten method!");
			}
			bool flag7 = !overrideAttribute.MethodFound;
			bool flag8 = flag7;
			if (flag8)
			{
				throw new Exception("The original method was never found!");
			}
			original = overrideAttribute.Method;
			bool flag9 = OverrideManager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != original);
			bool flag10 = flag9;
			if (flag10)
			{
				throw new Exception("The Override specified was not found!");
			}
			OverrideWrapper value = OverrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == original).Value;
			return value.CallOriginal(args, instance);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000065E4 File Offset: 0x000047E4
		public static bool EnableOverride(MethodInfo method)
		{
			OverrideWrapper value = OverrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
			return value != null && value.Override();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00006630 File Offset: 0x00004830
		public static bool DisableOverride(MethodInfo method)
		{
			OverrideWrapper value = OverrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
			return value != null && value.Revert();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000667C File Offset: 0x0000487C
		public unsafe static bool OverrideFunction(IntPtr ptrOriginal, IntPtr ptrModified)
		{
			bool result;
			try
			{
				int size = IntPtr.Size;
				bool flag = size != 4;
				if (flag)
				{
					bool flag2 = size != 8;
					if (flag2)
					{
						return false;
					}
					byte* ptr = (byte*)ptrOriginal.ToPointer();
					*ptr = 72;
					ptr[1] = 184;
					*(long*)(ptr + 2) = ptrModified.ToInt64();
					ptr[10] = byte.MaxValue;
					ptr[11] = 224;
				}
				else
				{
					byte* ptr2 = (byte*)ptrOriginal.ToPointer();
					*ptr2 = 104;
					*(int*)(ptr2 + 1) = ptrModified.ToInt32();
					ptr2[5] = 195;
				}
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006734 File Offset: 0x00004934
		public unsafe static bool RevertOverride(OverrideUtilities.OffsetBackup backup)
		{
			bool result;
			try
			{
				byte* ptr = (byte*)backup.Method.ToPointer();
				*ptr = backup.A;
				ptr[1] = backup.B;
				ptr[10] = backup.C;
				ptr[11] = backup.D;
				ptr[12] = backup.E;
				bool flag = IntPtr.Size == 4;
				bool flag2 = flag;
				if (flag2)
				{
					*(int*)(ptr + 1) = (int)backup.F32;
					ptr[5] = backup.G;
				}
				else
				{
					*(long*)(ptr + 2) = (long)backup.F64;
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x02000020 RID: 32
		public class OffsetBackup
		{
			// Token: 0x06000088 RID: 136 RVA: 0x000067D8 File Offset: 0x000049D8
			public unsafe OffsetBackup(IntPtr method)
			{
				this.Method = method;
				byte* ptr = (byte*)method.ToPointer();
				this.A = *ptr;
				this.B = ptr[1];
				this.C = ptr[10];
				this.D = ptr[11];
				this.E = ptr[12];
				bool flag = IntPtr.Size == 4;
				bool flag2 = flag;
				if (flag2)
				{
					this.F32 = *(uint*)(ptr + 1);
					this.G = ptr[5];
				}
				else
				{
					this.F64 = (ulong)(*(long*)(ptr + 2));
				}
			}

			// Token: 0x0400004C RID: 76
			public IntPtr Method;

			// Token: 0x0400004D RID: 77
			public byte A;

			// Token: 0x0400004E RID: 78
			public byte B;

			// Token: 0x0400004F RID: 79
			public byte C;

			// Token: 0x04000050 RID: 80
			public byte D;

			// Token: 0x04000051 RID: 81
			public byte E;

			// Token: 0x04000052 RID: 82
			public byte G;

			// Token: 0x04000053 RID: 83
			public ulong F64;

			// Token: 0x04000054 RID: 84
			public uint F32;
		}
	}
}

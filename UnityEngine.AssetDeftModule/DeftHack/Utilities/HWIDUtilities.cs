using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DeftHack.Misc.Enums;

namespace DeftHack.Utilities
{
	// Token: 0x02000016 RID: 22
	public static class HWIDUtilities
	{
		// Token: 0x0600005C RID: 92
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetVolumeInformation(string rootPathName, StringBuilder volumeNameBuffer, int volumeNameSize, out uint volumeSerialNumber, out uint maximumComponentLength, out FileSystemFeature fileSystemFlags, StringBuilder fileSystemNameBuffer, int nFileSystemNameSize);

		// Token: 0x0600005D RID: 93
		[DllImport("kernel32.dll")]
		private static extern bool GetComputerNameA(StringBuilder lpBuffer, ref uint lpnSize);

		// Token: 0x0600005E RID: 94
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

		// Token: 0x0600005F RID: 95
		[DllImport("kernel32")]
		private static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, uint dwFreeType);

		// Token: 0x06000060 RID: 96 RVA: 0x000055D0 File Offset: 0x000037D0
		public static string GetHWID()
		{
			uint num;
			uint num2;
			FileSystemFeature fileSystemFeature;
			HWIDUtilities.GetVolumeInformation("C:\\", null, 0, out num, out num2, out fileSystemFeature, null, 0);
			uint num3 = 32u;
			StringBuilder stringBuilder = new StringBuilder(32);
			HWIDUtilities.GetComputerNameA(stringBuilder, ref num3);
			string text = "";
			foreach (char c2 in stringBuilder.ToString())
			{
				text += ((byte)c2).ToString("x2");
			}
			string arg = "";
			byte[] source = HWIDUtilities.Invoke(0);
			for (int j = 0; j < 3; j++)
			{
				arg += BitConverter.ToInt32(source.Skip(j * 4).Take(4).ToArray<byte>(), 0);
			}
			string source2 = string.Format("{0}-{1}-{2}", arg, num, text);
			source2 = HWIDUtilities.HashFNV1a((from c in source2
			select (byte)c).ToArray<byte>()).ToString();
			return Convert.ToBase64String((from c in source2
			select (byte)c).ToArray<byte>());
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005730 File Offset: 0x00003930
		public static ulong HashFNV1a(byte[] bytes)
		{
			ulong num = 14695981039346656037UL;
			for (int i = 0; i < bytes.Length; i++)
			{
				num ^= (ulong)bytes[i];
				num *= 1099511628211UL;
			}
			return num;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005774 File Offset: 0x00003974
		public static byte[] Invoke(int level)
		{
			IntPtr intPtr = IntPtr.Zero;
			byte[] result;
			try
			{
				bool flag = IntPtr.Size == 4;
				bool flag2 = flag;
				byte[] array;
				if (flag2)
				{
					array = HWIDUtilities.x86CodeBytes;
				}
				else
				{
					array = HWIDUtilities.x64CodeBytes;
				}
				intPtr = HWIDUtilities.VirtualAlloc(IntPtr.Zero, new UIntPtr((uint)array.Length), AllocationType.COMMIT | AllocationType.RESERVE, MemoryProtection.EXECUTE_READWRITE);
				Marshal.Copy(array, 0, intPtr, array.Length);
				HWIDUtilities.CpuIDDelegate cpuIDDelegate = (HWIDUtilities.CpuIDDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(HWIDUtilities.CpuIDDelegate));
				GCHandle a = default(GCHandle);
				byte[] array2 = new byte[16];
				try
				{
					a = GCHandle.Alloc(array2, GCHandleType.Pinned);
					cpuIDDelegate(level, array2);
				}
				finally
				{
					bool flag3 = a != default(GCHandle);
					bool flag4 = flag3;
					if (flag4)
					{
						a.Free();
					}
				}
				result = array2;
			}
			finally
			{
				bool flag5 = intPtr != IntPtr.Zero;
				bool flag6 = flag5;
				if (flag6)
				{
					HWIDUtilities.VirtualFree(intPtr, 0u, 32768u);
					intPtr = IntPtr.Zero;
				}
			}
			return result;
		}

		// Token: 0x0400003E RID: 62
		private static readonly byte[] x86CodeBytes = new byte[]
		{
			85,
			139,
			236,
			83,
			87,
			139,
			69,
			8,
			15,
			162,
			139,
			125,
			12,
			137,
			7,
			137,
			95,
			4,
			137,
			79,
			8,
			137,
			87,
			12,
			95,
			91,
			139,
			229,
			93,
			195
		};

		// Token: 0x0400003F RID: 63
		private static readonly byte[] x64CodeBytes = new byte[]
		{
			83,
			73,
			137,
			208,
			137,
			200,
			15,
			162,
			65,
			137,
			64,
			0,
			65,
			137,
			88,
			4,
			65,
			137,
			72,
			8,
			65,
			137,
			80,
			12,
			91,
			195
		};

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000065 RID: 101
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void CpuIDDelegate(int level, byte[] buffer);
	}
}

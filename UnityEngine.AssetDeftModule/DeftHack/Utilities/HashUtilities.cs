using System;
using System.Linq;
using System.Security.Cryptography;

namespace DeftHack.Utilities
{
	// Token: 0x02000013 RID: 19
	public class HashUtilities
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00004F50 File Offset: 0x00003150
		public static string GetSHA2HashString(byte[] Bytes)
		{
			return string.Join("", HashUtilities.GetSHA2Hash(Bytes).Select(delegate(byte a)
			{
				byte b = a;
				return b.ToString("x2");
			}).ToArray<string>()).ToUpper();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004FA0 File Offset: 0x000031A0
		public static byte[] GetSHA2Hash(byte[] Bytes)
		{
			byte[] result;
			using (SHA256 sha = SHA256.Create())
			{
				result = sha.ComputeHash(Bytes);
			}
			return result;
		}
	}
}

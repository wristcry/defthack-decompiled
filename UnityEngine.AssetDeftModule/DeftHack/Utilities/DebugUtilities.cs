using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using DeftHack.Attributes;

namespace DeftHack.Utilities
{
	// Token: 0x02000010 RID: 16
	public class DebugUtilities
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003B00 File Offset: 0x00001D00
		[Thread]
		public static void DebugThread()
		{
			File.WriteAllText("df.log", "");
			DebugUtilities.Data.Enqueue(string.Format("Debug DEFTHACK Start: {0}\r\n\r\n", DateTime.Now));
			for (;;)
			{
				Thread.Sleep(500);
				while (DebugUtilities.Data.Count > 0)
				{
					string contents;
					bool flag = DebugUtilities.Data.TryDequeue(out contents);
					bool flag2 = flag;
					if (flag2)
					{
						File.AppendAllText("df.log", contents);
					}
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000022CA File Offset: 0x000004CA
		public static void Log(object Output)
		{
			DebugUtilities.Data.Enqueue(string.Format("{0}\r\n", Output));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000022E3 File Offset: 0x000004E3
		public static void LogException(Exception Exception)
		{
			DebugUtilities.Data.Enqueue(string.Format("\r\nBEGIN EXCEPTION\r\n{0}\r\nEND EXCEPTION\r\n", Exception));
		}

		// Token: 0x04000036 RID: 54
		public static ConcurrentQueue<string> Data = new ConcurrentQueue<string>();
	}
}

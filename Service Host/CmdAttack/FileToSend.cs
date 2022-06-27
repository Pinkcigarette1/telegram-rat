using System;
using System.IO;

namespace CmdAttack
{
	// Token: 0x02000004 RID: 4
	internal class FileToSend
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020FA File Offset: 0x000002FA
		public FileToSend(string v, FileStream fileStream)
		{
			this.v = v;
			this.fileStream = fileStream;
		}

		// Token: 0x04000004 RID: 4
		private FileStream fileStream;

		// Token: 0x04000005 RID: 5
		private string v;
	}
}

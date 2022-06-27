using System;
using System.Windows.Forms;

namespace CmdAttack
{
	// Token: 0x02000006 RID: 6
	internal static class Program
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000450C File Offset: 0x0000270C
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Core());
		}
	}
}

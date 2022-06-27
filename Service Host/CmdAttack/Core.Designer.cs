namespace CmdAttack
{
	// Token: 0x02000005 RID: 5
	public partial class Core : global::System.Windows.Forms.Form
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000042E4 File Offset: 0x000024E4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000431C File Offset: 0x0000251C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::CmdAttack.Core));
			this.backgroundWorker = new global::System.ComponentModel.BackgroundWorker();
			this.timer = new global::System.Windows.Forms.Timer(this.components);
			this.RamUsage = new global::System.Diagnostics.PerformanceCounter();
			this.CpuUsage = new global::System.Diagnostics.PerformanceCounter();
			((global::System.ComponentModel.ISupportInitialize)this.RamUsage).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.CpuUsage).BeginInit();
			base.SuspendLayout();
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new global::System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.timer.Enabled = true;
			this.timer.Tick += new global::System.EventHandler(this.timer_Tick);
			this.RamUsage.CategoryName = "Memory";
			this.RamUsage.CounterName = "% Committed Bytes In Use";
			this.CpuUsage.CategoryName = "Processor";
			this.CpuUsage.CounterName = "% Processor Time";
			this.CpuUsage.InstanceName = "_Total";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(8, 8);
			base.ControlBox = false;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Core";
			base.Opacity = 0.0;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Load += new global::System.EventHandler(this.Core_Load);
			((global::System.ComponentModel.ISupportInitialize)this.RamUsage).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.CpuUsage).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400000B RID: 11
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400000C RID: 12
		private global::System.ComponentModel.BackgroundWorker backgroundWorker;

		// Token: 0x0400000D RID: 13
		private global::System.Windows.Forms.Timer timer;

		// Token: 0x0400000E RID: 14
		private global::System.Diagnostics.PerformanceCounter RamUsage;

		// Token: 0x0400000F RID: 15
		private global::System.Diagnostics.PerformanceCounter CpuUsage;
	}
}

using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Win81StoreRevival
{
	// Token: 0x02000025 RID: 37
	public class DownloadedAppInfo
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000C440 File Offset: 0x0000A640
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000C448 File Offset: 0x0000A648
		public string AppId { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000C451 File Offset: 0x0000A651
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000C459 File Offset: 0x0000A659
		public string AppName { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000C462 File Offset: 0x0000A662
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000C46A File Offset: 0x0000A66A
		public string Publisher { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000C473 File Offset: 0x0000A673
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000C47B File Offset: 0x0000A67B
		public string Version { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000C484 File Offset: 0x0000A684
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000C48C File Offset: 0x0000A68C
		public string IconUrl { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000C495 File Offset: 0x0000A695
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000C49D File Offset: 0x0000A69D
		public DateTime DownloadDate { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000C4AE File Offset: 0x0000A6AE
		public string DownloadPath { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000C4B7 File Offset: 0x0000A6B7
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000C4BF File Offset: 0x0000A6BF
		public bool Installed { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public DateTime? InstallDate { get; set; }

		// Token: 0x06000210 RID: 528 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public string UpdateStatus()
		{
			return this.Installed ? "Installed" : "Not Installed";
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C504 File Offset: 0x0000A704
		public SolidColorBrush UpdateStatusColor()
		{
			bool installed = this.Installed;
			SolidColorBrush result;
			if (installed)
			{
				result = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79));
			}
			else
			{
				result = new SolidColorBrush(Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 0));
			}
			return result;
		}
	}
}

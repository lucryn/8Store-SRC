using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Win81StoreRevival
{
	// Token: 0x02000029 RID: 41
	public class DownloadedAppRecord
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000D672 File Offset: 0x0000B872
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000D67A File Offset: 0x0000B87A
		public string AppId { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000D683 File Offset: 0x0000B883
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000D68B File Offset: 0x0000B88B
		public string AppName { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000D694 File Offset: 0x0000B894
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000D69C File Offset: 0x0000B89C
		public string Publisher { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000D6A5 File Offset: 0x0000B8A5
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000D6AD File Offset: 0x0000B8AD
		public string Version { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000D6B6 File Offset: 0x0000B8B6
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000D6BE File Offset: 0x0000B8BE
		public string IconUrl { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000D6C7 File Offset: 0x0000B8C7
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000D6CF File Offset: 0x0000B8CF
		public DateTime DownloadDate { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public string DownloadPath { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000D6E9 File Offset: 0x0000B8E9
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000D6F1 File Offset: 0x0000B8F1
		public bool Installed { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000D6FA File Offset: 0x0000B8FA
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000D702 File Offset: 0x0000B902
		public DateTime? InstallDate { get; set; }

		// Token: 0x06000297 RID: 663 RVA: 0x0000D70B File Offset: 0x0000B90B
		public string UpdateStatus()
		{
			if (!this.Installed)
			{
				return "Not installed";
			}
			return "Installed";
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000D720 File Offset: 0x0000B920
		public SolidColorBrush UpdateStatusColor()
		{
			if (this.Installed)
			{
				return new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79));
			}
			return new SolidColorBrush(Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 0));
		}
	}
}

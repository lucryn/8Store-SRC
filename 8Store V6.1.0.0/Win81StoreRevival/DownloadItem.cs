using System;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace Win81StoreRevival
{
	// Token: 0x02000028 RID: 40
	public class DownloadItem
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000D476 File Offset: 0x0000B676
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000D47E File Offset: 0x0000B67E
		public string FileName { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000D487 File Offset: 0x0000B687
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000D48F File Offset: 0x0000B68F
		public string AppName { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000D498 File Offset: 0x0000B698
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		public string AppId { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000D4A9 File Offset: 0x0000B6A9
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000D4B1 File Offset: 0x0000B6B1
		public string Publisher { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000D4BA File Offset: 0x0000B6BA
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000D4C2 File Offset: 0x0000B6C2
		public string Version { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000D4CB File Offset: 0x0000B6CB
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000D4D3 File Offset: 0x0000B6D3
		public string IconUrl { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
		public double Progress { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000D4ED File Offset: 0x0000B6ED
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000D4F5 File Offset: 0x0000B6F5
		public string Status { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000D4FE File Offset: 0x0000B6FE
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000D506 File Offset: 0x0000B706
		public ulong BytesReceived { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000D50F File Offset: 0x0000B70F
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000D517 File Offset: 0x0000B717
		public ulong TotalBytes { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000D520 File Offset: 0x0000B720
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000D528 File Offset: 0x0000B728
		public DownloadOperation Operation { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000D531 File Offset: 0x0000B731
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000D539 File Offset: 0x0000B739
		public StorageFile File { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000D542 File Offset: 0x0000B742
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000D54A File Offset: 0x0000B74A
		public string DownloadPath { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000D553 File Offset: 0x0000B753
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000D55B File Offset: 0x0000B75B
		public string CertificatePath { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000D564 File Offset: 0x0000B764
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000D56C File Offset: 0x0000B76C
		public string PublicCertificatePath { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000D575 File Offset: 0x0000B775
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000D57D File Offset: 0x0000B77D
		public bool AutoSideload { get; set; } = true;

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000D586 File Offset: 0x0000B786
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000D58E File Offset: 0x0000B78E
		public bool CertInstalled { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000D598 File Offset: 0x0000B798
		public string ProgressText
		{
			get
			{
				return this.Progress.ToString("F1") + "%";
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public string FileSizeText
		{
			get
			{
				if (this.TotalBytes > 0UL)
				{
					string[] array = new string[]
					{
						"B",
						"KB",
						"MB",
						"GB",
						"TB"
					};
					double num = this.TotalBytes;
					int num2 = 0;
					while (num >= 1024.0 && num2 < array.Length - 1)
					{
						num2++;
						num /= 1024.0;
					}
					return string.Format("{0:0.##} {1}", new object[]
					{
						num,
						array[num2]
					});
				}
				return "NaN";
			}
		}
	}
}

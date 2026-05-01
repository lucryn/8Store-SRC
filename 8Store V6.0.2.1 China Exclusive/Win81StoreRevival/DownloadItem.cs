using System;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace Win81StoreRevival
{
	// Token: 0x02000024 RID: 36
	public class DownloadItem
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000C272 File Offset: 0x0000A472
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000C27A File Offset: 0x0000A47A
		public string FileName { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000C283 File Offset: 0x0000A483
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000C28B File Offset: 0x0000A48B
		public string AppName { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000C294 File Offset: 0x0000A494
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000C29C File Offset: 0x0000A49C
		public string IconUrl { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000C2A5 File Offset: 0x0000A4A5
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		public double Progress { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000C2B6 File Offset: 0x0000A4B6
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000C2BE File Offset: 0x0000A4BE
		public string Status { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000C2CF File Offset: 0x0000A4CF
		public ulong BytesReceived { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public ulong TotalBytes { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C2E9 File Offset: 0x0000A4E9
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000C2F1 File Offset: 0x0000A4F1
		public DownloadOperation Operation { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000C2FA File Offset: 0x0000A4FA
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000C302 File Offset: 0x0000A502
		public StorageFile File { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000C30B File Offset: 0x0000A50B
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000C313 File Offset: 0x0000A513
		public string CertificatePath { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000C31C File Offset: 0x0000A51C
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000C324 File Offset: 0x0000A524
		public bool AutoSideload { get; set; } = true;

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000C32D File Offset: 0x0000A52D
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000C335 File Offset: 0x0000A535
		public bool CertInstalled { get; set; } = false;

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000C340 File Offset: 0x0000A540
		public string ProgressText
		{
			get
			{
				return this.Progress.ToString("F1") + "%";
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000C370 File Offset: 0x0000A570
		public string FileSizeText
		{
			get
			{
				bool flag = this.TotalBytes > 0UL;
				string result;
				if (flag)
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
					result = string.Format("{0:0.##} {1}", new object[]
					{
						num,
						array[num2]
					});
				}
				else
				{
					result = "Calculating...";
				}
				return result;
			}
		}
	}
}

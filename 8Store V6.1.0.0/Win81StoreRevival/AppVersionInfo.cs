using System;

namespace Win81StoreRevival
{
	// Token: 0x02000037 RID: 55
	public class AppVersionInfo
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000152A4 File Offset: 0x000134A4
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x000152AC File Offset: 0x000134AC
		public string LatestVersion { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000152B5 File Offset: 0x000134B5
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x000152BD File Offset: 0x000134BD
		public string DownloadUrl { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x000152C6 File Offset: 0x000134C6
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x000152CE File Offset: 0x000134CE
		public string ReleaseNotes { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000152D7 File Offset: 0x000134D7
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x000152DF File Offset: 0x000134DF
		public bool ForceUpdate { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000152E8 File Offset: 0x000134E8
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x000152F0 File Offset: 0x000134F0
		public string MinimumSupportedVersion { get; set; }
	}
}

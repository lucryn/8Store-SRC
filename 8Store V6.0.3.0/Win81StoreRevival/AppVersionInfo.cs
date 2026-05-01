using System;

namespace Win81StoreRevival
{
	// Token: 0x02000033 RID: 51
	public class AppVersionInfo
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00014E74 File Offset: 0x00013074
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00014E7C File Offset: 0x0001307C
		public string LatestVersion { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00014E85 File Offset: 0x00013085
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00014E8D File Offset: 0x0001308D
		public string DownloadUrl { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00014E96 File Offset: 0x00013096
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00014E9E File Offset: 0x0001309E
		public string ReleaseNotes { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00014EA7 File Offset: 0x000130A7
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00014EAF File Offset: 0x000130AF
		public bool ForceUpdate { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00014EB8 File Offset: 0x000130B8
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00014EC0 File Offset: 0x000130C0
		public string MinimumSupportedVersion { get; set; }
	}
}

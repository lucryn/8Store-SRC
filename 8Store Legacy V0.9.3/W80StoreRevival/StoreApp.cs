using System;

namespace W80StoreRevival
{
	// Token: 0x02000010 RID: 16
	public class StoreApp
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000BE3C File Offset: 0x0000A03C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000BE53 File Offset: 0x0000A053
		public string Id { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000BE5C File Offset: 0x0000A05C
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000BE73 File Offset: 0x0000A073
		public string Name { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000BE7C File Offset: 0x0000A07C
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000BE93 File Offset: 0x0000A093
		public string Publisher { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000BE9C File Offset: 0x0000A09C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000BEB3 File Offset: 0x0000A0B3
		public string Version { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000BED3 File Offset: 0x0000A0D3
		public string DownloadUrl { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000BEF3 File Offset: 0x0000A0F3
		public string IconUrl { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000BF13 File Offset: 0x0000A113
		public string Description { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000BF1C File Offset: 0x0000A11C
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000BF33 File Offset: 0x0000A133
		public bool Featured { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000BF3C File Offset: 0x0000A13C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000BF53 File Offset: 0x0000A153
		public string Type { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000BF5C File Offset: 0x0000A15C
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000BF73 File Offset: 0x0000A173
		public string Category { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000BF7C File Offset: 0x0000A17C
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000BF93 File Offset: 0x0000A193
		public string Screenshot1 { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000BF9C File Offset: 0x0000A19C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000BFB3 File Offset: 0x0000A1B3
		public string Screenshot2 { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000BFD3 File Offset: 0x0000A1D3
		public string Screenshot3 { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000BFF3 File Offset: 0x0000A1F3
		public string Screenshot4 { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000C013 File Offset: 0x0000A213
		public string Screenshot5 { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000C01C File Offset: 0x0000A21C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000C033 File Offset: 0x0000A233
		public ReviewStats ReviewStats { get; set; }

		// Token: 0x060000B3 RID: 179 RVA: 0x0000C03C File Offset: 0x0000A23C
		public StoreApp()
		{
			this.ReviewStats = new ReviewStats();
		}
	}
}

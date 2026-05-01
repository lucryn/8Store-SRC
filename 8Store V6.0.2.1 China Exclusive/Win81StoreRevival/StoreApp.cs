using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000032 RID: 50
	public class StoreApp
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00014CAA File Offset: 0x00012EAA
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00014CB2 File Offset: 0x00012EB2
		public string Id { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00014CBB File Offset: 0x00012EBB
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00014CC3 File Offset: 0x00012EC3
		public string Name { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00014CCC File Offset: 0x00012ECC
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00014CD4 File Offset: 0x00012ED4
		public string Publisher { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00014CDD File Offset: 0x00012EDD
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00014CE5 File Offset: 0x00012EE5
		public string Version { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00014CEE File Offset: 0x00012EEE
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00014CF6 File Offset: 0x00012EF6
		public string DownloadUrl { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00014CFF File Offset: 0x00012EFF
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00014D07 File Offset: 0x00012F07
		public string IconUrl { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00014D10 File Offset: 0x00012F10
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00014D18 File Offset: 0x00012F18
		public string Description { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00014D21 File Offset: 0x00012F21
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00014D29 File Offset: 0x00012F29
		public bool Featured { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00014D32 File Offset: 0x00012F32
		// (set) Token: 0x0600032D RID: 813 RVA: 0x00014D3A File Offset: 0x00012F3A
		public string Type { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00014D43 File Offset: 0x00012F43
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00014D4B File Offset: 0x00012F4B
		public string Category { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00014D54 File Offset: 0x00012F54
		// (set) Token: 0x06000331 RID: 817 RVA: 0x00014D5C File Offset: 0x00012F5C
		public string Screenshot1 { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00014D65 File Offset: 0x00012F65
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00014D6D File Offset: 0x00012F6D
		public string Screenshot2 { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00014D76 File Offset: 0x00012F76
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00014D7E File Offset: 0x00012F7E
		public string Screenshot3 { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00014D87 File Offset: 0x00012F87
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00014D8F File Offset: 0x00012F8F
		[JsonIgnore]
		public ReviewStats ReviewStats { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00014D98 File Offset: 0x00012F98
		[JsonIgnore]
		public List<string> Screenshots
		{
			get
			{
				List<string> list = new List<string>();
				bool flag = !string.IsNullOrWhiteSpace(this.Screenshot1);
				if (flag)
				{
					list.Add(this.Screenshot1);
				}
				bool flag2 = !string.IsNullOrWhiteSpace(this.Screenshot2);
				if (flag2)
				{
					list.Add(this.Screenshot2);
				}
				bool flag3 = !string.IsNullOrWhiteSpace(this.Screenshot3);
				if (flag3)
				{
					list.Add(this.Screenshot3);
				}
				return list;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00014E10 File Offset: 0x00013010
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00014E18 File Offset: 0x00013018
		public List<StoreApp> RelatedApps { get; internal set; }

		// Token: 0x0600033B RID: 827 RVA: 0x00014E24 File Offset: 0x00013024
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.Name,
				" by ",
				this.Publisher,
				" (v",
				this.Version,
				")"
			});
		}
	}
}

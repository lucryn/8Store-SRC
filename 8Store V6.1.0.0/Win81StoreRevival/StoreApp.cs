using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000036 RID: 54
	public class StoreApp
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00014F12 File Offset: 0x00013112
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00014F1A File Offset: 0x0001311A
		public string Id { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00014F23 File Offset: 0x00013123
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00014F2B File Offset: 0x0001312B
		public string AuthorId { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00014F34 File Offset: 0x00013134
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00014F3C File Offset: 0x0001313C
		public string Name { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00014F45 File Offset: 0x00013145
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00014F4D File Offset: 0x0001314D
		public string Publisher { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00014F56 File Offset: 0x00013156
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00014F5E File Offset: 0x0001315E
		public string Version { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00014F67 File Offset: 0x00013167
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00014F6F File Offset: 0x0001316F
		public string DownloadUrl { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00014F78 File Offset: 0x00013178
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00014F80 File Offset: 0x00013180
		public string IconUrl { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00014F89 File Offset: 0x00013189
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00014F91 File Offset: 0x00013191
		public string Description { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00014F9A File Offset: 0x0001319A
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00014FA2 File Offset: 0x000131A2
		public bool Featured { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00014FAB File Offset: 0x000131AB
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00014FB3 File Offset: 0x000131B3
		public string Type { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00014FBC File Offset: 0x000131BC
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00014FC4 File Offset: 0x000131C4
		public string Category { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00014FCD File Offset: 0x000131CD
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00014FD5 File Offset: 0x000131D5
		[JsonProperty("appType")]
		public string AppType { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00014FDE File Offset: 0x000131DE
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00014FE6 File Offset: 0x000131E6
		[JsonProperty("packageFileName")]
		public string PackageFileName { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00014FEF File Offset: 0x000131EF
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00014FF7 File Offset: 0x000131F7
		[JsonProperty("screenshotUrls")]
		public List<string> ScreenshotUrls { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00015000 File Offset: 0x00013200
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00015008 File Offset: 0x00013208
		[JsonProperty("status")]
		public string Status { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00015011 File Offset: 0x00013211
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00015019 File Offset: 0x00013219
		[JsonProperty("targetOS")]
		public int TargetOS { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00015022 File Offset: 0x00013222
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0001502A File Offset: 0x0001322A
		[JsonProperty("rating")]
		public double Rating { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00015033 File Offset: 0x00013233
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0001503B File Offset: 0x0001323B
		[JsonProperty("ratingCount")]
		public int RatingCount { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00015044 File Offset: 0x00013244
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0001504C File Offset: 0x0001324C
		[JsonProperty("reviewCount")]
		public int ReviewCount { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00015055 File Offset: 0x00013255
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001505D File Offset: 0x0001325D
		[JsonProperty("pending_update")]
		public bool PendingUpdate { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00015066 File Offset: 0x00013266
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0001506E File Offset: 0x0001326E
		[JsonProperty("update")]
		public object Update { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00015077 File Offset: 0x00013277
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0001507F File Offset: 0x0001327F
		[JsonProperty("ratings")]
		public RatingSummary Ratings { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00015088 File Offset: 0x00013288
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00015090 File Offset: 0x00013290
		[JsonIgnore]
		public string ApproximateSizeText { get; set; } = "NaN";

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001509C File Offset: 0x0001329C
		[JsonIgnore]
		public string AppSummaryText
		{
			get
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrWhiteSpace(this.Version))
				{
					list.Add("v" + this.Version);
				}
				if (!string.IsNullOrWhiteSpace(this.Status))
				{
					list.Add(this.Status);
				}
				if (this.PendingUpdate)
				{
					list.Add("update pending");
				}
				if (list.Count == 0 && !string.IsNullOrWhiteSpace(this.AppType))
				{
					list.Add(this.AppType);
				}
				return string.Join(" • ", list);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000031B2 File Offset: 0x000013B2
		[JsonIgnore]
		public List<int> StarBg
		{
			get
			{
				List<int> list = new List<int>();
				list.Add(1);
				list.Add(1);
				list.Add(1);
				list.Add(1);
				list.Add(1);
				return list;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001512C File Offset: 0x0001332C
		[JsonIgnore]
		public List<double> StarList
		{
			get
			{
				List<double> list = new List<double>();
				double num = this.ReviewStats.AverageRating;
				for (int i = 1; i <= 5; i++)
				{
					double num2 = (num >= (double)i) ? 1.0 : ((num > (double)(i - 1)) ? (num - (double)(i - 1)) : 0.0);
					list.Add(num2);
				}
				return list;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00015189 File Offset: 0x00013389
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00015191 File Offset: 0x00013391
		public string Screenshot1 { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001519A File Offset: 0x0001339A
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000151A2 File Offset: 0x000133A2
		public string Screenshot2 { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000151AB File Offset: 0x000133AB
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x000151B3 File Offset: 0x000133B3
		public string Screenshot3 { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000151BC File Offset: 0x000133BC
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x000151C4 File Offset: 0x000133C4
		[JsonIgnore]
		public ReviewStats ReviewStats { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000151D0 File Offset: 0x000133D0
		[JsonIgnore]
		public List<string> Screenshots
		{
			get
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrWhiteSpace(this.Screenshot1))
				{
					list.Add(this.Screenshot1);
				}
				if (!string.IsNullOrWhiteSpace(this.Screenshot2))
				{
					list.Add(this.Screenshot2);
				}
				if (!string.IsNullOrWhiteSpace(this.Screenshot3))
				{
					list.Add(this.Screenshot3);
				}
				return list;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0001522F File Offset: 0x0001342F
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00015237 File Offset: 0x00013437
		public List<StoreApp> RelatedApps { get; internal set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00015240 File Offset: 0x00013440
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00015248 File Offset: 0x00013448
		[JsonIgnore]
		public string SpotlightBanner { get; set; }

		// Token: 0x060003FA RID: 1018 RVA: 0x00015251 File Offset: 0x00013451
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

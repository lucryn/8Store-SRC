using System;
using System.Collections.Generic;

namespace Win81StoreRevival
{
	// Token: 0x0200001B RID: 27
	public class AppCollection
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006BF7 File Offset: 0x00004DF7
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00006BFF File Offset: 0x00004DFF
		public string Name { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006C08 File Offset: 0x00004E08
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006C10 File Offset: 0x00004E10
		public string DisplayName { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006C19 File Offset: 0x00004E19
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00006C21 File Offset: 0x00004E21
		public List<string> AppIcons { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006C2A File Offset: 0x00004E2A
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006C32 File Offset: 0x00004E32
		public string AppCount { get; set; }

		// Token: 0x06000157 RID: 343 RVA: 0x00006C3B File Offset: 0x00004E3B
		public AppCollection()
		{
			this.AppIcons = new List<string>();
		}
	}
}

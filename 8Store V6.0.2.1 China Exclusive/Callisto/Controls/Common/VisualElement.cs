using System;
using Windows.UI;

namespace Callisto.Controls.Common
{
	// Token: 0x02000003 RID: 3
	public class VisualElement
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000232A File Offset: 0x0000052A
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002332 File Offset: 0x00000532
		public string DisplayName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000233B File Offset: 0x0000053B
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002343 File Offset: 0x00000543
		public string Description { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002354 File Offset: 0x00000554
		public Uri LogoUri { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000235D File Offset: 0x0000055D
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002365 File Offset: 0x00000565
		public Uri SmallLogoUri { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000236E File Offset: 0x0000056E
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002376 File Offset: 0x00000576
		public string BackgroundColorAsString { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000237F File Offset: 0x0000057F
		public Color BackgroundColor
		{
			get
			{
				return this.BackgroundColorAsString.ToColor();
			}
		}
	}
}

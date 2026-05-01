using System;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media.Imaging;

namespace Win81StoreRevival
{
	// Token: 0x02000032 RID: 50
	public class DownloadedAppInfo
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000FAC1 File Offset: 0x0000DCC1
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000FAC9 File Offset: 0x0000DCC9
		public string AppId { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000FAD2 File Offset: 0x0000DCD2
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000FADA File Offset: 0x0000DCDA
		public string AppName { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		public string Publisher { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		public string Version { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000FB05 File Offset: 0x0000DD05
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000FB0D File Offset: 0x0000DD0D
		public string IconUrl { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000FB16 File Offset: 0x0000DD16
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000FB1E File Offset: 0x0000DD1E
		public DateTime DownloadDate { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000FB27 File Offset: 0x0000DD27
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000FB2F File Offset: 0x0000DD2F
		public string DownloadPath { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000FB38 File Offset: 0x0000DD38
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000FB40 File Offset: 0x0000DD40
		[JsonIgnore]
		public string UpdateStatus { get; set; } = "Up to date";

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000FB49 File Offset: 0x0000DD49
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000FB51 File Offset: 0x0000DD51
		[JsonIgnore]
		public string UpdateStatusColor { get; set; } = "{Binding Accent, Source={StaticResource 8S}}";

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		[JsonIgnore]
		public BitmapImage IconSource
		{
			get
			{
				try
				{
					if (!string.IsNullOrEmpty(this.IconUrl))
					{
						return new BitmapImage(new Uri(this.IconUrl));
					}
				}
				catch
				{
				}
				return new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));
			}
		}
	}
}

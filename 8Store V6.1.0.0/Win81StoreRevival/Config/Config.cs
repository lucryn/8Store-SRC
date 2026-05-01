using System;
using Windows.ApplicationModel;

namespace Win81StoreRevival.Config
{
	// Token: 0x02000058 RID: 88
	public static class Config
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
		public static string version
		{
			get
			{
				PackageVersion version = Package.Current.Id.Version;
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					version.Major,
					version.Minor,
					version.Build,
					version.Revision
				});
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001CE09 File Offset: 0x0001B009
		public static bool IsDebugBuild
		{
			get
			{
				return "PUB".StartsWith("DB") || "PUB".StartsWith("DEV");
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001CE2D File Offset: 0x0001B02D
		public static bool IsPreviewBuild
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000253 RID: 595
		public const string endpoint = "https://8store.modyleprojects.ru";

		// Token: 0x04000254 RID: 596
		public const string endpointauth = "https://8store.modyleprojects.ru/api/auth";

		// Token: 0x04000255 RID: 597
		public const string webstore = "https://8store.modyleprojects.ru";

		// Token: 0x04000256 RID: 598
		public const string versioninfo = "2601PUB_DU4";

		// Token: 0x04000257 RID: 599
		public const string buildinfo = "PUB";
	}
}

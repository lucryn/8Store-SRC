using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C9 RID: 201
	public class JsonCloneSettings
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00029A89 File Offset: 0x00027C89
		public JsonCloneSettings()
		{
			this.CopyAnnotations = true;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00029A98 File Offset: 0x00027C98
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00029AA0 File Offset: 0x00027CA0
		public bool CopyAnnotations { get; set; }

		// Token: 0x040003BE RID: 958
		[Nullable(1)]
		internal static readonly JsonCloneSettings SkipCopyAnnotations = new JsonCloneSettings
		{
			CopyAnnotations = false
		};
	}
}

using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000CC RID: 204
	public class JsonSelectSettings
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00029BC0 File Offset: 0x00027DC0
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x00029BC8 File Offset: 0x00027DC8
		public TimeSpan? RegexMatchTimeout { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00029BD1 File Offset: 0x00027DD1
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00029BD9 File Offset: 0x00027DD9
		public bool ErrorWhenNoMatch { get; set; }
	}
}

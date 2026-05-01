using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(384, AllowMultiple = false)]
	public class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002FDC File Offset: 0x000011DC
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002FE4 File Offset: 0x000011E4
		public bool WriteData { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002FED File Offset: 0x000011ED
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00002FF5 File Offset: 0x000011F5
		public bool ReadData { get; set; }

		// Token: 0x060000AD RID: 173 RVA: 0x00002FFE File Offset: 0x000011FE
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}
	}
}

using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000025 RID: 37
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600018D RID: 397
		string Version { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600018E RID: 398
		// (set) Token: 0x0600018F RID: 399
		string Encoding { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000190 RID: 400
		// (set) Token: 0x06000191 RID: 401
		string Standalone { get; set; }
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F4 RID: 244
	[NullableContext(2)]
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000C4F RID: 3151
		string Version { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000C50 RID: 3152
		// (set) Token: 0x06000C51 RID: 3153
		string Encoding { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C52 RID: 3154
		// (set) Token: 0x06000C53 RID: 3155
		string Standalone { get; set; }
	}
}

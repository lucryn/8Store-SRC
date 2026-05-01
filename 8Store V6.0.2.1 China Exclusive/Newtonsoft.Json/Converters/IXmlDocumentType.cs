using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F5 RID: 245
	[NullableContext(2)]
	internal interface IXmlDocumentType : IXmlNode
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C54 RID: 3156
		[Nullable(1)]
		string Name { [NullableContext(1)] get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C55 RID: 3157
		string System { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C56 RID: 3158
		string Public { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C57 RID: 3159
		string InternalSubset { get; }
	}
}

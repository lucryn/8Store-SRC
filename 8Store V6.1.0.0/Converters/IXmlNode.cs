using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F7 RID: 247
	[NullableContext(2)]
	internal interface IXmlNode
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C5B RID: 3163
		XmlNodeType NodeType { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000C5C RID: 3164
		string LocalName { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C5D RID: 3165
		[Nullable(1)]
		List<IXmlNode> ChildNodes { [NullableContext(1)] get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C5E RID: 3166
		[Nullable(1)]
		List<IXmlNode> Attributes { [NullableContext(1)] get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C5F RID: 3167
		IXmlNode ParentNode { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C60 RID: 3168
		// (set) Token: 0x06000C61 RID: 3169
		string Value { get; set; }

		// Token: 0x06000C62 RID: 3170
		[NullableContext(1)]
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C63 RID: 3171
		string NamespaceUri { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C64 RID: 3172
		object WrappedNode { get; }
	}
}

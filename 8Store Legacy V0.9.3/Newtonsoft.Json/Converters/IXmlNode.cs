using System;
using System.Collections.Generic;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000023 RID: 35
	internal interface IXmlNode
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000177 RID: 375
		XmlNodeType NodeType { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000178 RID: 376
		string LocalName { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000179 RID: 377
		IList<IXmlNode> ChildNodes { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600017A RID: 378
		IList<IXmlNode> Attributes { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600017B RID: 379
		IXmlNode ParentNode { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600017C RID: 380
		// (set) Token: 0x0600017D RID: 381
		string Value { get; set; }

		// Token: 0x0600017E RID: 382
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600017F RID: 383
		string NamespaceUri { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000180 RID: 384
		object WrappedNode { get; }
	}
}

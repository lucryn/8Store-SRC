using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F6 RID: 246
	[NullableContext(1)]
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000C58 RID: 3160
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000C59 RID: 3161
		[return: Nullable(2)]
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C5A RID: 3162
		bool IsEmpty { get; }
	}
}

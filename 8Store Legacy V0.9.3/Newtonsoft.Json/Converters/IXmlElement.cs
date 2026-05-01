using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000026 RID: 38
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000192 RID: 402
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000193 RID: 403
		string GetPrefixOfNamespace(string namespaceUri);
	}
}

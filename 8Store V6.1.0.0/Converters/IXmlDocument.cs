using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F3 RID: 243
	[NullableContext(1)]
	internal interface IXmlDocument : IXmlNode
	{
		// Token: 0x06000C43 RID: 3139
		IXmlNode CreateComment([Nullable(2)] string text);

		// Token: 0x06000C44 RID: 3140
		IXmlNode CreateTextNode([Nullable(2)] string text);

		// Token: 0x06000C45 RID: 3141
		IXmlNode CreateCDataSection([Nullable(2)] string data);

		// Token: 0x06000C46 RID: 3142
		IXmlNode CreateWhitespace([Nullable(2)] string text);

		// Token: 0x06000C47 RID: 3143
		IXmlNode CreateSignificantWhitespace([Nullable(2)] string text);

		// Token: 0x06000C48 RID: 3144
		IXmlNode CreateXmlDeclaration(string version, [Nullable(2)] string encoding, [Nullable(2)] string standalone);

		// Token: 0x06000C49 RID: 3145
		IXmlNode CreateProcessingInstruction(string target, string data);

		// Token: 0x06000C4A RID: 3146
		IXmlElement CreateElement(string elementName);

		// Token: 0x06000C4B RID: 3147
		IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		// Token: 0x06000C4C RID: 3148
		IXmlNode CreateAttribute(string name, string value);

		// Token: 0x06000C4D RID: 3149
		IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value);

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000C4E RID: 3150
		[Nullable(2)]
		IXmlElement DocumentElement { [NullableContext(2)] get; }
	}
}

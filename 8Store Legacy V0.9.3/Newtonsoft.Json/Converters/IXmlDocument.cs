using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000024 RID: 36
	internal interface IXmlDocument : IXmlNode
	{
		// Token: 0x06000181 RID: 385
		IXmlNode CreateComment(string text);

		// Token: 0x06000182 RID: 386
		IXmlNode CreateTextNode(string text);

		// Token: 0x06000183 RID: 387
		IXmlNode CreateCDataSection(string data);

		// Token: 0x06000184 RID: 388
		IXmlNode CreateWhitespace(string text);

		// Token: 0x06000185 RID: 389
		IXmlNode CreateSignificantWhitespace(string text);

		// Token: 0x06000186 RID: 390
		IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone);

		// Token: 0x06000187 RID: 391
		IXmlNode CreateProcessingInstruction(string target, string data);

		// Token: 0x06000188 RID: 392
		IXmlElement CreateElement(string elementName);

		// Token: 0x06000189 RID: 393
		IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		// Token: 0x0600018A RID: 394
		IXmlNode CreateAttribute(string name, string value);

		// Token: 0x0600018B RID: 395
		IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value);

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600018C RID: 396
		IXmlElement DocumentElement { get; }
	}
}

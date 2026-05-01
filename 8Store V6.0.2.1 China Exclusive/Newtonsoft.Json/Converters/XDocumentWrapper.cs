using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FA RID: 250
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00030CAD File Offset: 0x0002EEAD
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00030CBA File Offset: 0x0002EEBA
		public XDocumentWrapper(XDocument document) : base(document)
		{
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00030CC4 File Offset: 0x0002EEC4
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				List<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != 17))
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00030D15 File Offset: 0x0002EF15
		protected override bool HasChildNodes
		{
			get
			{
				return base.HasChildNodes || this.Document.Declaration != null;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00030D2F File Offset: 0x0002EF2F
		public IXmlNode CreateComment([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00030D3C File Offset: 0x0002EF3C
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00030D49 File Offset: 0x0002EF49
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00030D56 File Offset: 0x0002EF56
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00030D63 File Offset: 0x0002EF63
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00030D70 File Offset: 0x0002EF70
		public IXmlNode CreateXmlDeclaration(string version, [Nullable(2)] string encoding, [Nullable(2)] string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00030D7F File Offset: 0x0002EF7F
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDocumentType([Nullable(1)] string name, string publicId, string systemId, string internalSubset)
		{
			return new XDocumentTypeWrapper(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00030D90 File Offset: 0x0002EF90
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00030D9E File Offset: 0x0002EF9E
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00030DB0 File Offset: 0x0002EFB0
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XElementWrapper(new XElement(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00030DC8 File Offset: 0x0002EFC8
		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00030DDB File Offset: 0x0002EFDB
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value)
		{
			return new XAttributeWrapper(new XAttribute(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00030DF4 File Offset: 0x0002EFF4
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00030E18 File Offset: 0x0002F018
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper.Declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200002A RID: 42
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007623 File Offset: 0x00005823
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007630 File Offset: 0x00005830
		public XDocumentWrapper(XDocument document) : base(document)
		{
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000763C File Offset: 0x0000583C
		public override IList<IXmlNode> ChildNodes
		{
			get
			{
				IList<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null)
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007675 File Offset: 0x00005875
		public IXmlNode CreateComment(string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007682 File Offset: 0x00005882
		public IXmlNode CreateTextNode(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000768F File Offset: 0x0000588F
		public IXmlNode CreateCDataSection(string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000769C File Offset: 0x0000589C
		public IXmlNode CreateWhitespace(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000076A9 File Offset: 0x000058A9
		public IXmlNode CreateSignificantWhitespace(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000076B6 File Offset: 0x000058B6
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000076C5 File Offset: 0x000058C5
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000076D3 File Offset: 0x000058D3
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000076E8 File Offset: 0x000058E8
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			string localName = MiscellaneousUtils.GetLocalName(qualifiedName);
			return new XElementWrapper(new XElement(XName.Get(localName, namespaceUri)));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000770D File Offset: 0x0000590D
		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007720 File Offset: 0x00005920
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value)
		{
			string localName = MiscellaneousUtils.GetLocalName(qualifiedName);
			return new XAttributeWrapper(new XAttribute(XName.Get(localName, namespaceUri), value));
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007746 File Offset: 0x00005946
		public IXmlElement DocumentElement
		{
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007768 File Offset: 0x00005968
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

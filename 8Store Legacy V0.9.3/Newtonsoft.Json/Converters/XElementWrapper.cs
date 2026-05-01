using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200002F RID: 47
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000078F1 File Offset: 0x00005AF1
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000078FE File Offset: 0x00005AFE
		public XElementWrapper(XElement element) : base(element)
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007908 File Offset: 0x00005B08
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007935 File Offset: 0x00005B35
		public override IList<IXmlNode> Attributes
		{
			get
			{
				return Enumerable.ToList<IXmlNode>(Enumerable.Cast<IXmlNode>(Enumerable.Select<XAttribute, XAttributeWrapper>(this.Element.Attributes(), (XAttribute a) => new XAttributeWrapper(a))));
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000796E File Offset: 0x00005B6E
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000797B File Offset: 0x00005B7B
		public override string Value
		{
			get
			{
				return this.Element.Value;
			}
			set
			{
				this.Element.Value = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007989 File Offset: 0x00005B89
		public override string LocalName
		{
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000799B File Offset: 0x00005B9B
		public override string NamespaceUri
		{
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000079AD File Offset: 0x00005BAD
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}
	}
}

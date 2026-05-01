using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000101 RID: 257
	[NullableContext(1)]
	[Nullable(0)]
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x000311AF File Offset: 0x0002F3AF
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000311BC File Offset: 0x0002F3BC
		public XElementWrapper(XElement element) : base(element)
		{
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000311C8 File Offset: 0x0002F3C8
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
			this._attributes = null;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x000311F4 File Offset: 0x0002F3F4
		public override List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.Element.HasAttributes && !this.HasImplicitNamespaceAttribute(this.NamespaceUri))
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>();
						foreach (XAttribute attribute in this.Element.Attributes())
						{
							this._attributes.Add(new XAttributeWrapper(attribute));
						}
						string namespaceUri = this.NamespaceUri;
						if (this.HasImplicitNamespaceAttribute(namespaceUri))
						{
							this._attributes.Insert(0, new XAttributeWrapper(new XAttribute("xmlns", namespaceUri)));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000312C8 File Offset: 0x0002F4C8
		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!StringUtils.IsNullOrEmpty(namespaceUri))
			{
				IXmlNode parentNode = this.ParentNode;
				if (namespaceUri != ((parentNode != null) ? parentNode.NamespaceUri : null) && StringUtils.IsNullOrEmpty(this.GetPrefixOfNamespace(namespaceUri)))
				{
					bool flag = false;
					if (this.Element.HasAttributes)
					{
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							if (xattribute.Name.LocalName == "xmlns" && StringUtils.IsNullOrEmpty(xattribute.Name.NamespaceName) && xattribute.Value == namespaceUri)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00031398 File Offset: 0x0002F598
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			IXmlNode result = base.AppendChild(newChild);
			this._attributes = null;
			return result;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x000313A8 File Offset: 0x0002F5A8
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x000313B5 File Offset: 0x0002F5B5
		[Nullable(2)]
		public override string Value
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Value;
			}
			[NullableContext(2)]
			set
			{
				this.Element.Value = (value ?? string.Empty);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x000313CC File Offset: 0x0002F5CC
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x000313DE File Offset: 0x0002F5DE
		[Nullable(2)]
		public override string NamespaceUri
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000313F0 File Offset: 0x0002F5F0
		[return: Nullable(2)]
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00031403 File Offset: 0x0002F603
		public bool IsEmpty
		{
			get
			{
				return this.Element.IsEmpty;
			}
		}

		// Token: 0x04000438 RID: 1080
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _attributes;
	}
}

using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200002E RID: 46
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000787B File Offset: 0x00005A7B
		private XAttribute Attribute
		{
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007888 File Offset: 0x00005A88
		public XAttributeWrapper(XAttribute attribute) : base(attribute)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007891 File Offset: 0x00005A91
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000789E File Offset: 0x00005A9E
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000078AC File Offset: 0x00005AAC
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000078BE File Offset: 0x00005ABE
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000078D0 File Offset: 0x00005AD0
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}

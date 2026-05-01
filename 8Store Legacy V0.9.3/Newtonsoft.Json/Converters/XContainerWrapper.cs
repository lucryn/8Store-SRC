using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000029 RID: 41
	internal class XContainerWrapper : XObjectWrapper
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000074FB File Offset: 0x000056FB
		private XContainer Container
		{
			get
			{
				return (XContainer)base.WrappedNode;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007508 File Offset: 0x00005708
		public XContainerWrapper(XContainer container) : base(container)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007519 File Offset: 0x00005719
		public override IList<IXmlNode> ChildNodes
		{
			get
			{
				return Enumerable.ToList<IXmlNode>(Enumerable.Select<XNode, IXmlNode>(this.Container.Nodes(), (XNode n) => XContainerWrapper.WrapNode(n)));
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000754D File Offset: 0x0000574D
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Container.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Container.Parent);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007570 File Offset: 0x00005770
		internal static IXmlNode WrapNode(XObject node)
		{
			if (node is XDocument)
			{
				return new XDocumentWrapper((XDocument)node);
			}
			if (node is XElement)
			{
				return new XElementWrapper((XElement)node);
			}
			if (node is XContainer)
			{
				return new XContainerWrapper((XContainer)node);
			}
			if (node is XProcessingInstruction)
			{
				return new XProcessingInstructionWrapper((XProcessingInstruction)node);
			}
			if (node is XText)
			{
				return new XTextWrapper((XText)node);
			}
			if (node is XComment)
			{
				return new XCommentWrapper((XComment)node);
			}
			if (node is XAttribute)
			{
				return new XAttributeWrapper((XAttribute)node);
			}
			return new XObjectWrapper(node);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000760F File Offset: 0x0000580F
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			this.Container.Add(newChild.WrappedNode);
			return newChild;
		}
	}
}

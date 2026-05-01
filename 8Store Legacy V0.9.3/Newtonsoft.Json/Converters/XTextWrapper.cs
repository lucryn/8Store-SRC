using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200002B RID: 43
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007799 File Offset: 0x00005999
		private XText Text
		{
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000077A6 File Offset: 0x000059A6
		public XTextWrapper(XText text) : base(text)
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000077AF File Offset: 0x000059AF
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000077BC File Offset: 0x000059BC
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000077CA File Offset: 0x000059CA
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}

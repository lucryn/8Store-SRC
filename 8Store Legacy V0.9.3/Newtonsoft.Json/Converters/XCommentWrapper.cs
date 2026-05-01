using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200002C RID: 44
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000077EB File Offset: 0x000059EB
		private XComment Text
		{
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000077F8 File Offset: 0x000059F8
		public XCommentWrapper(XComment text) : base(text)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007801 File Offset: 0x00005A01
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000780E File Offset: 0x00005A0E
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000781C File Offset: 0x00005A1C
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

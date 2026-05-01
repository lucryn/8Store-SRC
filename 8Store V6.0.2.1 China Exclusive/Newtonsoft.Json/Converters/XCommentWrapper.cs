using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FC RID: 252
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00030EA4 File Offset: 0x0002F0A4
		[Nullable(1)]
		private XComment Text
		{
			[NullableContext(1)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00030EB1 File Offset: 0x0002F0B1
		[NullableContext(1)]
		public XCommentWrapper(XComment text) : base(text)
		{
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00030EBA File Offset: 0x0002F0BA
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00030EC7 File Offset: 0x0002F0C7
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = (value ?? string.Empty);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00030EDE File Offset: 0x0002F0DE
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

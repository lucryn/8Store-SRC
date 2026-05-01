using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FB RID: 251
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00030E49 File Offset: 0x0002F049
		[Nullable(1)]
		private XText Text
		{
			[NullableContext(1)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00030E56 File Offset: 0x0002F056
		[NullableContext(1)]
		public XTextWrapper(XText text) : base(text)
		{
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00030E5F File Offset: 0x0002F05F
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x00030E6C File Offset: 0x0002F06C
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00030E83 File Offset: 0x0002F083
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

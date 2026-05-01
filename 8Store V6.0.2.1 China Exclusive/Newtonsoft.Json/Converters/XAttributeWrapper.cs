using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000100 RID: 256
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00031130 File Offset: 0x0002F330
		[Nullable(1)]
		private XAttribute Attribute
		{
			[NullableContext(1)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003113D File Offset: 0x0002F33D
		[NullableContext(1)]
		public XAttributeWrapper(XAttribute attribute) : base(attribute)
		{
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00031146 File Offset: 0x0002F346
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x00031153 File Offset: 0x0002F353
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = (value ?? string.Empty);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0003116A File Offset: 0x0002F36A
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0003117C File Offset: 0x0002F37C
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0003118E File Offset: 0x0002F38E
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

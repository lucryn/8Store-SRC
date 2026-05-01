using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FD RID: 253
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00030EFF File Offset: 0x0002F0FF
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00030F0C File Offset: 0x0002F10C
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction) : base(processingInstruction)
		{
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00030F15 File Offset: 0x0002F115
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00030F22 File Offset: 0x0002F122
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00030F2F File Offset: 0x0002F12F
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = (value ?? string.Empty);
			}
		}
	}
}

using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200002D RID: 45
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000783D File Offset: 0x00005A3D
		private XProcessingInstruction ProcessingInstruction
		{
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000784A File Offset: 0x00005A4A
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction) : base(processingInstruction)
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007853 File Offset: 0x00005A53
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007860 File Offset: 0x00005A60
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000786D File Offset: 0x00005A6D
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}

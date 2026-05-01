using System;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000028 RID: 40
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007493 File Offset: 0x00005693
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000749B File Offset: 0x0000569B
		internal XDeclaration Declaration { get; private set; }

		// Token: 0x060001A1 RID: 417 RVA: 0x000074A4 File Offset: 0x000056A4
		public XDeclarationWrapper(XDeclaration declaration) : base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000074B4 File Offset: 0x000056B4
		public override XmlNodeType NodeType
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000074B8 File Offset: 0x000056B8
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000074C5 File Offset: 0x000056C5
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000074D2 File Offset: 0x000056D2
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000074E0 File Offset: 0x000056E0
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000074ED File Offset: 0x000056ED
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}

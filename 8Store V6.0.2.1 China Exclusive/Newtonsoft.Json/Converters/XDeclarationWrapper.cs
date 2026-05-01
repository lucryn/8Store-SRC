using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F8 RID: 248
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00030C03 File Offset: 0x0002EE03
		[Nullable(1)]
		internal XDeclaration Declaration { [NullableContext(1)] get; }

		// Token: 0x06000C66 RID: 3174 RVA: 0x00030C0B File Offset: 0x0002EE0B
		[NullableContext(1)]
		public XDeclarationWrapper(XDeclaration declaration) : base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00030C1B File Offset: 0x0002EE1B
		public override XmlNodeType NodeType
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00030C1F File Offset: 0x0002EE1F
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00030C2C File Offset: 0x0002EE2C
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00030C39 File Offset: 0x0002EE39
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

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00030C47 File Offset: 0x0002EE47
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00030C54 File Offset: 0x0002EE54
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

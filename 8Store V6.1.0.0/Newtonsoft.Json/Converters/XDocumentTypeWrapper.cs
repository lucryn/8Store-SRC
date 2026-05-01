using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F9 RID: 249
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x00030C62 File Offset: 0x0002EE62
		[NullableContext(1)]
		public XDocumentTypeWrapper(XDocumentType documentType) : base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00030C72 File Offset: 0x0002EE72
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00030C7F File Offset: 0x0002EE7F
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00030C8C File Offset: 0x0002EE8C
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00030C99 File Offset: 0x0002EE99
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00030CA6 File Offset: 0x0002EEA6
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x04000435 RID: 1077
		[Nullable(1)]
		private readonly XDocumentType _documentType;
	}
}

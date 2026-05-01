using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FF RID: 255
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x000310DE File Offset: 0x0002F2DE
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x000310ED File Offset: 0x0002F2ED
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x000310F5 File Offset: 0x0002F2F5
		public virtual XmlNodeType NodeType
		{
			get
			{
				XObject xmlObject = this._xmlObject;
				if (xmlObject == null)
				{
					return 0;
				}
				return xmlObject.NodeType;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00031108 File Offset: 0x0002F308
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0003110B File Offset: 0x0002F30B
		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00031112 File Offset: 0x0002F312
		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00031119 File Offset: 0x0002F319
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0003111C File Offset: 0x0002F31C
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0003111F File Offset: 0x0002F31F
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00031126 File Offset: 0x0002F326
		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0003112D File Offset: 0x0002F32D
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000437 RID: 1079
		private readonly XObject _xmlObject;
	}
}

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000027 RID: 39
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000744B File Offset: 0x0000564B
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000745A File Offset: 0x0000565A
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007462 File Offset: 0x00005662
		public virtual XmlNodeType NodeType
		{
			get
			{
				return this._xmlObject.NodeType;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000746F File Offset: 0x0000566F
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007472 File Offset: 0x00005672
		public virtual IList<IXmlNode> ChildNodes
		{
			get
			{
				return new List<IXmlNode>();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007479 File Offset: 0x00005679
		public virtual IList<IXmlNode> Attributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000747C File Offset: 0x0000567C
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000747F File Offset: 0x0000567F
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00007482 File Offset: 0x00005682
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

		// Token: 0x0600019D RID: 413 RVA: 0x00007489 File Offset: 0x00005689
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00007490 File Offset: 0x00005690
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000092 RID: 146
		private readonly XObject _xmlObject;
	}
}

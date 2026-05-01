using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x02000046 RID: 70
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlSystemBaseType : IXamlType
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0001A6C3 File Offset: 0x000188C3
		public XamlSystemBaseType(string fullName, Type underlyingType)
		{
			this._fullName = fullName;
			this._underlyingType = underlyingType;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001A6DC File Offset: 0x000188DC
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001A6F4 File Offset: 0x000188F4
		public Type UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual IXamlType BaseType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual IXamlMember ContentProperty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual IXamlMember GetMember(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsArray
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsConstructible
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsDictionary
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsMarkupExtension
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsBindable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsReturnTypeStub
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual bool IsLocalType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual IXamlType ItemType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual IXamlType KeyType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual object ActivateInstance()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual void AddToMap(object instance, object key, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual void AddToVector(object instance, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual void RunInitializer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00015B76 File Offset: 0x00013D76
		public virtual object CreateFromString(string input)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001FF RID: 511
		private string _fullName;

		// Token: 0x04000200 RID: 512
		private Type _underlyingType;
	}
}

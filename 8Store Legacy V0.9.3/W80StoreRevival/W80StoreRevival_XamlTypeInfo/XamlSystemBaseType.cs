using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival.W80StoreRevival_XamlTypeInfo
{
	// Token: 0x02000018 RID: 24
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlSystemBaseType : IXamlType
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0001128F File Offset: 0x0000F48F
		public XamlSystemBaseType(string fullName, Type underlyingType)
		{
			this._fullName = fullName;
			this._underlyingType = underlyingType;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public Type UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public virtual IXamlType BaseType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public virtual IXamlMember ContentProperty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public virtual IXamlMember GetMember(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public virtual bool IsArray
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000112F8 File Offset: 0x0000F4F8
		public virtual bool IsCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00011300 File Offset: 0x0000F500
		public virtual bool IsConstructible
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00011308 File Offset: 0x0000F508
		public virtual bool IsDictionary
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00011310 File Offset: 0x0000F510
		public virtual bool IsMarkupExtension
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00011318 File Offset: 0x0000F518
		public virtual bool IsBindable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00011320 File Offset: 0x0000F520
		public virtual IXamlType ItemType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00011328 File Offset: 0x0000F528
		public virtual IXamlType KeyType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00011330 File Offset: 0x0000F530
		public virtual object ActivateInstance()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00011338 File Offset: 0x0000F538
		public virtual void AddToMap(object instance, object key, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00011340 File Offset: 0x0000F540
		public virtual void AddToVector(object instance, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00011348 File Offset: 0x0000F548
		public virtual void RunInitializer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00011350 File Offset: 0x0000F550
		public virtual object CreateFromString(string input)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000CA RID: 202
		private string _fullName;

		// Token: 0x040000CB RID: 203
		private Type _underlyingType;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x0200004B RID: 75
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlSystemBaseType : IXamlType
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0001BFB6 File Offset: 0x0001A1B6
		public XamlSystemBaseType(string fullName, Type underlyingType)
		{
			this._fullName = fullName;
			this._underlyingType = underlyingType;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001BFCC File Offset: 0x0001A1CC
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
		public Type UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual IXamlType BaseType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual IXamlMember ContentProperty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual IXamlMember GetMember(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsArray
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsConstructible
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsDictionary
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsMarkupExtension
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsBindable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsReturnTypeStub
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual bool IsLocalType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual IXamlType ItemType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual IXamlType KeyType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual object ActivateInstance()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual void AddToMap(object instance, object key, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual void AddToVector(object instance, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual void RunInitializer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public virtual object CreateFromString(string input)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000229 RID: 553
		private string _fullName;

		// Token: 0x0400022A RID: 554
		private Type _underlyingType;
	}
}

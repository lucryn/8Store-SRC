using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival.W80StoreRevival_XamlTypeInfo
{
	// Token: 0x0200001F RID: 31
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlMember : IXamlMember
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000117F1 File Offset: 0x0000F9F1
		public XamlMember(XamlTypeInfoProvider provider, string name, string typeName)
		{
			this._name = name;
			this._typeName = typeName;
			this._provider = provider;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00011814 File Offset: 0x0000FA14
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0001182C File Offset: 0x0000FA2C
		public IXamlType Type
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._typeName);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0001184F File Offset: 0x0000FA4F
		public void SetTargetTypeName(string targetTypeName)
		{
			this._targetTypeName = targetTypeName;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0001185C File Offset: 0x0000FA5C
		public IXamlType TargetType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._targetTypeName);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0001187F File Offset: 0x0000FA7F
		public void SetIsAttachable()
		{
			this._isAttachable = true;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0001188C File Offset: 0x0000FA8C
		public bool IsAttachable
		{
			get
			{
				return this._isAttachable;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public void SetIsDependencyProperty()
		{
			this._isDependencyProperty = true;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public bool IsDependencyProperty
		{
			get
			{
				return this._isDependencyProperty;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public void SetIsReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000118D4 File Offset: 0x0000FAD4
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000118EC File Offset: 0x0000FAEC
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00011903 File Offset: 0x0000FB03
		public Getter Getter { get; set; }

		// Token: 0x06000152 RID: 338 RVA: 0x0001190C File Offset: 0x0000FB0C
		public object GetValue(object instance)
		{
			if (this.Getter != null)
			{
				return this.Getter(instance);
			}
			throw new InvalidOperationException("GetValue");
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00011944 File Offset: 0x0000FB44
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0001195B File Offset: 0x0000FB5B
		public Setter Setter { get; set; }

		// Token: 0x06000155 RID: 341 RVA: 0x00011964 File Offset: 0x0000FB64
		public void SetValue(object instance, object value)
		{
			if (this.Setter != null)
			{
				this.Setter(instance, value);
				return;
			}
			throw new InvalidOperationException("SetValue");
		}

		// Token: 0x040000D9 RID: 217
		private XamlTypeInfoProvider _provider;

		// Token: 0x040000DA RID: 218
		private string _name;

		// Token: 0x040000DB RID: 219
		private bool _isAttachable;

		// Token: 0x040000DC RID: 220
		private bool _isDependencyProperty;

		// Token: 0x040000DD RID: 221
		private bool _isReadOnly;

		// Token: 0x040000DE RID: 222
		private string _typeName;

		// Token: 0x040000DF RID: 223
		private string _targetTypeName;
	}
}

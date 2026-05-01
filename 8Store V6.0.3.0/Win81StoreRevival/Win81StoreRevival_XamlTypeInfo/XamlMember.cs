using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x0200004D RID: 77
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlMember : IXamlMember
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x0001AB86 File Offset: 0x00018D86
		public XamlMember(XamlTypeInfoProvider provider, string name, string typeName)
		{
			this._name = name;
			this._typeName = typeName;
			this._provider = provider;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001ABA8 File Offset: 0x00018DA8
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		public IXamlType Type
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._typeName);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001ABE3 File Offset: 0x00018DE3
		public void SetTargetTypeName(string targetTypeName)
		{
			this._targetTypeName = targetTypeName;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0001ABF0 File Offset: 0x00018DF0
		public IXamlType TargetType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._targetTypeName);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001AC13 File Offset: 0x00018E13
		public void SetIsAttachable()
		{
			this._isAttachable = true;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001AC20 File Offset: 0x00018E20
		public bool IsAttachable
		{
			get
			{
				return this._isAttachable;
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001AC38 File Offset: 0x00018E38
		public void SetIsDependencyProperty()
		{
			this._isDependencyProperty = true;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001AC44 File Offset: 0x00018E44
		public bool IsDependencyProperty
		{
			get
			{
				return this._isDependencyProperty;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001AC5C File Offset: 0x00018E5C
		public void SetIsReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0001AC68 File Offset: 0x00018E68
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001AC80 File Offset: 0x00018E80
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0001AC88 File Offset: 0x00018E88
		public Getter Getter { get; set; }

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001AC94 File Offset: 0x00018E94
		public object GetValue(object instance)
		{
			bool flag = this.Getter != null;
			if (flag)
			{
				return this.Getter(instance);
			}
			throw new InvalidOperationException("GetValue");
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001ACCA File Offset: 0x00018ECA
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0001ACD2 File Offset: 0x00018ED2
		public Setter Setter { get; set; }

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001ACDC File Offset: 0x00018EDC
		public void SetValue(object instance, object value)
		{
			bool flag = this.Setter != null;
			if (flag)
			{
				this.Setter(instance, value);
				return;
			}
			throw new InvalidOperationException("SetValue");
		}

		// Token: 0x04000210 RID: 528
		private XamlTypeInfoProvider _provider;

		// Token: 0x04000211 RID: 529
		private string _name;

		// Token: 0x04000212 RID: 530
		private bool _isAttachable;

		// Token: 0x04000213 RID: 531
		private bool _isDependencyProperty;

		// Token: 0x04000214 RID: 532
		private bool _isReadOnly;

		// Token: 0x04000215 RID: 533
		private string _typeName;

		// Token: 0x04000216 RID: 534
		private string _targetTypeName;
	}
}

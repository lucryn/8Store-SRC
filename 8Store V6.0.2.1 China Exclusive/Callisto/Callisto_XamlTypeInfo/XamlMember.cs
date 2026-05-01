using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace Callisto.Callisto_XamlTypeInfo
{
	// Token: 0x02000048 RID: 72
	[DebuggerNonUserCode]
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	internal class XamlMember : IXamlMember
	{
		// Token: 0x06000370 RID: 880 RVA: 0x000107A2 File Offset: 0x0000E9A2
		public XamlMember(XamlTypeInfoProvider provider, string name, string typeName)
		{
			this._name = name;
			this._typeName = typeName;
			this._provider = provider;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000107BF File Offset: 0x0000E9BF
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000107C7 File Offset: 0x0000E9C7
		public IXamlType Type
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._typeName);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000107DA File Offset: 0x0000E9DA
		public void SetTargetTypeName(string targetTypeName)
		{
			this._targetTypeName = targetTypeName;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000107E3 File Offset: 0x0000E9E3
		public IXamlType TargetType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._targetTypeName);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000107F6 File Offset: 0x0000E9F6
		public void SetIsAttachable()
		{
			this._isAttachable = true;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000107FF File Offset: 0x0000E9FF
		public bool IsAttachable
		{
			get
			{
				return this._isAttachable;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010807 File Offset: 0x0000EA07
		public void SetIsDependencyProperty()
		{
			this._isDependencyProperty = true;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00010810 File Offset: 0x0000EA10
		public bool IsDependencyProperty
		{
			get
			{
				return this._isDependencyProperty;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010818 File Offset: 0x0000EA18
		public void SetIsReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00010821 File Offset: 0x0000EA21
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00010829 File Offset: 0x0000EA29
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00010831 File Offset: 0x0000EA31
		public Getter Getter { get; set; }

		// Token: 0x0600037D RID: 893 RVA: 0x0001083A File Offset: 0x0000EA3A
		public object GetValue(object instance)
		{
			if (this.Getter != null)
			{
				return this.Getter(instance);
			}
			throw new InvalidOperationException("GetValue");
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0001085B File Offset: 0x0000EA5B
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00010863 File Offset: 0x0000EA63
		public Setter Setter { get; set; }

		// Token: 0x06000380 RID: 896 RVA: 0x0001086C File Offset: 0x0000EA6C
		public void SetValue(object instance, object value)
		{
			if (this.Setter != null)
			{
				this.Setter(instance, value);
				return;
			}
			throw new InvalidOperationException("SetValue");
		}

		// Token: 0x0400014B RID: 331
		private XamlTypeInfoProvider _provider;

		// Token: 0x0400014C RID: 332
		private string _name;

		// Token: 0x0400014D RID: 333
		private bool _isAttachable;

		// Token: 0x0400014E RID: 334
		private bool _isDependencyProperty;

		// Token: 0x0400014F RID: 335
		private bool _isReadOnly;

		// Token: 0x04000150 RID: 336
		private string _typeName;

		// Token: 0x04000151 RID: 337
		private string _targetTypeName;
	}
}

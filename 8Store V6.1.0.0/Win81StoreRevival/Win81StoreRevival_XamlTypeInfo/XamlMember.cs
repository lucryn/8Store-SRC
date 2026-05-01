using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x02000052 RID: 82
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlMember : IXamlMember
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x0001C2FF File Offset: 0x0001A4FF
		public XamlMember(XamlTypeInfoProvider provider, string name, string typeName)
		{
			this._name = name;
			this._typeName = typeName;
			this._provider = provider;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001C31C File Offset: 0x0001A51C
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001C324 File Offset: 0x0001A524
		public IXamlType Type
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._typeName);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001C337 File Offset: 0x0001A537
		public void SetTargetTypeName(string targetTypeName)
		{
			this._targetTypeName = targetTypeName;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001C340 File Offset: 0x0001A540
		public IXamlType TargetType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._targetTypeName);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001C353 File Offset: 0x0001A553
		public void SetIsAttachable()
		{
			this._isAttachable = true;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001C35C File Offset: 0x0001A55C
		public bool IsAttachable
		{
			get
			{
				return this._isAttachable;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001C364 File Offset: 0x0001A564
		public void SetIsDependencyProperty()
		{
			this._isDependencyProperty = true;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001C36D File Offset: 0x0001A56D
		public bool IsDependencyProperty
		{
			get
			{
				return this._isDependencyProperty;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001C375 File Offset: 0x0001A575
		public void SetIsReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001C37E File Offset: 0x0001A57E
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001C386 File Offset: 0x0001A586
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0001C38E File Offset: 0x0001A58E
		public Getter Getter { get; set; }

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001C397 File Offset: 0x0001A597
		public object GetValue(object instance)
		{
			if (this.Getter != null)
			{
				return this.Getter(instance);
			}
			throw new InvalidOperationException("GetValue");
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		public Setter Setter { get; set; }

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001C3C9 File Offset: 0x0001A5C9
		public void SetValue(object instance, object value)
		{
			if (this.Setter != null)
			{
				this.Setter(instance, value);
				return;
			}
			throw new InvalidOperationException("SetValue");
		}

		// Token: 0x0400023A RID: 570
		private XamlTypeInfoProvider _provider;

		// Token: 0x0400023B RID: 571
		private string _name;

		// Token: 0x0400023C RID: 572
		private bool _isAttachable;

		// Token: 0x0400023D RID: 573
		private bool _isDependencyProperty;

		// Token: 0x0400023E RID: 574
		private bool _isReadOnly;

		// Token: 0x0400023F RID: 575
		private string _typeName;

		// Token: 0x04000240 RID: 576
		private string _targetTypeName;
	}
}

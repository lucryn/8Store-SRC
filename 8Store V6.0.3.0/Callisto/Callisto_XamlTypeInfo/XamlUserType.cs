using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Markup;

namespace Callisto.Callisto_XamlTypeInfo
{
	// Token: 0x02000045 RID: 69
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlUserType : XamlSystemBaseType
	{
		// Token: 0x06000347 RID: 839 RVA: 0x00010482 File Offset: 0x0000E682
		public XamlUserType(XamlTypeInfoProvider provider, string fullName, Type fullType, IXamlType baseType) : base(fullName, fullType)
		{
			this._provider = provider;
			this._baseType = baseType;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0001049B File Offset: 0x0000E69B
		public override IXamlType BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000104A3 File Offset: 0x0000E6A3
		public override bool IsArray
		{
			get
			{
				return this._isArray;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000104AB File Offset: 0x0000E6AB
		public override bool IsCollection
		{
			get
			{
				return this.CollectionAdd != null;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000104B9 File Offset: 0x0000E6B9
		public override bool IsConstructible
		{
			get
			{
				return this.Activator != null;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000104C7 File Offset: 0x0000E6C7
		public override bool IsDictionary
		{
			get
			{
				return this.DictionaryAdd != null;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000104D5 File Offset: 0x0000E6D5
		public override bool IsMarkupExtension
		{
			get
			{
				return this._isMarkupExtension;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000104DD File Offset: 0x0000E6DD
		public override bool IsBindable
		{
			get
			{
				return this._isBindable;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000104E5 File Offset: 0x0000E6E5
		public override bool IsReturnTypeStub
		{
			get
			{
				return this._isReturnTypeStub;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000104ED File Offset: 0x0000E6ED
		public override IXamlMember ContentProperty
		{
			get
			{
				return this._provider.GetMemberByLongName(this._contentPropertyName);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00010500 File Offset: 0x0000E700
		public override IXamlType ItemType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._itemTypeName);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00010513 File Offset: 0x0000E713
		public override IXamlType KeyType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._keyTypeName);
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010528 File Offset: 0x0000E728
		public override IXamlMember GetMember(string name)
		{
			if (this._memberNames == null)
			{
				return null;
			}
			string longMemberName;
			if (this._memberNames.TryGetValue(name, ref longMemberName))
			{
				return this._provider.GetMemberByLongName(longMemberName);
			}
			return null;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001055D File Offset: 0x0000E75D
		public override object ActivateInstance()
		{
			return this.Activator();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001056A File Offset: 0x0000E76A
		public override void AddToMap(object instance, object key, object item)
		{
			this.DictionaryAdd(instance, key, item);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001057A File Offset: 0x0000E77A
		public override void AddToVector(object instance, object item)
		{
			this.CollectionAdd(instance, item);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010589 File Offset: 0x0000E789
		public override void RunInitializer()
		{
			RuntimeHelpers.RunClassConstructor(base.UnderlyingType.TypeHandle);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001059C File Offset: 0x0000E79C
		public override object CreateFromString(string input)
		{
			if (this._enumValues != null)
			{
				int num = 0;
				string[] array = input.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					int num2 = 0;
					try
					{
						object obj;
						if (this._enumValues.TryGetValue(text.Trim(), ref obj))
						{
							num2 = Convert.ToInt32(obj);
						}
						else
						{
							try
							{
								num2 = Convert.ToInt32(text.Trim());
							}
							catch (FormatException)
							{
								foreach (string text2 in this._enumValues.Keys)
								{
									if (string.Compare(text.Trim(), text2, 5) == 0 && this._enumValues.TryGetValue(text2.Trim(), ref obj))
									{
										num2 = Convert.ToInt32(obj);
										break;
									}
								}
							}
						}
						num |= num2;
					}
					catch (FormatException)
					{
						throw new ArgumentException(input, base.FullName);
					}
				}
				return num;
			}
			throw new ArgumentException(input, base.FullName);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000106DC File Offset: 0x0000E8DC
		// (set) Token: 0x0600035A RID: 858 RVA: 0x000106E4 File Offset: 0x0000E8E4
		public Activator Activator { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000106ED File Offset: 0x0000E8ED
		// (set) Token: 0x0600035C RID: 860 RVA: 0x000106F5 File Offset: 0x0000E8F5
		public AddToCollection CollectionAdd { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000106FE File Offset: 0x0000E8FE
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00010706 File Offset: 0x0000E906
		public AddToDictionary DictionaryAdd { get; set; }

		// Token: 0x0600035F RID: 863 RVA: 0x0001070F File Offset: 0x0000E90F
		public void SetContentPropertyName(string contentPropertyName)
		{
			this._contentPropertyName = contentPropertyName;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00010718 File Offset: 0x0000E918
		public void SetIsArray()
		{
			this._isArray = true;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00010721 File Offset: 0x0000E921
		public void SetIsMarkupExtension()
		{
			this._isMarkupExtension = true;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001072A File Offset: 0x0000E92A
		public void SetIsBindable()
		{
			this._isBindable = true;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00010733 File Offset: 0x0000E933
		public void SetIsReturnTypeStub()
		{
			this._isReturnTypeStub = true;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001073C File Offset: 0x0000E93C
		public void SetItemTypeName(string itemTypeName)
		{
			this._itemTypeName = itemTypeName;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00010745 File Offset: 0x0000E945
		public void SetKeyTypeName(string keyTypeName)
		{
			this._keyTypeName = keyTypeName;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001074E File Offset: 0x0000E94E
		public void AddMemberName(string shortName)
		{
			if (this._memberNames == null)
			{
				this._memberNames = new Dictionary<string, string>();
			}
			this._memberNames.Add(shortName, base.FullName + "." + shortName);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00010780 File Offset: 0x0000E980
		public void AddEnumValue(string name, object value)
		{
			if (this._enumValues == null)
			{
				this._enumValues = new Dictionary<string, object>();
			}
			this._enumValues.Add(name, value);
		}

		// Token: 0x0400013D RID: 317
		private XamlTypeInfoProvider _provider;

		// Token: 0x0400013E RID: 318
		private IXamlType _baseType;

		// Token: 0x0400013F RID: 319
		private bool _isArray;

		// Token: 0x04000140 RID: 320
		private bool _isMarkupExtension;

		// Token: 0x04000141 RID: 321
		private bool _isBindable;

		// Token: 0x04000142 RID: 322
		private bool _isReturnTypeStub;

		// Token: 0x04000143 RID: 323
		private string _contentPropertyName;

		// Token: 0x04000144 RID: 324
		private string _itemTypeName;

		// Token: 0x04000145 RID: 325
		private string _keyTypeName;

		// Token: 0x04000146 RID: 326
		private Dictionary<string, string> _memberNames;

		// Token: 0x04000147 RID: 327
		private Dictionary<string, object> _enumValues;
	}
}

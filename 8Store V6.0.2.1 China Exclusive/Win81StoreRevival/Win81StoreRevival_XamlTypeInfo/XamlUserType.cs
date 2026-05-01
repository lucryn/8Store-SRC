using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x0200004A RID: 74
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlUserType : XamlSystemBaseType
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0001A70C File Offset: 0x0001890C
		public XamlUserType(XamlTypeInfoProvider provider, string fullName, Type fullType, IXamlType baseType) : base(fullName, fullType)
		{
			this._provider = provider;
			this._baseType = baseType;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001A728 File Offset: 0x00018928
		public override IXamlType BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001A740 File Offset: 0x00018940
		public override bool IsArray
		{
			get
			{
				return this._isArray;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001A758 File Offset: 0x00018958
		public override bool IsCollection
		{
			get
			{
				return this.CollectionAdd != null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001A774 File Offset: 0x00018974
		public override bool IsConstructible
		{
			get
			{
				return this.Activator != null;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001A790 File Offset: 0x00018990
		public override bool IsDictionary
		{
			get
			{
				return this.DictionaryAdd != null;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001A7AC File Offset: 0x000189AC
		public override bool IsMarkupExtension
		{
			get
			{
				return this._isMarkupExtension;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public override bool IsBindable
		{
			get
			{
				return this._isBindable;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001A7DC File Offset: 0x000189DC
		public override bool IsReturnTypeStub
		{
			get
			{
				return this._isReturnTypeStub;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public override bool IsLocalType
		{
			get
			{
				return this._isLocalType;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001A80C File Offset: 0x00018A0C
		public override IXamlMember ContentProperty
		{
			get
			{
				return this._provider.GetMemberByLongName(this._contentPropertyName);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001A830 File Offset: 0x00018A30
		public override IXamlType ItemType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._itemTypeName);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001A854 File Offset: 0x00018A54
		public override IXamlType KeyType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._keyTypeName);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001A878 File Offset: 0x00018A78
		public override IXamlMember GetMember(string name)
		{
			bool flag = this._memberNames == null;
			IXamlMember result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string longMemberName;
				bool flag2 = this._memberNames.TryGetValue(name, ref longMemberName);
				if (flag2)
				{
					result = this._provider.GetMemberByLongName(longMemberName);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public override object ActivateInstance()
		{
			return this.Activator();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001A8DD File Offset: 0x00018ADD
		public override void AddToMap(object instance, object key, object item)
		{
			this.DictionaryAdd(instance, key, item);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001A8EF File Offset: 0x00018AEF
		public override void AddToVector(object instance, object item)
		{
			this.CollectionAdd(instance, item);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001A900 File Offset: 0x00018B00
		public override void RunInitializer()
		{
			RuntimeHelpers.RunClassConstructor(base.UnderlyingType.TypeHandle);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001A914 File Offset: 0x00018B14
		public override object CreateFromString(string input)
		{
			bool flag = this._enumValues != null;
			if (flag)
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
						bool flag2 = this._enumValues.TryGetValue(text.Trim(), ref obj);
						if (flag2)
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
									bool flag3 = string.Compare(text.Trim(), text2, 5) == 0;
									if (flag3)
									{
										bool flag4 = this._enumValues.TryGetValue(text2.Trim(), ref obj);
										if (flag4)
										{
											num2 = Convert.ToInt32(obj);
											break;
										}
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001AA84 File Offset: 0x00018C84
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001AA8C File Offset: 0x00018C8C
		public Activator Activator { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001AA95 File Offset: 0x00018C95
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001AA9D File Offset: 0x00018C9D
		public AddToCollection CollectionAdd { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001AAA6 File Offset: 0x00018CA6
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0001AAAE File Offset: 0x00018CAE
		public AddToDictionary DictionaryAdd { get; set; }

		// Token: 0x06000487 RID: 1159 RVA: 0x0001AAB7 File Offset: 0x00018CB7
		public void SetContentPropertyName(string contentPropertyName)
		{
			this._contentPropertyName = contentPropertyName;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001AAC1 File Offset: 0x00018CC1
		public void SetIsArray()
		{
			this._isArray = true;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001AACB File Offset: 0x00018CCB
		public void SetIsMarkupExtension()
		{
			this._isMarkupExtension = true;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001AAD5 File Offset: 0x00018CD5
		public void SetIsBindable()
		{
			this._isBindable = true;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001AADF File Offset: 0x00018CDF
		public void SetIsReturnTypeStub()
		{
			this._isReturnTypeStub = true;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001AAE9 File Offset: 0x00018CE9
		public void SetIsLocalType()
		{
			this._isLocalType = true;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001AAF3 File Offset: 0x00018CF3
		public void SetItemTypeName(string itemTypeName)
		{
			this._itemTypeName = itemTypeName;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001AAFD File Offset: 0x00018CFD
		public void SetKeyTypeName(string keyTypeName)
		{
			this._keyTypeName = keyTypeName;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001AB08 File Offset: 0x00018D08
		public void AddMemberName(string shortName)
		{
			bool flag = this._memberNames == null;
			if (flag)
			{
				this._memberNames = new Dictionary<string, string>();
			}
			this._memberNames.Add(shortName, base.FullName + "." + shortName);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001AB50 File Offset: 0x00018D50
		public void AddEnumValue(string name, object value)
		{
			bool flag = this._enumValues == null;
			if (flag)
			{
				this._enumValues = new Dictionary<string, object>();
			}
			this._enumValues.Add(name, value);
		}

		// Token: 0x04000201 RID: 513
		private XamlTypeInfoProvider _provider;

		// Token: 0x04000202 RID: 514
		private IXamlType _baseType;

		// Token: 0x04000203 RID: 515
		private bool _isArray;

		// Token: 0x04000204 RID: 516
		private bool _isMarkupExtension;

		// Token: 0x04000205 RID: 517
		private bool _isBindable;

		// Token: 0x04000206 RID: 518
		private bool _isReturnTypeStub;

		// Token: 0x04000207 RID: 519
		private bool _isLocalType;

		// Token: 0x04000208 RID: 520
		private string _contentPropertyName;

		// Token: 0x04000209 RID: 521
		private string _itemTypeName;

		// Token: 0x0400020A RID: 522
		private string _keyTypeName;

		// Token: 0x0400020B RID: 523
		private Dictionary<string, string> _memberNames;

		// Token: 0x0400020C RID: 524
		private Dictionary<string, object> _enumValues;
	}
}

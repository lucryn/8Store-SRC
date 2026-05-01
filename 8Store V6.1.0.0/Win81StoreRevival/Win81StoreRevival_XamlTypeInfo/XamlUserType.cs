using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x0200004F RID: 79
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlUserType : XamlSystemBaseType
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0001BFE3 File Offset: 0x0001A1E3
		public XamlUserType(XamlTypeInfoProvider provider, string fullName, Type fullType, IXamlType baseType) : base(fullName, fullType)
		{
			this._provider = provider;
			this._baseType = baseType;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		public override IXamlType BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001C004 File Offset: 0x0001A204
		public override bool IsArray
		{
			get
			{
				return this._isArray;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001C00C File Offset: 0x0001A20C
		public override bool IsCollection
		{
			get
			{
				return this.CollectionAdd != null;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001C017 File Offset: 0x0001A217
		public override bool IsConstructible
		{
			get
			{
				return this.Activator != null;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001C022 File Offset: 0x0001A222
		public override bool IsDictionary
		{
			get
			{
				return this.DictionaryAdd != null;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001C02D File Offset: 0x0001A22D
		public override bool IsMarkupExtension
		{
			get
			{
				return this._isMarkupExtension;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001C035 File Offset: 0x0001A235
		public override bool IsBindable
		{
			get
			{
				return this._isBindable;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001C03D File Offset: 0x0001A23D
		public override bool IsReturnTypeStub
		{
			get
			{
				return this._isReturnTypeStub;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001C045 File Offset: 0x0001A245
		public override bool IsLocalType
		{
			get
			{
				return this._isLocalType;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001C04D File Offset: 0x0001A24D
		public override IXamlMember ContentProperty
		{
			get
			{
				return this._provider.GetMemberByLongName(this._contentPropertyName);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001C060 File Offset: 0x0001A260
		public override IXamlType ItemType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._itemTypeName);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001C073 File Offset: 0x0001A273
		public override IXamlType KeyType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._keyTypeName);
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001C088 File Offset: 0x0001A288
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

		// Token: 0x060005AA RID: 1450 RVA: 0x0001C0BD File Offset: 0x0001A2BD
		public override object ActivateInstance()
		{
			return this.Activator();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001C0CA File Offset: 0x0001A2CA
		public override void AddToMap(object instance, object key, object item)
		{
			this.DictionaryAdd(instance, key, item);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001C0DA File Offset: 0x0001A2DA
		public override void AddToVector(object instance, object item)
		{
			this.CollectionAdd(instance, item);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001C0E9 File Offset: 0x0001A2E9
		public override void RunInitializer()
		{
			RuntimeHelpers.RunClassConstructor(base.UnderlyingType.TypeHandle);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001C0FC File Offset: 0x0001A2FC
		public override object CreateFromString(string input)
		{
			if (this._enumValues != null)
			{
				int num = 0;
				foreach (string text in input.Split(new char[]
				{
					','
				}))
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

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001C230 File Offset: 0x0001A430
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001C238 File Offset: 0x0001A438
		public Activator Activator { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001C241 File Offset: 0x0001A441
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0001C249 File Offset: 0x0001A449
		public AddToCollection CollectionAdd { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001C252 File Offset: 0x0001A452
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001C25A File Offset: 0x0001A45A
		public AddToDictionary DictionaryAdd { get; set; }

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001C263 File Offset: 0x0001A463
		public void SetContentPropertyName(string contentPropertyName)
		{
			this._contentPropertyName = contentPropertyName;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001C26C File Offset: 0x0001A46C
		public void SetIsArray()
		{
			this._isArray = true;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001C275 File Offset: 0x0001A475
		public void SetIsMarkupExtension()
		{
			this._isMarkupExtension = true;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001C27E File Offset: 0x0001A47E
		public void SetIsBindable()
		{
			this._isBindable = true;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001C287 File Offset: 0x0001A487
		public void SetIsReturnTypeStub()
		{
			this._isReturnTypeStub = true;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001C290 File Offset: 0x0001A490
		public void SetIsLocalType()
		{
			this._isLocalType = true;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001C299 File Offset: 0x0001A499
		public void SetItemTypeName(string itemTypeName)
		{
			this._itemTypeName = itemTypeName;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001C2A2 File Offset: 0x0001A4A2
		public void SetKeyTypeName(string keyTypeName)
		{
			this._keyTypeName = keyTypeName;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001C2AB File Offset: 0x0001A4AB
		public void AddMemberName(string shortName)
		{
			if (this._memberNames == null)
			{
				this._memberNames = new Dictionary<string, string>();
			}
			this._memberNames.Add(shortName, base.FullName + "." + shortName);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001C2DD File Offset: 0x0001A4DD
		public void AddEnumValue(string name, object value)
		{
			if (this._enumValues == null)
			{
				this._enumValues = new Dictionary<string, object>();
			}
			this._enumValues.Add(name, value);
		}

		// Token: 0x0400022B RID: 555
		private XamlTypeInfoProvider _provider;

		// Token: 0x0400022C RID: 556
		private IXamlType _baseType;

		// Token: 0x0400022D RID: 557
		private bool _isArray;

		// Token: 0x0400022E RID: 558
		private bool _isMarkupExtension;

		// Token: 0x0400022F RID: 559
		private bool _isBindable;

		// Token: 0x04000230 RID: 560
		private bool _isReturnTypeStub;

		// Token: 0x04000231 RID: 561
		private bool _isLocalType;

		// Token: 0x04000232 RID: 562
		private string _contentPropertyName;

		// Token: 0x04000233 RID: 563
		private string _itemTypeName;

		// Token: 0x04000234 RID: 564
		private string _keyTypeName;

		// Token: 0x04000235 RID: 565
		private Dictionary<string, string> _memberNames;

		// Token: 0x04000236 RID: 566
		private Dictionary<string, object> _enumValues;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival.W80StoreRevival_XamlTypeInfo
{
	// Token: 0x0200001C RID: 28
	[DebuggerNonUserCode]
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	internal class XamlUserType : XamlSystemBaseType
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00011358 File Offset: 0x0000F558
		public XamlUserType(XamlTypeInfoProvider provider, string fullName, Type fullType, IXamlType baseType) : base(fullName, fullType)
		{
			this._provider = provider;
			this._baseType = baseType;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00011374 File Offset: 0x0000F574
		public override IXamlType BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0001138C File Offset: 0x0000F58C
		public override bool IsArray
		{
			get
			{
				return this._isArray;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000113A4 File Offset: 0x0000F5A4
		public override bool IsCollection
		{
			get
			{
				return this.CollectionAdd != null;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000113C4 File Offset: 0x0000F5C4
		public override bool IsConstructible
		{
			get
			{
				return this.Activator != null;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public override bool IsDictionary
		{
			get
			{
				return this.DictionaryAdd != null;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00011404 File Offset: 0x0000F604
		public override bool IsMarkupExtension
		{
			get
			{
				return this._isMarkupExtension;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0001141C File Offset: 0x0000F61C
		public override bool IsBindable
		{
			get
			{
				return this._isBindable;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00011434 File Offset: 0x0000F634
		public override IXamlMember ContentProperty
		{
			get
			{
				return this._provider.GetMemberByLongName(this._contentPropertyName);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00011458 File Offset: 0x0000F658
		public override IXamlType ItemType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._itemTypeName);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0001147C File Offset: 0x0000F67C
		public override IXamlType KeyType
		{
			get
			{
				return this._provider.GetXamlTypeByName(this._keyTypeName);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public override IXamlMember GetMember(string name)
		{
			IXamlMember result;
			string longMemberName;
			if (this._memberNames == null)
			{
				result = null;
			}
			else if (this._memberNames.TryGetValue(name, ref longMemberName))
			{
				result = this._provider.GetMemberByLongName(longMemberName);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public override object ActivateInstance()
		{
			return this.Activator();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0001150D File Offset: 0x0000F70D
		public override void AddToMap(object instance, object key, object item)
		{
			this.DictionaryAdd(instance, key, item);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0001151F File Offset: 0x0000F71F
		public override void AddToVector(object instance, object item)
		{
			this.CollectionAdd(instance, item);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00011530 File Offset: 0x0000F730
		public override void RunInitializer()
		{
			RuntimeHelpers.RunClassConstructor(base.UnderlyingType.TypeHandle);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00011544 File Offset: 0x0000F744
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
									if (string.Compare(text.Trim(), text2, 5) == 0)
									{
										if (this._enumValues.TryGetValue(text2.Trim(), ref obj))
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000116D0 File Offset: 0x0000F8D0
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000116E7 File Offset: 0x0000F8E7
		public Activator Activator { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000116F0 File Offset: 0x0000F8F0
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00011707 File Offset: 0x0000F907
		public AddToCollection CollectionAdd { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00011710 File Offset: 0x0000F910
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00011727 File Offset: 0x0000F927
		public AddToDictionary DictionaryAdd { get; set; }

		// Token: 0x06000135 RID: 309 RVA: 0x00011730 File Offset: 0x0000F930
		public void SetContentPropertyName(string contentPropertyName)
		{
			this._contentPropertyName = contentPropertyName;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0001173A File Offset: 0x0000F93A
		public void SetIsArray()
		{
			this._isArray = true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00011744 File Offset: 0x0000F944
		public void SetIsMarkupExtension()
		{
			this._isMarkupExtension = true;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0001174E File Offset: 0x0000F94E
		public void SetIsBindable()
		{
			this._isBindable = true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00011758 File Offset: 0x0000F958
		public void SetItemTypeName(string itemTypeName)
		{
			this._itemTypeName = itemTypeName;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00011762 File Offset: 0x0000F962
		public void SetKeyTypeName(string keyTypeName)
		{
			this._keyTypeName = keyTypeName;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0001176C File Offset: 0x0000F96C
		public void AddMemberName(string shortName)
		{
			if (this._memberNames == null)
			{
				this._memberNames = new Dictionary<string, string>();
			}
			this._memberNames.Add(shortName, base.FullName + "." + shortName);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000117B8 File Offset: 0x0000F9B8
		public void AddEnumValue(string name, object value)
		{
			if (this._enumValues == null)
			{
				this._enumValues = new Dictionary<string, object>();
			}
			this._enumValues.Add(name, value);
		}

		// Token: 0x040000CC RID: 204
		private XamlTypeInfoProvider _provider;

		// Token: 0x040000CD RID: 205
		private IXamlType _baseType;

		// Token: 0x040000CE RID: 206
		private bool _isArray;

		// Token: 0x040000CF RID: 207
		private bool _isMarkupExtension;

		// Token: 0x040000D0 RID: 208
		private bool _isBindable;

		// Token: 0x040000D1 RID: 209
		private string _contentPropertyName;

		// Token: 0x040000D2 RID: 210
		private string _itemTypeName;

		// Token: 0x040000D3 RID: 211
		private string _keyTypeName;

		// Token: 0x040000D4 RID: 212
		private Dictionary<string, string> _memberNames;

		// Token: 0x040000D5 RID: 213
		private Dictionary<string, object> _enumValues;
	}
}

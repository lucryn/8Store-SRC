using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009A RID: 154
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonProperty
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001CB00 File Offset: 0x0001AD00
		internal JsonContract PropertyContract { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001CB09 File Offset: 0x0001AD09
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001CB11 File Offset: 0x0001AD11
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this._skipPropertyNameEscape = !JavaScriptUtils.ShouldEscapeJavaScriptString(this._propertyName, JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001CB33 File Offset: 0x0001AD33
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001CB3B File Offset: 0x0001AD3B
		public Type DeclaringType { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001CB44 File Offset: 0x0001AD44
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		public int? Order { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001CB55 File Offset: 0x0001AD55
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001CB5D File Offset: 0x0001AD5D
		public string UnderlyingName { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001CB66 File Offset: 0x0001AD66
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001CB6E File Offset: 0x0001AD6E
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001CB77 File Offset: 0x0001AD77
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001CB7F File Offset: 0x0001AD7F
		public IAttributeProvider AttributeProvider { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001CB88 File Offset: 0x0001AD88
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001CB90 File Offset: 0x0001AD90
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (this._propertyType != value)
				{
					this._propertyType = value;
					this._hasGeneratedDefaultValue = false;
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001CBA9 File Offset: 0x0001ADA9
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001CBB1 File Offset: 0x0001ADB1
		public JsonConverter Converter { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001CBBA File Offset: 0x0001ADBA
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001CBC2 File Offset: 0x0001ADC2
		[Obsolete("MemberConverter is obsolete. Use Converter instead.")]
		public JsonConverter MemberConverter
		{
			get
			{
				return this.Converter;
			}
			set
			{
				this.Converter = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001CBCB File Offset: 0x0001ADCB
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001CBD3 File Offset: 0x0001ADD3
		public bool Ignored { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001CBDC File Offset: 0x0001ADDC
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		public bool Readable { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001CBED File Offset: 0x0001ADED
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001CBF5 File Offset: 0x0001ADF5
		public bool Writable { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001CBFE File Offset: 0x0001ADFE
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001CC06 File Offset: 0x0001AE06
		public bool HasMemberAttribute { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001CC0F File Offset: 0x0001AE0F
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0001CC21 File Offset: 0x0001AE21
		public object DefaultValue
		{
			get
			{
				if (!this._hasExplicitDefaultValue)
				{
					return null;
				}
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001CC31 File Offset: 0x0001AE31
		internal object GetResolvedDefaultValue()
		{
			if (this._propertyType == null)
			{
				return null;
			}
			if (!this._hasExplicitDefaultValue && !this._hasGeneratedDefaultValue)
			{
				this._defaultValue = ReflectionUtils.GetDefaultValue(this._propertyType);
				this._hasGeneratedDefaultValue = true;
			}
			return this._defaultValue;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001CC6B File Offset: 0x0001AE6B
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001CC78 File Offset: 0x0001AE78
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001CC86 File Offset: 0x0001AE86
		public bool IsRequiredSpecified
		{
			get
			{
				return this._required != null;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001CC93 File Offset: 0x0001AE93
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001CC9B File Offset: 0x0001AE9B
		public bool? IsReference { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001CCB5 File Offset: 0x0001AEB5
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001CCBD File Offset: 0x0001AEBD
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001CCC6 File Offset: 0x0001AEC6
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001CCCE File Offset: 0x0001AECE
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001CCD7 File Offset: 0x0001AED7
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001CCDF File Offset: 0x0001AEDF
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001CCF9 File Offset: 0x0001AEF9
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001CD01 File Offset: 0x0001AF01
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> ShouldSerialize { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001CD0A File Offset: 0x0001AF0A
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001CD12 File Offset: 0x0001AF12
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> ShouldDeserialize { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001CD1B File Offset: 0x0001AF1B
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001CD23 File Offset: 0x0001AF23
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> GetIsSpecified { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001CD2C File Offset: 0x0001AF2C
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001CD34 File Offset: 0x0001AF34
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Action<object, object> SetIsSpecified { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }

		// Token: 0x06000752 RID: 1874 RVA: 0x0001CD3D File Offset: 0x0001AF3D
		[NullableContext(1)]
		public override string ToString()
		{
			return this.PropertyName ?? string.Empty;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001CD4E File Offset: 0x0001AF4E
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001CD56 File Offset: 0x0001AF56
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001CD5F File Offset: 0x0001AF5F
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001CD67 File Offset: 0x0001AF67
		public bool? ItemIsReference { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001CD70 File Offset: 0x0001AF70
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0001CD78 File Offset: 0x0001AF78
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001CD81 File Offset: 0x0001AF81
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001CD89 File Offset: 0x0001AF89
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x0600075B RID: 1883 RVA: 0x0001CD94 File Offset: 0x0001AF94
		[NullableContext(1)]
		internal void WritePropertyName(JsonWriter writer)
		{
			string propertyName = this.PropertyName;
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(propertyName, false);
				return;
			}
			writer.WritePropertyName(propertyName);
		}

		// Token: 0x040002DC RID: 732
		internal Required? _required;

		// Token: 0x040002DD RID: 733
		internal bool _hasExplicitDefaultValue;

		// Token: 0x040002DE RID: 734
		private object _defaultValue;

		// Token: 0x040002DF RID: 735
		private bool _hasGeneratedDefaultValue;

		// Token: 0x040002E0 RID: 736
		private string _propertyName;

		// Token: 0x040002E1 RID: 737
		internal bool _skipPropertyNameEscape;

		// Token: 0x040002E2 RID: 738
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]
		private Type _propertyType;
	}
}

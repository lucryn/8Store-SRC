using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000098 RID: 152
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001C7A5 File Offset: 0x0001A9A5
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001C7AD File Offset: 0x0001A9AD
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001C7B6 File Offset: 0x0001A9B6
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001C7BE File Offset: 0x0001A9BE
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001C7C7 File Offset: 0x0001A9C7
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001C7CF File Offset: 0x0001A9CF
		public Required? ItemRequired { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001C7E9 File Offset: 0x0001A9E9
		[Nullable(1)]
		public JsonPropertyCollection Properties { [NullableContext(1)] get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001C7F1 File Offset: 0x0001A9F1
		[Nullable(1)]
		public JsonPropertyCollection CreatorParameters
		{
			[NullableContext(1)]
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001C812 File Offset: 0x0001AA12
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001C81A File Offset: 0x0001AA1A
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001C823 File Offset: 0x0001AA23
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001C82B File Offset: 0x0001AA2B
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._parameterizedCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001C834 File Offset: 0x0001AA34
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001C83C File Offset: 0x0001AA3C
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001C845 File Offset: 0x0001AA45
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001C84D File Offset: 0x0001AA4D
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001C856 File Offset: 0x0001AA56
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001C85E File Offset: 0x0001AA5E
		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = (value != null && typeof(JToken).IsAssignableFrom(value));
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001C883 File Offset: 0x0001AA83
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001C88B File Offset: 0x0001AA8B
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Func<string, string> ExtensionDataNameResolver { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			1
		})] set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001C894 File Offset: 0x0001AA94
		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required != Required.Default || (jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate).GetValueOrDefault() == DefaultValueHandling.Populate)
							{
								this._hasRequiredOrDefaultValueProperties = new bool?(true);
								break;
							}
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001C96C File Offset: 0x0001AB6C
		[NullableContext(1)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public JsonObjectContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x040002D4 RID: 724
		internal bool ExtensionDataIsJToken;

		// Token: 0x040002D5 RID: 725
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x040002D6 RID: 726
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x040002D7 RID: 727
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x040002D8 RID: 728
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x040002D9 RID: 729
		private Type _extensionDataValueType;
	}
}

using System;
using System.Reflection;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000099 RID: 153
	public class JsonObjectContract : JsonContainerContract
	{
		/// <summary>
		/// Gets or sets the object member serialization.
		/// </summary>
		/// <value>The member object serialization.</value>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001BD86 File Offset: 0x00019F86
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0001BD8E File Offset: 0x00019F8E
		public MemberSerialization MemberSerialization { get; set; }

		/// <summary>
		/// Gets or sets a value that indicates whether the object's properties are required.
		/// </summary>
		/// <value>
		/// 	A value indicating whether the object's properties are required.
		/// </value>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001BD97 File Offset: 0x00019F97
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0001BD9F File Offset: 0x00019F9F
		public Required? ItemRequired { get; set; }

		/// <summary>
		/// Gets the object's properties.
		/// </summary>
		/// <value>The object's properties.</value>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		public JsonPropertyCollection Properties { get; private set; }

		/// <summary>
		/// Gets the constructor parameters required for any non-default constructor
		/// </summary>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001BDB9 File Offset: 0x00019FB9
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001BDC1 File Offset: 0x00019FC1
		public JsonPropertyCollection ConstructorParameters { get; private set; }

		/// <summary>
		/// Gets or sets the override constructor used to create the object.
		/// This is set when a constructor is marked up using the
		/// JsonConstructor attribute.
		/// </summary>
		/// <value>The override constructor.</value>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001BDCA File Offset: 0x00019FCA
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001BDD2 File Offset: 0x00019FD2
		public ConstructorInfo OverrideConstructor { get; set; }

		/// <summary>
		/// Gets or sets the parametrized constructor used to create the object.
		/// </summary>
		/// <value>The parametrized constructor.</value>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001BDDB File Offset: 0x00019FDB
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001BDE3 File Offset: 0x00019FE3
		public ConstructorInfo ParametrizedConstructor { get; set; }

		/// <summary>
		/// Gets or sets the extension data setter.
		/// </summary>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001BDEC File Offset: 0x00019FEC
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001BE00 File Offset: 0x0001A000
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
							if (jsonProperty.Required != Required.Default || ((jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate) == DefaultValueHandling.Populate && jsonProperty.Writable))
							{
								this._hasRequiredOrDefaultValueProperties = new bool?(true);
								break;
							}
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.Value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x0600079D RID: 1949 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		public JsonObjectContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
			this.ConstructorParameters = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x040002C7 RID: 711
		private bool? _hasRequiredOrDefaultValueProperties;
	}
}

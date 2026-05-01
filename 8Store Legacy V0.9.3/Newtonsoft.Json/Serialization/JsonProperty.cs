using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Maps a JSON property to a .NET member or constructor parameter.
	/// </summary>
	// Token: 0x0200009B RID: 155
	public class JsonProperty
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001BF56 File Offset: 0x0001A156
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0001BF5E File Offset: 0x0001A15E
		internal JsonContract PropertyContract { get; set; }

		/// <summary>
		/// Gets or sets the name of the property.
		/// </summary>
		/// <value>The name of the property.</value>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001BF67 File Offset: 0x0001A167
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0001BF6F File Offset: 0x0001A16F
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this.CalculateSkipPropertyNameEscape();
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001BF80 File Offset: 0x0001A180
		private void CalculateSkipPropertyNameEscape()
		{
			if (this._propertyName == null)
			{
				this._skipPropertyNameEscape = false;
				return;
			}
			this._skipPropertyNameEscape = true;
			string propertyName = this._propertyName;
			for (int i = 0; i < propertyName.Length; i++)
			{
				char c = propertyName.get_Chars(i);
				if (!char.IsLetterOrDigit(c) && c != '_' && c != '@')
				{
					this._skipPropertyNameEscape = false;
					return;
				}
			}
		}

		/// <summary>
		/// Gets or sets the type that declared this property.
		/// </summary>
		/// <value>The type that declared this property.</value>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001BFDE File Offset: 0x0001A1DE
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		public Type DeclaringType { get; set; }

		/// <summary>
		/// Gets or sets the order of serialization and deserialization of a member.
		/// </summary>
		/// <value>The numeric order of serialization or deserialization.</value>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001BFEF File Offset: 0x0001A1EF
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0001BFF7 File Offset: 0x0001A1F7
		public int? Order { get; set; }

		/// <summary>
		/// Gets or sets the name of the underlying member or parameter.
		/// </summary>
		/// <value>The name of the underlying member or parameter.</value>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001C000 File Offset: 0x0001A200
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0001C008 File Offset: 0x0001A208
		public string UnderlyingName { get; set; }

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Serialization.IValueProvider" /> that will get and set the <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> during serialization.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Serialization.IValueProvider" /> that will get and set the <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> during serialization.</value>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001C011 File Offset: 0x0001A211
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0001C019 File Offset: 0x0001A219
		public IValueProvider ValueProvider { get; set; }

		/// <summary>
		/// Gets or sets the type of the property.
		/// </summary>
		/// <value>The type of the property.</value>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001C022 File Offset: 0x0001A222
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0001C02A File Offset: 0x0001A22A
		public Type PropertyType { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.JsonConverter" /> for the property.
		/// If set this converter takes presidence over the contract converter for the property type.
		/// </summary>
		/// <value>The converter.</value>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001C033 File Offset: 0x0001A233
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0001C03B File Offset: 0x0001A23B
		public JsonConverter Converter { get; set; }

		/// <summary>
		/// Gets the member converter.
		/// </summary>
		/// <value>The member converter.</value>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001C044 File Offset: 0x0001A244
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001C04C File Offset: 0x0001A24C
		public JsonConverter MemberConverter { get; set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> is ignored.
		/// </summary>
		/// <value><c>true</c> if ignored; otherwise, <c>false</c>.</value>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001C055 File Offset: 0x0001A255
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001C05D File Offset: 0x0001A25D
		public bool Ignored { get; set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> is readable.
		/// </summary>
		/// <value><c>true</c> if readable; otherwise, <c>false</c>.</value>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001C066 File Offset: 0x0001A266
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001C06E File Offset: 0x0001A26E
		public bool Readable { get; set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> is writable.
		/// </summary>
		/// <value><c>true</c> if writable; otherwise, <c>false</c>.</value>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001C077 File Offset: 0x0001A277
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0001C07F File Offset: 0x0001A27F
		public bool Writable { get; set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> has a member attribute.
		/// </summary>
		/// <value><c>true</c> if has a member attribute; otherwise, <c>false</c>.</value>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001C088 File Offset: 0x0001A288
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0001C090 File Offset: 0x0001A290
		public bool HasMemberAttribute { get; set; }

		/// <summary>
		/// Gets the default value.
		/// </summary>
		/// <value>The default value.</value>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001C099 File Offset: 0x0001A299
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0001C0A1 File Offset: 0x0001A2A1
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
		internal object GetResolvedDefaultValue()
		{
			if (!this._hasExplicitDefaultValue && this.PropertyType != null)
			{
				return ReflectionUtils.GetDefaultValue(this.PropertyType);
			}
			return this._defaultValue;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> is required.
		/// </summary>
		/// <value>A value indicating whether this <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> is required.</value>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0001C0FE File Offset: 0x0001A2FE
		public Required Required
		{
			get
			{
				Required? required = this._required;
				if (required == null)
				{
					return Required.Default;
				}
				return required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this property preserves object references.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is reference; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001C10C File Offset: 0x0001A30C
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0001C114 File Offset: 0x0001A314
		public bool? IsReference { get; set; }

		/// <summary>
		/// Gets the property null value handling.
		/// </summary>
		/// <value>The null value handling.</value>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001C11D File Offset: 0x0001A31D
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0001C125 File Offset: 0x0001A325
		public NullValueHandling? NullValueHandling { get; set; }

		/// <summary>
		/// Gets the property default value handling.
		/// </summary>
		/// <value>The default value handling.</value>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001C12E File Offset: 0x0001A32E
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0001C136 File Offset: 0x0001A336
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		/// <summary>
		/// Gets the property reference loop handling.
		/// </summary>
		/// <value>The reference loop handling.</value>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001C13F File Offset: 0x0001A33F
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0001C147 File Offset: 0x0001A347
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		/// <summary>
		/// Gets the property object creation handling.
		/// </summary>
		/// <value>The object creation handling.</value>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0001C150 File Offset: 0x0001A350
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0001C158 File Offset: 0x0001A358
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		/// <summary>
		/// Gets or sets the type name handling.
		/// </summary>
		/// <value>The type name handling.</value>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x0001C161 File Offset: 0x0001A361
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x0001C169 File Offset: 0x0001A369
		public TypeNameHandling? TypeNameHandling { get; set; }

		/// <summary>
		/// Gets or sets a predicate used to determine whether the property should be serialize.
		/// </summary>
		/// <value>A predicate used to determine whether the property should be serialize.</value>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0001C172 File Offset: 0x0001A372
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x0001C17A File Offset: 0x0001A37A
		public Predicate<object> ShouldSerialize { get; set; }

		/// <summary>
		/// Gets or sets a predicate used to determine whether the property should be serialized.
		/// </summary>
		/// <value>A predicate used to determine whether the property should be serialized.</value>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0001C183 File Offset: 0x0001A383
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0001C18B File Offset: 0x0001A38B
		public Predicate<object> GetIsSpecified { get; set; }

		/// <summary>
		/// Gets or sets an action used to set whether the property has been deserialized.
		/// </summary>
		/// <value>An action used to set whether the property has been deserialized.</value>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001C194 File Offset: 0x0001A394
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0001C19C File Offset: 0x0001A39C
		public Action<object, object> SetIsSpecified { get; set; }

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		// Token: 0x060007D3 RID: 2003 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		public override string ToString()
		{
			return this.PropertyName;
		}

		/// <summary>
		/// Gets or sets the converter used when serializing the property's collection items.
		/// </summary>
		/// <value>The collection's items converter.</value>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001C1AD File Offset: 0x0001A3AD
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001C1B5 File Offset: 0x0001A3B5
		public JsonConverter ItemConverter { get; set; }

		/// <summary>
		/// Gets or sets whether this property's collection items are serialized as a reference.
		/// </summary>
		/// <value>Whether this property's collection items are serialized as a reference.</value>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001C1BE File Offset: 0x0001A3BE
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001C1C6 File Offset: 0x0001A3C6
		public bool? ItemIsReference { get; set; }

		/// <summary>
		/// Gets or sets the the type name handling used when serializing the property's collection items.
		/// </summary>
		/// <value>The collection's items type name handling.</value>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001C1CF File Offset: 0x0001A3CF
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001C1D7 File Offset: 0x0001A3D7
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		/// <summary>
		/// Gets or sets the the reference loop handling used when serializing the property's collection items.
		/// </summary>
		/// <value>The collection's items reference loop handling.</value>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001C1E0 File Offset: 0x0001A3E0
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x060007DC RID: 2012 RVA: 0x0001C1F1 File Offset: 0x0001A3F1
		internal void WritePropertyName(JsonWriter writer)
		{
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(this.PropertyName, false);
				return;
			}
			writer.WritePropertyName(this.PropertyName);
		}

		// Token: 0x040002D0 RID: 720
		internal Required? _required;

		// Token: 0x040002D1 RID: 721
		internal bool _hasExplicitDefaultValue;

		// Token: 0x040002D2 RID: 722
		internal object _defaultValue;

		// Token: 0x040002D3 RID: 723
		private string _propertyName;

		// Token: 0x040002D4 RID: 724
		private bool _skipPropertyNameEscape;
	}
}

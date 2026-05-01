using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies the settings on a <see cref="T:Newtonsoft.Json.JsonSerializer" /> object.
	/// </summary>
	// Token: 0x0200004B RID: 75
	public class JsonSerializerSettings
	{
		/// <summary>
		/// Gets or sets how reference loops (e.g. a class referencing itself) is handled.
		/// </summary>
		/// <value>Reference loop handling.</value>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000AE6C File Offset: 0x0000906C
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000AE92 File Offset: 0x00009092
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? referenceLoopHandling = this._referenceLoopHandling;
				if (referenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets how missing members (e.g. JSON contains a property that isn't a member on the object) are handled during deserialization.
		/// </summary>
		/// <value>Missing member handling.</value>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000AEA0 File Offset: 0x000090A0
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000AEC6 File Offset: 0x000090C6
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				MissingMemberHandling? missingMemberHandling = this._missingMemberHandling;
				if (missingMemberHandling == null)
				{
					return MissingMemberHandling.Ignore;
				}
				return missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets how objects are created during deserialization.
		/// </summary>
		/// <value>The object creation handling.</value>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000AED4 File Offset: 0x000090D4
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000AEFA File Offset: 0x000090FA
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				ObjectCreationHandling? objectCreationHandling = this._objectCreationHandling;
				if (objectCreationHandling == null)
				{
					return ObjectCreationHandling.Auto;
				}
				return objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets how null values are handled during serialization and deserialization.
		/// </summary>
		/// <value>Null value handling.</value>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000AF08 File Offset: 0x00009108
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000AF2E File Offset: 0x0000912E
		public NullValueHandling NullValueHandling
		{
			get
			{
				NullValueHandling? nullValueHandling = this._nullValueHandling;
				if (nullValueHandling == null)
				{
					return NullValueHandling.Include;
				}
				return nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets how null default are handled during serialization and deserialization.
		/// </summary>
		/// <value>The default value handling.</value>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000AF3C File Offset: 0x0000913C
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000AF62 File Offset: 0x00009162
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				DefaultValueHandling? defaultValueHandling = this._defaultValueHandling;
				if (defaultValueHandling == null)
				{
					return DefaultValueHandling.Include;
				}
				return defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets a collection <see cref="T:Newtonsoft.Json.JsonConverter" /> that will be used during serialization.
		/// </summary>
		/// <value>The converters.</value>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000AF70 File Offset: 0x00009170
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000AF78 File Offset: 0x00009178
		public IList<JsonConverter> Converters { get; set; }

		/// <summary>
		/// Gets or sets how object references are preserved by the serializer.
		/// </summary>
		/// <value>The preserve references handling.</value>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000AF84 File Offset: 0x00009184
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000AFAA File Offset: 0x000091AA
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				PreserveReferencesHandling? preserveReferencesHandling = this._preserveReferencesHandling;
				if (preserveReferencesHandling == null)
				{
					return PreserveReferencesHandling.None;
				}
				return preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets how type name writing and reading is handled by the serializer.
		/// </summary>
		/// <value>The type name handling.</value>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000AFB8 File Offset: 0x000091B8
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000AFDE File Offset: 0x000091DE
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				TypeNameHandling? typeNameHandling = this._typeNameHandling;
				if (typeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets how a type name assembly is written and resolved by the serializer.
		/// </summary>
		/// <value>The type name assembly format.</value>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000AFEC File Offset: 0x000091EC
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000B012 File Offset: 0x00009212
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				FormatterAssemblyStyle? typeNameAssemblyFormat = this._typeNameAssemblyFormat;
				if (typeNameAssemblyFormat == null)
				{
					return FormatterAssemblyStyle.Simple;
				}
				return typeNameAssemblyFormat.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormat = new FormatterAssemblyStyle?(value);
			}
		}

		/// <summary>
		/// Gets or sets how constructors are used during deserialization.
		/// </summary>
		/// <value>The constructor handling.</value>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000B020 File Offset: 0x00009220
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000B046 File Offset: 0x00009246
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				ConstructorHandling? constructorHandling = this._constructorHandling;
				if (constructorHandling == null)
				{
					return ConstructorHandling.Default;
				}
				return constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets the contract resolver used by the serializer when
		/// serializing .NET objects to JSON and vice versa.
		/// </summary>
		/// <value>The contract resolver.</value>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000B054 File Offset: 0x00009254
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000B05C File Offset: 0x0000925C
		public IContractResolver ContractResolver { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Serialization.IReferenceResolver" /> used by the serializer when resolving references.
		/// </summary>
		/// <value>The reference resolver.</value>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000B065 File Offset: 0x00009265
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000B06D File Offset: 0x0000926D
		public IReferenceResolver ReferenceResolver { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Serialization.ITraceWriter" /> used by the serializer when writing trace messages.
		/// </summary>
		/// <value>The trace writer.</value>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000B076 File Offset: 0x00009276
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000B07E File Offset: 0x0000927E
		public ITraceWriter TraceWriter { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.SerializationBinder" /> used by the serializer when resolving type names.
		/// </summary>
		/// <value>The binder.</value>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B087 File Offset: 0x00009287
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000B08F File Offset: 0x0000928F
		public SerializationBinder Binder { get; set; }

		/// <summary>
		/// Gets or sets the error handler called during serialization and deserialization.
		/// </summary>
		/// <value>The error handler called during serialization and deserialization.</value>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000B098 File Offset: 0x00009298
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000B0A0 File Offset: 0x000092A0
		public EventHandler<ErrorEventArgs> Error { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used by the serializer when invoking serialization callback methods.
		/// </summary>
		/// <value>The context.</value>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000B0AC File Offset: 0x000092AC
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000B0D6 File Offset: 0x000092D6
		public StreamingContext Context
		{
			get
			{
				StreamingContext? context = this._context;
				if (context == null)
				{
					return JsonSerializerSettings.DefaultContext;
				}
				return context.GetValueOrDefault();
			}
			set
			{
				this._context = new StreamingContext?(value);
			}
		}

		/// <summary>
		/// Get or set how <see cref="T:System.DateTime" /> and <see cref="T:System.DateTimeOffset" /> values are formatting when writing JSON text.
		/// </summary>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B0E4 File Offset: 0x000092E4
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000B0F5 File Offset: 0x000092F5
		public string DateFormatString
		{
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		/// <summary>
		/// Gets or sets the maximum depth allowed when reading JSON. Reading past this depth will throw a <see cref="T:Newtonsoft.Json.JsonReaderException" />.
		/// </summary>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B105 File Offset: 0x00009305
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000B110 File Offset: 0x00009310
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		/// <summary>
		/// Indicates how JSON text output is formatted.
		/// </summary>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000B154 File Offset: 0x00009354
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000B17A File Offset: 0x0000937A
		public Formatting Formatting
		{
			get
			{
				Formatting? formatting = this._formatting;
				if (formatting == null)
				{
					return Formatting.None;
				}
				return formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		/// <summary>
		/// Get or set how dates are written to JSON text.
		/// </summary>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000B188 File Offset: 0x00009388
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000B1AE File Offset: 0x000093AE
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				DateFormatHandling? dateFormatHandling = this._dateFormatHandling;
				if (dateFormatHandling == null)
				{
					return DateFormatHandling.IsoDateFormat;
				}
				return dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		/// <summary>
		/// Get or set how <see cref="T:System.DateTime" /> time zones are handling during serialization and deserialization.
		/// </summary>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000B1BC File Offset: 0x000093BC
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000B1E2 File Offset: 0x000093E2
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				DateTimeZoneHandling? dateTimeZoneHandling = this._dateTimeZoneHandling;
				if (dateTimeZoneHandling == null)
				{
					return DateTimeZoneHandling.RoundtripKind;
				}
				return dateTimeZoneHandling.GetValueOrDefault();
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		/// <summary>
		/// Get or set how date formatted strings, e.g. "\/Date(1198908717056)\/" and "2012-03-21T05:40Z", are parsed when reading JSON.
		/// </summary>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000B1F0 File Offset: 0x000093F0
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000B216 File Offset: 0x00009416
		public DateParseHandling DateParseHandling
		{
			get
			{
				DateParseHandling? dateParseHandling = this._dateParseHandling;
				if (dateParseHandling == null)
				{
					return DateParseHandling.DateTime;
				}
				return dateParseHandling.GetValueOrDefault();
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		/// <summary>
		/// Get or set how special floating point numbers, e.g. <see cref="F:System.Double.NaN" />,
		/// <see cref="F:System.Double.PositiveInfinity" /> and <see cref="F:System.Double.NegativeInfinity" />,
		/// are written as JSON.
		/// </summary>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000B224 File Offset: 0x00009424
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000B24A File Offset: 0x0000944A
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				FloatFormatHandling? floatFormatHandling = this._floatFormatHandling;
				if (floatFormatHandling == null)
				{
					return FloatFormatHandling.String;
				}
				return floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		/// <summary>
		/// Get or set how floating point numbers, e.g. 1.0 and 9.9, are parsed when reading JSON text.
		/// </summary>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000B258 File Offset: 0x00009458
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000B27E File Offset: 0x0000947E
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				FloatParseHandling? floatParseHandling = this._floatParseHandling;
				if (floatParseHandling == null)
				{
					return FloatParseHandling.Double;
				}
				return floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		/// <summary>
		/// Get or set how strings are escaped when writing JSON text.
		/// </summary>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B28C File Offset: 0x0000948C
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000B2B2 File Offset: 0x000094B2
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				StringEscapeHandling? stringEscapeHandling = this._stringEscapeHandling;
				if (stringEscapeHandling == null)
				{
					return StringEscapeHandling.Default;
				}
				return stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets the culture used when reading JSON. Defaults to <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.
		/// </summary>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B2C0 File Offset: 0x000094C0
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000B2D1 File Offset: 0x000094D1
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether there will be a check for additional content after deserializing an object.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if there will be a check for additional content after deserializing an object; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000B2DC File Offset: 0x000094DC
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000B302 File Offset: 0x00009502
		public bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent ?? false;
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> class.
		/// </summary>
		// Token: 0x06000328 RID: 808 RVA: 0x0000B327 File Offset: 0x00009527
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x0400010A RID: 266
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x0400010B RID: 267
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x0400010C RID: 268
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x0400010D RID: 269
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x0400010E RID: 270
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x0400010F RID: 271
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x04000110 RID: 272
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x04000111 RID: 273
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x04000112 RID: 274
		internal const FormatterAssemblyStyle DefaultTypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;

		// Token: 0x04000113 RID: 275
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x04000114 RID: 276
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x04000115 RID: 277
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x04000116 RID: 278
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x04000117 RID: 279
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x04000118 RID: 280
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x04000119 RID: 281
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x0400011A RID: 282
		internal const FormatterAssemblyStyle DefaultFormatterAssemblyStyle = FormatterAssemblyStyle.Simple;

		// Token: 0x0400011B RID: 283
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x0400011C RID: 284
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x0400011D RID: 285
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x0400011E RID: 286
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x0400011F RID: 287
		internal Formatting? _formatting;

		// Token: 0x04000120 RID: 288
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x04000121 RID: 289
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x04000122 RID: 290
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x04000123 RID: 291
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x04000124 RID: 292
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x04000125 RID: 293
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x04000126 RID: 294
		internal CultureInfo _culture;

		// Token: 0x04000127 RID: 295
		internal bool? _checkAdditionalContent;

		// Token: 0x04000128 RID: 296
		internal int? _maxDepth;

		// Token: 0x04000129 RID: 297
		internal bool _maxDepthSet;

		// Token: 0x0400012A RID: 298
		internal string _dateFormatString;

		// Token: 0x0400012B RID: 299
		internal bool _dateFormatStringSet;

		// Token: 0x0400012C RID: 300
		internal FormatterAssemblyStyle? _typeNameAssemblyFormat;

		// Token: 0x0400012D RID: 301
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x0400012E RID: 302
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x0400012F RID: 303
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x04000130 RID: 304
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000131 RID: 305
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x04000132 RID: 306
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04000133 RID: 307
		internal StreamingContext? _context;

		// Token: 0x04000134 RID: 308
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x04000135 RID: 309
		internal TypeNameHandling? _typeNameHandling;
	}
}

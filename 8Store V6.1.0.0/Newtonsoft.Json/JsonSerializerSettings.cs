using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000034 RID: 52
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonSerializerSettings
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005C75 File Offset: 0x00003E75
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00005C82 File Offset: 0x00003E82
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005C90 File Offset: 0x00003E90
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00005C9D File Offset: 0x00003E9D
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005CAB File Offset: 0x00003EAB
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00005CC6 File Offset: 0x00003EC6
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00005CD3 File Offset: 0x00003ED3
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005CE1 File Offset: 0x00003EE1
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00005CEE File Offset: 0x00003EEE
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005CFC File Offset: 0x00003EFC
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00005D04 File Offset: 0x00003F04
		[Nullable(1)]
		public IList<JsonConverter> Converters { [NullableContext(1)] get; [NullableContext(1)] set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005D0D File Offset: 0x00003F0D
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00005D1A File Offset: 0x00003F1A
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00005D28 File Offset: 0x00003F28
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00005D35 File Offset: 0x00003F35
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005D43 File Offset: 0x00003F43
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005D50 File Offset: 0x00003F50
		public MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				this._metadataPropertyHandling = new MetadataPropertyHandling?(value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005D5E File Offset: 0x00003F5E
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00005D66 File Offset: 0x00003F66
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this.TypeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005D6F File Offset: 0x00003F6F
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00005D7C File Offset: 0x00003F7C
		public TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormatHandling = new TypeNameAssemblyFormatHandling?(value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00005D8A File Offset: 0x00003F8A
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00005D97 File Offset: 0x00003F97
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005DA5 File Offset: 0x00003FA5
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00005DAD File Offset: 0x00003FAD
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005DB6 File Offset: 0x00003FB6
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00005DBE File Offset: 0x00003FBE
		public IEqualityComparer EqualityComparer { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005DC7 File Offset: 0x00003FC7
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005DDC File Offset: 0x00003FDC
		[Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public IReferenceResolver ReferenceResolver
		{
			get
			{
				Func<IReferenceResolver> referenceResolverProvider = this.ReferenceResolverProvider;
				if (referenceResolverProvider == null)
				{
					return null;
				}
				return referenceResolverProvider.Invoke();
			}
			set
			{
				this.ReferenceResolverProvider = ((value != null) ? (() => value) : null);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005E13 File Offset: 0x00004013
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005E1B File Offset: 0x0000401B
		public Func<IReferenceResolver> ReferenceResolverProvider { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005E24 File Offset: 0x00004024
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00005E2C File Offset: 0x0000402C
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00005E38 File Offset: 0x00004038
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00005E6F File Offset: 0x0000406F
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public SerializationBinder Binder
		{
			get
			{
				if (this.SerializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this.SerializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				this.SerializationBinder = ((value == null) ? null : new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005E83 File Offset: 0x00004083
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00005E8B File Offset: 0x0000408B
		public ISerializationBinder SerializationBinder { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005E94 File Offset: 0x00004094
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00005E9C File Offset: 0x0000409C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public EventHandler<ErrorEventArgs> Error { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005EA8 File Offset: 0x000040A8
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00005ED2 File Offset: 0x000040D2
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00005EE0 File Offset: 0x000040E0
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00005EF1 File Offset: 0x000040F1
		[Nullable(1)]
		public string DateFormatString
		{
			[NullableContext(1)]
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			[NullableContext(1)]
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00005F01 File Offset: 0x00004101
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00005F1C File Offset: 0x0000411C
		public int? MaxDepth
		{
			get
			{
				if (!this._maxDepthSet)
				{
					return new int?(64);
				}
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if (num.GetValueOrDefault() <= num2 & num != null)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00005F62 File Offset: 0x00004162
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00005F6F File Offset: 0x0000416F
		public Formatting Formatting
		{
			get
			{
				return this._formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00005F7D File Offset: 0x0000417D
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00005F8A File Offset: 0x0000418A
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00005F98 File Offset: 0x00004198
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00005FA6 File Offset: 0x000041A6
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling.GetValueOrDefault(DateTimeZoneHandling.RoundtripKind);
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00005FB4 File Offset: 0x000041B4
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00005FC2 File Offset: 0x000041C2
		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling.GetValueOrDefault(DateParseHandling.DateTime);
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005FD0 File Offset: 0x000041D0
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00005FDD File Offset: 0x000041DD
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005FEB File Offset: 0x000041EB
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00005FF8 File Offset: 0x000041F8
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006006 File Offset: 0x00004206
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00006013 File Offset: 0x00004213
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00006021 File Offset: 0x00004221
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00006032 File Offset: 0x00004232
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000603B File Offset: 0x0000423B
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00006048 File Offset: 0x00004248
		public bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent.GetValueOrDefault();
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000606D File Offset: 0x0000426D
		[DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006080 File Offset: 0x00004280
		[NullableContext(1)]
		public JsonSerializerSettings(JsonSerializerSettings original)
		{
			this._floatParseHandling = original._floatParseHandling;
			this._floatFormatHandling = original._floatFormatHandling;
			this._dateParseHandling = original._dateParseHandling;
			this._dateTimeZoneHandling = original._dateTimeZoneHandling;
			this._dateFormatHandling = original._dateFormatHandling;
			this._formatting = original._formatting;
			this._maxDepth = original._maxDepth;
			this._maxDepthSet = original._maxDepthSet;
			this._dateFormatString = original._dateFormatString;
			this._dateFormatStringSet = original._dateFormatStringSet;
			this._context = original._context;
			this.Error = original.Error;
			this.SerializationBinder = original.SerializationBinder;
			this.TraceWriter = original.TraceWriter;
			this._culture = original._culture;
			this.ReferenceResolverProvider = original.ReferenceResolverProvider;
			this.EqualityComparer = original.EqualityComparer;
			this.ContractResolver = original.ContractResolver;
			this._constructorHandling = original._constructorHandling;
			this._typeNameAssemblyFormatHandling = original._typeNameAssemblyFormatHandling;
			this._metadataPropertyHandling = original._metadataPropertyHandling;
			this._typeNameHandling = original._typeNameHandling;
			this._preserveReferencesHandling = original._preserveReferencesHandling;
			this.Converters = Enumerable.ToList<JsonConverter>(original.Converters);
			this._defaultValueHandling = original._defaultValueHandling;
			this._nullValueHandling = original._nullValueHandling;
			this._objectCreationHandling = original._objectCreationHandling;
			this._missingMemberHandling = original._missingMemberHandling;
			this._referenceLoopHandling = original._referenceLoopHandling;
			this._checkAdditionalContent = original._checkAdditionalContent;
			this._stringEscapeHandling = original._stringEscapeHandling;
		}

		// Token: 0x040000B3 RID: 179
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x040000B4 RID: 180
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x040000B5 RID: 181
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x040000B6 RID: 182
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x040000B7 RID: 183
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x040000B8 RID: 184
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x040000B9 RID: 185
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x040000BA RID: 186
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x040000BB RID: 187
		internal const MetadataPropertyHandling DefaultMetadataPropertyHandling = MetadataPropertyHandling.Default;

		// Token: 0x040000BC RID: 188
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x040000BD RID: 189
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x040000BE RID: 190
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x040000BF RID: 191
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x040000C0 RID: 192
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x040000C1 RID: 193
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x040000C2 RID: 194
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x040000C3 RID: 195
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x040000C4 RID: 196
		internal const TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		// Token: 0x040000C5 RID: 197
		[Nullable(1)]
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x040000C6 RID: 198
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x040000C7 RID: 199
		[Nullable(1)]
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x040000C8 RID: 200
		internal const int DefaultMaxDepth = 64;

		// Token: 0x040000C9 RID: 201
		internal Formatting? _formatting;

		// Token: 0x040000CA RID: 202
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x040000CB RID: 203
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x040000CC RID: 204
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x040000CD RID: 205
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x040000CE RID: 206
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x040000CF RID: 207
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x040000D0 RID: 208
		internal CultureInfo _culture;

		// Token: 0x040000D1 RID: 209
		internal bool? _checkAdditionalContent;

		// Token: 0x040000D2 RID: 210
		internal int? _maxDepth;

		// Token: 0x040000D3 RID: 211
		internal bool _maxDepthSet;

		// Token: 0x040000D4 RID: 212
		internal string _dateFormatString;

		// Token: 0x040000D5 RID: 213
		internal bool _dateFormatStringSet;

		// Token: 0x040000D6 RID: 214
		internal TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		// Token: 0x040000D7 RID: 215
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040000D8 RID: 216
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x040000D9 RID: 217
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040000DA RID: 218
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040000DB RID: 219
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x040000DC RID: 220
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040000DD RID: 221
		internal StreamingContext? _context;

		// Token: 0x040000DE RID: 222
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x040000DF RID: 223
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040000E0 RID: 224
		internal MetadataPropertyHandling? _metadataPropertyHandling;
	}
}

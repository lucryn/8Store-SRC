using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Serializes and deserializes objects into and from the JSON format.
	/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> enables you to control how objects are encoded into JSON.
	/// </summary>
	// Token: 0x0200004A RID: 74
	public class JsonSerializer
	{
		/// <summary>
		/// Occurs when the <see cref="T:Newtonsoft.Json.JsonSerializer" /> errors during serialization and deserialization.
		/// </summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002A4 RID: 676 RVA: 0x00009F40 File Offset: 0x00008140
		// (remove) Token: 0x060002A5 RID: 677 RVA: 0x00009F78 File Offset: 0x00008178
		public virtual event EventHandler<ErrorEventArgs> Error;

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Serialization.IReferenceResolver" /> used by the serializer when resolving references.
		/// </summary>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00009FAD File Offset: 0x000081AD
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00009FB5 File Offset: 0x000081B5
		public virtual IReferenceResolver ReferenceResolver
		{
			get
			{
				return this.GetReferenceResolver();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Reference resolver cannot be null.");
				}
				this._referenceResolver = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.SerializationBinder" /> used by the serializer when resolving type names.
		/// </summary>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00009FD1 File Offset: 0x000081D1
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00009FD9 File Offset: 0x000081D9
		public virtual SerializationBinder Binder
		{
			get
			{
				return this._binder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._binder = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Serialization.ITraceWriter" /> used by the serializer when writing trace messages.
		/// </summary>
		/// <value>The trace writer.</value>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00009FF5 File Offset: 0x000081F5
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00009FFD File Offset: 0x000081FD
		public virtual ITraceWriter TraceWriter
		{
			get
			{
				return this._traceWriter;
			}
			set
			{
				this._traceWriter = value;
			}
		}

		/// <summary>
		/// Gets or sets how type name writing and reading is handled by the serializer.
		/// </summary>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A006 File Offset: 0x00008206
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000A00E File Offset: 0x0000820E
		public virtual TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling;
			}
			set
			{
				if (value < TypeNameHandling.None || value > TypeNameHandling.Auto)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameHandling = value;
			}
		}

		/// <summary>
		/// Gets or sets how a type name assembly is written and resolved by the serializer.
		/// </summary>
		/// <value>The type name assembly format.</value>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000A02A File Offset: 0x0000822A
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000A032 File Offset: 0x00008232
		public virtual FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._typeNameAssemblyFormat;
			}
			set
			{
				if (value < FormatterAssemblyStyle.Simple || value > FormatterAssemblyStyle.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormat = value;
			}
		}

		/// <summary>
		/// Gets or sets how object references are preserved by the serializer.
		/// </summary>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000A04E File Offset: 0x0000824E
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000A056 File Offset: 0x00008256
		public virtual PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling;
			}
			set
			{
				if (value < PreserveReferencesHandling.None || value > PreserveReferencesHandling.All)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preserveReferencesHandling = value;
			}
		}

		/// <summary>
		/// Get or set how reference loops (e.g. a class referencing itself) is handled.
		/// </summary>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A072 File Offset: 0x00008272
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000A07A File Offset: 0x0000827A
		public virtual ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling;
			}
			set
			{
				if (value < ReferenceLoopHandling.Error || value > ReferenceLoopHandling.Serialize)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._referenceLoopHandling = value;
			}
		}

		/// <summary>
		/// Get or set how missing members (e.g. JSON contains a property that isn't a member on the object) are handled during deserialization.
		/// </summary>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000A096 File Offset: 0x00008296
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000A09E File Offset: 0x0000829E
		public virtual MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling;
			}
			set
			{
				if (value < MissingMemberHandling.Ignore || value > MissingMemberHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._missingMemberHandling = value;
			}
		}

		/// <summary>
		/// Get or set how null values are handled during serialization and deserialization.
		/// </summary>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A0BA File Offset: 0x000082BA
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000A0C2 File Offset: 0x000082C2
		public virtual NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling;
			}
			set
			{
				if (value < NullValueHandling.Include || value > NullValueHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._nullValueHandling = value;
			}
		}

		/// <summary>
		/// Get or set how null default are handled during serialization and deserialization.
		/// </summary>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A0DE File Offset: 0x000082DE
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000A0E6 File Offset: 0x000082E6
		public virtual DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling;
			}
			set
			{
				if (value < DefaultValueHandling.Include || value > DefaultValueHandling.IgnoreAndPopulate)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._defaultValueHandling = value;
			}
		}

		/// <summary>
		/// Gets or sets how objects are created during deserialization.
		/// </summary>
		/// <value>The object creation handling.</value>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A102 File Offset: 0x00008302
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000A10A File Offset: 0x0000830A
		public virtual ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling;
			}
			set
			{
				if (value < ObjectCreationHandling.Auto || value > ObjectCreationHandling.Replace)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._objectCreationHandling = value;
			}
		}

		/// <summary>
		/// Gets or sets how constructors are used during deserialization.
		/// </summary>
		/// <value>The constructor handling.</value>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A126 File Offset: 0x00008326
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000A12E File Offset: 0x0000832E
		public virtual ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling;
			}
			set
			{
				if (value < ConstructorHandling.Default || value > ConstructorHandling.AllowNonPublicDefaultConstructor)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._constructorHandling = value;
			}
		}

		/// <summary>
		/// Gets a collection <see cref="T:Newtonsoft.Json.JsonConverter" /> that will be used during serialization.
		/// </summary>
		/// <value>Collection <see cref="T:Newtonsoft.Json.JsonConverter" /> that will be used during serialization.</value>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A14A File Offset: 0x0000834A
		public virtual JsonConverterCollection Converters
		{
			get
			{
				if (this._converters == null)
				{
					this._converters = new JsonConverterCollection();
				}
				return this._converters;
			}
		}

		/// <summary>
		/// Gets or sets the contract resolver used by the serializer when
		/// serializing .NET objects to JSON and vice versa.
		/// </summary>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000A165 File Offset: 0x00008365
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000A16D File Offset: 0x0000836D
		public virtual IContractResolver ContractResolver
		{
			get
			{
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = (value ?? DefaultContractResolver.Instance);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used by the serializer when invoking serialization callback methods.
		/// </summary>
		/// <value>The context.</value>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000A17F File Offset: 0x0000837F
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000A187 File Offset: 0x00008387
		public virtual StreamingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		/// <summary>
		/// Indicates how JSON text output is formatted.
		/// </summary>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A190 File Offset: 0x00008390
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000A1B6 File Offset: 0x000083B6
		public virtual Formatting Formatting
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
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A1C4 File Offset: 0x000083C4
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000A1EA File Offset: 0x000083EA
		public virtual DateFormatHandling DateFormatHandling
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
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A1F8 File Offset: 0x000083F8
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000A21E File Offset: 0x0000841E
		public virtual DateTimeZoneHandling DateTimeZoneHandling
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
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000A22C File Offset: 0x0000842C
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000A252 File Offset: 0x00008452
		public virtual DateParseHandling DateParseHandling
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
		/// Get or set how floating point numbers, e.g. 1.0 and 9.9, are parsed when reading JSON text.
		/// </summary>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000A260 File Offset: 0x00008460
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000A286 File Offset: 0x00008486
		public virtual FloatParseHandling FloatParseHandling
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
		/// Get or set how special floating point numbers, e.g. <see cref="F:System.Double.NaN" />,
		/// <see cref="F:System.Double.PositiveInfinity" /> and <see cref="F:System.Double.NegativeInfinity" />,
		/// are written as JSON text.
		/// </summary>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A294 File Offset: 0x00008494
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000A2BA File Offset: 0x000084BA
		public virtual FloatFormatHandling FloatFormatHandling
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
		/// Get or set how strings are escaped when writing JSON text.
		/// </summary>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A2C8 File Offset: 0x000084C8
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000A2EE File Offset: 0x000084EE
		public virtual StringEscapeHandling StringEscapeHandling
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
		/// Get or set how <see cref="T:System.DateTime" /> and <see cref="T:System.DateTimeOffset" /> values are formatting when writing JSON text.
		/// </summary>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A2FC File Offset: 0x000084FC
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000A30D File Offset: 0x0000850D
		public virtual string DateFormatString
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
		/// Gets or sets the culture used when reading JSON. Defaults to <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.
		/// </summary>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000A31D File Offset: 0x0000851D
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000A32E File Offset: 0x0000852E
		public virtual CultureInfo Culture
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
		/// Gets or sets the maximum depth allowed when reading JSON. Reading past this depth will throw a <see cref="T:Newtonsoft.Json.JsonReaderException" />.
		/// </summary>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A337 File Offset: 0x00008537
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000A340 File Offset: 0x00008540
		public virtual int? MaxDepth
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
		/// Gets a value indicating whether there will be a check for additional JSON content after deserializing an object.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if there will be a check for additional JSON content after deserializing an object; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000A384 File Offset: 0x00008584
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000A3AA File Offset: 0x000085AA
		public virtual bool CheckAdditionalContent
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

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A3B8 File Offset: 0x000085B8
		internal bool IsCheckAdditionalContentSet()
		{
			return this._checkAdditionalContent != null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonSerializer" /> class.
		/// </summary>
		// Token: 0x060002DA RID: 730 RVA: 0x0000A3C8 File Offset: 0x000085C8
		public JsonSerializer()
		{
			this._referenceLoopHandling = ReferenceLoopHandling.Error;
			this._missingMemberHandling = MissingMemberHandling.Ignore;
			this._nullValueHandling = NullValueHandling.Include;
			this._defaultValueHandling = DefaultValueHandling.Include;
			this._objectCreationHandling = ObjectCreationHandling.Auto;
			this._preserveReferencesHandling = PreserveReferencesHandling.None;
			this._constructorHandling = ConstructorHandling.Default;
			this._typeNameHandling = TypeNameHandling.None;
			this._context = JsonSerializerSettings.DefaultContext;
			this._binder = DefaultSerializationBinder.Instance;
			this._culture = JsonSerializerSettings.DefaultCulture;
			this._contractResolver = DefaultContractResolver.Instance;
		}

		/// <summary>
		/// Creates a new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will not use default settings.
		/// </summary>
		/// <returns>
		/// A new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will not use default settings.
		/// </returns>
		// Token: 0x060002DB RID: 731 RVA: 0x0000A43F File Offset: 0x0000863F
		public static JsonSerializer Create()
		{
			return new JsonSerializer();
		}

		/// <summary>
		/// Creates a new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance using the specified <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will not use default settings.
		/// </summary>
		/// <param name="settings">The settings to be applied to the <see cref="T:Newtonsoft.Json.JsonSerializer" />.</param>
		/// <returns>
		/// A new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance using the specified <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will not use default settings.
		/// </returns>
		// Token: 0x060002DC RID: 732 RVA: 0x0000A448 File Offset: 0x00008648
		public static JsonSerializer Create(JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		/// <summary>
		/// Creates a new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will use default settings.
		/// </summary>
		/// <returns>
		/// A new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will use default settings.
		/// </returns>
		// Token: 0x060002DD RID: 733 RVA: 0x0000A468 File Offset: 0x00008668
		public static JsonSerializer CreateDefault()
		{
			Func<JsonSerializerSettings> defaultSettings = JsonConvert.DefaultSettings;
			JsonSerializerSettings settings = (defaultSettings != null) ? defaultSettings.Invoke() : null;
			return JsonSerializer.Create(settings);
		}

		/// <summary>
		/// Creates a new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance using the specified <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will use default settings.
		/// </summary>
		/// <param name="settings">The settings to be applied to the <see cref="T:Newtonsoft.Json.JsonSerializer" />.</param>
		/// <returns>
		/// A new <see cref="T:Newtonsoft.Json.JsonSerializer" /> instance using the specified <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// The <see cref="T:Newtonsoft.Json.JsonSerializer" /> will use default settings.
		/// </returns>
		// Token: 0x060002DE RID: 734 RVA: 0x0000A490 File Offset: 0x00008690
		public static JsonSerializer CreateDefault(JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A4B0 File Offset: 0x000086B0
		private static void ApplySerializerSettings(JsonSerializer serializer, JsonSerializerSettings settings)
		{
			if (!CollectionUtils.IsNullOrEmpty<JsonConverter>(settings.Converters))
			{
				for (int i = 0; i < settings.Converters.Count; i++)
				{
					serializer.Converters.Insert(i, settings.Converters[i]);
				}
			}
			if (settings._typeNameHandling != null)
			{
				serializer.TypeNameHandling = settings.TypeNameHandling;
			}
			if (settings._typeNameAssemblyFormat != null)
			{
				serializer.TypeNameAssemblyFormat = settings.TypeNameAssemblyFormat;
			}
			if (settings._preserveReferencesHandling != null)
			{
				serializer.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			}
			if (settings._referenceLoopHandling != null)
			{
				serializer.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			}
			if (settings._missingMemberHandling != null)
			{
				serializer.MissingMemberHandling = settings.MissingMemberHandling;
			}
			if (settings._objectCreationHandling != null)
			{
				serializer.ObjectCreationHandling = settings.ObjectCreationHandling;
			}
			if (settings._nullValueHandling != null)
			{
				serializer.NullValueHandling = settings.NullValueHandling;
			}
			if (settings._defaultValueHandling != null)
			{
				serializer.DefaultValueHandling = settings.DefaultValueHandling;
			}
			if (settings._constructorHandling != null)
			{
				serializer.ConstructorHandling = settings.ConstructorHandling;
			}
			if (settings._context != null)
			{
				serializer.Context = settings.Context;
			}
			if (settings._checkAdditionalContent != null)
			{
				serializer._checkAdditionalContent = settings._checkAdditionalContent;
			}
			if (settings.Error != null)
			{
				serializer.Error += settings.Error;
			}
			if (settings.ContractResolver != null)
			{
				serializer.ContractResolver = settings.ContractResolver;
			}
			if (settings.ReferenceResolver != null)
			{
				serializer.ReferenceResolver = settings.ReferenceResolver;
			}
			if (settings.TraceWriter != null)
			{
				serializer.TraceWriter = settings.TraceWriter;
			}
			if (settings.Binder != null)
			{
				serializer.Binder = settings.Binder;
			}
			if (settings._formatting != null)
			{
				serializer._formatting = settings._formatting;
			}
			if (settings._dateFormatHandling != null)
			{
				serializer._dateFormatHandling = settings._dateFormatHandling;
			}
			if (settings._dateTimeZoneHandling != null)
			{
				serializer._dateTimeZoneHandling = settings._dateTimeZoneHandling;
			}
			if (settings._dateParseHandling != null)
			{
				serializer._dateParseHandling = settings._dateParseHandling;
			}
			if (settings._dateFormatStringSet)
			{
				serializer._dateFormatString = settings._dateFormatString;
				serializer._dateFormatStringSet = settings._dateFormatStringSet;
			}
			if (settings._floatFormatHandling != null)
			{
				serializer._floatFormatHandling = settings._floatFormatHandling;
			}
			if (settings._floatParseHandling != null)
			{
				serializer._floatParseHandling = settings._floatParseHandling;
			}
			if (settings._stringEscapeHandling != null)
			{
				serializer._stringEscapeHandling = settings._stringEscapeHandling;
			}
			if (settings._culture != null)
			{
				serializer._culture = settings._culture;
			}
			if (settings._maxDepthSet)
			{
				serializer._maxDepth = settings._maxDepth;
				serializer._maxDepthSet = settings._maxDepthSet;
			}
		}

		/// <summary>
		/// Populates the JSON values onto the target object.
		/// </summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> that contains the JSON structure to reader values from.</param>
		/// <param name="target">The target object to populate values onto.</param>
		// Token: 0x060002E0 RID: 736 RVA: 0x0000A772 File Offset: 0x00008972
		public void Populate(TextReader reader, object target)
		{
			this.Populate(new JsonTextReader(reader), target);
		}

		/// <summary>
		/// Populates the JSON values onto the target object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> that contains the JSON structure to reader values from.</param>
		/// <param name="target">The target object to populate values onto.</param>
		// Token: 0x060002E1 RID: 737 RVA: 0x0000A781 File Offset: 0x00008981
		public void Populate(JsonReader reader, object target)
		{
			this.PopulateInternal(reader, target);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A78C File Offset: 0x0000898C
		internal virtual void PopulateInternal(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(target, "target");
			JsonSerializerInternalReader jsonSerializerInternalReader = new JsonSerializerInternalReader(this);
			jsonSerializerInternalReader.Populate(reader, target);
		}

		/// <summary>
		/// Deserializes the Json structure contained by the specified <see cref="T:Newtonsoft.Json.JsonReader" />.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> that contains the JSON structure to deserialize.</param>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000A7BE File Offset: 0x000089BE
		public object Deserialize(JsonReader reader)
		{
			return this.Deserialize(reader, null);
		}

		/// <summary>
		/// Deserializes the Json structure contained by the specified <see cref="T:System.IO.StringReader" />
		/// into an instance of the specified type.
		/// </summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> containing the object.</param>
		/// <param name="objectType">The <see cref="T:System.Type" /> of object being deserialized.</param>
		/// <returns>The instance of <paramref name="objectType" /> being deserialized.</returns>
		// Token: 0x060002E4 RID: 740 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public object Deserialize(TextReader reader, Type objectType)
		{
			return this.Deserialize(new JsonTextReader(reader), objectType);
		}

		/// <summary>
		/// Deserializes the Json structure contained by the specified <see cref="T:Newtonsoft.Json.JsonReader" />
		/// into an instance of the specified type.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> containing the object.</param>
		/// <typeparam name="T">The type of the object to deserialize.</typeparam>
		/// <returns>The instance of <typeparamref name="T" /> being deserialized.</returns>
		// Token: 0x060002E5 RID: 741 RVA: 0x0000A7D7 File Offset: 0x000089D7
		public T Deserialize<T>(JsonReader reader)
		{
			return (T)((object)this.Deserialize(reader, typeof(T)));
		}

		/// <summary>
		/// Deserializes the Json structure contained by the specified <see cref="T:Newtonsoft.Json.JsonReader" />
		/// into an instance of the specified type.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> containing the object.</param>
		/// <param name="objectType">The <see cref="T:System.Type" /> of object being deserialized.</param>
		/// <returns>The instance of <paramref name="objectType" /> being deserialized.</returns>
		// Token: 0x060002E6 RID: 742 RVA: 0x0000A7EF File Offset: 0x000089EF
		public object Deserialize(JsonReader reader, Type objectType)
		{
			return this.DeserializeInternal(reader, objectType);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000A7FC File Offset: 0x000089FC
		internal virtual object DeserializeInternal(JsonReader reader, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			CultureInfo cultureInfo = null;
			if (this._culture != null && !this._culture.Equals(reader.Culture))
			{
				cultureInfo = reader.Culture;
				reader.Culture = this._culture;
			}
			DateTimeZoneHandling? dateTimeZoneHandling = default(DateTimeZoneHandling?);
			if (this._dateTimeZoneHandling != null && reader.DateTimeZoneHandling != this._dateTimeZoneHandling)
			{
				dateTimeZoneHandling = new DateTimeZoneHandling?(reader.DateTimeZoneHandling);
				reader.DateTimeZoneHandling = this._dateTimeZoneHandling.Value;
			}
			DateParseHandling? dateParseHandling = default(DateParseHandling?);
			if (this._dateParseHandling != null && reader.DateParseHandling != this._dateParseHandling)
			{
				dateParseHandling = new DateParseHandling?(reader.DateParseHandling);
				reader.DateParseHandling = this._dateParseHandling.Value;
			}
			FloatParseHandling? floatParseHandling = default(FloatParseHandling?);
			if (this._floatParseHandling != null && reader.FloatParseHandling != this._floatParseHandling)
			{
				floatParseHandling = new FloatParseHandling?(reader.FloatParseHandling);
				reader.FloatParseHandling = this._floatParseHandling.Value;
			}
			int? maxDepth = default(int?);
			if (this._maxDepthSet && reader.MaxDepth != this._maxDepth)
			{
				maxDepth = reader.MaxDepth;
				reader.MaxDepth = this._maxDepth;
			}
			TraceJsonReader traceJsonReader = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? new TraceJsonReader(reader) : null;
			JsonSerializerInternalReader jsonSerializerInternalReader = new JsonSerializerInternalReader(this);
			object result = jsonSerializerInternalReader.Deserialize(traceJsonReader ?? reader, objectType, this.CheckAdditionalContent);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, "Deserialized JSON: " + Environment.NewLine + traceJsonReader.GetJson(), null);
			}
			if (cultureInfo != null)
			{
				reader.Culture = cultureInfo;
			}
			if (dateTimeZoneHandling != null)
			{
				reader.DateTimeZoneHandling = dateTimeZoneHandling.Value;
			}
			if (dateParseHandling != null)
			{
				reader.DateParseHandling = dateParseHandling.Value;
			}
			if (floatParseHandling != null)
			{
				reader.FloatParseHandling = floatParseHandling.Value;
			}
			if (this._maxDepthSet)
			{
				reader.MaxDepth = maxDepth;
			}
			return result;
		}

		/// <summary>
		/// Serializes the specified <see cref="T:System.Object" /> and writes the Json structure
		/// to a <c>Stream</c> using the specified <see cref="T:System.IO.TextWriter" />. 
		/// </summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the Json structure.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to serialize.</param>
		// Token: 0x060002E8 RID: 744 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public void Serialize(TextWriter textWriter, object value)
		{
			this.Serialize(new JsonTextWriter(textWriter), value);
		}

		/// <summary>
		/// Serializes the specified <see cref="T:System.Object" /> and writes the Json structure
		/// to a <c>Stream</c> using the specified <see cref="T:System.IO.TextWriter" />. 
		/// </summary>
		/// <param name="jsonWriter">The <see cref="T:Newtonsoft.Json.JsonWriter" /> used to write the Json structure.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to serialize.</param>
		/// <param name="objectType">
		/// The type of the value being serialized.
		/// This parameter is used when <see cref="P:Newtonsoft.Json.JsonSerializer.TypeNameHandling" /> is Auto to write out the type name if the type of the value does not match.
		/// Specifing the type is optional.
		/// </param>
		// Token: 0x060002E9 RID: 745 RVA: 0x0000AA8B File Offset: 0x00008C8B
		public void Serialize(JsonWriter jsonWriter, object value, Type objectType)
		{
			this.SerializeInternal(jsonWriter, value, objectType);
		}

		/// <summary>
		/// Serializes the specified <see cref="T:System.Object" /> and writes the Json structure
		/// to a <c>Stream</c> using the specified <see cref="T:System.IO.TextWriter" />. 
		/// </summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the Json structure.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to serialize.</param>
		/// <param name="objectType">
		/// The type of the value being serialized.
		/// This parameter is used when <see cref="P:Newtonsoft.Json.JsonSerializer.TypeNameHandling" /> is Auto to write out the type name if the type of the value does not match.
		/// Specifing the type is optional.
		/// </param>
		// Token: 0x060002EA RID: 746 RVA: 0x0000AA96 File Offset: 0x00008C96
		public void Serialize(TextWriter textWriter, object value, Type objectType)
		{
			this.Serialize(new JsonTextWriter(textWriter), value, objectType);
		}

		/// <summary>
		/// Serializes the specified <see cref="T:System.Object" /> and writes the Json structure
		/// to a <c>Stream</c> using the specified <see cref="T:Newtonsoft.Json.JsonWriter" />. 
		/// </summary>
		/// <param name="jsonWriter">The <see cref="T:Newtonsoft.Json.JsonWriter" /> used to write the Json structure.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to serialize.</param>
		// Token: 0x060002EB RID: 747 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		public void Serialize(JsonWriter jsonWriter, object value)
		{
			this.SerializeInternal(jsonWriter, value, null);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		internal virtual void SerializeInternal(JsonWriter jsonWriter, object value, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(jsonWriter, "jsonWriter");
			Formatting? formatting = default(Formatting?);
			if (this._formatting != null && jsonWriter.Formatting != this._formatting)
			{
				formatting = new Formatting?(jsonWriter.Formatting);
				jsonWriter.Formatting = this._formatting.Value;
			}
			DateFormatHandling? dateFormatHandling = default(DateFormatHandling?);
			if (this._dateFormatHandling != null && jsonWriter.DateFormatHandling != this._dateFormatHandling)
			{
				dateFormatHandling = new DateFormatHandling?(jsonWriter.DateFormatHandling);
				jsonWriter.DateFormatHandling = this._dateFormatHandling.Value;
			}
			DateTimeZoneHandling? dateTimeZoneHandling = default(DateTimeZoneHandling?);
			if (this._dateTimeZoneHandling != null && jsonWriter.DateTimeZoneHandling != this._dateTimeZoneHandling)
			{
				dateTimeZoneHandling = new DateTimeZoneHandling?(jsonWriter.DateTimeZoneHandling);
				jsonWriter.DateTimeZoneHandling = this._dateTimeZoneHandling.Value;
			}
			FloatFormatHandling? floatFormatHandling = default(FloatFormatHandling?);
			if (this._floatFormatHandling != null && jsonWriter.FloatFormatHandling != this._floatFormatHandling)
			{
				floatFormatHandling = new FloatFormatHandling?(jsonWriter.FloatFormatHandling);
				jsonWriter.FloatFormatHandling = this._floatFormatHandling.Value;
			}
			StringEscapeHandling? stringEscapeHandling = default(StringEscapeHandling?);
			if (this._stringEscapeHandling != null && jsonWriter.StringEscapeHandling != this._stringEscapeHandling)
			{
				stringEscapeHandling = new StringEscapeHandling?(jsonWriter.StringEscapeHandling);
				jsonWriter.StringEscapeHandling = this._stringEscapeHandling.Value;
			}
			CultureInfo cultureInfo = null;
			if (this._culture != null && !this._culture.Equals(jsonWriter.Culture))
			{
				cultureInfo = jsonWriter.Culture;
				jsonWriter.Culture = this._culture;
			}
			string dateFormatString = null;
			if (this._dateFormatStringSet && jsonWriter.DateFormatString != this._dateFormatString)
			{
				dateFormatString = jsonWriter.DateFormatString;
				jsonWriter.DateFormatString = this._dateFormatString;
			}
			TraceJsonWriter traceJsonWriter = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? new TraceJsonWriter(jsonWriter) : null;
			JsonSerializerInternalWriter jsonSerializerInternalWriter = new JsonSerializerInternalWriter(this);
			jsonSerializerInternalWriter.Serialize(traceJsonWriter ?? jsonWriter, value, objectType);
			if (traceJsonWriter != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, "Serialized JSON: " + Environment.NewLine + traceJsonWriter.GetJson(), null);
			}
			if (formatting != null)
			{
				jsonWriter.Formatting = formatting.Value;
			}
			if (dateFormatHandling != null)
			{
				jsonWriter.DateFormatHandling = dateFormatHandling.Value;
			}
			if (dateTimeZoneHandling != null)
			{
				jsonWriter.DateTimeZoneHandling = dateTimeZoneHandling.Value;
			}
			if (floatFormatHandling != null)
			{
				jsonWriter.FloatFormatHandling = floatFormatHandling.Value;
			}
			if (stringEscapeHandling != null)
			{
				jsonWriter.StringEscapeHandling = stringEscapeHandling.Value;
			}
			if (this._dateFormatStringSet)
			{
				jsonWriter.DateFormatString = dateFormatString;
			}
			if (cultureInfo != null)
			{
				jsonWriter.Culture = cultureInfo;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000ADEB File Offset: 0x00008FEB
		internal IReferenceResolver GetReferenceResolver()
		{
			if (this._referenceResolver == null)
			{
				this._referenceResolver = new DefaultReferenceResolver();
			}
			return this._referenceResolver;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000AE06 File Offset: 0x00009006
		internal JsonConverter GetMatchingConverter(Type type)
		{
			return JsonSerializer.GetMatchingConverter(this._converters, type);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000AE14 File Offset: 0x00009014
		internal static JsonConverter GetMatchingConverter(IList<JsonConverter> converters, Type objectType)
		{
			if (converters != null)
			{
				for (int i = 0; i < converters.Count; i++)
				{
					JsonConverter jsonConverter = converters[i];
					if (jsonConverter.CanConvert(objectType))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000AE4C File Offset: 0x0000904C
		internal void OnError(ErrorEventArgs e)
		{
			EventHandler<ErrorEventArgs> error = this.Error;
			if (error != null)
			{
				error.Invoke(this, e);
			}
		}

		// Token: 0x040000ED RID: 237
		internal TypeNameHandling _typeNameHandling;

		// Token: 0x040000EE RID: 238
		internal FormatterAssemblyStyle _typeNameAssemblyFormat;

		// Token: 0x040000EF RID: 239
		internal PreserveReferencesHandling _preserveReferencesHandling;

		// Token: 0x040000F0 RID: 240
		internal ReferenceLoopHandling _referenceLoopHandling;

		// Token: 0x040000F1 RID: 241
		internal MissingMemberHandling _missingMemberHandling;

		// Token: 0x040000F2 RID: 242
		internal ObjectCreationHandling _objectCreationHandling;

		// Token: 0x040000F3 RID: 243
		internal NullValueHandling _nullValueHandling;

		// Token: 0x040000F4 RID: 244
		internal DefaultValueHandling _defaultValueHandling;

		// Token: 0x040000F5 RID: 245
		internal ConstructorHandling _constructorHandling;

		// Token: 0x040000F6 RID: 246
		internal JsonConverterCollection _converters;

		// Token: 0x040000F7 RID: 247
		internal IContractResolver _contractResolver;

		// Token: 0x040000F8 RID: 248
		internal ITraceWriter _traceWriter;

		// Token: 0x040000F9 RID: 249
		internal SerializationBinder _binder;

		// Token: 0x040000FA RID: 250
		internal StreamingContext _context;

		// Token: 0x040000FB RID: 251
		private IReferenceResolver _referenceResolver;

		// Token: 0x040000FC RID: 252
		private Formatting? _formatting;

		// Token: 0x040000FD RID: 253
		private DateFormatHandling? _dateFormatHandling;

		// Token: 0x040000FE RID: 254
		private DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x040000FF RID: 255
		private DateParseHandling? _dateParseHandling;

		// Token: 0x04000100 RID: 256
		private FloatFormatHandling? _floatFormatHandling;

		// Token: 0x04000101 RID: 257
		private FloatParseHandling? _floatParseHandling;

		// Token: 0x04000102 RID: 258
		private StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x04000103 RID: 259
		private CultureInfo _culture;

		// Token: 0x04000104 RID: 260
		private int? _maxDepth;

		// Token: 0x04000105 RID: 261
		private bool _maxDepthSet;

		// Token: 0x04000106 RID: 262
		private bool? _checkAdditionalContent;

		// Token: 0x04000107 RID: 263
		private string _dateFormatString;

		// Token: 0x04000108 RID: 264
		private bool _dateFormatStringSet;
	}
}

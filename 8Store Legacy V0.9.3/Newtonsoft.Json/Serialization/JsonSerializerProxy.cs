using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A2 RID: 162
	internal class JsonSerializerProxy : JsonSerializer
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600083F RID: 2111 RVA: 0x00020785 File Offset: 0x0001E985
		// (remove) Token: 0x06000840 RID: 2112 RVA: 0x00020793 File Offset: 0x0001E993
		public override event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this._serializer.Error += value;
			}
			remove
			{
				this._serializer.Error -= value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000207A1 File Offset: 0x0001E9A1
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x000207AE File Offset: 0x0001E9AE
		public override IReferenceResolver ReferenceResolver
		{
			get
			{
				return this._serializer.ReferenceResolver;
			}
			set
			{
				this._serializer.ReferenceResolver = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000207BC File Offset: 0x0001E9BC
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x000207C9 File Offset: 0x0001E9C9
		public override ITraceWriter TraceWriter
		{
			get
			{
				return this._serializer.TraceWriter;
			}
			set
			{
				this._serializer.TraceWriter = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000207D7 File Offset: 0x0001E9D7
		public override JsonConverterCollection Converters
		{
			get
			{
				return this._serializer.Converters;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000207E4 File Offset: 0x0001E9E4
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x000207F1 File Offset: 0x0001E9F1
		public override DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._serializer.DefaultValueHandling;
			}
			set
			{
				this._serializer.DefaultValueHandling = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000207FF File Offset: 0x0001E9FF
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0002080C File Offset: 0x0001EA0C
		public override IContractResolver ContractResolver
		{
			get
			{
				return this._serializer.ContractResolver;
			}
			set
			{
				this._serializer.ContractResolver = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0002081A File Offset: 0x0001EA1A
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x00020827 File Offset: 0x0001EA27
		public override MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._serializer.MissingMemberHandling;
			}
			set
			{
				this._serializer.MissingMemberHandling = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00020835 File Offset: 0x0001EA35
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00020842 File Offset: 0x0001EA42
		public override NullValueHandling NullValueHandling
		{
			get
			{
				return this._serializer.NullValueHandling;
			}
			set
			{
				this._serializer.NullValueHandling = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00020850 File Offset: 0x0001EA50
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0002085D File Offset: 0x0001EA5D
		public override ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._serializer.ObjectCreationHandling;
			}
			set
			{
				this._serializer.ObjectCreationHandling = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0002086B File Offset: 0x0001EA6B
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00020878 File Offset: 0x0001EA78
		public override ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._serializer.ReferenceLoopHandling;
			}
			set
			{
				this._serializer.ReferenceLoopHandling = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00020886 File Offset: 0x0001EA86
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00020893 File Offset: 0x0001EA93
		public override PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._serializer.PreserveReferencesHandling;
			}
			set
			{
				this._serializer.PreserveReferencesHandling = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x000208A1 File Offset: 0x0001EAA1
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x000208AE File Offset: 0x0001EAAE
		public override TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._serializer.TypeNameHandling;
			}
			set
			{
				this._serializer.TypeNameHandling = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x000208BC File Offset: 0x0001EABC
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x000208C9 File Offset: 0x0001EAC9
		public override FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormat;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormat = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000208D7 File Offset: 0x0001EAD7
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x000208E4 File Offset: 0x0001EAE4
		public override ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._serializer.ConstructorHandling;
			}
			set
			{
				this._serializer.ConstructorHandling = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000208F2 File Offset: 0x0001EAF2
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x000208FF File Offset: 0x0001EAFF
		public override SerializationBinder Binder
		{
			get
			{
				return this._serializer.Binder;
			}
			set
			{
				this._serializer.Binder = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0002090D File Offset: 0x0001EB0D
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x0002091A File Offset: 0x0001EB1A
		public override StreamingContext Context
		{
			get
			{
				return this._serializer.Context;
			}
			set
			{
				this._serializer.Context = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00020928 File Offset: 0x0001EB28
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00020935 File Offset: 0x0001EB35
		public override Formatting Formatting
		{
			get
			{
				return this._serializer.Formatting;
			}
			set
			{
				this._serializer.Formatting = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00020943 File Offset: 0x0001EB43
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x00020950 File Offset: 0x0001EB50
		public override DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._serializer.DateFormatHandling;
			}
			set
			{
				this._serializer.DateFormatHandling = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0002095E File Offset: 0x0001EB5E
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0002096B File Offset: 0x0001EB6B
		public override DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._serializer.DateTimeZoneHandling;
			}
			set
			{
				this._serializer.DateTimeZoneHandling = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00020979 File Offset: 0x0001EB79
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00020986 File Offset: 0x0001EB86
		public override DateParseHandling DateParseHandling
		{
			get
			{
				return this._serializer.DateParseHandling;
			}
			set
			{
				this._serializer.DateParseHandling = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00020994 File Offset: 0x0001EB94
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x000209A1 File Offset: 0x0001EBA1
		public override FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._serializer.FloatFormatHandling;
			}
			set
			{
				this._serializer.FloatFormatHandling = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x000209AF File Offset: 0x0001EBAF
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x000209BC File Offset: 0x0001EBBC
		public override FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._serializer.FloatParseHandling;
			}
			set
			{
				this._serializer.FloatParseHandling = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x000209CA File Offset: 0x0001EBCA
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x000209D7 File Offset: 0x0001EBD7
		public override StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._serializer.StringEscapeHandling;
			}
			set
			{
				this._serializer.StringEscapeHandling = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000209E5 File Offset: 0x0001EBE5
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x000209F2 File Offset: 0x0001EBF2
		public override string DateFormatString
		{
			get
			{
				return this._serializer.DateFormatString;
			}
			set
			{
				this._serializer.DateFormatString = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00020A00 File Offset: 0x0001EC00
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x00020A0D File Offset: 0x0001EC0D
		public override CultureInfo Culture
		{
			get
			{
				return this._serializer.Culture;
			}
			set
			{
				this._serializer.Culture = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00020A1B File Offset: 0x0001EC1B
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00020A28 File Offset: 0x0001EC28
		public override int? MaxDepth
		{
			get
			{
				return this._serializer.MaxDepth;
			}
			set
			{
				this._serializer.MaxDepth = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00020A36 File Offset: 0x0001EC36
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00020A43 File Offset: 0x0001EC43
		public override bool CheckAdditionalContent
		{
			get
			{
				return this._serializer.CheckAdditionalContent;
			}
			set
			{
				this._serializer.CheckAdditionalContent = value;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00020A51 File Offset: 0x0001EC51
		internal JsonSerializerInternalBase GetInternalSerializer()
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader;
			}
			return this._serializerWriter;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00020A68 File Offset: 0x0001EC68
		public JsonSerializerProxy(JsonSerializerInternalReader serializerReader)
		{
			ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			this._serializerReader = serializerReader;
			this._serializer = serializerReader.Serializer;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00020A8E File Offset: 0x0001EC8E
		public JsonSerializerProxy(JsonSerializerInternalWriter serializerWriter)
		{
			ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			this._serializerWriter = serializerWriter;
			this._serializer = serializerWriter.Serializer;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		internal override object DeserializeInternal(JsonReader reader, Type objectType)
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader.Deserialize(reader, objectType, false);
			}
			return this._serializer.Deserialize(reader, objectType);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00020ADA File Offset: 0x0001ECDA
		internal override void PopulateInternal(JsonReader reader, object target)
		{
			if (this._serializerReader != null)
			{
				this._serializerReader.Populate(reader, target);
				return;
			}
			this._serializer.Populate(reader, target);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00020AFF File Offset: 0x0001ECFF
		internal override void SerializeInternal(JsonWriter jsonWriter, object value, Type rootType)
		{
			if (this._serializerWriter != null)
			{
				this._serializerWriter.Serialize(jsonWriter, value, rootType);
				return;
			}
			this._serializer.Serialize(jsonWriter, value);
		}

		// Token: 0x04000301 RID: 769
		private readonly JsonSerializerInternalReader _serializerReader;

		// Token: 0x04000302 RID: 770
		private readonly JsonSerializerInternalWriter _serializerWriter;

		// Token: 0x04000303 RID: 771
		private readonly JsonSerializer _serializer;
	}
}

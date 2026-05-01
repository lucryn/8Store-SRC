using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000AB RID: 171
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00023030 File Offset: 0x00021230
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00023091 File Offset: 0x00021291
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002309E File Offset: 0x0002129E
		public override bool Read()
		{
			bool result = this._innerReader.Read();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000230B1 File Offset: 0x000212B1
		public override int? ReadAsInt32()
		{
			int? result = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000230C4 File Offset: 0x000212C4
		[NullableContext(2)]
		public override string ReadAsString()
		{
			string result = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000230D7 File Offset: 0x000212D7
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			byte[] result = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000230EA File Offset: 0x000212EA
		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000230FD File Offset: 0x000212FD
		public override double? ReadAsDouble()
		{
			double? result = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00023110 File Offset: 0x00021310
		public override bool? ReadAsBoolean()
		{
			bool? result = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00023123 File Offset: 0x00021323
		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00023136 File Offset: 0x00021336
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00023149 File Offset: 0x00021349
		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0002315F File Offset: 0x0002135F
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0002316C File Offset: 0x0002136C
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00023179 File Offset: 0x00021379
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00023186 File Offset: 0x00021386
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00023194 File Offset: 0x00021394
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000231A1 File Offset: 0x000213A1
		[Nullable(2)]
		public override object Value
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x000231AE File Offset: 0x000213AE
		[Nullable(2)]
		public override Type ValueType
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000231BB File Offset: 0x000213BB
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000231C8 File Offset: 0x000213C8
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000231EC File Offset: 0x000213EC
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00023210 File Offset: 0x00021410
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x0400031D RID: 797
		private readonly JsonReader _innerReader;

		// Token: 0x0400031E RID: 798
		private readonly JsonTextWriter _textWriter;

		// Token: 0x0400031F RID: 799
		private readonly StringWriter _sw;
	}
}

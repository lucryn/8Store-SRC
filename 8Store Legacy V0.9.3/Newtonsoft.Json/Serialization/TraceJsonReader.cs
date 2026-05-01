using System;
using System.Globalization;
using System.IO;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A9 RID: 169
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0002101C File Offset: 0x0001F21C
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00021058 File Offset: 0x0001F258
		public string GetJson()
		{
			return this._sw.ToString();
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00021068 File Offset: 0x0001F268
		public override bool Read()
		{
			bool result = this._innerReader.Read();
			this._textWriter.WriteToken(this._innerReader, false, false);
			return result;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00021098 File Offset: 0x0001F298
		public override int? ReadAsInt32()
		{
			int? result = this._innerReader.ReadAsInt32();
			this._textWriter.WriteToken(this._innerReader, false, false);
			return result;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000210C8 File Offset: 0x0001F2C8
		public override string ReadAsString()
		{
			string result = this._innerReader.ReadAsString();
			this._textWriter.WriteToken(this._innerReader, false, false);
			return result;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000210F8 File Offset: 0x0001F2F8
		public override byte[] ReadAsBytes()
		{
			byte[] result = this._innerReader.ReadAsBytes();
			this._textWriter.WriteToken(this._innerReader, false, false);
			return result;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00021128 File Offset: 0x0001F328
		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._innerReader.ReadAsDecimal();
			this._textWriter.WriteToken(this._innerReader, false, false);
			return result;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00021158 File Offset: 0x0001F358
		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._innerReader.ReadAsDateTime();
			this._textWriter.WriteToken(this._innerReader, false, false);
			return result;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00021188 File Offset: 0x0001F388
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._innerReader.ReadAsDateTimeOffset();
			this._textWriter.WriteToken(this._innerReader, false, false);
			return result;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000211B5 File Offset: 0x0001F3B5
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000211C2 File Offset: 0x0001F3C2
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000211CF File Offset: 0x0001F3CF
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x000211DC File Offset: 0x0001F3DC
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

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000211EA File Offset: 0x0001F3EA
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000211F7 File Offset: 0x0001F3F7
		public override object Value
		{
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00021204 File Offset: 0x0001F404
		public override Type ValueType
		{
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00021211 File Offset: 0x0001F411
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00021220 File Offset: 0x0001F420
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00021244 File Offset: 0x0001F444
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00021268 File Offset: 0x0001F468
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

		// Token: 0x04000311 RID: 785
		private readonly JsonReader _innerReader;

		// Token: 0x04000312 RID: 786
		private readonly JsonTextWriter _textWriter;

		// Token: 0x04000313 RID: 787
		private readonly StringWriter _sw;
	}
}

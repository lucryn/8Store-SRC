using System;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000AA RID: 170
	internal class TraceJsonWriter : JsonWriter
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x0002128C File Offset: 0x0001F48C
		public TraceJsonWriter(JsonWriter innerWriter)
		{
			this._innerWriter = innerWriter;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
			this._textWriter.Culture = innerWriter.Culture;
			this._textWriter.DateFormatHandling = innerWriter.DateFormatHandling;
			this._textWriter.DateFormatString = innerWriter.DateFormatString;
			this._textWriter.DateTimeZoneHandling = innerWriter.DateTimeZoneHandling;
			this._textWriter.FloatFormatHandling = innerWriter.FloatFormatHandling;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00021328 File Offset: 0x0001F528
		public string GetJson()
		{
			return this._sw.ToString();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00021335 File Offset: 0x0001F535
		public override void WriteValue(decimal value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00021356 File Offset: 0x0001F556
		public override void WriteValue(bool value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00021377 File Offset: 0x0001F577
		public override void WriteValue(byte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00021398 File Offset: 0x0001F598
		public override void WriteValue(byte? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000213B9 File Offset: 0x0001F5B9
		public override void WriteValue(char value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000213DA File Offset: 0x0001F5DA
		public override void WriteValue(byte[] value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000213FB File Offset: 0x0001F5FB
		public override void WriteValue(DateTime value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002141C File Offset: 0x0001F61C
		public override void WriteValue(DateTimeOffset value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002143D File Offset: 0x0001F63D
		public override void WriteValue(double value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002145E File Offset: 0x0001F65E
		public override void WriteUndefined()
		{
			this._textWriter.WriteUndefined();
			this._innerWriter.WriteUndefined();
			base.WriteUndefined();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002147C File Offset: 0x0001F67C
		public override void WriteNull()
		{
			this._textWriter.WriteNull();
			this._innerWriter.WriteNull();
			base.WriteUndefined();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002149A File Offset: 0x0001F69A
		public override void WriteValue(float value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000214BB File Offset: 0x0001F6BB
		public override void WriteValue(Guid value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000214DC File Offset: 0x0001F6DC
		public override void WriteValue(int value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000214FD File Offset: 0x0001F6FD
		public override void WriteValue(long value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00021520 File Offset: 0x0001F720
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				this._textWriter.WriteValue(value);
				this._innerWriter.WriteValue(value);
				base.InternalWriteValue(JsonToken.Integer);
				return;
			}
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00021574 File Offset: 0x0001F774
		public override void WriteValue(sbyte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00021595 File Offset: 0x0001F795
		public override void WriteValue(short value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000215B6 File Offset: 0x0001F7B6
		public override void WriteValue(string value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000215D7 File Offset: 0x0001F7D7
		public override void WriteValue(TimeSpan value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000215F8 File Offset: 0x0001F7F8
		public override void WriteValue(uint value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00021619 File Offset: 0x0001F819
		public override void WriteValue(ulong value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002163A File Offset: 0x0001F83A
		public override void WriteValue(Uri value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002165B File Offset: 0x0001F85B
		public override void WriteValue(ushort value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002167C File Offset: 0x0001F87C
		public override void WriteWhitespace(string ws)
		{
			this._textWriter.WriteWhitespace(ws);
			this._innerWriter.WriteWhitespace(ws);
			base.WriteWhitespace(ws);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002169D File Offset: 0x0001F89D
		public override void WriteComment(string text)
		{
			this._textWriter.WriteComment(text);
			this._innerWriter.WriteComment(text);
			base.WriteComment(text);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000216BE File Offset: 0x0001F8BE
		public override void WriteStartArray()
		{
			this._textWriter.WriteStartArray();
			this._innerWriter.WriteStartArray();
			base.WriteStartArray();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000216DC File Offset: 0x0001F8DC
		public override void WriteEndArray()
		{
			this._textWriter.WriteEndArray();
			this._innerWriter.WriteEndArray();
			base.WriteEndArray();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000216FA File Offset: 0x0001F8FA
		public override void WriteStartConstructor(string name)
		{
			this._textWriter.WriteStartConstructor(name);
			this._innerWriter.WriteStartConstructor(name);
			base.WriteStartConstructor(name);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002171B File Offset: 0x0001F91B
		public override void WriteEndConstructor()
		{
			this._textWriter.WriteEndConstructor();
			this._innerWriter.WriteEndConstructor();
			base.WriteEndConstructor();
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00021739 File Offset: 0x0001F939
		public override void WritePropertyName(string name)
		{
			this._textWriter.WritePropertyName(name);
			this._innerWriter.WritePropertyName(name);
			base.WritePropertyName(name);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002175A File Offset: 0x0001F95A
		public override void WritePropertyName(string name, bool escape)
		{
			this._textWriter.WritePropertyName(name, escape);
			this._innerWriter.WritePropertyName(name, escape);
			base.WritePropertyName(name);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002177D File Offset: 0x0001F97D
		public override void WriteStartObject()
		{
			this._textWriter.WriteStartObject();
			this._innerWriter.WriteStartObject();
			base.WriteStartObject();
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002179B File Offset: 0x0001F99B
		public override void WriteEndObject()
		{
			this._textWriter.WriteEndObject();
			this._innerWriter.WriteEndObject();
			base.WriteEndObject();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000217B9 File Offset: 0x0001F9B9
		public override void WriteRaw(string json)
		{
			this._textWriter.WriteRaw(json);
			this._innerWriter.WriteRaw(json);
			base.WriteRaw(json);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000217DA File Offset: 0x0001F9DA
		public override void WriteRawValue(string json)
		{
			this._textWriter.WriteRawValue(json);
			this._innerWriter.WriteRawValue(json);
			base.WriteRawValue(json);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000217FB File Offset: 0x0001F9FB
		public override void Close()
		{
			this._textWriter.Close();
			this._innerWriter.Close();
			base.Close();
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00021819 File Offset: 0x0001FA19
		public override void Flush()
		{
			this._textWriter.Flush();
			this._innerWriter.Flush();
		}

		// Token: 0x04000314 RID: 788
		private readonly JsonWriter _innerWriter;

		// Token: 0x04000315 RID: 789
		private readonly JsonTextWriter _textWriter;

		// Token: 0x04000316 RID: 790
		private readonly StringWriter _sw;
	}
}

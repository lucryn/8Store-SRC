using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000AC RID: 172
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonWriter : JsonWriter
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x00023234 File Offset: 0x00021434
		public TraceJsonWriter(JsonWriter innerWriter)
		{
			this._innerWriter = innerWriter;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Serialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
			this._textWriter.Culture = innerWriter.Culture;
			this._textWriter.DateFormatHandling = innerWriter.DateFormatHandling;
			this._textWriter.DateFormatString = innerWriter.DateFormatString;
			this._textWriter.DateTimeZoneHandling = innerWriter.DateTimeZoneHandling;
			this._textWriter.FloatFormatHandling = innerWriter.FloatFormatHandling;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000232EA File Offset: 0x000214EA
		public string GetSerializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000232F7 File Offset: 0x000214F7
		public override void WriteValue(decimal value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00023318 File Offset: 0x00021518
		public override void WriteValue(decimal? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002334F File Offset: 0x0002154F
		public override void WriteValue(bool value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00023370 File Offset: 0x00021570
		public override void WriteValue(bool? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000233A7 File Offset: 0x000215A7
		public override void WriteValue(byte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000233C8 File Offset: 0x000215C8
		public override void WriteValue(byte? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000233FF File Offset: 0x000215FF
		public override void WriteValue(char value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00023420 File Offset: 0x00021620
		public override void WriteValue(char? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00023457 File Offset: 0x00021657
		[NullableContext(2)]
		public override void WriteValue(byte[] value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00023482 File Offset: 0x00021682
		public override void WriteValue(DateTime value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000234A3 File Offset: 0x000216A3
		public override void WriteValue(DateTime? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000234DA File Offset: 0x000216DA
		public override void WriteValue(DateTimeOffset value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000234FB File Offset: 0x000216FB
		public override void WriteValue(DateTimeOffset? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00023532 File Offset: 0x00021732
		public override void WriteValue(double value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00023553 File Offset: 0x00021753
		public override void WriteValue(double? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002358A File Offset: 0x0002178A
		public override void WriteUndefined()
		{
			this._textWriter.WriteUndefined();
			this._innerWriter.WriteUndefined();
			base.WriteUndefined();
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000235A8 File Offset: 0x000217A8
		public override void WriteNull()
		{
			this._textWriter.WriteNull();
			this._innerWriter.WriteNull();
			base.WriteUndefined();
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000235C6 File Offset: 0x000217C6
		public override void WriteValue(float value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000235E7 File Offset: 0x000217E7
		public override void WriteValue(float? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002361E File Offset: 0x0002181E
		public override void WriteValue(Guid value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002363F File Offset: 0x0002183F
		public override void WriteValue(Guid? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00023676 File Offset: 0x00021876
		public override void WriteValue(int value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00023697 File Offset: 0x00021897
		public override void WriteValue(int? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000236CE File Offset: 0x000218CE
		public override void WriteValue(long value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000236EF File Offset: 0x000218EF
		public override void WriteValue(long? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00023726 File Offset: 0x00021926
		[NullableContext(2)]
		public override void WriteValue(object value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00023752 File Offset: 0x00021952
		public override void WriteValue(sbyte value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00023773 File Offset: 0x00021973
		public override void WriteValue(sbyte? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000237AA File Offset: 0x000219AA
		public override void WriteValue(short value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000237CB File Offset: 0x000219CB
		public override void WriteValue(short? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00023802 File Offset: 0x00021A02
		[NullableContext(2)]
		public override void WriteValue(string value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00023823 File Offset: 0x00021A23
		public override void WriteValue(TimeSpan value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00023844 File Offset: 0x00021A44
		public override void WriteValue(TimeSpan? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002387B File Offset: 0x00021A7B
		public override void WriteValue(uint value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002389C File Offset: 0x00021A9C
		public override void WriteValue(uint? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000238D3 File Offset: 0x00021AD3
		public override void WriteValue(ulong value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000238F4 File Offset: 0x00021AF4
		public override void WriteValue(ulong? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002392B File Offset: 0x00021B2B
		[NullableContext(2)]
		public override void WriteValue(Uri value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value == null)
			{
				base.WriteUndefined();
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002395C File Offset: 0x00021B5C
		public override void WriteValue(ushort value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			base.WriteValue(value);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002397D File Offset: 0x00021B7D
		public override void WriteValue(ushort? value)
		{
			this._textWriter.WriteValue(value);
			this._innerWriter.WriteValue(value);
			if (value != null)
			{
				base.WriteValue(value.GetValueOrDefault());
				return;
			}
			base.WriteUndefined();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000239B4 File Offset: 0x00021BB4
		public override void WriteWhitespace(string ws)
		{
			this._textWriter.WriteWhitespace(ws);
			this._innerWriter.WriteWhitespace(ws);
			base.WriteWhitespace(ws);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000239D5 File Offset: 0x00021BD5
		[NullableContext(2)]
		public override void WriteComment(string text)
		{
			this._textWriter.WriteComment(text);
			this._innerWriter.WriteComment(text);
			base.WriteComment(text);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000239F6 File Offset: 0x00021BF6
		public override void WriteStartArray()
		{
			this._textWriter.WriteStartArray();
			this._innerWriter.WriteStartArray();
			base.WriteStartArray();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00023A14 File Offset: 0x00021C14
		public override void WriteEndArray()
		{
			this._textWriter.WriteEndArray();
			this._innerWriter.WriteEndArray();
			base.WriteEndArray();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00023A32 File Offset: 0x00021C32
		public override void WriteStartConstructor(string name)
		{
			this._textWriter.WriteStartConstructor(name);
			this._innerWriter.WriteStartConstructor(name);
			base.WriteStartConstructor(name);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00023A53 File Offset: 0x00021C53
		public override void WriteEndConstructor()
		{
			this._textWriter.WriteEndConstructor();
			this._innerWriter.WriteEndConstructor();
			base.WriteEndConstructor();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00023A71 File Offset: 0x00021C71
		public override void WritePropertyName(string name)
		{
			this._textWriter.WritePropertyName(name);
			this._innerWriter.WritePropertyName(name);
			base.WritePropertyName(name);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00023A92 File Offset: 0x00021C92
		public override void WritePropertyName(string name, bool escape)
		{
			this._textWriter.WritePropertyName(name, escape);
			this._innerWriter.WritePropertyName(name, escape);
			base.WritePropertyName(name);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00023AB5 File Offset: 0x00021CB5
		public override void WriteStartObject()
		{
			this._textWriter.WriteStartObject();
			this._innerWriter.WriteStartObject();
			base.WriteStartObject();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00023AD3 File Offset: 0x00021CD3
		public override void WriteEndObject()
		{
			this._textWriter.WriteEndObject();
			this._innerWriter.WriteEndObject();
			base.WriteEndObject();
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00023AF1 File Offset: 0x00021CF1
		[NullableContext(2)]
		public override void WriteRawValue(string json)
		{
			this._textWriter.WriteRawValue(json);
			this._innerWriter.WriteRawValue(json);
			base.InternalWriteValue(JsonToken.Undefined);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00023B13 File Offset: 0x00021D13
		[NullableContext(2)]
		public override void WriteRaw(string json)
		{
			this._textWriter.WriteRaw(json);
			this._innerWriter.WriteRaw(json);
			base.WriteRaw(json);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00023B34 File Offset: 0x00021D34
		public override void Close()
		{
			this._textWriter.Close();
			this._innerWriter.Close();
			base.Close();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00023B52 File Offset: 0x00021D52
		public override void Flush()
		{
			this._textWriter.Flush();
			this._innerWriter.Flush();
		}

		// Token: 0x04000320 RID: 800
		private readonly JsonWriter _innerWriter;

		// Token: 0x04000321 RID: 801
		private readonly JsonTextWriter _textWriter;

		// Token: 0x04000322 RID: 802
		private readonly StringWriter _sw;
	}
}

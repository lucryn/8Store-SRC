using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonReaderException : JsonException
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004AA6 File Offset: 0x00002CA6
		public int LineNumber { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004AAE File Offset: 0x00002CAE
		public int LinePosition { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004AB6 File Offset: 0x00002CB6
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x0600013B RID: 315 RVA: 0x00004ABE File Offset: 0x00002CBE
		public JsonReaderException()
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004AC6 File Offset: 0x00002CC6
		public JsonReaderException(string message) : base(message)
		{
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004ACF File Offset: 0x00002CCF
		public JsonReaderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004AD9 File Offset: 0x00002CD9
		public JsonReaderException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004AFA File Offset: 0x00002CFA
		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return JsonReaderException.Create(reader, message, null);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004B04 File Offset: 0x00002D04
		internal static JsonReaderException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonReaderException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004B1C File Offset: 0x00002D1C
		internal static JsonReaderException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int lineNumber;
			int linePosition;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				lineNumber = lineInfo.LineNumber;
				linePosition = lineInfo.LinePosition;
			}
			else
			{
				lineNumber = 0;
				linePosition = 0;
			}
			return new JsonReaderException(message, path, lineNumber, linePosition, ex);
		}
	}
}

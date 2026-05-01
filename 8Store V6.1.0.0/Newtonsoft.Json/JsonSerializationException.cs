using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonSerializationException : JsonException
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004B64 File Offset: 0x00002D64
		public int LineNumber { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004B6C File Offset: 0x00002D6C
		public int LinePosition { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004B74 File Offset: 0x00002D74
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x06000146 RID: 326 RVA: 0x00004B7C File Offset: 0x00002D7C
		public JsonSerializationException()
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004B84 File Offset: 0x00002D84
		public JsonSerializationException(string message) : base(message)
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004B8D File Offset: 0x00002D8D
		public JsonSerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004B97 File Offset: 0x00002D97
		public JsonSerializationException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004BB8 File Offset: 0x00002DB8
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004BC2 File Offset: 0x00002DC2
		internal static JsonSerializationException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004BD8 File Offset: 0x00002DD8
		internal static JsonSerializationException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
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
			return new JsonSerializationException(message, path, lineNumber, linePosition, ex);
		}
	}
}

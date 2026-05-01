using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonWriterException : JsonException
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000FEEB File Offset: 0x0000E0EB
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x06000422 RID: 1058 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
		public JsonWriterException()
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000FEFB File Offset: 0x0000E0FB
		public JsonWriterException(string message) : base(message)
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000FF04 File Offset: 0x0000E104
		public JsonWriterException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000FF0E File Offset: 0x0000E10E
		public JsonWriterException(string message, string path, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000FF1F File Offset: 0x0000E11F
		internal static JsonWriterException Create(JsonWriter writer, string message, [Nullable(2)] Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000FF2E File Offset: 0x0000E12E
		internal static JsonWriterException Create(string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, path, ex);
		}
	}
}

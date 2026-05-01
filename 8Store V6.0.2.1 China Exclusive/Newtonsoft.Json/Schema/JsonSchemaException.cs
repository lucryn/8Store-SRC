using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B1 RID: 177
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaException : JsonException
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002510F File Offset: 0x0002330F
		public int LineNumber { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00025117 File Offset: 0x00023317
		public int LinePosition { get; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0002511F File Offset: 0x0002331F
		public string Path { get; }

		// Token: 0x060008F8 RID: 2296 RVA: 0x00025127 File Offset: 0x00023327
		public JsonSchemaException()
		{
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002512F File Offset: 0x0002332F
		public JsonSchemaException(string message) : base(message)
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00025138 File Offset: 0x00023338
		public JsonSchemaException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00025142 File Offset: 0x00023342
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}

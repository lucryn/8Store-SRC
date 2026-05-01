using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000027 RID: 39
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonException : Exception
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00002FAF File Offset: 0x000011AF
		public JsonException()
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002FB7 File Offset: 0x000011B7
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002FC0 File Offset: 0x000011C0
		public JsonException(string message, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002FCA File Offset: 0x000011CA
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}

using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// The exception thrown when an error occurs during Json serialization or deserialization.
	/// </summary>
	// Token: 0x02000049 RID: 73
	public class JsonSerializationException : JsonException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonSerializationException" /> class.
		/// </summary>
		// Token: 0x0600029E RID: 670 RVA: 0x00009EF0 File Offset: 0x000080F0
		public JsonSerializationException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonSerializationException" /> class
		/// with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600029F RID: 671 RVA: 0x00009EF8 File Offset: 0x000080F8
		public JsonSerializationException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonSerializationException" /> class
		/// with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060002A0 RID: 672 RVA: 0x00009F01 File Offset: 0x00008101
		public JsonSerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009F0B File Offset: 0x0000810B
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009F15 File Offset: 0x00008115
		internal static JsonSerializationException Create(JsonReader reader, string message, Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009F2A File Offset: 0x0000812A
		internal static JsonSerializationException Create(IJsonLineInfo lineInfo, string path, string message, Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonSerializationException(message, ex);
		}
	}
}

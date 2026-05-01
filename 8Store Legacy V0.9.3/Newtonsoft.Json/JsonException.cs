using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// The exception thrown when an error occurs during Json serialization or deserialization.
	/// </summary>
	// Token: 0x02000041 RID: 65
	public class JsonException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonException" /> class.
		/// </summary>
		// Token: 0x06000263 RID: 611 RVA: 0x00009961 File Offset: 0x00007B61
		public JsonException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonException" /> class
		/// with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000264 RID: 612 RVA: 0x00009969 File Offset: 0x00007B69
		public JsonException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonException" /> class
		/// with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x06000265 RID: 613 RVA: 0x00009972 File Offset: 0x00007B72
		public JsonException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000997C File Offset: 0x00007B7C
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}

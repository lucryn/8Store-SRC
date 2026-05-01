using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// The exception thrown when an error occurs while reading Json text.
	/// </summary>
	// Token: 0x02000052 RID: 82
	public class JsonWriterException : JsonException
	{
		/// <summary>
		/// Gets the path to the JSON where the error occurred.
		/// </summary>
		/// <value>The path to the JSON where the error occurred.</value>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000F16C File Offset: 0x0000D36C
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000F174 File Offset: 0x0000D374
		public string Path { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonWriterException" /> class.
		/// </summary>
		// Token: 0x060003E0 RID: 992 RVA: 0x0000F17D File Offset: 0x0000D37D
		public JsonWriterException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonWriterException" /> class
		/// with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060003E1 RID: 993 RVA: 0x0000F185 File Offset: 0x0000D385
		public JsonWriterException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonWriterException" /> class
		/// with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000F18E File Offset: 0x0000D38E
		public JsonWriterException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F198 File Offset: 0x0000D398
		internal JsonWriterException(string message, Exception innerException, string path) : base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F1A9 File Offset: 0x0000D3A9
		internal static JsonWriterException Create(JsonWriter writer, string message, Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		internal static JsonWriterException Create(string path, string message, Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, ex, path);
		}
	}
}

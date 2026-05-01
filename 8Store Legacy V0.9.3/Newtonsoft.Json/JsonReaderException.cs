using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// The exception thrown when an error occurs while reading Json text.
	/// </summary>
	// Token: 0x02000048 RID: 72
	public class JsonReaderException : JsonException
	{
		/// <summary>
		/// Gets the line number indicating where the error occurred.
		/// </summary>
		/// <value>The line number indicating where the error occurred.</value>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009E1F File Offset: 0x0000801F
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00009E27 File Offset: 0x00008027
		public int LineNumber { get; private set; }

		/// <summary>
		/// Gets the line position indicating where the error occurred.
		/// </summary>
		/// <value>The line position indicating where the error occurred.</value>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009E30 File Offset: 0x00008030
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00009E38 File Offset: 0x00008038
		public int LinePosition { get; private set; }

		/// <summary>
		/// Gets the path to the JSON where the error occurred.
		/// </summary>
		/// <value>The path to the JSON where the error occurred.</value>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00009E41 File Offset: 0x00008041
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00009E49 File Offset: 0x00008049
		public string Path { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonReaderException" /> class.
		/// </summary>
		// Token: 0x06000297 RID: 663 RVA: 0x00009E52 File Offset: 0x00008052
		public JsonReaderException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonReaderException" /> class
		/// with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000298 RID: 664 RVA: 0x00009E5A File Offset: 0x0000805A
		public JsonReaderException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonReaderException" /> class
		/// with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x06000299 RID: 665 RVA: 0x00009E63 File Offset: 0x00008063
		public JsonReaderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009E6D File Offset: 0x0000806D
		internal JsonReaderException(string message, Exception innerException, string path, int lineNumber, int linePosition) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009E8E File Offset: 0x0000808E
		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return JsonReaderException.Create(reader, message, null);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009E98 File Offset: 0x00008098
		internal static JsonReaderException Create(JsonReader reader, string message, Exception ex)
		{
			return JsonReaderException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009EB0 File Offset: 0x000080B0
		internal static JsonReaderException Create(IJsonLineInfo lineInfo, string path, string message, Exception ex)
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
			return new JsonReaderException(message, ex, path, lineNumber, linePosition);
		}
	}
}

using System;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// Returns detailed information about the schema exception.
	/// </summary>
	// Token: 0x02000073 RID: 115
	public class JsonSchemaException : JsonException
	{
		/// <summary>
		/// Gets the line number indicating where the error occurred.
		/// </summary>
		/// <value>The line number indicating where the error occurred.</value>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001727D File Offset: 0x0001547D
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00017285 File Offset: 0x00015485
		public int LineNumber { get; private set; }

		/// <summary>
		/// Gets the line position indicating where the error occurred.
		/// </summary>
		/// <value>The line position indicating where the error occurred.</value>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001728E File Offset: 0x0001548E
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00017296 File Offset: 0x00015496
		public int LinePosition { get; private set; }

		/// <summary>
		/// Gets the path to the JSON where the error occurred.
		/// </summary>
		/// <value>The path to the JSON where the error occurred.</value>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001729F File Offset: 0x0001549F
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x000172A7 File Offset: 0x000154A7
		public string Path { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Schema.JsonSchemaException" /> class.
		/// </summary>
		// Token: 0x06000642 RID: 1602 RVA: 0x000172B0 File Offset: 0x000154B0
		public JsonSchemaException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Schema.JsonSchemaException" /> class
		/// with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000643 RID: 1603 RVA: 0x000172B8 File Offset: 0x000154B8
		public JsonSchemaException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Schema.JsonSchemaException" /> class
		/// with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x06000644 RID: 1604 RVA: 0x000172C1 File Offset: 0x000154C1
		public JsonSchemaException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000172CB File Offset: 0x000154CB
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}

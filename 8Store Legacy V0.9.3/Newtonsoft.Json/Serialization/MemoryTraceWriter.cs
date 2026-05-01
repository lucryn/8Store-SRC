using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Represents a trace writer that writes to memory. When the trace message limit is
	/// reached then old trace messages will be removed as new messages are added.
	/// </summary>
	// Token: 0x020000A8 RID: 168
	public class MemoryTraceWriter : ITraceWriter
	{
		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.TraceLevel" /> that will be used to filter the trace messages passed to the writer.
		/// For example a filter level of <code>Info</code> will exclude <code>Verbose</code> messages and include <code>Info</code>,
		/// <code>Warning</code> and <code>Error</code> messages.
		/// </summary>
		/// <value>
		/// The <see cref="T:Newtonsoft.Json.TraceLevel" /> that will be used to filter the trace messages passed to the writer.
		/// </value>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x00020EF8 File Offset: 0x0001F0F8
		public TraceLevel LevelFilter { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.MemoryTraceWriter" /> class.
		/// </summary>
		// Token: 0x06000896 RID: 2198 RVA: 0x00020F01 File Offset: 0x0001F101
		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
		}

		/// <summary>
		/// Writes the specified trace level, message and optional exception.
		/// </summary>
		/// <param name="level">The <see cref="T:Newtonsoft.Json.TraceLevel" /> at which to write this trace.</param>
		/// <param name="message">The trace message.</param>
		/// <param name="ex">The trace exception. This parameter is optional.</param>
		// Token: 0x06000897 RID: 2199 RVA: 0x00020F1C File Offset: 0x0001F11C
		public void Trace(TraceLevel level, string message, Exception ex)
		{
			string text = string.Concat(new string[]
			{
				DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture),
				" ",
				level.ToString("g"),
				" ",
				message
			});
			if (this._traceMessages.Count >= 1000)
			{
				this._traceMessages.Dequeue();
			}
			this._traceMessages.Enqueue(text);
		}

		/// <summary>
		/// Returns an enumeration of the most recent trace messages.
		/// </summary>
		/// <returns>An enumeration of the most recent trace messages.</returns>
		// Token: 0x06000898 RID: 2200 RVA: 0x00020FA2 File Offset: 0x0001F1A2
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> of the most recent trace messages.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> of the most recent trace messages.
		/// </returns>
		// Token: 0x06000899 RID: 2201 RVA: 0x00020FAC File Offset: 0x0001F1AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this._traceMessages)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400030F RID: 783
		private readonly Queue<string> _traceMessages;
	}
}

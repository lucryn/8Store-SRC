using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Represents a trace writer.
	/// </summary>
	// Token: 0x0200008E RID: 142
	public interface ITraceWriter
	{
		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.TraceLevel" /> that will be used to filter the trace messages passed to the writer.
		/// For example a filter level of <code>Info</code> will exclude <code>Verbose</code> messages and include <code>Info</code>,
		/// <code>Warning</code> and <code>Error</code> messages.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.TraceLevel" /> that will be used to filter the trace messages passed to the writer.</value>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000729 RID: 1833
		TraceLevel LevelFilter { get; }

		/// <summary>
		/// Writes the specified trace level, message and optional exception.
		/// </summary>
		/// <param name="level">The <see cref="T:Newtonsoft.Json.TraceLevel" /> at which to write this trace.</param>
		/// <param name="message">The trace message.</param>
		/// <param name="ex">The trace exception. This parameter is optional.</param>
		// Token: 0x0600072A RID: 1834
		void Trace(TraceLevel level, string message, Exception ex);
	}
}

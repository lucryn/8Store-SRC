using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies what messages to output for the <see cref="T:Newtonsoft.Json.Serialization.ITraceWriter" /> class.
	/// </summary>
	// Token: 0x020000AC RID: 172
	public enum TraceLevel
	{
		/// <summary>
		/// Output no tracing and debugging messages.
		/// </summary>
		// Token: 0x0400031C RID: 796
		Off,
		/// <summary>
		/// Output error-handling messages.
		/// </summary>
		// Token: 0x0400031D RID: 797
		Error,
		/// <summary>
		/// Output warnings and error-handling messages.
		/// </summary>
		// Token: 0x0400031E RID: 798
		Warning,
		/// <summary>
		/// Output informational messages, warnings, and error-handling messages.
		/// </summary>
		// Token: 0x0400031F RID: 799
		Info,
		/// <summary>
		/// Output all debugging and tracing messages.
		/// </summary>
		// Token: 0x04000320 RID: 800
		Verbose
	}
}

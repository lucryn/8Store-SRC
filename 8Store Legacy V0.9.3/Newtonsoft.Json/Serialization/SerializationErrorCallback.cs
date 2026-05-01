using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Handles <see cref="T:Newtonsoft.Json.JsonSerializer" /> serialization error callback events.
	/// </summary>
	/// <param name="o">The object that raised the callback event.</param>
	/// <param name="context">The streaming context.</param>
	/// <param name="errorContext">The error context.</param>
	// Token: 0x02000094 RID: 148
	// (Invoke) Token: 0x0600076E RID: 1902
	public delegate void SerializationErrorCallback(object o, StreamingContext context, ErrorContext errorContext);
}

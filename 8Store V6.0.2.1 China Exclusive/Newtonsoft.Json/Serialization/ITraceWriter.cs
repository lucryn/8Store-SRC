using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008B RID: 139
	[NullableContext(1)]
	public interface ITraceWriter
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600069D RID: 1693
		TraceLevel LevelFilter { get; }

		// Token: 0x0600069E RID: 1694
		void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex);
	}
}

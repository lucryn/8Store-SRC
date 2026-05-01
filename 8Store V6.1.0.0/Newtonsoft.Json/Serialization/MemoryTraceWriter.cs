using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A3 RID: 163
	[NullableContext(1)]
	[Nullable(0)]
	public class MemoryTraceWriter : ITraceWriter
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00022BC2 File Offset: 0x00020DC2
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00022BCA File Offset: 0x00020DCA
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x0600081A RID: 2074 RVA: 0x00022BD3 File Offset: 0x00020DD3
		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
			this._lock = new object();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00022BF8 File Offset: 0x00020DF8
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
			stringBuilder.Append(" ");
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			string text = stringBuilder.ToString();
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._traceMessages.Count >= 1000)
				{
					this._traceMessages.Dequeue();
				}
				this._traceMessages.Enqueue(text);
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00022CBC File Offset: 0x00020EBC
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00022CC4 File Offset: 0x00020EC4
		public override string ToString()
		{
			object @lock = this._lock;
			string result;
			lock (@lock)
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
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x04000314 RID: 788
		private readonly Queue<string> _traceMessages;

		// Token: 0x04000315 RID: 789
		private readonly object _lock;
	}
}

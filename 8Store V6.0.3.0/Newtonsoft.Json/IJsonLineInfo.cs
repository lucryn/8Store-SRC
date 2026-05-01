using System;

namespace Newtonsoft.Json
{
	// Token: 0x0200001D RID: 29
	public interface IJsonLineInfo
	{
		// Token: 0x0600002F RID: 47
		bool HasLineInfo();

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48
		int LineNumber { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49
		int LinePosition { get; }
	}
}

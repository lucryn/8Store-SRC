using System;

namespace Callisto.OAuth
{
	// Token: 0x02000035 RID: 53
	public class WebPair
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000C76A File Offset: 0x0000A96A
		public WebPair(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000C780 File Offset: 0x0000A980
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000C788 File Offset: 0x0000A988
		public string Value { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000C791 File Offset: 0x0000A991
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000C799 File Offset: 0x0000A999
		public string Name { get; private set; }
	}
}

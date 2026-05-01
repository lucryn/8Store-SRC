using System;
using System.Collections.Generic;

namespace Callisto.OAuth
{
	// Token: 0x02000037 RID: 55
	public class WebParameterCollection : WebPairCollection
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public WebParameterCollection(IEnumerable<WebPair> parameters) : base(parameters)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
		public WebParameterCollection(NameValueCollection collection) : base(collection)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000C7BE File Offset: 0x0000A9BE
		public WebParameterCollection()
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		public WebParameterCollection(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000C7CF File Offset: 0x0000A9CF
		public WebParameterCollection(IDictionary<string, string> collection) : base(collection)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public override void Add(string name, string value)
		{
			WebParameter parameter = new WebParameter(name, value);
			base.Add(parameter);
		}
	}
}

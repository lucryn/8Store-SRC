using System;
using System.Collections.Generic;
using System.Linq;

namespace Callisto.OAuth
{
	// Token: 0x02000038 RID: 56
	public class NameValueCollection : List<KeyValuePair<string, string>>
	{
		// Token: 0x17000067 RID: 103
		public string this[int index]
		{
			get
			{
				return base[index].Value;
			}
		}

		// Token: 0x17000068 RID: 104
		public string this[string name]
		{
			get
			{
				return Enumerable.SingleOrDefault<KeyValuePair<string, string>>(this, (KeyValuePair<string, string> kv) => kv.Key.Equals(name)).Value;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C860 File Offset: 0x0000AA60
		public NameValueCollection()
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C868 File Offset: 0x0000AA68
		public NameValueCollection(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C871 File Offset: 0x0000AA71
		public void Add(string name, string value)
		{
			base.Add(new KeyValuePair<string, string>(name, value));
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000C889 File Offset: 0x0000AA89
		public IEnumerable<string> AllKeys
		{
			get
			{
				return Enumerable.Select<KeyValuePair<string, string>, string>(this, (KeyValuePair<string, string> pair) => pair.Key);
			}
		}
	}
}

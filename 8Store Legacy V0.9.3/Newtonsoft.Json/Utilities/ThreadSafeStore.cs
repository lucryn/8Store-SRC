using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D7 RID: 215
	internal class ThreadSafeStore<TKey, TValue>
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x000283ED File Offset: 0x000265ED
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			if (creator == null)
			{
				throw new ArgumentNullException("creator");
			}
			this._creator = creator;
			this._store = new Dictionary<TKey, TValue>();
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00028420 File Offset: 0x00026620
		public TValue Get(TKey key)
		{
			TValue result;
			if (!this._store.TryGetValue(key, ref result))
			{
				return this.AddValue(key);
			}
			return result;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00028448 File Offset: 0x00026648
		private TValue AddValue(TKey key)
		{
			TValue tvalue = this._creator.Invoke(key);
			TValue result2;
			lock (this._lock)
			{
				if (this._store == null)
				{
					this._store = new Dictionary<TKey, TValue>();
					this._store[key] = tvalue;
				}
				else
				{
					TValue result;
					if (this._store.TryGetValue(key, ref result))
					{
						return result;
					}
					Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(this._store);
					dictionary[key] = tvalue;
					this._store = dictionary;
				}
				result2 = tvalue;
			}
			return result2;
		}

		// Token: 0x040003E1 RID: 993
		private readonly object _lock = new object();

		// Token: 0x040003E2 RID: 994
		private Dictionary<TKey, TValue> _store;

		// Token: 0x040003E3 RID: 995
		private readonly Func<TKey, TValue> _creator;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	[Nullable(0)]
	internal class ThreadSafeStore<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00018A0F File Offset: 0x00016C0F
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			this._creator = creator;
			this._store = new Dictionary<TKey, TValue>();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00018A40 File Offset: 0x00016C40
		public TValue Get(TKey key)
		{
			TValue result;
			if (!this._store.TryGetValue(key, ref result))
			{
				return this.AddValue(key);
			}
			return result;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00018A68 File Offset: 0x00016C68
		private TValue AddValue(TKey key)
		{
			TValue tvalue = this._creator.Invoke(key);
			object @lock = this._lock;
			TValue result2;
			lock (@lock)
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

		// Token: 0x04000269 RID: 617
		private readonly object _lock = new object();

		// Token: 0x0400026A RID: 618
		private Dictionary<TKey, TValue> _store;

		// Token: 0x0400026B RID: 619
		private readonly Func<TKey, TValue> _creator;
	}
}

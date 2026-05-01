using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BC RID: 188
	internal class DictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IWrappedDictionary, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x000241E3 File Offset: 0x000223E3
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000241FD File Offset: 0x000223FD
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00024217 File Offset: 0x00022417
		public DictionaryWrapper(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._readOnlyDictionary = dictionary;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00024231 File Offset: 0x00022431
		public void Add(TKey key, TValue value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(key, value);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002426E File Offset: 0x0002246E
		public bool ContainsKey(TKey key)
		{
			if (this._dictionary != null)
			{
				return this._dictionary.Contains(key);
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey(key);
			}
			return this._genericDictionary.ContainsKey(key);
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000242AC File Offset: 0x000224AC
		public ICollection<TKey> Keys
		{
			get
			{
				if (this._dictionary != null)
				{
					return Enumerable.ToList<TKey>(Enumerable.Cast<TKey>(this._dictionary.Keys));
				}
				if (this._readOnlyDictionary != null)
				{
					return Enumerable.ToList<TKey>(this._readOnlyDictionary.Keys);
				}
				return this._genericDictionary.Keys;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000242FC File Offset: 0x000224FC
		public bool Remove(TKey key)
		{
			if (this._dictionary != null)
			{
				if (this._dictionary.Contains(key))
				{
					this._dictionary.Remove(key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this._genericDictionary.Remove(key);
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00024354 File Offset: 0x00022554
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(key))
				{
					value = default(TValue);
					return false;
				}
				value = (TValue)((object)this._dictionary[key]);
				return true;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this._genericDictionary.TryGetValue(key, ref value);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x000243C0 File Offset: 0x000225C0
		public ICollection<TValue> Values
		{
			get
			{
				if (this._dictionary != null)
				{
					return Enumerable.ToList<TValue>(Enumerable.Cast<TValue>(this._dictionary.Values));
				}
				if (this._readOnlyDictionary != null)
				{
					return Enumerable.ToList<TValue>(this._readOnlyDictionary.Values);
				}
				return this._genericDictionary.Values;
			}
		}

		// Token: 0x170001F3 RID: 499
		public TValue this[TKey key]
		{
			get
			{
				if (this._dictionary != null)
				{
					return (TValue)((object)this._dictionary[key]);
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[key];
				}
				return this._genericDictionary[key];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this._genericDictionary[key] = value;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002449C File Offset: 0x0002269C
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				((IList)this._dictionary).Add(item);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(item);
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000244EB File Offset: 0x000226EB
		public void Clear()
		{
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.Clear();
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002451C File Offset: 0x0002271C
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				return ((IList)this._dictionary).Contains(item);
			}
			if (this._readOnlyDictionary != null)
			{
				return Enumerable.Contains<KeyValuePair<TKey, TValue>>(this._readOnlyDictionary, item);
			}
			return this._genericDictionary.Contains(item);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002456C File Offset: 0x0002276C
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._dictionary != null)
			{
				using (IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
					}
					return;
				}
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00024610 File Offset: 0x00022810
		public int Count
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Count;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Count;
				}
				return this._genericDictionary.Count;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00024645 File Offset: 0x00022845
		public bool IsReadOnly
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.IsReadOnly;
				}
				return this._readOnlyDictionary != null || this._genericDictionary.IsReadOnly;
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00024670 File Offset: 0x00022870
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(item.Key))
				{
					return true;
				}
				object obj = this._dictionary[item.Key];
				if (object.Equals(obj, item.Value))
				{
					this._dictionary.Remove(item.Key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this._genericDictionary.Remove(item);
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00024720 File Offset: 0x00022920
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return Enumerable.Select<DictionaryEntry, KeyValuePair<TKey, TValue>>(Enumerable.Cast<DictionaryEntry>(this._dictionary), (DictionaryEntry de) => new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.GetEnumerator();
			}
			return this._genericDictionary.GetEnumerator();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00024787 File Offset: 0x00022987
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002478F File Offset: 0x0002298F
		void IDictionary.Add(object key, object value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x000247CC File Offset: 0x000229CC
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00024823 File Offset: 0x00022A23
		object IDictionary.Item
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[(TKey)((object)key)];
				}
				return this._genericDictionary[(TKey)((object)key)];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this._genericDictionary[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00024860 File Offset: 0x00022A60
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return this._dictionary.GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._readOnlyDictionary.GetEnumerator());
			}
			return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._genericDictionary.GetEnumerator());
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000248B4 File Offset: 0x00022AB4
		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00024901 File Offset: 0x00022B01
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && (this._readOnlyDictionary != null || this._dictionary.IsFixedSize);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00024922 File Offset: 0x00022B22
		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return Enumerable.ToList<TKey>(this._genericDictionary.Keys);
				}
				if (this._readOnlyDictionary != null)
				{
					return Enumerable.ToList<TKey>(this._readOnlyDictionary.Keys);
				}
				return this._dictionary.Keys;
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00024961 File Offset: 0x00022B61
		public void Remove(object key)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Remove(key);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.Remove((TKey)((object)key));
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00024998 File Offset: 0x00022B98
		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return Enumerable.ToList<TValue>(this._genericDictionary.Values);
				}
				if (this._readOnlyDictionary != null)
				{
					return Enumerable.ToList<TValue>(this._readOnlyDictionary.Values);
				}
				return this._dictionary.Values;
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000249D7 File Offset: 0x00022BD7
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._dictionary != null)
			{
				this._dictionary.CopyTo(array, index);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00024A0F File Offset: 0x00022C0F
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._dictionary != null && this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00024A26 File Offset: 0x00022C26
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00024A48 File Offset: 0x00022C48
		public object UnderlyingDictionary
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary;
				}
				return this._genericDictionary;
			}
		}

		// Token: 0x04000395 RID: 917
		private readonly IDictionary _dictionary;

		// Token: 0x04000396 RID: 918
		private readonly IDictionary<TKey, TValue> _genericDictionary;

		// Token: 0x04000397 RID: 919
		private readonly IReadOnlyDictionary<TKey, TValue> _readOnlyDictionary;

		// Token: 0x04000398 RID: 920
		private object _syncRoot;

		// Token: 0x020000BD RID: 189
		private struct DictionaryEnumerator<TEnumeratorKey, TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000966 RID: 2406 RVA: 0x00024A6E File Offset: 0x00022C6E
			public DictionaryEnumerator(IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06000967 RID: 2407 RVA: 0x00024A82 File Offset: 0x00022C82
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000968 RID: 2408 RVA: 0x00024A90 File Offset: 0x00022C90
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000969 RID: 2409 RVA: 0x00024AAC File Offset: 0x00022CAC
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x0600096A RID: 2410 RVA: 0x00024AC8 File Offset: 0x00022CC8
			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object obj = keyValuePair.Key;
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair2 = this._e.Current;
					return new DictionaryEntry(obj, keyValuePair2.Value);
				}
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x00024B0F File Offset: 0x00022D0F
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x0600096C RID: 2412 RVA: 0x00024B1C File Offset: 0x00022D1C
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x0400039A RID: 922
			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}

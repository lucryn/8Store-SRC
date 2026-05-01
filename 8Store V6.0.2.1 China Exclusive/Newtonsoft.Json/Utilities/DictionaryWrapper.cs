using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000059 RID: 89
	[NullableContext(1)]
	[Nullable(0)]
	internal class DictionaryWrapper<[Nullable(2)] TKey, [Nullable(2)] TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IWrappedDictionary, IDictionary, ICollection
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x000133C1 File Offset: 0x000115C1
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000133DB File Offset: 0x000115DB
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000133F5 File Offset: 0x000115F5
		public DictionaryWrapper(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._readOnlyDictionary = dictionary;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001340F File Offset: 0x0001160F
		internal IDictionary<TKey, TValue> GenericDictionary
		{
			get
			{
				return this._genericDictionary;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00013417 File Offset: 0x00011617
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

		// Token: 0x060004BD RID: 1213 RVA: 0x00013454 File Offset: 0x00011654
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
			return this.GenericDictionary.ContainsKey(key);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00013494 File Offset: 0x00011694
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
				return this.GenericDictionary.Keys;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000134E4 File Offset: 0x000116E4
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
				return this.GenericDictionary.Remove(key);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001353C File Offset: 0x0001173C
		public bool TryGetValue(TKey key, [Nullable(2)] out TValue value)
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
				return this.GenericDictionary.TryGetValue(key, ref value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000135A8 File Offset: 0x000117A8
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
				return this.GenericDictionary.Values;
			}
		}

		// Token: 0x170000BF RID: 191
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
				return this.GenericDictionary[key];
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
				this.GenericDictionary[key] = value;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013684 File Offset: 0x00011884
		public void Add([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
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
			IDictionary<TKey, TValue> genericDictionary = this._genericDictionary;
			if (genericDictionary == null)
			{
				return;
			}
			genericDictionary.Add(item);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000136D0 File Offset: 0x000118D0
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
			this.GenericDictionary.Clear();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00013700 File Offset: 0x00011900
		public bool Contains([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				return ((IList)this._dictionary).Contains(item);
			}
			if (this._readOnlyDictionary != null)
			{
				return Enumerable.Contains<KeyValuePair<TKey, TValue>>(this._readOnlyDictionary, item);
			}
			return this.GenericDictionary.Contains(item);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00013750 File Offset: 0x00011950
		public void CopyTo([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._dictionary != null)
			{
				using (IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry entry = enumerator.Entry;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value));
					}
					return;
				}
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000137EC File Offset: 0x000119EC
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
				return this.GenericDictionary.Count;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00013821 File Offset: 0x00011A21
		public bool IsReadOnly
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.IsReadOnly;
				}
				return this._readOnlyDictionary != null || this.GenericDictionary.IsReadOnly;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001384C File Offset: 0x00011A4C
		public bool Remove([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(item.Key))
				{
					return true;
				}
				if (object.Equals(this._dictionary[item.Key], item.Value))
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
				return this.GenericDictionary.Remove(item);
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000138DC File Offset: 0x00011ADC
		[return: Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})]
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
			return this.GenericDictionary.GetEnumerator();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00013945 File Offset: 0x00011B45
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001394D File Offset: 0x00011B4D
		void IDictionary.Add(object key, [Nullable(2)] object value)
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
			this.GenericDictionary.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001398C File Offset: 0x00011B8C
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x000139E3 File Offset: 0x00011BE3
		[Nullable(2)]
		object IDictionary.Item
		{
			[return: Nullable(2)]
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
				return this.GenericDictionary[(TKey)((object)key)];
			}
			[param: Nullable(2)]
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
				this.GenericDictionary[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013A20 File Offset: 0x00011C20
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
			return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this.GenericDictionary.GetEnumerator());
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013A74 File Offset: 0x00011C74
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00013AC1 File Offset: 0x00011CC1
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && (this._readOnlyDictionary != null || this._dictionary.IsFixedSize);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00013AE2 File Offset: 0x00011CE2
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

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013B21 File Offset: 0x00011D21
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
			this.GenericDictionary.Remove((TKey)((object)key));
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00013B58 File Offset: 0x00011D58
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

		// Token: 0x060004D6 RID: 1238 RVA: 0x00013B97 File Offset: 0x00011D97
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
			this.GenericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00013BCF File Offset: 0x00011DCF
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._dictionary != null && this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00013BE6 File Offset: 0x00011DE6
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00013C08 File Offset: 0x00011E08
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
				return this.GenericDictionary;
			}
		}

		// Token: 0x040001F8 RID: 504
		[Nullable(2)]
		private readonly IDictionary _dictionary;

		// Token: 0x040001F9 RID: 505
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private readonly IDictionary<TKey, TValue> _genericDictionary;

		// Token: 0x040001FA RID: 506
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private readonly IReadOnlyDictionary<TKey, TValue> _readOnlyDictionary;

		// Token: 0x040001FB RID: 507
		[Nullable(2)]
		private object _syncRoot;

		// Token: 0x02000169 RID: 361
		[Nullable(0)]
		private readonly struct DictionaryEnumerator<[Nullable(2)] TEnumeratorKey, [Nullable(2)] TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000E19 RID: 3609 RVA: 0x0003EFFF File Offset: 0x0003D1FF
			public DictionaryEnumerator([Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0003F013 File Offset: 0x0003D213
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0003F020 File Offset: 0x0003D220
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0003F03C File Offset: 0x0003D23C
			[Nullable(2)]
			public object Value
			{
				[NullableContext(2)]
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0003F058 File Offset: 0x0003D258
			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this._e.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x06000E1E RID: 3614 RVA: 0x0003F09F File Offset: 0x0003D29F
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x0003F0AC File Offset: 0x0003D2AC
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x040006C6 RID: 1734
			[Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})]
			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}

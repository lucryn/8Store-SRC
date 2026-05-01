using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000055 RID: 85
	public class ObservableDictionary : IObservableMap<string, object>, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004E2 RID: 1250 RVA: 0x0001B91C File Offset: 0x00019B1C
		// (remove) Token: 0x060004E3 RID: 1251 RVA: 0x0001B92F File Offset: 0x00019B2F
		public event MapChangedEventHandler<string, object> MapChanged
		{
			[CompilerGenerated]
			add
			{
				return EventRegistrationTokenTable<MapChangedEventHandler<string, object>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged).AddEventHandler(value);
			}
			[CompilerGenerated]
			remove
			{
				EventRegistrationTokenTable<MapChangedEventHandler<string, object>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged).RemoveEventHandler(value);
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001B944 File Offset: 0x00019B44
		private void InvokeMapChanged(CollectionChange change, string key)
		{
			MapChangedEventHandler<string, object> invocationList = EventRegistrationTokenTable<MapChangedEventHandler<string, object>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged).InvocationList;
			bool flag = invocationList != null;
			if (flag)
			{
				invocationList.Invoke(this, new ObservableDictionary.ObservableDictionaryChangedEventArgs(change, key));
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001B97C File Offset: 0x00019B7C
		public void Add(string key, object value)
		{
			this._dictionary.Add(key, value);
			this.InvokeMapChanged(1, key);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001B996 File Offset: 0x00019B96
		public void Add(KeyValuePair<string, object> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		public bool Remove(string key)
		{
			bool flag = this._dictionary.Remove(key);
			bool result;
			if (flag)
			{
				this.InvokeMapChanged(2, key);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		public bool Remove(KeyValuePair<string, object> item)
		{
			object obj;
			bool flag = this._dictionary.TryGetValue(item.Key, ref obj) && object.Equals(item.Value, obj) && this._dictionary.Remove(item.Key);
			bool result;
			if (flag)
			{
				this.InvokeMapChanged(2, item.Key);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170000F2 RID: 242
		public object this[string key]
		{
			get
			{
				return this._dictionary[key];
			}
			set
			{
				this._dictionary[key] = value;
				this.InvokeMapChanged(3, key);
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001BA84 File Offset: 0x00019C84
		public void Clear()
		{
			string[] array = Enumerable.ToArray<string>(this._dictionary.Keys);
			this._dictionary.Clear();
			foreach (string key in array)
			{
				this.InvokeMapChanged(2, key);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		public ICollection<string> Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001BAF0 File Offset: 0x00019CF0
		public bool ContainsKey(string key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001BB10 File Offset: 0x00019D10
		public bool TryGetValue(string key, out object value)
		{
			return this._dictionary.TryGetValue(key, ref value);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001BB30 File Offset: 0x00019D30
		public ICollection<object> Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001BB50 File Offset: 0x00019D50
		public bool Contains(KeyValuePair<string, object> item)
		{
			return Enumerable.Contains<KeyValuePair<string, object>>(this._dictionary, item);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001BB70 File Offset: 0x00019D70
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001BB90 File Offset: 0x00019D90
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001BBC8 File Offset: 0x00019DC8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001BBEC File Offset: 0x00019DEC
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			int num = array.Length;
			foreach (KeyValuePair<string, object> keyValuePair in this._dictionary)
			{
				bool flag = arrayIndex >= num;
				if (flag)
				{
					break;
				}
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x0400022A RID: 554
		private Dictionary<string, object> _dictionary = new Dictionary<string, object>();

		// Token: 0x02000155 RID: 341
		private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<string>
		{
			// Token: 0x06000812 RID: 2066 RVA: 0x0003DC5C File Offset: 0x0003BE5C
			public ObservableDictionaryChangedEventArgs(CollectionChange change, string key)
			{
				this.CollectionChange = change;
				this.Key = key;
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000813 RID: 2067 RVA: 0x0003DC76 File Offset: 0x0003BE76
			// (set) Token: 0x06000814 RID: 2068 RVA: 0x0003DC7E File Offset: 0x0003BE7E
			public CollectionChange CollectionChange { get; private set; }

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000815 RID: 2069 RVA: 0x0003DC87 File Offset: 0x0003BE87
			// (set) Token: 0x06000816 RID: 2070 RVA: 0x0003DC8F File Offset: 0x0003BE8F
			public string Key { get; private set; }
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000061 RID: 97
	public class ObservableDictionary : IObservableMap<string, object>, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600062F RID: 1583 RVA: 0x0001D594 File Offset: 0x0001B794
		// (remove) Token: 0x06000630 RID: 1584 RVA: 0x0001D5A7 File Offset: 0x0001B7A7
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

		// Token: 0x06000631 RID: 1585 RVA: 0x0001D5BC File Offset: 0x0001B7BC
		private void InvokeMapChanged(CollectionChange change, string key)
		{
			MapChangedEventHandler<string, object> invocationList = EventRegistrationTokenTable<MapChangedEventHandler<string, object>>.GetOrCreateEventRegistrationTokenTable(ref this.MapChanged).InvocationList;
			if (invocationList != null)
			{
				invocationList.Invoke(this, new ObservableDictionary.ObservableDictionaryChangedEventArgs(change, key));
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001D5EB File Offset: 0x0001B7EB
		public void Add(string key, object value)
		{
			this._dictionary.Add(key, value);
			this.InvokeMapChanged(1, key);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001D602 File Offset: 0x0001B802
		public void Add(KeyValuePair<string, object> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001D618 File Offset: 0x0001B818
		public bool Remove(string key)
		{
			if (this._dictionary.Remove(key))
			{
				this.InvokeMapChanged(2, key);
				return true;
			}
			return false;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001D634 File Offset: 0x0001B834
		public bool Remove(KeyValuePair<string, object> item)
		{
			object obj;
			if (this._dictionary.TryGetValue(item.Key, ref obj) && object.Equals(item.Value, obj) && this._dictionary.Remove(item.Key))
			{
				this.InvokeMapChanged(2, item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x17000134 RID: 308
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

		// Token: 0x06000638 RID: 1592 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		public void Clear()
		{
			string[] array = Enumerable.ToArray<string>(this._dictionary.Keys);
			this._dictionary.Clear();
			foreach (string key in array)
			{
				this.InvokeMapChanged(2, key);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001D6F3 File Offset: 0x0001B8F3
		public ICollection<string> Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001D700 File Offset: 0x0001B900
		public bool ContainsKey(string key)
		{
			return this._dictionary.ContainsKey(key);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001D70E File Offset: 0x0001B90E
		public bool TryGetValue(string key, out object value)
		{
			return this._dictionary.TryGetValue(key, ref value);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001D71D File Offset: 0x0001B91D
		public ICollection<object> Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001D72A File Offset: 0x0001B92A
		public bool Contains(KeyValuePair<string, object> item)
		{
			return Enumerable.Contains<KeyValuePair<string, object>>(this._dictionary, item);
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001D738 File Offset: 0x0001B938
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001CE2D File Offset: 0x0001B02D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001D745 File Offset: 0x0001B945
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001D745 File Offset: 0x0001B945
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001D758 File Offset: 0x0001B958
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			int num = array.Length;
			foreach (KeyValuePair<string, object> keyValuePair in this._dictionary)
			{
				if (arrayIndex >= num)
				{
					break;
				}
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x04000261 RID: 609
		private Dictionary<string, object> _dictionary = new Dictionary<string, object>();

		// Token: 0x0200016C RID: 364
		private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<string>
		{
			// Token: 0x06000923 RID: 2339 RVA: 0x0003A552 File Offset: 0x00038752
			public ObservableDictionaryChangedEventArgs(CollectionChange change, string key)
			{
				this.CollectionChange = change;
				this.Key = key;
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000924 RID: 2340 RVA: 0x0003A568 File Offset: 0x00038768
			// (set) Token: 0x06000925 RID: 2341 RVA: 0x0003A570 File Offset: 0x00038770
			public CollectionChange CollectionChange { get; private set; }

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x06000926 RID: 2342 RVA: 0x0003A579 File Offset: 0x00038779
			// (set) Token: 0x06000927 RID: 2343 RVA: 0x0003A581 File Offset: 0x00038781
			public string Key { get; private set; }
		}
	}
}

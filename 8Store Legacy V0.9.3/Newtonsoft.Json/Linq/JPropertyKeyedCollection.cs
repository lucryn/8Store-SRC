using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000060 RID: 96
	internal class JPropertyKeyedCollection : Collection<JToken>
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x000141BC File Offset: 0x000123BC
		private void AddKey(string key, JToken item)
		{
			this.EnsureDictionary();
			this._dictionary[key] = item;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000141D4 File Offset: 0x000123D4
		protected void ChangeItemKey(JToken item, string newKey)
		{
			if (!this.ContainsItem(item))
			{
				throw new ArgumentException("The specified item does not exist in this KeyedCollection.");
			}
			string keyForItem = this.GetKeyForItem(item);
			if (!JPropertyKeyedCollection.Comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00014220 File Offset: 0x00012420
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001423B File Offset: 0x0001243B
		public bool Contains(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._dictionary != null && this._dictionary.ContainsKey(key);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00014264 File Offset: 0x00012464
		private bool ContainsItem(JToken item)
		{
			if (this._dictionary == null)
			{
				return false;
			}
			string keyForItem = this.GetKeyForItem(item);
			JToken jtoken;
			return this._dictionary.TryGetValue(keyForItem, ref jtoken);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00014291 File Offset: 0x00012491
		private void EnsureDictionary()
		{
			if (this._dictionary == null)
			{
				this._dictionary = new Dictionary<string, JToken>(JPropertyKeyedCollection.Comparer);
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000142AB File Offset: 0x000124AB
		private string GetKeyForItem(JToken item)
		{
			return ((JProperty)item).Name;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000142B8 File Offset: 0x000124B8
		protected override void InsertItem(int index, JToken item)
		{
			this.AddKey(this.GetKeyForItem(item), item);
			base.InsertItem(index, item);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000142D0 File Offset: 0x000124D0
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._dictionary != null && this._dictionary.ContainsKey(key) && base.Remove(this._dictionary[key]);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001430C File Offset: 0x0001250C
		protected override void RemoveItem(int index)
		{
			string keyForItem = this.GetKeyForItem(base.Items[index]);
			this.RemoveKey(keyForItem);
			base.RemoveItem(index);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001433A File Offset: 0x0001253A
		private void RemoveKey(string key)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Remove(key);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00014354 File Offset: 0x00012554
		protected override void SetItem(int index, JToken item)
		{
			string keyForItem = this.GetKeyForItem(item);
			string keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (JPropertyKeyedCollection.Comparer.Equals(keyForItem2, keyForItem))
			{
				if (this._dictionary != null)
				{
					this._dictionary[keyForItem] = item;
				}
			}
			else
			{
				this.AddKey(keyForItem, item);
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x1700010E RID: 270
		public JToken this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000143E5 File Offset: 0x000125E5
		public bool TryGetValue(string key, out JToken value)
		{
			if (this._dictionary == null)
			{
				value = null;
				return false;
			}
			return this._dictionary.TryGetValue(key, ref value);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00014401 File Offset: 0x00012601
		public ICollection<string> Keys
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Keys;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00014414 File Offset: 0x00012614
		public ICollection<JToken> Values
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Values;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00014428 File Offset: 0x00012628
		public bool Compare(JPropertyKeyedCollection other)
		{
			if (this == other)
			{
				return true;
			}
			Dictionary<string, JToken> dictionary = this._dictionary;
			Dictionary<string, JToken> dictionary2 = other._dictionary;
			if (dictionary == null && dictionary2 == null)
			{
				return true;
			}
			if (dictionary == null)
			{
				return dictionary2.Count == 0;
			}
			if (dictionary2 == null)
			{
				return dictionary.Count == 0;
			}
			if (dictionary.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in dictionary)
			{
				JToken jtoken;
				if (!dictionary2.TryGetValue(keyValuePair.Key, ref jtoken))
				{
					return false;
				}
				JProperty jproperty = (JProperty)keyValuePair.Value;
				JProperty jproperty2 = (JProperty)jtoken;
				if (!jproperty.Value.DeepEquals(jproperty2.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040001A7 RID: 423
		private static readonly IEqualityComparer<string> Comparer = StringComparer.Ordinal;

		// Token: 0x040001A8 RID: 424
		private Dictionary<string, JToken> _dictionary;
	}
}

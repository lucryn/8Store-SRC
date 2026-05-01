using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Callisto.OAuth
{
	// Token: 0x02000034 RID: 52
	public class WebPairCollection : IList<WebPair>, ICollection<WebPair>, IEnumerable<WebPair>, IEnumerable
	{
		// Token: 0x1700005F RID: 95
		public virtual WebPair this[string name]
		{
			get
			{
				IEnumerable<WebPair> enumerable = Enumerable.Where<WebPair>(this, (WebPair p) => p.Name.Equals(name));
				if (Enumerable.Count<WebPair>(enumerable) == 0)
				{
					return null;
				}
				if (Enumerable.Count<WebPair>(enumerable) == 1)
				{
					return Enumerable.Single<WebPair>(enumerable);
				}
				string value = string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<WebPair, string>(enumerable, (WebPair p) => p.Value)));
				return new WebPair(name, value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000C3C6 File Offset: 0x0000A5C6
		public virtual IEnumerable<string> Names
		{
			get
			{
				return Enumerable.Select<WebPair, string>(this._parameters, (WebPair p) => p.Name);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public virtual IEnumerable<string> Values
		{
			get
			{
				return Enumerable.Select<WebPair, string>(this._parameters, (WebPair p) => p.Value);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C422 File Offset: 0x0000A622
		public WebPairCollection(IEnumerable<WebPair> parameters)
		{
			this._parameters = new List<WebPair>(parameters);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C436 File Offset: 0x0000A636
		public WebPairCollection(NameValueCollection collection) : this()
		{
			this.AddCollection(collection);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C445 File Offset: 0x0000A645
		public virtual void AddRange(NameValueCollection collection)
		{
			this.AddCollection(collection);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C46C File Offset: 0x0000A66C
		private void AddCollection(NameValueCollection collection)
		{
			IEnumerable<WebPair> enumerable = Enumerable.Select<string, WebPair>(collection.AllKeys, (string key) => new WebPair(key, collection[key]));
			foreach (WebPair webPair in enumerable)
			{
				this._parameters.Add(webPair);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		public WebPairCollection(IDictionary<string, string> collection) : this()
		{
			this.AddCollection(collection);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C510 File Offset: 0x0000A710
		public void AddCollection(IDictionary<string, string> collection)
		{
			foreach (WebPair webPair in Enumerable.Select<string, WebPair>(collection.Keys, (string key) => new WebPair(key, collection[key])))
			{
				this._parameters.Add(webPair);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000C588 File Offset: 0x0000A788
		public WebPairCollection()
		{
			this._parameters = new List<WebPair>(0);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C59C File Offset: 0x0000A79C
		public WebPairCollection(int capacity)
		{
			this._parameters = new List<WebPair>(capacity);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		private void AddCollection(IEnumerable<WebPair> collection)
		{
			foreach (WebPair webPair in Enumerable.Select<WebPair, WebPair>(collection, (WebPair parameter) => new WebPair(parameter.Name, parameter.Value)))
			{
				this._parameters.Add(webPair);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C634 File Offset: 0x0000A834
		public virtual void AddRange(WebPairCollection collection)
		{
			this.AddCollection(collection);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C63D File Offset: 0x0000A83D
		public virtual void AddRange(IEnumerable<WebPair> collection)
		{
			this.AddCollection(collection);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C658 File Offset: 0x0000A858
		public virtual bool RemoveAll(IEnumerable<WebPair> parameters)
		{
			WebPair[] array = Enumerable.ToArray<WebPair>(parameters);
			bool flag = Enumerable.Aggregate<WebPair, bool>(array, true, (bool current, WebPair parameter) => current & this._parameters.Remove(parameter));
			return flag && array.Length > 0;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C68C File Offset: 0x0000A88C
		public virtual void Add(string name, string value)
		{
			WebPair webPair = new WebPair(name, value);
			this._parameters.Add(webPair);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C6AD File Offset: 0x0000A8AD
		public virtual IEnumerator<WebPair> GetEnumerator()
		{
			return this._parameters.GetEnumerator();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C6BA File Offset: 0x0000A8BA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		public virtual void Add(WebPair parameter)
		{
			this._parameters.Add(parameter);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		public virtual void Clear()
		{
			this._parameters.Clear();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C6DD File Offset: 0x0000A8DD
		public virtual bool Contains(WebPair parameter)
		{
			return this._parameters.Contains(parameter);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000C6EB File Offset: 0x0000A8EB
		public virtual void CopyTo(WebPair[] parameters, int arrayIndex)
		{
			this._parameters.CopyTo(parameters, arrayIndex);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000C6FA File Offset: 0x0000A8FA
		public virtual bool Remove(WebPair parameter)
		{
			return this._parameters.Remove(parameter);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000C708 File Offset: 0x0000A908
		public virtual int Count
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000C715 File Offset: 0x0000A915
		public virtual bool IsReadOnly
		{
			get
			{
				return this._parameters.IsReadOnly;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C722 File Offset: 0x0000A922
		public virtual int IndexOf(WebPair parameter)
		{
			return this._parameters.IndexOf(parameter);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C730 File Offset: 0x0000A930
		public virtual void Insert(int index, WebPair parameter)
		{
			this._parameters.Insert(index, parameter);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C73F File Offset: 0x0000A93F
		public virtual void RemoveAt(int index)
		{
			this._parameters.RemoveAt(index);
		}

		// Token: 0x17000064 RID: 100
		public virtual WebPair this[int index]
		{
			get
			{
				return this._parameters[index];
			}
			set
			{
				this._parameters[index] = value;
			}
		}

		// Token: 0x04000119 RID: 281
		private IList<WebPair> _parameters;
	}
}

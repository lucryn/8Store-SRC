using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B2 RID: 178
	internal class CollectionWrapper<T> : ICollection<T>, IEnumerable<T>, IWrappedCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00021F5D File Offset: 0x0002015D
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			if (list is ICollection<T>)
			{
				this._genericCollection = (ICollection<T>)list;
				return;
			}
			this._list = list;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00021F8C File Offset: 0x0002018C
		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericCollection = list;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00021FA6 File Offset: 0x000201A6
		public virtual void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00021FCF File Offset: 0x000201CF
		public virtual void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00021FF0 File Offset: 0x000201F0
		public virtual bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00022018 File Offset: 0x00020218
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002203D File Offset: 0x0002023D
		public virtual int Count
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.Count;
				}
				return this._list.Count;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0002205E File Offset: 0x0002025E
		public virtual bool IsReadOnly
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00022080 File Offset: 0x00020280
		public virtual bool Remove(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Remove(item);
			}
			bool flag = this._list.Contains(item);
			if (flag)
			{
				this._list.Remove(item);
			}
			return flag;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000220C9 File Offset: 0x000202C9
		public virtual IEnumerator<T> GetEnumerator()
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.GetEnumerator();
			}
			return Enumerable.Cast<T>(this._list).GetEnumerator();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000220EF File Offset: 0x000202EF
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.GetEnumerator();
			}
			return this._list.GetEnumerator();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00022110 File Offset: 0x00020310
		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002212C File Offset: 0x0002032C
		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00022144 File Offset: 0x00020344
		int IList.IndexOf(object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support IndexOf.");
			}
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				return this._list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00022179 File Offset: 0x00020379
		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support RemoveAt.");
			}
			this._list.RemoveAt(index);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002219A File Offset: 0x0002039A
		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support Insert.");
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000221CC File Offset: 0x000203CC
		bool IList.IsFixedSize
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000221ED File Offset: 0x000203ED
		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00022204 File Offset: 0x00020404
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00022225 File Offset: 0x00020425
		object IList.Item
		{
			get
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				return this._list[index];
			}
			set
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				CollectionWrapper<T>.VerifyValueType(value);
				this._list[index] = (T)((object)value);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00022257 File Offset: 0x00020457
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00022266 File Offset: 0x00020466
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00022269 File Offset: 0x00020469
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

		// Token: 0x06000906 RID: 2310 RVA: 0x0002228B File Offset: 0x0002048B
		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), "value");
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000222BA File Offset: 0x000204BA
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType() || ReflectionUtils.IsNullableType(typeof(T))));
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x000222EC File Offset: 0x000204EC
		public object UnderlyingCollection
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection;
				}
				return this._list;
			}
		}

		// Token: 0x04000331 RID: 817
		private readonly IList _list;

		// Token: 0x04000332 RID: 818
		private readonly ICollection<T> _genericCollection;

		// Token: 0x04000333 RID: 819
		private object _syncRoot;
	}
}

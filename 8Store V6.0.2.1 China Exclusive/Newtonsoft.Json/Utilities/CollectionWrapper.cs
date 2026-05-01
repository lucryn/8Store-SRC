using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000050 RID: 80
	[NullableContext(1)]
	[Nullable(0)]
	internal class CollectionWrapper<[Nullable(2)] T> : ICollection<T>, IEnumerable<T>, IEnumerable, IWrappedCollection, IList, ICollection
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00010C38 File Offset: 0x0000EE38
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			ICollection<T> collection = list as ICollection<T>;
			if (collection != null)
			{
				this._genericCollection = collection;
				return;
			}
			this._list = list;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00010C6F File Offset: 0x0000EE6F
		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericCollection = list;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00010C89 File Offset: 0x0000EE89
		public virtual void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00010CB2 File Offset: 0x0000EEB2
		public virtual void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00010CD3 File Offset: 0x0000EED3
		public virtual bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00010CFB File Offset: 0x0000EEFB
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00010D20 File Offset: 0x0000EF20
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00010D41 File Offset: 0x0000EF41
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

		// Token: 0x06000466 RID: 1126 RVA: 0x00010D62 File Offset: 0x0000EF62
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

		// Token: 0x06000467 RID: 1127 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		public virtual IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> genericCollection = this._genericCollection;
			return (genericCollection ?? Enumerable.Cast<T>(this._list)).GetEnumerator();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00010DCC File Offset: 0x0000EFCC
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable genericCollection = this._genericCollection;
			return (genericCollection ?? this._list).GetEnumerator();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		[NullableContext(2)]
		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010E0C File Offset: 0x0000F00C
		[NullableContext(2)]
		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010E24 File Offset: 0x0000F024
		[NullableContext(2)]
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

		// Token: 0x0600046C RID: 1132 RVA: 0x00010E59 File Offset: 0x0000F059
		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support RemoveAt.");
			}
			this._list.RemoveAt(index);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00010E7A File Offset: 0x0000F07A
		[NullableContext(2)]
		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support Insert.");
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00010EAC File Offset: 0x0000F0AC
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

		// Token: 0x0600046F RID: 1135 RVA: 0x00010ECD File Offset: 0x0000F0CD
		[NullableContext(2)]
		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00010F05 File Offset: 0x0000F105
		[Nullable(2)]
		object IList.Item
		{
			[NullableContext(2)]
			get
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				return this._list[index];
			}
			[NullableContext(2)]
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

		// Token: 0x06000472 RID: 1138 RVA: 0x00010F37 File Offset: 0x0000F137
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00010F46 File Offset: 0x0000F146
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00010F49 File Offset: 0x0000F149
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

		// Token: 0x06000475 RID: 1141 RVA: 0x00010F6B File Offset: 0x0000F16B
		[NullableContext(2)]
		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), "value");
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00010F9A File Offset: 0x0000F19A
		[NullableContext(2)]
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType() || ReflectionUtils.IsNullableType(typeof(T))));
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public object UnderlyingCollection
		{
			get
			{
				return this._genericCollection ?? this._list;
			}
		}

		// Token: 0x04000199 RID: 409
		[Nullable(2)]
		private readonly IList _list;

		// Token: 0x0400019A RID: 410
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private readonly ICollection<T> _genericCollection;

		// Token: 0x0400019B RID: 411
		[Nullable(2)]
		private object _syncRoot;
	}
}

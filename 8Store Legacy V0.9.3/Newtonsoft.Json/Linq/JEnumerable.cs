using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a collection of <see cref="T:Newtonsoft.Json.Linq.JToken" /> objects.
	/// </summary>
	/// <typeparam name="T">The type of token</typeparam>
	// Token: 0x0200005A RID: 90
	public struct JEnumerable<T> : IJEnumerable<T>, IEnumerable<T>, IEnumerable where T : JToken
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JEnumerable`1" /> struct.
		/// </summary>
		/// <param name="enumerable">The enumerable.</param>
		// Token: 0x060004F0 RID: 1264 RVA: 0x00013088 File Offset: 0x00011288
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		// Token: 0x060004F1 RID: 1265 RVA: 0x0001309C File Offset: 0x0001129C
		public IEnumerator<T> GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
		/// </returns>
		// Token: 0x060004F2 RID: 1266 RVA: 0x000130A9 File Offset: 0x000112A9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.IJEnumerable`1" /> with the specified key.
		/// </summary>
		/// <value></value>
		// Token: 0x17000101 RID: 257
		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
		/// <returns>
		/// 	<c>true</c> if the specified <see cref="T:System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060004F4 RID: 1268 RVA: 0x000130C9 File Offset: 0x000112C9
		public override bool Equals(object obj)
		{
			return obj is JEnumerable<T> && this._enumerable.Equals(((JEnumerable<T>)obj)._enumerable);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		// Token: 0x060004F5 RID: 1269 RVA: 0x000130EB File Offset: 0x000112EB
		public override int GetHashCode()
		{
			return this._enumerable.GetHashCode();
		}

		/// <summary>
		/// An empty collection of <see cref="T:Newtonsoft.Json.Linq.JToken" /> objects.
		/// </summary>
		// Token: 0x0400019C RID: 412
		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		// Token: 0x0400019D RID: 413
		private readonly IEnumerable<T> _enumerable;
	}
}

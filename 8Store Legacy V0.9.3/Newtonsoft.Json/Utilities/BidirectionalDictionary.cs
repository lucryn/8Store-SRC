using System;
using System.Collections.Generic;
using System.Globalization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AF RID: 175
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x000219F1 File Offset: 0x0001FBF1
		public BidirectionalDictionary() : this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00021A03 File Offset: 0x0001FC03
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer) : this(firstEqualityComparer, secondEqualityComparer, "Duplicate item already exists for '{0}'.", "Duplicate item already exists for '{0}'.")
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00021A17 File Offset: 0x0001FC17
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
			this._duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			this._duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00021A48 File Offset: 0x0001FC48
		public void Set(TFirst first, TSecond second)
		{
			TSecond tsecond;
			if (this._firstToSecond.TryGetValue(first, ref tsecond) && !tsecond.Equals(second))
			{
				throw new ArgumentException(this._duplicateFirstErrorMessage.FormatWith(CultureInfo.InvariantCulture, first));
			}
			TFirst tfirst;
			if (this._secondToFirst.TryGetValue(second, ref tfirst) && !tfirst.Equals(first))
			{
				throw new ArgumentException(this._duplicateSecondErrorMessage.FormatWith(CultureInfo.InvariantCulture, second));
			}
			this._firstToSecond.Add(first, second);
			this._secondToFirst.Add(second, first);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00021AF1 File Offset: 0x0001FCF1
		public bool TryGetByFirst(TFirst first, out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, ref second);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00021B00 File Offset: 0x0001FD00
		public bool TryGetBySecond(TSecond second, out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, ref first);
		}

		// Token: 0x0400032D RID: 813
		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		// Token: 0x0400032E RID: 814
		private readonly IDictionary<TSecond, TFirst> _secondToFirst;

		// Token: 0x0400032F RID: 815
		private readonly string _duplicateFirstErrorMessage;

		// Token: 0x04000330 RID: 816
		private readonly string _duplicateSecondErrorMessage;
	}
}

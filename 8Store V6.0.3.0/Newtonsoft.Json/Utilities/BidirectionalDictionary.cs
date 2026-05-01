using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	[Nullable(0)]
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00010350 File Offset: 0x0000E550
		public BidirectionalDictionary() : this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00010362 File Offset: 0x0000E562
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer) : this(firstEqualityComparer, secondEqualityComparer, "Duplicate item already exists for '{0}'.", "Duplicate item already exists for '{0}'.")
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00010376 File Offset: 0x0000E576
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
			this._duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			this._duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000103A8 File Offset: 0x0000E5A8
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

		// Token: 0x06000444 RID: 1092 RVA: 0x00010451 File Offset: 0x0000E651
		public bool TryGetByFirst(TFirst first, [Nullable(2)] [NotNullWhen(true)] out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, ref second);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00010460 File Offset: 0x0000E660
		public bool TryGetBySecond(TSecond second, [Nullable(2)] [NotNullWhen(true)] out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, ref first);
		}

		// Token: 0x0400017A RID: 378
		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		// Token: 0x0400017B RID: 379
		private readonly IDictionary<TSecond, TFirst> _secondToFirst;

		// Token: 0x0400017C RID: 380
		private readonly string _duplicateFirstErrorMessage;

		// Token: 0x0400017D RID: 381
		private readonly string _duplicateSecondErrorMessage;
	}
}

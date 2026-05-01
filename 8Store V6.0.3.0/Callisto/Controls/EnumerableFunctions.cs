using System;
using System.Collections.Generic;
using System.Linq;

namespace Callisto.Controls
{
	// Token: 0x02000014 RID: 20
	internal static class EnumerableFunctions
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00005974 File Offset: 0x00003B74
		public static IEnumerable<R> Scan<T, R>(this IEnumerable<T> that, Func<R, T, R> func, R initialValue)
		{
			R acc = initialValue;
			yield return acc;
			foreach (T t in that)
			{
				acc = func.Invoke(acc, t);
				yield return acc;
			}
			yield break;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public static IEnumerable<R> Zip<T0, T1, R>(IEnumerable<T0> enumerable0, IEnumerable<T1> enumerable1, Func<T0, T1, R> func)
		{
			IEnumerator<T0> enumerator0 = enumerable0.GetEnumerator();
			IEnumerator<T1> enumerator = enumerable1.GetEnumerator();
			while (enumerator0.MoveNext() && enumerator.MoveNext())
			{
				yield return func.Invoke(enumerator0.Current, enumerator.Current);
			}
			yield break;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005B0C File Offset: 0x00003D0C
		public static int? IndexOf<T>(this IEnumerable<T> that, T item)
		{
			IEnumerator<T> enumerator = that.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				if (object.ReferenceEquals(enumerator.Current, item))
				{
					return new int?(num);
				}
				num++;
			}
			return default(int?);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public static IEnumerable<double> GetWeightedValues(this IEnumerable<double> values, double percent)
		{
			double total = (double)Enumerable.Count<double>(values);
			if (total == 0.0)
			{
				return Enumerable.Select<double, double>(values, (double _) => 0.0);
			}
			return Enumerable.Select<Tuple<double, double>, double>(Enumerable.Select<Tuple<double, double>, Tuple<double, double>>(EnumerableFunctions.Zip<double, double, Tuple<double, double>>(values.Scan((double acc, double current) => acc + current, 0.0), values, (double acc, double current) => Tuple.Create<double, double>(acc, current)), (Tuple<double, double> tuple) => Tuple.Create<double, double>(tuple.Item1 / total, tuple.Item2 / total)), delegate(Tuple<double, double> tuple)
			{
				double item = tuple.Item1;
				double item2 = tuple.Item2;
				if (percent > item && item + item2 > percent)
				{
					return (percent - item) * total;
				}
				if (percent <= item)
				{
					return 0.0;
				}
				return 1.0;
			});
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B0 RID: 176
	internal static class CollectionUtils
	{
		/// <summary>
		/// Determines whether the collection is null or empty.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <returns>
		/// 	<c>true</c> if the collection is null or empty; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060008E0 RID: 2272 RVA: 0x00021B0F File Offset: 0x0001FD0F
		public static bool IsNullOrEmpty<T>(ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		/// <summary>
		/// Adds the elements of the specified collection to the specified generic IList.
		/// </summary>
		/// <param name="initial">The list to add to.</param>
		/// <param name="collection">The collection of elements to add.</param>
		// Token: 0x060008E1 RID: 2273 RVA: 0x00021B20 File Offset: 0x0001FD20
		public static void AddRange<T>(this IList<T> initial, IEnumerable<T> collection)
		{
			if (initial == null)
			{
				throw new ArgumentNullException("initial");
			}
			if (collection == null)
			{
				return;
			}
			foreach (T t in collection)
			{
				initial.Add(t);
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00021B7C File Offset: 0x0001FD7C
		public static bool IsDictionaryType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return typeof(IDictionary).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IDictionary)) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IReadOnlyDictionary));
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00021BD4 File Offset: 0x0001FDD4
		public static ConstructorInfo ResolveEnumableCollectionConstructor(Type collectionType, Type collectionItemType)
		{
			Type type = typeof(IEnumerable).MakeGenericType(new Type[]
			{
				collectionItemType
			});
			foreach (ConstructorInfo constructorInfo in collectionType.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
			{
				IList<ParameterInfo> parameters = constructorInfo.GetParameters();
				if (parameters.Count == 1 && type.IsAssignableFrom(parameters[0].ParameterType))
				{
					return constructorInfo;
				}
			}
			return null;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00021C6C File Offset: 0x0001FE6C
		public static bool AddDistinct<T>(this IList<T> list, T value)
		{
			return list.AddDistinct(value, EqualityComparer<T>.Default);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00021C7A File Offset: 0x0001FE7A
		public static bool AddDistinct<T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
		{
			if (list.ContainsValue(value, comparer))
			{
				return false;
			}
			list.Add(value);
			return true;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00021C90 File Offset: 0x0001FE90
		public static bool ContainsValue<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			foreach (TSource tsource in source)
			{
				if (comparer.Equals(tsource, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00021CFC File Offset: 0x0001FEFC
		public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
		{
			bool result = true;
			foreach (T value in values)
			{
				if (!list.AddDistinct(value, comparer))
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			int num = 0;
			foreach (T t in collection)
			{
				if (predicate.Invoke(t))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		/// <summary>
		/// Returns the index of the first occurrence in a sequence by using a specified IEqualityComparer.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="list">A sequence in which to locate a value.</param>
		/// <param name="value">The object to locate in the sequence</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		/// <returns>The zero-based index of the first occurrence of value within the entire sequence, if found; otherwise, –1.</returns>
		// Token: 0x060008E9 RID: 2281 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value, IEqualityComparer<TSource> comparer)
		{
			int num = 0;
			foreach (TSource tsource in list)
			{
				if (comparer.Equals(tsource, value))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00021DFC File Offset: 0x0001FFFC
		private static IList<int> GetDimensions(IList values)
		{
			IList<int> list = new List<int>();
			IList list2 = values;
			for (;;)
			{
				list.Add(list2.Count);
				if (list2.Count == 0)
				{
					break;
				}
				object obj = list2[0];
				if (!(obj is IList))
				{
					break;
				}
				list2 = (IList)obj;
			}
			return list;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00021E40 File Offset: 0x00020040
		private static void CopyFromJaggedToMultidimensionalArray(IList values, Array multidimensionalArray, int[] indices)
		{
			int num = indices.Length;
			if (num == multidimensionalArray.Rank)
			{
				multidimensionalArray.SetValue(CollectionUtils.JaggedArrayGetValue(values, indices), indices);
				return;
			}
			int length = multidimensionalArray.GetLength(num);
			IList list = (IList)CollectionUtils.JaggedArrayGetValue(values, indices);
			int count = list.Count;
			if (count != length)
			{
				throw new Exception("Cannot deserialize non-cubical array as multidimensional array.");
			}
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			for (int j = 0; j < multidimensionalArray.GetLength(num); j++)
			{
				array[num] = j;
				CollectionUtils.CopyFromJaggedToMultidimensionalArray(values, multidimensionalArray, array);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00021EDC File Offset: 0x000200DC
		private static object JaggedArrayGetValue(IList values, int[] indices)
		{
			IList list = values;
			for (int i = 0; i < indices.Length; i++)
			{
				int num = indices[i];
				if (i == indices.Length - 1)
				{
					return list[num];
				}
				list = (IList)list[num];
			}
			return list;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00021F1C File Offset: 0x0002011C
		public static Array ToMultidimensionalArray(IList values, Type type, int rank)
		{
			IList<int> dimensions = CollectionUtils.GetDimensions(values);
			while (dimensions.Count < rank)
			{
				dimensions.Add(0);
			}
			Array array = Array.CreateInstance(type, Enumerable.ToArray<int>(dimensions));
			CollectionUtils.CopyFromJaggedToMultidimensionalArray(values, array, new int[0]);
			return array;
		}
	}
}

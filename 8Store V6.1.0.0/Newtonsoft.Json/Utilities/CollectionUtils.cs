using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004E RID: 78
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CollectionUtils
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00010798 File Offset: 0x0000E998
		public static bool IsNullOrEmpty<[Nullable(2)] T>(ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000107A8 File Offset: 0x0000E9A8
		public static void AddRange<[Nullable(2)] T>(this IList<T> initial, IEnumerable<T> collection)
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

		// Token: 0x0600044E RID: 1102 RVA: 0x00010804 File Offset: 0x0000EA04
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		public static bool IsDictionaryType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return typeof(IDictionary).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IDictionary)) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IReadOnlyDictionary));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001085C File Offset: 0x0000EA5C
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static ConstructorInfo ResolveEnumerableCollectionConstructor([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type collectionType, Type collectionItemType)
		{
			Type constructorArgumentType = typeof(IList).MakeGenericType(new Type[]
			{
				collectionItemType
			});
			return CollectionUtils.ResolveEnumerableCollectionConstructor(collectionType, collectionItemType, constructorArgumentType);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001088C File Offset: 0x0000EA8C
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static ConstructorInfo ResolveEnumerableCollectionConstructor([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type collectionType, Type collectionItemType, Type constructorArgumentType)
		{
			Type type = typeof(IEnumerable).MakeGenericType(new Type[]
			{
				collectionItemType
			});
			ConstructorInfo constructorInfo = null;
			foreach (ConstructorInfo constructorInfo2 in collectionType.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
			{
				IList<ParameterInfo> parameters = constructorInfo2.GetParameters();
				if (parameters.Count == 1)
				{
					Type parameterType = parameters[0].ParameterType;
					if (type == parameterType)
					{
						constructorInfo = constructorInfo2;
						break;
					}
					if (constructorInfo == null && parameterType.IsAssignableFrom(constructorArgumentType))
					{
						constructorInfo = constructorInfo2;
					}
				}
			}
			return constructorInfo;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static bool AddDistinct<[Nullable(2)] T>(this IList<T> list, T value)
		{
			return list.AddDistinct(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001093A File Offset: 0x0000EB3A
		public static bool AddDistinct<[Nullable(2)] T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
		{
			if (list.ContainsValue(value, comparer))
			{
				return false;
			}
			list.Add(value);
			return true;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00010950 File Offset: 0x0000EB50
		public static bool ContainsValue<[Nullable(2)] TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
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

		// Token: 0x06000454 RID: 1108 RVA: 0x000109BC File Offset: 0x0000EBBC
		public static bool AddRangeDistinct<[Nullable(2)] T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
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

		// Token: 0x06000455 RID: 1109 RVA: 0x00010A0C File Offset: 0x0000EC0C
		public static int IndexOf<[Nullable(2)] T>(this IEnumerable<T> collection, Func<T, bool> predicate)
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

		// Token: 0x06000456 RID: 1110 RVA: 0x00010A64 File Offset: 0x0000EC64
		public static bool Contains<[Nullable(2)] T>(this List<T> list, T value, IEqualityComparer comparer)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (comparer.Equals(value, list[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		public static int IndexOfReference<[Nullable(2)] T>(this List<T> list, T item)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (item == list[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		private static IList<int> GetDimensions(IList values, int dimensionsCount)
		{
			IList<int> list = new List<int>();
			IList list2 = values;
			for (;;)
			{
				list.Add(list2.Count);
				if (list.Count == dimensionsCount || list2.Count == 0)
				{
					break;
				}
				IList list3 = list2[0] as IList;
				if (list3 == null)
				{
					break;
				}
				list2 = list3;
			}
			return list;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010B20 File Offset: 0x0000ED20
		private static void CopyFromJaggedToMultidimensionalArray(IList values, Array multidimensionalArray, int[] indices)
		{
			int num = indices.Length;
			if (num == multidimensionalArray.Rank)
			{
				multidimensionalArray.SetValue(CollectionUtils.JaggedArrayGetValue(values, indices), indices);
				return;
			}
			int length = multidimensionalArray.GetLength(num);
			if (((IList)CollectionUtils.JaggedArrayGetValue(values, indices)).Count != length)
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

		// Token: 0x0600045A RID: 1114 RVA: 0x00010BB0 File Offset: 0x0000EDB0
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

		// Token: 0x0600045B RID: 1115 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static Array ToMultidimensionalArray(IList values, Type type, int rank)
		{
			IList<int> dimensions = CollectionUtils.GetDimensions(values, rank);
			while (dimensions.Count < rank)
			{
				dimensions.Add(0);
			}
			Array array = Array.CreateInstance(type, Enumerable.ToArray<int>(dimensions));
			CollectionUtils.CopyFromJaggedToMultidimensionalArray(values, array, CollectionUtils.ArrayEmpty<int>());
			return array;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00010C31 File Offset: 0x0000EE31
		public static T[] ArrayEmpty<[Nullable(2)] T>()
		{
			return CollectionUtils.EmptyArrayContainer<T>.Empty;
		}

		// Token: 0x02000165 RID: 357
		[NullableContext(0)]
		private static class EmptyArrayContainer<[Nullable(2)] T>
		{
			// Token: 0x040006BE RID: 1726
			[Nullable(1)]
			public static readonly T[] Empty = new T[0];
		}
	}
}

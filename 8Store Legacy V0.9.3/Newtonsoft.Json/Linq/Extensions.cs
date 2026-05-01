using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Contains the LINQ to JSON extension methods.
	/// </summary>
	// Token: 0x02000053 RID: 83
	public static class Extensions
	{
		/// <summary>
		/// Returns a collection of tokens that contains the ancestors of every token in the source collection.
		/// </summary>
		/// <typeparam name="T">The type of the objects in source, constrained to <see cref="T:Newtonsoft.Json.Linq.JToken" />.</typeparam>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the ancestors of every node in the source collection.</returns>
		// Token: 0x060003E6 RID: 998 RVA: 0x0000F1DB File Offset: 0x0000D3DB
		public static IJEnumerable<JToken> Ancestors<T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.Ancestors()).AsJEnumerable();
		}

		/// <summary>
		/// Returns a collection of tokens that contains the descendants of every token in the source collection.
		/// </summary>
		/// <typeparam name="T">The type of the objects in source, constrained to <see cref="T:Newtonsoft.Json.Linq.JContainer" />.</typeparam>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the descendants of every node in the source collection.</returns>
		// Token: 0x060003E7 RID: 999 RVA: 0x0000F20E File Offset: 0x0000D40E
		public static IJEnumerable<JToken> Descendants<T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.Descendants()).AsJEnumerable();
		}

		/// <summary>
		/// Returns a collection of child properties of every object in the source collection.
		/// </summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JObject" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JProperty" /> that contains the properties of every object in the source collection.</returns>
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000F23A File Offset: 0x0000D43A
		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<JObject, JProperty>(source, (JObject d) => d.Properties()).AsJEnumerable<JProperty>();
		}

		/// <summary>
		/// Returns a collection of child values of every object in the source collection with the given key.
		/// </summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <param name="key">The token key.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the values of every node in the source collection with the given key.</returns>
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000F26F File Offset: 0x0000D46F
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key).AsJEnumerable();
		}

		/// <summary>
		/// Returns a collection of child values of every object in the source collection.
		/// </summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the values of every node in the source collection.</returns>
		// Token: 0x060003EA RID: 1002 RVA: 0x0000F27D File Offset: 0x0000D47D
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		/// <summary>
		/// Returns a collection of converted child values of every object in the source collection with the given key.
		/// </summary>
		/// <typeparam name="U">The type to convert the values to.</typeparam>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <param name="key">The token key.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the converted values of every node in the source collection with the given key.</returns>
		// Token: 0x060003EB RID: 1003 RVA: 0x0000F286 File Offset: 0x0000D486
		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key);
		}

		/// <summary>
		/// Returns a collection of converted child values of every object in the source collection.
		/// </summary>
		/// <typeparam name="U">The type to convert the values to.</typeparam>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the converted values of every node in the source collection.</returns>
		// Token: 0x060003EC RID: 1004 RVA: 0x0000F28F File Offset: 0x0000D48F
		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		/// <summary>
		/// Converts the value.
		/// </summary>
		/// <typeparam name="U">The type to convert the value to.</typeparam>
		/// <param name="value">A <see cref="T:Newtonsoft.Json.Linq.JToken" /> cast as a <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" />.</param>
		/// <returns>A converted value.</returns>
		// Token: 0x060003ED RID: 1005 RVA: 0x0000F298 File Offset: 0x0000D498
		public static U Value<U>(this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		/// <summary>
		/// Converts the value.
		/// </summary>
		/// <typeparam name="T">The source collection type.</typeparam>
		/// <typeparam name="U">The type to convert the value to.</typeparam>
		/// <param name="value">A <see cref="T:Newtonsoft.Json.Linq.JToken" /> cast as a <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" />.</param>
		/// <returns>A converted value.</returns>
		// Token: 0x060003EE RID: 1006 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		public static U Value<T, U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, "source");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Source value must be a JToken.");
			}
			return jtoken.Convert<JToken, U>();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		internal static IEnumerable<U> Values<T, U>(this IEnumerable<T> source, object key) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T t2 in source)
			{
				JToken token = t2;
				if (key == null)
				{
					if (token is JValue)
					{
						yield return ((JValue)token).Convert<JValue, U>();
					}
					else
					{
						foreach (JToken t in token.Children())
						{
							yield return t.Convert<JToken, U>();
						}
					}
				}
				else
				{
					JToken value = token[key];
					if (value != null)
					{
						yield return value.Convert<JToken, U>();
					}
				}
			}
			yield break;
		}

		/// <summary>
		/// Returns a collection of child tokens of every array in the source collection.
		/// </summary>
		/// <typeparam name="T">The source collection type.</typeparam>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the values of every node in the source collection.</returns>
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		public static IJEnumerable<JToken> Children<T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		/// <summary>
		/// Returns a collection of converted child tokens of every array in the source collection.
		/// </summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <typeparam name="U">The type to convert the values to.</typeparam>
		/// <typeparam name="T">The source collection type.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the converted values of every node in the source collection.</returns>
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F60D File Offset: 0x0000D80D
		public static IEnumerable<U> Children<T, U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T c) => c.Children()).Convert<JToken, U>();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		internal static IEnumerable<U> Convert<T, U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T token in source)
			{
				yield return token.Convert<JToken, U>();
			}
			yield break;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		internal static U Convert<T, U>(this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U && typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
			{
				return (U)((object)token);
			}
			JValue jvalue = token as JValue;
			if (jvalue == null)
			{
				throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			if (jvalue.Value is U)
			{
				return (U)((object)jvalue.Value);
			}
			Type type = typeof(U);
			if (ReflectionUtils.IsNullableType(type))
			{
				if (jvalue.Value == null)
				{
					return default(U);
				}
				type = Nullable.GetUnderlyingType(type);
			}
			return (U)((object)System.Convert.ChangeType(jvalue.Value, type, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Returns the input typed as <see cref="T:Newtonsoft.Json.Linq.IJEnumerable`1" />.
		/// </summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <returns>The input typed as <see cref="T:Newtonsoft.Json.Linq.IJEnumerable`1" />.</returns>
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F8F2 File Offset: 0x0000DAF2
		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		/// <summary>
		/// Returns the input typed as <see cref="T:Newtonsoft.Json.Linq.IJEnumerable`1" />.
		/// </summary>
		/// <typeparam name="T">The source collection type.</typeparam>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the source collection.</param>
		/// <returns>The input typed as <see cref="T:Newtonsoft.Json.Linq.IJEnumerable`1" />.</returns>
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F8FA File Offset: 0x0000DAFA
		public static IJEnumerable<T> AsJEnumerable<T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			if (source is IJEnumerable<T>)
			{
				return (IJEnumerable<T>)source;
			}
			return new JEnumerable<T>(source);
		}
	}
}

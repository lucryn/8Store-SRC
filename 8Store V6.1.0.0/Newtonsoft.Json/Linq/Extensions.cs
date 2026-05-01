using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BF RID: 191
	[NullableContext(1)]
	[Nullable(0)]
	public static class Extensions
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x00026C63 File Offset: 0x00024E63
		public static IJEnumerable<JToken> Ancestors<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.Ancestors()).AsJEnumerable();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00026C9A File Offset: 0x00024E9A
		public static IJEnumerable<JToken> AncestorsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.AncestorsAndSelf()).AsJEnumerable();
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00026CD1 File Offset: 0x00024ED1
		public static IJEnumerable<JToken> Descendants<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.Descendants()).AsJEnumerable();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00026D08 File Offset: 0x00024F08
		public static IJEnumerable<JToken> DescendantsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T j) => j.DescendantsAndSelf()).AsJEnumerable();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00026D3F File Offset: 0x00024F3F
		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<JObject, JProperty>(source, (JObject d) => d.Properties()).AsJEnumerable<JProperty>();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00026D76 File Offset: 0x00024F76
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, [Nullable(2)] object key)
		{
			return source.Values(key).AsJEnumerable();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00026D84 File Offset: 0x00024F84
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00026D8D File Offset: 0x00024F8D
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00026D96 File Offset: 0x00024F96
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00026D9F File Offset: 0x00024F9F
		[NullableContext(2)]
		public static U Value<U>([Nullable(1)] this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00026DA7 File Offset: 0x00024FA7
		[return: Nullable(2)]
		public static U Value<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Source value must be a JToken.");
			}
			return jtoken.Convert<JToken, U>();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00026DCD File Offset: 0x00024FCD
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		internal static IEnumerable<U> Values<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source, [Nullable(2)] object key) where T : JToken
		{
			Extensions.<Values>d__11<T, U> <Values>d__ = new Extensions.<Values>d__11<T, U>(-2);
			<Values>d__.<>3__source = source;
			<Values>d__.<>3__key = key;
			return <Values>d__;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00026DE4 File Offset: 0x00024FE4
		public static IJEnumerable<JToken> Children<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00026DF1 File Offset: 0x00024FF1
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public static IEnumerable<U> Children<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return Enumerable.SelectMany<T, JToken>(source, (T c) => c.Children()).Convert<JToken, U>();
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00026E28 File Offset: 0x00025028
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		internal static IEnumerable<U> Convert<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			Extensions.<Convert>d__14<T, U> <Convert>d__ = new Extensions.<Convert>d__14<T, U>(-2);
			<Convert>d__.<>3__source = source;
			return <Convert>d__;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00026E38 File Offset: 0x00025038
		[NullableContext(2)]
		internal static U Convert<[Nullable(0)] T, U>([Nullable(1)] this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U)
			{
				U result = token as U;
				if (typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
				{
					return result;
				}
			}
			JValue jvalue = token as JValue;
			if (jvalue == null)
			{
				throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			object value = jvalue.Value;
			if (value is U)
			{
				return (U)((object)value);
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

		// Token: 0x0600097D RID: 2429 RVA: 0x00026F3E File Offset: 0x0002513E
		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00026F48 File Offset: 0x00025148
		public static IJEnumerable<T> AsJEnumerable<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			IJEnumerable<T> ijenumerable = source as IJEnumerable<T>;
			if (ijenumerable != null)
			{
				return ijenumerable;
			}
			return new JEnumerable<T>(source);
		}
	}
}

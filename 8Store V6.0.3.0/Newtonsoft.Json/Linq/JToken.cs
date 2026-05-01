using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq.JsonPath;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000CD RID: 205
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JToken : IJEnumerable<JToken>, IEnumerable<JToken>, IEnumerable, IJsonLineInfo, IDynamicMetaObjectProvider
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00029BEA File Offset: 0x00027DEA
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public virtual Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00029BF4 File Offset: 0x00027DF4
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public Task WriteToAsync(JsonWriter writer, params JsonConverter[] converters)
		{
			return this.WriteToAsync(writer, default(CancellationToken), converters);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00029C14 File Offset: 0x00027E14
		[UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "WriteToAsync without converters is safe.")]
		[UnconditionalSuppressMessage("AOT", "IL3050", Justification = "WriteToAsync without converters is safe.")]
		public Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken)
		{
			return this.WriteToAsync(writer, default(CancellationToken), CollectionUtils.ArrayEmpty<JsonConverter>());
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00029C38 File Offset: 0x00027E38
		[UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "WriteToAsync without converters is safe.")]
		[UnconditionalSuppressMessage("AOT", "IL3050", Justification = "WriteToAsync without converters is safe.")]
		public Task WriteToAsync(JsonWriter writer)
		{
			return this.WriteToAsync(writer, default(CancellationToken), CollectionUtils.ArrayEmpty<JsonConverter>());
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00029C5A File Offset: 0x00027E5A
		public static Task<JToken> ReadFromAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JToken.ReadFromAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00029C64 File Offset: 0x00027E64
		public static Task<JToken> ReadFromAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			JToken.<ReadFromAsync>d__5 <ReadFromAsync>d__;
			<ReadFromAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JToken>.Create();
			<ReadFromAsync>d__.reader = reader;
			<ReadFromAsync>d__.settings = settings;
			<ReadFromAsync>d__.cancellationToken = cancellationToken;
			<ReadFromAsync>d__.<>1__state = -1;
			<ReadFromAsync>d__.<>t__builder.Start<JToken.<ReadFromAsync>d__5>(ref <ReadFromAsync>d__);
			return <ReadFromAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00029CB7 File Offset: 0x00027EB7
		public static Task<JToken> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JToken.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00029CC1 File Offset: 0x00027EC1
		public static Task<JToken> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JToken.ReadFromAsync(reader, settings, cancellationToken);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00029CCB File Offset: 0x00027ECB
		public static JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (JToken._equalityComparer == null)
				{
					JToken._equalityComparer = new JTokenEqualityComparer();
				}
				return JToken._equalityComparer;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00029CE3 File Offset: 0x00027EE3
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00029CEB File Offset: 0x00027EEB
		[Nullable(2)]
		public JContainer Parent
		{
			[NullableContext(2)]
			[DebuggerStepThrough]
			get
			{
				return this._parent;
			}
			[NullableContext(2)]
			internal set
			{
				this._parent = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00029CF4 File Offset: 0x00027EF4
		public JToken Root
		{
			get
			{
				JContainer parent = this.Parent;
				if (parent == null)
				{
					return this;
				}
				while (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				return parent;
			}
		}

		// Token: 0x06000AA4 RID: 2724
		internal abstract JToken CloneToken([Nullable(2)] JsonCloneSettings settings);

		// Token: 0x06000AA5 RID: 2725
		internal abstract bool DeepEquals(JToken node);

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000AA6 RID: 2726
		public abstract JTokenType Type { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000AA7 RID: 2727
		public abstract bool HasValues { get; }

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00029D1D File Offset: 0x00027F1D
		[NullableContext(2)]
		public static bool DeepEquals(JToken t1, JToken t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.DeepEquals(t2));
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00029D34 File Offset: 0x00027F34
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x00029D3C File Offset: 0x00027F3C
		[Nullable(2)]
		public JToken Next
		{
			[NullableContext(2)]
			get
			{
				return this._next;
			}
			[NullableContext(2)]
			internal set
			{
				this._next = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00029D45 File Offset: 0x00027F45
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x00029D4D File Offset: 0x00027F4D
		[Nullable(2)]
		public JToken Previous
		{
			[NullableContext(2)]
			get
			{
				return this._previous;
			}
			[NullableContext(2)]
			internal set
			{
				this._previous = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00029D58 File Offset: 0x00027F58
		public string Path
		{
			get
			{
				if (this.Parent == null)
				{
					return string.Empty;
				}
				List<JsonPosition> list = new List<JsonPosition>();
				JToken jtoken = null;
				for (JToken jtoken2 = this; jtoken2 != null; jtoken2 = jtoken2.Parent)
				{
					JTokenType type = jtoken2.Type;
					if (type - JTokenType.Array > 1)
					{
						if (type == JTokenType.Property)
						{
							JProperty jproperty = (JProperty)jtoken2;
							List<JsonPosition> list2 = list;
							JsonPosition jsonPosition = new JsonPosition(JsonContainerType.Object)
							{
								PropertyName = jproperty.Name
							};
							list2.Add(jsonPosition);
						}
					}
					else if (jtoken != null)
					{
						int position = ((IList<JToken>)jtoken2).IndexOf(jtoken);
						List<JsonPosition> list3 = list;
						JsonPosition jsonPosition = new JsonPosition(JsonContainerType.Array)
						{
							Position = position
						};
						list3.Add(jsonPosition);
					}
					jtoken = jtoken2;
				}
				list.Reverse();
				return JsonPosition.BuildPath(list, default(JsonPosition?));
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00029E05 File Offset: 0x00028005
		internal JToken()
		{
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00029E10 File Offset: 0x00028010
		[NullableContext(2)]
		public void AddAfterSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.TryAddInternal(num + 1, content, false, true);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00029E50 File Offset: 0x00028050
		[NullableContext(2)]
		public void AddBeforeSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int index = this._parent.IndexOfItem(this);
			this._parent.TryAddInternal(index, content, false, true);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00029E8D File Offset: 0x0002808D
		public IEnumerable<JToken> Ancestors()
		{
			return this.GetAncestors(false);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00029E96 File Offset: 0x00028096
		public IEnumerable<JToken> AncestorsAndSelf()
		{
			return this.GetAncestors(true);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00029E9F File Offset: 0x0002809F
		internal IEnumerable<JToken> GetAncestors(bool self)
		{
			JToken.<GetAncestors>d__49 <GetAncestors>d__ = new JToken.<GetAncestors>d__49(-2);
			<GetAncestors>d__.<>4__this = this;
			<GetAncestors>d__.<>3__self = self;
			return <GetAncestors>d__;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00029EB6 File Offset: 0x000280B6
		public IEnumerable<JToken> AfterSelf()
		{
			JToken.<AfterSelf>d__50 <AfterSelf>d__ = new JToken.<AfterSelf>d__50(-2);
			<AfterSelf>d__.<>4__this = this;
			return <AfterSelf>d__;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00029EC6 File Offset: 0x000280C6
		public IEnumerable<JToken> BeforeSelf()
		{
			JToken.<BeforeSelf>d__51 <BeforeSelf>d__ = new JToken.<BeforeSelf>d__51(-2);
			<BeforeSelf>d__.<>4__this = this;
			return <BeforeSelf>d__;
		}

		// Token: 0x170001F7 RID: 503
		[Nullable(2)]
		public virtual JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			[param: Nullable(2)]
			set
			{
				throw new InvalidOperationException("Cannot set child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00029F10 File Offset: 0x00028110
		[NullableContext(2)]
		public virtual T Value<T>([Nullable(1)] object key)
		{
			JToken jtoken = this[key];
			if (jtoken != null)
			{
				return jtoken.Convert<JToken, T>();
			}
			return default(T);
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00029F38 File Offset: 0x00028138
		[Nullable(2)]
		public virtual JToken First
		{
			[NullableContext(2)]
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00029F54 File Offset: 0x00028154
		[Nullable(2)]
		public virtual JToken Last
		{
			[NullableContext(2)]
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00029F70 File Offset: 0x00028170
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public virtual JEnumerable<JToken> Children()
		{
			return JEnumerable<JToken>.Empty;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00029F77 File Offset: 0x00028177
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(Enumerable.OfType<T>(this.Children()));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00029F8E File Offset: 0x0002818E
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual IEnumerable<T> Values<T>()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00029FAA File Offset: 0x000281AA
		public void Remove()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.RemoveItem(this);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00029FCC File Offset: 0x000281CC
		public void Replace(JToken value)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.ReplaceItem(this, value);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00029FEE File Offset: 0x000281EE
		[UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "WriteTo without converters is safe.")]
		[UnconditionalSuppressMessage("AOT", "IL3050", Justification = "WriteTo without converters is safe.")]
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, CollectionUtils.ArrayEmpty<JsonConverter>());
		}

		// Token: 0x06000AC1 RID: 2753
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00029FFC File Offset: 0x000281FC
		public override string ToString()
		{
			return this.ToString(Formatting.Indented);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002A008 File Offset: 0x00028208
		public string ToString(Formatting formatting)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(new JsonTextWriter(stringWriter)
				{
					Formatting = formatting
				});
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002A05C File Offset: 0x0002825C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public string ToString(Formatting formatting, params JsonConverter[] converters)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(new JsonTextWriter(stringWriter)
				{
					Formatting = formatting
				}, converters);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002A0B0 File Offset: 0x000282B0
		[return: Nullable(2)]
		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			JProperty jproperty = value as JProperty;
			if (jproperty != null)
			{
				value = jproperty.Value;
			}
			return value as JValue;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002A0E4 File Offset: 0x000282E4
		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			JProperty jproperty = token as JProperty;
			if (jproperty != null)
			{
				token = jproperty.Value;
			}
			return token.Type.ToString();
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002A122 File Offset: 0x00028322
		private static bool ValidateToken(JToken o, JTokenType[] validTypes, bool nullable)
		{
			return Array.IndexOf<JTokenType>(validTypes, o.Type) != -1 || (nullable && (o.Type == JTokenType.Null || o.Type == JTokenType.Undefined));
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002A150 File Offset: 0x00028350
		public static explicit operator bool(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002A1A0 File Offset: 0x000283A0
		public static explicit operator DateTimeOffset(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				return (DateTimeOffset)value2;
			}
			string text = jvalue.Value as string;
			if (text != null)
			{
				return DateTimeOffset.Parse(text, CultureInfo.InvariantCulture);
			}
			return new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002A228 File Offset: 0x00028428
		[NullableContext(2)]
		public static explicit operator bool?(JToken value)
		{
			if (value == null)
			{
				return default(bool?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(bool?);
			}
			return new bool?(Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002A29C File Offset: 0x0002849C
		public static explicit operator long(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002A2EC File Offset: 0x000284EC
		[NullableContext(2)]
		public static explicit operator DateTime?(JToken value)
		{
			if (value == null)
			{
				return default(DateTime?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				return new DateTime?(((DateTimeOffset)value2).DateTime);
			}
			if (jvalue.Value == null)
			{
				return default(DateTime?);
			}
			return new DateTime?(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002A384 File Offset: 0x00028584
		[NullableContext(2)]
		public static explicit operator DateTimeOffset?(JToken value)
		{
			if (value == null)
			{
				return default(DateTimeOffset?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(DateTimeOffset?);
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value2;
				return new DateTimeOffset?(dateTimeOffset);
			}
			string text = jvalue.Value as string;
			if (text != null)
			{
				return new DateTimeOffset?(DateTimeOffset.Parse(text, CultureInfo.InvariantCulture));
			}
			return new DateTimeOffset?(new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002A440 File Offset: 0x00028640
		[NullableContext(2)]
		public static explicit operator decimal?(JToken value)
		{
			if (value == null)
			{
				return default(decimal?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(decimal?);
			}
			return new decimal?(Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002A4B4 File Offset: 0x000286B4
		[NullableContext(2)]
		public static explicit operator double?(JToken value)
		{
			if (value == null)
			{
				return default(double?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(double?);
			}
			return new double?(Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002A528 File Offset: 0x00028728
		[NullableContext(2)]
		public static explicit operator char?(JToken value)
		{
			if (value == null)
			{
				return default(char?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Char.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(char?);
			}
			return new char?(Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002A59C File Offset: 0x0002879C
		public static explicit operator int(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002A5EC File Offset: 0x000287EC
		public static explicit operator short(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002A63C File Offset: 0x0002883C
		[CLSCompliant(false)]
		public static explicit operator ushort(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002A68C File Offset: 0x0002888C
		[CLSCompliant(false)]
		public static explicit operator char(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Char.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002A6DC File Offset: 0x000288DC
		public static explicit operator byte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Byte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002A72C File Offset: 0x0002892C
		[CLSCompliant(false)]
		public static explicit operator sbyte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to SByte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002A77C File Offset: 0x0002897C
		[NullableContext(2)]
		public static explicit operator int?(JToken value)
		{
			if (value == null)
			{
				return default(int?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(int?);
			}
			return new int?(Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002A7F0 File Offset: 0x000289F0
		[NullableContext(2)]
		public static explicit operator short?(JToken value)
		{
			if (value == null)
			{
				return default(short?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(short?);
			}
			return new short?(Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002A864 File Offset: 0x00028A64
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator ushort?(JToken value)
		{
			if (value == null)
			{
				return default(ushort?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(ushort?);
			}
			return new ushort?(Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002A8D8 File Offset: 0x00028AD8
		[NullableContext(2)]
		public static explicit operator byte?(JToken value)
		{
			if (value == null)
			{
				return default(byte?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Byte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(byte?);
			}
			return new byte?(Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002A94C File Offset: 0x00028B4C
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator sbyte?(JToken value)
		{
			if (value == null)
			{
				return default(sbyte?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to SByte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(sbyte?);
			}
			return new sbyte?(Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002A9C0 File Offset: 0x00028BC0
		public static explicit operator DateTime(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				return ((DateTimeOffset)value2).DateTime;
			}
			return Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002AA30 File Offset: 0x00028C30
		[NullableContext(2)]
		public static explicit operator long?(JToken value)
		{
			if (value == null)
			{
				return default(long?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(long?);
			}
			return new long?(Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002AAA4 File Offset: 0x00028CA4
		[NullableContext(2)]
		public static explicit operator float?(JToken value)
		{
			if (value == null)
			{
				return default(float?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(float?);
			}
			return new float?(Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002AB18 File Offset: 0x00028D18
		public static explicit operator decimal(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002AB68 File Offset: 0x00028D68
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator uint?(JToken value)
		{
			if (value == null)
			{
				return default(uint?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(uint?);
			}
			return new uint?(Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002ABDC File Offset: 0x00028DDC
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator ulong?(JToken value)
		{
			if (value == null)
			{
				return default(ulong?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(ulong?);
			}
			return new ulong?(Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002AC50 File Offset: 0x00028E50
		public static explicit operator double(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002ACA0 File Offset: 0x00028EA0
		public static explicit operator float(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002ACF0 File Offset: 0x00028EF0
		[NullableContext(2)]
		public static explicit operator string(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.StringTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to String.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return Convert.ToBase64String(array);
			}
			return Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002AD68 File Offset: 0x00028F68
		[CLSCompliant(false)]
		public static explicit operator uint(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002ADB8 File Offset: 0x00028FB8
		[CLSCompliant(false)]
		public static explicit operator ulong(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002AE08 File Offset: 0x00029008
		[NullableContext(2)]
		public static explicit operator byte[](JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BytesTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is string)
			{
				return Convert.FromBase64String(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return array;
			}
			throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002AE9C File Offset: 0x0002909C
		public static explicit operator Guid(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Guid.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return new Guid(array);
			}
			object value2 = jvalue.Value;
			if (value2 is Guid)
			{
				return (Guid)value2;
			}
			return new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002AF24 File Offset: 0x00029124
		[NullableContext(2)]
		public static explicit operator Guid?(JToken value)
		{
			if (value == null)
			{
				return default(Guid?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Guid.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(Guid?);
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return new Guid?(new Guid(array));
			}
			object value2 = jvalue.Value;
			Guid guid2;
			if (value2 is Guid)
			{
				Guid guid = (Guid)value2;
				guid2 = guid;
			}
			else
			{
				guid2 = new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return new Guid?(guid2);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002AFD8 File Offset: 0x000291D8
		public static explicit operator TimeSpan(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to TimeSpan.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is TimeSpan)
			{
				return (TimeSpan)value2;
			}
			return ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002B048 File Offset: 0x00029248
		[NullableContext(2)]
		public static explicit operator TimeSpan?(JToken value)
		{
			if (value == null)
			{
				return default(TimeSpan?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to TimeSpan.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(TimeSpan?);
			}
			object value2 = jvalue.Value;
			TimeSpan timeSpan2;
			if (value2 is TimeSpan)
			{
				TimeSpan timeSpan = (TimeSpan)value2;
				timeSpan2 = timeSpan;
			}
			else
			{
				timeSpan2 = ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return new TimeSpan?(timeSpan2);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002B0DC File Offset: 0x000292DC
		[NullableContext(2)]
		public static explicit operator Uri(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.UriTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Uri.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			Uri uri = jvalue.Value as Uri;
			if (uri == null)
			{
				return new Uri(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return uri;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002B151 File Offset: 0x00029351
		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002B159 File Offset: 0x00029359
		public static implicit operator JToken(DateTimeOffset value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002B161 File Offset: 0x00029361
		public static implicit operator JToken(byte value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002B16A File Offset: 0x0002936A
		public static implicit operator JToken(byte? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002B177 File Offset: 0x00029377
		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte value)
		{
			return new JValue((long)value);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002B180 File Offset: 0x00029380
		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002B18D File Offset: 0x0002938D
		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002B19A File Offset: 0x0002939A
		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002B1A2 File Offset: 0x000293A2
		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002B1AF File Offset: 0x000293AF
		public static implicit operator JToken(DateTimeOffset? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002B1BC File Offset: 0x000293BC
		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002B1C9 File Offset: 0x000293C9
		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002B1D6 File Offset: 0x000293D6
		[CLSCompliant(false)]
		public static implicit operator JToken(short value)
		{
			return new JValue((long)value);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002B1DF File Offset: 0x000293DF
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002B1E8 File Offset: 0x000293E8
		public static implicit operator JToken(int value)
		{
			return new JValue((long)value);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002B1F1 File Offset: 0x000293F1
		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002B1FE File Offset: 0x000293FE
		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002B206 File Offset: 0x00029406
		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002B213 File Offset: 0x00029413
		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002B220 File Offset: 0x00029420
		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002B228 File Offset: 0x00029428
		[CLSCompliant(false)]
		public static implicit operator JToken(short? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002B235 File Offset: 0x00029435
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002B242 File Offset: 0x00029442
		[CLSCompliant(false)]
		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002B24F File Offset: 0x0002944F
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002B25C File Offset: 0x0002945C
		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002B264 File Offset: 0x00029464
		public static implicit operator JToken(float value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002B26C File Offset: 0x0002946C
		public static implicit operator JToken([Nullable(2)] string value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002B274 File Offset: 0x00029474
		[CLSCompliant(false)]
		public static implicit operator JToken(uint value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002B27D File Offset: 0x0002947D
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002B285 File Offset: 0x00029485
		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002B28D File Offset: 0x0002948D
		public static implicit operator JToken([Nullable(2)] Uri value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002B295 File Offset: 0x00029495
		public static implicit operator JToken(TimeSpan value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002B29D File Offset: 0x0002949D
		public static implicit operator JToken(TimeSpan? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002B2AA File Offset: 0x000294AA
		public static implicit operator JToken(Guid value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002B2B2 File Offset: 0x000294B2
		public static implicit operator JToken(Guid? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002B2BF File Offset: 0x000294BF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002B2C8 File Offset: 0x000294C8
		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x06000B12 RID: 2834
		internal abstract int GetDeepHashCode();

		// Token: 0x170001FA RID: 506
		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key]
		{
			get
			{
				return this[key];
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002B2EC File Offset: 0x000294EC
		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002B2F4 File Offset: 0x000294F4
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		internal static JToken FromObjectInternal(object o, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jsonSerializer.Serialize(jtokenWriter, o);
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002B34C File Offset: 0x0002954C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static JToken FromObject(object o)
		{
			return JToken.FromObjectInternal(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002B359 File Offset: 0x00029559
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return JToken.FromObjectInternal(o, jsonSerializer);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002B362 File Offset: 0x00029562
		[NullableContext(2)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public T ToObject<T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002B37C File Offset: 0x0002957C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public object ToObject(Type objectType)
		{
			if (JsonConvert.DefaultSettings == null)
			{
				bool flag;
				PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(objectType, out flag);
				if (flag)
				{
					if (this.Type == JTokenType.String)
					{
						try
						{
							return this.ToObject(objectType, JsonSerializer.CreateDefault());
						}
						catch (Exception ex)
						{
							Type type = objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType);
							throw new ArgumentException("Could not convert '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, (string)this, type.Name), ex);
						}
					}
					if (this.Type == JTokenType.Integer)
					{
						return Enum.ToObject(objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType), ((JValue)this).Value);
					}
				}
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					return (char)this;
				case PrimitiveTypeCode.CharNullable:
					return (char?)this;
				case PrimitiveTypeCode.Boolean:
					return (bool)this;
				case PrimitiveTypeCode.BooleanNullable:
					return (bool?)this;
				case PrimitiveTypeCode.SByte:
					return (sbyte)this;
				case PrimitiveTypeCode.SByteNullable:
					return (sbyte?)this;
				case PrimitiveTypeCode.Int16:
					return (short)this;
				case PrimitiveTypeCode.Int16Nullable:
					return (short?)this;
				case PrimitiveTypeCode.UInt16:
					return (ushort)this;
				case PrimitiveTypeCode.UInt16Nullable:
					return (ushort?)this;
				case PrimitiveTypeCode.Int32:
					return (int)this;
				case PrimitiveTypeCode.Int32Nullable:
					return (int?)this;
				case PrimitiveTypeCode.Byte:
					return (byte)this;
				case PrimitiveTypeCode.ByteNullable:
					return (byte?)this;
				case PrimitiveTypeCode.UInt32:
					return (uint)this;
				case PrimitiveTypeCode.UInt32Nullable:
					return (uint?)this;
				case PrimitiveTypeCode.Int64:
					return (long)this;
				case PrimitiveTypeCode.Int64Nullable:
					return (long?)this;
				case PrimitiveTypeCode.UInt64:
					return (ulong)this;
				case PrimitiveTypeCode.UInt64Nullable:
					return (ulong?)this;
				case PrimitiveTypeCode.Single:
					return (float)this;
				case PrimitiveTypeCode.SingleNullable:
					return (float?)this;
				case PrimitiveTypeCode.Double:
					return (double)this;
				case PrimitiveTypeCode.DoubleNullable:
					return (double?)this;
				case PrimitiveTypeCode.DateTime:
					return (DateTime)this;
				case PrimitiveTypeCode.DateTimeNullable:
					return (DateTime?)this;
				case PrimitiveTypeCode.DateTimeOffset:
					return (DateTimeOffset)this;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					return (DateTimeOffset?)this;
				case PrimitiveTypeCode.Decimal:
					return (decimal)this;
				case PrimitiveTypeCode.DecimalNullable:
					return (decimal?)this;
				case PrimitiveTypeCode.Guid:
					return (Guid)this;
				case PrimitiveTypeCode.GuidNullable:
					return (Guid?)this;
				case PrimitiveTypeCode.TimeSpan:
					return (TimeSpan)this;
				case PrimitiveTypeCode.TimeSpanNullable:
					return (TimeSpan?)this;
				case PrimitiveTypeCode.Uri:
					return (Uri)this;
				case PrimitiveTypeCode.String:
					return (string)this;
				}
			}
			return this.ToObject(objectType, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002B688 File Offset: 0x00029888
		[NullableContext(2)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public T ToObject<T>([Nullable(1)] JsonSerializer jsonSerializer)
		{
			return (T)((object)this.ToObject(typeof(T), jsonSerializer));
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002B6A0 File Offset: 0x000298A0
		[NullableContext(2)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public object ToObject(Type objectType, [Nullable(1)] JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			object result;
			using (JTokenReader jtokenReader = new JTokenReader(this))
			{
				JsonSerializerProxy jsonSerializerProxy = jsonSerializer as JsonSerializerProxy;
				if (jsonSerializerProxy != null)
				{
					CultureInfo cultureInfo;
					DateTimeZoneHandling? dateTimeZoneHandling;
					DateParseHandling? dateParseHandling;
					FloatParseHandling? floatParseHandling;
					int? num;
					string text;
					jsonSerializerProxy._serializer.SetupReader(jtokenReader, out cultureInfo, out dateTimeZoneHandling, out dateParseHandling, out floatParseHandling, out num, out text);
				}
				result = jsonSerializer.Deserialize(jtokenReader, objectType);
			}
			return result;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002B70C File Offset: 0x0002990C
		public static JToken ReadFrom(JsonReader reader)
		{
			return JToken.ReadFrom(reader, null);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002B718 File Offset: 0x00029918
		public static JToken ReadFrom(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			bool flag;
			if (reader.TokenType == JsonToken.None)
			{
				flag = ((settings != null && settings.CommentHandling == CommentHandling.Ignore) ? reader.ReadAndMoveToContent() : reader.Read());
			}
			else
			{
				flag = (reader.TokenType != JsonToken.Comment || settings == null || settings.CommentHandling != CommentHandling.Ignore || reader.ReadAndMoveToContent());
			}
			if (!flag)
			{
				throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader.");
			}
			IJsonLineInfo lineInfo = reader as IJsonLineInfo;
			switch (reader.TokenType)
			{
			case JsonToken.StartObject:
				return JObject.Load(reader, settings);
			case JsonToken.StartArray:
				return JArray.Load(reader, settings);
			case JsonToken.StartConstructor:
				return JConstructor.Load(reader, settings);
			case JsonToken.PropertyName:
				return JProperty.Load(reader, settings);
			case JsonToken.Comment:
			{
				JValue jvalue = JValue.CreateComment(reader.Value.ToString());
				jvalue.SetLineInfo(lineInfo, settings);
				return jvalue;
			}
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
			{
				JValue jvalue2 = new JValue(reader.Value);
				jvalue2.SetLineInfo(lineInfo, settings);
				return jvalue2;
			}
			case JsonToken.Null:
			{
				JValue jvalue3 = JValue.CreateNull();
				jvalue3.SetLineInfo(lineInfo, settings);
				return jvalue3;
			}
			case JsonToken.Undefined:
			{
				JValue jvalue4 = JValue.CreateUndefined();
				jvalue4.SetLineInfo(lineInfo, settings);
				return jvalue4;
			}
			}
			throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader. Unexpected token: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002B867 File Offset: 0x00029A67
		public static JToken Parse(string json)
		{
			return JToken.Parse(json, null);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002B870 File Offset: 0x00029A70
		public static JToken Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JToken result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JToken jtoken = JToken.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jtoken;
			}
			return result;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002B8B8 File Offset: 0x00029AB8
		public static JToken Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			return JToken.ReadFrom(reader, settings);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002B8C1 File Offset: 0x00029AC1
		public static JToken Load(JsonReader reader)
		{
			return JToken.Load(reader, null);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002B8CA File Offset: 0x00029ACA
		[NullableContext(2)]
		internal void SetLineInfo(IJsonLineInfo lineInfo, JsonLoadSettings settings)
		{
			if (settings != null && settings.LineInfoHandling != LineInfoHandling.Load)
			{
				return;
			}
			if (lineInfo == null || !lineInfo.HasLineInfo())
			{
				return;
			}
			this.SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002B8F7 File Offset: 0x00029AF7
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this.AddAnnotation(new JToken.LineInfoAnnotation(lineNumber, linePosition));
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002B906 File Offset: 0x00029B06
		bool IJsonLineInfo.HasLineInfo()
		{
			return this.Annotation<JToken.LineInfoAnnotation>() != null;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002B914 File Offset: 0x00029B14
		int IJsonLineInfo.LineNumber
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002B934 File Offset: 0x00029B34
		int IJsonLineInfo.LinePosition
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002B953 File Offset: 0x00029B53
		[return: Nullable(2)]
		public JToken SelectToken(string path)
		{
			return this.SelectToken(path, null);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002B960 File Offset: 0x00029B60
		[return: Nullable(2)]
		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			object obj;
			if (!errorWhenNoMatch)
			{
				obj = null;
			}
			else
			{
				(obj = new JsonSelectSettings()).ErrorWhenNoMatch = true;
			}
			JsonSelectSettings settings = obj;
			return this.SelectToken(path, settings);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002B988 File Offset: 0x00029B88
		[NullableContext(2)]
		public JToken SelectToken([Nullable(1)] string path, JsonSelectSettings settings)
		{
			JPath jpath = new JPath(path);
			JToken jtoken = null;
			foreach (JToken jtoken2 in jpath.Evaluate(this, this, settings))
			{
				if (jtoken != null)
				{
					throw new JsonException("Path returned multiple tokens.");
				}
				jtoken = jtoken2;
			}
			return jtoken;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002B9E8 File Offset: 0x00029BE8
		public IEnumerable<JToken> SelectTokens(string path)
		{
			return this.SelectTokens(path, null);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002B9F4 File Offset: 0x00029BF4
		public IEnumerable<JToken> SelectTokens(string path, bool errorWhenNoMatch)
		{
			object obj;
			if (!errorWhenNoMatch)
			{
				obj = null;
			}
			else
			{
				(obj = new JsonSelectSettings()).ErrorWhenNoMatch = true;
			}
			JsonSelectSettings settings = obj;
			return this.SelectTokens(path, settings);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002BA1C File Offset: 0x00029C1C
		public IEnumerable<JToken> SelectTokens(string path, [Nullable(2)] JsonSelectSettings settings)
		{
			return new JPath(path).Evaluate(this, this, settings);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002BA2C File Offset: 0x00029C2C
		protected virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JToken>(parameter, this, new DynamicProxy<JToken>());
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002BA3A File Offset: 0x00029C3A
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return this.GetMetaObject(parameter);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002BA43 File Offset: 0x00029C43
		public JToken DeepClone()
		{
			return this.CloneToken(null);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002BA4C File Offset: 0x00029C4C
		public JToken DeepClone(JsonCloneSettings settings)
		{
			return this.CloneToken(settings);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002BA58 File Offset: 0x00029C58
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this._annotations == null)
			{
				object annotations;
				if (!(annotation is object[]))
				{
					annotations = annotation;
				}
				else
				{
					(annotations = new object[1])[0] = annotation;
				}
				this._annotations = annotations;
				return;
			}
			object[] array = this._annotations as object[];
			if (array == null)
			{
				this._annotations = new object[]
				{
					this._annotations,
					annotation
				};
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this._annotations = array;
			}
			array[num] = annotation;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002BAF0 File Offset: 0x00029CF0
		[return: Nullable(2)]
		public T Annotation<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					return this._annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002BB5C File Offset: 0x00029D5C
		[return: Nullable(2)]
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						return this._annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		public IEnumerable<T> Annotations<T>() where T : class
		{
			JToken.<Annotations>d__185<T> <Annotations>d__ = new JToken.<Annotations>d__185<T>(-2);
			<Annotations>d__.<>4__this = this;
			return <Annotations>d__;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002BBD4 File Offset: 0x00029DD4
		public IEnumerable<object> Annotations(Type type)
		{
			JToken.<Annotations>d__186 <Annotations>d__ = new JToken.<Annotations>d__186(-2);
			<Annotations>d__.<>4__this = this;
			<Annotations>d__.<>3__type = type;
			return <Annotations>d__;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002BBEC File Offset: 0x00029DEC
		public void RemoveAnnotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (this._annotations is T)
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002BC68 File Offset: 0x00029E68
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!type.IsInstanceOfType(obj))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002BCF4 File Offset: 0x00029EF4
		internal void CopyAnnotations(JToken target, JToken source)
		{
			object[] array = source._annotations as object[];
			if (array != null)
			{
				target._annotations = Enumerable.ToArray<object>(array);
				return;
			}
			target._annotations = source._annotations;
		}

		// Token: 0x040003C8 RID: 968
		[Nullable(2)]
		private static JTokenEqualityComparer _equalityComparer;

		// Token: 0x040003C9 RID: 969
		[Nullable(2)]
		private JContainer _parent;

		// Token: 0x040003CA RID: 970
		[Nullable(2)]
		private JToken _previous;

		// Token: 0x040003CB RID: 971
		[Nullable(2)]
		private JToken _next;

		// Token: 0x040003CC RID: 972
		[Nullable(2)]
		private object _annotations;

		// Token: 0x040003CD RID: 973
		private static readonly JTokenType[] BooleanTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x040003CE RID: 974
		private static readonly JTokenType[] NumberTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x040003CF RID: 975
		private static readonly JTokenType[] StringTypes = new JTokenType[]
		{
			JTokenType.Date,
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean,
			JTokenType.Bytes,
			JTokenType.Guid,
			JTokenType.TimeSpan,
			JTokenType.Uri
		};

		// Token: 0x040003D0 RID: 976
		private static readonly JTokenType[] GuidTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Guid,
			JTokenType.Bytes
		};

		// Token: 0x040003D1 RID: 977
		private static readonly JTokenType[] TimeSpanTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.TimeSpan
		};

		// Token: 0x040003D2 RID: 978
		private static readonly JTokenType[] UriTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Uri
		};

		// Token: 0x040003D3 RID: 979
		private static readonly JTokenType[] CharTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x040003D4 RID: 980
		private static readonly JTokenType[] DateTimeTypes = new JTokenType[]
		{
			JTokenType.Date,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x040003D5 RID: 981
		private static readonly JTokenType[] BytesTypes = new JTokenType[]
		{
			JTokenType.Bytes,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Integer
		};

		// Token: 0x020001DA RID: 474
		[NullableContext(0)]
		private class LineInfoAnnotation
		{
			// Token: 0x06000F57 RID: 3927 RVA: 0x00042FEE File Offset: 0x000411EE
			public LineInfoAnnotation(int lineNumber, int linePosition)
			{
				this.LineNumber = lineNumber;
				this.LinePosition = linePosition;
			}

			// Token: 0x04000812 RID: 2066
			internal readonly int LineNumber;

			// Token: 0x04000813 RID: 2067
			internal readonly int LinePosition;
		}
	}
}

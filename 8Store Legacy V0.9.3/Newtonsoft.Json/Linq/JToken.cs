using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents an abstract JSON token.
	/// </summary>
	// Token: 0x02000055 RID: 85
	public abstract class JToken : IJEnumerable<JToken>, IEnumerable<JToken>, IEnumerable, IJsonLineInfo, IDynamicMetaObjectProvider
	{
		/// <summary>
		/// Gets a comparer that can compare two tokens for value equality.
		/// </summary>
		/// <value>A <see cref="T:Newtonsoft.Json.Linq.JTokenEqualityComparer" /> that can compare two nodes for value equality.</value>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000F91B File Offset: 0x0000DB1B
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

		/// <summary>
		/// Gets or sets the parent.
		/// </summary>
		/// <value>The parent.</value>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000F933 File Offset: 0x0000DB33
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000F93B File Offset: 0x0000DB3B
		public JContainer Parent
		{
			[DebuggerStepThrough]
			get
			{
				return this._parent;
			}
			internal set
			{
				this._parent = value;
			}
		}

		/// <summary>
		/// Gets the root <see cref="T:Newtonsoft.Json.Linq.JToken" /> of this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <value>The root <see cref="T:Newtonsoft.Json.Linq.JToken" /> of this <see cref="T:Newtonsoft.Json.Linq.JToken" />.</value>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000F944 File Offset: 0x0000DB44
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

		// Token: 0x060003FF RID: 1023
		internal abstract JToken CloneToken();

		// Token: 0x06000400 RID: 1024
		internal abstract bool DeepEquals(JToken node);

		/// <summary>
		/// Gets the node type for this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <value>The type.</value>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000401 RID: 1025
		public abstract JTokenType Type { get; }

		/// <summary>
		/// Gets a value indicating whether this token has childen tokens.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this token has child values; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000402 RID: 1026
		public abstract bool HasValues { get; }

		/// <summary>
		/// Compares the values of two tokens, including the values of all descendant tokens.
		/// </summary>
		/// <param name="t1">The first <see cref="T:Newtonsoft.Json.Linq.JToken" /> to compare.</param>
		/// <param name="t2">The second <see cref="T:Newtonsoft.Json.Linq.JToken" /> to compare.</param>
		/// <returns>true if the tokens are equal; otherwise false.</returns>
		// Token: 0x06000403 RID: 1027 RVA: 0x0000F96D File Offset: 0x0000DB6D
		public static bool DeepEquals(JToken t1, JToken t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.DeepEquals(t2));
		}

		/// <summary>
		/// Gets the next sibling token of this node.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the next sibling token.</value>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000F984 File Offset: 0x0000DB84
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000F98C File Offset: 0x0000DB8C
		public JToken Next
		{
			get
			{
				return this._next;
			}
			internal set
			{
				this._next = value;
			}
		}

		/// <summary>
		/// Gets the previous sibling token of this node.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the previous sibling token.</value>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000F995 File Offset: 0x0000DB95
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000F99D File Offset: 0x0000DB9D
		public JToken Previous
		{
			get
			{
				return this._previous;
			}
			internal set
			{
				this._previous = value;
			}
		}

		/// <summary>
		/// Gets the path of the JSON token. 
		/// </summary>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		public string Path
		{
			get
			{
				if (this.Parent == null)
				{
					return string.Empty;
				}
				IList<JToken> list = Enumerable.ToList<JToken>(Enumerable.Reverse<JToken>(this.Ancestors()));
				list.Add(this);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < list.Count; i++)
				{
					JToken jtoken = list[i];
					JToken jtoken2 = (i + 1 < list.Count) ? list[i + 1] : null;
					if (jtoken2 != null)
					{
						switch (jtoken.Type)
						{
						case JTokenType.Array:
						case JTokenType.Constructor:
						{
							int num = ((IList<JToken>)jtoken).IndexOf(jtoken2);
							stringBuilder.Append("[" + num + "]");
							break;
						}
						case JTokenType.Property:
						{
							JProperty jproperty = (JProperty)jtoken;
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(".");
							}
							stringBuilder.Append(jproperty.Name);
							break;
						}
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000FA9B File Offset: 0x0000DC9B
		internal JToken()
		{
		}

		/// <summary>
		/// Adds the specified content immediately after this token.
		/// </summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added after this token.</param>
		// Token: 0x0600040A RID: 1034 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public void AddAfterSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num + 1, content, false);
		}

		/// <summary>
		/// Adds the specified content immediately before this token.
		/// </summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added before this token.</param>
		// Token: 0x0600040B RID: 1035 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		public void AddBeforeSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int index = this._parent.IndexOfItem(this);
			this._parent.AddInternal(index, content, false);
		}

		/// <summary>
		/// Returns a collection of the ancestor tokens of this token.
		/// </summary>
		/// <returns>A collection of the ancestor tokens of this token.</returns>
		// Token: 0x0600040C RID: 1036 RVA: 0x0000FC14 File Offset: 0x0000DE14
		public IEnumerable<JToken> Ancestors()
		{
			for (JToken parent = this.Parent; parent != null; parent = parent.Parent)
			{
				yield return parent;
			}
			yield break;
		}

		/// <summary>
		/// Returns a collection of the sibling tokens after this token, in document order.
		/// </summary>
		/// <returns>A collection of the sibling tokens after this tokens, in document order.</returns>
		// Token: 0x0600040D RID: 1037 RVA: 0x0000FD34 File Offset: 0x0000DF34
		public IEnumerable<JToken> AfterSelf()
		{
			if (this.Parent != null)
			{
				for (JToken o = this.Next; o != null; o = o.Next)
				{
					yield return o;
				}
			}
			yield break;
		}

		/// <summary>
		/// Returns a collection of the sibling tokens before this token, in document order.
		/// </summary>
		/// <returns>A collection of the sibling tokens before this token, in document order.</returns>
		// Token: 0x0600040E RID: 1038 RVA: 0x0000FE54 File Offset: 0x0000E054
		public IEnumerable<JToken> BeforeSelf()
		{
			for (JToken o = this.Parent.First; o != this; o = o.Next)
			{
				yield return o;
			}
			yield break;
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.</value>
		// Token: 0x170000E6 RID: 230
		public virtual JToken this[object key]
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			set
			{
				throw new InvalidOperationException("Cannot set child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key converted to the specified type.
		/// </summary>
		/// <typeparam name="T">The type to convert the token to.</typeparam>
		/// <param name="key">The token key.</param>
		/// <returns>The converted token value.</returns>
		// Token: 0x06000411 RID: 1041 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		public virtual T Value<T>(object key)
		{
			JToken token = this[key];
			return token.Convert<JToken, T>();
		}

		/// <summary>
		/// Get the first child token of this token.
		/// </summary>
		/// <value>A <see cref="T:Newtonsoft.Json.Linq.JToken" /> containing the first child token of the <see cref="T:Newtonsoft.Json.Linq.JToken" />.</value>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000FEC7 File Offset: 0x0000E0C7
		public virtual JToken First
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		/// <summary>
		/// Get the last child token of this token.
		/// </summary>
		/// <value>A <see cref="T:Newtonsoft.Json.Linq.JToken" /> containing the last child token of the <see cref="T:Newtonsoft.Json.Linq.JToken" />.</value>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000FEE3 File Offset: 0x0000E0E3
		public virtual JToken Last
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		/// <summary>
		/// Returns a collection of the child tokens of this token, in document order.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> containing the child tokens of this <see cref="T:Newtonsoft.Json.Linq.JToken" />, in document order.</returns>
		// Token: 0x06000414 RID: 1044 RVA: 0x0000FEFF File Offset: 0x0000E0FF
		public virtual JEnumerable<JToken> Children()
		{
			return JEnumerable<JToken>.Empty;
		}

		/// <summary>
		/// Returns a collection of the child tokens of this token, in document order, filtered by the specified type.
		/// </summary>
		/// <typeparam name="T">The type to filter the child tokens on.</typeparam>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JEnumerable`1" /> containing the child tokens of this <see cref="T:Newtonsoft.Json.Linq.JToken" />, in document order.</returns>
		// Token: 0x06000415 RID: 1045 RVA: 0x0000FF06 File Offset: 0x0000E106
		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(Enumerable.OfType<T>(this.Children()));
		}

		/// <summary>
		/// Returns a collection of the child values of this token, in document order.
		/// </summary>
		/// <typeparam name="T">The type to convert the values to.</typeparam>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerable`1" /> containing the child values of this <see cref="T:Newtonsoft.Json.Linq.JToken" />, in document order.</returns>
		// Token: 0x06000416 RID: 1046 RVA: 0x0000FF1D File Offset: 0x0000E11D
		public virtual IEnumerable<T> Values<T>()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
		}

		/// <summary>
		/// Removes this token from its parent.
		/// </summary>
		// Token: 0x06000417 RID: 1047 RVA: 0x0000FF39 File Offset: 0x0000E139
		public void Remove()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.RemoveItem(this);
		}

		/// <summary>
		/// Replaces this token with the specified token.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x06000418 RID: 1048 RVA: 0x0000FF5B File Offset: 0x0000E15B
		public void Replace(JToken value)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.ReplaceItem(this, value);
		}

		/// <summary>
		/// Writes this token to a <see cref="T:Newtonsoft.Json.JsonWriter" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		/// <param name="converters">A collection of <see cref="T:Newtonsoft.Json.JsonConverter" /> which will be used when writing the token.</param>
		// Token: 0x06000419 RID: 1049
		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		/// <summary>
		/// Returns the indented JSON for this token.
		/// </summary>
		/// <returns>
		/// The indented JSON for this token.
		/// </returns>
		// Token: 0x0600041A RID: 1050 RVA: 0x0000FF7D File Offset: 0x0000E17D
		public override string ToString()
		{
			return this.ToString(Formatting.Indented, new JsonConverter[0]);
		}

		/// <summary>
		/// Returns the JSON for this token using the given formatting and converters.
		/// </summary>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <param name="converters">A collection of <see cref="T:Newtonsoft.Json.JsonConverter" /> which will be used when writing the token.</param>
		/// <returns>The JSON for this token using the given formatting and converters.</returns>
		// Token: 0x0600041B RID: 1051 RVA: 0x0000FF8C File Offset: 0x0000E18C
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

		// Token: 0x0600041C RID: 1052 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is JProperty)
			{
				value = ((JProperty)value).Value;
			}
			return value as JValue;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00010018 File Offset: 0x0000E218
		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			if (token is JProperty)
			{
				token = ((JProperty)token).Value;
			}
			return token.Type.ToString();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001004A File Offset: 0x0000E24A
		private static bool ValidateToken(JToken o, JTokenType[] validTypes, bool nullable)
		{
			return Array.IndexOf<JTokenType>(validTypes, o.Type) != -1 || (nullable && (o.Type == JTokenType.Null || o.Type == JTokenType.Undefined));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Boolean" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600041F RID: 1055 RVA: 0x00010078 File Offset: 0x0000E278
		public static explicit operator bool(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return Convert.ToBoolean((int)((BigInteger)jvalue.Value));
			}
			return Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.DateTimeOffset" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000420 RID: 1056 RVA: 0x000100EC File Offset: 0x0000E2EC
		public static explicit operator DateTimeOffset(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is DateTimeOffset)
			{
				return (DateTimeOffset)jvalue.Value;
			}
			if (jvalue.Value is string)
			{
				return DateTimeOffset.Parse((string)jvalue.Value, CultureInfo.InvariantCulture);
			}
			return new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000421 RID: 1057 RVA: 0x00010180 File Offset: 0x0000E380
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
			if (jvalue.Value is BigInteger)
			{
				return new bool?(Convert.ToBoolean((int)((BigInteger)jvalue.Value)));
			}
			if (jvalue.Value == null)
			{
				return default(bool?);
			}
			return new bool?(Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Int64" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000422 RID: 1058 RVA: 0x0001021C File Offset: 0x0000E41C
		public static explicit operator long(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (long)((BigInteger)jvalue.Value);
			}
			return Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000423 RID: 1059 RVA: 0x0001028C File Offset: 0x0000E48C
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
			if (jvalue.Value is DateTimeOffset)
			{
				return new DateTime?(((DateTimeOffset)jvalue.Value).DateTime);
			}
			if (jvalue.Value == null)
			{
				return default(DateTime?);
			}
			return new DateTime?(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000424 RID: 1060 RVA: 0x00010328 File Offset: 0x0000E528
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
			if (jvalue.Value is DateTimeOffset)
			{
				return (DateTimeOffset?)jvalue.Value;
			}
			if (jvalue.Value is string)
			{
				return new DateTimeOffset?(DateTimeOffset.Parse((string)jvalue.Value, CultureInfo.InvariantCulture));
			}
			return new DateTimeOffset?(new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000425 RID: 1061 RVA: 0x000103E4 File Offset: 0x0000E5E4
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
			if (jvalue.Value is BigInteger)
			{
				return new decimal?((decimal)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(decimal?);
			}
			return new decimal?(Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000426 RID: 1062 RVA: 0x0001047C File Offset: 0x0000E67C
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
			if (jvalue.Value is BigInteger)
			{
				return new double?((double)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(double?);
			}
			return new double?(Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000427 RID: 1063 RVA: 0x00010514 File Offset: 0x0000E714
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
			if (jvalue.Value is BigInteger)
			{
				return new char?((char)((ushort)((BigInteger)jvalue.Value)));
			}
			if (jvalue.Value == null)
			{
				return default(char?);
			}
			return new char?(Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Int32" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000428 RID: 1064 RVA: 0x000105AC File Offset: 0x0000E7AC
		public static explicit operator int(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (int)((BigInteger)jvalue.Value);
			}
			return Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Int16" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000429 RID: 1065 RVA: 0x0001061C File Offset: 0x0000E81C
		public static explicit operator short(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (short)((BigInteger)jvalue.Value);
			}
			return Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.UInt16" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600042A RID: 1066 RVA: 0x0001068C File Offset: 0x0000E88C
		[CLSCompliant(false)]
		public static explicit operator ushort(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (ushort)((BigInteger)jvalue.Value);
			}
			return Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Char" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600042B RID: 1067 RVA: 0x000106FC File Offset: 0x0000E8FC
		[CLSCompliant(false)]
		public static explicit operator char(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Char.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (char)((ushort)((BigInteger)jvalue.Value));
			}
			return Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Byte" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600042C RID: 1068 RVA: 0x0001076C File Offset: 0x0000E96C
		public static explicit operator byte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Byte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (byte)((BigInteger)jvalue.Value);
			}
			return Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600042D RID: 1069 RVA: 0x000107DC File Offset: 0x0000E9DC
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
			if (jvalue.Value is BigInteger)
			{
				return new int?((int)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(int?);
			}
			return new int?(Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600042E RID: 1070 RVA: 0x00010874 File Offset: 0x0000EA74
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
			if (jvalue.Value is BigInteger)
			{
				return new short?((short)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(short?);
			}
			return new short?(Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600042F RID: 1071 RVA: 0x0001090C File Offset: 0x0000EB0C
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
			if (jvalue.Value is BigInteger)
			{
				return new ushort?((ushort)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(ushort?);
			}
			return new ushort?(Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000430 RID: 1072 RVA: 0x000109A4 File Offset: 0x0000EBA4
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
			if (jvalue.Value is BigInteger)
			{
				return new byte?((byte)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(byte?);
			}
			return new byte?(Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.DateTime" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000431 RID: 1073 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public static explicit operator DateTime(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is DateTimeOffset)
			{
				return ((DateTimeOffset)jvalue.Value).DateTime;
			}
			return Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000432 RID: 1074 RVA: 0x00010AB0 File Offset: 0x0000ECB0
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
			if (jvalue.Value is BigInteger)
			{
				return new long?((long)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(long?);
			}
			return new long?(Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000433 RID: 1075 RVA: 0x00010B48 File Offset: 0x0000ED48
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
			if (jvalue.Value is BigInteger)
			{
				return new float?((float)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(float?);
			}
			return new float?(Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Decimal" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000434 RID: 1076 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		public static explicit operator decimal(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (decimal)((BigInteger)jvalue.Value);
			}
			return Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000435 RID: 1077 RVA: 0x00010C50 File Offset: 0x0000EE50
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
			if (jvalue.Value is BigInteger)
			{
				return new uint?((uint)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(uint?);
			}
			return new uint?(Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000436 RID: 1078 RVA: 0x00010CE8 File Offset: 0x0000EEE8
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
			if (jvalue.Value is BigInteger)
			{
				return new ulong?((ulong)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(ulong?);
			}
			return new ulong?(Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Double" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000437 RID: 1079 RVA: 0x00010D80 File Offset: 0x0000EF80
		public static explicit operator double(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (double)((BigInteger)jvalue.Value);
			}
			return Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Single" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000438 RID: 1080 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		public static explicit operator float(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (float)((BigInteger)jvalue.Value);
			}
			return Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.String" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000439 RID: 1081 RVA: 0x00010E60 File Offset: 0x0000F060
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
			if (jvalue.Value is byte[])
			{
				return Convert.ToBase64String((byte[])jvalue.Value);
			}
			if (jvalue.Value is BigInteger)
			{
				return ((BigInteger)jvalue.Value).ToString(CultureInfo.InvariantCulture);
			}
			return Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.UInt32" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600043A RID: 1082 RVA: 0x00010F04 File Offset: 0x0000F104
		[CLSCompliant(false)]
		public static explicit operator uint(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (uint)((BigInteger)jvalue.Value);
			}
			return Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.UInt64" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600043B RID: 1083 RVA: 0x00010F74 File Offset: 0x0000F174
		[CLSCompliant(false)]
		public static explicit operator ulong(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (ulong)((BigInteger)jvalue.Value);
			}
			return Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Byte[]" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600043C RID: 1084 RVA: 0x00010FE4 File Offset: 0x0000F1E4
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
			if (jvalue.Value is BigInteger)
			{
				return ((BigInteger)jvalue.Value).ToByteArray();
			}
			if (jvalue.Value is byte[])
			{
				return (byte[])jvalue.Value;
			}
			throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Guid" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600043D RID: 1085 RVA: 0x000110A0 File Offset: 0x0000F2A0
		public static explicit operator Guid(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Guid.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is byte[])
			{
				return new Guid((byte[])jvalue.Value);
			}
			if (!(jvalue.Value is Guid))
			{
				return new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return (Guid)jvalue.Value;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Guid" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600043E RID: 1086 RVA: 0x0001112C File Offset: 0x0000F32C
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
			if (jvalue.Value is byte[])
			{
				return new Guid?(new Guid((byte[])jvalue.Value));
			}
			return new Guid?((jvalue.Value is Guid) ? ((Guid)jvalue.Value) : new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.TimeSpan" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x0600043F RID: 1087 RVA: 0x000111E4 File Offset: 0x0000F3E4
		public static explicit operator TimeSpan(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to TimeSpan.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (!(jvalue.Value is TimeSpan))
			{
				return ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return (TimeSpan)jvalue.Value;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.TimeSpan" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000440 RID: 1088 RVA: 0x00011254 File Offset: 0x0000F454
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
			return new TimeSpan?((jvalue.Value is TimeSpan) ? ((TimeSpan)jvalue.Value) : ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="T:Newtonsoft.Json.Linq.JToken" /> to <see cref="T:System.Uri" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		// Token: 0x06000441 RID: 1089 RVA: 0x000112E8 File Offset: 0x0000F4E8
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
			if (!(jvalue.Value is Uri))
			{
				return new Uri(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return (Uri)jvalue.Value;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011368 File Offset: 0x0000F568
		private static BigInteger ToBigInteger(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return ConvertUtils.ToBigInteger(jvalue.Value);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000113B4 File Offset: 0x0000F5B4
		private static BigInteger? ToBigIntegerNullable(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(BigInteger?);
			}
			return new BigInteger?(ConvertUtils.ToBigInteger(jvalue.Value));
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Boolean" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000444 RID: 1092 RVA: 0x00011416 File Offset: 0x0000F616
		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.DateTimeOffset" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000445 RID: 1093 RVA: 0x0001141E File Offset: 0x0000F61E
		public static implicit operator JToken(DateTimeOffset value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000446 RID: 1094 RVA: 0x0001142B File Offset: 0x0000F62B
		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000447 RID: 1095 RVA: 0x00011438 File Offset: 0x0000F638
		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000448 RID: 1096 RVA: 0x00011440 File Offset: 0x0000F640
		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000449 RID: 1097 RVA: 0x0001144D File Offset: 0x0000F64D
		public static implicit operator JToken(DateTimeOffset? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600044A RID: 1098 RVA: 0x0001145A File Offset: 0x0000F65A
		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600044B RID: 1099 RVA: 0x00011467 File Offset: 0x0000F667
		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Int16" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600044C RID: 1100 RVA: 0x00011474 File Offset: 0x0000F674
		[CLSCompliant(false)]
		public static implicit operator JToken(short value)
		{
			return new JValue((long)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.UInt16" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600044D RID: 1101 RVA: 0x0001147D File Offset: 0x0000F67D
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort value)
		{
			return new JValue((long)((ulong)value));
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Int32" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600044E RID: 1102 RVA: 0x00011486 File Offset: 0x0000F686
		public static implicit operator JToken(int value)
		{
			return new JValue((long)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600044F RID: 1103 RVA: 0x0001148F File Offset: 0x0000F68F
		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.DateTime" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000450 RID: 1104 RVA: 0x0001149C File Offset: 0x0000F69C
		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000451 RID: 1105 RVA: 0x000114A4 File Offset: 0x0000F6A4
		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000452 RID: 1106 RVA: 0x000114B1 File Offset: 0x0000F6B1
		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Decimal" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000453 RID: 1107 RVA: 0x000114BE File Offset: 0x0000F6BE
		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000454 RID: 1108 RVA: 0x000114CB File Offset: 0x0000F6CB
		[CLSCompliant(false)]
		public static implicit operator JToken(short? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000455 RID: 1109 RVA: 0x000114D8 File Offset: 0x0000F6D8
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000456 RID: 1110 RVA: 0x000114E5 File Offset: 0x0000F6E5
		[CLSCompliant(false)]
		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000457 RID: 1111 RVA: 0x000114F2 File Offset: 0x0000F6F2
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Double" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000458 RID: 1112 RVA: 0x000114FF File Offset: 0x0000F6FF
		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Single" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000459 RID: 1113 RVA: 0x00011507 File Offset: 0x0000F707
		public static implicit operator JToken(float value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.String" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600045A RID: 1114 RVA: 0x0001150F File Offset: 0x0000F70F
		public static implicit operator JToken(string value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.UInt32" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600045B RID: 1115 RVA: 0x00011517 File Offset: 0x0000F717
		[CLSCompliant(false)]
		public static implicit operator JToken(uint value)
		{
			return new JValue((long)((ulong)value));
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.UInt64" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600045C RID: 1116 RVA: 0x00011520 File Offset: 0x0000F720
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Byte[]" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600045D RID: 1117 RVA: 0x00011528 File Offset: 0x0000F728
		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Uri" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600045E RID: 1118 RVA: 0x00011530 File Offset: 0x0000F730
		public static implicit operator JToken(Uri value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.TimeSpan" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x0600045F RID: 1119 RVA: 0x00011538 File Offset: 0x0000F738
		public static implicit operator JToken(TimeSpan value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000460 RID: 1120 RVA: 0x00011540 File Offset: 0x0000F740
		public static implicit operator JToken(TimeSpan? value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Guid" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000461 RID: 1121 RVA: 0x0001154D File Offset: 0x0000F74D
		public static implicit operator JToken(Guid value)
		{
			return new JValue(value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T:System.Nullable`1" /> to <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="value">The value to create a <see cref="T:Newtonsoft.Json.Linq.JValue" /> from.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JValue" /> initialized with the specified value.</returns>
		// Token: 0x06000462 RID: 1122 RVA: 0x00011555 File Offset: 0x0000F755
		public static implicit operator JToken(Guid? value)
		{
			return new JValue(value);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00011562 File Offset: 0x0000F762
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001156C File Offset: 0x0000F76C
		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x06000465 RID: 1125
		internal abstract int GetDeepHashCode();

		// Token: 0x170000E9 RID: 233
		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key]
		{
			get
			{
				return this[key];
			}
		}

		/// <summary>
		/// Creates an <see cref="T:Newtonsoft.Json.JsonReader" /> for this token.
		/// </summary>
		/// <returns>An <see cref="T:Newtonsoft.Json.JsonReader" /> that can be used to read this token and its descendants.</returns>
		// Token: 0x06000467 RID: 1127 RVA: 0x00011590 File Offset: 0x0000F790
		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00011598 File Offset: 0x0000F798
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

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JToken" /> from an object.
		/// </summary>
		/// <param name="o">The object that will be used to create <see cref="T:Newtonsoft.Json.Linq.JToken" />.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the value of the specified object</returns>
		// Token: 0x06000469 RID: 1129 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public static JToken FromObject(object o)
		{
			return JToken.FromObjectInternal(o, JsonSerializer.CreateDefault());
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JToken" /> from an object using the specified <see cref="T:Newtonsoft.Json.JsonSerializer" />.
		/// </summary>
		/// <param name="o">The object that will be used to create <see cref="T:Newtonsoft.Json.Linq.JToken" />.</param>
		/// <param name="jsonSerializer">The <see cref="T:Newtonsoft.Json.JsonSerializer" /> that will be used when reading the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the value of the specified object</returns>
		// Token: 0x0600046A RID: 1130 RVA: 0x000115FD File Offset: 0x0000F7FD
		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return JToken.FromObjectInternal(o, jsonSerializer);
		}

		/// <summary>
		/// Creates the specified .NET type from the <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <typeparam name="T">The object type that the token will be deserialized to.</typeparam>
		/// <returns>The new object created from the JSON value.</returns>
		// Token: 0x0600046B RID: 1131 RVA: 0x00011606 File Offset: 0x0000F806
		public T ToObject<T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		/// <summary>
		/// Creates the specified .NET type from the <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="objectType">The object type that the token will be deserialized to.</param>
		/// <returns>The new object created from the JSON value.</returns>
		// Token: 0x0600046C RID: 1132 RVA: 0x00011620 File Offset: 0x0000F820
		public object ToObject(Type objectType)
		{
			if (JsonConvert.DefaultSettings == null)
			{
				switch (ConvertUtils.GetTypeCode(objectType))
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
				{
					short? num = (short?)this;
					return (num != null) ? new sbyte?((sbyte)num.GetValueOrDefault()) : default(sbyte?);
				}
				case PrimitiveTypeCode.SByteNullable:
					return (sbyte)((short)this);
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
				case PrimitiveTypeCode.BigInteger:
					return JToken.ToBigInteger(this);
				case PrimitiveTypeCode.BigIntegerNullable:
					return JToken.ToBigIntegerNullable(this);
				case PrimitiveTypeCode.Uri:
					return (Uri)this;
				case PrimitiveTypeCode.String:
					return (string)this;
				}
			}
			return this.ToObject(objectType, JsonSerializer.CreateDefault());
		}

		/// <summary>
		/// Creates the specified .NET type from the <see cref="T:Newtonsoft.Json.Linq.JToken" /> using the specified <see cref="T:Newtonsoft.Json.JsonSerializer" />.
		/// </summary>
		/// <typeparam name="T">The object type that the token will be deserialized to.</typeparam>
		/// <param name="jsonSerializer">The <see cref="T:Newtonsoft.Json.JsonSerializer" /> that will be used when creating the object.</param>
		/// <returns>The new object created from the JSON value.</returns>
		// Token: 0x0600046D RID: 1133 RVA: 0x000118D4 File Offset: 0x0000FAD4
		public T ToObject<T>(JsonSerializer jsonSerializer)
		{
			return (T)((object)this.ToObject(typeof(T), jsonSerializer));
		}

		/// <summary>
		/// Creates the specified .NET type from the <see cref="T:Newtonsoft.Json.Linq.JToken" /> using the specified <see cref="T:Newtonsoft.Json.JsonSerializer" />.
		/// </summary>
		/// <param name="objectType">The object type that the token will be deserialized to.</param>
		/// <param name="jsonSerializer">The <see cref="T:Newtonsoft.Json.JsonSerializer" /> that will be used when creating the object.</param>
		/// <returns>The new object created from the JSON value.</returns>
		// Token: 0x0600046E RID: 1134 RVA: 0x000118EC File Offset: 0x0000FAEC
		public object ToObject(Type objectType, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			object result;
			using (JTokenReader jtokenReader = new JTokenReader(this))
			{
				result = jsonSerializer.Deserialize(jtokenReader, objectType);
			}
			return result;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JToken" /> from a <see cref="T:Newtonsoft.Json.JsonReader" />.
		/// </summary>
		/// <param name="reader">An <see cref="T:Newtonsoft.Json.JsonReader" /> positioned at the token to read into this <see cref="T:Newtonsoft.Json.Linq.JToken" />.</param>
		/// <returns>
		/// An <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the token and its descendant tokens
		/// that were read from the reader. The runtime type of the token is determined
		/// by the token type of the first token encountered in the reader.
		/// </returns>
		// Token: 0x0600046F RID: 1135 RVA: 0x00011934 File Offset: 0x0000FB34
		public static JToken ReadFrom(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader.");
			}
			if (reader.TokenType == JsonToken.StartObject)
			{
				return JObject.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				return JArray.Load(reader);
			}
			if (reader.TokenType == JsonToken.PropertyName)
			{
				return JProperty.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartConstructor)
			{
				return JConstructor.Load(reader);
			}
			if (!JsonReader.IsStartToken(reader.TokenType))
			{
				return new JValue(reader.Value);
			}
			throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader. Unexpected token: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		/// <summary>
		/// Load a <see cref="T:Newtonsoft.Json.Linq.JToken" /> from a string that contains JSON.
		/// </summary>
		/// <param name="json">A <see cref="T:System.String" /> that contains JSON.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JToken" /> populated from the string that contains JSON.</returns>
		// Token: 0x06000470 RID: 1136 RVA: 0x000119E4 File Offset: 0x0000FBE4
		public static JToken Parse(string json)
		{
			JsonReader jsonReader = new JsonTextReader(new StringReader(json));
			JToken result = JToken.Load(jsonReader);
			if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
			{
				throw JsonReaderException.Create(jsonReader, "Additional text found in JSON string after parsing content.");
			}
			return result;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JToken" /> from a <see cref="T:Newtonsoft.Json.JsonReader" />.
		/// </summary>
		/// <param name="reader">An <see cref="T:Newtonsoft.Json.JsonReader" /> positioned at the token to read into this <see cref="T:Newtonsoft.Json.Linq.JToken" />.</param>
		/// <returns>
		/// An <see cref="T:Newtonsoft.Json.Linq.JToken" /> that contains the token and its descendant tokens
		/// that were read from the reader. The runtime type of the token is determined
		/// by the token type of the first token encountered in the reader.
		/// </returns>
		// Token: 0x06000471 RID: 1137 RVA: 0x00011A22 File Offset: 0x0000FC22
		public static JToken Load(JsonReader reader)
		{
			return JToken.ReadFrom(reader);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00011A2A File Offset: 0x0000FC2A
		internal void SetLineInfo(IJsonLineInfo lineInfo)
		{
			if (lineInfo == null || !lineInfo.HasLineInfo())
			{
				return;
			}
			this.SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011A4A File Offset: 0x0000FC4A
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this._lineNumber = new int?(lineNumber);
			this._linePosition = new int?(linePosition);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00011A64 File Offset: 0x0000FC64
		bool IJsonLineInfo.HasLineInfo()
		{
			return this._lineNumber != null && this._linePosition != null;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00011A80 File Offset: 0x0000FC80
		int IJsonLineInfo.LineNumber
		{
			get
			{
				int? lineNumber = this._lineNumber;
				if (lineNumber == null)
				{
					return 0;
				}
				return lineNumber.GetValueOrDefault();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		int IJsonLineInfo.LinePosition
		{
			get
			{
				int? linePosition = this._linePosition;
				if (linePosition == null)
				{
					return 0;
				}
				return linePosition.GetValueOrDefault();
			}
		}

		/// <summary>
		/// Selects the token that matches the object path.
		/// </summary>
		/// <param name="path">
		/// The object path from the current <see cref="T:Newtonsoft.Json.Linq.JToken" /> to the <see cref="T:Newtonsoft.Json.Linq.JToken" />
		/// to be returned. This must be a string of property names or array indexes separated
		/// by periods, such as <code>Tables[0].DefaultView[0].Price</code> in C# or
		/// <code>Tables(0).DefaultView(0).Price</code> in Visual Basic.
		/// </param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> that matches the object path or a null reference if no matching token is found.</returns>
		// Token: 0x06000477 RID: 1143 RVA: 0x00011ACE File Offset: 0x0000FCCE
		public JToken SelectToken(string path)
		{
			return this.SelectToken(path, false);
		}

		/// <summary>
		/// Selects the token that matches the object path.
		/// </summary>
		/// <param name="path">
		/// The object path from the current <see cref="T:Newtonsoft.Json.Linq.JToken" /> to the <see cref="T:Newtonsoft.Json.Linq.JToken" />
		/// to be returned. This must be a string of property names or array indexes separated
		/// by periods, such as <code>Tables[0].DefaultView[0].Price</code> in C# or
		/// <code>Tables(0).DefaultView(0).Price</code> in Visual Basic.
		/// </param>
		/// <param name="errorWhenNoMatch">A flag to indicate whether an error should be thrown if no token is found.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> that matches the object path.</returns>
		// Token: 0x06000478 RID: 1144 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			JPath jpath = new JPath(path);
			return jpath.Evaluate(this, errorWhenNoMatch);
		}

		/// <summary>
		/// Returns the <see cref="T:System.Dynamic.DynamicMetaObject" /> responsible for binding operations performed on this object.
		/// </summary>
		/// <param name="parameter">The expression tree representation of the runtime value.</param>
		/// <returns>
		/// The <see cref="T:System.Dynamic.DynamicMetaObject" /> to bind this object.
		/// </returns>
		// Token: 0x06000479 RID: 1145 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		protected virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JToken>(parameter, this, new DynamicProxy<JToken>(), true);
		}

		/// <summary>
		/// Returns the <see cref="T:System.Dynamic.DynamicMetaObject" /> responsible for binding operations performed on this object.
		/// </summary>
		/// <param name="parameter">The expression tree representation of the runtime value.</param>
		/// <returns>
		/// The <see cref="T:System.Dynamic.DynamicMetaObject" /> to bind this object.
		/// </returns>
		// Token: 0x0600047A RID: 1146 RVA: 0x00011B03 File Offset: 0x0000FD03
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return this.GetMetaObject(parameter);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="T:Newtonsoft.Json.Linq.JToken" />. All child tokens are recursively cloned.
		/// </summary>
		/// <returns>A new instance of the <see cref="T:Newtonsoft.Json.Linq.JToken" />.</returns>
		// Token: 0x0600047B RID: 1147 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public JToken DeepClone()
		{
			return this.CloneToken();
		}

		// Token: 0x04000185 RID: 389
		private JContainer _parent;

		// Token: 0x04000186 RID: 390
		private JToken _previous;

		// Token: 0x04000187 RID: 391
		private JToken _next;

		// Token: 0x04000188 RID: 392
		private static JTokenEqualityComparer _equalityComparer;

		// Token: 0x04000189 RID: 393
		private int? _lineNumber;

		// Token: 0x0400018A RID: 394
		private int? _linePosition;

		// Token: 0x0400018B RID: 395
		private static readonly JTokenType[] BooleanTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x0400018C RID: 396
		private static readonly JTokenType[] NumberTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x0400018D RID: 397
		private static readonly JTokenType[] BigIntegerTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean,
			JTokenType.Bytes
		};

		// Token: 0x0400018E RID: 398
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

		// Token: 0x0400018F RID: 399
		private static readonly JTokenType[] GuidTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Guid,
			JTokenType.Bytes
		};

		// Token: 0x04000190 RID: 400
		private static readonly JTokenType[] TimeSpanTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.TimeSpan
		};

		// Token: 0x04000191 RID: 401
		private static readonly JTokenType[] UriTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Uri
		};

		// Token: 0x04000192 RID: 402
		private static readonly JTokenType[] CharTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x04000193 RID: 403
		private static readonly JTokenType[] DateTimeTypes = new JTokenType[]
		{
			JTokenType.Date,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x04000194 RID: 404
		private static readonly JTokenType[] BytesTypes = new JTokenType[]
		{
			JTokenType.Bytes,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Integer
		};
	}
}

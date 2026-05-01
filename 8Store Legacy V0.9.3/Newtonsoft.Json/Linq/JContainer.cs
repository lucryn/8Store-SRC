using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a token that can contain other tokens.
	/// </summary>
	// Token: 0x02000056 RID: 86
	public abstract class JContainer : JToken, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IList, ICollection, IEnumerable
	{
		/// <summary>
		/// Occurs when the items list of the collection has changed, or the collection is reset.
		/// </summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600047D RID: 1149 RVA: 0x00011CCB File Offset: 0x0000FECB
		// (remove) Token: 0x0600047E RID: 1150 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(this._collectionChanged, value);
			}
			remove
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(this._collectionChanged, value);
			}
		}

		/// <summary>
		/// Gets the container's children tokens.
		/// </summary>
		/// <value>The container's children tokens.</value>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600047F RID: 1151
		protected abstract IList<JToken> ChildrenTokens { get; }

		// Token: 0x06000480 RID: 1152 RVA: 0x00011CFD File Offset: 0x0000FEFD
		internal JContainer()
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011D08 File Offset: 0x0000FF08
		internal JContainer(JContainer other) : this()
		{
			ValidationUtils.ArgumentNotNull(other, "c");
			foreach (JToken content in other)
			{
				this.Add(content);
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00011D64 File Offset: 0x0000FF64
		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00011D89 File Offset: 0x0000FF89
		internal virtual IList<JToken> CreateChildrenCollection()
		{
			return new List<JToken>();
		}

		/// <summary>
		/// Raises the <see cref="E:Newtonsoft.Json.Linq.JContainer.CollectionChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
		// Token: 0x06000484 RID: 1156 RVA: 0x00011D90 File Offset: 0x0000FF90
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this._collectionChanged;
			if (collectionChanged != null)
			{
				this._busy = true;
				try
				{
					collectionChanged.Invoke(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this token has childen tokens.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this token has child values; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		internal bool ContentsEqual(JContainer container)
		{
			if (container == this)
			{
				return true;
			}
			IList<JToken> childrenTokens = this.ChildrenTokens;
			IList<JToken> childrenTokens2 = container.ChildrenTokens;
			if (childrenTokens.Count != childrenTokens2.Count)
			{
				return false;
			}
			for (int i = 0; i < childrenTokens.Count; i++)
			{
				if (!childrenTokens[i].DeepEquals(childrenTokens2[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Get the first child token of this token.
		/// </summary>
		/// <value>
		/// A <see cref="T:Newtonsoft.Json.Linq.JToken" /> containing the first child token of the <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </value>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00011E3A File Offset: 0x0001003A
		public override JToken First
		{
			get
			{
				return Enumerable.FirstOrDefault<JToken>(this.ChildrenTokens);
			}
		}

		/// <summary>
		/// Get the last child token of this token.
		/// </summary>
		/// <value>
		/// A <see cref="T:Newtonsoft.Json.Linq.JToken" /> containing the last child token of the <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </value>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00011E47 File Offset: 0x00010047
		public override JToken Last
		{
			get
			{
				return Enumerable.LastOrDefault<JToken>(this.ChildrenTokens);
			}
		}

		/// <summary>
		/// Returns a collection of the child tokens of this token, in document order.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:Newtonsoft.Json.Linq.JToken" /> containing the child tokens of this <see cref="T:Newtonsoft.Json.Linq.JToken" />, in document order.
		/// </returns>
		// Token: 0x06000489 RID: 1161 RVA: 0x00011E54 File Offset: 0x00010054
		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		/// <summary>
		/// Returns a collection of the child values of this token, in document order.
		/// </summary>
		/// <typeparam name="T">The type to convert the values to.</typeparam>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerable`1" /> containing the child values of this <see cref="T:Newtonsoft.Json.Linq.JToken" />, in document order.
		/// </returns>
		// Token: 0x0600048A RID: 1162 RVA: 0x00011E61 File Offset: 0x00010061
		public override IEnumerable<T> Values<T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		/// <summary>
		/// Returns a collection of the descendant tokens for this token in document order.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> containing the descendant tokens of the <see cref="T:Newtonsoft.Json.Linq.JToken" />.</returns>
		// Token: 0x0600048B RID: 1163 RVA: 0x000120E4 File Offset: 0x000102E4
		public IEnumerable<JToken> Descendants()
		{
			foreach (JToken o in this.ChildrenTokens)
			{
				yield return o;
				JContainer c = o as JContainer;
				if (c != null)
				{
					foreach (JToken d in c.Descendants())
					{
						yield return d;
					}
				}
			}
			yield break;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00012101 File Offset: 0x00010301
		internal bool IsMultiContent(object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00012129 File Offset: 0x00010329
		internal JToken EnsureParentToken(JToken item, bool skipParentCheck)
		{
			if (item == null)
			{
				return new JValue(null);
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				item = item.CloneToken();
			}
			return item;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00012160 File Offset: 0x00010360
		internal int IndexOfItem(JToken item)
		{
			return this.ChildrenTokens.IndexOf(item, JContainer.JTokenReferenceEqualityComparer.Instance);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00012174 File Offset: 0x00010374
		internal virtual void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (index > this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, skipParentCheck);
			JToken jtoken = (index == 0) ? null : this.ChildrenTokens[index - 1];
			JToken jtoken2 = (index == this.ChildrenTokens.Count) ? null : this.ChildrenTokens[index];
			this.ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jtoken;
			if (jtoken != null)
			{
				jtoken.Next = item;
			}
			item.Next = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Previous = item;
			}
			this.ChildrenTokens.Insert(index, item);
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(0, item, index));
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00012238 File Offset: 0x00010438
		internal virtual void RemoveItemAt(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			this.CheckReentrancy();
			JToken jtoken = this.ChildrenTokens[index];
			JToken jtoken2 = (index == 0) ? null : this.ChildrenTokens[index - 1];
			JToken jtoken3 = (index == this.ChildrenTokens.Count - 1) ? null : this.ChildrenTokens[index + 1];
			if (jtoken2 != null)
			{
				jtoken2.Next = jtoken3;
			}
			if (jtoken3 != null)
			{
				jtoken3.Previous = jtoken2;
			}
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			this.ChildrenTokens.RemoveAt(index);
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(1, jtoken, index));
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001230C File Offset: 0x0001050C
		internal virtual bool RemoveItem(JToken item)
		{
			int num = this.IndexOfItem(item);
			if (num >= 0)
			{
				this.RemoveItemAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001232F File Offset: 0x0001052F
		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00012340 File Offset: 0x00010540
		internal virtual void SetItem(int index, JToken item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jtoken = this.ChildrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, false);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = (index == 0) ? null : this.ChildrenTokens[index - 1];
			JToken jtoken3 = (index == this.ChildrenTokens.Count - 1) ? null : this.ChildrenTokens[index + 1];
			item.Parent = this;
			item.Previous = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Next = item;
			}
			item.Next = jtoken3;
			if (jtoken3 != null)
			{
				jtoken3.Previous = item;
			}
			this.ChildrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(2, item, jtoken, index));
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00012448 File Offset: 0x00010648
		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			this.ChildrenTokens.Clear();
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(4));
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000124C8 File Offset: 0x000106C8
		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing == null || existing.Parent != this)
			{
				return;
			}
			int index = this.IndexOfItem(existing);
			this.SetItem(index, replacement);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000124F2 File Offset: 0x000106F2
		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00012504 File Offset: 0x00010704
		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				array.SetValue(jtoken, new int[]
				{
					arrayIndex + num
				});
				num++;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000125BC File Offset: 0x000107BC
		internal static bool IsTokenUnchanged(JToken currentValue, JToken newValue)
		{
			JValue jvalue = currentValue as JValue;
			return jvalue != null && ((jvalue.Type == JTokenType.Null && newValue == null) || jvalue.Equals(newValue));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000125EB File Offset: 0x000107EB
		internal virtual void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
		}

		/// <summary>
		/// Adds the specified content as children of this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="content">The content to be added.</param>
		// Token: 0x0600049A RID: 1178 RVA: 0x00012622 File Offset: 0x00010822
		public virtual void Add(object content)
		{
			this.AddInternal(this.ChildrenTokens.Count, content, false);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00012637 File Offset: 0x00010837
		internal void AddAndSkipParentCheck(JToken token)
		{
			this.AddInternal(this.ChildrenTokens.Count, token, true);
		}

		/// <summary>
		/// Adds the specified content as the first children of this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="content">The content to be added.</param>
		// Token: 0x0600049C RID: 1180 RVA: 0x0001264C File Offset: 0x0001084C
		public void AddFirst(object content)
		{
			this.AddInternal(0, content, false);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00012658 File Offset: 0x00010858
		internal void AddInternal(int index, object content, bool skipParentCheck)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object content2 = enumerator.Current;
						this.AddInternal(num, content2, skipParentCheck);
						num++;
					}
					return;
				}
			}
			JToken item = this.CreateFromContent(content);
			this.InsertItem(index, item, skipParentCheck);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000126D8 File Offset: 0x000108D8
		internal JToken CreateFromContent(object content)
		{
			if (content is JToken)
			{
				return (JToken)content;
			}
			return new JValue(content);
		}

		/// <summary>
		/// Creates an <see cref="T:Newtonsoft.Json.JsonWriter" /> that can be used to add tokens to the <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <returns>An <see cref="T:Newtonsoft.Json.JsonWriter" /> that is ready to have content written to it.</returns>
		// Token: 0x0600049F RID: 1183 RVA: 0x000126EF File Offset: 0x000108EF
		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		/// <summary>
		/// Replaces the children nodes of this token with the specified content.
		/// </summary>
		/// <param name="content">The content.</param>
		// Token: 0x060004A0 RID: 1184 RVA: 0x000126F7 File Offset: 0x000108F7
		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		/// <summary>
		/// Removes the child nodes from this token.
		/// </summary>
		// Token: 0x060004A1 RID: 1185 RVA: 0x00012706 File Offset: 0x00010906
		public void RemoveAll()
		{
			this.ClearItems();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012710 File Offset: 0x00010910
		internal void ReadTokenFrom(JsonReader reader)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			this.ReadContentFrom(reader);
			int depth2 = reader.Depth;
			if (depth2 > depth)
			{
				throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00012780 File Offset: 0x00010980
		internal void ReadContentFrom(JsonReader r)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo lineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				if (jcontainer is JProperty && ((JProperty)jcontainer).Value != null)
				{
					if (jcontainer == this)
					{
						break;
					}
					jcontainer = jcontainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.None:
					goto IL_216;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_216;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_216;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(jconstructor);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_216;
				}
				case JsonToken.PropertyName:
				{
					string name = r.Value.ToString();
					JProperty jproperty = new JProperty(name);
					jproperty.SetLineInfo(lineInfo);
					JObject jobject2 = (JObject)jcontainer;
					JProperty jproperty2 = jobject2.Property(name);
					if (jproperty2 == null)
					{
						jcontainer.Add(jproperty);
					}
					else
					{
						jproperty2.Replace(jproperty);
					}
					jcontainer = jproperty;
					goto IL_216;
				}
				case JsonToken.Comment:
				{
					JValue jvalue = JValue.CreateComment(r.Value.ToString());
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_216;
				}
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_216;
				}
				case JsonToken.Null:
				{
					JValue jvalue = new JValue(null, JTokenType.Null);
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_216;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = new JValue(null, JTokenType.Undefined);
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_216;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_216;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_216;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_216;
				}
				goto Block_4;
				IL_216:
				if (!r.Read())
				{
					return;
				}
			}
			return;
			Block_4:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, r.TokenType));
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000129B0 File Offset: 0x00010BB0
		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012A04 File Offset: 0x00010C04
		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00012A0D File Offset: 0x00010C0D
		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00012A18 File Offset: 0x00010C18
		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00012A21 File Offset: 0x00010C21
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00012A2A File Offset: 0x00010C2A
		JToken IList<JToken>.Item
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00012A34 File Offset: 0x00010C34
		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00012A3D File Offset: 0x00010C3D
		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00012A45 File Offset: 0x00010C45
		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00012A4E File Offset: 0x00010C4E
		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00012A58 File Offset: 0x00010C58
		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00012A5B File Offset: 0x00010C5B
		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00012A64 File Offset: 0x00010C64
		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is JToken)
			{
				return (JToken)value;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00012A84 File Offset: 0x00010C84
		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012A9B File Offset: 0x00010C9B
		void IList.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00012AA3 File Offset: 0x00010CA3
		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00012AB2 File Offset: 0x00010CB2
		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00012AC1 File Offset: 0x00010CC1
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value), false);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00012AD2 File Offset: 0x00010CD2
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00012AD5 File Offset: 0x00010CD5
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012AD8 File Offset: 0x00010CD8
		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00012AE8 File Offset: 0x00010CE8
		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00012AF1 File Offset: 0x00010CF1
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00012AFA File Offset: 0x00010CFA
		object IList.Item
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00012B0A File Offset: 0x00010D0A
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		/// <summary>
		/// Gets the count of child JSON tokens.
		/// </summary>
		/// <value>The count of child JSON tokens</value>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00012B14 File Offset: 0x00010D14
		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00012B21 File Offset: 0x00010D21
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00012B24 File Offset: 0x00010D24
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x04000195 RID: 405
		internal NotifyCollectionChangedEventHandler _collectionChanged;

		// Token: 0x04000196 RID: 406
		private object _syncRoot;

		// Token: 0x04000197 RID: 407
		private bool _busy;

		// Token: 0x02000057 RID: 87
		private class JTokenReferenceEqualityComparer : IEqualityComparer<JToken>
		{
			// Token: 0x060004C0 RID: 1216 RVA: 0x00012B46 File Offset: 0x00010D46
			public bool Equals(JToken x, JToken y)
			{
				return object.ReferenceEquals(x, y);
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x00012B4F File Offset: 0x00010D4F
			public int GetHashCode(JToken obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			// Token: 0x04000198 RID: 408
			public static readonly JContainer.JTokenReferenceEqualityComparer Instance = new JContainer.JTokenReferenceEqualityComparer();
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C3 RID: 195
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JContainer : JToken, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable, IList, ICollection, INotifyCollectionChanged
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x00027728 File Offset: 0x00025928
		internal Task ReadTokenFromAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings options, CancellationToken cancellationToken = default(CancellationToken))
		{
			JContainer.<ReadTokenFromAsync>d__0 <ReadTokenFromAsync>d__;
			<ReadTokenFromAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReadTokenFromAsync>d__.<>4__this = this;
			<ReadTokenFromAsync>d__.reader = reader;
			<ReadTokenFromAsync>d__.options = options;
			<ReadTokenFromAsync>d__.cancellationToken = cancellationToken;
			<ReadTokenFromAsync>d__.<>1__state = -1;
			<ReadTokenFromAsync>d__.<>t__builder.Start<JContainer.<ReadTokenFromAsync>d__0>(ref <ReadTokenFromAsync>d__);
			return <ReadTokenFromAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00027784 File Offset: 0x00025984
		private Task ReadContentFromAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			JContainer.<ReadContentFromAsync>d__1 <ReadContentFromAsync>d__;
			<ReadContentFromAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReadContentFromAsync>d__.<>4__this = this;
			<ReadContentFromAsync>d__.reader = reader;
			<ReadContentFromAsync>d__.settings = settings;
			<ReadContentFromAsync>d__.cancellationToken = cancellationToken;
			<ReadContentFromAsync>d__.<>1__state = -1;
			<ReadContentFromAsync>d__.<>t__builder.Start<JContainer.<ReadContentFromAsync>d__1>(ref <ReadContentFromAsync>d__);
			return <ReadContentFromAsync>d__.<>t__builder.Task;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060009BD RID: 2493 RVA: 0x000277DF File Offset: 0x000259DF
		// (remove) Token: 0x060009BE RID: 2494 RVA: 0x000277F8 File Offset: 0x000259F8
		[Nullable(2)]
		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			[NullableContext(2)]
			add
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(this._collectionChanged, value);
			}
			[NullableContext(2)]
			remove
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(this._collectionChanged, value);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009BF RID: 2495
		protected abstract IList<JToken> ChildrenTokens { get; }

		// Token: 0x060009C0 RID: 2496 RVA: 0x00027811 File Offset: 0x00025A11
		internal JContainer()
		{
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002781C File Offset: 0x00025A1C
		internal JContainer(JContainer other, [Nullable(2)] JsonCloneSettings settings) : this()
		{
			ValidationUtils.ArgumentNotNull(other, "other");
			bool flag = settings == null || settings.CopyAnnotations;
			if (flag)
			{
				base.CopyAnnotations(this, other);
			}
			int num = 0;
			foreach (JToken content in other)
			{
				this.TryAddInternal(num, content, false, flag);
				num++;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00027898 File Offset: 0x00025A98
		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000278BD File Offset: 0x00025ABD
		internal virtual IList<JToken> CreateChildrenCollection()
		{
			return new List<JToken>();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000278C4 File Offset: 0x00025AC4
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00027904 File Offset: 0x00025B04
		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00027914 File Offset: 0x00025B14
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

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00027970 File Offset: 0x00025B70
		[Nullable(2)]
		public override JToken First
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				if (childrenTokens.Count <= 0)
				{
					return null;
				}
				return childrenTokens[0];
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00027998 File Offset: 0x00025B98
		[Nullable(2)]
		public override JToken Last
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				int count = childrenTokens.Count;
				if (count <= 0)
				{
					return null;
				}
				return childrenTokens[count - 1];
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000279C2 File Offset: 0x00025BC2
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000279CF File Offset: 0x00025BCF
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public override IEnumerable<T> Values<T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000279DC File Offset: 0x00025BDC
		public IEnumerable<JToken> Descendants()
		{
			return this.GetDescendants(false);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000279E5 File Offset: 0x00025BE5
		public IEnumerable<JToken> DescendantsAndSelf()
		{
			return this.GetDescendants(true);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000279EE File Offset: 0x00025BEE
		internal IEnumerable<JToken> GetDescendants(bool self)
		{
			JContainer.<GetDescendants>d__26 <GetDescendants>d__ = new JContainer.<GetDescendants>d__26(-2);
			<GetDescendants>d__.<>4__this = this;
			<GetDescendants>d__.<>3__self = self;
			return <GetDescendants>d__;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00027A05 File Offset: 0x00025C05
		[NullableContext(2)]
		internal bool IsMultiContent([NotNullWhen(true)] object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00027A30 File Offset: 0x00025C30
		internal JToken EnsureParentToken([Nullable(2)] JToken item, bool skipParentCheck, bool copyAnnotations)
		{
			if (item == null)
			{
				return JValue.CreateNull();
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				JsonCloneSettings settings = copyAnnotations ? null : JsonCloneSettings.SkipCopyAnnotations;
				item = item.CloneToken(settings);
			}
			return item;
		}

		// Token: 0x060009D0 RID: 2512
		[NullableContext(2)]
		internal abstract int IndexOfItem(JToken item);

		// Token: 0x060009D1 RID: 2513 RVA: 0x00027A80 File Offset: 0x00025C80
		[NullableContext(2)]
		internal virtual bool InsertItem(int index, JToken item, bool skipParentCheck, bool copyAnnotations)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index > childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, skipParentCheck, copyAnnotations);
			JToken jtoken = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken2 = (index == childrenTokens.Count) ? null : childrenTokens[index];
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
			childrenTokens.Insert(index, item);
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(0, item, index));
			}
			return true;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00027B34 File Offset: 0x00025D34
		internal virtual void RemoveItemAt(int index)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			this.CheckReentrancy();
			JToken jtoken = childrenTokens[index];
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
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
			childrenTokens.RemoveAt(index);
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(1, jtoken, index));
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00027BF4 File Offset: 0x00025DF4
		[NullableContext(2)]
		internal virtual bool RemoveItem(JToken item)
		{
			if (item != null)
			{
				int num = this.IndexOfItem(item);
				if (num >= 0)
				{
					this.RemoveItemAt(num);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00027C1A File Offset: 0x00025E1A
		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00027C28 File Offset: 0x00025E28
		[NullableContext(2)]
		internal virtual void SetItem(int index, JToken item)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jtoken = childrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, false, true);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
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
			childrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(2, item, jtoken, index));
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00027D1C File Offset: 0x00025F1C
		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			IList<JToken> childrenTokens = this.ChildrenTokens;
			foreach (JToken jtoken in childrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			childrenTokens.Clear();
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(4));
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00027D98 File Offset: 0x00025F98
		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing == null || existing.Parent != this)
			{
				return;
			}
			int index = this.IndexOfItem(existing);
			this.SetItem(index, replacement);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00027DC2 File Offset: 0x00025FC2
		[NullableContext(2)]
		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00027DD4 File Offset: 0x00025FD4
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

		// Token: 0x060009DA RID: 2522 RVA: 0x00027E88 File Offset: 0x00026088
		internal static bool IsTokenUnchanged(JToken currentValue, [Nullable(2)] JToken newValue)
		{
			JValue jvalue = currentValue as JValue;
			if (jvalue == null)
			{
				return false;
			}
			if (newValue == null)
			{
				return jvalue.Type == JTokenType.Null;
			}
			return jvalue.Equals(newValue);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00027EB6 File Offset: 0x000260B6
		internal virtual void ValidateToken(JToken o, [Nullable(2)] JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00027EED File Offset: 0x000260ED
		[NullableContext(2)]
		public virtual void Add(object content)
		{
			this.TryAddInternal(this.ChildrenTokens.Count, content, false, true);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00027F04 File Offset: 0x00026104
		[NullableContext(2)]
		internal bool TryAdd(object content)
		{
			return this.TryAddInternal(this.ChildrenTokens.Count, content, false, true);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00027F1A File Offset: 0x0002611A
		internal void AddAndSkipParentCheck(JToken token)
		{
			this.TryAddInternal(this.ChildrenTokens.Count, token, true, true);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00027F31 File Offset: 0x00026131
		[NullableContext(2)]
		public void AddFirst(object content)
		{
			this.TryAddInternal(0, content, false, true);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00027F40 File Offset: 0x00026140
		[NullableContext(2)]
		internal bool TryAddInternal(int index, object content, bool skipParentCheck, bool copyAnnotations)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				foreach (object content2 in enumerable)
				{
					this.TryAddInternal(num, content2, skipParentCheck, copyAnnotations);
					num++;
				}
				return true;
			}
			JToken item = JContainer.CreateFromContent(content);
			return this.InsertItem(index, item, skipParentCheck, copyAnnotations);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00027FC0 File Offset: 0x000261C0
		internal static JToken CreateFromContent([Nullable(2)] object content)
		{
			JToken jtoken = content as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			return new JValue(content);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00027FDF File Offset: 0x000261DF
		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00027FE7 File Offset: 0x000261E7
		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00027FF6 File Offset: 0x000261F6
		public void RemoveAll()
		{
			this.ClearItems();
		}

		// Token: 0x060009E5 RID: 2533
		internal abstract void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings);

		// Token: 0x060009E6 RID: 2534 RVA: 0x00027FFE File Offset: 0x000261FE
		[NullableContext(2)]
		public void Merge(object content)
		{
			if (content == null)
			{
				return;
			}
			this.ValidateContent(content);
			this.MergeItem(content, null);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00028013 File Offset: 0x00026213
		[NullableContext(2)]
		public void Merge(object content, JsonMergeSettings settings)
		{
			if (content == null)
			{
				return;
			}
			this.ValidateContent(content);
			this.MergeItem(content, settings);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00028028 File Offset: 0x00026228
		private void ValidateContent(object content)
		{
			if (content.GetType().IsSubclassOf(typeof(JToken)))
			{
				return;
			}
			if (this.IsMultiContent(content))
			{
				return;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, content.GetType()), "content");
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00028078 File Offset: 0x00026278
		internal void ReadTokenFrom(JsonReader reader, [Nullable(2)] JsonLoadSettings options)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			this.ReadContentFrom(reader, options);
			if (reader.Depth > depth)
			{
				throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000280E8 File Offset: 0x000262E8
		internal void ReadContentFrom(JsonReader r, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo lineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				JProperty jproperty = jcontainer as JProperty;
				if (jproperty != null && jproperty.Value != null)
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
					goto IL_1F2;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_1F2;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_1F2;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_1F2;
				}
				case JsonToken.PropertyName:
				{
					JProperty jproperty2 = JContainer.ReadProperty(r, settings, lineInfo, jcontainer);
					if (jproperty2 != null)
					{
						jcontainer = jproperty2;
						goto IL_1F2;
					}
					r.Skip();
					goto IL_1F2;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jvalue = JValue.CreateComment(r.Value.ToString());
						jvalue.SetLineInfo(lineInfo, settings);
						jcontainer.Add(jvalue);
						goto IL_1F2;
					}
					goto IL_1F2;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_1F2;
				}
				case JsonToken.Null:
				{
					JValue jvalue = JValue.CreateNull();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_1F2;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = JValue.CreateUndefined();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_1F2;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_1F2;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_1F2;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_1F2;
				}
				goto Block_4;
				IL_1F2:
				if (!r.Read())
				{
					return;
				}
			}
			return;
			Block_4:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, r.TokenType));
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000282F4 File Offset: 0x000264F4
		[NullableContext(2)]
		private static JProperty ReadProperty([Nullable(1)] JsonReader r, JsonLoadSettings settings, IJsonLineInfo lineInfo, [Nullable(1)] JContainer parent)
		{
			DuplicatePropertyNameHandling duplicatePropertyNameHandling = (settings != null) ? settings.DuplicatePropertyNameHandling : DuplicatePropertyNameHandling.Replace;
			JObject jobject = (JObject)parent;
			string text = r.Value.ToString();
			JProperty jproperty = jobject.Property(text, 4);
			if (jproperty != null)
			{
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Ignore)
				{
					return null;
				}
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Error)
				{
					throw JsonReaderException.Create(r, "Property with the name '{0}' already exists in the current JSON object.".FormatWith(CultureInfo.InvariantCulture, text));
				}
			}
			JProperty jproperty2 = new JProperty(text);
			jproperty2.SetLineInfo(lineInfo, settings);
			if (jproperty == null)
			{
				parent.Add(jproperty2);
			}
			else
			{
				jproperty.Replace(jproperty2);
			}
			return jproperty2;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00028370 File Offset: 0x00026570
		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000283C4 File Offset: 0x000265C4
		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000283CD File Offset: 0x000265CD
		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false, true);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000283DA File Offset: 0x000265DA
		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000283E3 File Offset: 0x000265E3
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000283EC File Offset: 0x000265EC
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

		// Token: 0x060009F2 RID: 2546 RVA: 0x000283F6 File Offset: 0x000265F6
		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000283FF File Offset: 0x000265FF
		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00028407 File Offset: 0x00026607
		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00028410 File Offset: 0x00026610
		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002841A File Offset: 0x0002661A
		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002841D File Offset: 0x0002661D
		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00028428 File Offset: 0x00026628
		[NullableContext(2)]
		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			JToken jtoken = value as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00028450 File Offset: 0x00026650
		[NullableContext(2)]
		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00028467 File Offset: 0x00026667
		void IList.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002846F File Offset: 0x0002666F
		[NullableContext(2)]
		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002847E File Offset: 0x0002667E
		[NullableContext(2)]
		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002848D File Offset: 0x0002668D
		[NullableContext(2)]
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value), false, false);
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x000284A0 File Offset: 0x000266A0
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000284A3 File Offset: 0x000266A3
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000284A6 File Offset: 0x000266A6
		[NullableContext(2)]
		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000284B6 File Offset: 0x000266B6
		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000284BF File Offset: 0x000266BF
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x000284C8 File Offset: 0x000266C8
		[Nullable(2)]
		object IList.Item
		{
			[NullableContext(2)]
			get
			{
				return this.GetItem(index);
			}
			[NullableContext(2)]
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000284D8 File Offset: 0x000266D8
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000284E2 File Offset: 0x000266E2
		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x000284EF File Offset: 0x000266EF
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x000284F2 File Offset: 0x000266F2
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

		// Token: 0x06000A08 RID: 2568 RVA: 0x00028514 File Offset: 0x00026714
		internal static void MergeEnumerableContent(JContainer target, IEnumerable content, [Nullable(2)] JsonMergeSettings settings)
		{
			switch ((settings != null) ? settings.MergeArrayHandling : MergeArrayHandling.Concat)
			{
			case MergeArrayHandling.Concat:
				using (IEnumerator enumerator = content.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object content2 = enumerator.Current;
						target.Add(JContainer.CreateFromContent(content2));
					}
					return;
				}
				break;
			case MergeArrayHandling.Union:
				break;
			case MergeArrayHandling.Replace:
				goto IL_BC;
			case MergeArrayHandling.Merge:
				goto IL_108;
			default:
				goto IL_19E;
			}
			HashSet<JToken> hashSet = new HashSet<JToken>(target, JToken.EqualityComparer);
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object content3 = enumerator.Current;
					JToken jtoken = JContainer.CreateFromContent(content3);
					if (hashSet.Add(jtoken))
					{
						target.Add(jtoken);
					}
				}
				return;
			}
			IL_BC:
			if (target == content)
			{
				return;
			}
			target.ClearItems();
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object content4 = enumerator.Current;
					target.Add(JContainer.CreateFromContent(content4));
				}
				return;
			}
			IL_108:
			int num = 0;
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (num < target.Count)
					{
						JContainer jcontainer = target[num] as JContainer;
						if (jcontainer != null)
						{
							jcontainer.Merge(obj, settings);
						}
						else if (obj != null)
						{
							JToken jtoken2 = JContainer.CreateFromContent(obj);
							if (jtoken2.Type != JTokenType.Null)
							{
								target[num] = jtoken2;
							}
						}
					}
					else
					{
						target.Add(JContainer.CreateFromContent(obj));
					}
					num++;
				}
				return;
			}
			IL_19E:
			throw new ArgumentOutOfRangeException("settings", "Unexpected merge array handling when merging JSON.");
		}

		// Token: 0x040003B3 RID: 947
		[Nullable(2)]
		internal NotifyCollectionChangedEventHandler _collectionChanged;

		// Token: 0x040003B4 RID: 948
		[Nullable(2)]
		private object _syncRoot;

		// Token: 0x040003B5 RID: 949
		private bool _busy;
	}
}

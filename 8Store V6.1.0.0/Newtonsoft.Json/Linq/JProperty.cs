using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C6 RID: 198
	[NullableContext(1)]
	[Nullable(0)]
	public class JProperty : JContainer
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x0002915C File Offset: 0x0002735C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00029194 File Offset: 0x00027394
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			JProperty.<WriteToAsync>d__1 <WriteToAsync>d__;
			<WriteToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToAsync>d__.<>4__this = this;
			<WriteToAsync>d__.task = task;
			<WriteToAsync>d__.writer = writer;
			<WriteToAsync>d__.cancellationToken = cancellationToken;
			<WriteToAsync>d__.converters = converters;
			<WriteToAsync>d__.<>1__state = -1;
			<WriteToAsync>d__.<>t__builder.Start<JProperty.<WriteToAsync>d__1>(ref <WriteToAsync>d__);
			return <WriteToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000291F8 File Offset: 0x000273F8
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00029220 File Offset: 0x00027420
		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002922C File Offset: 0x0002742C
		public new static Task<JProperty> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			JProperty.<LoadAsync>d__4 <LoadAsync>d__;
			<LoadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JProperty>.Create();
			<LoadAsync>d__.reader = reader;
			<LoadAsync>d__.settings = settings;
			<LoadAsync>d__.cancellationToken = cancellationToken;
			<LoadAsync>d__.<>1__state = -1;
			<LoadAsync>d__.<>t__builder.Start<JProperty.<LoadAsync>d__4>(ref <LoadAsync>d__);
			return <LoadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002927F File Offset: 0x0002747F
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00029287 File Offset: 0x00027487
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002928F File Offset: 0x0002748F
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x0002929C File Offset: 0x0002749C
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken item = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, item, false, true);
					return;
				}
				this.SetItem(0, item);
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000292DB File Offset: 0x000274DB
		public JProperty(JProperty other) : base(other, null)
		{
			this._name = other.Name;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000292FC File Offset: 0x000274FC
		internal JProperty(JProperty other, [Nullable(2)] JsonCloneSettings settings) : base(other, settings)
		{
			this._name = other.Name;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002931D File Offset: 0x0002751D
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00029330 File Offset: 0x00027530
		[NullableContext(2)]
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002938A File Offset: 0x0002758A
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000293AA File Offset: 0x000275AA
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000293CA File Offset: 0x000275CA
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000293E0 File Offset: 0x000275E0
		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck, bool copyAnnotations)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return false;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			return base.InsertItem(0, item, false, copyAnnotations);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002942D File Offset: 0x0002762D
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00029438 File Offset: 0x00027638
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = (jproperty != null) ? jproperty.Value : null;
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002946C File Offset: 0x0002766C
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002948C File Offset: 0x0002768C
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000294BF File Offset: 0x000276BF
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JProperty(this, settings);
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x000294C8 File Offset: 0x000276C8
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000294CB File Offset: 0x000276CB
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000294F0 File Offset: 0x000276F0
		public JProperty(string name, params object[] content) : this(name, content)
		{
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000294FC File Offset: 0x000276FC
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002954C File Offset: 0x0002774C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002957E File Offset: 0x0002777E
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002959E File Offset: 0x0002779E
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000295A8 File Offset: 0x000277A8
		public new static JProperty Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		// Token: 0x040003BA RID: 954
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x040003BB RID: 955
		private readonly string _name;

		// Token: 0x020001D6 RID: 470
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06000F42 RID: 3906 RVA: 0x00042A02 File Offset: 0x00040C02
			public IEnumerator<JToken> GetEnumerator()
			{
				JProperty.JPropertyList.<GetEnumerator>d__1 <GetEnumerator>d__ = new JProperty.JPropertyList.<GetEnumerator>d__1(0);
				<GetEnumerator>d__.<>4__this = this;
				return <GetEnumerator>d__;
			}

			// Token: 0x06000F43 RID: 3907 RVA: 0x00042A11 File Offset: 0x00040C11
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000F44 RID: 3908 RVA: 0x00042A19 File Offset: 0x00040C19
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x06000F45 RID: 3909 RVA: 0x00042A22 File Offset: 0x00040C22
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x00042A2B File Offset: 0x00040C2B
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x00042A36 File Offset: 0x00040C36
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x00042A49 File Offset: 0x00040C49
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00042A5E File Offset: 0x00040C5E
			public int Count
			{
				get
				{
					return (this._token != null) ? 1 : 0;
				}
			}

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00042A69 File Offset: 0x00040C69
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000F4B RID: 3915 RVA: 0x00042A6C File Offset: 0x00040C6C
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06000F4C RID: 3916 RVA: 0x00042A7A File Offset: 0x00040C7A
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06000F4D RID: 3917 RVA: 0x00042A86 File Offset: 0x00040C86
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x17000289 RID: 649
			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this._token;
				}
				set
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					this._token = value;
				}
			}

			// Token: 0x040007FA RID: 2042
			[Nullable(2)]
			internal JToken _token;
		}
	}
}

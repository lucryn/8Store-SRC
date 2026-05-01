using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C2 RID: 194
	[NullableContext(1)]
	[Nullable(0)]
	public class JConstructor : JContainer
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x00027394 File Offset: 0x00025594
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			JConstructor.<WriteToAsync>d__0 <WriteToAsync>d__;
			<WriteToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToAsync>d__.<>4__this = this;
			<WriteToAsync>d__.writer = writer;
			<WriteToAsync>d__.cancellationToken = cancellationToken;
			<WriteToAsync>d__.converters = converters;
			<WriteToAsync>d__.<>1__state = -1;
			<WriteToAsync>d__.<>t__builder.Start<JConstructor.<WriteToAsync>d__0>(ref <WriteToAsync>d__);
			return <WriteToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000273EF File Offset: 0x000255EF
		public new static Task<JConstructor> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JConstructor.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000273FC File Offset: 0x000255FC
		public new static Task<JConstructor> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			JConstructor.<LoadAsync>d__2 <LoadAsync>d__;
			<LoadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JConstructor>.Create();
			<LoadAsync>d__.reader = reader;
			<LoadAsync>d__.settings = settings;
			<LoadAsync>d__.cancellationToken = cancellationToken;
			<LoadAsync>d__.<>1__state = -1;
			<LoadAsync>d__.<>t__builder.Start<JConstructor.<LoadAsync>d__2>(ref <LoadAsync>d__);
			return <LoadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002744F File Offset: 0x0002564F
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00027457 File Offset: 0x00025657
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002746C File Offset: 0x0002566C
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JConstructor jconstructor = content as JConstructor;
			if (jconstructor == null)
			{
				return;
			}
			if (jconstructor.Name != null)
			{
				this.Name = jconstructor.Name;
			}
			JContainer.MergeEnumerableContent(this, jconstructor, settings);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000274A0 File Offset: 0x000256A0
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x000274A8 File Offset: 0x000256A8
		[Nullable(2)]
		public string Name
		{
			[NullableContext(2)]
			get
			{
				return this._name;
			}
			[NullableContext(2)]
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000274B1 File Offset: 0x000256B1
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Constructor;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000274B4 File Offset: 0x000256B4
		public JConstructor()
		{
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000274C7 File Offset: 0x000256C7
		public JConstructor(JConstructor other) : base(other, null)
		{
			this._name = other.Name;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000274E8 File Offset: 0x000256E8
		internal JConstructor(JConstructor other, [Nullable(2)] JsonCloneSettings settings) : base(other, settings)
		{
			this._name = other.Name;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00027509 File Offset: 0x00025709
		public JConstructor(string name, params object[] content) : this(name, content)
		{
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00027513 File Offset: 0x00025713
		public JConstructor(string name, object content) : this(name)
		{
			this.Add(content);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00027523 File Offset: 0x00025723
		public JConstructor(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Constructor name cannot be empty.", "name");
			}
			this._name = name;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00027564 File Offset: 0x00025764
		internal override bool DeepEquals(JToken node)
		{
			JConstructor jconstructor = node as JConstructor;
			return jconstructor != null && this._name == jconstructor.Name && base.ContentsEqual(jconstructor);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00027597 File Offset: 0x00025797
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings = null)
		{
			return new JConstructor(this, settings);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000275A0 File Offset: 0x000257A0
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartConstructor(this._name);
			int count = this._values.Count;
			for (int i = 0; i < count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		// Token: 0x170001CA RID: 458
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (key is int)
				{
					int index = (int)key;
					return this.GetItem(index);
				}
				throw new ArgumentException("Accessed JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (key is int)
				{
					int index = (int)key;
					this.SetItem(index, value);
					return;
				}
				throw new ArgumentException("Set JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00027684 File Offset: 0x00025884
		internal override int GetDeepHashCode()
		{
			string name = this._name;
			return ((name != null) ? name.GetHashCode() : 0) ^ base.ContentsHashCode();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002769F File Offset: 0x0002589F
		public new static JConstructor Load(JsonReader reader)
		{
			return JConstructor.Load(reader, null);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000276A8 File Offset: 0x000258A8
		public new static JConstructor Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor jconstructor = new JConstructor((string)reader.Value);
			jconstructor.SetLineInfo(reader as IJsonLineInfo, settings);
			jconstructor.ReadTokenFrom(reader, settings);
			return jconstructor;
		}

		// Token: 0x040003B1 RID: 945
		[Nullable(2)]
		private string _name;

		// Token: 0x040003B2 RID: 946
		private readonly List<JToken> _values = new List<JToken>();
	}
}

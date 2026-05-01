using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C1 RID: 193
	[NullableContext(1)]
	[Nullable(0)]
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x00026F74 File Offset: 0x00025174
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			JArray.<WriteToAsync>d__0 <WriteToAsync>d__;
			<WriteToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToAsync>d__.<>4__this = this;
			<WriteToAsync>d__.writer = writer;
			<WriteToAsync>d__.cancellationToken = cancellationToken;
			<WriteToAsync>d__.converters = converters;
			<WriteToAsync>d__.<>1__state = -1;
			<WriteToAsync>d__.<>t__builder.Start<JArray.<WriteToAsync>d__0>(ref <WriteToAsync>d__);
			return <WriteToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00026FCF File Offset: 0x000251CF
		public new static Task<JArray> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JArray.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00026FDC File Offset: 0x000251DC
		public new static Task<JArray> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			JArray.<LoadAsync>d__2 <LoadAsync>d__;
			<LoadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JArray>.Create();
			<LoadAsync>d__.reader = reader;
			<LoadAsync>d__.settings = settings;
			<LoadAsync>d__.cancellationToken = cancellationToken;
			<LoadAsync>d__.<>1__state = -1;
			<LoadAsync>d__.<>t__builder.Start<JArray.<LoadAsync>d__2>(ref <LoadAsync>d__);
			return <LoadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0002702F File Offset: 0x0002522F
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00027037 File Offset: 0x00025237
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002703A File Offset: 0x0002523A
		public JArray()
		{
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002704D File Offset: 0x0002524D
		public JArray(JArray other) : base(other, null)
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00027062 File Offset: 0x00025262
		internal JArray(JArray other, [Nullable(2)] JsonCloneSettings settings) : base(other, settings)
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00027077 File Offset: 0x00025277
		public JArray(params object[] content) : this(content)
		{
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00027080 File Offset: 0x00025280
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002709C File Offset: 0x0002529C
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000270BC File Offset: 0x000252BC
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings = null)
		{
			return new JArray(this, settings);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000270C5 File Offset: 0x000252C5
		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000270D0 File Offset: 0x000252D0
		public new static JArray Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00027144 File Offset: 0x00025344
		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00027150 File Offset: 0x00025350
		public new static JArray Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JArray result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jarray;
			}
			return result;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00027198 File Offset: 0x00025398
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000271A8 File Offset: 0x000253A8
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000271EC File Offset: 0x000253EC
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170001C4 RID: 452
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
				throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
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
				throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
			}
		}

		// Token: 0x170001C5 RID: 453
		public JToken this[int index]
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

		// Token: 0x06000997 RID: 2455 RVA: 0x000272D9 File Offset: 0x000254D9
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000272EC File Offset: 0x000254EC
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			IEnumerable enumerable = (base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null;
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00027320 File Offset: 0x00025520
		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00027329 File Offset: 0x00025529
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false, true);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00027336 File Offset: 0x00025536
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00027340 File Offset: 0x00025540
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002735B File Offset: 0x0002555B
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00027364 File Offset: 0x00025564
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002736C File Offset: 0x0002556C
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00027375 File Offset: 0x00025575
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002737F File Offset: 0x0002557F
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00027382 File Offset: 0x00025582
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002738B File Offset: 0x0002558B
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x040003B0 RID: 944
		private readonly List<JToken> _values = new List<JToken>();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C5 RID: 197
	[NullableContext(1)]
	[Nullable(0)]
	public class JObject : JContainer, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable, INotifyPropertyChanged
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x000287CC File Offset: 0x000269CC
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WriteStartObjectAsync(cancellationToken);
			if (!task.IsCompletedSuccessfully())
			{
				return this.<WriteToAsync>g__AwaitProperties|0_0(task, 0, writer, cancellationToken, converters);
			}
			for (int i = 0; i < this._properties.Count; i++)
			{
				task = this._properties[i].WriteToAsync(writer, cancellationToken, converters);
				if (!task.IsCompletedSuccessfully())
				{
					return this.<WriteToAsync>g__AwaitProperties|0_0(task, i + 1, writer, cancellationToken, converters);
				}
			}
			return writer.WriteEndObjectAsync(cancellationToken);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002883D File Offset: 0x00026A3D
		public new static Task<JObject> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JObject.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00028848 File Offset: 0x00026A48
		public new static Task<JObject> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			JObject.<LoadAsync>d__2 <LoadAsync>d__;
			<LoadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JObject>.Create();
			<LoadAsync>d__.reader = reader;
			<LoadAsync>d__.settings = settings;
			<LoadAsync>d__.cancellationToken = cancellationToken;
			<LoadAsync>d__.<>1__state = -1;
			<LoadAsync>d__.<>t__builder.Start<JObject.<LoadAsync>d__2>(ref <LoadAsync>d__);
			return <LoadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002889B File Offset: 0x00026A9B
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000A15 RID: 2581 RVA: 0x000288A4 File Offset: 0x00026AA4
		// (remove) Token: 0x06000A16 RID: 2582 RVA: 0x000288DC File Offset: 0x00026ADC
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A17 RID: 2583 RVA: 0x00028911 File Offset: 0x00026B11
		public JObject()
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00028924 File Offset: 0x00026B24
		public JObject(JObject other) : base(other, null)
		{
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00028939 File Offset: 0x00026B39
		internal JObject(JObject other, [Nullable(2)] JsonCloneSettings settings) : base(other, settings)
		{
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002894E File Offset: 0x00026B4E
		public JObject(params object[] content) : this(content)
		{
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00028957 File Offset: 0x00026B57
		public JObject(object content)
		{
			this.Add(content);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00028974 File Offset: 0x00026B74
		internal override bool DeepEquals(JToken node)
		{
			JObject jobject = node as JObject;
			return jobject != null && this._properties.Compare(jobject._properties);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002899E File Offset: 0x00026B9E
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._properties.IndexOfReference(item);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000289B1 File Offset: 0x00026BB1
		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck, bool copyAnnotations)
		{
			return (item == null || item.Type != JTokenType.Comment) && base.InsertItem(index, item, skipParentCheck, copyAnnotations);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000289CC File Offset: 0x00026BCC
		internal override void ValidateToken(JToken o, [Nullable(2)] JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
			JProperty jproperty = (JProperty)o;
			if (existing != null)
			{
				JProperty jproperty2 = (JProperty)existing;
				if (jproperty.Name == jproperty2.Name)
				{
					return;
				}
			}
			if (this._properties.TryGetValue(jproperty.Name, out existing))
			{
				throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name, base.GetType()));
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00028A6C File Offset: 0x00026C6C
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JObject jobject = content as JObject;
			if (jobject == null)
			{
				return;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
			{
				JProperty jproperty = this.Property(keyValuePair.Key, (settings != null) ? settings.PropertyNameComparison : 4);
				if (jproperty == null)
				{
					this.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else if (keyValuePair.Value != null)
				{
					JContainer jcontainer = jproperty.Value as JContainer;
					if (jcontainer == null || jcontainer.Type != keyValuePair.Value.Type)
					{
						if (!JObject.IsNull(keyValuePair.Value) || (settings != null && settings.MergeNullValueHandling == MergeNullValueHandling.Merge))
						{
							jproperty.Value = keyValuePair.Value;
						}
					}
					else
					{
						jcontainer.Merge(keyValuePair.Value, settings);
					}
				}
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00028B58 File Offset: 0x00026D58
		private static bool IsNull(JToken token)
		{
			if (token.Type == JTokenType.Null)
			{
				return true;
			}
			JValue jvalue = token as JValue;
			return jvalue != null && jvalue.Value == null;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00028B86 File Offset: 0x00026D86
		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(2, childProperty, childProperty, this.IndexOfItem(childProperty)));
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00028BB1 File Offset: 0x00026DB1
		internal void InternalPropertyChanging(JProperty childProperty)
		{
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00028BB3 File Offset: 0x00026DB3
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JObject(this, settings);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00028BBC File Offset: 0x00026DBC
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00028BBF File Offset: 0x00026DBF
		public IEnumerable<JProperty> Properties()
		{
			return Enumerable.Cast<JProperty>(this._properties);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00028BCC File Offset: 0x00026DCC
		[return: Nullable(2)]
		public JProperty Property(string name)
		{
			return this.Property(name, 4);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00028BD8 File Offset: 0x00026DD8
		[return: Nullable(2)]
		public JProperty Property(string name, StringComparison comparison)
		{
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			if (this._properties.TryGetValue(name, out jtoken))
			{
				return (JProperty)jtoken;
			}
			if (comparison != 4)
			{
				for (int i = 0; i < this._properties.Count; i++)
				{
					JProperty jproperty = (JProperty)this._properties[i];
					if (string.Equals(jproperty.Name, name, comparison))
					{
						return jproperty;
					}
				}
			}
			return null;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00028C3F File Offset: 0x00026E3F
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(Enumerable.Select<JProperty, JToken>(this.Properties(), (JProperty p) => p.Value));
		}

		// Token: 0x170001DA RID: 474
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this[text];
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this[text] = value;
			}
		}

		// Token: 0x170001DB RID: 475
		[Nullable(2)]
		public JToken this[string propertyName]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				JProperty jproperty = this.Property(propertyName, 4);
				if (jproperty == null)
				{
					return null;
				}
				return jproperty.Value;
			}
			[param: Nullable(2)]
			set
			{
				JProperty jproperty = this.Property(propertyName, 4);
				if (jproperty != null)
				{
					jproperty.Value = value;
					return;
				}
				this.Add(propertyName, value);
				this.OnPropertyChanged(propertyName);
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00028D4C File Offset: 0x00026F4C
		public new static JObject Load(JsonReader reader)
		{
			return JObject.Load(reader, null);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00028D58 File Offset: 0x00026F58
		public new static JObject Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo, settings);
			jobject.ReadTokenFrom(reader, settings);
			return jobject;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00028DD7 File Offset: 0x00026FD7
		public new static JObject Parse(string json)
		{
			return JObject.Parse(json, null);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00028DE0 File Offset: 0x00026FE0
		public new static JObject Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JObject result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JObject jobject = JObject.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jobject;
			}
			return result;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00028E28 File Offset: 0x00027028
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00028E38 File Offset: 0x00027038
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JObject)jtoken;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00028E7C File Offset: 0x0002707C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			for (int i = 0; i < this._properties.Count; i++)
			{
				this._properties[i].WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00028EBE File Offset: 0x000270BE
		[NullableContext(2)]
		public JToken GetValue(string propertyName)
		{
			return this.GetValue(propertyName, 4);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00028EC8 File Offset: 0x000270C8
		[NullableContext(2)]
		public JToken GetValue(string propertyName, StringComparison comparison)
		{
			if (propertyName == null)
			{
				return null;
			}
			JProperty jproperty = this.Property(propertyName, comparison);
			if (jproperty == null)
			{
				return null;
			}
			return jproperty.Value;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00028EE2 File Offset: 0x000270E2
		public bool TryGetValue(string propertyName, StringComparison comparison, [Nullable(2)] [NotNullWhen(true)] out JToken value)
		{
			value = this.GetValue(propertyName, comparison);
			return value != null;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00028EF3 File Offset: 0x000270F3
		public void Add(string propertyName, [Nullable(2)] JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00028F02 File Offset: 0x00027102
		public bool ContainsKey(string propertyName)
		{
			ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
			return this._properties.Contains(propertyName);
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00028F1B File Offset: 0x0002711B
		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Keys;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00028F28 File Offset: 0x00027128
		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName, 4);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00028F4C File Offset: 0x0002714C
		public bool TryGetValue(string propertyName, [Nullable(2)] [NotNullWhen(true)] out JToken value)
		{
			JProperty jproperty = this.Property(propertyName, 4);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00028F73 File Offset: 0x00027173
		[Nullable(new byte[]
		{
			1,
			2
		})]
		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			[return: Nullable(new byte[]
			{
				1,
				2
			})]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00028F7A File Offset: 0x0002717A
		void ICollection<KeyValuePair<string, JToken>>.Add([Nullable(new byte[]
		{
			0,
			1,
			2
		})] KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00028F95 File Offset: 0x00027195
		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00028FA0 File Offset: 0x000271A0
		bool ICollection<KeyValuePair<string, JToken>>.Contains([Nullable(new byte[]
		{
			0,
			1,
			2
		})] KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key, 4);
			return jproperty != null && jproperty.Value == item.Value;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00028FD0 File Offset: 0x000271D0
		void ICollection<KeyValuePair<string, JToken>>.CopyTo([Nullable(new byte[]
		{
			1,
			0,
			1,
			2
		})] KeyValuePair<string, JToken>[] array, int arrayIndex)
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
			if (base.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				array[arrayIndex + num] = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
				num++;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0002908C File Offset: 0x0002728C
		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002908F File Offset: 0x0002728F
		bool ICollection<KeyValuePair<string, JToken>>.Remove([Nullable(new byte[]
		{
			0,
			1,
			2
		})] KeyValuePair<string, JToken> item)
		{
			if (!this.Contains(item))
			{
				return false;
			}
			this.Remove(item.Key);
			return true;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000290AB File Offset: 0x000272AB
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000290B3 File Offset: 0x000272B3
		[return: Nullable(new byte[]
		{
			1,
			0,
			1,
			2
		})]
		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			JObject.<GetEnumerator>d__61 <GetEnumerator>d__ = new JObject.<GetEnumerator>d__61(0);
			<GetEnumerator>d__.<>4__this = this;
			return <GetEnumerator>d__;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000290C2 File Offset: 0x000272C2
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000290DB File Offset: 0x000272DB
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JObject>(parameter, this, new JObject.JObjectDynamicProxy());
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000290EC File Offset: 0x000272EC
		[CompilerGenerated]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private Task <WriteToAsync>g__AwaitProperties|0_0(Task task, int i, JsonWriter Writer, CancellationToken CancellationToken, JsonConverter[] Converters)
		{
			JObject.<<WriteToAsync>g__AwaitProperties|0_0>d <<WriteToAsync>g__AwaitProperties|0_0>d;
			<<WriteToAsync>g__AwaitProperties|0_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<WriteToAsync>g__AwaitProperties|0_0>d.<>4__this = this;
			<<WriteToAsync>g__AwaitProperties|0_0>d.task = task;
			<<WriteToAsync>g__AwaitProperties|0_0>d.i = i;
			<<WriteToAsync>g__AwaitProperties|0_0>d.Writer = Writer;
			<<WriteToAsync>g__AwaitProperties|0_0>d.CancellationToken = CancellationToken;
			<<WriteToAsync>g__AwaitProperties|0_0>d.Converters = Converters;
			<<WriteToAsync>g__AwaitProperties|0_0>d.<>1__state = -1;
			<<WriteToAsync>g__AwaitProperties|0_0>d.<>t__builder.Start<JObject.<<WriteToAsync>g__AwaitProperties|0_0>d>(ref <<WriteToAsync>g__AwaitProperties|0_0>d);
			return <<WriteToAsync>g__AwaitProperties|0_0>d.<>t__builder.Task;
		}

		// Token: 0x040003B8 RID: 952
		private readonly JPropertyKeyedCollection _properties = new JPropertyKeyedCollection();

		// Token: 0x020001D1 RID: 465
		[Nullable(new byte[]
		{
			0,
			1
		})]
		private class JObjectDynamicProxy : DynamicProxy<JObject>
		{
			// Token: 0x06000F30 RID: 3888 RVA: 0x0004239A File Offset: 0x0004059A
			public override bool TryGetMember(JObject instance, GetMemberBinder binder, [Nullable(2)] out object result)
			{
				result = instance[binder.Name];
				return true;
			}

			// Token: 0x06000F31 RID: 3889 RVA: 0x000423AC File Offset: 0x000405AC
			public override bool TrySetMember(JObject instance, SetMemberBinder binder, object value)
			{
				JToken jtoken = value as JToken;
				if (jtoken == null)
				{
					jtoken = new JValue(value);
				}
				instance[binder.Name] = jtoken;
				return true;
			}

			// Token: 0x06000F32 RID: 3890 RVA: 0x000423D8 File Offset: 0x000405D8
			public override IEnumerable<string> GetDynamicMemberNames(JObject instance)
			{
				return Enumerable.Select<JProperty, string>(instance.Properties(), (JProperty p) => p.Name);
			}
		}
	}
}

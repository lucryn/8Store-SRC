using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a JSON object.
	/// </summary>
	/// <example>
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\LinqToJsonTests.cs" region="LinqToJsonCreateParse" title="Parsing a JSON Object from Text" />
	/// </example>
	// Token: 0x0200005B RID: 91
	public class JObject : JContainer, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable, INotifyPropertyChanged
	{
		/// <summary>
		/// Gets the container's children tokens.
		/// </summary>
		/// <value>The container's children tokens.</value>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00013109 File Offset: 0x00011309
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004F8 RID: 1272 RVA: 0x00013114 File Offset: 0x00011314
		// (remove) Token: 0x060004F9 RID: 1273 RVA: 0x0001314C File Offset: 0x0001134C
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JObject" /> class.
		/// </summary>
		// Token: 0x060004FA RID: 1274 RVA: 0x00013181 File Offset: 0x00011381
		public JObject()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JObject" /> class from another <see cref="T:Newtonsoft.Json.Linq.JObject" /> object.
		/// </summary>
		/// <param name="other">A <see cref="T:Newtonsoft.Json.Linq.JObject" /> object to copy from.</param>
		// Token: 0x060004FB RID: 1275 RVA: 0x00013194 File Offset: 0x00011394
		public JObject(JObject other) : base(other)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JObject" /> class with the specified content.
		/// </summary>
		/// <param name="content">The contents of the object.</param>
		// Token: 0x060004FC RID: 1276 RVA: 0x000131A8 File Offset: 0x000113A8
		public JObject(params object[] content) : this(content)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JObject" /> class with the specified content.
		/// </summary>
		/// <param name="content">The contents of the object.</param>
		// Token: 0x060004FD RID: 1277 RVA: 0x000131B1 File Offset: 0x000113B1
		public JObject(object content)
		{
			this.Add(content);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000131CC File Offset: 0x000113CC
		internal override bool DeepEquals(JToken node)
		{
			JObject jobject = node as JObject;
			return jobject != null && this._properties.Compare(jobject._properties);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000131F6 File Offset: 0x000113F6
		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			base.InsertItem(index, item, skipParentCheck);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013210 File Offset: 0x00011410
		internal override void ValidateToken(JToken o, JToken existing)
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

		// Token: 0x06000501 RID: 1281 RVA: 0x000132AD File Offset: 0x000114AD
		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(2, childProperty, childProperty, base.IndexOfItem(childProperty)));
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000132D8 File Offset: 0x000114D8
		internal void InternalPropertyChanging(JProperty childProperty)
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000132DA File Offset: 0x000114DA
		internal override JToken CloneToken()
		{
			return new JObject(this);
		}

		/// <summary>
		/// Gets the node type for this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <value>The type.</value>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000132E2 File Offset: 0x000114E2
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.IEnumerable`1" /> of this object's properties.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of this object's properties.</returns>
		// Token: 0x06000505 RID: 1285 RVA: 0x000132E5 File Offset: 0x000114E5
		public IEnumerable<JProperty> Properties()
		{
			return Enumerable.Cast<JProperty>(this._properties);
		}

		/// <summary>
		/// Gets a <see cref="T:Newtonsoft.Json.Linq.JProperty" /> the specified name.
		/// </summary>
		/// <param name="name">The property name.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JProperty" /> with the specified name or null.</returns>
		// Token: 0x06000506 RID: 1286 RVA: 0x000132F4 File Offset: 0x000114F4
		public JProperty Property(string name)
		{
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			this._properties.TryGetValue(name, out jtoken);
			return (JProperty)jtoken;
		}

		/// <summary>
		/// Gets an <see cref="T:Newtonsoft.Json.Linq.JEnumerable`1" /> of this object's property values.
		/// </summary>
		/// <returns>An <see cref="T:Newtonsoft.Json.Linq.JEnumerable`1" /> of this object's property values.</returns>
		// Token: 0x06000507 RID: 1287 RVA: 0x00013323 File Offset: 0x00011523
		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(Enumerable.Select<JProperty, JToken>(this.Properties(), (JProperty p) => p.Value));
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.</value>
		// Token: 0x17000104 RID: 260
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this[text];
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this[text] = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified property name.
		/// </summary>
		/// <value></value>
		// Token: 0x17000105 RID: 261
		public JToken this[string propertyName]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				JProperty jproperty = this.Property(propertyName);
				if (jproperty == null)
				{
					return null;
				}
				return jproperty.Value;
			}
			set
			{
				JProperty jproperty = this.Property(propertyName);
				if (jproperty != null)
				{
					jproperty.Value = value;
					return;
				}
				this.Add(new JProperty(propertyName, value));
				this.OnPropertyChanged(propertyName);
			}
		}

		/// <summary>
		/// Loads an <see cref="T:Newtonsoft.Json.Linq.JObject" /> from a <see cref="T:Newtonsoft.Json.JsonReader" />. 
		/// </summary>
		/// <param name="reader">A <see cref="T:Newtonsoft.Json.JsonReader" /> that will be read for the content of the <see cref="T:Newtonsoft.Json.Linq.JObject" />.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JObject" /> that contains the JSON that was read from the specified <see cref="T:Newtonsoft.Json.JsonReader" />.</returns>
		// Token: 0x0600050C RID: 1292 RVA: 0x00013440 File Offset: 0x00011640
		public new static JObject Load(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
			}
			while (reader.TokenType == JsonToken.Comment)
			{
				reader.Read();
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo);
			jobject.ReadTokenFrom(reader);
			return jobject;
		}

		/// <summary>
		/// Load a <see cref="T:Newtonsoft.Json.Linq.JObject" /> from a string that contains JSON.
		/// </summary>
		/// <param name="json">A <see cref="T:System.String" /> that contains JSON.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JObject" /> populated from the string that contains JSON.</returns>
		/// <example>
		///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\LinqToJsonTests.cs" region="LinqToJsonCreateParse" title="Parsing a JSON Object from Text" />
		/// </example>
		// Token: 0x0600050D RID: 1293 RVA: 0x000134C8 File Offset: 0x000116C8
		public new static JObject Parse(string json)
		{
			JsonReader jsonReader = new JsonTextReader(new StringReader(json));
			JObject result = JObject.Load(jsonReader);
			if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
			{
				throw JsonReaderException.Create(jsonReader, "Additional text found in JSON string after parsing content.");
			}
			return result;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JObject" /> from an object.
		/// </summary>
		/// <param name="o">The object that will be used to create <see cref="T:Newtonsoft.Json.Linq.JObject" />.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JObject" /> with the values of the specified object</returns>
		// Token: 0x0600050E RID: 1294 RVA: 0x00013506 File Offset: 0x00011706
		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, JsonSerializer.CreateDefault());
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JArray" /> from an object.
		/// </summary>
		/// <param name="o">The object that will be used to create <see cref="T:Newtonsoft.Json.Linq.JArray" />.</param>
		/// <param name="jsonSerializer">The <see cref="T:Newtonsoft.Json.JsonSerializer" /> that will be used to read the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JArray" /> with the values of the specified object</returns>
		// Token: 0x0600050F RID: 1295 RVA: 0x00013514 File Offset: 0x00011714
		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken != null && jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JObject)jtoken;
		}

		/// <summary>
		/// Writes this token to a <see cref="T:Newtonsoft.Json.JsonWriter" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		/// <param name="converters">A collection of <see cref="T:Newtonsoft.Json.JsonConverter" /> which will be used when writing the token.</param>
		// Token: 0x06000510 RID: 1296 RVA: 0x0001355C File Offset: 0x0001175C
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			for (int i = 0; i < this._properties.Count; i++)
			{
				this._properties[i].WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified property name.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified property name.</returns>
		// Token: 0x06000511 RID: 1297 RVA: 0x0001359E File Offset: 0x0001179E
		public JToken GetValue(string propertyName)
		{
			return this.GetValue(propertyName, 4);
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified property name.
		/// The exact property name will be searched for first and if no matching property is found then
		/// the <see cref="T:System.StringComparison" /> will be used to match a property.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="comparison">One of the enumeration values that specifies how the strings will be compared.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified property name.</returns>
		// Token: 0x06000512 RID: 1298 RVA: 0x000135A8 File Offset: 0x000117A8
		public JToken GetValue(string propertyName, StringComparison comparison)
		{
			if (propertyName == null)
			{
				return null;
			}
			JProperty jproperty = this.Property(propertyName);
			if (jproperty != null)
			{
				return jproperty.Value;
			}
			if (comparison != 4)
			{
				foreach (JToken jtoken in this._properties)
				{
					JProperty jproperty2 = (JProperty)jtoken;
					if (string.Equals(jproperty2.Name, propertyName, comparison))
					{
						return jproperty2.Value;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Tries to get the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified property name.
		/// The exact property name will be searched for first and if no matching property is found then
		/// the <see cref="T:System.StringComparison" /> will be used to match a property.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="value">The value.</param>
		/// <param name="comparison">One of the enumeration values that specifies how the strings will be compared.</param>
		/// <returns>true if a value was successfully retrieved; otherwise, false.</returns>
		// Token: 0x06000513 RID: 1299 RVA: 0x0001362C File Offset: 0x0001182C
		public bool TryGetValue(string propertyName, StringComparison comparison, out JToken value)
		{
			value = this.GetValue(propertyName, comparison);
			return value != null;
		}

		/// <summary>
		/// Adds the specified property name.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="value">The value.</param>
		// Token: 0x06000514 RID: 1300 RVA: 0x00013640 File Offset: 0x00011840
		public void Add(string propertyName, JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001364F File Offset: 0x0001184F
		bool IDictionary<string, JToken>.ContainsKey(string key)
		{
			return this._properties.Contains(key);
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001365D File Offset: 0x0001185D
		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Keys;
			}
		}

		/// <summary>
		/// Removes the property with the specified name.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>true if item was successfully removed; otherwise, false.</returns>
		// Token: 0x06000517 RID: 1303 RVA: 0x0001366C File Offset: 0x0001186C
		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		/// <summary>
		/// Tries the get value.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="value">The value.</param>
		/// <returns>true if a value was successfully retrieved; otherwise, false.</returns>
		// Token: 0x06000518 RID: 1304 RVA: 0x00013690 File Offset: 0x00011890
		public bool TryGetValue(string propertyName, out JToken value)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000136B6 File Offset: 0x000118B6
		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000136BD File Offset: 0x000118BD
		void ICollection<KeyValuePair<string, JToken>>.Add(KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000136D8 File Offset: 0x000118D8
		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000136E0 File Offset: 0x000118E0
		bool ICollection<KeyValuePair<string, JToken>>.Contains(KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key);
			return jproperty != null && jproperty.Value == item.Value;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00013710 File Offset: 0x00011910
		void ICollection<KeyValuePair<string, JToken>>.CopyTo(KeyValuePair<string, JToken>[] array, int arrayIndex)
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000137D0 File Offset: 0x000119D0
		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000137D3 File Offset: 0x000119D3
		bool ICollection<KeyValuePair<string, JToken>>.Remove(KeyValuePair<string, JToken> item)
		{
			if (!this.Contains(item))
			{
				return false;
			}
			this.Remove(item.Key);
			return true;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000137EF File Offset: 0x000119EF
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		// Token: 0x06000521 RID: 1313 RVA: 0x0001394C File Offset: 0x00011B4C
		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			foreach (JToken jtoken in this._properties)
			{
				JProperty property = (JProperty)jtoken;
				yield return new KeyValuePair<string, JToken>(property.Name, property.Value);
			}
			yield break;
		}

		/// <summary>
		/// Raises the <see cref="E:Newtonsoft.Json.Linq.JObject.PropertyChanged" /> event with the provided arguments.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		// Token: 0x06000522 RID: 1314 RVA: 0x00013968 File Offset: 0x00011B68
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary>
		/// Returns the <see cref="T:System.Dynamic.DynamicMetaObject" /> responsible for binding operations performed on this object.
		/// </summary>
		/// <param name="parameter">The expression tree representation of the runtime value.</param>
		/// <returns>
		/// The <see cref="T:System.Dynamic.DynamicMetaObject" /> to bind this object.
		/// </returns>
		// Token: 0x06000523 RID: 1315 RVA: 0x00013984 File Offset: 0x00011B84
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JObject>(parameter, this, new JObject.JObjectDynamicProxy(), true);
		}

		// Token: 0x0400019E RID: 414
		private readonly JPropertyKeyedCollection _properties = new JPropertyKeyedCollection();

		// Token: 0x0200005D RID: 93
		private class JObjectDynamicProxy : DynamicProxy<JObject>
		{
			// Token: 0x06000533 RID: 1331 RVA: 0x000139E4 File Offset: 0x00011BE4
			public override bool TryGetMember(JObject instance, GetMemberBinder binder, out object result)
			{
				result = instance[binder.Name];
				return true;
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x000139F8 File Offset: 0x00011BF8
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

			// Token: 0x06000535 RID: 1333 RVA: 0x00013A2C File Offset: 0x00011C2C
			public override IEnumerable<string> GetDynamicMemberNames(JObject instance)
			{
				return Enumerable.Select<JProperty, string>(instance.Properties(), (JProperty p) => p.Name);
			}
		}
	}
}

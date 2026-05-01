using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a JSON array.
	/// </summary>
	/// <example>
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\LinqToJsonTests.cs" region="LinqToJsonCreateParseArray" title="Parsing a JSON Array from Text" />
	/// </example>
	// Token: 0x02000058 RID: 88
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		/// <summary>
		/// Gets the container's children tokens.
		/// </summary>
		/// <value>The container's children tokens.</value>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00012B70 File Offset: 0x00010D70
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		/// <summary>
		/// Gets the node type for this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <value>The type.</value>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00012B78 File Offset: 0x00010D78
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JArray" /> class.
		/// </summary>
		// Token: 0x060004C6 RID: 1222 RVA: 0x00012B7B File Offset: 0x00010D7B
		public JArray()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JArray" /> class from another <see cref="T:Newtonsoft.Json.Linq.JArray" /> object.
		/// </summary>
		/// <param name="other">A <see cref="T:Newtonsoft.Json.Linq.JArray" /> object to copy from.</param>
		// Token: 0x060004C7 RID: 1223 RVA: 0x00012B8E File Offset: 0x00010D8E
		public JArray(JArray other) : base(other)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JArray" /> class with the specified content.
		/// </summary>
		/// <param name="content">The contents of the array.</param>
		// Token: 0x060004C8 RID: 1224 RVA: 0x00012BA2 File Offset: 0x00010DA2
		public JArray(params object[] content) : this(content)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JArray" /> class with the specified content.
		/// </summary>
		/// <param name="content">The contents of the array.</param>
		// Token: 0x060004C9 RID: 1225 RVA: 0x00012BAB File Offset: 0x00010DAB
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00012BC8 File Offset: 0x00010DC8
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00012BE8 File Offset: 0x00010DE8
		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		/// <summary>
		/// Loads an <see cref="T:Newtonsoft.Json.Linq.JArray" /> from a <see cref="T:Newtonsoft.Json.JsonReader" />. 
		/// </summary>
		/// <param name="reader">A <see cref="T:Newtonsoft.Json.JsonReader" /> that will be read for the content of the <see cref="T:Newtonsoft.Json.Linq.JArray" />.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JArray" /> that contains the JSON that was read from the specified <see cref="T:Newtonsoft.Json.JsonReader" />.</returns>
		// Token: 0x060004CC RID: 1228 RVA: 0x00012BF0 File Offset: 0x00010DF0
		public new static JArray Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			while (reader.TokenType == JsonToken.Comment)
			{
				reader.Read();
			}
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo);
			jarray.ReadTokenFrom(reader);
			return jarray;
		}

		/// <summary>
		/// Load a <see cref="T:Newtonsoft.Json.Linq.JArray" /> from a string that contains JSON.
		/// </summary>
		/// <param name="json">A <see cref="T:System.String" /> that contains JSON.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JArray" /> populated from the string that contains JSON.</returns>
		/// <example>
		///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\LinqToJsonTests.cs" region="LinqToJsonCreateParseArray" title="Parsing a JSON Array from Text" />
		/// </example>
		// Token: 0x060004CD RID: 1229 RVA: 0x00012C70 File Offset: 0x00010E70
		public new static JArray Parse(string json)
		{
			JsonReader jsonReader = new JsonTextReader(new StringReader(json));
			JArray result = JArray.Load(jsonReader);
			if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
			{
				throw JsonReaderException.Create(jsonReader, "Additional text found in JSON string after parsing content.");
			}
			return result;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JArray" /> from an object.
		/// </summary>
		/// <param name="o">The object that will be used to create <see cref="T:Newtonsoft.Json.Linq.JArray" />.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JArray" /> with the values of the specified object</returns>
		// Token: 0x060004CE RID: 1230 RVA: 0x00012CAE File Offset: 0x00010EAE
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JArray" /> from an object.
		/// </summary>
		/// <param name="o">The object that will be used to create <see cref="T:Newtonsoft.Json.Linq.JArray" />.</param>
		/// <param name="jsonSerializer">The <see cref="T:Newtonsoft.Json.JsonSerializer" /> that will be used to read the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JArray" /> with the values of the specified object</returns>
		// Token: 0x060004CF RID: 1231 RVA: 0x00012CBC File Offset: 0x00010EBC
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		/// <summary>
		/// Writes this token to a <see cref="T:Newtonsoft.Json.JsonWriter" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		/// <param name="converters">A collection of <see cref="T:Newtonsoft.Json.JsonConverter" /> which will be used when writing the token.</param>
		// Token: 0x060004D0 RID: 1232 RVA: 0x00012D00 File Offset: 0x00010F00
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.</value>
		// Token: 0x170000FA RID: 250
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Array position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Array position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> at the specified index.
		/// </summary>
		/// <value></value>
		// Token: 0x170000FB RID: 251
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

		/// <summary>
		/// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <returns>
		/// The index of <paramref name="item" /> if found in the list; otherwise, -1.
		/// </returns>
		// Token: 0x060004D5 RID: 1237 RVA: 0x00012DCE File Offset: 0x00010FCE
		public int IndexOf(JToken item)
		{
			return base.IndexOfItem(item);
		}

		/// <summary>
		/// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// 	<paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
		// Token: 0x060004D6 RID: 1238 RVA: 0x00012DD7 File Offset: 0x00010FD7
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		/// <summary>
		/// Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// 	<paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
		// Token: 0x060004D7 RID: 1239 RVA: 0x00012DE2 File Offset: 0x00010FE2
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		// Token: 0x060004D8 RID: 1240 RVA: 0x00012DEC File Offset: 0x00010FEC
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x060004D9 RID: 1241 RVA: 0x00012E07 File Offset: 0x00011007
		public void Add(JToken item)
		{
			this.Add(item);
		}

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only. </exception>
		// Token: 0x060004DA RID: 1242 RVA: 0x00012E10 File Offset: 0x00011010
		public void Clear()
		{
			this.ClearItems();
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
		/// </returns>
		// Token: 0x060004DB RID: 1243 RVA: 0x00012E18 File Offset: 0x00011018
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="arrayIndex">Index of the array.</param>
		// Token: 0x060004DC RID: 1244 RVA: 0x00012E21 File Offset: 0x00011021
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00012E2B File Offset: 0x0001102B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x060004DE RID: 1246 RVA: 0x00012E2E File Offset: 0x0001102E
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00012E37 File Offset: 0x00011037
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x04000199 RID: 409
		private readonly List<JToken> _values = new List<JToken>();
	}
}

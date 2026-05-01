using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a JSON property.
	/// </summary>
	// Token: 0x0200005F RID: 95
	public class JProperty : JContainer
	{
		/// <summary>
		/// Gets the container's children tokens.
		/// </summary>
		/// <value>The container's children tokens.</value>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00013E80 File Offset: 0x00012080
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		/// <summary>
		/// Gets the property name.
		/// </summary>
		/// <value>The property name.</value>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00013E88 File Offset: 0x00012088
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		/// <value>The property value.</value>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00013E90 File Offset: 0x00012090
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00013EB0 File Offset: 0x000120B0
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				if (this._content.Count <= 0)
				{
					return null;
				}
				return this._content[0];
			}
			set
			{
				base.CheckReentrancy();
				JToken item = value ?? new JValue(null);
				if (this._content.Count == 0)
				{
					this.InsertItem(0, item, false);
					return;
				}
				this.SetItem(0, item);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JProperty" /> class from another <see cref="T:Newtonsoft.Json.Linq.JProperty" /> object.
		/// </summary>
		/// <param name="other">A <see cref="T:Newtonsoft.Json.Linq.JProperty" /> object to copy from.</param>
		// Token: 0x06000542 RID: 1346 RVA: 0x00013EEE File Offset: 0x000120EE
		public JProperty(JProperty other) : base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00013F0E File Offset: 0x0001210E
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00013F20 File Offset: 0x00012120
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
			if (base.Parent != null)
			{
				((JObject)base.Parent).InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			if (base.Parent != null)
			{
				((JObject)base.Parent).InternalPropertyChanged(this);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013F7F File Offset: 0x0001217F
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00013F9F File Offset: 0x0001219F
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013FBF File Offset: 0x000121BF
		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			base.InsertItem(0, item, false);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00013FF1 File Offset: 0x000121F1
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00013FFC File Offset: 0x000121FC
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001401C File Offset: 0x0001221C
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001404F File Offset: 0x0001224F
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		/// <summary>
		/// Gets the node type for this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <value>The type.</value>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00014057 File Offset: 0x00012257
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001405A File Offset: 0x0001225A
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JProperty" /> class.
		/// </summary>
		/// <param name="name">The property name.</param>
		/// <param name="content">The property content.</param>
		// Token: 0x0600054E RID: 1358 RVA: 0x0001407F File Offset: 0x0001227F
		public JProperty(string name, params object[] content) : this(name, content)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JProperty" /> class.
		/// </summary>
		/// <param name="name">The property name.</param>
		/// <param name="content">The property content.</param>
		// Token: 0x0600054F RID: 1359 RVA: 0x0001408C File Offset: 0x0001228C
		public JProperty(string name, object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : base.CreateFromContent(content));
		}

		/// <summary>
		/// Writes this token to a <see cref="T:Newtonsoft.Json.JsonWriter" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		/// <param name="converters">A collection of <see cref="T:Newtonsoft.Json.JsonConverter" /> which will be used when writing the token.</param>
		// Token: 0x06000550 RID: 1360 RVA: 0x000140DC File Offset: 0x000122DC
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

		// Token: 0x06000551 RID: 1361 RVA: 0x0001410E File Offset: 0x0001230E
		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ ((this.Value != null) ? this.Value.GetDeepHashCode() : 0);
		}

		/// <summary>
		/// Loads an <see cref="T:Newtonsoft.Json.Linq.JProperty" /> from a <see cref="T:Newtonsoft.Json.JsonReader" />. 
		/// </summary>
		/// <param name="reader">A <see cref="T:Newtonsoft.Json.JsonReader" /> that will be read for the content of the <see cref="T:Newtonsoft.Json.Linq.JProperty" />.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JProperty" /> that contains the JSON that was read from the specified <see cref="T:Newtonsoft.Json.JsonReader" />.</returns>
		// Token: 0x06000552 RID: 1362 RVA: 0x00014134 File Offset: 0x00012334
		public new static JProperty Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			while (reader.TokenType == JsonToken.Comment)
			{
				reader.Read();
			}
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo);
			jproperty.ReadTokenFrom(reader);
			return jproperty;
		}

		// Token: 0x040001A5 RID: 421
		private readonly List<JToken> _content = new List<JToken>();

		// Token: 0x040001A6 RID: 422
		private readonly string _name;
	}
}

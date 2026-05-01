using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a JSON constructor.
	/// </summary>
	// Token: 0x02000059 RID: 89
	public class JConstructor : JContainer
	{
		/// <summary>
		/// Gets the container's children tokens.
		/// </summary>
		/// <value>The container's children tokens.</value>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00012E3F File Offset: 0x0001103F
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		/// <summary>
		/// Gets or sets the name of this constructor.
		/// </summary>
		/// <value>The constructor name.</value>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00012E47 File Offset: 0x00011047
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00012E4F File Offset: 0x0001104F
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>
		/// Gets the node type for this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <value>The type.</value>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00012E58 File Offset: 0x00011058
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Constructor;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> class.
		/// </summary>
		// Token: 0x060004E4 RID: 1252 RVA: 0x00012E5B File Offset: 0x0001105B
		public JConstructor()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> class from another <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> object.
		/// </summary>
		/// <param name="other">A <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> object to copy from.</param>
		// Token: 0x060004E5 RID: 1253 RVA: 0x00012E6E File Offset: 0x0001106E
		public JConstructor(JConstructor other) : base(other)
		{
			this._name = other.Name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> class with the specified name and content.
		/// </summary>
		/// <param name="name">The constructor name.</param>
		/// <param name="content">The contents of the constructor.</param>
		// Token: 0x060004E6 RID: 1254 RVA: 0x00012E8E File Offset: 0x0001108E
		public JConstructor(string name, params object[] content) : this(name, content)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> class with the specified name and content.
		/// </summary>
		/// <param name="name">The constructor name.</param>
		/// <param name="content">The contents of the constructor.</param>
		// Token: 0x060004E7 RID: 1255 RVA: 0x00012E98 File Offset: 0x00011098
		public JConstructor(string name, object content) : this(name)
		{
			this.Add(content);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> class with the specified name.
		/// </summary>
		/// <param name="name">The constructor name.</param>
		// Token: 0x060004E8 RID: 1256 RVA: 0x00012EA8 File Offset: 0x000110A8
		public JConstructor(string name)
		{
			ValidationUtils.ArgumentNotNullOrEmpty(name, "name");
			this._name = name;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00012ED0 File Offset: 0x000110D0
		internal override bool DeepEquals(JToken node)
		{
			JConstructor jconstructor = node as JConstructor;
			return jconstructor != null && this._name == jconstructor.Name && base.ContentsEqual(jconstructor);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00012F03 File Offset: 0x00011103
		internal override JToken CloneToken()
		{
			return new JConstructor(this);
		}

		/// <summary>
		/// Writes this token to a <see cref="T:Newtonsoft.Json.JsonWriter" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		/// <param name="converters">A collection of <see cref="T:Newtonsoft.Json.JsonConverter" /> which will be used when writing the token.</param>
		// Token: 0x060004EB RID: 1259 RVA: 0x00012F0C File Offset: 0x0001110C
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartConstructor(this._name);
			foreach (JToken jtoken in this.Children())
			{
				jtoken.WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Linq.JToken" /> with the specified key.</value>
		// Token: 0x17000100 RID: 256
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Set JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00012FE9 File Offset: 0x000111E9
		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ base.ContentsHashCode();
		}

		/// <summary>
		/// Loads an <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> from a <see cref="T:Newtonsoft.Json.JsonReader" />. 
		/// </summary>
		/// <param name="reader">A <see cref="T:Newtonsoft.Json.JsonReader" /> that will be read for the content of the <see cref="T:Newtonsoft.Json.Linq.JConstructor" />.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JConstructor" /> that contains the JSON that was read from the specified <see cref="T:Newtonsoft.Json.JsonReader" />.</returns>
		// Token: 0x060004EF RID: 1263 RVA: 0x00013000 File Offset: 0x00011200
		public new static JConstructor Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
			}
			while (reader.TokenType == JsonToken.Comment)
			{
				reader.Read();
			}
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor jconstructor = new JConstructor((string)reader.Value);
			jconstructor.SetLineInfo(reader as IJsonLineInfo);
			jconstructor.ReadTokenFrom(reader);
			return jconstructor;
		}

		// Token: 0x0400019A RID: 410
		private string _name;

		// Token: 0x0400019B RID: 411
		private readonly List<JToken> _values = new List<JToken>();
	}
}

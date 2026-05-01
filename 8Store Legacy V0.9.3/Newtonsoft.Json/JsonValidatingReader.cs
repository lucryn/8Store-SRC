using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Represents a reader that provides <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> validation.
	/// </summary>
	// Token: 0x02000050 RID: 80
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		/// <summary>
		/// Sets an event handler for receiving schema validation errors.
		/// </summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000392 RID: 914 RVA: 0x0000D884 File Offset: 0x0000BA84
		// (remove) Token: 0x06000393 RID: 915 RVA: 0x0000D8BC File Offset: 0x0000BABC
		public event ValidationEventHandler ValidationEventHandler;

		/// <summary>
		/// Gets the text value of the current JSON token.
		/// </summary>
		/// <value></value>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000D8F1 File Offset: 0x0000BAF1
		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		/// <summary>
		/// Gets the depth of the current token in the JSON document.
		/// </summary>
		/// <value>The depth of the current token in the JSON document.</value>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000D8FE File Offset: 0x0000BAFE
		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		/// <summary>
		/// Gets the path of the current JSON token. 
		/// </summary>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D90B File Offset: 0x0000BB0B
		public override string Path
		{
			get
			{
				return this._reader.Path;
			}
		}

		/// <summary>
		/// Gets the quotation mark character used to enclose the value of a string.
		/// </summary>
		/// <value></value>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000D918 File Offset: 0x0000BB18
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000D925 File Offset: 0x0000BB25
		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		/// <summary>
		/// Gets the type of the current JSON token.
		/// </summary>
		/// <value></value>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000D927 File Offset: 0x0000BB27
		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		/// <summary>
		/// Gets the Common Language Runtime (CLR) type for the current JSON token.
		/// </summary>
		/// <value></value>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000D934 File Offset: 0x0000BB34
		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D941 File Offset: 0x0000BB41
		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D958 File Offset: 0x0000BB58
		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope result = this._stack.Pop();
			this._currentScope = ((this._stack.Count != 0) ? this._stack.Peek() : null);
			return result;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D993 File Offset: 0x0000BB93
		private IList<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		private IList<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[]
					{
						this._model
					});
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return JsonValidatingReader.EmptySchemaList;
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new JsonReaderException("CurrentPropertyName has not been set on scope.");
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel jsonSchemaModel2;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, ref jsonSchemaModel2))
						{
							list.Add(jsonSchemaModel2);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
					{
						if (!jsonSchemaModel3.PositionalItemsValidation)
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0)
							{
								list2.Add(jsonSchemaModel3.Items[0]);
							}
						}
						else
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0 && jsonSchemaModel3.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel3.Items[this._currentScope.ArrayItemCount - 1]);
							}
							if (jsonSchemaModel3.AllowAdditionalItems && jsonSchemaModel3.AdditionalItems != null)
							{
								list2.Add(jsonSchemaModel3.AdditionalItems);
							}
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return JsonValidatingReader.EmptySchemaList;
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, this._currentScope.TokenType));
				}
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string message2 = ((IJsonLineInfo)this).HasLineInfo() ? (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition)) : message;
			this.OnValidationEvent(new JsonSchemaException(message2, null, this.Path, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonValidatingReader" /> class that
		/// validates the content returned from the given <see cref="T:Newtonsoft.Json.JsonReader" />.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from while validating.</param>
		// Token: 0x060003A1 RID: 929 RVA: 0x0000DCFA File Offset: 0x0000BEFA
		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		/// <summary>
		/// Gets or sets the schema.
		/// </summary>
		/// <value>The schema.</value>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000DD1F File Offset: 0x0000BF1F
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000DD27 File Offset: 0x0000BF27
		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new InvalidOperationException("Cannot change schema while validating JSON.");
				}
				this._schema = value;
				this._model = null;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.JsonReader" /> used to construct this <see cref="T:Newtonsoft.Json.JsonValidatingReader" />.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.JsonReader" /> specified in the constructor.</value>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000DD4A File Offset: 0x0000BF4A
		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DD54 File Offset: 0x0000BF54
		private void ValidateNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.Value))
			{
				this.RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return default(JsonSchemaType?);
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />.</returns>
		// Token: 0x060003A7 RID: 935 RVA: 0x0000DE38 File Offset: 0x0000C038
		public override int? ReadAsInt32()
		{
			int? result = this._reader.ReadAsInt32();
			this.ValidateCurrentToken();
			return result;
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:Byte[]" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:Byte[]" /> or a null reference if the next JSON token is null.
		/// </returns>
		// Token: 0x060003A8 RID: 936 RVA: 0x0000DE58 File Offset: 0x0000C058
		public override byte[] ReadAsBytes()
		{
			byte[] result = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return result;
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />.</returns>
		// Token: 0x060003A9 RID: 937 RVA: 0x0000DE78 File Offset: 0x0000C078
		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return result;
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.String" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x060003AA RID: 938 RVA: 0x0000DE98 File Offset: 0x0000C098
		public override string ReadAsString()
		{
			string result = this._reader.ReadAsString();
			this.ValidateCurrentToken();
			return result;
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x060003AB RID: 939 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._reader.ReadAsDateTime();
			this.ValidateCurrentToken();
			return result;
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />.</returns>
		// Token: 0x060003AC RID: 940 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return result;
		}

		/// <summary>
		/// Reads the next JSON token from the stream.
		/// </summary>
		/// <returns>
		/// true if the next token was read successfully; false if there are no more tokens to read.
		/// </returns>
		// Token: 0x060003AD RID: 941 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DF20 File Offset: 0x0000C120
		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
				if (!JsonWriter.IsStartToken(this._reader.TokenType))
				{
					this.Push(new JsonValidatingReader.SchemaScope(JTokenType.None, this.CurrentMemberSchemas));
				}
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas = Enumerable.ToList<JsonSchemaModel>(Enumerable.Where<JsonSchemaModel>(this.CurrentMemberSchemas, new Func<JsonSchemaModel, bool>(this.ValidateObject)));
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, schemas));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas2 = Enumerable.ToList<JsonSchemaModel>(Enumerable.Where<JsonSchemaModel>(this.CurrentMemberSchemas, new Func<JsonSchemaModel, bool>(this.ValidateArray)));
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, schemas2));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartConstructor:
				this.ProcessValue();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				this.WriteToken(this.CurrentSchemas);
				return;
			case JsonToken.PropertyName:
				this.WriteToken(this.CurrentSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel schema = enumerator.Current;
						this.ValidatePropertyName(schema);
					}
					return;
				}
				break;
			case JsonToken.Comment:
				goto IL_3BD;
			case JsonToken.Raw:
				break;
			case JsonToken.Integer:
				this.ProcessValue();
				this.WriteToken(this.CurrentMemberSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator2 = this.CurrentMemberSchemas.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						JsonSchemaModel schema2 = enumerator2.Current;
						this.ValidateInteger(schema2);
					}
					return;
				}
				goto IL_1D6;
			case JsonToken.Float:
				goto IL_1D6;
			case JsonToken.String:
				goto IL_222;
			case JsonToken.Boolean:
				goto IL_26E;
			case JsonToken.Null:
				goto IL_2BA;
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.WriteToken(this.CurrentMemberSchemas);
				return;
			case JsonToken.EndObject:
				goto IL_306;
			case JsonToken.EndArray:
				this.WriteToken(this.CurrentSchemas);
				foreach (JsonSchemaModel schema3 in this.CurrentSchemas)
				{
					this.ValidateEndArray(schema3);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.WriteToken(this.CurrentSchemas);
				this.Pop();
				return;
			default:
				goto IL_3BD;
			}
			this.ProcessValue();
			return;
			IL_1D6:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator4 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					JsonSchemaModel schema4 = enumerator4.Current;
					this.ValidateFloat(schema4);
				}
				return;
			}
			IL_222:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator5 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					JsonSchemaModel schema5 = enumerator5.Current;
					this.ValidateString(schema5);
				}
				return;
			}
			IL_26E:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator6 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					JsonSchemaModel schema6 = enumerator6.Current;
					this.ValidateBoolean(schema6);
				}
				return;
			}
			IL_2BA:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator7 = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator7.MoveNext())
				{
					JsonSchemaModel schema7 = enumerator7.Current;
					this.ValidateNull(schema7);
				}
				return;
			}
			IL_306:
			this.WriteToken(this.CurrentSchemas);
			foreach (JsonSchemaModel schema8 in this.CurrentSchemas)
			{
				this.ValidateEndObject(schema8);
			}
			this.Pop();
			return;
			IL_3BD:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000E378 File Offset: 0x0000C578
		private void WriteToken(IList<JsonSchemaModel> schemas)
		{
			foreach (JsonValidatingReader.SchemaScope schemaScope in this._stack)
			{
				bool flag = schemaScope.TokenType == JTokenType.Array && schemaScope.IsUniqueArray && schemaScope.ArrayItemCount > 0;
				if (!flag && !schemaScope.IsEnum)
				{
					if (!Enumerable.Any<JsonSchemaModel>(schemas, (JsonSchemaModel s) => s.Enum != null))
					{
						continue;
					}
				}
				if (schemaScope.CurrentItemWriter == null)
				{
					if (JsonWriter.IsEndToken(this._reader.TokenType))
					{
						continue;
					}
					schemaScope.CurrentItemWriter = new JTokenWriter();
				}
				schemaScope.CurrentItemWriter.WriteToken(this._reader, false);
				if (schemaScope.CurrentItemWriter.Top == 0 && this._reader.TokenType != JsonToken.PropertyName)
				{
					JToken token = schemaScope.CurrentItemWriter.Token;
					schemaScope.CurrentItemWriter = null;
					if (flag)
					{
						if (Enumerable.Contains<JToken>(schemaScope.UniqueArrayItems, token, JToken.EqualityComparer))
						{
							this.RaiseError("Non-unique array item at index {0}.".FormatWith(CultureInfo.InvariantCulture, schemaScope.ArrayItemCount - 1), Enumerable.First<JsonSchemaModel>(schemaScope.Schemas, (JsonSchemaModel s) => s.UniqueItems));
						}
						schemaScope.UniqueArrayItems.Add(token);
					}
					else
					{
						if (!schemaScope.IsEnum)
						{
							if (!Enumerable.Any<JsonSchemaModel>(schemas, (JsonSchemaModel s) => s.Enum != null))
							{
								continue;
							}
						}
						foreach (JsonSchemaModel jsonSchemaModel in schemas)
						{
							if (jsonSchemaModel.Enum != null && !jsonSchemaModel.Enum.ContainsValue(token, JToken.EqualityComparer))
							{
								StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
								token.WriteTo(new JsonTextWriter(stringWriter), new JsonConverter[0]);
								this.RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, stringWriter.ToString()), jsonSchemaModel);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				List<string> list = Enumerable.ToList<string>(Enumerable.Select<KeyValuePair<string, bool>, string>(Enumerable.Where<KeyValuePair<string, bool>>(requiredProperties, (KeyValuePair<string, bool> kv) => !kv.Value), (KeyValuePair<string, bool> kv) => kv.Key));
				if (list.Count > 0)
				{
					this.RaiseError("Required properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", list.ToArray())), schema);
				}
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000E684 File Offset: 0x0000C884
		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null && arrayItemCount > schema.MaximumItems)
			{
				this.RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
			}
			if (schema.MinimumItems != null && arrayItemCount < schema.MinimumItems)
			{
				this.RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000E751 File Offset: 0x0000C951
		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000E76A File Offset: 0x0000C96A
		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E784 File Offset: 0x0000C984
		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null && text.Length > schema.MaximumLength)
			{
				this.RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
			}
			if (schema.MinimumLength != null && text.Length < schema.MinimumLength)
			{
				this.RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, text2), schema);
					}
				}
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			object value = this._reader.Value;
			if (schema.Maximum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Maximum) > 0)
				{
					this.RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && JValue.Compare(JTokenType.Integer, value, schema.Maximum) == 0)
				{
					this.RaiseError("Integer {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
			}
			if (schema.Minimum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Minimum) < 0)
				{
					this.RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && JValue.Compare(JTokenType.Integer, value, schema.Minimum) == 0)
				{
					this.RaiseError("Integer {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy != null)
			{
				bool flag2;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					bool flag = !Math.Abs(schema.DivisibleBy.Value - Math.Truncate(schema.DivisibleBy.Value)).Equals(0.0);
					if (flag)
					{
						flag2 = (bigInteger != 0L);
					}
					else
					{
						flag2 = (bigInteger % new BigInteger(schema.DivisibleBy.Value) != 0L);
					}
				}
				else
				{
					flag2 = !JsonValidatingReader.IsZero((double)Convert.ToInt64(value, CultureInfo.InvariantCulture) % schema.DivisibleBy.Value);
				}
				if (flag2)
				{
					this.RaiseError("Integer {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(value), schema.DivisibleBy), schema);
				}
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				this._currentScope.ArrayItemCount++;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.PositionalItemsValidation && !jsonSchemaModel.AllowAdditionalItems && (jsonSchemaModel.Items == null || this._currentScope.ArrayItemCount - 1 >= jsonSchemaModel.Items.Count))
					{
						this.RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, this._currentScope.ArrayItemCount), jsonSchemaModel);
					}
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double num2 = num;
				double? maximum = schema.Maximum;
				if (num2 > maximum.GetValueOrDefault() && maximum != null)
				{
					this.RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum)
				{
					double num3 = num;
					double? maximum2 = schema.Maximum;
					if (num3 == maximum2.GetValueOrDefault() && maximum2 != null)
					{
						this.RaiseError("Float {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
					}
				}
			}
			if (schema.Minimum != null)
			{
				double num4 = num;
				double? minimum = schema.Minimum;
				if (num4 < minimum.GetValueOrDefault() && minimum != null)
				{
					this.RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum)
				{
					double num5 = num;
					double? minimum2 = schema.Minimum;
					if (num5 == minimum2.GetValueOrDefault() && minimum2 != null)
					{
						this.RaiseError("Float {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
					}
				}
			}
			if (schema.DivisibleBy != null)
			{
				double value = JsonValidatingReader.FloatingPointRemainder(num, schema.DivisibleBy.Value);
				if (!JsonValidatingReader.IsZero(value))
				{
					this.RaiseError("Float {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.DivisibleBy), schema);
				}
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		private static double FloatingPointRemainder(double dividend, double divisor)
		{
			return dividend - Math.Floor(dividend / divisor) * divisor;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000EDBA File Offset: 0x0000CFBA
		private static bool IsZero(double value)
		{
			return Math.Abs(value) < 2.220446049250313E-15;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, text), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000EE54 File Offset: 0x0000D054
		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string text in schema.PatternProperties.Keys)
				{
					if (Regex.IsMatch(propertyName, text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000EF30 File Offset: 0x0000D130
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000EF54 File Offset: 0x0000D154
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000EF78 File Offset: 0x0000D178
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x04000169 RID: 361
		private readonly JsonReader _reader;

		// Token: 0x0400016A RID: 362
		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		// Token: 0x0400016B RID: 363
		private JsonSchema _schema;

		// Token: 0x0400016C RID: 364
		private JsonSchemaModel _model;

		// Token: 0x0400016D RID: 365
		private JsonValidatingReader.SchemaScope _currentScope;

		// Token: 0x0400016F RID: 367
		private static readonly IList<JsonSchemaModel> EmptySchemaList = new List<JsonSchemaModel>();

		// Token: 0x02000051 RID: 81
		private class SchemaScope
		{
			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
			// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
			public string CurrentPropertyName { get; set; }

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060003CA RID: 970 RVA: 0x0000EFB9 File Offset: 0x0000D1B9
			// (set) Token: 0x060003CB RID: 971 RVA: 0x0000EFC1 File Offset: 0x0000D1C1
			public int ArrayItemCount { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060003CC RID: 972 RVA: 0x0000EFCA File Offset: 0x0000D1CA
			// (set) Token: 0x060003CD RID: 973 RVA: 0x0000EFD2 File Offset: 0x0000D1D2
			public bool IsUniqueArray { get; set; }

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060003CE RID: 974 RVA: 0x0000EFDB File Offset: 0x0000D1DB
			// (set) Token: 0x060003CF RID: 975 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
			public bool IsEnum { get; set; }

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000EFEC File Offset: 0x0000D1EC
			// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
			public IList<JToken> UniqueArrayItems { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000EFFD File Offset: 0x0000D1FD
			// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000F005 File Offset: 0x0000D205
			public JTokenWriter CurrentItemWriter { get; set; }

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000F00E File Offset: 0x0000D20E
			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000F016 File Offset: 0x0000D216
			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000F01E File Offset: 0x0000D21E
			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x0000F034 File Offset: 0x0000D234
			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = Enumerable.ToDictionary<string, string, bool>(Enumerable.Distinct<string>(Enumerable.SelectMany<JsonSchemaModel, string>(schemas, new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties))), (string p) => p, (string p) => false);
				if (tokenType == JTokenType.Array)
				{
					if (Enumerable.Any<JsonSchemaModel>(schemas, (JsonSchemaModel s) => s.UniqueItems))
					{
						this.IsUniqueArray = true;
						this.UniqueArrayItems = new List<JToken>();
					}
				}
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x0000F104 File Offset: 0x0000D304
			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (schema == null || schema.Properties == null)
				{
					return Enumerable.Empty<string>();
				}
				return Enumerable.Select<KeyValuePair<string, JsonSchemaModel>, string>(Enumerable.Where<KeyValuePair<string, JsonSchemaModel>>(schema.Properties, (KeyValuePair<string, JsonSchemaModel> p) => p.Value.Required), (KeyValuePair<string, JsonSchemaModel> p) => p.Key);
			}

			// Token: 0x04000175 RID: 373
			private readonly JTokenType _tokenType;

			// Token: 0x04000176 RID: 374
			private readonly IList<JsonSchemaModel> _schemas;

			// Token: 0x04000177 RID: 375
			private readonly Dictionary<string, bool> _requiredProperties;
		}
	}
}

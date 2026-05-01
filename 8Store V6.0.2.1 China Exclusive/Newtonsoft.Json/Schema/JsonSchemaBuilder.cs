using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000AF RID: 175
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaBuilder
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x00023FE9 File Offset: 0x000221E9
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._documentSchemas = new Dictionary<string, JsonSchema>();
			this._resolver = resolver;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002400E File Offset: 0x0002220E
		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
			this._documentSchemas.Add(value.Location, value);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00024046 File Offset: 0x00022246
		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = Enumerable.LastOrDefault<JsonSchema>(this._stack);
			return currentSchema;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00024077 File Offset: 0x00022277
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00024080 File Offset: 0x00022280
		internal JsonSchema Read(JsonReader reader)
		{
			JToken jtoken = JToken.ReadFrom(reader);
			this._rootSchema = (jtoken as JObject);
			JsonSchema jsonSchema = this.BuildSchema(jtoken);
			this.ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000240B1 File Offset: 0x000222B1
		private string UnescapeReference(string reference)
		{
			return StringUtils.Replace(StringUtils.Replace(Uri.UnescapeDataString(reference), "~1", "/"), "~0", "~");
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000240D8 File Offset: 0x000222D8
		private JsonSchema ResolveReferences(JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag = text.StartsWith("#", 4);
				if (flag)
				{
					text = this.UnescapeReference(text);
				}
				JsonSchema jsonSchema = this._resolver.GetSchema(text);
				if (jsonSchema == null)
				{
					if (flag)
					{
						string[] array = schema.DeferredReference.TrimStart(new char[]
						{
							'#'
						}).Split(new char[]
						{
							'/'
						}, 1);
						JToken jtoken = this._rootSchema;
						foreach (string reference in array)
						{
							string text2 = this.UnescapeReference(reference);
							if (jtoken.Type == JTokenType.Object)
							{
								jtoken = jtoken[text2];
							}
							else if (jtoken.Type == JTokenType.Array || jtoken.Type == JTokenType.Constructor)
							{
								int num;
								if (int.TryParse(text2, ref num) && num >= 0 && num < Enumerable.Count<JToken>(jtoken))
								{
									jtoken = jtoken[num];
								}
								else
								{
									jtoken = null;
								}
							}
							if (jtoken == null)
							{
								break;
							}
						}
						if (jtoken != null)
						{
							jsonSchema = this.BuildSchema(jtoken);
						}
					}
					if (jsonSchema == null)
					{
						throw new JsonException("Could not resolve schema reference '{0}'.".FormatWith(CultureInfo.InvariantCulture, schema.DeferredReference));
					}
				}
				schema = jsonSchema;
			}
			if (schema.ReferencesResolved)
			{
				return schema;
			}
			schema.ReferencesResolved = true;
			if (schema.Extends != null)
			{
				for (int j = 0; j < schema.Extends.Count; j++)
				{
					schema.Extends[j] = this.ResolveReferences(schema.Extends[j]);
				}
			}
			if (schema.Items != null)
			{
				for (int k = 0; k < schema.Items.Count; k++)
				{
					schema.Items[k] = this.ResolveReferences(schema.Items[k]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				schema.AdditionalItems = this.ResolveReferences(schema.AdditionalItems);
			}
			if (schema.PatternProperties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in Enumerable.ToList<KeyValuePair<string, JsonSchema>>(schema.PatternProperties))
				{
					schema.PatternProperties[keyValuePair.Key] = this.ResolveReferences(keyValuePair.Value);
				}
			}
			if (schema.Properties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair2 in Enumerable.ToList<KeyValuePair<string, JsonSchema>>(schema.Properties))
				{
					schema.Properties[keyValuePair2.Key] = this.ResolveReferences(keyValuePair2.Value);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				schema.AdditionalProperties = this.ResolveReferences(schema.AdditionalProperties);
			}
			return schema;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000243A0 File Offset: 0x000225A0
		private JsonSchema BuildSchema(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject == null)
			{
				throw JsonException.Create(token, token.Path, "Expected object while parsing schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			JToken value;
			if (jobject.TryGetValue("$ref", out value))
			{
				return new JsonSchema
				{
					DeferredReference = (string)value
				};
			}
			string text = token.Path;
			text = StringUtils.Replace(text, ".", "/");
			text = StringUtils.Replace(text, "[", "/");
			text = StringUtils.Replace(text, "]", string.Empty);
			if (!StringUtils.IsNullOrEmpty(text))
			{
				text = "/" + text;
			}
			text = "#" + text;
			JsonSchema result;
			if (this._documentSchemas.TryGetValue(text, ref result))
			{
				return result;
			}
			this.Push(new JsonSchema
			{
				Location = text
			});
			this.ProcessSchemaProperties(jobject);
			return this.Pop();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002448C File Offset: 0x0002268C
		private void ProcessSchemaProperties(JObject schemaObject)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in schemaObject)
			{
				string key = keyValuePair.Key;
				if (key != null)
				{
					switch (key.Length)
					{
					case 2:
						if (key == "id")
						{
							this.CurrentSchema.Id = (string)keyValuePair.Value;
						}
						break;
					case 4:
					{
						char c = key.get_Chars(0);
						if (c != 'e')
						{
							if (c == 't')
							{
								if (key == "type")
								{
									this.CurrentSchema.Type = this.ProcessType(keyValuePair.Value);
								}
							}
						}
						else if (key == "enum")
						{
							this.ProcessEnum(keyValuePair.Value);
						}
						break;
					}
					case 5:
					{
						char c = key.get_Chars(0);
						if (c != 'i')
						{
							if (c == 't')
							{
								if (key == "title")
								{
									this.CurrentSchema.Title = (string)keyValuePair.Value;
								}
							}
						}
						else if (key == "items")
						{
							this.ProcessItems(keyValuePair.Value);
						}
						break;
					}
					case 6:
					{
						char c = key.get_Chars(0);
						if (c != 'f')
						{
							if (c == 'h')
							{
								if (key == "hidden")
								{
									this.CurrentSchema.Hidden = new bool?((bool)keyValuePair.Value);
								}
							}
						}
						else if (key == "format")
						{
							this.CurrentSchema.Format = (string)keyValuePair.Value;
						}
						break;
					}
					case 7:
					{
						char c = key.get_Chars(0);
						if (c <= 'e')
						{
							if (c != 'd')
							{
								if (c == 'e')
								{
									if (key == "extends")
									{
										this.ProcessExtends(keyValuePair.Value);
									}
								}
							}
							else if (key == "default")
							{
								this.CurrentSchema.Default = keyValuePair.Value.DeepClone();
							}
						}
						else if (c != 'm')
						{
							if (c == 'p')
							{
								if (key == "pattern")
								{
									this.CurrentSchema.Pattern = (string)keyValuePair.Value;
								}
							}
						}
						else if (!(key == "minimum"))
						{
							if (key == "maximum")
							{
								this.CurrentSchema.Maximum = new double?((double)keyValuePair.Value);
							}
						}
						else
						{
							this.CurrentSchema.Minimum = new double?((double)keyValuePair.Value);
						}
						break;
					}
					case 8:
					{
						char c = key.get_Chars(2);
						if (c <= 'n')
						{
							if (c != 'a')
							{
								if (c == 'n')
								{
									if (key == "minItems")
									{
										this.CurrentSchema.MinimumItems = new int?((int)keyValuePair.Value);
									}
								}
							}
							else if (key == "readonly")
							{
								this.CurrentSchema.ReadOnly = new bool?((bool)keyValuePair.Value);
							}
						}
						else if (c != 'q')
						{
							if (c != 's')
							{
								if (c == 'x')
								{
									if (key == "maxItems")
									{
										this.CurrentSchema.MaximumItems = new int?((int)keyValuePair.Value);
									}
								}
							}
							else if (key == "disallow")
							{
								this.CurrentSchema.Disallow = this.ProcessType(keyValuePair.Value);
							}
						}
						else if (!(key == "required"))
						{
							if (key == "requires")
							{
								this.CurrentSchema.Requires = (string)keyValuePair.Value;
							}
						}
						else
						{
							this.CurrentSchema.Required = new bool?((bool)keyValuePair.Value);
						}
						break;
					}
					case 9:
					{
						char c = key.get_Chars(1);
						if (c != 'a')
						{
							if (c == 'i')
							{
								if (key == "minLength")
								{
									this.CurrentSchema.MinimumLength = new int?((int)keyValuePair.Value);
								}
							}
						}
						else if (key == "maxLength")
						{
							this.CurrentSchema.MaximumLength = new int?((int)keyValuePair.Value);
						}
						break;
					}
					case 10:
						if (key == "properties")
						{
							this.CurrentSchema.Properties = this.ProcessProperties(keyValuePair.Value);
						}
						break;
					case 11:
					{
						char c = key.get_Chars(1);
						if (c != 'e')
						{
							if (c != 'i')
							{
								if (c == 'n')
								{
									if (key == "uniqueItems")
									{
										this.CurrentSchema.UniqueItems = (bool)keyValuePair.Value;
									}
								}
							}
							else if (key == "divisibleBy")
							{
								this.CurrentSchema.DivisibleBy = new double?((double)keyValuePair.Value);
							}
						}
						else if (key == "description")
						{
							this.CurrentSchema.Description = (string)keyValuePair.Value;
						}
						break;
					}
					case 15:
						if (key == "additionalItems")
						{
							this.ProcessAdditionalItems(keyValuePair.Value);
						}
						break;
					case 16:
					{
						char c = key.get_Chars(10);
						if (c != 'a')
						{
							if (c == 'i')
							{
								if (key == "exclusiveMinimum")
								{
									this.CurrentSchema.ExclusiveMinimum = new bool?((bool)keyValuePair.Value);
								}
							}
						}
						else if (key == "exclusiveMaximum")
						{
							this.CurrentSchema.ExclusiveMaximum = new bool?((bool)keyValuePair.Value);
						}
						break;
					}
					case 17:
						if (key == "patternProperties")
						{
							this.CurrentSchema.PatternProperties = this.ProcessProperties(keyValuePair.Value);
						}
						break;
					case 20:
						if (key == "additionalProperties")
						{
							this.ProcessAdditionalProperties(keyValuePair.Value);
						}
						break;
					}
				}
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00024C1C File Offset: 0x00022E1C
		private void ProcessExtends(JToken token)
		{
			IList<JsonSchema> list = new List<JsonSchema>();
			if (token.Type == JTokenType.Array)
			{
				using (IEnumerator<JToken> enumerator = token.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken token2 = enumerator.Current;
						list.Add(this.BuildSchema(token2));
					}
					goto IL_52;
				}
			}
			JsonSchema jsonSchema = this.BuildSchema(token);
			if (jsonSchema != null)
			{
				list.Add(jsonSchema);
			}
			IL_52:
			if (list.Count > 0)
			{
				this.CurrentSchema.Extends = list;
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00024CA0 File Offset: 0x00022EA0
		private void ProcessEnum(JToken token)
		{
			if (token.Type != JTokenType.Array)
			{
				throw JsonException.Create(token, token.Path, "Expected Array token while parsing enum values, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Enum = new List<JToken>();
			foreach (JToken jtoken in token)
			{
				this.CurrentSchema.Enum.Add(jtoken.DeepClone());
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00024D38 File Offset: 0x00022F38
		private void ProcessAdditionalProperties(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalProperties = this.BuildSchema(token);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00024D68 File Offset: 0x00022F68
		private void ProcessAdditionalItems(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalItems = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalItems = this.BuildSchema(token);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00024D98 File Offset: 0x00022F98
		private IDictionary<string, JsonSchema> ProcessProperties(JToken token)
		{
			IDictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (token.Type != JTokenType.Object)
			{
				throw JsonException.Create(token, token.Path, "Expected Object token while parsing schema properties, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			foreach (JToken jtoken in token)
			{
				JProperty jproperty = (JProperty)jtoken;
				if (dictionary.ContainsKey(jproperty.Name))
				{
					throw new JsonException("Property {0} has already been defined in schema.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name));
				}
				dictionary.Add(jproperty.Name, this.BuildSchema(jproperty.Value));
			}
			return dictionary;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00024E58 File Offset: 0x00023058
		private void ProcessItems(JToken token)
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			JTokenType type = token.Type;
			if (type != JTokenType.Object)
			{
				if (type == JTokenType.Array)
				{
					this.CurrentSchema.PositionalItemsValidation = true;
					using (IEnumerator<JToken> enumerator = token.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JToken token2 = enumerator.Current;
							this.CurrentSchema.Items.Add(this.BuildSchema(token2));
						}
						return;
					}
				}
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Items.Add(this.BuildSchema(token));
			this.CurrentSchema.PositionalItemsValidation = false;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00024F28 File Offset: 0x00023128
		private JsonSchemaType? ProcessType(JToken token)
		{
			JTokenType type = token.Type;
			if (type == JTokenType.Array)
			{
				JsonSchemaType? jsonSchemaType = new JsonSchemaType?(JsonSchemaType.None);
				foreach (JToken jtoken in token)
				{
					if (jtoken.Type != JTokenType.String)
					{
						throw JsonException.Create(jtoken, jtoken.Path, "Expected JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
					}
					jsonSchemaType |= JsonSchemaBuilder.MapType((string)jtoken);
				}
				return jsonSchemaType;
			}
			if (type != JTokenType.String)
			{
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			return new JsonSchemaType?(JsonSchemaBuilder.MapType((string)token));
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00025028 File Offset: 0x00023228
		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType result;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, ref result))
			{
				throw new JsonException("Invalid JSON schema type: {0}".FormatWith(CultureInfo.InvariantCulture, type));
			}
			return result;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002505C File Offset: 0x0002325C
		internal static string MapType(JsonSchemaType type)
		{
			return Enumerable.Single<KeyValuePair<string, JsonSchemaType>>(JsonSchemaConstants.JsonSchemaTypeMapping, (KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		// Token: 0x04000348 RID: 840
		private readonly IList<JsonSchema> _stack;

		// Token: 0x04000349 RID: 841
		private readonly JsonSchemaResolver _resolver;

		// Token: 0x0400034A RID: 842
		private readonly IDictionary<string, JsonSchema> _documentSchemas;

		// Token: 0x0400034B RID: 843
		private JsonSchema _currentSchema;

		// Token: 0x0400034C RID: 844
		private JObject _rootSchema;
	}
}

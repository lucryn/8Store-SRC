using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000071 RID: 113
	internal class JsonSchemaBuilder
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x000162F7 File Offset: 0x000144F7
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._documentSchemas = new Dictionary<string, JsonSchema>();
			this._resolver = resolver;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001631C File Offset: 0x0001451C
		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
			this._documentSchemas.Add(value.Location, value);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00016354 File Offset: 0x00014554
		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = Enumerable.LastOrDefault<JsonSchema>(this._stack);
			return currentSchema;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00016392 File Offset: 0x00014592
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001639C File Offset: 0x0001459C
		internal JsonSchema Read(JsonReader reader)
		{
			JToken jtoken = JToken.ReadFrom(reader);
			this._rootSchema = (jtoken as JObject);
			JsonSchema jsonSchema = this.BuildSchema(jtoken);
			this.ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000163CD File Offset: 0x000145CD
		private string UnescapeReference(string reference)
		{
			return Uri.UnescapeDataString(reference).Replace("~1", "/").Replace("~0", "~");
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000163F4 File Offset: 0x000145F4
		private JsonSchema ResolveReferences(JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag = text.StartsWith("#", 5);
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

		// Token: 0x06000630 RID: 1584 RVA: 0x000166D4 File Offset: 0x000148D4
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
			string text = token.Path.Replace(".", "/").Replace("[", "/").Replace("]", string.Empty);
			if (!string.IsNullOrEmpty(text))
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

		// Token: 0x06000631 RID: 1585 RVA: 0x000167C0 File Offset: 0x000149C0
		private void ProcessSchemaProperties(JObject schemaObject)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in schemaObject)
			{
				string key;
				if ((key = keyValuePair.Key) != null)
				{
					if (<PrivateImplementationDetails>{1F889241-B8CF-4E84-B28B-D119DB42D29C}.$$method0x6000614-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(29);
						dictionary.Add("type", 0);
						dictionary.Add("id", 1);
						dictionary.Add("title", 2);
						dictionary.Add("description", 3);
						dictionary.Add("properties", 4);
						dictionary.Add("items", 5);
						dictionary.Add("additionalProperties", 6);
						dictionary.Add("additionalItems", 7);
						dictionary.Add("patternProperties", 8);
						dictionary.Add("required", 9);
						dictionary.Add("requires", 10);
						dictionary.Add("minimum", 11);
						dictionary.Add("maximum", 12);
						dictionary.Add("exclusiveMinimum", 13);
						dictionary.Add("exclusiveMaximum", 14);
						dictionary.Add("maxLength", 15);
						dictionary.Add("minLength", 16);
						dictionary.Add("maxItems", 17);
						dictionary.Add("minItems", 18);
						dictionary.Add("divisibleBy", 19);
						dictionary.Add("disallow", 20);
						dictionary.Add("default", 21);
						dictionary.Add("hidden", 22);
						dictionary.Add("readonly", 23);
						dictionary.Add("format", 24);
						dictionary.Add("pattern", 25);
						dictionary.Add("enum", 26);
						dictionary.Add("extends", 27);
						dictionary.Add("uniqueItems", 28);
						<PrivateImplementationDetails>{1F889241-B8CF-4E84-B28B-D119DB42D29C}.$$method0x6000614-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{1F889241-B8CF-4E84-B28B-D119DB42D29C}.$$method0x6000614-1.TryGetValue(key, ref num))
					{
						switch (num)
						{
						case 0:
							this.CurrentSchema.Type = this.ProcessType(keyValuePair.Value);
							break;
						case 1:
							this.CurrentSchema.Id = (string)keyValuePair.Value;
							break;
						case 2:
							this.CurrentSchema.Title = (string)keyValuePair.Value;
							break;
						case 3:
							this.CurrentSchema.Description = (string)keyValuePair.Value;
							break;
						case 4:
							this.CurrentSchema.Properties = this.ProcessProperties(keyValuePair.Value);
							break;
						case 5:
							this.ProcessItems(keyValuePair.Value);
							break;
						case 6:
							this.ProcessAdditionalProperties(keyValuePair.Value);
							break;
						case 7:
							this.ProcessAdditionalItems(keyValuePair.Value);
							break;
						case 8:
							this.CurrentSchema.PatternProperties = this.ProcessProperties(keyValuePair.Value);
							break;
						case 9:
							this.CurrentSchema.Required = new bool?((bool)keyValuePair.Value);
							break;
						case 10:
							this.CurrentSchema.Requires = (string)keyValuePair.Value;
							break;
						case 11:
							this.CurrentSchema.Minimum = new double?((double)keyValuePair.Value);
							break;
						case 12:
							this.CurrentSchema.Maximum = new double?((double)keyValuePair.Value);
							break;
						case 13:
							this.CurrentSchema.ExclusiveMinimum = new bool?((bool)keyValuePair.Value);
							break;
						case 14:
							this.CurrentSchema.ExclusiveMaximum = new bool?((bool)keyValuePair.Value);
							break;
						case 15:
							this.CurrentSchema.MaximumLength = new int?((int)keyValuePair.Value);
							break;
						case 16:
							this.CurrentSchema.MinimumLength = new int?((int)keyValuePair.Value);
							break;
						case 17:
							this.CurrentSchema.MaximumItems = new int?((int)keyValuePair.Value);
							break;
						case 18:
							this.CurrentSchema.MinimumItems = new int?((int)keyValuePair.Value);
							break;
						case 19:
							this.CurrentSchema.DivisibleBy = new double?((double)keyValuePair.Value);
							break;
						case 20:
							this.CurrentSchema.Disallow = this.ProcessType(keyValuePair.Value);
							break;
						case 21:
							this.CurrentSchema.Default = keyValuePair.Value.DeepClone();
							break;
						case 22:
							this.CurrentSchema.Hidden = new bool?((bool)keyValuePair.Value);
							break;
						case 23:
							this.CurrentSchema.ReadOnly = new bool?((bool)keyValuePair.Value);
							break;
						case 24:
							this.CurrentSchema.Format = (string)keyValuePair.Value;
							break;
						case 25:
							this.CurrentSchema.Pattern = (string)keyValuePair.Value;
							break;
						case 26:
							this.ProcessEnum(keyValuePair.Value);
							break;
						case 27:
							this.ProcessExtends(keyValuePair.Value);
							break;
						case 28:
							this.CurrentSchema.UniqueItems = (bool)keyValuePair.Value;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00016D68 File Offset: 0x00014F68
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

		// Token: 0x06000633 RID: 1587 RVA: 0x00016DEC File Offset: 0x00014FEC
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

		// Token: 0x06000634 RID: 1588 RVA: 0x00016E84 File Offset: 0x00015084
		private void ProcessAdditionalProperties(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalProperties = this.BuildSchema(token);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00016EB4 File Offset: 0x000150B4
		private void ProcessAdditionalItems(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalItems = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalItems = this.BuildSchema(token);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00016EE4 File Offset: 0x000150E4
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

		// Token: 0x06000637 RID: 1591 RVA: 0x00016FA4 File Offset: 0x000151A4
		private void ProcessItems(JToken token)
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			switch (token.Type)
			{
			case JTokenType.Object:
				this.CurrentSchema.Items.Add(this.BuildSchema(token));
				this.CurrentSchema.PositionalItemsValidation = false;
				return;
			case JTokenType.Array:
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
				break;
			}
			throw JsonException.Create(token, token.Path, "Expected array or JSON schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001707C File Offset: 0x0001527C
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
						throw JsonException.Create(jtoken, jtoken.Path, "Exception JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
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

		// Token: 0x06000639 RID: 1593 RVA: 0x0001717C File Offset: 0x0001537C
		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType result;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, ref result))
			{
				throw new JsonException("Invalid JSON schema type: {0}".FormatWith(CultureInfo.InvariantCulture, type));
			}
			return result;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000171C8 File Offset: 0x000153C8
		internal static string MapType(JsonSchemaType type)
		{
			return Enumerable.Single<KeyValuePair<string, JsonSchemaType>>(JsonSchemaConstants.JsonSchemaTypeMapping, (KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		// Token: 0x04000204 RID: 516
		private readonly IList<JsonSchema> _stack;

		// Token: 0x04000205 RID: 517
		private readonly JsonSchemaResolver _resolver;

		// Token: 0x04000206 RID: 518
		private readonly IDictionary<string, JsonSchema> _documentSchemas;

		// Token: 0x04000207 RID: 519
		private JsonSchema _currentSchema;

		// Token: 0x04000208 RID: 520
		private JObject _rootSchema;
	}
}

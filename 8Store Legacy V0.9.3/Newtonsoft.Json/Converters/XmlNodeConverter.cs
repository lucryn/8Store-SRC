using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Converts XML to and from JSON.
	/// </summary>
	// Token: 0x02000030 RID: 48
	public class XmlNodeConverter : JsonConverter
	{
		/// <summary>
		/// Gets or sets the name of the root element to insert when deserializing to XML if the JSON structure has produces multiple root elements.
		/// </summary>
		/// <value>The name of the deserialize root element.</value>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000079C0 File Offset: 0x00005BC0
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000079C8 File Offset: 0x00005BC8
		public string DeserializeRootElementName { get; set; }

		/// <summary>
		/// Gets or sets a flag to indicate whether to write the Json.NET array attribute.
		/// This attribute helps preserve arrays when converting the written XML back to JSON.
		/// </summary>
		/// <value><c>true</c> if the array attibute is written to the XML; otherwise, <c>false</c>.</value>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000079D1 File Offset: 0x00005BD1
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000079D9 File Offset: 0x00005BD9
		public bool WriteArrayAttribute { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to write the root JSON object.
		/// </summary>
		/// <value><c>true</c> if the JSON root object is omitted; otherwise, <c>false</c>.</value>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000079E2 File Offset: 0x00005BE2
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000079EA File Offset: 0x00005BEA
		public bool OmitRootObject { get; set; }

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <param name="value">The value.</param>
		// Token: 0x060001E5 RID: 485 RVA: 0x000079F4 File Offset: 0x00005BF4
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			IXmlNode node = this.WrapXml(value);
			XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
			this.PushParentNamespaces(node, manager);
			if (!this.OmitRootObject)
			{
				writer.WriteStartObject();
			}
			this.SerializeNode(writer, node, manager, !this.OmitRootObject);
			if (!this.OmitRootObject)
			{
				writer.WriteEndObject();
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007A4A File Offset: 0x00005C4A
		private IXmlNode WrapXml(object value)
		{
			if (value is XObject)
			{
				return XContainerWrapper.WrapNode((XObject)value);
			}
			throw new ArgumentException("Value must be an XML object.", "value");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007A70 File Offset: 0x00005C70
		private void PushParentNamespaces(IXmlNode node, XmlNamespaceManager manager)
		{
			List<IXmlNode> list = null;
			IXmlNode xmlNode = node;
			while ((xmlNode = xmlNode.ParentNode) != null)
			{
				if (xmlNode.NodeType == 1)
				{
					if (list == null)
					{
						list = new List<IXmlNode>();
					}
					list.Add(xmlNode);
				}
			}
			if (list != null)
			{
				list.Reverse();
				foreach (IXmlNode xmlNode2 in list)
				{
					manager.PushScope();
					foreach (IXmlNode xmlNode3 in xmlNode2.Attributes)
					{
						if (xmlNode3.NamespaceUri == "http://www.w3.org/2000/xmlns/" && xmlNode3.LocalName != "xmlns")
						{
							manager.AddNamespace(xmlNode3.LocalName, xmlNode3.Value);
						}
					}
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007B64 File Offset: 0x00005D64
		private string ResolveFullName(IXmlNode node, XmlNamespaceManager manager)
		{
			string text = (node.NamespaceUri == null || (node.LocalName == "xmlns" && node.NamespaceUri == "http://www.w3.org/2000/xmlns/")) ? null : manager.LookupPrefix(node.NamespaceUri);
			if (!string.IsNullOrEmpty(text))
			{
				return text + ":" + node.LocalName;
			}
			return node.LocalName;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007BD0 File Offset: 0x00005DD0
		private string GetPropertyName(IXmlNode node, XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case 1:
				return this.ResolveFullName(node, manager);
			case 2:
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return "@" + this.ResolveFullName(node, manager);
			case 3:
				return "#text";
			case 4:
				return "#cdata-section";
			case 7:
				return "?" + this.ResolveFullName(node, manager);
			case 8:
				return "#comment";
			case 13:
				return "#whitespace";
			case 14:
				return "#significant-whitespace";
			case 17:
				return "?xml";
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when getting node name: " + node.NodeType);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007CE8 File Offset: 0x00005EE8
		private bool IsArray(IXmlNode node)
		{
			IXmlNode xmlNode;
			if (node.Attributes == null)
			{
				xmlNode = null;
			}
			else
			{
				xmlNode = Enumerable.SingleOrDefault<IXmlNode>(node.Attributes, (IXmlNode a) => a.LocalName == "Array" && a.NamespaceUri == "http://james.newtonking.com/projects/json");
			}
			IXmlNode xmlNode2 = xmlNode;
			return xmlNode2 != null && XmlConvert.ToBoolean(xmlNode2.Value);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007D3C File Offset: 0x00005F3C
		private void SerializeGroupedNodes(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			Dictionary<string, List<IXmlNode>> dictionary = new Dictionary<string, List<IXmlNode>>();
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				IXmlNode xmlNode = node.ChildNodes[i];
				string propertyName = this.GetPropertyName(xmlNode, manager);
				List<IXmlNode> list;
				if (!dictionary.TryGetValue(propertyName, ref list))
				{
					list = new List<IXmlNode>();
					dictionary.Add(propertyName, list);
				}
				list.Add(xmlNode);
			}
			foreach (KeyValuePair<string, List<IXmlNode>> keyValuePair in dictionary)
			{
				List<IXmlNode> value = keyValuePair.Value;
				if (value.Count == 1 && !this.IsArray(value[0]))
				{
					this.SerializeNode(writer, value[0], manager, writePropertyName);
				}
				else
				{
					string key = keyValuePair.Key;
					if (writePropertyName)
					{
						writer.WritePropertyName(key);
					}
					writer.WriteStartArray();
					for (int j = 0; j < value.Count; j++)
					{
						this.SerializeNode(writer, value[j], manager, false);
					}
					writer.WriteEndArray();
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007E8C File Offset: 0x0000608C
		private void SerializeNode(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case 1:
				if (this.IsArray(node))
				{
					if (Enumerable.All<IXmlNode>(node.ChildNodes, (IXmlNode n) => n.LocalName == node.LocalName) && node.ChildNodes.Count > 0)
					{
						this.SerializeGroupedNodes(writer, node, manager, false);
						return;
					}
				}
				manager.PushScope();
				foreach (IXmlNode xmlNode in node.Attributes)
				{
					if (xmlNode.NamespaceUri == "http://www.w3.org/2000/xmlns/")
					{
						string text = (xmlNode.LocalName != "xmlns") ? xmlNode.LocalName : string.Empty;
						string value = xmlNode.Value;
						manager.AddNamespace(text, value);
					}
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				if (!Enumerable.Any<IXmlNode>(this.ValueAttributes(node.Attributes)) && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == 3)
				{
					writer.WriteValue(node.ChildNodes[0].Value);
				}
				else if (node.ChildNodes.Count == 0 && CollectionUtils.IsNullOrEmpty<IXmlNode>(node.Attributes))
				{
					writer.WriteNull();
				}
				else
				{
					writer.WriteStartObject();
					for (int i = 0; i < node.Attributes.Count; i++)
					{
						this.SerializeNode(writer, node.Attributes[i], manager, true);
					}
					this.SerializeGroupedNodes(writer, node, manager, true);
					writer.WriteEndObject();
				}
				manager.PopScope();
				return;
			case 2:
			case 3:
			case 4:
			case 7:
			case 13:
			case 14:
				if (node.NamespaceUri == "http://www.w3.org/2000/xmlns/" && node.Value == "http://james.newtonking.com/projects/json")
				{
					return;
				}
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json" && node.LocalName == "Array")
				{
					return;
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				writer.WriteValue(node.Value);
				return;
			case 8:
				if (writePropertyName)
				{
					writer.WriteComment(node.Value);
					return;
				}
				return;
			case 9:
			case 11:
				this.SerializeGroupedNodes(writer, node, manager, writePropertyName);
				return;
			case 17:
			{
				IXmlDeclaration xmlDeclaration = (IXmlDeclaration)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName("@version");
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName("@encoding");
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName("@standalone");
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				return;
			}
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when serializing nodes: " + node.NodeType);
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		// Token: 0x060001ED RID: 493 RVA: 0x00008278 File Offset: 0x00006478
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
			IXmlDocument xmlDocument = null;
			IXmlNode xmlNode = null;
			if (typeof(XObject).IsAssignableFrom(objectType))
			{
				if (objectType != typeof(XDocument) && objectType != typeof(XElement))
				{
					throw new JsonSerializationException("XmlNodeConverter only supports deserializing XDocument or XElement.");
				}
				XDocument document = new XDocument();
				xmlDocument = new XDocumentWrapper(document);
				xmlNode = xmlDocument;
			}
			if (xmlDocument == null || xmlNode == null)
			{
				throw new JsonSerializationException("Unexpected type when converting XML: " + objectType);
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw new JsonSerializationException("XmlNodeConverter can only convert JSON that begins with an object.");
			}
			if (!string.IsNullOrEmpty(this.DeserializeRootElementName))
			{
				this.ReadElement(reader, xmlDocument, xmlNode, this.DeserializeRootElementName, manager);
			}
			else
			{
				reader.Read();
				this.DeserializeNode(reader, xmlDocument, manager, xmlNode);
			}
			if (objectType == typeof(XElement))
			{
				XElement xelement = (XElement)xmlDocument.DocumentElement.WrappedNode;
				xelement.Remove();
				return xelement;
			}
			return xmlDocument.WrappedNode;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008370 File Offset: 0x00006570
		private void DeserializeValue(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, string propertyName, IXmlNode currentNode)
		{
			if (propertyName != null)
			{
				if (propertyName == "#text")
				{
					currentNode.AppendChild(document.CreateTextNode(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#cdata-section")
				{
					currentNode.AppendChild(document.CreateCDataSection(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#whitespace")
				{
					currentNode.AppendChild(document.CreateWhitespace(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#significant-whitespace")
				{
					currentNode.AppendChild(document.CreateSignificantWhitespace(reader.Value.ToString()));
					return;
				}
			}
			if (!string.IsNullOrEmpty(propertyName) && propertyName.get_Chars(0) == '?')
			{
				this.CreateInstruction(reader, document, currentNode, propertyName);
				return;
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				this.ReadArrayElements(reader, document, propertyName, currentNode, manager);
				return;
			}
			this.ReadElement(reader, document, currentNode, propertyName, manager);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000846C File Offset: 0x0000666C
		private void ReadElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, XmlNamespaceManager manager)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new JsonSerializationException("XmlNodeConverter cannot convert JSON with an empty property name to XML.");
			}
			Dictionary<string, string> dictionary = this.ReadAttributeElements(reader, manager);
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			if (propertyName.StartsWith("@"))
			{
				string text = propertyName.Substring(1);
				string value = reader.Value.ToString();
				string prefix2 = MiscellaneousUtils.GetPrefix(text);
				IXmlNode attributeNode = (!string.IsNullOrEmpty(prefix2)) ? document.CreateAttribute(text, manager.LookupNamespace(prefix2), value) : document.CreateAttribute(text, value);
				((IXmlElement)currentNode).SetAttributeNode(attributeNode);
				return;
			}
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				string prefix3 = MiscellaneousUtils.GetPrefix(keyValuePair.Key);
				IXmlNode attributeNode2 = (!string.IsNullOrEmpty(prefix3)) ? document.CreateAttribute(keyValuePair.Key, manager.LookupNamespace(prefix3), keyValuePair.Value) : document.CreateAttribute(keyValuePair.Key, keyValuePair.Value);
				xmlElement.SetAttributeNode(attributeNode2);
			}
			if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Boolean || reader.TokenType == JsonToken.Date)
			{
				xmlElement.AppendChild(document.CreateTextNode(this.ConvertTokenToXmlValue(reader)));
				return;
			}
			if (reader.TokenType == JsonToken.Null)
			{
				return;
			}
			if (reader.TokenType != JsonToken.EndObject)
			{
				manager.PushScope();
				this.DeserializeNode(reader, document, manager, xmlElement);
				manager.PopScope();
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000861C File Offset: 0x0000681C
		private string ConvertTokenToXmlValue(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.String)
			{
				return reader.Value.ToString();
			}
			if (reader.TokenType == JsonToken.Integer)
			{
				return XmlConvert.ToString(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
			}
			if (reader.TokenType == JsonToken.Float)
			{
				return XmlConvert.ToString(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
			}
			if (reader.TokenType == JsonToken.Boolean)
			{
				return XmlConvert.ToString(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
			}
			if (reader.TokenType == JsonToken.Date)
			{
				DateTime dateTime = Convert.ToDateTime(reader.Value, CultureInfo.InvariantCulture);
				return XmlConvert.ToString(dateTime);
			}
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			throw JsonSerializationException.Create(reader, "Cannot get an XML string value from token type '{0}'.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000870C File Offset: 0x0000690C
		private void ReadArrayElements(JsonReader reader, IXmlDocument document, string propertyName, IXmlNode currentNode, XmlNamespaceManager manager)
		{
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			int num = 0;
			while (reader.Read() && reader.TokenType != JsonToken.EndArray)
			{
				this.DeserializeValue(reader, document, manager, propertyName, xmlElement);
				num++;
			}
			if (this.WriteArrayAttribute)
			{
				this.AddJsonArrayAttribute(xmlElement, document);
			}
			if (num == 1 && this.WriteArrayAttribute)
			{
				IXmlElement element = Enumerable.Single<IXmlElement>(Enumerable.OfType<IXmlElement>(xmlElement.ChildNodes), (IXmlElement n) => n.LocalName == propertyName);
				this.AddJsonArrayAttribute(element, document);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000087CC File Offset: 0x000069CC
		private void AddJsonArrayAttribute(IXmlElement element, IXmlDocument document)
		{
			element.SetAttributeNode(document.CreateAttribute("json:Array", "http://james.newtonking.com/projects/json", "true"));
			if (element is XElementWrapper && element.GetPrefixOfNamespace("http://james.newtonking.com/projects/json") == null)
			{
				element.SetAttributeNode(document.CreateAttribute("xmlns:json", "http://www.w3.org/2000/xmlns/", "http://james.newtonking.com/projects/json"));
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008824 File Offset: 0x00006A24
		private Dictionary<string, string> ReadAttributeElements(JsonReader reader, XmlNamespaceManager manager)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			bool flag = false;
			bool flag2 = false;
			if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null && reader.TokenType != JsonToken.Boolean && reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.Float && reader.TokenType != JsonToken.Date && reader.TokenType != JsonToken.StartConstructor)
			{
				while (!flag && !flag2 && reader.Read())
				{
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.PropertyName)
					{
						if (tokenType != JsonToken.EndObject)
						{
							throw new JsonSerializationException("Unexpected JsonToken: " + reader.TokenType);
						}
						flag2 = true;
					}
					else
					{
						string text = reader.Value.ToString();
						if (!string.IsNullOrEmpty(text))
						{
							char c = text.get_Chars(0);
							char c2 = c;
							if (c2 != '$')
							{
								if (c2 == '@')
								{
									text = text.Substring(1);
									reader.Read();
									string text2 = this.ConvertTokenToXmlValue(reader);
									dictionary.Add(text, text2);
									string text3;
									if (this.IsNamespaceAttribute(text, out text3))
									{
										manager.AddNamespace(text3, text2);
									}
								}
								else
								{
									flag = true;
								}
							}
							else
							{
								text = text.Substring(1);
								reader.Read();
								string text2 = reader.Value.ToString();
								string text4 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
								if (text4 == null)
								{
									int? num = default(int?);
									while (manager.LookupNamespace("json" + num) != null)
									{
										num = new int?(num.GetValueOrDefault() + 1);
									}
									text4 = "json" + num;
									dictionary.Add("xmlns:" + text4, "http://james.newtonking.com/projects/json");
									manager.AddNamespace(text4, "http://james.newtonking.com/projects/json");
								}
								dictionary.Add(text4 + ":" + text, text2);
							}
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008A0C File Offset: 0x00006C0C
		private void CreateInstruction(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName)
		{
			if (propertyName == "?xml")
			{
				string version = null;
				string encoding = null;
				string standalone = null;
				while (reader.Read() && reader.TokenType != JsonToken.EndObject)
				{
					string text;
					if ((text = reader.Value.ToString()) != null)
					{
						if (text == "@version")
						{
							reader.Read();
							version = reader.Value.ToString();
							continue;
						}
						if (text == "@encoding")
						{
							reader.Read();
							encoding = reader.Value.ToString();
							continue;
						}
						if (text == "@standalone")
						{
							reader.Read();
							standalone = reader.Value.ToString();
							continue;
						}
					}
					throw new JsonSerializationException("Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
				}
				IXmlNode newChild = document.CreateXmlDeclaration(version, encoding, standalone);
				currentNode.AppendChild(newChild);
				return;
			}
			IXmlNode newChild2 = document.CreateProcessingInstruction(propertyName.Substring(1), reader.Value.ToString());
			currentNode.AppendChild(newChild2);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008B14 File Offset: 0x00006D14
		private IXmlElement CreateElement(string elementName, IXmlDocument document, string elementPrefix, XmlNamespaceManager manager)
		{
			string text = string.IsNullOrEmpty(elementPrefix) ? manager.DefaultNamespace : manager.LookupNamespace(elementPrefix);
			return (!string.IsNullOrEmpty(text)) ? document.CreateElement(elementName, text) : document.CreateElement(elementName);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008B74 File Offset: 0x00006D74
		private void DeserializeNode(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, IXmlNode currentNode)
		{
			JsonToken tokenType;
			for (;;)
			{
				tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.StartConstructor:
				{
					string propertyName2 = reader.Value.ToString();
					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.EndConstructor)
						{
							break;
						}
						this.DeserializeValue(reader, document, manager, propertyName2, currentNode);
					}
					goto IL_162;
				}
				case JsonToken.PropertyName:
				{
					if (currentNode.NodeType == 9 && document.DocumentElement != null)
					{
						goto Block_3;
					}
					string propertyName = reader.Value.ToString();
					reader.Read();
					if (reader.TokenType != JsonToken.StartArray)
					{
						this.DeserializeValue(reader, document, manager, propertyName, currentNode);
						goto IL_162;
					}
					int num = 0;
					while (reader.Read() && reader.TokenType != JsonToken.EndArray)
					{
						this.DeserializeValue(reader, document, manager, propertyName, currentNode);
						num++;
					}
					if (num == 1 && this.WriteArrayAttribute)
					{
						IXmlElement element = Enumerable.Single<IXmlElement>(Enumerable.OfType<IXmlElement>(currentNode.ChildNodes), (IXmlElement n) => n.LocalName == propertyName);
						this.AddJsonArrayAttribute(element, document);
						goto IL_162;
					}
					goto IL_162;
				}
				case JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					goto IL_162;
				}
				break;
				IL_162:
				if (reader.TokenType != JsonToken.PropertyName && !reader.Read())
				{
					return;
				}
			}
			switch (tokenType)
			{
			case JsonToken.EndObject:
			case JsonToken.EndArray:
				return;
			default:
				throw new JsonSerializationException("Unexpected JsonToken when deserializing node: " + reader.TokenType);
			}
			Block_3:
			throw new JsonSerializationException("JSON root object has multiple properties. The root object must have a single property in order to create a valid XML document. Consider specifing a DeserializeRootElementName.");
		}

		/// <summary>
		/// Checks if the attributeName is a namespace attribute.
		/// </summary>
		/// <param name="attributeName">Attribute name to test.</param>
		/// <param name="prefix">The attribute name prefix if it has one, otherwise an empty string.</param>
		/// <returns>True if attribute name is for a namespace attribute, otherwise false.</returns>
		// Token: 0x060001F7 RID: 503 RVA: 0x00008CFC File Offset: 0x00006EFC
		private bool IsNamespaceAttribute(string attributeName, out string prefix)
		{
			if (attributeName.StartsWith("xmlns", 4))
			{
				if (attributeName.Length == 5)
				{
					prefix = string.Empty;
					return true;
				}
				if (attributeName.get_Chars(5) == ':')
				{
					prefix = attributeName.Substring(6, attributeName.Length - 6);
					return true;
				}
			}
			prefix = null;
			return false;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008D5D File Offset: 0x00006F5D
		private IEnumerable<IXmlNode> ValueAttributes(IEnumerable<IXmlNode> c)
		{
			return Enumerable.Where<IXmlNode>(c, (IXmlNode a) => a.NamespaceUri != "http://james.newtonking.com/projects/json");
		}

		/// <summary>
		/// Determines whether this instance can convert the specified value type.
		/// </summary>
		/// <param name="valueType">Type of the value.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can convert the specified value type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060001F9 RID: 505 RVA: 0x00008D82 File Offset: 0x00006F82
		public override bool CanConvert(Type valueType)
		{
			return typeof(XObject).IsAssignableFrom(valueType);
		}

		// Token: 0x04000096 RID: 150
		private const string TextName = "#text";

		// Token: 0x04000097 RID: 151
		private const string CommentName = "#comment";

		// Token: 0x04000098 RID: 152
		private const string CDataName = "#cdata-section";

		// Token: 0x04000099 RID: 153
		private const string WhitespaceName = "#whitespace";

		// Token: 0x0400009A RID: 154
		private const string SignificantWhitespaceName = "#significant-whitespace";

		// Token: 0x0400009B RID: 155
		private const string DeclarationName = "?xml";

		// Token: 0x0400009C RID: 156
		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";
	}
}

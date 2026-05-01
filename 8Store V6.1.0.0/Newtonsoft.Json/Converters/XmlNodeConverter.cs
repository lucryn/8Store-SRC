using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000102 RID: 258
	[NullableContext(1)]
	[Nullable(0)]
	public class XmlNodeConverter : JsonConverter
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00031410 File Offset: 0x0002F610
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00031418 File Offset: 0x0002F618
		[Nullable(2)]
		public string DeserializeRootElementName { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00031421 File Offset: 0x0002F621
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00031429 File Offset: 0x0002F629
		public bool WriteArrayAttribute { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00031432 File Offset: 0x0002F632
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0003143A File Offset: 0x0002F63A
		public bool OmitRootObject { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00031443 File Offset: 0x0002F643
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0003144B File Offset: 0x0002F64B
		public bool EncodeSpecialCharacters { get; set; }

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00031454 File Offset: 0x0002F654
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
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

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000314B4 File Offset: 0x0002F6B4
		private IXmlNode WrapXml(object value)
		{
			XObject xobject = value as XObject;
			if (xobject != null)
			{
				return XContainerWrapper.WrapNode(xobject);
			}
			throw new ArgumentException("Value must be an XML object.", "value");
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000314E4 File Offset: 0x0002F6E4
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

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000315DC File Offset: 0x0002F7DC
		private string ResolveFullName(IXmlNode node, XmlNamespaceManager manager)
		{
			string text = (node.NamespaceUri == null || (node.LocalName == "xmlns" && node.NamespaceUri == "http://www.w3.org/2000/xmlns/")) ? null : manager.LookupPrefix(node.NamespaceUri);
			if (!StringUtils.IsNullOrEmpty(text))
			{
				return text + ":" + XmlConvert.DecodeName(node.LocalName);
			}
			return XmlConvert.DecodeName(node.LocalName);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00031650 File Offset: 0x0002F850
		private string GetPropertyName(IXmlNode node, XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case 1:
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
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
			case 10:
				return "!" + this.ResolveFullName(node, manager);
			case 13:
				return "#whitespace";
			case 14:
				return "#significant-whitespace";
			case 17:
				return "?xml";
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when getting node name: " + node.NodeType.ToString());
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00031784 File Offset: 0x0002F984
		private bool IsArray(IXmlNode node)
		{
			foreach (IXmlNode xmlNode in node.Attributes)
			{
				if (xmlNode.LocalName == "Array" && xmlNode.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return XmlConvert.ToBoolean(xmlNode.Value);
				}
			}
			return false;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00031808 File Offset: 0x0002FA08
		private void SerializeGroupedNodes(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			int count = node.ChildNodes.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					string propertyName = this.GetPropertyName(node.ChildNodes[0], manager);
					this.WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, propertyName);
					return;
				}
				Dictionary<string, object> dictionary = null;
				string text = null;
				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					IXmlNode xmlNode = node.ChildNodes[i];
					string propertyName2 = this.GetPropertyName(xmlNode, manager);
					object obj;
					if (dictionary == null)
					{
						if (text == null)
						{
							text = propertyName2;
						}
						else if (!(propertyName2 == text))
						{
							dictionary = new Dictionary<string, object>();
							if (i > 1)
							{
								List<IXmlNode> list = new List<IXmlNode>(i);
								for (int j = 0; j < i; j++)
								{
									list.Add(node.ChildNodes[j]);
								}
								dictionary.Add(text, list);
							}
							else
							{
								dictionary.Add(text, node.ChildNodes[0]);
							}
							dictionary.Add(propertyName2, xmlNode);
						}
					}
					else if (!dictionary.TryGetValue(propertyName2, ref obj))
					{
						dictionary.Add(propertyName2, xmlNode);
					}
					else
					{
						List<IXmlNode> list2 = obj as List<IXmlNode>;
						if (list2 == null)
						{
							List<IXmlNode> list3 = new List<IXmlNode>();
							list3.Add((IXmlNode)obj);
							list2 = list3;
							dictionary[propertyName2] = list2;
						}
						list2.Add(xmlNode);
					}
				}
				if (dictionary == null)
				{
					this.WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, text);
					return;
				}
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					List<IXmlNode> list4 = keyValuePair.Value as List<IXmlNode>;
					if (list4 != null)
					{
						this.WriteGroupedNodes(writer, manager, writePropertyName, list4, keyValuePair.Key);
					}
					else
					{
						this.WriteGroupedNodes(writer, manager, writePropertyName, (IXmlNode)keyValuePair.Value, keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000319F0 File Offset: 0x0002FBF0
		private void WriteGroupedNodes(JsonWriter writer, XmlNamespaceManager manager, bool writePropertyName, List<IXmlNode> groupedNodes, string elementNames)
		{
			if (groupedNodes.Count == 1 && !this.IsArray(groupedNodes[0]))
			{
				this.SerializeNode(writer, groupedNodes[0], manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			for (int i = 0; i < groupedNodes.Count; i++)
			{
				this.SerializeNode(writer, groupedNodes[i], manager, false);
			}
			writer.WriteEndArray();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00031A66 File Offset: 0x0002FC66
		private void WriteGroupedNodes(JsonWriter writer, XmlNamespaceManager manager, bool writePropertyName, IXmlNode node, string elementNames)
		{
			if (!this.IsArray(node))
			{
				this.SerializeNode(writer, node, manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			this.SerializeNode(writer, node, manager, false);
			writer.WriteEndArray();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00031AA0 File Offset: 0x0002FCA0
		private void SerializeNode(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case 1:
				if (this.IsArray(node) && XmlNodeConverter.AllSameName(node) && node.ChildNodes.Count > 0)
				{
					this.SerializeGroupedNodes(writer, node, manager, false);
					return;
				}
				manager.PushScope();
				foreach (IXmlNode xmlNode in node.Attributes)
				{
					if (xmlNode.NamespaceUri == "http://www.w3.org/2000/xmlns/")
					{
						string text = (xmlNode.LocalName != "xmlns") ? XmlConvert.DecodeName(xmlNode.LocalName) : string.Empty;
						string value = xmlNode.Value;
						if (value == null)
						{
							throw new JsonSerializationException("Namespace attribute must have a value.");
						}
						manager.AddNamespace(text, value);
					}
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				if (!this.ValueAttributes(node.Attributes) && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == 3)
				{
					writer.WriteValue(node.ChildNodes[0].Value);
				}
				else if (node.ChildNodes.Count == 0 && node.Attributes.Count == 0)
				{
					if (((IXmlElement)node).IsEmpty)
					{
						writer.WriteNull();
					}
					else
					{
						writer.WriteValue(string.Empty);
					}
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
			case 10:
			{
				IXmlDocumentType xmlDocumentType = (IXmlDocumentType)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!StringUtils.IsNullOrEmpty(xmlDocumentType.Name))
				{
					writer.WritePropertyName("@name");
					writer.WriteValue(xmlDocumentType.Name);
				}
				if (!StringUtils.IsNullOrEmpty(xmlDocumentType.Public))
				{
					writer.WritePropertyName("@public");
					writer.WriteValue(xmlDocumentType.Public);
				}
				if (!StringUtils.IsNullOrEmpty(xmlDocumentType.System))
				{
					writer.WritePropertyName("@system");
					writer.WriteValue(xmlDocumentType.System);
				}
				if (!StringUtils.IsNullOrEmpty(xmlDocumentType.InternalSubset))
				{
					writer.WritePropertyName("@internalSubset");
					writer.WriteValue(xmlDocumentType.InternalSubset);
				}
				writer.WriteEndObject();
				return;
			}
			case 17:
			{
				IXmlDeclaration xmlDeclaration = (IXmlDeclaration)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!StringUtils.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName("@version");
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!StringUtils.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName("@encoding");
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!StringUtils.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName("@standalone");
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				return;
			}
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when serializing nodes: " + node.NodeType.ToString());
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00031EA8 File Offset: 0x000300A8
		private static bool AllSameName(IXmlNode node)
		{
			using (List<IXmlNode>.Enumerator enumerator = node.ChildNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.LocalName != node.LocalName)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00031F0C File Offset: 0x0003010C
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType != JsonToken.StartObject)
			{
				if (tokenType == JsonToken.Null)
				{
					return null;
				}
				throw JsonSerializationException.Create(reader, "XmlNodeConverter can only convert JSON that begins with an object.");
			}
			else
			{
				XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
				IXmlDocument xmlDocument = null;
				IXmlNode xmlNode = null;
				if (typeof(XObject).IsAssignableFrom(objectType))
				{
					if (objectType != typeof(XContainer) && objectType != typeof(XDocument) && objectType != typeof(XElement) && objectType != typeof(XNode) && objectType != typeof(XObject))
					{
						throw JsonSerializationException.Create(reader, "XmlNodeConverter only supports deserializing XDocument, XElement, XContainer, XNode or XObject.");
					}
					xmlDocument = new XDocumentWrapper(new XDocument());
					xmlNode = xmlDocument;
				}
				if (xmlDocument == null || xmlNode == null)
				{
					throw JsonSerializationException.Create(reader, "Unexpected type when converting XML: " + ((objectType != null) ? objectType.ToString() : null));
				}
				if (!StringUtils.IsNullOrEmpty(this.DeserializeRootElementName))
				{
					this.ReadElement(reader, xmlDocument, xmlNode, this.DeserializeRootElementName, manager);
				}
				else
				{
					reader.ReadAndAssert();
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
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00032030 File Offset: 0x00030230
		private void DeserializeValue(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, string propertyName, IXmlNode currentNode)
		{
			if (!this.EncodeSpecialCharacters)
			{
				if (propertyName == "#text")
				{
					currentNode.AppendChild(document.CreateTextNode(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (propertyName == "#cdata-section")
				{
					currentNode.AppendChild(document.CreateCDataSection(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (propertyName == "#whitespace")
				{
					currentNode.AppendChild(document.CreateWhitespace(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (propertyName == "#significant-whitespace")
				{
					currentNode.AppendChild(document.CreateSignificantWhitespace(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (!StringUtils.IsNullOrEmpty(propertyName) && propertyName.get_Chars(0) == '?')
				{
					this.CreateInstruction(reader, document, currentNode, propertyName);
					return;
				}
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				this.ReadArrayElements(reader, document, propertyName, currentNode, manager);
				return;
			}
			this.ReadElement(reader, document, currentNode, propertyName, manager);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0003211C File Offset: 0x0003031C
		private void ReadElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, XmlNamespaceManager manager)
		{
			if (StringUtils.IsNullOrEmpty(propertyName))
			{
				throw JsonSerializationException.Create(reader, "XmlNodeConverter cannot convert JSON with an empty property name to XML.");
			}
			Dictionary<string, string> attributeNameValues = null;
			string elementPrefix = null;
			if (!this.EncodeSpecialCharacters)
			{
				attributeNameValues = (this.ShouldReadInto(reader) ? this.ReadAttributeElements(reader, manager) : null);
				elementPrefix = MiscellaneousUtils.GetPrefix(propertyName);
				if (propertyName.StartsWith('@'))
				{
					string text = propertyName.Substring(1);
					string prefix = MiscellaneousUtils.GetPrefix(text);
					XmlNodeConverter.AddAttribute(reader, document, currentNode, propertyName, text, manager, prefix);
					return;
				}
				if (propertyName.StartsWith('$'))
				{
					if (propertyName == "$values")
					{
						propertyName = propertyName.Substring(1);
						elementPrefix = manager.LookupPrefix("http://james.newtonking.com/projects/json");
						this.CreateElement(reader, document, currentNode, propertyName, manager, elementPrefix, attributeNameValues);
						return;
					}
					if (propertyName == "$id" || propertyName == "$ref" || propertyName == "$type" || propertyName == "$value")
					{
						string attributeName = propertyName.Substring(1);
						string attributePrefix = manager.LookupPrefix("http://james.newtonking.com/projects/json");
						XmlNodeConverter.AddAttribute(reader, document, currentNode, propertyName, attributeName, manager, attributePrefix);
						return;
					}
				}
			}
			else if (this.ShouldReadInto(reader))
			{
				reader.ReadAndAssert();
			}
			this.CreateElement(reader, document, currentNode, propertyName, manager, elementPrefix, attributeNameValues);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003225C File Offset: 0x0003045C
		private void CreateElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string elementName, XmlNamespaceManager manager, [Nullable(2)] string elementPrefix, [Nullable(new byte[]
		{
			2,
			1,
			2
		})] Dictionary<string, string> attributeNameValues)
		{
			IXmlElement xmlElement = this.CreateElement(elementName, document, elementPrefix, manager);
			currentNode.AppendChild(xmlElement);
			if (attributeNameValues != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in attributeNameValues)
				{
					string text = XmlConvert.EncodeName(keyValuePair.Key);
					string prefix = MiscellaneousUtils.GetPrefix(keyValuePair.Key);
					IXmlNode attributeNode = (!StringUtils.IsNullOrEmpty(prefix)) ? document.CreateAttribute(text, manager.LookupNamespace(prefix) ?? string.Empty, keyValuePair.Value) : document.CreateAttribute(text, keyValuePair.Value);
					xmlElement.SetAttributeNode(attributeNode);
				}
			}
			switch (reader.TokenType)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
			{
				string text2 = XmlNodeConverter.ConvertTokenToXmlValue(reader);
				if (text2 != null)
				{
					xmlElement.AppendChild(document.CreateTextNode(text2));
					return;
				}
				return;
			}
			case JsonToken.Null:
				return;
			case JsonToken.EndObject:
				manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
				return;
			}
			manager.PushScope();
			this.DeserializeNode(reader, document, manager, xmlElement);
			manager.PopScope();
			manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000323BC File Offset: 0x000305BC
		private static void AddAttribute(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, string attributeName, XmlNamespaceManager manager, [Nullable(2)] string attributePrefix)
		{
			if (currentNode.NodeType == 9)
			{
				throw JsonSerializationException.Create(reader, "JSON root object has property '{0}' that will be converted to an attribute. A root object cannot have any attribute properties. Consider specifying a DeserializeRootElementName.".FormatWith(CultureInfo.InvariantCulture, propertyName));
			}
			string text = XmlConvert.EncodeName(attributeName);
			string value = XmlNodeConverter.ConvertTokenToXmlValue(reader);
			IXmlNode attributeNode = (!StringUtils.IsNullOrEmpty(attributePrefix)) ? document.CreateAttribute(text, manager.LookupNamespace(attributePrefix), value) : document.CreateAttribute(text, value);
			((IXmlElement)currentNode).SetAttributeNode(attributeNode);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0003242C File Offset: 0x0003062C
		[return: Nullable(2)]
		private static string ConvertTokenToXmlValue(JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonToken.Integer:
				return XmlConvert.ToString(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.Float:
			{
				object value = reader.Value;
				if (value is decimal)
				{
					decimal num = (decimal)value;
					return XmlConvert.ToString(num);
				}
				value = reader.Value;
				if (value is float)
				{
					float num2 = (float)value;
					return XmlConvert.ToString(num2);
				}
				return XmlConvert.ToString(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
			}
			case JsonToken.String:
			{
				object value2 = reader.Value;
				if (value2 == null)
				{
					return null;
				}
				return value2.ToString();
			}
			case JsonToken.Boolean:
				return XmlConvert.ToString(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.Null:
				return null;
			case JsonToken.Date:
			{
				object value = reader.Value;
				if (value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					return XmlConvert.ToString(dateTimeOffset);
				}
				DateTime dateTime = Convert.ToDateTime(reader.Value, CultureInfo.InvariantCulture);
				return dateTime.ToString(DateTimeUtils.ToDateTimeFormat(dateTime.Kind), CultureInfo.InvariantCulture);
			}
			case JsonToken.Bytes:
				return Convert.ToBase64String((byte[])reader.Value);
			}
			throw JsonSerializationException.Create(reader, "Cannot get an XML string value from token type '{0}'.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00032584 File Offset: 0x00030784
		private void ReadArrayElements(JsonReader reader, IXmlDocument document, string propertyName, IXmlNode currentNode, XmlNamespaceManager manager)
		{
			string text = this.EncodeSpecialCharacters ? XmlConvert.EncodeLocalName(propertyName) : XmlConvert.EncodeName(propertyName);
			string prefix = MiscellaneousUtils.GetPrefix(text);
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
				foreach (IXmlNode xmlNode in xmlElement.ChildNodes)
				{
					IXmlElement xmlElement2 = xmlNode as IXmlElement;
					if (xmlElement2 != null && xmlElement2.LocalName == text)
					{
						this.AddJsonArrayAttribute(xmlElement2, document);
						break;
					}
				}
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003266C File Offset: 0x0003086C
		private void AddJsonArrayAttribute(IXmlElement element, IXmlDocument document)
		{
			if (element.Attributes.Find((IXmlNode x) => x.LocalName == "Array" && x.NamespaceUri == "http://james.newtonking.com/projects/json") != null)
			{
				return;
			}
			element.SetAttributeNode(document.CreateAttribute("json:Array", "http://james.newtonking.com/projects/json", "true"));
			if (element is XElementWrapper && element.GetPrefixOfNamespace("http://james.newtonking.com/projects/json") == null)
			{
				element.SetAttributeNode(document.CreateAttribute("xmlns:json", "http://www.w3.org/2000/xmlns/", "http://james.newtonking.com/projects/json"));
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000326F8 File Offset: 0x000308F8
		private bool ShouldReadInto(JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonToken.StartConstructor:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return false;
			}
			return true;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00032758 File Offset: 0x00030958
		[return: Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		private Dictionary<string, string> ReadAttributeElements(JsonReader reader, XmlNamespaceManager manager)
		{
			Dictionary<string, string> dictionary = null;
			bool flag = false;
			while (!flag && reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment && tokenType != JsonToken.EndObject)
					{
						throw JsonSerializationException.Create(reader, "Unexpected JsonToken: " + reader.TokenType.ToString());
					}
					flag = true;
				}
				else
				{
					string text = reader.Value.ToString();
					if (!StringUtils.IsNullOrEmpty(text))
					{
						char c = text.get_Chars(0);
						if (c != '$')
						{
							if (c == '@')
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								text = text.Substring(1);
								reader.ReadAndAssert();
								string text2 = XmlNodeConverter.ConvertTokenToXmlValue(reader);
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
						else if (text == "$values" || text == "$id" || text == "$ref" || text == "$type" || text == "$value")
						{
							string text4 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
							if (text4 == null)
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								int? num = default(int?);
								int? num2;
								for (;;)
								{
									string text5 = "json";
									num2 = num;
									if (manager.LookupNamespace(text5 + num2.ToString()) == null)
									{
										break;
									}
									num = new int?(num.GetValueOrDefault() + 1);
								}
								string text6 = "json";
								num2 = num;
								text4 = text6 + num2.ToString();
								dictionary.Add("xmlns:" + text4, "http://james.newtonking.com/projects/json");
								manager.AddNamespace(text4, "http://james.newtonking.com/projects/json");
							}
							if (text == "$values")
							{
								flag = true;
							}
							else
							{
								text = text.Substring(1);
								reader.ReadAndAssert();
								if (!JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
								{
									throw JsonSerializationException.Create(reader, "Unexpected JsonToken: " + reader.TokenType.ToString());
								}
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								object value = reader.Value;
								string text2 = (value != null) ? value.ToString() : null;
								dictionary.Add(text4 + ":" + text, text2);
							}
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000329B0 File Offset: 0x00030BB0
		private void CreateInstruction(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName)
		{
			if (!(propertyName == "?xml"))
			{
				IXmlNode newChild = document.CreateProcessingInstruction(propertyName.Substring(1), XmlNodeConverter.ConvertTokenToXmlValue(reader));
				currentNode.AppendChild(newChild);
				return;
			}
			string text = null;
			string encoding = null;
			string standalone = null;
			while (reader.Read() && reader.TokenType != JsonToken.EndObject)
			{
				object value = reader.Value;
				string text2 = (value != null) ? value.ToString() : null;
				if (!(text2 == "@version"))
				{
					if (!(text2 == "@encoding"))
					{
						if (!(text2 == "@standalone"))
						{
							string text3 = "Unexpected property name encountered while deserializing XmlDeclaration: ";
							object value2 = reader.Value;
							throw JsonSerializationException.Create(reader, text3 + ((value2 != null) ? value2.ToString() : null));
						}
						reader.ReadAndAssert();
						standalone = XmlNodeConverter.ConvertTokenToXmlValue(reader);
					}
					else
					{
						reader.ReadAndAssert();
						encoding = XmlNodeConverter.ConvertTokenToXmlValue(reader);
					}
				}
				else
				{
					reader.ReadAndAssert();
					text = XmlNodeConverter.ConvertTokenToXmlValue(reader);
				}
			}
			if (text == null)
			{
				throw JsonSerializationException.Create(reader, "Version not specified for XML declaration.");
			}
			IXmlNode newChild2 = document.CreateXmlDeclaration(text, encoding, standalone);
			currentNode.AppendChild(newChild2);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00032AC0 File Offset: 0x00030CC0
		private IXmlElement CreateElement(string elementName, IXmlDocument document, [Nullable(2)] string elementPrefix, XmlNamespaceManager manager)
		{
			string text = this.EncodeSpecialCharacters ? XmlConvert.EncodeLocalName(elementName) : XmlConvert.EncodeName(elementName);
			string text2 = StringUtils.IsNullOrEmpty(elementPrefix) ? manager.DefaultNamespace : manager.LookupNamespace(elementPrefix);
			if (StringUtils.IsNullOrEmpty(text2))
			{
				return document.CreateElement(text);
			}
			return document.CreateElement(text, text2);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00032B18 File Offset: 0x00030D18
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
					string propertyName = reader.Value.ToString();
					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.EndConstructor)
						{
							break;
						}
						this.DeserializeValue(reader, document, manager, propertyName, currentNode);
					}
					goto IL_1C3;
				}
				case JsonToken.PropertyName:
				{
					if (currentNode.NodeType == 9 && document.DocumentElement != null)
					{
						goto Block_3;
					}
					string text = reader.Value.ToString();
					reader.ReadAndAssert();
					if (reader.TokenType == JsonToken.StartArray)
					{
						int num = 0;
						while (reader.Read() && reader.TokenType != JsonToken.EndArray)
						{
							this.DeserializeValue(reader, document, manager, text, currentNode);
							num++;
						}
						if (num != 1 || !this.WriteArrayAttribute)
						{
							goto IL_1C3;
						}
						string text2;
						string text3;
						MiscellaneousUtils.GetQualifiedNameParts(this.EncodeSpecialCharacters ? XmlConvert.EncodeLocalName(text) : XmlConvert.EncodeName(text), out text2, out text3);
						string text4 = StringUtils.IsNullOrEmpty(text2) ? manager.DefaultNamespace : manager.LookupNamespace(text2);
						using (List<IXmlNode>.Enumerator enumerator = currentNode.ChildNodes.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IXmlNode xmlNode = enumerator.Current;
								IXmlElement xmlElement = xmlNode as IXmlElement;
								if (xmlElement != null && xmlElement.LocalName == text3 && xmlElement.NamespaceUri == text4)
								{
									this.AddJsonArrayAttribute(xmlElement, document);
									break;
								}
							}
							goto IL_1C3;
						}
					}
					this.DeserializeValue(reader, document, manager, text, currentNode);
					goto IL_1C3;
				}
				case JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					goto IL_1C3;
				}
				break;
				IL_1C3:
				if (!reader.Read())
				{
					return;
				}
			}
			if (tokenType - JsonToken.EndObject > 1)
			{
				throw JsonSerializationException.Create(reader, "Unexpected JsonToken when deserializing node: " + reader.TokenType.ToString());
			}
			return;
			Block_3:
			throw JsonSerializationException.Create(reader, "JSON root object has multiple properties. The root object must have a single property in order to create a valid XML document. Consider specifying a DeserializeRootElementName.");
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00032D04 File Offset: 0x00030F04
		private bool IsNamespaceAttribute(string attributeName, [Nullable(2)] [NotNullWhen(true)] out string prefix)
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

		// Token: 0x06000CDA RID: 3290 RVA: 0x00032D54 File Offset: 0x00030F54
		private bool ValueAttributes(List<IXmlNode> c)
		{
			foreach (IXmlNode xmlNode in c)
			{
				if (!(xmlNode.NamespaceUri == "http://james.newtonking.com/projects/json") && (!(xmlNode.NamespaceUri == "http://www.w3.org/2000/xmlns/") || !(xmlNode.Value == "http://james.newtonking.com/projects/json")))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00032DD8 File Offset: 0x00030FD8
		public override bool CanConvert(Type valueType)
		{
			return valueType.AssignableToTypeName("System.Xml.Linq.XObject") && this.IsXObject(valueType);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00032DF0 File Offset: 0x00030FF0
		[MethodImpl(8)]
		private bool IsXObject(Type valueType)
		{
			return typeof(XObject).IsAssignableFrom(valueType);
		}

		// Token: 0x04000439 RID: 1081
		internal static readonly List<IXmlNode> EmptyChildNodes = new List<IXmlNode>();

		// Token: 0x0400043A RID: 1082
		private const string TextName = "#text";

		// Token: 0x0400043B RID: 1083
		private const string CommentName = "#comment";

		// Token: 0x0400043C RID: 1084
		private const string CDataName = "#cdata-section";

		// Token: 0x0400043D RID: 1085
		private const string WhitespaceName = "#whitespace";

		// Token: 0x0400043E RID: 1086
		private const string SignificantWhitespaceName = "#significant-whitespace";

		// Token: 0x0400043F RID: 1087
		private const string DeclarationName = "?xml";

		// Token: 0x04000440 RID: 1088
		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";
	}
}

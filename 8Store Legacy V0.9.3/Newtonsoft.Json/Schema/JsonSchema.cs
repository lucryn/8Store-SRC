using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// An in-memory representation of a JSON Schema.
	/// </summary>
	// Token: 0x02000070 RID: 112
	public class JsonSchema
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00015F60 File Offset: 0x00014160
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00015F68 File Offset: 0x00014168
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00015F71 File Offset: 0x00014171
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00015F79 File Offset: 0x00014179
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets whether the object is required.
		/// </summary>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00015F82 File Offset: 0x00014182
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00015F8A File Offset: 0x0001418A
		public bool? Required { get; set; }

		/// <summary>
		/// Gets or sets whether the object is read only.
		/// </summary>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00015F93 File Offset: 0x00014193
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x00015F9B File Offset: 0x0001419B
		public bool? ReadOnly { get; set; }

		/// <summary>
		/// Gets or sets whether the object is visible to users.
		/// </summary>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00015FA4 File Offset: 0x000141A4
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00015FAC File Offset: 0x000141AC
		public bool? Hidden { get; set; }

		/// <summary>
		/// Gets or sets whether the object is transient.
		/// </summary>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00015FB5 File Offset: 0x000141B5
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00015FBD File Offset: 0x000141BD
		public bool? Transient { get; set; }

		/// <summary>
		/// Gets or sets the description of the object.
		/// </summary>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00015FC6 File Offset: 0x000141C6
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00015FCE File Offset: 0x000141CE
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the types of values allowed by the object.
		/// </summary>
		/// <value>The type.</value>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00015FD7 File Offset: 0x000141D7
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00015FDF File Offset: 0x000141DF
		public JsonSchemaType? Type { get; set; }

		/// <summary>
		/// Gets or sets the pattern.
		/// </summary>
		/// <value>The pattern.</value>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00015FE8 File Offset: 0x000141E8
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00015FF0 File Offset: 0x000141F0
		public string Pattern { get; set; }

		/// <summary>
		/// Gets or sets the minimum length.
		/// </summary>
		/// <value>The minimum length.</value>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00015FF9 File Offset: 0x000141F9
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00016001 File Offset: 0x00014201
		public int? MinimumLength { get; set; }

		/// <summary>
		/// Gets or sets the maximum length.
		/// </summary>
		/// <value>The maximum length.</value>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001600A File Offset: 0x0001420A
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00016012 File Offset: 0x00014212
		public int? MaximumLength { get; set; }

		/// <summary>
		/// Gets or sets a number that the value should be divisble by.
		/// </summary>
		/// <value>A number that the value should be divisble by.</value>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001601B File Offset: 0x0001421B
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00016023 File Offset: 0x00014223
		public double? DivisibleBy { get; set; }

		/// <summary>
		/// Gets or sets the minimum.
		/// </summary>
		/// <value>The minimum.</value>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001602C File Offset: 0x0001422C
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00016034 File Offset: 0x00014234
		public double? Minimum { get; set; }

		/// <summary>
		/// Gets or sets the maximum.
		/// </summary>
		/// <value>The maximum.</value>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001603D File Offset: 0x0001423D
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00016045 File Offset: 0x00014245
		public double? Maximum { get; set; }

		/// <summary>
		/// Gets or sets a flag indicating whether the value can not equal the number defined by the "minimum" attribute.
		/// </summary>
		/// <value>A flag indicating whether the value can not equal the number defined by the "minimum" attribute.</value>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001604E File Offset: 0x0001424E
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00016056 File Offset: 0x00014256
		public bool? ExclusiveMinimum { get; set; }

		/// <summary>
		/// Gets or sets a flag indicating whether the value can not equal the number defined by the "maximum" attribute.
		/// </summary>
		/// <value>A flag indicating whether the value can not equal the number defined by the "maximum" attribute.</value>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001605F File Offset: 0x0001425F
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00016067 File Offset: 0x00014267
		public bool? ExclusiveMaximum { get; set; }

		/// <summary>
		/// Gets or sets the minimum number of items.
		/// </summary>
		/// <value>The minimum number of items.</value>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00016070 File Offset: 0x00014270
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00016078 File Offset: 0x00014278
		public int? MinimumItems { get; set; }

		/// <summary>
		/// Gets or sets the maximum number of items.
		/// </summary>
		/// <value>The maximum number of items.</value>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00016081 File Offset: 0x00014281
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00016089 File Offset: 0x00014289
		public int? MaximumItems { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of items.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of items.</value>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00016092 File Offset: 0x00014292
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0001609A File Offset: 0x0001429A
		public IList<JsonSchema> Items { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether items in an array are validated using the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> instance at their array position from <see cref="P:Newtonsoft.Json.Schema.JsonSchema.Items" />.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if items are validated using their array position; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x000160A3 File Offset: 0x000142A3
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x000160AB File Offset: 0x000142AB
		public bool PositionalItemsValidation { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of additional items.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of additional items.</value>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x000160B4 File Offset: 0x000142B4
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x000160BC File Offset: 0x000142BC
		public JsonSchema AdditionalItems { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether additional items are allowed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if additional items are allowed; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x000160C5 File Offset: 0x000142C5
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x000160CD File Offset: 0x000142CD
		public bool AllowAdditionalItems { get; set; }

		/// <summary>
		/// Gets or sets whether the array items must be unique.
		/// </summary>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x000160D6 File Offset: 0x000142D6
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x000160DE File Offset: 0x000142DE
		public bool UniqueItems { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of properties.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of properties.</value>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000160E7 File Offset: 0x000142E7
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x000160EF File Offset: 0x000142EF
		public IDictionary<string, JsonSchema> Properties { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of additional properties.
		/// </summary>
		/// <value>The <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of additional properties.</value>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000160F8 File Offset: 0x000142F8
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00016100 File Offset: 0x00014300
		public JsonSchema AdditionalProperties { get; set; }

		/// <summary>
		/// Gets or sets the pattern properties.
		/// </summary>
		/// <value>The pattern properties.</value>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00016109 File Offset: 0x00014309
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00016111 File Offset: 0x00014311
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether additional properties are allowed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if additional properties are allowed; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001611A File Offset: 0x0001431A
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00016122 File Offset: 0x00014322
		public bool AllowAdditionalProperties { get; set; }

		/// <summary>
		/// Gets or sets the required property if this property is present.
		/// </summary>
		/// <value>The required property if this property is present.</value>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001612B File Offset: 0x0001432B
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00016133 File Offset: 0x00014333
		public string Requires { get; set; }

		/// <summary>
		/// Gets or sets the a collection of valid enum values allowed.
		/// </summary>
		/// <value>A collection of valid enum values allowed.</value>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001613C File Offset: 0x0001433C
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00016144 File Offset: 0x00014344
		public IList<JToken> Enum { get; set; }

		/// <summary>
		/// Gets or sets disallowed types.
		/// </summary>
		/// <value>The disallow types.</value>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001614D File Offset: 0x0001434D
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00016155 File Offset: 0x00014355
		public JsonSchemaType? Disallow { get; set; }

		/// <summary>
		/// Gets or sets the default value.
		/// </summary>
		/// <value>The default value.</value>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001615E File Offset: 0x0001435E
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00016166 File Offset: 0x00014366
		public JToken Default { get; set; }

		/// <summary>
		/// Gets or sets the collection of <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> that this schema extends.
		/// </summary>
		/// <value>The collection of <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> that this schema extends.</value>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001616F File Offset: 0x0001436F
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00016177 File Offset: 0x00014377
		public IList<JsonSchema> Extends { get; set; }

		/// <summary>
		/// Gets or sets the format.
		/// </summary>
		/// <value>The format.</value>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00016180 File Offset: 0x00014380
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00016188 File Offset: 0x00014388
		public string Format { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00016191 File Offset: 0x00014391
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00016199 File Offset: 0x00014399
		internal string Location { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000161A2 File Offset: 0x000143A2
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000161AA File Offset: 0x000143AA
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x000161B2 File Offset: 0x000143B2
		internal string DeferredReference { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000161BB File Offset: 0x000143BB
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x000161C3 File Offset: 0x000143C3
		internal bool ReferencesResolved { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> class.
		/// </summary>
		// Token: 0x06000621 RID: 1569 RVA: 0x000161CC File Offset: 0x000143CC
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		/// <summary>
		/// Reads a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from the specified <see cref="T:Newtonsoft.Json.JsonReader" />.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> containing the JSON Schema to read.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> object representing the JSON Schema.</returns>
		// Token: 0x06000622 RID: 1570 RVA: 0x00016205 File Offset: 0x00014405
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		/// <summary>
		/// Reads a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from the specified <see cref="T:Newtonsoft.Json.JsonReader" />.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> containing the JSON Schema to read.</param>
		/// <param name="resolver">The <see cref="T:Newtonsoft.Json.Schema.JsonSchemaResolver" /> to use when resolving schema references.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> object representing the JSON Schema.</returns>
		// Token: 0x06000623 RID: 1571 RVA: 0x00016214 File Offset: 0x00014414
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			JsonSchemaBuilder jsonSchemaBuilder = new JsonSchemaBuilder(resolver);
			return jsonSchemaBuilder.Read(reader);
		}

		/// <summary>
		/// Load a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from a string that contains schema JSON.
		/// </summary>
		/// <param name="json">A <see cref="T:System.String" /> that contains JSON.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> populated from the string that contains JSON.</returns>
		// Token: 0x06000624 RID: 1572 RVA: 0x00016245 File Offset: 0x00014445
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		/// <summary>
		/// Parses the specified json.
		/// </summary>
		/// <param name="json">The json.</param>
		/// <param name="resolver">The resolver.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> populated from the string that contains JSON.</returns>
		// Token: 0x06000625 RID: 1573 RVA: 0x00016254 File Offset: 0x00014454
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonReader reader = new JsonTextReader(new StringReader(json));
			return JsonSchema.Read(reader, resolver);
		}

		/// <summary>
		/// Writes this schema to a <see cref="T:Newtonsoft.Json.JsonWriter" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		// Token: 0x06000626 RID: 1574 RVA: 0x0001627F File Offset: 0x0001447F
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		/// <summary>
		/// Writes this schema to a <see cref="T:Newtonsoft.Json.JsonWriter" /> using the specified <see cref="T:Newtonsoft.Json.Schema.JsonSchemaResolver" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		/// <param name="resolver">The resolver used.</param>
		// Token: 0x06000627 RID: 1575 RVA: 0x00016290 File Offset: 0x00014490
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			JsonSchemaWriter jsonSchemaWriter = new JsonSchemaWriter(writer, resolver);
			jsonSchemaWriter.WriteSchema(this);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		// Token: 0x06000628 RID: 1576 RVA: 0x000162C4 File Offset: 0x000144C4
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x040001DF RID: 479
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}

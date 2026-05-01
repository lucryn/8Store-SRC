using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000AE RID: 174
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00023C54 File Offset: 0x00021E54
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00023C5C File Offset: 0x00021E5C
		public string Id { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00023C65 File Offset: 0x00021E65
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00023C6D File Offset: 0x00021E6D
		public string Title { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00023C76 File Offset: 0x00021E76
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00023C7E File Offset: 0x00021E7E
		public bool? Required { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00023C87 File Offset: 0x00021E87
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00023C8F File Offset: 0x00021E8F
		public bool? ReadOnly { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00023C98 File Offset: 0x00021E98
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00023CA0 File Offset: 0x00021EA0
		public bool? Hidden { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00023CA9 File Offset: 0x00021EA9
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00023CB1 File Offset: 0x00021EB1
		public bool? Transient { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00023CBA File Offset: 0x00021EBA
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x00023CC2 File Offset: 0x00021EC2
		public string Description { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00023CCB File Offset: 0x00021ECB
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00023CD3 File Offset: 0x00021ED3
		public JsonSchemaType? Type { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00023CDC File Offset: 0x00021EDC
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x00023CE4 File Offset: 0x00021EE4
		public string Pattern { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00023CED File Offset: 0x00021EED
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00023CF5 File Offset: 0x00021EF5
		public int? MinimumLength { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00023CFE File Offset: 0x00021EFE
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00023D06 File Offset: 0x00021F06
		public int? MaximumLength { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00023D0F File Offset: 0x00021F0F
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00023D17 File Offset: 0x00021F17
		public double? DivisibleBy { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00023D20 File Offset: 0x00021F20
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x00023D28 File Offset: 0x00021F28
		public double? Minimum { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00023D31 File Offset: 0x00021F31
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00023D39 File Offset: 0x00021F39
		public double? Maximum { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00023D42 File Offset: 0x00021F42
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00023D4A File Offset: 0x00021F4A
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00023D53 File Offset: 0x00021F53
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00023D5B File Offset: 0x00021F5B
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00023D64 File Offset: 0x00021F64
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00023D6C File Offset: 0x00021F6C
		public int? MinimumItems { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00023D75 File Offset: 0x00021F75
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00023D7D File Offset: 0x00021F7D
		public int? MaximumItems { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00023D86 File Offset: 0x00021F86
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00023D8E File Offset: 0x00021F8E
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00023D97 File Offset: 0x00021F97
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00023D9F File Offset: 0x00021F9F
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00023DA8 File Offset: 0x00021FA8
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00023DB0 File Offset: 0x00021FB0
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00023DB9 File Offset: 0x00021FB9
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00023DC1 File Offset: 0x00021FC1
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00023DCA File Offset: 0x00021FCA
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00023DD2 File Offset: 0x00021FD2
		public bool UniqueItems { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00023DDB File Offset: 0x00021FDB
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00023DE3 File Offset: 0x00021FE3
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00023DEC File Offset: 0x00021FEC
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00023DFD File Offset: 0x00021FFD
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00023E05 File Offset: 0x00022005
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00023E0E File Offset: 0x0002200E
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00023E16 File Offset: 0x00022016
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00023E1F File Offset: 0x0002201F
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00023E27 File Offset: 0x00022027
		public string Requires { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00023E30 File Offset: 0x00022030
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x00023E38 File Offset: 0x00022038
		public IList<JToken> Enum { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00023E41 File Offset: 0x00022041
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00023E49 File Offset: 0x00022049
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00023E52 File Offset: 0x00022052
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00023E5A File Offset: 0x0002205A
		public JToken Default { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00023E63 File Offset: 0x00022063
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00023E6B File Offset: 0x0002206B
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00023E74 File Offset: 0x00022074
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00023E7C File Offset: 0x0002207C
		public string Format { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00023E85 File Offset: 0x00022085
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00023E8D File Offset: 0x0002208D
		internal string Location { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00023E96 File Offset: 0x00022096
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00023E9E File Offset: 0x0002209E
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00023EA6 File Offset: 0x000220A6
		internal string DeferredReference { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00023EAF File Offset: 0x000220AF
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00023EB7 File Offset: 0x000220B7
		internal bool ReferencesResolved { get; set; }

		// Token: 0x060008DA RID: 2266 RVA: 0x00023EC0 File Offset: 0x000220C0
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00023EF9 File Offset: 0x000220F9
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00023F06 File Offset: 0x00022106
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00023F2A File Offset: 0x0002212A
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00023F38 File Offset: 0x00022138
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonSchema result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				result = JsonSchema.Read(jsonReader, resolver);
			}
			return result;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00023F84 File Offset: 0x00022184
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00023F92 File Offset: 0x00022192
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00023FB8 File Offset: 0x000221B8
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x04000345 RID: 837
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}

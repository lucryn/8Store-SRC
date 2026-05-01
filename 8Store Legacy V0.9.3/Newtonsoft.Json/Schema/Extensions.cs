using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// Contains the JSON schema extension methods.
	/// </summary>
	// Token: 0x0200006F RID: 111
	public static class Extensions
	{
		/// <summary>
		/// Determines whether the <see cref="T:Newtonsoft.Json.Linq.JToken" /> is valid.
		/// </summary>
		/// <param name="source">The source <see cref="T:Newtonsoft.Json.Linq.JToken" /> to test.</param>
		/// <param name="schema">The schema to test with.</param>
		/// <returns>
		/// 	<c>true</c> if the specified <see cref="T:Newtonsoft.Json.Linq.JToken" /> is valid; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060005D4 RID: 1492 RVA: 0x00015E5C File Offset: 0x0001405C
		public static bool IsValid(this JToken source, JsonSchema schema)
		{
			bool valid = true;
			source.Validate(schema, delegate(object sender, ValidationEventArgs args)
			{
				valid = false;
			});
			return valid;
		}

		/// <summary>
		/// Determines whether the <see cref="T:Newtonsoft.Json.Linq.JToken" /> is valid.
		/// </summary>
		/// <param name="source">The source <see cref="T:Newtonsoft.Json.Linq.JToken" /> to test.</param>
		/// <param name="schema">The schema to test with.</param>
		/// <param name="errorMessages">When this method returns, contains any error messages generated while validating. </param>
		/// <returns>
		/// 	<c>true</c> if the specified <see cref="T:Newtonsoft.Json.Linq.JToken" /> is valid; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060005D5 RID: 1493 RVA: 0x00015EAC File Offset: 0x000140AC
		public static bool IsValid(this JToken source, JsonSchema schema, out IList<string> errorMessages)
		{
			IList<string> errors = new List<string>();
			source.Validate(schema, delegate(object sender, ValidationEventArgs args)
			{
				errors.Add(args.Message);
			});
			errorMessages = errors;
			return errorMessages.Count == 0;
		}

		/// <summary>
		/// Validates the specified <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="source">The source <see cref="T:Newtonsoft.Json.Linq.JToken" /> to test.</param>
		/// <param name="schema">The schema to test with.</param>
		// Token: 0x060005D6 RID: 1494 RVA: 0x00015EEF File Offset: 0x000140EF
		public static void Validate(this JToken source, JsonSchema schema)
		{
			source.Validate(schema, null);
		}

		/// <summary>
		/// Validates the specified <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <param name="source">The source <see cref="T:Newtonsoft.Json.Linq.JToken" /> to test.</param>
		/// <param name="schema">The schema to test with.</param>
		/// <param name="validationEventHandler">The validation event handler.</param>
		// Token: 0x060005D7 RID: 1495 RVA: 0x00015EFC File Offset: 0x000140FC
		public static void Validate(this JToken source, JsonSchema schema, ValidationEventHandler validationEventHandler)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			ValidationUtils.ArgumentNotNull(schema, "schema");
			using (JsonValidatingReader jsonValidatingReader = new JsonValidatingReader(source.CreateReader()))
			{
				jsonValidatingReader.Schema = schema;
				if (validationEventHandler != null)
				{
					jsonValidatingReader.ValidationEventHandler += validationEventHandler;
				}
				while (jsonValidatingReader.Read())
				{
				}
			}
		}
	}
}

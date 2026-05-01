using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Instructs the <see cref="T:Newtonsoft.Json.JsonSerializer" /> to use the specified <see cref="T:Newtonsoft.Json.JsonConverter" /> when serializing the member or class.
	/// </summary>
	// Token: 0x0200003E RID: 62
	[AttributeUsage(3484, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		/// <summary>
		/// Gets the type of the converter.
		/// </summary>
		/// <value>The type of the converter.</value>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000098DF File Offset: 0x00007ADF
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonConverterAttribute" /> class.
		/// </summary>
		/// <param name="converterType">Type of the converter.</param>
		// Token: 0x0600025E RID: 606 RVA: 0x000098E7 File Offset: 0x00007AE7
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009904 File Offset: 0x00007B04
		internal static JsonConverter CreateJsonConverterInstance(Type converterType)
		{
			JsonConverter result;
			try
			{
				result = (JsonConverter)Activator.CreateInstance(converterType);
			}
			catch (Exception innerException)
			{
				throw new JsonException("Error creating {0}".FormatWith(CultureInfo.InvariantCulture, converterType), innerException);
			}
			return result;
		}

		// Token: 0x040000D1 RID: 209
		private readonly Type _converterType;
	}
}

using System;
using System.Globalization;
using System.IO;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a raw JSON string.
	/// </summary>
	// Token: 0x02000063 RID: 99
	public class JRaw : JValue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JRaw" /> class from another <see cref="T:Newtonsoft.Json.Linq.JRaw" /> object.
		/// </summary>
		/// <param name="other">A <see cref="T:Newtonsoft.Json.Linq.JRaw" /> object to copy from.</param>
		// Token: 0x06000592 RID: 1426 RVA: 0x00015573 File Offset: 0x00013773
		public JRaw(JRaw other) : base(other)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JRaw" /> class.
		/// </summary>
		/// <param name="rawJson">The raw json.</param>
		// Token: 0x06000593 RID: 1427 RVA: 0x0001557C File Offset: 0x0001377C
		public JRaw(object rawJson) : base(rawJson, JTokenType.Raw)
		{
		}

		/// <summary>
		/// Creates an instance of <see cref="T:Newtonsoft.Json.Linq.JRaw" /> with the content of the reader's current token.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>An instance of <see cref="T:Newtonsoft.Json.Linq.JRaw" /> with the content of the reader's current token.</returns>
		// Token: 0x06000594 RID: 1428 RVA: 0x00015588 File Offset: 0x00013788
		public static JRaw Create(JsonReader reader)
		{
			JRaw result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					result = new JRaw(stringWriter.ToString());
				}
			}
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000155F0 File Offset: 0x000137F0
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}

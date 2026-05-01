using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	/// <summary>
	/// Represents a BSON Oid (object id).
	/// </summary>
	// Token: 0x02000004 RID: 4
	public class BsonObjectId
	{
		/// <summary>
		/// Gets or sets the value of the Oid.
		/// </summary>
		/// <value>The value of the Oid.</value>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002841 File Offset: 0x00000A41
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002849 File Offset: 0x00000A49
		public byte[] Value { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Bson.BsonObjectId" /> class.
		/// </summary>
		/// <param name="value">The Oid value.</param>
		// Token: 0x06000010 RID: 16 RVA: 0x00002852 File Offset: 0x00000A52
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}
	}
}

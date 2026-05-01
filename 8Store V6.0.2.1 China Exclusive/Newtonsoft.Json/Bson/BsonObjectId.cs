using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000105 RID: 261
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00033565 File Offset: 0x00031765
		public byte[] Value { get; }

		// Token: 0x06000CED RID: 3309 RVA: 0x0003356D File Offset: 0x0003176D
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

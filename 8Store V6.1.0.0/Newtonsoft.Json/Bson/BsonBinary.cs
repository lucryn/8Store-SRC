using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010E RID: 270
	internal class BsonBinary : BsonValue
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00034335 File Offset: 0x00032535
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0003433D File Offset: 0x0003253D
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x06000D2E RID: 3374 RVA: 0x00034346 File Offset: 0x00032546
		public BsonBinary(byte[] value, BsonBinaryType binaryType) : base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}

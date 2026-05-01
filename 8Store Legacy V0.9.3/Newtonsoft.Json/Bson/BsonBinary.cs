using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000F RID: 15
	internal class BsonBinary : BsonValue
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000043D0 File Offset: 0x000025D0
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000043D8 File Offset: 0x000025D8
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x06000089 RID: 137 RVA: 0x000043E1 File Offset: 0x000025E1
		public BsonBinary(byte[] value, BsonBinaryType binaryType) : base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}

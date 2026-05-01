using System;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Specifies the type of token.
	/// </summary>
	// Token: 0x02000066 RID: 102
	public enum JTokenType
	{
		/// <summary>
		/// No token type has been set.
		/// </summary>
		// Token: 0x040001AF RID: 431
		None,
		/// <summary>
		/// A JSON object.
		/// </summary>
		// Token: 0x040001B0 RID: 432
		Object,
		/// <summary>
		/// A JSON array.
		/// </summary>
		// Token: 0x040001B1 RID: 433
		Array,
		/// <summary>
		/// A JSON constructor.
		/// </summary>
		// Token: 0x040001B2 RID: 434
		Constructor,
		/// <summary>
		/// A JSON object property.
		/// </summary>
		// Token: 0x040001B3 RID: 435
		Property,
		/// <summary>
		/// A comment.
		/// </summary>
		// Token: 0x040001B4 RID: 436
		Comment,
		/// <summary>
		/// An integer value.
		/// </summary>
		// Token: 0x040001B5 RID: 437
		Integer,
		/// <summary>
		/// A float value.
		/// </summary>
		// Token: 0x040001B6 RID: 438
		Float,
		/// <summary>
		/// A string value.
		/// </summary>
		// Token: 0x040001B7 RID: 439
		String,
		/// <summary>
		/// A boolean value.
		/// </summary>
		// Token: 0x040001B8 RID: 440
		Boolean,
		/// <summary>
		/// A null value.
		/// </summary>
		// Token: 0x040001B9 RID: 441
		Null,
		/// <summary>
		/// An undefined value.
		/// </summary>
		// Token: 0x040001BA RID: 442
		Undefined,
		/// <summary>
		/// A date value.
		/// </summary>
		// Token: 0x040001BB RID: 443
		Date,
		/// <summary>
		/// A raw JSON value.
		/// </summary>
		// Token: 0x040001BC RID: 444
		Raw,
		/// <summary>
		/// A collection of bytes value.
		/// </summary>
		// Token: 0x040001BD RID: 445
		Bytes,
		/// <summary>
		/// A Guid value.
		/// </summary>
		// Token: 0x040001BE RID: 446
		Guid,
		/// <summary>
		/// A Uri value.
		/// </summary>
		// Token: 0x040001BF RID: 447
		Uri,
		/// <summary>
		/// A TimeSpan value.
		/// </summary>
		// Token: 0x040001C0 RID: 448
		TimeSpan
	}
}

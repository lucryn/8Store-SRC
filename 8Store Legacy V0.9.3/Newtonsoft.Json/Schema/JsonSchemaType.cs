using System;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// The value types allowed by the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" />.
	/// </summary>
	// Token: 0x0200007B RID: 123
	[Flags]
	public enum JsonSchemaType
	{
		/// <summary>
		/// No type specified.
		/// </summary>
		// Token: 0x0400025A RID: 602
		None = 0,
		/// <summary>
		/// String type.
		/// </summary>
		// Token: 0x0400025B RID: 603
		String = 1,
		/// <summary>
		/// Float type.
		/// </summary>
		// Token: 0x0400025C RID: 604
		Float = 2,
		/// <summary>
		/// Integer type.
		/// </summary>
		// Token: 0x0400025D RID: 605
		Integer = 4,
		/// <summary>
		/// Boolean type.
		/// </summary>
		// Token: 0x0400025E RID: 606
		Boolean = 8,
		/// <summary>
		/// Object type.
		/// </summary>
		// Token: 0x0400025F RID: 607
		Object = 16,
		/// <summary>
		/// Array type.
		/// </summary>
		// Token: 0x04000260 RID: 608
		Array = 32,
		/// <summary>
		/// Null type.
		/// </summary>
		// Token: 0x04000261 RID: 609
		Null = 64,
		/// <summary>
		/// Any type.
		/// </summary>
		// Token: 0x04000262 RID: 610
		Any = 127
	}
}

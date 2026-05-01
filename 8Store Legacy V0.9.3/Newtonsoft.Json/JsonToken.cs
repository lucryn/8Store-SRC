using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies the type of Json token.
	/// </summary>
	// Token: 0x0200004F RID: 79
	public enum JsonToken
	{
		/// <summary>
		/// This is returned by the <see cref="T:Newtonsoft.Json.JsonReader" /> if a <see cref="M:Newtonsoft.Json.JsonReader.Read" /> method has not been called. 
		/// </summary>
		// Token: 0x04000157 RID: 343
		None,
		/// <summary>
		/// An object start token.
		/// </summary>
		// Token: 0x04000158 RID: 344
		StartObject,
		/// <summary>
		/// An array start token.
		/// </summary>
		// Token: 0x04000159 RID: 345
		StartArray,
		/// <summary>
		/// A constructor start token.
		/// </summary>
		// Token: 0x0400015A RID: 346
		StartConstructor,
		/// <summary>
		/// An object property name.
		/// </summary>
		// Token: 0x0400015B RID: 347
		PropertyName,
		/// <summary>
		/// A comment.
		/// </summary>
		// Token: 0x0400015C RID: 348
		Comment,
		/// <summary>
		/// Raw JSON.
		/// </summary>
		// Token: 0x0400015D RID: 349
		Raw,
		/// <summary>
		/// An integer.
		/// </summary>
		// Token: 0x0400015E RID: 350
		Integer,
		/// <summary>
		/// A float.
		/// </summary>
		// Token: 0x0400015F RID: 351
		Float,
		/// <summary>
		/// A string.
		/// </summary>
		// Token: 0x04000160 RID: 352
		String,
		/// <summary>
		/// A boolean.
		/// </summary>
		// Token: 0x04000161 RID: 353
		Boolean,
		/// <summary>
		/// A null token.
		/// </summary>
		// Token: 0x04000162 RID: 354
		Null,
		/// <summary>
		/// An undefined token.
		/// </summary>
		// Token: 0x04000163 RID: 355
		Undefined,
		/// <summary>
		/// An object end token.
		/// </summary>
		// Token: 0x04000164 RID: 356
		EndObject,
		/// <summary>
		/// An array end token.
		/// </summary>
		// Token: 0x04000165 RID: 357
		EndArray,
		/// <summary>
		/// A constructor end token.
		/// </summary>
		// Token: 0x04000166 RID: 358
		EndConstructor,
		/// <summary>
		/// A Date.
		/// </summary>
		// Token: 0x04000167 RID: 359
		Date,
		/// <summary>
		/// Byte data.
		/// </summary>
		// Token: 0x04000168 RID: 360
		Bytes
	}
}

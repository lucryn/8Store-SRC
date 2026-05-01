using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies how dates are formatted when writing JSON text.
	/// </summary>
	// Token: 0x02000031 RID: 49
	public enum DateFormatHandling
	{
		/// <summary>
		/// Dates are written in the ISO 8601 format, e.g. "2012-03-21T05:40Z".
		/// </summary>
		// Token: 0x040000A3 RID: 163
		IsoDateFormat,
		/// <summary>
		/// Dates are written in the Microsoft JSON format, e.g. "\/Date(1198908717056)\/".
		/// </summary>
		// Token: 0x040000A4 RID: 164
		MicrosoftDateFormat
	}
}

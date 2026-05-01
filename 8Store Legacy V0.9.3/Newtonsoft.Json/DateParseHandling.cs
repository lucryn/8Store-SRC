using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies how date formatted strings, e.g. "\/Date(1198908717056)\/" and "2012-03-21T05:40Z", are parsed when reading JSON text.
	/// </summary>
	// Token: 0x02000032 RID: 50
	public enum DateParseHandling
	{
		/// <summary>
		/// Date formatted strings are not parsed to a date type and are read as strings.
		/// </summary>
		// Token: 0x040000A6 RID: 166
		None,
		/// <summary>
		/// Date formatted strings, e.g. "\/Date(1198908717056)\/" and "2012-03-21T05:40Z", are parsed to <see cref="F:Newtonsoft.Json.DateParseHandling.DateTime" />.
		/// </summary>
		// Token: 0x040000A7 RID: 167
		DateTime,
		/// <summary>
		/// Date formatted strings, e.g. "\/Date(1198908717056)\/" and "2012-03-21T05:40Z", are parsed to <see cref="F:Newtonsoft.Json.DateParseHandling.DateTimeOffset" />.
		/// </summary>
		// Token: 0x040000A8 RID: 168
		DateTimeOffset
	}
}

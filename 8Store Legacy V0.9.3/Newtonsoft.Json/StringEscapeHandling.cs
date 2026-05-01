using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies how strings are escaped when writing JSON text.
	/// </summary>
	// Token: 0x020000AB RID: 171
	public enum StringEscapeHandling
	{
		/// <summary>
		/// Only control characters (e.g. newline) are escaped.
		/// </summary>
		// Token: 0x04000318 RID: 792
		Default,
		/// <summary>
		/// All non-ASCII and control characters (e.g. newline) are escaped.
		/// </summary>
		// Token: 0x04000319 RID: 793
		EscapeNonAscii,
		/// <summary>
		/// HTML (&lt;, &gt;, &amp;, ', ") and control characters (e.g. newline) are escaped.
		/// </summary>
		// Token: 0x0400031A RID: 794
		EscapeHtml
	}
}

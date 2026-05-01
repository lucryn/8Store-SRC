using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Provides an interface to enable a class to return line and position information.
	/// </summary>
	// Token: 0x02000039 RID: 57
	public interface IJsonLineInfo
	{
		/// <summary>
		/// Gets a value indicating whether the class can return line information.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if LineNumber and LinePosition can be provided; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060001FD RID: 509
		bool HasLineInfo();

		/// <summary>
		/// Gets the current line number.
		/// </summary>
		/// <value>The current line number or 0 if no line information is available (for example, HasLineInfo returns false).</value>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001FE RID: 510
		int LineNumber { get; }

		/// <summary>
		/// Gets the current line position.
		/// </summary>
		/// <value>The current line position or 0 if no line information is available (for example, HasLineInfo returns false).</value>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001FF RID: 511
		int LinePosition { get; }
	}
}

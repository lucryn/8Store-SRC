using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies float format handling options when writing special floating point numbers, e.g. <see cref="F:System.Double.NaN" />,
	/// <see cref="F:System.Double.PositiveInfinity" /> and <see cref="F:System.Double.NegativeInfinity" /> with <see cref="T:Newtonsoft.Json.JsonWriter" />.
	/// </summary>
	// Token: 0x02000034 RID: 52
	public enum FloatFormatHandling
	{
		/// <summary>
		/// Write special floating point values as strings in JSON, e.g. "NaN", "Infinity", "-Infinity".
		/// </summary>
		// Token: 0x040000AF RID: 175
		String,
		/// <summary>
		/// Write special floating point values as symbols in JSON, e.g. NaN, Infinity, -Infinity.
		/// Note that this will produce non-valid JSON.
		/// </summary>
		// Token: 0x040000B0 RID: 176
		Symbol,
		/// <summary>
		/// Write special floating point values as the property's default value in JSON, e.g. 0.0 for a <see cref="T:System.Double" /> property, null for a <see cref="T:System.Nullable`1" /> property.
		/// </summary>
		// Token: 0x040000B1 RID: 177
		DefaultValue
	}
}

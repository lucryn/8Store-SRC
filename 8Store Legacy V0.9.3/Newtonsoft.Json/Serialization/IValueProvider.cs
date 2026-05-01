using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Provides methods to get and set values.
	/// </summary>
	// Token: 0x0200008C RID: 140
	public interface IValueProvider
	{
		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="target">The target to set the value on.</param>
		/// <param name="value">The value to set on the target.</param>
		// Token: 0x06000724 RID: 1828
		void SetValue(object target, object value);

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <param name="target">The target to get the value from.</param>
		/// <returns>The value.</returns>
		// Token: 0x06000725 RID: 1829
		object GetValue(object target);
	}
}

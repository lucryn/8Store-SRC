using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Compares tokens to determine whether they are equal.
	/// </summary>
	// Token: 0x02000064 RID: 100
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object of type <see cref="T:Newtonsoft.Json.Linq.JToken" /> to compare.</param>
		/// <param name="y">The second object of type <see cref="T:Newtonsoft.Json.Linq.JToken" /> to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		// Token: 0x06000596 RID: 1430 RVA: 0x000155F8 File Offset: 0x000137F8
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
		// Token: 0x06000597 RID: 1431 RVA: 0x00015601 File Offset: 0x00013801
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}

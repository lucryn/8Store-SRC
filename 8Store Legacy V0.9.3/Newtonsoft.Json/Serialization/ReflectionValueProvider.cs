using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Get and set values for a <see cref="T:System.Reflection.MemberInfo" /> using reflection.
	/// </summary>
	// Token: 0x020000A7 RID: 167
	public class ReflectionValueProvider : IValueProvider
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.ReflectionValueProvider" /> class.
		/// </summary>
		/// <param name="memberInfo">The member info.</param>
		// Token: 0x06000891 RID: 2193 RVA: 0x00020E27 File Offset: 0x0001F027
		public ReflectionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="target">The target to set the value on.</param>
		/// <param name="value">The value to set on the target.</param>
		// Token: 0x06000892 RID: 2194 RVA: 0x00020E44 File Offset: 0x0001F044
		public void SetValue(object target, object value)
		{
			try
			{
				ReflectionUtils.SetMemberValue(this._memberInfo, target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <param name="target">The target to get the value from.</param>
		/// <returns>The value.</returns>
		// Token: 0x06000893 RID: 2195 RVA: 0x00020E98 File Offset: 0x0001F098
		public object GetValue(object target)
		{
			object memberValue;
			try
			{
				memberValue = ReflectionUtils.GetMemberValue(this._memberInfo, target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return memberValue;
		}

		// Token: 0x0400030E RID: 782
		private readonly MemberInfo _memberInfo;
	}
}

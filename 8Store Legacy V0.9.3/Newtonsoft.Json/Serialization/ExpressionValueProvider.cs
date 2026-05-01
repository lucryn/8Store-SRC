using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Get and set values for a <see cref="T:System.Reflection.MemberInfo" /> using dynamic methods.
	/// </summary>
	// Token: 0x0200008D RID: 141
	public class ExpressionValueProvider : IValueProvider
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.ExpressionValueProvider" /> class.
		/// </summary>
		/// <param name="memberInfo">The member info.</param>
		// Token: 0x06000726 RID: 1830 RVA: 0x0001AB5F File Offset: 0x00018D5F
		public ExpressionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="target">The target to set the value on.</param>
		/// <param name="value">The value to set on the target.</param>
		// Token: 0x06000727 RID: 1831 RVA: 0x0001AB7C File Offset: 0x00018D7C
		public void SetValue(object target, object value)
		{
			try
			{
				if (this._setter == null)
				{
					this._setter = ExpressionReflectionDelegateFactory.Instance.CreateSet<object>(this._memberInfo);
				}
				this._setter.Invoke(target, value);
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
		// Token: 0x06000728 RID: 1832 RVA: 0x0001ABF0 File Offset: 0x00018DF0
		public object GetValue(object target)
		{
			object result;
			try
			{
				if (this._getter == null)
				{
					this._getter = ExpressionReflectionDelegateFactory.Instance.CreateGet<object>(this._memberInfo);
				}
				result = this._getter.Invoke(target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return result;
		}

		// Token: 0x04000287 RID: 647
		private readonly MemberInfo _memberInfo;

		// Token: 0x04000288 RID: 648
		private Func<object, object> _getter;

		// Token: 0x04000289 RID: 649
		private Action<object, object> _setter;
	}
}

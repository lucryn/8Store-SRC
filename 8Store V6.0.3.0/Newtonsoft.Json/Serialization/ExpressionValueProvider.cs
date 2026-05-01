using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000086 RID: 134
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
	public class ExpressionValueProvider : IValueProvider
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x0001B58C File Offset: 0x0001978C
		public ExpressionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public void SetValue(object target, [Nullable(2)] object value)
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

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B61C File Offset: 0x0001981C
		[return: Nullable(2)]
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

		// Token: 0x04000286 RID: 646
		private readonly MemberInfo _memberInfo;

		// Token: 0x04000287 RID: 647
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		private Func<object, object> _getter;

		// Token: 0x04000288 RID: 648
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		private Action<object, object> _setter;
	}
}

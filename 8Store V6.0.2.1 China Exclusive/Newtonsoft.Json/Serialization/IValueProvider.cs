using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008C RID: 140
	[NullableContext(1)]
	public interface IValueProvider
	{
		// Token: 0x0600069F RID: 1695
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x060006A0 RID: 1696
		[return: Nullable(2)]
		object GetValue(object target);
	}
}

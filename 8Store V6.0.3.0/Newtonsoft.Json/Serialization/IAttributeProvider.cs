using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000087 RID: 135
	[NullableContext(1)]
	public interface IAttributeProvider
	{
		// Token: 0x06000694 RID: 1684
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x06000695 RID: 1685
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}

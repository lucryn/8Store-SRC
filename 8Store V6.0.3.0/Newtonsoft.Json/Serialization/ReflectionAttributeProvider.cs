using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A7 RID: 167
	[NullableContext(1)]
	[Nullable(0)]
	public class ReflectionAttributeProvider : IAttributeProvider
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x00022E98 File Offset: 0x00021098
		public ReflectionAttributeProvider(object attributeProvider)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00022EB2 File Offset: 0x000210B2
		public IList<Attribute> GetAttributes(bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, null, inherit);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00022EC1 File Offset: 0x000210C1
		public IList<Attribute> GetAttributes(Type attributeType, bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, attributeType, inherit);
		}

		// Token: 0x0400031A RID: 794
		private readonly object _attributeProvider;
	}
}

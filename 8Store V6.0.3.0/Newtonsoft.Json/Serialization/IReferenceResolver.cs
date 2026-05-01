using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000089 RID: 137
	[NullableContext(1)]
	public interface IReferenceResolver
	{
		// Token: 0x06000697 RID: 1687
		object ResolveReference(object context, string reference);

		// Token: 0x06000698 RID: 1688
		string GetReference(object context, object value);

		// Token: 0x06000699 RID: 1689
		bool IsReferenced(object context, object value);

		// Token: 0x0600069A RID: 1690
		void AddReference(object context, string reference, object value);
	}
}

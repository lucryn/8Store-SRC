using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005C RID: 92
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DynamicUtils
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00014603 File Offset: 0x00012803
		public static IEnumerable<string> GetDynamicMemberNames(this IDynamicMetaObjectProvider dynamicProvider)
		{
			return dynamicProvider.GetMetaObject(Expression.Constant(dynamicProvider)).GetDynamicMemberNames();
		}

		// Token: 0x0200017A RID: 378
		[Nullable(0)]
		internal static class BinderWrapper
		{
			// Token: 0x06000E45 RID: 3653 RVA: 0x0003F2EA File Offset: 0x0003D4EA
			[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
			[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
			public static CallSiteBinder GetMember(string name, Type context)
			{
				return Binder.GetMember(0, name, context, new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(0, null)
				});
			}

			// Token: 0x06000E46 RID: 3654 RVA: 0x0003F304 File Offset: 0x0003D504
			[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
			[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
			public static CallSiteBinder SetMember(string name, Type context)
			{
				return Binder.SetMember(0, name, context, new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(1, null),
					CSharpArgumentInfo.Create(2, null)
				});
			}
		}
	}
}

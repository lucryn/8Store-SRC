using System;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005D RID: 93
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
	internal class NoThrowGetBinderMember : GetMemberBinder
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00014616 File Offset: 0x00012816
		public NoThrowGetBinderMember(GetMemberBinder innerBinder) : base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00014634 File Offset: 0x00012834
		public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, [Nullable(2)] DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, CollectionUtils.ArrayEmpty<DynamicMetaObject>());
			return new DynamicMetaObject(new NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}

		// Token: 0x040001FD RID: 509
		private readonly GetMemberBinder _innerBinder;
	}
}

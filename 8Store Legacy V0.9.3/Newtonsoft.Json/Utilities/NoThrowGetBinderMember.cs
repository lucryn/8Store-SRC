using System;
using System.Dynamic;
using System.Linq.Expressions;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C3 RID: 195
	internal class NoThrowGetBinderMember : GetMemberBinder
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x00025997 File Offset: 0x00023B97
		public NoThrowGetBinderMember(GetMemberBinder innerBinder) : base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000259B4 File Offset: 0x00023BB4
		public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, new DynamicMetaObject[0]);
			NoThrowExpressionVisitor noThrowExpressionVisitor = new NoThrowExpressionVisitor();
			Expression expression = noThrowExpressionVisitor.Visit(dynamicMetaObject.Expression);
			return new DynamicMetaObject(expression, dynamicMetaObject.Restrictions);
		}

		// Token: 0x040003A9 RID: 937
		private readonly GetMemberBinder _innerBinder;
	}
}

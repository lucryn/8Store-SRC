using System;
using System.Dynamic;
using System.Linq.Expressions;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C4 RID: 196
	internal class NoThrowSetBinderMember : SetMemberBinder
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x000259F5 File Offset: 0x00023BF5
		public NoThrowSetBinderMember(SetMemberBinder innerBinder) : base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00025A10 File Offset: 0x00023C10
		public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, new DynamicMetaObject[]
			{
				value
			});
			NoThrowExpressionVisitor noThrowExpressionVisitor = new NoThrowExpressionVisitor();
			Expression expression = noThrowExpressionVisitor.Visit(dynamicMetaObject.Expression);
			return new DynamicMetaObject(expression, dynamicMetaObject.Restrictions);
		}

		// Token: 0x040003AA RID: 938
		private readonly SetMemberBinder _innerBinder;
	}
}

using System;
using System.Linq.Expressions;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C5 RID: 197
	internal class NoThrowExpressionVisitor : ExpressionVisitor
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x00025A5A File Offset: 0x00023C5A
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			if (node.IfFalse.NodeType == 60)
			{
				return Expression.Condition(node.Test, node.IfTrue, Expression.Constant(NoThrowExpressionVisitor.ErrorResult));
			}
			return base.VisitConditional(node);
		}

		// Token: 0x040003AB RID: 939
		internal static readonly object ErrorResult = new object();
	}
}

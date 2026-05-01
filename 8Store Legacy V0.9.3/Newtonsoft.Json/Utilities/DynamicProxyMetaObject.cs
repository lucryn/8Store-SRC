using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BE RID: 190
	internal sealed class DynamicProxyMetaObject<T> : DynamicMetaObject
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x00024B29 File Offset: 0x00022D29
		internal DynamicProxyMetaObject(Expression expression, T value, DynamicProxy<T> proxy, bool dontFallbackFirst) : base(expression, BindingRestrictions.Empty, value)
		{
			this._proxy = proxy;
			this._dontFallbackFirst = dontFallbackFirst;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00024B4C File Offset: 0x00022D4C
		private T Value
		{
			get
			{
				return (T)((object)base.Value);
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00024B59 File Offset: 0x00022D59
		private bool IsOverridden(string method)
		{
			return ReflectionUtils.IsMethodOverridden(this._proxy.GetType(), typeof(DynamicProxy<T>), method);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00024B94 File Offset: 0x00022D94
		public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			if (!this.IsOverridden("TryGetMember"))
			{
				return base.BindGetMember(binder);
			}
			return this.CallMethodWithResult("TryGetMember", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackGetMember(this, e), null);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00024C14 File Offset: 0x00022E14
		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			if (!this.IsOverridden("TrySetMember"))
			{
				return base.BindSetMember(binder, value);
			}
			return this.CallMethodReturnLast("TrySetMember", binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[]
			{
				value
			}), (DynamicMetaObject e) => binder.FallbackSetMember(this, value, e));
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00024CAC File Offset: 0x00022EAC
		public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			if (!this.IsOverridden("TryDeleteMember"))
			{
				return base.BindDeleteMember(binder);
			}
			return this.CallMethodNoResult("TryDeleteMember", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackDeleteMember(this, e));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00024D28 File Offset: 0x00022F28
		public override DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			if (!this.IsOverridden("TryConvert"))
			{
				return base.BindConvert(binder);
			}
			return this.CallMethodWithResult("TryConvert", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackConvert(this, e), null);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00024DC0 File Offset: 0x00022FC0
		public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryInvokeMember"))
			{
				return base.BindInvokeMember(binder, args);
			}
			DynamicProxyMetaObject<T>.Fallback fallback = (DynamicMetaObject e) => binder.FallbackInvokeMember(this, args, e);
			DynamicMetaObject dynamicMetaObject = this.BuildCallMethodWithResult("TryInvokeMember", binder, DynamicProxyMetaObject<T>.GetArgArray(args), this.BuildCallMethodWithResult("TryGetMember", new DynamicProxyMetaObject<T>.GetBinderAdapter(binder), DynamicProxyMetaObject<T>.NoArgs, fallback(null), (DynamicMetaObject e) => binder.FallbackInvoke(e, args, null)), null);
			if (!this._dontFallbackFirst)
			{
				return fallback(dynamicMetaObject);
			}
			return dynamicMetaObject;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00024E94 File Offset: 0x00023094
		public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryCreateInstance"))
			{
				return base.BindCreateInstance(binder, args);
			}
			return this.CallMethodWithResult("TryCreateInstance", binder, DynamicProxyMetaObject<T>.GetArgArray(args), (DynamicMetaObject e) => binder.FallbackCreateInstance(this, args, e), null);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00024F28 File Offset: 0x00023128
		public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryInvoke"))
			{
				return base.BindInvoke(binder, args);
			}
			return this.CallMethodWithResult("TryInvoke", binder, DynamicProxyMetaObject<T>.GetArgArray(args), (DynamicMetaObject e) => binder.FallbackInvoke(this, args, e), null);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00024FBC File Offset: 0x000231BC
		public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			if (!this.IsOverridden("TryBinaryOperation"))
			{
				return base.BindBinaryOperation(binder, arg);
			}
			return this.CallMethodWithResult("TryBinaryOperation", binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[]
			{
				arg
			}), (DynamicMetaObject e) => binder.FallbackBinaryOperation(this, arg, e), null);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00025054 File Offset: 0x00023254
		public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			if (!this.IsOverridden("TryUnaryOperation"))
			{
				return base.BindUnaryOperation(binder);
			}
			return this.CallMethodWithResult("TryUnaryOperation", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackUnaryOperation(this, e), null);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000250D4 File Offset: 0x000232D4
		public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden("TryGetIndex"))
			{
				return base.BindGetIndex(binder, indexes);
			}
			return this.CallMethodWithResult("TryGetIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), (DynamicMetaObject e) => binder.FallbackGetIndex(this, indexes, e), null);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00025170 File Offset: 0x00023370
		public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			if (!this.IsOverridden("TrySetIndex"))
			{
				return base.BindSetIndex(binder, indexes, value);
			}
			return this.CallMethodReturnLast("TrySetIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes, value), (DynamicMetaObject e) => binder.FallbackSetIndex(this, indexes, value, e));
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00025218 File Offset: 0x00023418
		public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden("TryDeleteIndex"))
			{
				return base.BindDeleteIndex(binder, indexes);
			}
			return this.CallMethodNoResult("TryDeleteIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), (DynamicMetaObject e) => binder.FallbackDeleteIndex(this, indexes, e));
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002529F File Offset: 0x0002349F
		private static Expression[] GetArgs(params DynamicMetaObject[] args)
		{
			return Enumerable.ToArray<UnaryExpression>(Enumerable.Select<DynamicMetaObject, UnaryExpression>(args, (DynamicMetaObject arg) => Expression.Convert(arg.Expression, typeof(object))));
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000252CC File Offset: 0x000234CC
		private static Expression[] GetArgArray(DynamicMetaObject[] args)
		{
			return new NewArrayExpression[]
			{
				Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args))
			};
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000252FC File Offset: 0x000234FC
		private static Expression[] GetArgArray(DynamicMetaObject[] args, DynamicMetaObject value)
		{
			return new Expression[]
			{
				Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args)),
				Expression.Convert(value.Expression, typeof(object))
			};
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00025344 File Offset: 0x00023544
		private static ConstantExpression Constant(DynamicMetaObjectBinder binder)
		{
			Type type = binder.GetType();
			while (!type.IsVisible())
			{
				type = type.BaseType();
			}
			return Expression.Constant(binder, type);
		}

		/// <summary>
		/// Helper method for generating a MetaObject which calls a
		/// specific method on Dynamic that returns a result
		/// </summary>
		// Token: 0x06000980 RID: 2432 RVA: 0x00025370 File Offset: 0x00023570
		private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicProxyMetaObject<T>.Fallback fallback, DynamicProxyMetaObject<T>.Fallback fallbackInvoke = null)
		{
			DynamicMetaObject fallbackResult = fallback(null);
			DynamicMetaObject dynamicMetaObject = this.BuildCallMethodWithResult(methodName, binder, args, fallbackResult, fallbackInvoke);
			if (!this._dontFallbackFirst)
			{
				return fallback(dynamicMetaObject);
			}
			return dynamicMetaObject;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000253A8 File Offset: 0x000235A8
		private DynamicMetaObject BuildCallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicMetaObject fallbackResult, DynamicProxyMetaObject<T>.Fallback fallbackInvoke)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list.Add(parameterExpression);
			DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
			if (binder.ReturnType != typeof(object))
			{
				UnaryExpression unaryExpression = Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType);
				dynamicMetaObject = new DynamicMetaObject(unaryExpression, dynamicMetaObject.Restrictions);
			}
			if (fallbackInvoke != null)
			{
				dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
			}
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), dynamicMetaObject.Expression, fallbackResult.Expression, binder.ReturnType)
			}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
		}

		/// <summary>
		/// Helper method for generating a MetaObject which calls a
		/// specific method on Dynamic, but uses one of the arguments for
		/// the result.
		/// </summary>
		// Token: 0x06000982 RID: 2434 RVA: 0x000254CC File Offset: 0x000236CC
		private DynamicMetaObject CallMethodReturnLast(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list[args.Length + 1] = Expression.Assign(parameterExpression, list[args.Length + 1]);
			DynamicMetaObject dynamicMetaObject2 = new DynamicMetaObject(Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), parameterExpression, dynamicMetaObject.Expression, typeof(object))
			}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
			if (!this._dontFallbackFirst)
			{
				return fallback(dynamicMetaObject2);
			}
			return dynamicMetaObject2;
		}

		/// <summary>
		/// Helper method for generating a MetaObject which calls a
		/// specific method on Dynamic, but uses one of the arguments for
		/// the result.
		/// </summary>
		// Token: 0x06000983 RID: 2435 RVA: 0x000255C8 File Offset: 0x000237C8
		private DynamicMetaObject CallMethodNoResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			DynamicMetaObject dynamicMetaObject2 = new DynamicMetaObject(Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), Expression.Empty(), dynamicMetaObject.Expression, typeof(void)), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
			if (!this._dontFallbackFirst)
			{
				return fallback(dynamicMetaObject2);
			}
			return dynamicMetaObject2;
		}

		/// <summary>
		/// Returns a Restrictions object which includes our current restrictions merged
		/// with a restriction limiting our type
		/// </summary>
		// Token: 0x06000984 RID: 2436 RVA: 0x00025676 File Offset: 0x00023876
		private BindingRestrictions GetRestrictions()
		{
			if (this.Value != null || !base.HasValue)
			{
				return BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
			}
			return BindingRestrictions.GetInstanceRestriction(base.Expression, null);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000256AB File Offset: 0x000238AB
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return this._proxy.GetDynamicMemberNames(this.Value);
		}

		// Token: 0x0400039B RID: 923
		private readonly DynamicProxy<T> _proxy;

		// Token: 0x0400039C RID: 924
		private readonly bool _dontFallbackFirst;

		// Token: 0x0400039D RID: 925
		private static readonly Expression[] NoArgs = new Expression[0];

		// Token: 0x020000BF RID: 191
		// (Invoke) Token: 0x06000989 RID: 2441
		private delegate DynamicMetaObject Fallback(DynamicMetaObject errorSuggestion);

		// Token: 0x020000C0 RID: 192
		private sealed class GetBinderAdapter : GetMemberBinder
		{
			// Token: 0x0600098C RID: 2444 RVA: 0x000256CB File Offset: 0x000238CB
			internal GetBinderAdapter(InvokeMemberBinder binder) : base(binder.Name, binder.IgnoreCase)
			{
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x000256DF File Offset: 0x000238DF
			public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
			{
				throw new NotSupportedException();
			}
		}
	}
}

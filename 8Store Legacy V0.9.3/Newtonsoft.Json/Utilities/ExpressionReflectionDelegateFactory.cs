using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000CA RID: 202
	internal class ExpressionReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00025EF2 File Offset: 0x000240F2
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return ExpressionReflectionDelegateFactory._instance;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00025EFC File Offset: 0x000240FC
		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			Type typeFromHandle = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "target");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "args");
			ParameterInfo[] parameters = method.GetParameters();
			Expression[] array = new Expression[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				Expression expression = Expression.Constant(i);
				Expression expression2 = Expression.ArrayIndex(parameterExpression2, expression);
				expression2 = this.EnsureCastExpression(expression2, parameters[i].ParameterType);
				array[i] = expression2;
			}
			Expression expression3;
			if (method.IsConstructor)
			{
				expression3 = Expression.New((ConstructorInfo)method, array);
			}
			else if (method.IsStatic)
			{
				expression3 = Expression.Call((MethodInfo)method, array);
			}
			else
			{
				Expression expression4 = this.EnsureCastExpression(parameterExpression, method.DeclaringType);
				expression3 = Expression.Call(expression4, (MethodInfo)method, array);
			}
			if (method is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)method;
				if (methodInfo.ReturnType != typeof(void))
				{
					expression3 = this.EnsureCastExpression(expression3, typeFromHandle);
				}
				else
				{
					expression3 = Expression.Block(expression3, Expression.Constant(null));
				}
			}
			LambdaExpression lambdaExpression = Expression.Lambda(typeof(MethodCall<T, object>), expression3, new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			});
			return (MethodCall<T, object>)lambdaExpression.Compile();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002608C File Offset: 0x0002428C
		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsAbstract())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			Func<T> result;
			try
			{
				Type typeFromHandle = typeof(T);
				Expression expression = Expression.New(type);
				expression = this.EnsureCastExpression(expression, typeFromHandle);
				LambdaExpression lambdaExpression = Expression.Lambda(typeof(Func<T>), expression, new ParameterExpression[0]);
				Func<T> func = (Func<T>)lambdaExpression.Compile();
				result = func;
			}
			catch
			{
				result = (() => (T)((object)Activator.CreateInstance(type)));
			}
			return result;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00026150 File Offset: 0x00024350
		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			Type typeFromHandle = typeof(T);
			Type typeFromHandle2 = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "instance");
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			Expression expression;
			if (getMethod.IsStatic)
			{
				expression = Expression.MakeMemberAccess(null, propertyInfo);
			}
			else
			{
				Expression expression2 = this.EnsureCastExpression(parameterExpression, propertyInfo.DeclaringType);
				expression = Expression.MakeMemberAccess(expression2, propertyInfo);
			}
			expression = this.EnsureCastExpression(expression, typeFromHandle2);
			LambdaExpression lambdaExpression = Expression.Lambda(typeof(Func<T, object>), expression, new ParameterExpression[]
			{
				parameterExpression
			});
			return (Func<T, object>)lambdaExpression.Compile();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000261F8 File Offset: 0x000243F8
		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "source");
			Expression expression;
			if (fieldInfo.IsStatic)
			{
				expression = Expression.Field(null, fieldInfo);
			}
			else
			{
				Expression expression2 = this.EnsureCastExpression(parameterExpression, fieldInfo.DeclaringType);
				expression = Expression.Field(expression2, fieldInfo);
			}
			expression = this.EnsureCastExpression(expression, typeof(object));
			return Expression.Lambda<Func<T, object>>(expression, new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002627C File Offset: 0x0002447C
		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			if (fieldInfo.DeclaringType.IsValueType() || fieldInfo.IsInitOnly)
			{
				return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(fieldInfo);
			}
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "source");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "value");
			Expression expression;
			if (fieldInfo.IsStatic)
			{
				expression = Expression.Field(null, fieldInfo);
			}
			else
			{
				Expression expression2 = this.EnsureCastExpression(parameterExpression, fieldInfo.DeclaringType);
				expression = Expression.Field(expression2, fieldInfo);
			}
			Expression expression3 = this.EnsureCastExpression(parameterExpression2, expression.Type);
			BinaryExpression binaryExpression = Expression.Assign(expression, expression3);
			LambdaExpression lambdaExpression = Expression.Lambda(typeof(Action<T, object>), binaryExpression, new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			});
			return (Action<T, object>)lambdaExpression.Compile();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00026358 File Offset: 0x00024558
		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			if (propertyInfo.DeclaringType.IsValueType())
			{
				return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(propertyInfo);
			}
			Type typeFromHandle = typeof(T);
			Type typeFromHandle2 = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "instance");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeFromHandle2, "value");
			Expression expression = this.EnsureCastExpression(parameterExpression2, propertyInfo.PropertyType);
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			Expression expression2;
			if (setMethod.IsStatic)
			{
				expression2 = Expression.Call(setMethod, expression);
			}
			else
			{
				Expression expression3 = this.EnsureCastExpression(parameterExpression, propertyInfo.DeclaringType);
				expression2 = Expression.Call(expression3, setMethod, new Expression[]
				{
					expression
				});
			}
			LambdaExpression lambdaExpression = Expression.Lambda(typeof(Action<T, object>), expression2, new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			});
			return (Action<T, object>)lambdaExpression.Compile();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00026448 File Offset: 0x00024648
		private Expression EnsureCastExpression(Expression expression, Type targetType)
		{
			Type type = expression.Type;
			if (type == targetType || (!type.IsValueType() && targetType.IsAssignableFrom(type)))
			{
				return expression;
			}
			return Expression.Convert(expression, targetType);
		}

		// Token: 0x040003B0 RID: 944
		private static readonly ExpressionReflectionDelegateFactory _instance = new ExpressionReflectionDelegateFactory();
	}
}

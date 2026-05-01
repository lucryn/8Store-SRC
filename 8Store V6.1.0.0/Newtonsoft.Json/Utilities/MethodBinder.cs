using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006B RID: 107
	[NullableContext(1)]
	[Nullable(0)]
	internal static class MethodBinder
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x000169F4 File Offset: 0x00014BF4
		private static bool CanConvertPrimitive(Type from, Type to)
		{
			if (from == to)
			{
				return true;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < MethodBinder.PrimitiveTypes.Length; i++)
			{
				if (MethodBinder.PrimitiveTypes[i] == from)
				{
					num = MethodBinder.WideningMasks[i];
				}
				else if (MethodBinder.PrimitiveTypes[i] == to)
				{
					num2 = 1 << i;
				}
				if (num != 0 && num2 != 0)
				{
					break;
				}
			}
			return (num & num2) != 0;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00016A50 File Offset: 0x00014C50
		private static bool FilterParameters(ParameterInfo[] parameters, IList<Type> types, bool enableParamArray)
		{
			ValidationUtils.ArgumentNotNull(parameters, "parameters");
			ValidationUtils.ArgumentNotNull(types, "types");
			if (parameters.Length == 0)
			{
				return types.Count == 0;
			}
			if (parameters.Length > types.Count)
			{
				return false;
			}
			Type type = null;
			if (enableParamArray)
			{
				ParameterInfo parameterInfo = parameters[parameters.Length - 1];
				if (parameterInfo.ParameterType.IsArray && CustomAttributeExtensions.IsDefined(parameterInfo, typeof(ParamArrayAttribute)))
				{
					type = parameterInfo.ParameterType.GetElementType();
				}
			}
			if (type == null && parameters.Length != types.Count)
			{
				return false;
			}
			for (int i = 0; i < types.Count; i++)
			{
				Type type2 = (type != null && i >= parameters.Length - 1) ? type : parameters[i].ParameterType;
				if (type2 != types[i] && type2 != typeof(object))
				{
					if (type2.IsPrimitive())
					{
						if (!types[i].IsPrimitive() || !MethodBinder.CanConvertPrimitive(types[i], type2))
						{
							return false;
						}
					}
					else if (!type2.IsAssignableFrom(types[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00016B50 File Offset: 0x00014D50
		public static TMethod SelectMethod<[Nullable(0)] TMethod>(IEnumerable<TMethod> candidates, IList<Type> types) where TMethod : MethodBase
		{
			ValidationUtils.ArgumentNotNull(candidates, "candidates");
			ValidationUtils.ArgumentNotNull(types, "types");
			return Enumerable.FirstOrDefault<TMethod>(Enumerable.OrderBy<TMethod, ParameterInfo[]>(Enumerable.Where<TMethod>(candidates, (TMethod m) => MethodBinder.FilterParameters(m.GetParameters(), types, false)), (TMethod m) => m.GetParameters(), new MethodBinder.ParametersMatchComparer(types, false)));
		}

		// Token: 0x04000239 RID: 569
		private static readonly Type[] PrimitiveTypes = new Type[]
		{
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double)
		};

		// Token: 0x0400023A RID: 570
		private static readonly int[] WideningMasks = new int[]
		{
			1,
			4066,
			3412,
			4090,
			3408,
			4066,
			3392,
			3968,
			3328,
			3584,
			3072,
			2048
		};

		// Token: 0x02000190 RID: 400
		[Nullable(0)]
		private class ParametersMatchComparer : IComparer<ParameterInfo[]>
		{
			// Token: 0x06000E7E RID: 3710 RVA: 0x0003FF76 File Offset: 0x0003E176
			public ParametersMatchComparer(IList<Type> types, bool enableParamArray)
			{
				ValidationUtils.ArgumentNotNull(types, "types");
				this._types = types;
				this._enableParamArray = enableParamArray;
			}

			// Token: 0x06000E7F RID: 3711 RVA: 0x0003FF98 File Offset: 0x0003E198
			public int Compare(ParameterInfo[] parameters1, ParameterInfo[] parameters2)
			{
				ValidationUtils.ArgumentNotNull(parameters1, "parameters1");
				ValidationUtils.ArgumentNotNull(parameters2, "parameters2");
				if (parameters1.Length == 0)
				{
					return -1;
				}
				if (parameters2.Length == 0)
				{
					return 1;
				}
				Type type = null;
				Type type2 = null;
				if (this._enableParamArray)
				{
					ParameterInfo parameterInfo = parameters1[parameters1.Length - 1];
					if (parameterInfo.ParameterType.IsArray && CustomAttributeExtensions.IsDefined(parameterInfo, typeof(ParamArrayAttribute)))
					{
						type = parameterInfo.ParameterType.GetElementType();
					}
					ParameterInfo parameterInfo2 = parameters2[parameters2.Length - 1];
					if (parameterInfo2.ParameterType.IsArray && CustomAttributeExtensions.IsDefined(parameterInfo2, typeof(ParamArrayAttribute)))
					{
						type2 = parameterInfo2.ParameterType.GetElementType();
					}
					if (type != null && type2 == null)
					{
						return 1;
					}
					if (type2 != null && type == null)
					{
						return -1;
					}
				}
				for (int i = 0; i < this._types.Count; i++)
				{
					Type type3 = (type != null && i >= parameters1.Length - 1) ? type : parameters1[i].ParameterType;
					Type type4 = (type2 != null && i >= parameters2.Length - 1) ? type2 : parameters2[i].ParameterType;
					if (type3 != type4)
					{
						if (type3 == this._types[i])
						{
							return -1;
						}
						if (type4 == this._types[i])
						{
							return 1;
						}
						int num = MethodBinder.ParametersMatchComparer.ChooseMorePreciseType(type3, type4);
						if (num != 0)
						{
							return num;
						}
					}
				}
				return 0;
			}

			// Token: 0x06000E80 RID: 3712 RVA: 0x000400DC File Offset: 0x0003E2DC
			private static int ChooseMorePreciseType(Type type1, Type type2)
			{
				if (type1.IsByRef || type2.IsByRef)
				{
					if (type1.IsByRef && type2.IsByRef)
					{
						type1 = type1.GetElementType();
						type2 = type2.GetElementType();
					}
					else if (type1.IsByRef)
					{
						type1 = type1.GetElementType();
						if (type1 == type2)
						{
							return 1;
						}
					}
					else
					{
						type2 = type2.GetElementType();
						if (type2 == type1)
						{
							return -1;
						}
					}
				}
				bool flag;
				bool flag2;
				if (type1.IsPrimitive() && type2.IsPrimitive())
				{
					flag = MethodBinder.CanConvertPrimitive(type2, type1);
					flag2 = MethodBinder.CanConvertPrimitive(type1, type2);
				}
				else
				{
					flag = type1.IsAssignableFrom(type2);
					flag2 = type2.IsAssignableFrom(type1);
				}
				if (flag == flag2)
				{
					return 0;
				}
				if (!flag)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x0400072C RID: 1836
			private readonly IList<Type> _types;

			// Token: 0x0400072D RID: 1837
			private readonly bool _enableParamArray;
		}
	}
}

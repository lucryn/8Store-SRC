using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000064 RID: 100
	[NullableContext(1)]
	[Nullable(0)]
	internal class FSharpUtils
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x00015590 File Offset: 0x00013790
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private FSharpUtils(Assembly fsharpCoreAssembly)
		{
			this.FSharpCoreAssembly = fsharpCoreAssembly;
			Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpType");
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, "IsUnion", BindingFlags.Static | BindingFlags.Public);
			this.IsUnion = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodInfo methodWithNonPublicFallback2 = FSharpUtils.GetMethodWithNonPublicFallback(type, "GetUnionCases", BindingFlags.Static | BindingFlags.Public);
			this.GetUnionCases = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
			Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpValue");
			this.PreComputeUnionTagReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionTagReader");
			this.PreComputeUnionReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionReader");
			this.PreComputeUnionConstructor = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionConstructor");
			Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.UnionCaseInfo");
			this.GetUnionCaseInfoName = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Name"));
			this.GetUnionCaseInfoTag = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Tag"));
			this.GetUnionCaseInfoDeclaringType = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("DeclaringType"));
			this.GetUnionCaseInfoFields = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod("GetFields"));
			Type type4 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule");
			this._ofSeq = type4.GetMethod("OfSeq");
			this._mapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000156D9 File Offset: 0x000138D9
		public static FSharpUtils Instance
		{
			get
			{
				return FSharpUtils._instance;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000156E0 File Offset: 0x000138E0
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x000156E8 File Offset: 0x000138E8
		public Assembly FSharpCoreAssembly { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000156F1 File Offset: 0x000138F1
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x000156F9 File Offset: 0x000138F9
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> IsUnion { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00015702 File Offset: 0x00013902
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0001570A File Offset: 0x0001390A
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> GetUnionCases { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00015713 File Offset: 0x00013913
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001571B File Offset: 0x0001391B
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionTagReader { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00015724 File Offset: 0x00013924
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0001572C File Offset: 0x0001392C
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionReader { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00015735 File Offset: 0x00013935
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001573D File Offset: 0x0001393D
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionConstructor { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00015746 File Offset: 0x00013946
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0001574E File Offset: 0x0001394E
		public Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00015757 File Offset: 0x00013957
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001575F File Offset: 0x0001395F
		public Func<object, object> GetUnionCaseInfoName { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00015768 File Offset: 0x00013968
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00015770 File Offset: 0x00013970
		public Func<object, object> GetUnionCaseInfoTag { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00015779 File Offset: 0x00013979
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00015781 File Offset: 0x00013981
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public MethodCall<object, object> GetUnionCaseInfoFields { [return: Nullable(new byte[]
		{
			1,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			1,
			1,
			2
		})] private set; }

		// Token: 0x06000539 RID: 1337 RVA: 0x0001578C File Offset: 0x0001398C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static void EnsureInitialized(Assembly fsharpCoreAssembly)
		{
			if (FSharpUtils._instance == null)
			{
				object @lock = FSharpUtils.Lock;
				lock (@lock)
				{
					if (FSharpUtils._instance == null)
					{
						FSharpUtils._instance = new FSharpUtils(fsharpCoreAssembly);
					}
				}
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000157E0 File Offset: 0x000139E0
		private static MethodInfo GetMethodWithNonPublicFallback([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo method = type.GetMethod(methodName, bindingFlags);
			if (method == null && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.NonPublic)
			{
				method = type.GetMethod(methodName, bindingFlags | BindingFlags.NonPublic);
			}
			return method;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00015810 File Offset: 0x00013A10
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return ([Nullable(2)] object target, [Nullable(new byte[]
			{
				1,
				2
			})] object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001586C File Offset: 0x00013A6C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo method = this._ofSeq.MakeGenericMethod(new Type[]
			{
				t
			});
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001589A File Offset: 0x00013A9A
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(new Type[]
			{
				keyType,
				valueType
			}).Invoke(this, null);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000158D0 File Offset: 0x00013AD0
		[NullableContext(2)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(1)]
		public ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = this._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[]
			{
				typeof(IEnumerable<Tuple<TKey, TValue>>)
			});
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate([Nullable(new byte[]
			{
				1,
				2
			})] object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = Enumerable.Select<KeyValuePair<TKey, TValue>, Tuple<TKey, TValue>>((IEnumerable<KeyValuePair<TKey, TValue>>)args[0], (KeyValuePair<TKey, TValue> kv) => new Tuple<TKey, TValue>(kv.Key, kv.Value));
				return ctorDelegate(new object[]
				{
					enumerable
				});
			};
		}

		// Token: 0x0400020B RID: 523
		private static readonly object Lock = new object();

		// Token: 0x0400020C RID: 524
		[Nullable(2)]
		private static FSharpUtils _instance;

		// Token: 0x0400020D RID: 525
		private MethodInfo _ofSeq;

		// Token: 0x0400020E RID: 526
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
		private Type _mapType;

		// Token: 0x04000219 RID: 537
		public const string FSharpSetTypeName = "FSharpSet`1";

		// Token: 0x0400021A RID: 538
		public const string FSharpListTypeName = "FSharpList`1";

		// Token: 0x0400021B RID: 539
		public const string FSharpMapTypeName = "FSharpMap`2";
	}
}

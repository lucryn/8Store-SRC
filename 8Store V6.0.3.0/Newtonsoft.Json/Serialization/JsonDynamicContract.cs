using System;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000096 RID: 150
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonDynamicContract : JsonContainerContract
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001C65A File Offset: 0x0001A85A
		public JsonPropertyCollection Properties { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001C662 File Offset: 0x0001A862
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001C66A File Offset: 0x0001A86A
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Func<string, string> PropertyNameResolver { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			1
		})] set; }

		// Token: 0x060006FA RID: 1786 RVA: 0x0001C673 File Offset: 0x0001A873
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private static CallSite<Func<CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			return CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember((GetMemberBinder)DynamicUtils.BinderWrapper.GetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001C694 File Offset: 0x0001A894
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			2,
			1
		})]
		private static CallSite<Func<CallSite, object, object, object>> CreateCallSiteSetter(string name)
		{
			return CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember((SetMemberBinder)DynamicUtils.BinderWrapper.SetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public JsonDynamicContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Dynamic;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001C714 File Offset: 0x0001A914
		internal bool TryGetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] out object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object>> callSite = this._callSiteGetters.Get(name);
			object obj = callSite.Target.Invoke(callSite, dynamicProvider);
			if (obj != NoThrowExpressionVisitor.ErrorResult)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001C758 File Offset: 0x0001A958
		internal bool TrySetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object, object>> callSite = this._callSiteSetters.Get(name);
			return callSite.Target.Invoke(callSite, dynamicProvider, value) != NoThrowExpressionVisitor.ErrorResult;
		}

		// Token: 0x040002CA RID: 714
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>> _callSiteGetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object>>>(JsonDynamicContract.CreateCallSiteGetter));

		// Token: 0x040002CB RID: 715
		[Nullable(new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			1
		})]
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>> _callSiteSetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object, object>>>(JsonDynamicContract.CreateCallSiteSetter));
	}
}

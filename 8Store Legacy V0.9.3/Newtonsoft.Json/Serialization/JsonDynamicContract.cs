using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000097 RID: 151
	public class JsonDynamicContract : JsonContainerContract
	{
		/// <summary>
		/// Gets the object's properties.
		/// </summary>
		/// <value>The object's properties.</value>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001BC08 File Offset: 0x00019E08
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0001BC10 File Offset: 0x00019E10
		public JsonPropertyCollection Properties { get; private set; }

		/// <summary>
		/// Gets or sets the property name resolver.
		/// </summary>
		/// <value>The property name resolver.</value>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001BC19 File Offset: 0x00019E19
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0001BC21 File Offset: 0x00019E21
		public Func<string, string> PropertyNameResolver { get; set; }

		// Token: 0x06000788 RID: 1928 RVA: 0x0001BC2C File Offset: 0x00019E2C
		private static CallSite<Func<CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			GetMemberBinder innerBinder = (GetMemberBinder)DynamicUtils.BinderWrapper.GetMember(name, typeof(DynamicUtils));
			return CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember(innerBinder));
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001BC5C File Offset: 0x00019E5C
		private static CallSite<Func<CallSite, object, object, object>> CreateCallSiteSetter(string name)
		{
			SetMemberBinder innerBinder = (SetMemberBinder)DynamicUtils.BinderWrapper.SetMember(name, typeof(DynamicUtils));
			return CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember(innerBinder));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonDynamicContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x0600078A RID: 1930 RVA: 0x0001BC8C File Offset: 0x00019E8C
		public JsonDynamicContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Dynamic;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001BCE8 File Offset: 0x00019EE8
		internal bool TryGetMember(IDynamicMetaObjectProvider dynamicProvider, string name, out object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object>> callSite = this._callSiteGetters.Get(name);
			object obj = callSite.Target.Invoke(callSite, dynamicProvider);
			if (!object.ReferenceEquals(obj, NoThrowExpressionVisitor.ErrorResult))
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001BD34 File Offset: 0x00019F34
		internal bool TrySetMember(IDynamicMetaObjectProvider dynamicProvider, string name, object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object, object>> callSite = this._callSiteSetters.Get(name);
			object obj = callSite.Target.Invoke(callSite, dynamicProvider, value);
			return !object.ReferenceEquals(obj, NoThrowExpressionVisitor.ErrorResult);
		}

		// Token: 0x040002C3 RID: 707
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>> _callSiteGetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object>>>(JsonDynamicContract.CreateCallSiteGetter));

		// Token: 0x040002C4 RID: 708
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>> _callSiteSetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object, object>>>(JsonDynamicContract.CreateCallSiteSetter));
	}
}

using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000085 RID: 133
	internal struct ResolverContractKey : IEquatable<ResolverContractKey>
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0001A805 File Offset: 0x00018A05
		public ResolverContractKey(Type resolverType, Type contractType)
		{
			this._resolverType = resolverType;
			this._contractType = contractType;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001A815 File Offset: 0x00018A15
		public override int GetHashCode()
		{
			return this._resolverType.GetHashCode() ^ this._contractType.GetHashCode();
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001A82E File Offset: 0x00018A2E
		public override bool Equals(object obj)
		{
			return obj is ResolverContractKey && this.Equals((ResolverContractKey)obj);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001A846 File Offset: 0x00018A46
		public bool Equals(ResolverContractKey other)
		{
			return this._resolverType == other._resolverType && this._contractType == other._contractType;
		}

		// Token: 0x04000278 RID: 632
		private readonly Type _resolverType;

		// Token: 0x04000279 RID: 633
		private readonly Type _contractType;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A4 RID: 164
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class NamingStrategy
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00022D64 File Offset: 0x00020F64
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00022D6C File Offset: 0x00020F6C
		public bool ProcessDictionaryKeys { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00022D75 File Offset: 0x00020F75
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00022D7D File Offset: 0x00020F7D
		public bool ProcessExtensionDataNames { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00022D86 File Offset: 0x00020F86
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00022D8E File Offset: 0x00020F8E
		public bool OverrideSpecifiedNames { get; set; }

		// Token: 0x06000824 RID: 2084 RVA: 0x00022D97 File Offset: 0x00020F97
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00022DAD File Offset: 0x00020FAD
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00022DC0 File Offset: 0x00020FC0
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x06000827 RID: 2087
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x06000828 RID: 2088 RVA: 0x00022DD4 File Offset: 0x00020FD4
		public override int GetHashCode()
		{
			return ((base.GetType().GetHashCode() * 397 ^ this.ProcessDictionaryKeys.GetHashCode()) * 397 ^ this.ProcessExtensionDataNames.GetHashCode()) * 397 ^ this.OverrideSpecifiedNames.GetHashCode();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00022E2B File Offset: 0x0002102B
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamingStrategy);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00022E3C File Offset: 0x0002103C
		[NullableContext(2)]
		protected bool Equals(NamingStrategy other)
		{
			return other != null && (base.GetType() == other.GetType() && this.ProcessDictionaryKeys == other.ProcessDictionaryKeys && this.ProcessExtensionDataNames == other.ProcessExtensionDataNames) && this.OverrideSpecifiedNames == other.OverrideSpecifiedNames;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007E RID: 126
	public class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x000194C6 File Offset: 0x000176C6
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000194DC File Offset: 0x000176DC
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000194ED File Offset: 0x000176ED
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000194F5 File Offset: 0x000176F5
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}

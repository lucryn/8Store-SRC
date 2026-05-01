using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000AA RID: 170
	public class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x00022FF6 File Offset: 0x000211F6
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002300C File Offset: 0x0002120C
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002301D File Offset: 0x0002121D
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00023025 File Offset: 0x00021225
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}

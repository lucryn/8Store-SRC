using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A2 RID: 162
	public class KebabCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000814 RID: 2068 RVA: 0x00022B8B File Offset: 0x00020D8B
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00022BA1 File Offset: 0x00020DA1
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames) : this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00022BB2 File Offset: 0x00020DB2
		public KebabCaseNamingStrategy()
		{
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00022BBA File Offset: 0x00020DBA
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToKebabCase(name);
		}
	}
}

using System;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C8 RID: 200
	internal class EnumValues<T> : KeyedCollection<string, EnumValue<T>> where T : struct
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x00025E4A File Offset: 0x0002404A
		protected override string GetKeyForItem(EnumValue<T> item)
		{
			return item.Name;
		}
	}
}

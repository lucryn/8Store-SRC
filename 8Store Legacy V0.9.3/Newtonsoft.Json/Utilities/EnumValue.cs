using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C7 RID: 199
	internal class EnumValue<T> where T : struct
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00025E24 File Offset: 0x00024024
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00025E2C File Offset: 0x0002402C
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00025E34 File Offset: 0x00024034
		public EnumValue(string name, T value)
		{
			this._name = name;
			this._value = value;
		}

		// Token: 0x040003AE RID: 942
		private readonly string _name;

		// Token: 0x040003AF RID: 943
		private readonly T _value;
	}
}

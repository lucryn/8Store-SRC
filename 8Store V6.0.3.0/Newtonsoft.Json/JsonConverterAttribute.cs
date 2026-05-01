using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(3484, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002F59 File Offset: 0x00001159
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002F61 File Offset: 0x00001161
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] ConverterParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; }

		// Token: 0x060000A0 RID: 160 RVA: 0x00002F69 File Offset: 0x00001169
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002F86 File Offset: 0x00001186
		public JsonConverterAttribute(Type converterType, params object[] converterParameters) : this(converterType)
		{
			this.ConverterParameters = converterParameters;
		}

		// Token: 0x0400005D RID: 93
		private readonly Type _converterType;
	}
}

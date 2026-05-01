using System;
using System.Globalization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D9 RID: 217
	internal static class ValidationUtils
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x00029449 File Offset: 0x00027649
		public static void ArgumentNotNullOrEmpty(string value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("'{0}' cannot be empty.".FormatWith(CultureInfo.InvariantCulture, parameterName), parameterName);
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00029474 File Offset: 0x00027674
		public static void ArgumentTypeIsEnum(Type enumType, string parameterName)
		{
			ValidationUtils.ArgumentNotNull(enumType, "enumType");
			if (!enumType.IsEnum())
			{
				throw new ArgumentException("Type {0} is not an Enum.".FormatWith(CultureInfo.InvariantCulture, enumType), parameterName);
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000294A0 File Offset: 0x000276A0
		public static void ArgumentNotNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}
	}
}

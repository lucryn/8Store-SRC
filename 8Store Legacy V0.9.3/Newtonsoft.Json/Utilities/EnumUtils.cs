using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C6 RID: 198
	internal static class EnumUtils
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x00025AB0 File Offset: 0x00023CB0
		public static IList<T> GetFlagsValues<T>(T value) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("Enum type {0} is not a set of flags.".FormatWith(CultureInfo.InvariantCulture, typeFromHandle));
			}
			Type underlyingType = Enum.GetUnderlyingType(value.GetType());
			ulong num = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
			EnumValues<ulong> namesAndValues = EnumUtils.GetNamesAndValues<T>();
			IList<T> list = new List<T>();
			foreach (EnumValue<ulong> enumValue in namesAndValues)
			{
				if ((num & enumValue.Value) == enumValue.Value && enumValue.Value != 0UL)
				{
					list.Add((T)((object)Convert.ChangeType(enumValue.Value, underlyingType, CultureInfo.CurrentCulture)));
				}
			}
			if (list.Count == 0 && Enumerable.SingleOrDefault<EnumValue<ulong>>(namesAndValues, (EnumValue<ulong> v) => v.Value == 0UL) != null)
			{
				list.Add(default(T));
			}
			return list;
		}

		/// <summary>
		/// Gets a dictionary of the names and values of an Enum type.
		/// </summary>
		/// <returns></returns>
		// Token: 0x0600099C RID: 2460 RVA: 0x00025BCC File Offset: 0x00023DCC
		public static EnumValues<ulong> GetNamesAndValues<T>() where T : struct
		{
			return EnumUtils.GetNamesAndValues<ulong>(typeof(T));
		}

		/// <summary>
		/// Gets a dictionary of the names and values of an Enum type.
		/// </summary>
		/// <param name="enumType">The enum type to get names and values for.</param>
		/// <returns></returns>
		// Token: 0x0600099D RID: 2461 RVA: 0x00025BE0 File Offset: 0x00023DE0
		public static EnumValues<TUnderlyingType> GetNamesAndValues<TUnderlyingType>(Type enumType) where TUnderlyingType : struct
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			ValidationUtils.ArgumentTypeIsEnum(enumType, "enumType");
			IList<object> values = EnumUtils.GetValues(enumType);
			IList<string> names = EnumUtils.GetNames(enumType);
			EnumValues<TUnderlyingType> enumValues = new EnumValues<TUnderlyingType>();
			for (int i = 0; i < values.Count; i++)
			{
				try
				{
					enumValues.Add(new EnumValue<TUnderlyingType>(names[i], (TUnderlyingType)((object)Convert.ChangeType(values[i], typeof(TUnderlyingType), CultureInfo.CurrentCulture))));
				}
				catch (OverflowException ex)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Value from enum with the underlying type of {0} cannot be added to dictionary with a value type of {1}. Value was too large: {2}", new object[]
					{
						Enum.GetUnderlyingType(enumType),
						typeof(TUnderlyingType),
						Convert.ToUInt64(values[i], CultureInfo.InvariantCulture)
					}), ex);
				}
			}
			return enumValues;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00025CD4 File Offset: 0x00023ED4
		public static IList<object> GetValues(Type enumType)
		{
			if (!enumType.IsEnum())
			{
				throw new ArgumentException("Type '" + enumType.Name + "' is not an enum.");
			}
			List<object> list = new List<object>();
			IEnumerable<FieldInfo> enumerable = Enumerable.Where<FieldInfo>(enumType.GetFields(), (FieldInfo field) => field.IsLiteral);
			foreach (FieldInfo fieldInfo in enumerable)
			{
				object value = fieldInfo.GetValue(enumType);
				list.Add(value);
			}
			return list;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00025D84 File Offset: 0x00023F84
		public static IList<string> GetNames(Type enumType)
		{
			if (!enumType.IsEnum())
			{
				throw new ArgumentException("Type '" + enumType.Name + "' is not an enum.");
			}
			List<string> list = new List<string>();
			IEnumerable<FieldInfo> enumerable = Enumerable.Where<FieldInfo>(enumType.GetFields(), (FieldInfo field) => field.IsLiteral);
			foreach (FieldInfo fieldInfo in enumerable)
			{
				list.Add(fieldInfo.Name);
			}
			return list;
		}
	}
}

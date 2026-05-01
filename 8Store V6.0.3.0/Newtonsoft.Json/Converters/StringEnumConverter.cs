using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F0 RID: 240
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
	[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
	public class StringEnumConverter : JsonConverter
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00030666 File Offset: 0x0002E866
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x00030676 File Offset: 0x0002E876
		[Obsolete("StringEnumConverter.CamelCaseText is obsolete. Set StringEnumConverter.NamingStrategy with CamelCaseNamingStrategy instead.")]
		public bool CamelCaseText
		{
			get
			{
				return this.NamingStrategy is CamelCaseNamingStrategy;
			}
			set
			{
				if (value)
				{
					if (this.NamingStrategy is CamelCaseNamingStrategy)
					{
						return;
					}
					this.NamingStrategy = new CamelCaseNamingStrategy();
					return;
				}
				else
				{
					if (!(this.NamingStrategy is CamelCaseNamingStrategy))
					{
						return;
					}
					this.NamingStrategy = null;
					return;
				}
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000306AA File Offset: 0x0002E8AA
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x000306B2 File Offset: 0x0002E8B2
		[Nullable(2)]
		public NamingStrategy NamingStrategy { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x000306BB File Offset: 0x0002E8BB
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x000306C3 File Offset: 0x0002E8C3
		public bool AllowIntegerValues { get; set; } = true;

		// Token: 0x06000C2F RID: 3119 RVA: 0x000306CC File Offset: 0x0002E8CC
		public StringEnumConverter()
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000306DB File Offset: 0x0002E8DB
		[Obsolete("StringEnumConverter(bool) is obsolete. Create a converter with StringEnumConverter(NamingStrategy, bool) instead.")]
		public StringEnumConverter(bool camelCaseText)
		{
			if (camelCaseText)
			{
				this.NamingStrategy = new CamelCaseNamingStrategy();
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000306F8 File Offset: 0x0002E8F8
		public StringEnumConverter(NamingStrategy namingStrategy, bool allowIntegerValues = true)
		{
			this.NamingStrategy = namingStrategy;
			this.AllowIntegerValues = allowIntegerValues;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00030715 File Offset: 0x0002E915
		public StringEnumConverter(Type namingStrategyType)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, null);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0003073C File Offset: 0x0002E93C
		public StringEnumConverter(Type namingStrategyType, object[] namingStrategyParameters)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00030763 File Offset: 0x0002E963
		public StringEnumConverter(Type namingStrategyType, object[] namingStrategyParameters, bool allowIntegerValues)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
			this.AllowIntegerValues = allowIntegerValues;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00030794 File Offset: 0x0002E994
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string value2;
			if (EnumUtils.TryToString(@enum.GetType(), value, this.NamingStrategy, out value2))
			{
				writer.WriteValue(value2);
				return;
			}
			if (!this.AllowIntegerValues)
			{
				throw JsonSerializationException.Create(null, writer.ContainerPath, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, @enum.ToString("D")), null);
			}
			writer.WriteValue(value);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00030808 File Offset: 0x0002EA08
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Null)
			{
				bool flag = ReflectionUtils.IsNullableType(objectType);
				Type type = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
				try
				{
					if (reader.TokenType == JsonToken.String)
					{
						object value = reader.Value;
						string value2 = (value != null) ? value.ToString() : null;
						if (StringUtils.IsNullOrEmpty(value2) && flag)
						{
							return null;
						}
						return EnumUtils.ParseEnum(type, this.NamingStrategy, value2, !this.AllowIntegerValues);
					}
					else if (reader.TokenType == JsonToken.Integer)
					{
						if (!this.AllowIntegerValues)
						{
							throw JsonSerializationException.Create(reader, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, reader.Value));
						}
						return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
					}
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(reader.Value), objectType), ex);
				}
				throw JsonSerializationException.Create(reader, "Unexpected token {0} when parsing enum.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			if (!ReflectionUtils.IsNullableType(objectType))
			{
				throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			return null;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0003093C File Offset: 0x0002EB3C
		public override bool CanConvert(Type objectType)
		{
			return (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType).IsEnum();
		}
	}
}

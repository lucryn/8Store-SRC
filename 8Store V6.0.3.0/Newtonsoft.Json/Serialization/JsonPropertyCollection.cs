using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009B RID: 155
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	public class JsonPropertyCollection : KeyedCollection<string, JsonProperty>
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x0001CDC8 File Offset: 0x0001AFC8
		public JsonPropertyCollection(Type type) : base(StringComparer.Ordinal)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			this._type = type;
			this._list = (List<JsonProperty>)base.Items;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		protected override string GetKeyForItem(JsonProperty item)
		{
			return item.PropertyName;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001CE00 File Offset: 0x0001B000
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		public void AddProperty(JsonProperty property)
		{
			if (base.Contains(property.PropertyName))
			{
				if (property.Ignored)
				{
					return;
				}
				JsonProperty jsonProperty = base[property.PropertyName];
				bool flag = true;
				if (jsonProperty.Ignored)
				{
					base.Remove(jsonProperty);
					flag = false;
				}
				else if (property.DeclaringType != null && jsonProperty.DeclaringType != null)
				{
					if (property.DeclaringType.IsSubclassOf(jsonProperty.DeclaringType) || (jsonProperty.DeclaringType.IsInterface() && property.DeclaringType.ImplementInterface(jsonProperty.DeclaringType)))
					{
						base.Remove(jsonProperty);
						flag = false;
					}
					if (jsonProperty.DeclaringType.IsSubclassOf(property.DeclaringType) || (property.DeclaringType.IsInterface() && jsonProperty.DeclaringType.ImplementInterface(property.DeclaringType)))
					{
						return;
					}
					if (this._type.ImplementInterface(jsonProperty.DeclaringType) && this._type.ImplementInterface(property.DeclaringType))
					{
						return;
					}
				}
				if (flag)
				{
					throw new JsonSerializationException("A member with the name '{0}' already exists on '{1}'. Use the JsonPropertyAttribute to specify another name.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, this._type));
				}
			}
			base.Add(property);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001CF28 File Offset: 0x0001B128
		[return: Nullable(2)]
		public JsonProperty GetClosestMatchProperty(string propertyName)
		{
			JsonProperty property = this.GetProperty(propertyName, 4);
			if (property == null)
			{
				property = this.GetProperty(propertyName, 5);
			}
			return property;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001CF4B File Offset: 0x0001B14B
		private bool TryGetProperty(string key, [Nullable(2)] [NotNullWhen(true)] out JsonProperty item)
		{
			if (base.Dictionary == null)
			{
				item = null;
				return false;
			}
			return base.Dictionary.TryGetValue(key, ref item);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001CF68 File Offset: 0x0001B168
		[return: Nullable(2)]
		public JsonProperty GetProperty(string propertyName, StringComparison comparisonType)
		{
			if (comparisonType != 4)
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					JsonProperty jsonProperty = this._list[i];
					if (string.Equals(propertyName, jsonProperty.PropertyName, comparisonType))
					{
						return jsonProperty;
					}
				}
				return null;
			}
			JsonProperty result;
			if (this.TryGetProperty(propertyName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x040002FC RID: 764
		private readonly Type _type;

		// Token: 0x040002FD RID: 765
		private readonly List<JsonProperty> _list;
	}
}

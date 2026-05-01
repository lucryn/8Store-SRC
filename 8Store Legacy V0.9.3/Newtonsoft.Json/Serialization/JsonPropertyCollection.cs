using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// A collection of <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> objects.
	/// </summary>
	// Token: 0x0200009C RID: 156
	public class JsonPropertyCollection : KeyedCollection<string, JsonProperty>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonPropertyCollection" /> class.
		/// </summary>
		/// <param name="type">The type.</param>
		// Token: 0x060007DE RID: 2014 RVA: 0x0001C21D File Offset: 0x0001A41D
		public JsonPropertyCollection(Type type) : base(StringComparer.Ordinal)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			this._type = type;
		}

		/// <summary>
		/// When implemented in a derived class, extracts the key from the specified element.
		/// </summary>
		/// <param name="item">The element from which to extract the key.</param>
		/// <returns>The key for the specified element.</returns>
		// Token: 0x060007DF RID: 2015 RVA: 0x0001C23C File Offset: 0x0001A43C
		protected override string GetKeyForItem(JsonProperty item)
		{
			return item.PropertyName;
		}

		/// <summary>
		/// Adds a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> object.
		/// </summary>
		/// <param name="property">The property to add to the collection.</param>
		// Token: 0x060007E0 RID: 2016 RVA: 0x0001C244 File Offset: 0x0001A444
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
					if (property.DeclaringType.IsSubclassOf(jsonProperty.DeclaringType))
					{
						base.Remove(jsonProperty);
						flag = false;
					}
					if (jsonProperty.DeclaringType.IsSubclassOf(property.DeclaringType))
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

		/// <summary>
		/// Gets the closest matching <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> object.
		/// First attempts to get an exact case match of propertyName and then
		/// a case insensitive match.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>A matching property if found.</returns>
		// Token: 0x060007E1 RID: 2017 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public JsonProperty GetClosestMatchProperty(string propertyName)
		{
			JsonProperty property = this.GetProperty(propertyName, 4);
			if (property == null)
			{
				property = this.GetProperty(propertyName, 5);
			}
			return property;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001C31F File Offset: 0x0001A51F
		private bool TryGetValue(string key, out JsonProperty item)
		{
			if (base.Dictionary == null)
			{
				item = null;
				return false;
			}
			return base.Dictionary.TryGetValue(key, ref item);
		}

		/// <summary>
		/// Gets a property by property name.
		/// </summary>
		/// <param name="propertyName">The name of the property to get.</param>
		/// <param name="comparisonType">Type property name string comparison.</param>
		/// <returns>A matching property if found.</returns>
		// Token: 0x060007E3 RID: 2019 RVA: 0x0001C33C File Offset: 0x0001A53C
		public JsonProperty GetProperty(string propertyName, StringComparison comparisonType)
		{
			if (comparisonType != 4)
			{
				foreach (JsonProperty jsonProperty in this)
				{
					if (string.Equals(propertyName, jsonProperty.PropertyName, comparisonType))
					{
						return jsonProperty;
					}
				}
				return null;
			}
			JsonProperty result;
			if (this.TryGetValue(propertyName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x040002EE RID: 750
		private readonly Type _type;
	}
}

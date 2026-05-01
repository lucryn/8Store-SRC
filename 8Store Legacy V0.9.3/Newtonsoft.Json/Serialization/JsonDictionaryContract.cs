using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000096 RID: 150
	public class JsonDictionaryContract : JsonContainerContract
	{
		/// <summary>
		/// Gets or sets the property name resolver.
		/// </summary>
		/// <value>The property name resolver.</value>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001B8E1 File Offset: 0x00019AE1
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x0001B8E9 File Offset: 0x00019AE9
		public Func<string, string> PropertyNameResolver { get; set; }

		/// <summary>
		/// Gets the <see cref="T:System.Type" /> of the dictionary keys.
		/// </summary>
		/// <value>The <see cref="T:System.Type" /> of the dictionary keys.</value>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001B8F2 File Offset: 0x00019AF2
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001B8FA File Offset: 0x00019AFA
		public Type DictionaryKeyType { get; private set; }

		/// <summary>
		/// Gets the <see cref="T:System.Type" /> of the dictionary values.
		/// </summary>
		/// <value>The <see cref="T:System.Type" /> of the dictionary values.</value>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001B903 File Offset: 0x00019B03
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0001B90B File Offset: 0x00019B0B
		public Type DictionaryValueType { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001B914 File Offset: 0x00019B14
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001B91C File Offset: 0x00019B1C
		internal JsonContract KeyContract { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001B925 File Offset: 0x00019B25
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0001B92D File Offset: 0x00019B2D
		internal bool ShouldCreateWrapper { get; private set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001B936 File Offset: 0x00019B36
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0001B93E File Offset: 0x00019B3E
		internal ConstructorInfo ParametrizedConstructor { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonDictionaryContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x06000781 RID: 1921 RVA: 0x0001B948 File Offset: 0x00019B48
		public JsonDictionaryContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Dictionary;
			Type type;
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IDictionary), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IDictionary)))
				{
					base.CreatedType = typeof(Dictionary).MakeGenericType(new Type[]
					{
						type,
						type2
					});
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(underlyingType, typeof(ReadOnlyDictionary));
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IReadOnlyDictionary), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IReadOnlyDictionary)))
				{
					base.CreatedType = typeof(ReadOnlyDictionary).MakeGenericType(new Type[]
					{
						type,
						type2
					});
				}
				this.IsReadOnlyOrFixedSize = true;
			}
			else
			{
				ReflectionUtils.GetDictionaryKeyValueTypes(base.UnderlyingType, out type, out type2);
				if (base.UnderlyingType == typeof(IDictionary))
				{
					base.CreatedType = typeof(Dictionary<object, object>);
				}
			}
			if (type != null && type2 != null)
			{
				this.ParametrizedConstructor = CollectionUtils.ResolveEnumableCollectionConstructor(base.CreatedType, typeof(KeyValuePair).MakeGenericType(new Type[]
				{
					type,
					type2
				}));
			}
			this.ShouldCreateWrapper = !typeof(IDictionary).IsAssignableFrom(base.CreatedType);
			this.DictionaryKeyType = type;
			this.DictionaryValueType = type2;
			if (this.DictionaryValueType != null)
			{
				this._isDictionaryValueTypeNullableType = ReflectionUtils.IsNullableType(this.DictionaryValueType);
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001BB18 File Offset: 0x00019D18
		internal IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(DictionaryWrapper<, >).MakeGenericType(new Type[]
				{
					this.DictionaryKeyType,
					this.DictionaryValueType
				});
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[]
				{
					this._genericCollectionDefinitionType
				});
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(constructor);
			}
			return (IWrappedDictionary)this._genericWrapperCreator(null, new object[]
			{
				dictionary
			});
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		internal IDictionary CreateTemporaryDictionary()
		{
			if (this._genericTemporaryDictionaryCreator == null)
			{
				Type type = typeof(Dictionary).MakeGenericType(new Type[]
				{
					this.DictionaryKeyType,
					this.DictionaryValueType
				});
				this._genericTemporaryDictionaryCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type);
			}
			return (IDictionary)this._genericTemporaryDictionaryCreator.Invoke();
		}

		// Token: 0x040002B8 RID: 696
		private readonly bool _isDictionaryValueTypeNullableType;

		// Token: 0x040002B9 RID: 697
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x040002BA RID: 698
		private Type _genericWrapperType;

		// Token: 0x040002BB RID: 699
		private MethodCall<object, object> _genericWrapperCreator;

		// Token: 0x040002BC RID: 700
		private Func<object> _genericTemporaryDictionaryCreator;
	}
}

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
	// Token: 0x02000091 RID: 145
	public class JsonArrayContract : JsonContainerContract
	{
		/// <summary>
		/// Gets the <see cref="T:System.Type" /> of the collection items.
		/// </summary>
		/// <value>The <see cref="T:System.Type" /> of the collection items.</value>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001B34F File Offset: 0x0001954F
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0001B357 File Offset: 0x00019557
		public Type CollectionItemType { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the collection type is a multidimensional array.
		/// </summary>
		/// <value><c>true</c> if the collection type is a multidimensional array; otherwise, <c>false</c>.</value>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001B360 File Offset: 0x00019560
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0001B368 File Offset: 0x00019568
		public bool IsMultidimensionalArray { get; private set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001B371 File Offset: 0x00019571
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0001B379 File Offset: 0x00019579
		internal bool ShouldCreateWrapper { get; private set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001B382 File Offset: 0x00019582
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x0001B38A File Offset: 0x0001958A
		internal bool CanDeserialize { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001B393 File Offset: 0x00019593
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x0001B39B File Offset: 0x0001959B
		internal ConstructorInfo ParametrizedConstructor { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonArrayContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x06000766 RID: 1894 RVA: 0x0001B3A4 File Offset: 0x000195A4
		public JsonArrayContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Array;
			bool canDeserialize;
			Type type;
			if (base.CreatedType.IsArray)
			{
				this.CollectionItemType = ReflectionUtils.GetCollectionItemType(base.UnderlyingType);
				this.IsReadOnlyOrFixedSize = true;
				this._genericCollectionDefinitionType = typeof(List).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				canDeserialize = true;
				this.IsMultidimensionalArray = (base.UnderlyingType.IsArray && base.UnderlyingType.GetArrayRank() > 1);
			}
			else if (typeof(IList).IsAssignableFrom(underlyingType))
			{
				if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(ICollection), out this._genericCollectionDefinitionType))
				{
					this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				}
				else
				{
					this.CollectionItemType = ReflectionUtils.GetCollectionItemType(underlyingType);
				}
				if (underlyingType == typeof(IList))
				{
					base.CreatedType = typeof(List<object>);
				}
				if (this.CollectionItemType != null)
				{
					this.ParametrizedConstructor = CollectionUtils.ResolveEnumableCollectionConstructor(underlyingType, this.CollectionItemType);
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(underlyingType, typeof(ReadOnlyCollection));
				canDeserialize = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(ICollection), out this._genericCollectionDefinitionType))
			{
				this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(underlyingType, typeof(ICollection)) || ReflectionUtils.IsGenericDefinition(underlyingType, typeof(IList)))
				{
					base.CreatedType = typeof(List).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				if (ReflectionUtils.IsGenericDefinition(underlyingType, typeof(ISet)))
				{
					base.CreatedType = typeof(HashSet).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this.ParametrizedConstructor = CollectionUtils.ResolveEnumableCollectionConstructor(underlyingType, this.CollectionItemType);
				canDeserialize = true;
				this.ShouldCreateWrapper = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IReadOnlyCollection), out type))
			{
				this.CollectionItemType = underlyingType.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(underlyingType, typeof(IReadOnlyCollection)) || ReflectionUtils.IsGenericDefinition(underlyingType, typeof(IReadOnlyList)))
				{
					base.CreatedType = typeof(ReadOnlyCollection).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this._genericCollectionDefinitionType = typeof(List).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				this.ParametrizedConstructor = CollectionUtils.ResolveEnumableCollectionConstructor(base.CreatedType, this.CollectionItemType);
				this.IsReadOnlyOrFixedSize = true;
				canDeserialize = (this.ParametrizedConstructor != null);
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IEnumerable), out type))
			{
				this.CollectionItemType = type.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IEnumerable)))
				{
					base.CreatedType = typeof(List).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this.ParametrizedConstructor = CollectionUtils.ResolveEnumableCollectionConstructor(underlyingType, this.CollectionItemType);
				if (underlyingType.IsGenericType() && underlyingType.GetGenericTypeDefinition() == typeof(IEnumerable))
				{
					this._genericCollectionDefinitionType = type;
					this.IsReadOnlyOrFixedSize = false;
					this.ShouldCreateWrapper = false;
					canDeserialize = true;
				}
				else
				{
					this._genericCollectionDefinitionType = typeof(List).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
					this.IsReadOnlyOrFixedSize = true;
					this.ShouldCreateWrapper = true;
					canDeserialize = (this.ParametrizedConstructor != null);
				}
			}
			else
			{
				canDeserialize = false;
				this.ShouldCreateWrapper = true;
			}
			this.CanDeserialize = canDeserialize;
			if (this.CollectionItemType != null)
			{
				this._isCollectionItemTypeNullableType = ReflectionUtils.IsNullableType(this.CollectionItemType);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001B790 File Offset: 0x00019990
		internal IWrappedCollection CreateWrapper(object list)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(CollectionWrapper<>).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				Type type;
				if (ReflectionUtils.InheritsGenericDefinition(this._genericCollectionDefinitionType, typeof(List)) || this._genericCollectionDefinitionType.GetGenericTypeDefinition() == typeof(IEnumerable))
				{
					type = typeof(ICollection).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				else
				{
					type = this._genericCollectionDefinitionType;
				}
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[]
				{
					type
				});
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(constructor);
			}
			return (IWrappedCollection)this._genericWrapperCreator(null, new object[]
			{
				list
			});
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001B874 File Offset: 0x00019A74
		internal IList CreateTemporaryCollection()
		{
			if (this._genericTemporaryCollectionCreator == null)
			{
				Type type = this.IsMultidimensionalArray ? typeof(object) : this.CollectionItemType;
				Type type2 = typeof(List).MakeGenericType(new Type[]
				{
					type
				});
				this._genericTemporaryCollectionCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type2);
			}
			return (IList)this._genericTemporaryCollectionCreator.Invoke();
		}

		// Token: 0x040002A5 RID: 677
		private readonly bool _isCollectionItemTypeNullableType;

		// Token: 0x040002A6 RID: 678
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x040002A7 RID: 679
		private Type _genericWrapperType;

		// Token: 0x040002A8 RID: 680
		private MethodCall<object, object> _genericWrapperCreator;

		// Token: 0x040002A9 RID: 681
		private Func<object> _genericTemporaryCollectionCreator;
	}
}

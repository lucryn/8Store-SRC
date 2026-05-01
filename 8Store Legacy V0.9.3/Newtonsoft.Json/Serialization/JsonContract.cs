using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x0200008F RID: 143
	public abstract class JsonContract
	{
		/// <summary>
		/// Gets the underlying type for the contract.
		/// </summary>
		/// <value>The underlying type for the contract.</value>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001AC64 File Offset: 0x00018E64
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001AC6C File Offset: 0x00018E6C
		public Type UnderlyingType { get; private set; }

		/// <summary>
		/// Gets or sets the type created during deserialization.
		/// </summary>
		/// <value>The type created during deserialization.</value>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001AC75 File Offset: 0x00018E75
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001AC7D File Offset: 0x00018E7D
		public Type CreatedType { get; set; }

		/// <summary>
		/// Gets or sets whether this type contract is serialized as a reference.
		/// </summary>
		/// <value>Whether this type contract is serialized as a reference.</value>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001AC86 File Offset: 0x00018E86
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001AC8E File Offset: 0x00018E8E
		public bool? IsReference { get; set; }

		/// <summary>
		/// Gets or sets the default <see cref="T:Newtonsoft.Json.JsonConverter" /> for this contract.
		/// </summary>
		/// <value>The converter.</value>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001AC97 File Offset: 0x00018E97
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001AC9F File Offset: 0x00018E9F
		public JsonConverter Converter { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		internal JsonConverter InternalConverter { get; set; }

		/// <summary>
		/// Gets or sets all methods called immediately after deserialization of the object.
		/// </summary>
		/// <value>The methods called immediately after deserialization of the object.</value>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001ACB9 File Offset: 0x00018EB9
		public IList<SerializationCallback> OnDeserializedCallbacks
		{
			get
			{
				if (this._onDeserializedCallbacks == null)
				{
					this._onDeserializedCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializedCallbacks;
			}
		}

		/// <summary>
		/// Gets or sets all methods called during deserialization of the object.
		/// </summary>
		/// <value>The methods called during deserialization of the object.</value>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		public IList<SerializationCallback> OnDeserializingCallbacks
		{
			get
			{
				if (this._onDeserializingCallbacks == null)
				{
					this._onDeserializingCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializingCallbacks;
			}
		}

		/// <summary>
		/// Gets or sets all methods called after serialization of the object graph.
		/// </summary>
		/// <value>The methods called after serialization of the object graph.</value>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001ACEF File Offset: 0x00018EEF
		public IList<SerializationCallback> OnSerializedCallbacks
		{
			get
			{
				if (this._onSerializedCallbacks == null)
				{
					this._onSerializedCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializedCallbacks;
			}
		}

		/// <summary>
		/// Gets or sets all methods called before serialization of the object.
		/// </summary>
		/// <value>The methods called before serialization of the object.</value>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001AD0A File Offset: 0x00018F0A
		public IList<SerializationCallback> OnSerializingCallbacks
		{
			get
			{
				if (this._onSerializingCallbacks == null)
				{
					this._onSerializingCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializingCallbacks;
			}
		}

		/// <summary>
		/// Gets or sets all method called when an error is thrown during the serialization of the object.
		/// </summary>
		/// <value>The methods called when an error is thrown during the serialization of the object.</value>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001AD25 File Offset: 0x00018F25
		public IList<SerializationErrorCallback> OnErrorCallbacks
		{
			get
			{
				if (this._onErrorCallbacks == null)
				{
					this._onErrorCallbacks = new List<SerializationErrorCallback>();
				}
				return this._onErrorCallbacks;
			}
		}

		/// <summary>
		/// Gets or sets the method called immediately after deserialization of the object.
		/// </summary>
		/// <value>The method called immediately after deserialization of the object.</value>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001AD40 File Offset: 0x00018F40
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001AD63 File Offset: 0x00018F63
		[Obsolete("This property is obsolete and has been replaced by the OnDeserializedCallbacks collection.")]
		public MethodInfo OnDeserialized
		{
			get
			{
				if (this.OnDeserializedCallbacks.Count <= 0)
				{
					return null;
				}
				return this.OnDeserializedCallbacks[0].Method();
			}
			set
			{
				this.OnDeserializedCallbacks.Clear();
				this.OnDeserializedCallbacks.Add(JsonContract.CreateSerializationCallback(value));
			}
		}

		/// <summary>
		/// Gets or sets the method called during deserialization of the object.
		/// </summary>
		/// <value>The method called during deserialization of the object.</value>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001AD81 File Offset: 0x00018F81
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		[Obsolete("This property is obsolete and has been replaced by the OnDeserializingCallbacks collection.")]
		public MethodInfo OnDeserializing
		{
			get
			{
				if (this.OnDeserializingCallbacks.Count <= 0)
				{
					return null;
				}
				return this.OnDeserializingCallbacks[0].Method();
			}
			set
			{
				this.OnDeserializingCallbacks.Clear();
				this.OnDeserializingCallbacks.Add(JsonContract.CreateSerializationCallback(value));
			}
		}

		/// <summary>
		/// Gets or sets the method called after serialization of the object graph.
		/// </summary>
		/// <value>The method called after serialization of the object graph.</value>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001ADC2 File Offset: 0x00018FC2
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001ADE5 File Offset: 0x00018FE5
		[Obsolete("This property is obsolete and has been replaced by the OnSerializedCallbacks collection.")]
		public MethodInfo OnSerialized
		{
			get
			{
				if (this.OnSerializedCallbacks.Count <= 0)
				{
					return null;
				}
				return this.OnSerializedCallbacks[0].Method();
			}
			set
			{
				this.OnSerializedCallbacks.Clear();
				this.OnSerializedCallbacks.Add(JsonContract.CreateSerializationCallback(value));
			}
		}

		/// <summary>
		/// Gets or sets the method called before serialization of the object.
		/// </summary>
		/// <value>The method called before serialization of the object.</value>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001AE03 File Offset: 0x00019003
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001AE26 File Offset: 0x00019026
		[Obsolete("This property is obsolete and has been replaced by the OnSerializingCallbacks collection.")]
		public MethodInfo OnSerializing
		{
			get
			{
				if (this.OnSerializingCallbacks.Count <= 0)
				{
					return null;
				}
				return this.OnSerializingCallbacks[0].Method();
			}
			set
			{
				this.OnSerializingCallbacks.Clear();
				this.OnSerializingCallbacks.Add(JsonContract.CreateSerializationCallback(value));
			}
		}

		/// <summary>
		/// Gets or sets the method called when an error is thrown during the serialization of the object.
		/// </summary>
		/// <value>The method called when an error is thrown during the serialization of the object.</value>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001AE44 File Offset: 0x00019044
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001AE67 File Offset: 0x00019067
		[Obsolete("This property is obsolete and has been replaced by the OnErrorCallbacks collection.")]
		public MethodInfo OnError
		{
			get
			{
				if (this.OnErrorCallbacks.Count <= 0)
				{
					return null;
				}
				return this.OnErrorCallbacks[0].Method();
			}
			set
			{
				this.OnErrorCallbacks.Clear();
				this.OnErrorCallbacks.Add(JsonContract.CreateSerializationErrorCallback(value));
			}
		}

		/// <summary>
		/// Gets or sets the default creator method used to create the object.
		/// </summary>
		/// <value>The default creator method used to create the object.</value>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001AE85 File Offset: 0x00019085
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001AE8D File Offset: 0x0001908D
		public Func<object> DefaultCreator { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the default creator is non public.
		/// </summary>
		/// <value><c>true</c> if the default object creator is non-public; otherwise, <c>false</c>.</value>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001AE96 File Offset: 0x00019096
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001AE9E File Offset: 0x0001909E
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x06000748 RID: 1864 RVA: 0x0001AEA8 File Offset: 0x000190A8
		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			this.IsSealed = underlyingType.IsSealed();
			this.IsInstantiable = (!underlyingType.IsInterface() && !underlyingType.IsAbstract());
			this.IsNullable = ReflectionUtils.IsNullable(underlyingType);
			this.NonNullableUnderlyingType = ((this.IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			this.CreatedType = this.NonNullableUnderlyingType;
			this.IsConvertable = ConvertUtils.IsConvertible(this.NonNullableUnderlyingType);
			this.IsEnum = this.NonNullableUnderlyingType.IsEnum();
			if (this.NonNullableUnderlyingType == typeof(byte[]))
			{
				this.InternalReadType = ReadType.ReadAsBytes;
				return;
			}
			if (this.NonNullableUnderlyingType == typeof(int))
			{
				this.InternalReadType = ReadType.ReadAsInt32;
				return;
			}
			if (this.NonNullableUnderlyingType == typeof(decimal))
			{
				this.InternalReadType = ReadType.ReadAsDecimal;
				return;
			}
			if (this.NonNullableUnderlyingType == typeof(string))
			{
				this.InternalReadType = ReadType.ReadAsString;
				return;
			}
			if (this.NonNullableUnderlyingType == typeof(DateTime))
			{
				this.InternalReadType = ReadType.ReadAsDateTime;
				return;
			}
			if (this.NonNullableUnderlyingType == typeof(DateTimeOffset))
			{
				this.InternalReadType = ReadType.ReadAsDateTimeOffset;
				return;
			}
			this.InternalReadType = ReadType.Read;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001AFF0 File Offset: 0x000191F0
		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (this._onSerializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001B048 File Offset: 0x00019248
		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (this._onSerializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001B0A0 File Offset: 0x000192A0
		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (this._onDeserializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001B0F8 File Offset: 0x000192F8
		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (this._onDeserializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001B154 File Offset: 0x00019354
		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (this._onErrorCallbacks != null)
			{
				foreach (SerializationErrorCallback serializationErrorCallback in this._onErrorCallbacks)
				{
					serializationErrorCallback(o, context, errorContext);
				}
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001B1E0 File Offset: 0x000193E0
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[]
				{
					context
				});
			};
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001B240 File Offset: 0x00019440
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[]
				{
					context,
					econtext
				});
			};
		}

		// Token: 0x0400028A RID: 650
		internal bool IsNullable;

		// Token: 0x0400028B RID: 651
		internal bool IsConvertable;

		// Token: 0x0400028C RID: 652
		internal bool IsSealed;

		// Token: 0x0400028D RID: 653
		internal bool IsEnum;

		// Token: 0x0400028E RID: 654
		internal Type NonNullableUnderlyingType;

		// Token: 0x0400028F RID: 655
		internal ReadType InternalReadType;

		// Token: 0x04000290 RID: 656
		internal JsonContractType ContractType;

		// Token: 0x04000291 RID: 657
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x04000292 RID: 658
		internal bool IsInstantiable;

		// Token: 0x04000293 RID: 659
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x04000294 RID: 660
		private IList<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x04000295 RID: 661
		private IList<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x04000296 RID: 662
		private IList<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x04000297 RID: 663
		private IList<SerializationErrorCallback> _onErrorCallbacks;
	}
}

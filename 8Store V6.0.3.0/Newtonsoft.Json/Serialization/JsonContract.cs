using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000094 RID: 148
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonContract
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001BE3D File Offset: 0x0001A03D
		public Type UnderlyingType { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001BE45 File Offset: 0x0001A045
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001BE50 File Offset: 0x0001A050
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public Type CreatedType
		{
			get
			{
				return this._createdType;
			}
			set
			{
				ValidationUtils.ArgumentNotNull(value, "value");
				this._createdType = value;
				this.IsSealed = this._createdType.IsSealed();
				this.IsInstantiable = (!this._createdType.IsInterface() && !this._createdType.IsAbstract());
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001BEA4 File Offset: 0x0001A0A4
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		public bool? IsReference { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001BEB5 File Offset: 0x0001A0B5
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0001BEBD File Offset: 0x0001A0BD
		[Nullable(2)]
		public JsonConverter Converter { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001BEC6 File Offset: 0x0001A0C6
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0001BECE File Offset: 0x0001A0CE
		[Nullable(2)]
		public JsonConverter InternalConverter { [NullableContext(2)] get; [NullableContext(2)] internal set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001BED7 File Offset: 0x0001A0D7
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001BEF2 File Offset: 0x0001A0F2
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001BF0D File Offset: 0x0001A10D
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001BF28 File Offset: 0x0001A128
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001BF43 File Offset: 0x0001A143
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001BF5E File Offset: 0x0001A15E
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001BF66 File Offset: 0x0001A166
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Func<object> DefaultCreator { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001BF6F File Offset: 0x0001A16F
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0001BF77 File Offset: 0x0001A177
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x060006DF RID: 1759 RVA: 0x0001BF80 File Offset: 0x0001A180
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		internal JsonContract([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			underlyingType = ReflectionUtils.EnsureNotByRefType(underlyingType);
			this.IsNullable = ReflectionUtils.IsNullable(underlyingType);
			this.NonNullableUnderlyingType = ((this.IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			this._createdType = (this.CreatedType = this.NonNullableUnderlyingType);
			this.IsConvertable = ConvertUtils.IsConvertible(this.NonNullableUnderlyingType);
			this.IsEnum = this.NonNullableUnderlyingType.IsEnum();
			this.InternalReadType = ReadType.Read;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001C018 File Offset: 0x0001A218
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

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001C074 File Offset: 0x0001A274
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

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
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

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001C12C File Offset: 0x0001A32C
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001C188 File Offset: 0x0001A388
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

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
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

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001C1FD File Offset: 0x0001A3FD
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

		// Token: 0x040002A6 RID: 678
		internal bool IsNullable;

		// Token: 0x040002A7 RID: 679
		internal bool IsConvertable;

		// Token: 0x040002A8 RID: 680
		internal bool IsEnum;

		// Token: 0x040002A9 RID: 681
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		internal Type NonNullableUnderlyingType;

		// Token: 0x040002AA RID: 682
		internal ReadType InternalReadType;

		// Token: 0x040002AB RID: 683
		internal JsonContractType ContractType;

		// Token: 0x040002AC RID: 684
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x040002AD RID: 685
		internal bool IsSealed;

		// Token: 0x040002AE RID: 686
		internal bool IsInstantiable;

		// Token: 0x040002AF RID: 687
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x040002B0 RID: 688
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x040002B1 RID: 689
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x040002B2 RID: 690
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x040002B3 RID: 691
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x040002B4 RID: 692
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		private Type _createdType;
	}
}

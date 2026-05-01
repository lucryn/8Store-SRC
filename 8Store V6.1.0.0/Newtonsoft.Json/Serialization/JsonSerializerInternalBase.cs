using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009C RID: 156
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class JsonSerializerInternalBase
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x0001CFBD File Offset: 0x0001B1BD
		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			this.Serializer = serializer;
			this.TraceWriter = serializer.TraceWriter;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001CFE3 File Offset: 0x0001B1E3
		internal BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (this._mappings == null)
				{
					this._mappings = new BidirectionalDictionary<string, object>(EqualityComparer<string>.Default, new JsonSerializerInternalBase.ReferenceEqualsEqualityComparer(), "A different value already has the Id '{0}'.", "A different Id has already been assigned for value '{0}'. This error may be caused by an object being reused multiple times during deserialization and can be fixed with the setting ObjectCreationHandling.Replace.");
				}
				return this._mappings;
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001D014 File Offset: 0x0001B214
		protected NullValueHandling ResolvedNullValueHandling([Nullable(2)] JsonObjectContract containerContract, JsonProperty property)
		{
			NullValueHandling? nullValueHandling = property.NullValueHandling;
			if (nullValueHandling != null)
			{
				return nullValueHandling.GetValueOrDefault();
			}
			NullValueHandling? nullValueHandling2 = (containerContract != null) ? containerContract.ItemNullValueHandling : default(NullValueHandling?);
			if (nullValueHandling2 == null)
			{
				return this.Serializer._nullValueHandling;
			}
			return nullValueHandling2.GetValueOrDefault();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001D06A File Offset: 0x0001B26A
		private ErrorContext GetErrorContext([Nullable(2)] object currentObject, [Nullable(2)] object member, string path, Exception error)
		{
			if (this._currentErrorContext == null)
			{
				this._currentErrorContext = new ErrorContext(currentObject, member, path, error);
			}
			if (this._currentErrorContext.Error != error)
			{
				throw new InvalidOperationException("Current error context error is different to requested error.");
			}
			return this._currentErrorContext;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		protected void ClearErrorContext()
		{
			if (this._currentErrorContext == null)
			{
				throw new InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			this._currentErrorContext = null;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001D0C0 File Offset: 0x0001B2C0
		[NullableContext(2)]
		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, IJsonLineInfo lineInfo, [Nullable(1)] string path, [Nullable(1)] Exception ex)
		{
			ErrorContext errorContext = this.GetErrorContext(currentObject, keyValue, path, ex);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Error && !errorContext.Traced)
			{
				errorContext.Traced = true;
				string text = (base.GetType() == typeof(JsonSerializerInternalWriter)) ? "Error serializing" : "Error deserializing";
				if (contract != null)
				{
					string text2 = text;
					string text3 = " ";
					Type underlyingType = contract.UnderlyingType;
					text = text2 + text3 + ((underlyingType != null) ? underlyingType.ToString() : null);
				}
				text = text + ". " + ex.Message;
				if (!(ex is JsonException))
				{
					text = JsonPosition.FormatMessage(lineInfo, path, text);
				}
				this.TraceWriter.Trace(TraceLevel.Error, text, ex);
			}
			if (contract != null && currentObject != null)
			{
				contract.InvokeOnError(currentObject, this.Serializer.Context, errorContext);
			}
			if (!errorContext.Handled)
			{
				this.Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}

		// Token: 0x040002FE RID: 766
		[Nullable(2)]
		private ErrorContext _currentErrorContext;

		// Token: 0x040002FF RID: 767
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private BidirectionalDictionary<string, object> _mappings;

		// Token: 0x04000300 RID: 768
		internal readonly JsonSerializer Serializer;

		// Token: 0x04000301 RID: 769
		[Nullable(2)]
		internal readonly ITraceWriter TraceWriter;

		// Token: 0x04000302 RID: 770
		[Nullable(2)]
		protected JsonSerializerProxy InternalSerializer;

		// Token: 0x020001B1 RID: 433
		[NullableContext(0)]
		private class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x06000ECC RID: 3788 RVA: 0x000408BE File Offset: 0x0003EABE
			[NullableContext(2)]
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x000408C4 File Offset: 0x0003EAC4
			[NullableContext(1)]
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}
		}
	}
}

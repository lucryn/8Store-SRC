using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009D RID: 157
	internal abstract class JsonSerializerInternalBase
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			this.Serializer = serializer;
			this.TraceWriter = serializer.TraceWriter;
			this._serializing = (base.GetType() == typeof(JsonSerializerInternalWriter));
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001C3E6 File Offset: 0x0001A5E6
		internal BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (this._mappings == null)
				{
					this._mappings = new BidirectionalDictionary<string, object>(EqualityComparer<string>.Default, new JsonSerializerInternalBase.ReferenceEqualsEqualityComparer(), "A different value already has the Id '{0}'.", "A different Id has already been assigned for value '{0}'.");
				}
				return this._mappings;
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001C415 File Offset: 0x0001A615
		private ErrorContext GetErrorContext(object currentObject, object member, string path, Exception error)
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

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001C44F File Offset: 0x0001A64F
		protected void ClearErrorContext()
		{
			if (this._currentErrorContext == null)
			{
				throw new InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			this._currentErrorContext = null;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001C46C File Offset: 0x0001A66C
		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, IJsonLineInfo lineInfo, string path, Exception ex)
		{
			ErrorContext errorContext = this.GetErrorContext(currentObject, keyValue, path, ex);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Error && !errorContext.Traced)
			{
				errorContext.Traced = true;
				string text = this._serializing ? "Error serializing" : "Error deserializing";
				if (contract != null)
				{
					text = text + " " + contract.UnderlyingType;
				}
				text = text + ". " + ex.Message;
				if (!(ex is JsonException))
				{
					text = JsonPosition.FormatMessage(lineInfo, path, text);
				}
				this.TraceWriter.Trace(TraceLevel.Error, text, ex);
			}
			if (contract != null)
			{
				contract.InvokeOnError(currentObject, this.Serializer.Context, errorContext);
			}
			if (!errorContext.Handled)
			{
				this.Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}

		// Token: 0x040002EF RID: 751
		private ErrorContext _currentErrorContext;

		// Token: 0x040002F0 RID: 752
		private BidirectionalDictionary<string, object> _mappings;

		// Token: 0x040002F1 RID: 753
		private bool _serializing;

		// Token: 0x040002F2 RID: 754
		internal readonly JsonSerializer Serializer;

		// Token: 0x040002F3 RID: 755
		internal readonly ITraceWriter TraceWriter;

		// Token: 0x0200009E RID: 158
		private class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x060007E9 RID: 2025 RVA: 0x0001C541 File Offset: 0x0001A741
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return object.ReferenceEquals(x, y);
			}

			// Token: 0x060007EA RID: 2026 RVA: 0x0001C54A File Offset: 0x0001A74A
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return -1;
			}
		}
	}
}

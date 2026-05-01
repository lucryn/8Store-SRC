using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonWriter : IDisposable
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0000D690 File Offset: 0x0000B890
		internal Task AutoCompleteAsync(JsonToken tokenBeingWritten, CancellationToken cancellationToken)
		{
			JsonWriter.State currentState = this._currentState;
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), currentState.ToString()), null);
			}
			this._currentState = state;
			if (this._formatting == Formatting.Indented)
			{
				switch (currentState)
				{
				case JsonWriter.State.Start:
					goto IL_F3;
				case JsonWriter.State.Property:
					return this.WriteIndentSpaceAsync(cancellationToken);
				case JsonWriter.State.Object:
					if (tokenBeingWritten == JsonToken.PropertyName)
					{
						return this.AutoCompleteAsync(cancellationToken);
					}
					if (tokenBeingWritten != JsonToken.Comment)
					{
						return this.WriteValueDelimiterAsync(cancellationToken);
					}
					goto IL_F3;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.ConstructorStart:
					return this.WriteIndentAsync(cancellationToken);
				case JsonWriter.State.Array:
				case JsonWriter.State.Constructor:
					if (tokenBeingWritten != JsonToken.Comment)
					{
						return this.AutoCompleteAsync(cancellationToken);
					}
					return this.WriteIndentAsync(cancellationToken);
				}
				if (tokenBeingWritten == JsonToken.PropertyName)
				{
					return this.WriteIndentAsync(cancellationToken);
				}
			}
			else if (tokenBeingWritten != JsonToken.Comment)
			{
				switch (currentState)
				{
				case JsonWriter.State.Object:
				case JsonWriter.State.Array:
				case JsonWriter.State.Constructor:
					return this.WriteValueDelimiterAsync(cancellationToken);
				}
			}
			IL_F3:
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000D798 File Offset: 0x0000B998
		private Task AutoCompleteAsync(CancellationToken cancellationToken)
		{
			JsonWriter.<AutoCompleteAsync>d__1 <AutoCompleteAsync>d__;
			<AutoCompleteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AutoCompleteAsync>d__.<>4__this = this;
			<AutoCompleteAsync>d__.cancellationToken = cancellationToken;
			<AutoCompleteAsync>d__.<>1__state = -1;
			<AutoCompleteAsync>d__.<>t__builder.Start<JsonWriter.<AutoCompleteAsync>d__1>(ref <AutoCompleteAsync>d__);
			return <AutoCompleteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000D7E3 File Offset: 0x0000B9E3
		public virtual Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.Close();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000D800 File Offset: 0x0000BA00
		public virtual Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.Flush();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000D81D File Offset: 0x0000BA1D
		protected virtual Task WriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEnd(token);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D83B File Offset: 0x0000BA3B
		protected virtual Task WriteIndentAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteIndent();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D858 File Offset: 0x0000BA58
		protected virtual Task WriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValueDelimiter();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000D875 File Offset: 0x0000BA75
		protected virtual Task WriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteIndentSpace();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000D892 File Offset: 0x0000BA92
		public virtual Task WriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteRaw(json);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public virtual Task WriteEndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEnd();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		internal Task WriteEndInternalAsync(CancellationToken cancellationToken)
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.Object:
				return this.WriteEndObjectAsync(cancellationToken);
			case JsonContainerType.Array:
				return this.WriteEndArrayAsync(cancellationToken);
			case JsonContainerType.Constructor:
				return this.WriteEndConstructorAsync(cancellationToken);
			default:
				if (cancellationToken.IsCancellationRequested)
				{
					return cancellationToken.FromCanceled();
				}
				throw JsonWriterException.Create(this, "Unexpected type when writing end: " + jsonContainerType.ToString(), null);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000D940 File Offset: 0x0000BB40
		internal Task InternalWriteEndAsync(JsonContainerType type, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			int levelsToComplete = this.CalculateLevelsToComplete(type);
			while (levelsToComplete-- > 0)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				Task task;
				if (this._currentState == JsonWriter.State.Property)
				{
					task = this.WriteNullAsync(cancellationToken);
					if (!task.IsCompletedSuccessfully())
					{
						return this.<InternalWriteEndAsync>g__AwaitProperty|11_0(task, levelsToComplete, closeTokenForType, cancellationToken);
					}
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					task = this.WriteIndentAsync(cancellationToken);
					if (!task.IsCompletedSuccessfully())
					{
						return this.<InternalWriteEndAsync>g__AwaitIndent|11_1(task, levelsToComplete, closeTokenForType, cancellationToken);
					}
				}
				task = this.WriteEndAsync(closeTokenForType, cancellationToken);
				if (!task.IsCompletedSuccessfully())
				{
					return this.<InternalWriteEndAsync>g__AwaitEnd|11_2(task, levelsToComplete, cancellationToken);
				}
				this.UpdateCurrentState();
			}
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000DA02 File Offset: 0x0000BC02
		public virtual Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndArray();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000DA1F File Offset: 0x0000BC1F
		public virtual Task WriteEndConstructorAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndConstructor();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		public virtual Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndObject();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000DA59 File Offset: 0x0000BC59
		public virtual Task WriteNullAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteNull();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000DA76 File Offset: 0x0000BC76
		public virtual Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WritePropertyName(name);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public virtual Task WritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WritePropertyName(name, escape);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
		internal Task InternalWritePropertyNameAsync(string name, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this._currentPosition.PropertyName = name;
			return this.AutoCompleteAsync(JsonToken.PropertyName, cancellationToken);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000DAD9 File Offset: 0x0000BCD9
		public virtual Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartArray();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		internal Task InternalWriteStartAsync(JsonToken token, JsonContainerType container, CancellationToken cancellationToken)
		{
			JsonWriter.<InternalWriteStartAsync>d__20 <InternalWriteStartAsync>d__;
			<InternalWriteStartAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InternalWriteStartAsync>d__.<>4__this = this;
			<InternalWriteStartAsync>d__.token = token;
			<InternalWriteStartAsync>d__.container = container;
			<InternalWriteStartAsync>d__.cancellationToken = cancellationToken;
			<InternalWriteStartAsync>d__.<>1__state = -1;
			<InternalWriteStartAsync>d__.<>t__builder.Start<JsonWriter.<InternalWriteStartAsync>d__20>(ref <InternalWriteStartAsync>d__);
			return <InternalWriteStartAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000DB53 File Offset: 0x0000BD53
		public virtual Task WriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteComment(text);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000DB71 File Offset: 0x0000BD71
		internal Task InternalWriteCommentAsync(CancellationToken cancellationToken)
		{
			return this.AutoCompleteAsync(JsonToken.Comment, cancellationToken);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000DB7B File Offset: 0x0000BD7B
		public virtual Task WriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteRawValue(json);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000DB99 File Offset: 0x0000BD99
		public virtual Task WriteStartConstructorAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartConstructor(name);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000DBB7 File Offset: 0x0000BDB7
		public virtual Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartObject();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public Task WriteTokenAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.WriteTokenAsync(reader, true, cancellationToken);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000DBDF File Offset: 0x0000BDDF
		public Task WriteTokenAsync(JsonReader reader, bool writeChildren, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			return this.WriteTokenAsync(reader, writeChildren, true, true, cancellationToken);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
		public Task WriteTokenAsync(JsonToken token, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.WriteTokenAsync(token, null, cancellationToken);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public Task WriteTokenAsync(JsonToken token, [Nullable(2)] object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			switch (token)
			{
			case JsonToken.None:
				return AsyncUtils.CompletedTask;
			case JsonToken.StartObject:
				return this.WriteStartObjectAsync(cancellationToken);
			case JsonToken.StartArray:
				return this.WriteStartArrayAsync(cancellationToken);
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WriteStartConstructorAsync(value.ToString(), cancellationToken);
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WritePropertyNameAsync(value.ToString(), cancellationToken);
			case JsonToken.Comment:
				return this.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			case JsonToken.Raw:
				return this.WriteRawValueAsync((value != null) ? value.ToString() : null, cancellationToken);
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WriteValueAsync(Convert.ToInt64(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal)
				{
					decimal value2 = (decimal)value;
					return this.WriteValueAsync(value2, cancellationToken);
				}
				if (value is double)
				{
					double value3 = (double)value;
					return this.WriteValueAsync(value3, cancellationToken);
				}
				if (value is float)
				{
					float value4 = (float)value;
					return this.WriteValueAsync(value4, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToDouble(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.String:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WriteValueAsync(value.ToString(), cancellationToken);
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WriteValueAsync(Convert.ToBoolean(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Null:
				return this.WriteNullAsync(cancellationToken);
			case JsonToken.Undefined:
				return this.WriteUndefinedAsync(cancellationToken);
			case JsonToken.EndObject:
				return this.WriteEndObjectAsync(cancellationToken);
			case JsonToken.EndArray:
				return this.WriteEndArrayAsync(cancellationToken);
			case JsonToken.EndConstructor:
				return this.WriteEndConstructorAsync(cancellationToken);
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is DateTimeOffset)
				{
					DateTimeOffset value5 = (DateTimeOffset)value;
					return this.WriteValueAsync(value5, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToDateTime(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is Guid)
				{
					Guid value6 = (Guid)value;
					return this.WriteValueAsync(value6, cancellationToken);
				}
				return this.WriteValueAsync((byte[])value, cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000DE48 File Offset: 0x0000C048
		internal virtual Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			JsonWriter.<WriteTokenAsync>d__30 <WriteTokenAsync>d__;
			<WriteTokenAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteTokenAsync>d__.<>4__this = this;
			<WriteTokenAsync>d__.reader = reader;
			<WriteTokenAsync>d__.writeChildren = writeChildren;
			<WriteTokenAsync>d__.writeDateConstructorAsDate = writeDateConstructorAsDate;
			<WriteTokenAsync>d__.writeComments = writeComments;
			<WriteTokenAsync>d__.cancellationToken = cancellationToken;
			<WriteTokenAsync>d__.<>1__state = -1;
			<WriteTokenAsync>d__.<>t__builder.Start<JsonWriter.<WriteTokenAsync>d__30>(ref <WriteTokenAsync>d__);
			return <WriteTokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		internal Task WriteTokenSyncReadingAsync(JsonReader reader, CancellationToken cancellationToken)
		{
			JsonWriter.<WriteTokenSyncReadingAsync>d__31 <WriteTokenSyncReadingAsync>d__;
			<WriteTokenSyncReadingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteTokenSyncReadingAsync>d__.<>4__this = this;
			<WriteTokenSyncReadingAsync>d__.reader = reader;
			<WriteTokenSyncReadingAsync>d__.cancellationToken = cancellationToken;
			<WriteTokenSyncReadingAsync>d__.<>1__state = -1;
			<WriteTokenSyncReadingAsync>d__.<>t__builder.Start<JsonWriter.<WriteTokenSyncReadingAsync>d__31>(ref <WriteTokenSyncReadingAsync>d__);
			return <WriteTokenSyncReadingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000DF0C File Offset: 0x0000C10C
		private Task WriteConstructorDateAsync(JsonReader reader, CancellationToken cancellationToken)
		{
			JsonWriter.<WriteConstructorDateAsync>d__32 <WriteConstructorDateAsync>d__;
			<WriteConstructorDateAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteConstructorDateAsync>d__.<>4__this = this;
			<WriteConstructorDateAsync>d__.reader = reader;
			<WriteConstructorDateAsync>d__.cancellationToken = cancellationToken;
			<WriteConstructorDateAsync>d__.<>1__state = -1;
			<WriteConstructorDateAsync>d__.<>t__builder.Start<JsonWriter.<WriteConstructorDateAsync>d__32>(ref <WriteConstructorDateAsync>d__);
			return <WriteConstructorDateAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000DF5F File Offset: 0x0000C15F
		public virtual Task WriteValueAsync(bool value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000DF7D File Offset: 0x0000C17D
		public virtual Task WriteValueAsync(bool? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000DF9B File Offset: 0x0000C19B
		public virtual Task WriteValueAsync(byte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000DFB9 File Offset: 0x0000C1B9
		public virtual Task WriteValueAsync(byte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000DFD7 File Offset: 0x0000C1D7
		public virtual Task WriteValueAsync([Nullable(2)] byte[] value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000DFF5 File Offset: 0x0000C1F5
		public virtual Task WriteValueAsync(char value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000E013 File Offset: 0x0000C213
		public virtual Task WriteValueAsync(char? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000E031 File Offset: 0x0000C231
		public virtual Task WriteValueAsync(DateTime value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E04F File Offset: 0x0000C24F
		public virtual Task WriteValueAsync(DateTime? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E06D File Offset: 0x0000C26D
		public virtual Task WriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000E08B File Offset: 0x0000C28B
		public virtual Task WriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000E0A9 File Offset: 0x0000C2A9
		public virtual Task WriteValueAsync(decimal value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000E0C7 File Offset: 0x0000C2C7
		public virtual Task WriteValueAsync(decimal? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000E0E5 File Offset: 0x0000C2E5
		public virtual Task WriteValueAsync(double value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000E103 File Offset: 0x0000C303
		public virtual Task WriteValueAsync(double? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000E121 File Offset: 0x0000C321
		public virtual Task WriteValueAsync(float value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E13F File Offset: 0x0000C33F
		public virtual Task WriteValueAsync(float? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000E15D File Offset: 0x0000C35D
		public virtual Task WriteValueAsync(Guid value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000E17B File Offset: 0x0000C37B
		public virtual Task WriteValueAsync(Guid? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000E199 File Offset: 0x0000C399
		public virtual Task WriteValueAsync(int value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000E1B7 File Offset: 0x0000C3B7
		public virtual Task WriteValueAsync(int? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000E1D5 File Offset: 0x0000C3D5
		public virtual Task WriteValueAsync(long value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000E1F3 File Offset: 0x0000C3F3
		public virtual Task WriteValueAsync(long? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000E211 File Offset: 0x0000C411
		public virtual Task WriteValueAsync([Nullable(2)] object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000E22F File Offset: 0x0000C42F
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(sbyte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000E24D File Offset: 0x0000C44D
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(sbyte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000E26B File Offset: 0x0000C46B
		public virtual Task WriteValueAsync(short value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000E289 File Offset: 0x0000C489
		public virtual Task WriteValueAsync(short? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000E2A7 File Offset: 0x0000C4A7
		public virtual Task WriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000E2C5 File Offset: 0x0000C4C5
		public virtual Task WriteValueAsync(TimeSpan value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000E2E3 File Offset: 0x0000C4E3
		public virtual Task WriteValueAsync(TimeSpan? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000E301 File Offset: 0x0000C501
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(uint value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000E31F File Offset: 0x0000C51F
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(uint? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000E33D File Offset: 0x0000C53D
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ulong value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000E35B File Offset: 0x0000C55B
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ulong? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000E379 File Offset: 0x0000C579
		public virtual Task WriteValueAsync([Nullable(2)] Uri value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000E397 File Offset: 0x0000C597
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ushort value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000E3B5 File Offset: 0x0000C5B5
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ushort? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000E3D3 File Offset: 0x0000C5D3
		public virtual Task WriteUndefinedAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteUndefined();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public virtual Task WriteWhitespaceAsync(string ws, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteWhitespace(ws);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000E40E File Offset: 0x0000C60E
		internal Task InternalWriteValueAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.UpdateScopeWithFinishedValue();
			return this.AutoCompleteAsync(token, cancellationToken);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000E430 File Offset: 0x0000C630
		protected Task SetWriteStateAsync(JsonToken token, object value, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			switch (token)
			{
			case JsonToken.StartObject:
				return this.InternalWriteStartAsync(token, JsonContainerType.Object, cancellationToken);
			case JsonToken.StartArray:
				return this.InternalWriteStartAsync(token, JsonContainerType.Array, cancellationToken);
			case JsonToken.StartConstructor:
				return this.InternalWriteStartAsync(token, JsonContainerType.Constructor, cancellationToken);
			case JsonToken.PropertyName:
			{
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException("A name is required when setting property name state.", "value");
				}
				return this.InternalWritePropertyNameAsync(text, cancellationToken);
			}
			case JsonToken.Comment:
				return this.InternalWriteCommentAsync(cancellationToken);
			case JsonToken.Raw:
				return AsyncUtils.CompletedTask;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return this.InternalWriteValueAsync(token, cancellationToken);
			case JsonToken.EndObject:
				return this.InternalWriteEndAsync(JsonContainerType.Object, cancellationToken);
			case JsonToken.EndArray:
				return this.InternalWriteEndAsync(JsonContainerType.Array, cancellationToken);
			case JsonToken.EndConstructor:
				return this.InternalWriteEndAsync(JsonContainerType.Constructor, cancellationToken);
			default:
				throw new ArgumentOutOfRangeException("token");
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000E518 File Offset: 0x0000C718
		internal static Task WriteValueAsync(JsonWriter writer, PrimitiveTypeCode typeCode, object value, CancellationToken cancellationToken)
		{
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
				return writer.WriteValueAsync((char)value, cancellationToken);
			case PrimitiveTypeCode.CharNullable:
				return writer.WriteValueAsync((value == null) ? default(char?) : new char?((char)value), cancellationToken);
			case PrimitiveTypeCode.Boolean:
				return writer.WriteValueAsync((bool)value, cancellationToken);
			case PrimitiveTypeCode.BooleanNullable:
				return writer.WriteValueAsync((value == null) ? default(bool?) : new bool?((bool)value), cancellationToken);
			case PrimitiveTypeCode.SByte:
				return writer.WriteValueAsync((sbyte)value, cancellationToken);
			case PrimitiveTypeCode.SByteNullable:
				return writer.WriteValueAsync((value == null) ? default(sbyte?) : new sbyte?((sbyte)value), cancellationToken);
			case PrimitiveTypeCode.Int16:
				return writer.WriteValueAsync((short)value, cancellationToken);
			case PrimitiveTypeCode.Int16Nullable:
				return writer.WriteValueAsync((value == null) ? default(short?) : new short?((short)value), cancellationToken);
			case PrimitiveTypeCode.UInt16:
				return writer.WriteValueAsync((ushort)value, cancellationToken);
			case PrimitiveTypeCode.UInt16Nullable:
				return writer.WriteValueAsync((value == null) ? default(ushort?) : new ushort?((ushort)value), cancellationToken);
			case PrimitiveTypeCode.Int32:
				return writer.WriteValueAsync((int)value, cancellationToken);
			case PrimitiveTypeCode.Int32Nullable:
				return writer.WriteValueAsync((value == null) ? default(int?) : new int?((int)value), cancellationToken);
			case PrimitiveTypeCode.Byte:
				return writer.WriteValueAsync((byte)value, cancellationToken);
			case PrimitiveTypeCode.ByteNullable:
				return writer.WriteValueAsync((value == null) ? default(byte?) : new byte?((byte)value), cancellationToken);
			case PrimitiveTypeCode.UInt32:
				return writer.WriteValueAsync((uint)value, cancellationToken);
			case PrimitiveTypeCode.UInt32Nullable:
				return writer.WriteValueAsync((value == null) ? default(uint?) : new uint?((uint)value), cancellationToken);
			case PrimitiveTypeCode.Int64:
				return writer.WriteValueAsync((long)value, cancellationToken);
			case PrimitiveTypeCode.Int64Nullable:
				return writer.WriteValueAsync((value == null) ? default(long?) : new long?((long)value), cancellationToken);
			case PrimitiveTypeCode.UInt64:
				return writer.WriteValueAsync((ulong)value, cancellationToken);
			case PrimitiveTypeCode.UInt64Nullable:
				return writer.WriteValueAsync((value == null) ? default(ulong?) : new ulong?((ulong)value), cancellationToken);
			case PrimitiveTypeCode.Single:
				return writer.WriteValueAsync((float)value, cancellationToken);
			case PrimitiveTypeCode.SingleNullable:
				return writer.WriteValueAsync((value == null) ? default(float?) : new float?((float)value), cancellationToken);
			case PrimitiveTypeCode.Double:
				return writer.WriteValueAsync((double)value, cancellationToken);
			case PrimitiveTypeCode.DoubleNullable:
				return writer.WriteValueAsync((value == null) ? default(double?) : new double?((double)value), cancellationToken);
			case PrimitiveTypeCode.DateTime:
				return writer.WriteValueAsync((DateTime)value, cancellationToken);
			case PrimitiveTypeCode.DateTimeNullable:
				return writer.WriteValueAsync((value == null) ? default(DateTime?) : new DateTime?((DateTime)value), cancellationToken);
			case PrimitiveTypeCode.DateTimeOffset:
				return writer.WriteValueAsync((DateTimeOffset)value, cancellationToken);
			case PrimitiveTypeCode.DateTimeOffsetNullable:
				return writer.WriteValueAsync((value == null) ? default(DateTimeOffset?) : new DateTimeOffset?((DateTimeOffset)value), cancellationToken);
			case PrimitiveTypeCode.Decimal:
				return writer.WriteValueAsync((decimal)value, cancellationToken);
			case PrimitiveTypeCode.DecimalNullable:
				return writer.WriteValueAsync((value == null) ? default(decimal?) : new decimal?((decimal)value), cancellationToken);
			case PrimitiveTypeCode.Guid:
				return writer.WriteValueAsync((Guid)value, cancellationToken);
			case PrimitiveTypeCode.GuidNullable:
				return writer.WriteValueAsync((value == null) ? default(Guid?) : new Guid?((Guid)value), cancellationToken);
			case PrimitiveTypeCode.TimeSpan:
				return writer.WriteValueAsync((TimeSpan)value, cancellationToken);
			case PrimitiveTypeCode.TimeSpanNullable:
				return writer.WriteValueAsync((value == null) ? default(TimeSpan?) : new TimeSpan?((TimeSpan)value), cancellationToken);
			case PrimitiveTypeCode.Uri:
				return writer.WriteValueAsync((Uri)value, cancellationToken);
			case PrimitiveTypeCode.String:
				return writer.WriteValueAsync((string)value, cancellationToken);
			case PrimitiveTypeCode.Bytes:
				return writer.WriteValueAsync((byte[])value, cancellationToken);
			}
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			throw JsonWriter.CreateUnsupportedTypeException(writer, value);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000E938 File Offset: 0x0000CB38
		internal static JsonWriter.State[][] BuildStateArray()
		{
			List<JsonWriter.State[]> list = Enumerable.ToList<JsonWriter.State[]>(JsonWriter.StateArrayTemplate);
			JsonWriter.State[] array = JsonWriter.StateArrayTemplate[0];
			JsonWriter.State[] array2 = JsonWriter.StateArrayTemplate[7];
			foreach (ulong num in EnumUtils.GetEnumValuesAndNames(typeof(JsonToken)).Values)
			{
				if (list.Count <= (int)num)
				{
					JsonToken jsonToken = (JsonToken)num;
					if (jsonToken - JsonToken.Integer <= 5 || jsonToken - JsonToken.Date <= 1)
					{
						list.Add(array2);
					}
					else
					{
						list.Add(array);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		static JsonWriter()
		{
			JsonWriter.StateArray = JsonWriter.BuildStateArray();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000EA8E File Offset: 0x0000CC8E
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000EA96 File Offset: 0x0000CC96
		public bool CloseOutput { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000EA9F File Offset: 0x0000CC9F
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000EAA7 File Offset: 0x0000CCA7
		public bool AutoCompleteOnClose { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		protected internal int Top
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = (stack != null) ? stack.Count : 0;
				if (this.Peek() != JsonContainerType.None)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				default:
					throw JsonWriterException.Create(this, "Invalid state: " + this._currentState.ToString(), null);
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000EB54 File Offset: 0x0000CD54
		internal string ContainerPath
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None || this._stack == null)
				{
					return string.Empty;
				}
				return JsonPosition.BuildPath(this._stack, default(JsonPosition?));
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = (this._currentState != JsonWriter.State.ArrayStart && this._currentState != JsonWriter.State.ConstructorStart && this._currentState != JsonWriter.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : default(JsonPosition?);
				return JsonPosition.BuildPath(this._stack, currentPosition);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000EBF6 File Offset: 0x0000CDF6
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000EBFE File Offset: 0x0000CDFE
		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				if (value < Formatting.None || value > Formatting.Indented)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._formatting = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000EC1A File Offset: 0x0000CE1A
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000EC22 File Offset: 0x0000CE22
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling;
			}
			set
			{
				if (value < DateFormatHandling.IsoDateFormat || value > DateFormatHandling.MicrosoftDateFormat)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateFormatHandling = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000EC3E File Offset: 0x0000CE3E
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000EC46 File Offset: 0x0000CE46
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateTimeZoneHandling = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000EC62 File Offset: 0x0000CE62
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000EC6A File Offset: 0x0000CE6A
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling;
			}
			set
			{
				if (value < StringEscapeHandling.Default || value > StringEscapeHandling.EscapeHtml)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._stringEscapeHandling = value;
				this.OnStringEscapeHandlingChanged();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		internal virtual void OnStringEscapeHandlingChanged()
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000EC8E File Offset: 0x0000CE8E
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000EC96 File Offset: 0x0000CE96
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling;
			}
			set
			{
				if (value < FloatFormatHandling.String || value > FloatFormatHandling.DefaultValue)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatFormatHandling = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000ECB2 File Offset: 0x0000CEB2
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000ECBA File Offset: 0x0000CEBA
		[Nullable(2)]
		public string DateFormatString
		{
			[NullableContext(2)]
			get
			{
				return this._dateFormatString;
			}
			[NullableContext(2)]
			set
			{
				this._dateFormatString = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000ECC3 File Offset: 0x0000CEC3
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000ECDD File Offset: 0x0000CEDD
		protected JsonWriter()
		{
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this.CloseOutput = true;
			this.AutoCompleteOnClose = true;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000ED08 File Offset: 0x0000CF08
		internal void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000ED27 File Offset: 0x0000CF27
		private void Push(JsonContainerType value)
		{
			if (this._currentPosition.Type != JsonContainerType.None)
			{
				if (this._stack == null)
				{
					this._stack = new List<JsonPosition>();
				}
				this._stack.Add(this._currentPosition);
			}
			this._currentPosition = new JsonPosition(value);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000ED68 File Offset: 0x0000CF68
		private JsonContainerType Pop()
		{
			ref JsonPosition currentPosition = this._currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				this._currentPosition = default(JsonPosition);
			}
			return currentPosition.Type;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000EDDA File Offset: 0x0000CFDA
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x060003C6 RID: 966
		public abstract void Flush();

		// Token: 0x060003C7 RID: 967 RVA: 0x0000EDE7 File Offset: 0x0000CFE7
		public virtual void Close()
		{
			if (this.AutoCompleteOnClose)
			{
				this.AutoCompleteAll();
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000EDF7 File Offset: 0x0000CFF7
		public virtual void WriteStartObject()
		{
			this.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000EE01 File Offset: 0x0000D001
		public virtual void WriteEndObject()
		{
			this.InternalWriteEnd(JsonContainerType.Object);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000EE0A File Offset: 0x0000D00A
		public virtual void WriteStartArray()
		{
			this.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000EE14 File Offset: 0x0000D014
		public virtual void WriteEndArray()
		{
			this.InternalWriteEnd(JsonContainerType.Array);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000EE1D File Offset: 0x0000D01D
		public virtual void WriteStartConstructor(string name)
		{
			this.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000EE27 File Offset: 0x0000D027
		public virtual void WriteEndConstructor()
		{
			this.InternalWriteEnd(JsonContainerType.Constructor);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000EE30 File Offset: 0x0000D030
		public virtual void WritePropertyName(string name)
		{
			this.InternalWritePropertyName(name);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000EE39 File Offset: 0x0000D039
		public virtual void WritePropertyName(string name, bool escape)
		{
			this.WritePropertyName(name);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000EE42 File Offset: 0x0000D042
		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000EE50 File Offset: 0x0000D050
		public void WriteToken(JsonReader reader)
		{
			this.WriteToken(reader, true);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000EE5A File Offset: 0x0000D05A
		public void WriteToken(JsonReader reader, bool writeChildren)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this.WriteToken(reader, writeChildren, true, true);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000EE74 File Offset: 0x0000D074
		[NullableContext(2)]
		public void WriteToken(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
				this.WriteStartObject();
				return;
			case JsonToken.StartArray:
				this.WriteStartArray();
				return;
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteStartConstructor(value.ToString());
				return;
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WritePropertyName(value.ToString());
				return;
			case JsonToken.Comment:
				this.WriteComment((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Raw:
				this.WriteRawValue((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteValue(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal)
				{
					decimal value2 = (decimal)value;
					this.WriteValue(value2);
					return;
				}
				if (value is double)
				{
					double value3 = (double)value;
					this.WriteValue(value3);
					return;
				}
				if (value is float)
				{
					float value4 = (float)value;
					this.WriteValue(value4);
					return;
				}
				this.WriteValue(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.String:
				this.WriteValue((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteValue(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Null:
				this.WriteNull();
				return;
			case JsonToken.Undefined:
				this.WriteUndefined();
				return;
			case JsonToken.EndObject:
				this.WriteEndObject();
				return;
			case JsonToken.EndArray:
				this.WriteEndArray();
				return;
			case JsonToken.EndConstructor:
				this.WriteEndConstructor();
				return;
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is DateTimeOffset)
				{
					DateTimeOffset value5 = (DateTimeOffset)value;
					this.WriteValue(value5);
					return;
				}
				this.WriteValue(Convert.ToDateTime(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is Guid)
				{
					Guid value6 = (Guid)value;
					this.WriteValue(value6);
					return;
				}
				this.WriteValue((byte[])value);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F085 File Offset: 0x0000D285
		public void WriteToken(JsonToken token)
		{
			this.WriteToken(token, null);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000F090 File Offset: 0x0000D290
		internal virtual void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			int num = this.CalculateWriteTokenInitialDepth(reader);
			for (;;)
			{
				if (!writeDateConstructorAsDate || reader.TokenType != JsonToken.StartConstructor)
				{
					goto IL_3C;
				}
				object value = reader.Value;
				if (!string.Equals((value != null) ? value.ToString() : null, "Date", 4))
				{
					goto IL_3C;
				}
				this.WriteConstructorDate(reader);
				IL_5B:
				if (num - 1 >= reader.Depth - ((JsonTokenUtils.IsEndToken(reader.TokenType) > false) ? 1 : 0) || !writeChildren || !reader.Read())
				{
					break;
				}
				continue;
				IL_3C:
				if (writeComments || reader.TokenType != JsonToken.Comment)
				{
					this.WriteToken(reader.TokenType, reader.Value);
					goto IL_5B;
				}
				goto IL_5B;
			}
			if (this.IsWriteTokenIncomplete(reader, writeChildren, num))
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000F138 File Offset: 0x0000D338
		private bool IsWriteTokenIncomplete(JsonReader reader, bool writeChildren, int initialDepth)
		{
			int num = this.CalculateWriteTokenFinalDepth(reader);
			return initialDepth < num || (writeChildren && initialDepth == num && JsonTokenUtils.IsStartToken(reader.TokenType));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000F168 File Offset: 0x0000D368
		private int CalculateWriteTokenInitialDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsStartToken(tokenType))
			{
				return reader.Depth + 1;
			}
			return reader.Depth;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000F198 File Offset: 0x0000D398
		private int CalculateWriteTokenFinalDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsEndToken(tokenType))
			{
				return reader.Depth;
			}
			return reader.Depth - 1;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		private void WriteConstructorDate(JsonReader reader)
		{
			DateTime value;
			string message;
			if (!JavaScriptUtils.TryGetDateFromConstructorJson(reader, out value, out message))
			{
				throw JsonWriterException.Create(this, message, null);
			}
			this.WriteValue(value);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		private void WriteEnd(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				this.WriteEndObject();
				return;
			case JsonContainerType.Array:
				this.WriteEndArray();
				return;
			case JsonContainerType.Constructor:
				this.WriteEndConstructor();
				return;
			default:
				throw JsonWriterException.Create(this, "Unexpected type when writing end: " + type.ToString(), null);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000F24A File Offset: 0x0000D44A
		private void AutoCompleteAll()
		{
			while (this.Top > 0)
			{
				this.WriteEnd();
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000F25D File Offset: 0x0000D45D
		private JsonToken GetCloseTokenForType(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				return JsonToken.EndObject;
			case JsonContainerType.Array:
				return JsonToken.EndArray;
			case JsonContainerType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw JsonWriterException.Create(this, "No close token for type: " + type.ToString(), null);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000F29C File Offset: 0x0000D49C
		private void AutoCompleteClose(JsonContainerType type)
		{
			int num = this.CalculateLevelsToComplete(type);
			for (int i = 0; i < num; i++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteNull();
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
				this.UpdateCurrentState();
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000F308 File Offset: 0x0000D508
		private int CalculateLevelsToComplete(JsonContainerType type)
		{
			int num = 0;
			if (this._currentPosition.Type == type)
			{
				num = 1;
			}
			else
			{
				int num2 = this.Top - 2;
				for (int i = num2; i >= 0; i--)
				{
					int num3 = num2 - i;
					if (this._stack[num3].Type == type)
					{
						num = i + 2;
						break;
					}
				}
			}
			if (num == 0)
			{
				throw JsonWriterException.Create(this, "No token to close.", null);
			}
			return num;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000F370 File Offset: 0x0000D570
		private void UpdateCurrentState()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this._currentState = JsonWriter.State.Start;
				return;
			case JsonContainerType.Object:
				this._currentState = JsonWriter.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonWriter.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonWriter.State.Array;
				return;
			default:
				throw JsonWriterException.Create(this, "Unknown JsonType: " + jsonContainerType.ToString(), null);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000F3DA File Offset: 0x0000D5DA
		protected virtual void WriteEnd(JsonToken token)
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000F3DC File Offset: 0x0000D5DC
		protected virtual void WriteIndent()
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000F3DE File Offset: 0x0000D5DE
		protected virtual void WriteValueDelimiter()
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		protected virtual void WriteIndentSpace()
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), this._currentState.ToString()), null);
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			if (this._formatting == Formatting.Indented)
			{
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteIndentSpace();
				}
				if (this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.ArrayStart || this._currentState == JsonWriter.State.Constructor || this._currentState == JsonWriter.State.ConstructorStart || (tokenBeingWritten == JsonToken.PropertyName && this._currentState != JsonWriter.State.Start))
				{
					this.WriteIndent();
				}
			}
			this._currentState = state;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		public virtual void WriteNull()
		{
			this.InternalWriteValue(JsonToken.Null);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000F4BE File Offset: 0x0000D6BE
		public virtual void WriteUndefined()
		{
			this.InternalWriteValue(JsonToken.Undefined);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		[NullableContext(2)]
		public virtual void WriteRaw(string json)
		{
			this.InternalWriteRaw();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		[NullableContext(2)]
		public virtual void WriteRawValue(string json)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000F4E7 File Offset: 0x0000D6E7
		[NullableContext(2)]
		public virtual void WriteValue(string value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000F4F1 File Offset: 0x0000D6F1
		public virtual void WriteValue(int value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000F4FA File Offset: 0x0000D6FA
		[CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000F503 File Offset: 0x0000D703
		public virtual void WriteValue(long value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000F50C File Offset: 0x0000D70C
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000F515 File Offset: 0x0000D715
		public virtual void WriteValue(float value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F51E File Offset: 0x0000D71E
		public virtual void WriteValue(double value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000F527 File Offset: 0x0000D727
		public virtual void WriteValue(bool value)
		{
			this.InternalWriteValue(JsonToken.Boolean);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F531 File Offset: 0x0000D731
		public virtual void WriteValue(short value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F53A File Offset: 0x0000D73A
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F543 File Offset: 0x0000D743
		public virtual void WriteValue(char value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F54D File Offset: 0x0000D74D
		public virtual void WriteValue(byte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F556 File Offset: 0x0000D756
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000F55F File Offset: 0x0000D75F
		public virtual void WriteValue(decimal value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F568 File Offset: 0x0000D768
		public virtual void WriteValue(DateTime value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000F572 File Offset: 0x0000D772
		public virtual void WriteValue(DateTimeOffset value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000F57C File Offset: 0x0000D77C
		public virtual void WriteValue(Guid value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000F586 File Offset: 0x0000D786
		public virtual void WriteValue(TimeSpan value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000F590 File Offset: 0x0000D790
		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000F5AF File Offset: 0x0000D7AF
		[CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000F5CE File Offset: 0x0000D7CE
		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000F5ED File Offset: 0x0000D7ED
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000F60C File Offset: 0x0000D80C
		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F62B File Offset: 0x0000D82B
		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F64A File Offset: 0x0000D84A
		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000F669 File Offset: 0x0000D869
		public virtual void WriteValue(short? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000F688 File Offset: 0x0000D888
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000F6A7 File Offset: 0x0000D8A7
		public virtual void WriteValue(char? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000F6C6 File Offset: 0x0000D8C6
		public virtual void WriteValue(byte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000F6E5 File Offset: 0x0000D8E5
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000F704 File Offset: 0x0000D904
		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000F723 File Offset: 0x0000D923
		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000F742 File Offset: 0x0000D942
		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F761 File Offset: 0x0000D961
		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F780 File Offset: 0x0000D980
		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F79F File Offset: 0x0000D99F
		[NullableContext(2)]
		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.Bytes);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F7B3 File Offset: 0x0000D9B3
		[NullableContext(2)]
		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F7CD File Offset: 0x0000D9CD
		[NullableContext(2)]
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			JsonWriter.WriteValue(this, ConvertUtils.GetTypeCode(value.GetType()), value);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F7EB File Offset: 0x0000D9EB
		[NullableContext(2)]
		public virtual void WriteComment(string text)
		{
			this.InternalWriteComment();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F7F3 File Offset: 0x0000D9F3
		public virtual void WriteWhitespace(string ws)
		{
			this.InternalWriteWhitespace(ws);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000F80B File Offset: 0x0000DA0B
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonWriter.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F824 File Offset: 0x0000DA24
		internal static void WriteValue(JsonWriter writer, PrimitiveTypeCode typeCode, object value)
		{
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
				writer.WriteValue((char)value);
				return;
			case PrimitiveTypeCode.CharNullable:
				writer.WriteValue((value == null) ? default(char?) : new char?((char)value));
				return;
			case PrimitiveTypeCode.Boolean:
				writer.WriteValue((bool)value);
				return;
			case PrimitiveTypeCode.BooleanNullable:
				writer.WriteValue((value == null) ? default(bool?) : new bool?((bool)value));
				return;
			case PrimitiveTypeCode.SByte:
				writer.WriteValue((sbyte)value);
				return;
			case PrimitiveTypeCode.SByteNullable:
				writer.WriteValue((value == null) ? default(sbyte?) : new sbyte?((sbyte)value));
				return;
			case PrimitiveTypeCode.Int16:
				writer.WriteValue((short)value);
				return;
			case PrimitiveTypeCode.Int16Nullable:
				writer.WriteValue((value == null) ? default(short?) : new short?((short)value));
				return;
			case PrimitiveTypeCode.UInt16:
				writer.WriteValue((ushort)value);
				return;
			case PrimitiveTypeCode.UInt16Nullable:
				writer.WriteValue((value == null) ? default(ushort?) : new ushort?((ushort)value));
				return;
			case PrimitiveTypeCode.Int32:
				writer.WriteValue((int)value);
				return;
			case PrimitiveTypeCode.Int32Nullable:
				writer.WriteValue((value == null) ? default(int?) : new int?((int)value));
				return;
			case PrimitiveTypeCode.Byte:
				writer.WriteValue((byte)value);
				return;
			case PrimitiveTypeCode.ByteNullable:
				writer.WriteValue((value == null) ? default(byte?) : new byte?((byte)value));
				return;
			case PrimitiveTypeCode.UInt32:
				writer.WriteValue((uint)value);
				return;
			case PrimitiveTypeCode.UInt32Nullable:
				writer.WriteValue((value == null) ? default(uint?) : new uint?((uint)value));
				return;
			case PrimitiveTypeCode.Int64:
				writer.WriteValue((long)value);
				return;
			case PrimitiveTypeCode.Int64Nullable:
				writer.WriteValue((value == null) ? default(long?) : new long?((long)value));
				return;
			case PrimitiveTypeCode.UInt64:
				writer.WriteValue((ulong)value);
				return;
			case PrimitiveTypeCode.UInt64Nullable:
				writer.WriteValue((value == null) ? default(ulong?) : new ulong?((ulong)value));
				return;
			case PrimitiveTypeCode.Single:
				writer.WriteValue((float)value);
				return;
			case PrimitiveTypeCode.SingleNullable:
				writer.WriteValue((value == null) ? default(float?) : new float?((float)value));
				return;
			case PrimitiveTypeCode.Double:
				writer.WriteValue((double)value);
				return;
			case PrimitiveTypeCode.DoubleNullable:
				writer.WriteValue((value == null) ? default(double?) : new double?((double)value));
				return;
			case PrimitiveTypeCode.DateTime:
				writer.WriteValue((DateTime)value);
				return;
			case PrimitiveTypeCode.DateTimeNullable:
				writer.WriteValue((value == null) ? default(DateTime?) : new DateTime?((DateTime)value));
				return;
			case PrimitiveTypeCode.DateTimeOffset:
				writer.WriteValue((DateTimeOffset)value);
				return;
			case PrimitiveTypeCode.DateTimeOffsetNullable:
				writer.WriteValue((value == null) ? default(DateTimeOffset?) : new DateTimeOffset?((DateTimeOffset)value));
				return;
			case PrimitiveTypeCode.Decimal:
				writer.WriteValue((decimal)value);
				return;
			case PrimitiveTypeCode.DecimalNullable:
				writer.WriteValue((value == null) ? default(decimal?) : new decimal?((decimal)value));
				return;
			case PrimitiveTypeCode.Guid:
				writer.WriteValue((Guid)value);
				return;
			case PrimitiveTypeCode.GuidNullable:
				writer.WriteValue((value == null) ? default(Guid?) : new Guid?((Guid)value));
				return;
			case PrimitiveTypeCode.TimeSpan:
				writer.WriteValue((TimeSpan)value);
				return;
			case PrimitiveTypeCode.TimeSpanNullable:
				writer.WriteValue((value == null) ? default(TimeSpan?) : new TimeSpan?((TimeSpan)value));
				return;
			case PrimitiveTypeCode.Uri:
				writer.WriteValue((Uri)value);
				return;
			case PrimitiveTypeCode.String:
				writer.WriteValue((string)value);
				return;
			case PrimitiveTypeCode.Bytes:
				writer.WriteValue((byte[])value);
				return;
			}
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			throw JsonWriter.CreateUnsupportedTypeException(writer, value);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		private static JsonWriterException CreateUnsupportedTypeException(JsonWriter writer, object value)
		{
			return JsonWriterException.Create(writer, "Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()), null);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		protected void SetWriteState(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.StartObject:
				this.InternalWriteStart(token, JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this.InternalWriteStart(token, JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this.InternalWriteStart(token, JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
			{
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException("A name is required when setting property name state.", "value");
				}
				this.InternalWritePropertyName(text);
				return;
			}
			case JsonToken.Comment:
				this.InternalWriteComment();
				return;
			case JsonToken.Raw:
				this.InternalWriteRaw();
				return;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.InternalWriteValue(token);
				return;
			case JsonToken.EndObject:
				this.InternalWriteEnd(JsonContainerType.Object);
				return;
			case JsonToken.EndArray:
				this.InternalWriteEnd(JsonContainerType.Array);
				return;
			case JsonToken.EndConstructor:
				this.InternalWriteEnd(JsonContainerType.Constructor);
				return;
			default:
				throw new ArgumentOutOfRangeException("token");
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		internal void InternalWriteEnd(JsonContainerType container)
		{
			this.AutoCompleteClose(container);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000FD15 File Offset: 0x0000DF15
		internal void InternalWritePropertyName(string name)
		{
			this._currentPosition.PropertyName = name;
			this.AutoComplete(JsonToken.PropertyName);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000FD2A File Offset: 0x0000DF2A
		internal void InternalWriteRaw()
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		internal void InternalWriteStart(JsonToken token, JsonContainerType container)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
			this.Push(container);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000FD42 File Offset: 0x0000DF42
		internal void InternalWriteValue(JsonToken token)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000FD51 File Offset: 0x0000DF51
		internal void InternalWriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw JsonWriterException.Create(this, "Only white space characters should be used.", null);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000FD6B File Offset: 0x0000DF6B
		internal void InternalWriteComment()
		{
			this.AutoComplete(JsonToken.Comment);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000FD74 File Offset: 0x0000DF74
		[CompilerGenerated]
		private Task <InternalWriteEndAsync>g__AwaitProperty|11_0(Task task, int LevelsToComplete, JsonToken token, CancellationToken CancellationToken)
		{
			JsonWriter.<<InternalWriteEndAsync>g__AwaitProperty|11_0>d <<InternalWriteEndAsync>g__AwaitProperty|11_0>d;
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.<>4__this = this;
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.task = task;
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.LevelsToComplete = LevelsToComplete;
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.token = token;
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.CancellationToken = CancellationToken;
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.<>1__state = -1;
			<<InternalWriteEndAsync>g__AwaitProperty|11_0>d.<>t__builder.Start<JsonWriter.<<InternalWriteEndAsync>g__AwaitProperty|11_0>d>(ref <<InternalWriteEndAsync>g__AwaitProperty|11_0>d);
			return <<InternalWriteEndAsync>g__AwaitProperty|11_0>d.<>t__builder.Task;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		[CompilerGenerated]
		private Task <InternalWriteEndAsync>g__AwaitIndent|11_1(Task task, int LevelsToComplete, JsonToken token, CancellationToken CancellationToken)
		{
			JsonWriter.<<InternalWriteEndAsync>g__AwaitIndent|11_1>d <<InternalWriteEndAsync>g__AwaitIndent|11_1>d;
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.<>4__this = this;
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.task = task;
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.LevelsToComplete = LevelsToComplete;
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.token = token;
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.CancellationToken = CancellationToken;
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.<>1__state = -1;
			<<InternalWriteEndAsync>g__AwaitIndent|11_1>d.<>t__builder.Start<JsonWriter.<<InternalWriteEndAsync>g__AwaitIndent|11_1>d>(ref <<InternalWriteEndAsync>g__AwaitIndent|11_1>d);
			return <<InternalWriteEndAsync>g__AwaitIndent|11_1>d.<>t__builder.Task;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000FE3C File Offset: 0x0000E03C
		[CompilerGenerated]
		private Task <InternalWriteEndAsync>g__AwaitEnd|11_2(Task task, int LevelsToComplete, CancellationToken CancellationToken)
		{
			JsonWriter.<<InternalWriteEndAsync>g__AwaitEnd|11_2>d <<InternalWriteEndAsync>g__AwaitEnd|11_2>d;
			<<InternalWriteEndAsync>g__AwaitEnd|11_2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<InternalWriteEndAsync>g__AwaitEnd|11_2>d.<>4__this = this;
			<<InternalWriteEndAsync>g__AwaitEnd|11_2>d.task = task;
			<<InternalWriteEndAsync>g__AwaitEnd|11_2>d.LevelsToComplete = LevelsToComplete;
			<<InternalWriteEndAsync>g__AwaitEnd|11_2>d.CancellationToken = CancellationToken;
			<<InternalWriteEndAsync>g__AwaitEnd|11_2>d.<>1__state = -1;
			<<InternalWriteEndAsync>g__AwaitEnd|11_2>d.<>t__builder.Start<JsonWriter.<<InternalWriteEndAsync>g__AwaitEnd|11_2>d>(ref <<InternalWriteEndAsync>g__AwaitEnd|11_2>d);
			return <<InternalWriteEndAsync>g__AwaitEnd|11_2>d.<>t__builder.Task;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FE98 File Offset: 0x0000E098
		[CompilerGenerated]
		private Task <InternalWriteEndAsync>g__AwaitRemaining|11_3(int LevelsToComplete, CancellationToken CancellationToken)
		{
			JsonWriter.<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d <<InternalWriteEndAsync>g__AwaitRemaining|11_3>d;
			<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d.<>4__this = this;
			<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d.LevelsToComplete = LevelsToComplete;
			<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d.CancellationToken = CancellationToken;
			<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d.<>1__state = -1;
			<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d.<>t__builder.Start<JsonWriter.<<InternalWriteEndAsync>g__AwaitRemaining|11_3>d>(ref <<InternalWriteEndAsync>g__AwaitRemaining|11_3>d);
			return <<InternalWriteEndAsync>g__AwaitRemaining|11_3>d.<>t__builder.Task;
		}

		// Token: 0x04000127 RID: 295
		private static readonly JsonWriter.State[][] StateArray;

		// Token: 0x04000128 RID: 296
		internal static readonly JsonWriter.State[][] StateArrayTemplate = new JsonWriter.State[][]
		{
			new JsonWriter.State[]
			{
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Property,
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Object,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Array,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			}
		};

		// Token: 0x04000129 RID: 297
		[Nullable(2)]
		private List<JsonPosition> _stack;

		// Token: 0x0400012A RID: 298
		private JsonPosition _currentPosition;

		// Token: 0x0400012B RID: 299
		private JsonWriter.State _currentState;

		// Token: 0x0400012C RID: 300
		private Formatting _formatting;

		// Token: 0x0400012F RID: 303
		private DateFormatHandling _dateFormatHandling;

		// Token: 0x04000130 RID: 304
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x04000131 RID: 305
		private StringEscapeHandling _stringEscapeHandling;

		// Token: 0x04000132 RID: 306
		private FloatFormatHandling _floatFormatHandling;

		// Token: 0x04000133 RID: 307
		[Nullable(2)]
		private string _dateFormatString;

		// Token: 0x04000134 RID: 308
		[Nullable(2)]
		private CultureInfo _culture;

		// Token: 0x02000158 RID: 344
		[NullableContext(0)]
		internal enum State
		{
			// Token: 0x04000662 RID: 1634
			Start,
			// Token: 0x04000663 RID: 1635
			Property,
			// Token: 0x04000664 RID: 1636
			ObjectStart,
			// Token: 0x04000665 RID: 1637
			Object,
			// Token: 0x04000666 RID: 1638
			ArrayStart,
			// Token: 0x04000667 RID: 1639
			Array,
			// Token: 0x04000668 RID: 1640
			ConstructorStart,
			// Token: 0x04000669 RID: 1641
			Constructor,
			// Token: 0x0400066A RID: 1642
			Closed,
			// Token: 0x0400066B RID: 1643
			Error
		}
	}
}

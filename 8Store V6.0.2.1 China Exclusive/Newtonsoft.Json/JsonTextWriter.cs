using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonTextWriter : JsonWriter
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000A07E File Offset: 0x0000827E
		public override Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.FlushAsync(cancellationToken);
			}
			return this.DoFlushAsync(cancellationToken);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A097 File Offset: 0x00008297
		internal Task DoFlushAsync(CancellationToken cancellationToken)
		{
			return cancellationToken.CancelIfRequestedAsync() ?? this._writer.FlushAsync();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A0AE File Offset: 0x000082AE
		protected override Task WriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteValueDelimiterAsync(cancellationToken);
			}
			return this.DoWriteValueDelimiterAsync(cancellationToken);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A0C7 File Offset: 0x000082C7
		internal Task DoWriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(',', cancellationToken);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A0D7 File Offset: 0x000082D7
		protected override Task WriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteEndAsync(token, cancellationToken);
			}
			return this.DoWriteEndAsync(token, cancellationToken);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A0F4 File Offset: 0x000082F4
		internal Task DoWriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return this._writer.WriteAsync('}', cancellationToken);
			case JsonToken.EndArray:
				return this._writer.WriteAsync(']', cancellationToken);
			case JsonToken.EndConstructor:
				return this._writer.WriteAsync(')', cancellationToken);
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token.ToString(), null);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A163 File Offset: 0x00008363
		public override Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.CloseAsync(cancellationToken);
			}
			return this.DoCloseAsync(cancellationToken);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A17C File Offset: 0x0000837C
		internal Task DoCloseAsync(CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoCloseAsync>d__8 <DoCloseAsync>d__;
			<DoCloseAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoCloseAsync>d__.<>4__this = this;
			<DoCloseAsync>d__.cancellationToken = cancellationToken;
			<DoCloseAsync>d__.<>1__state = -1;
			<DoCloseAsync>d__.<>t__builder.Start<JsonTextWriter.<DoCloseAsync>d__8>(ref <DoCloseAsync>d__);
			return <DoCloseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A1C8 File Offset: 0x000083C8
		private Task CloseBufferAndWriterAsync()
		{
			JsonTextWriter.<CloseBufferAndWriterAsync>d__9 <CloseBufferAndWriterAsync>d__;
			<CloseBufferAndWriterAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CloseBufferAndWriterAsync>d__.<>4__this = this;
			<CloseBufferAndWriterAsync>d__.<>1__state = -1;
			<CloseBufferAndWriterAsync>d__.<>t__builder.Start<JsonTextWriter.<CloseBufferAndWriterAsync>d__9>(ref <CloseBufferAndWriterAsync>d__);
			return <CloseBufferAndWriterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A20B File Offset: 0x0000840B
		public override Task WriteEndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndAsync(cancellationToken);
			}
			return base.WriteEndInternalAsync(cancellationToken);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A224 File Offset: 0x00008424
		protected override Task WriteIndentAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteIndentAsync(cancellationToken);
			}
			return this.DoWriteIndentAsync(cancellationToken);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A240 File Offset: 0x00008440
		internal Task DoWriteIndentAsync(CancellationToken cancellationToken)
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			if (num <= 12)
			{
				return this._writer.WriteAsync(this._indentChars, 0, num2 + num, cancellationToken);
			}
			return this.WriteIndentAsync(num, num2, cancellationToken);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A288 File Offset: 0x00008488
		private Task WriteIndentAsync(int currentIndentCount, int newLineLen, CancellationToken cancellationToken)
		{
			JsonTextWriter.<WriteIndentAsync>d__13 <WriteIndentAsync>d__;
			<WriteIndentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteIndentAsync>d__.<>4__this = this;
			<WriteIndentAsync>d__.currentIndentCount = currentIndentCount;
			<WriteIndentAsync>d__.newLineLen = newLineLen;
			<WriteIndentAsync>d__.cancellationToken = cancellationToken;
			<WriteIndentAsync>d__.<>1__state = -1;
			<WriteIndentAsync>d__.<>t__builder.Start<JsonTextWriter.<WriteIndentAsync>d__13>(ref <WriteIndentAsync>d__);
			return <WriteIndentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A2E4 File Offset: 0x000084E4
		private Task WriteValueInternalAsync(JsonToken token, string value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(token, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync(value, cancellationToken);
			}
			return this.WriteValueInternalAsync(task, value, cancellationToken);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A31C File Offset: 0x0000851C
		private Task WriteValueInternalAsync(Task task, string value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<WriteValueInternalAsync>d__15 <WriteValueInternalAsync>d__;
			<WriteValueInternalAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteValueInternalAsync>d__.<>4__this = this;
			<WriteValueInternalAsync>d__.task = task;
			<WriteValueInternalAsync>d__.value = value;
			<WriteValueInternalAsync>d__.cancellationToken = cancellationToken;
			<WriteValueInternalAsync>d__.<>1__state = -1;
			<WriteValueInternalAsync>d__.<>t__builder.Start<JsonTextWriter.<WriteValueInternalAsync>d__15>(ref <WriteValueInternalAsync>d__);
			return <WriteValueInternalAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A377 File Offset: 0x00008577
		protected override Task WriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteIndentSpaceAsync(cancellationToken);
			}
			return this.DoWriteIndentSpaceAsync(cancellationToken);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A390 File Offset: 0x00008590
		internal Task DoWriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(' ', cancellationToken);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public override Task WriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteRawAsync(json, cancellationToken);
			}
			return this.DoWriteRawAsync(json, cancellationToken);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A3BB File Offset: 0x000085BB
		internal Task DoWriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(json, cancellationToken);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A3CA File Offset: 0x000085CA
		public override Task WriteNullAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteNullAsync(cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A3E3 File Offset: 0x000085E3
		internal Task DoWriteNullAsync(CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Null, JsonConvert.Null, cancellationToken);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A3F4 File Offset: 0x000085F4
		private Task WriteDigitsAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			if (uvalue <= 9UL & !negative)
			{
				return this._writer.WriteAsync((char)(48UL + uvalue), cancellationToken);
			}
			int count = this.WriteNumberToBuffer(uvalue, negative);
			return this._writer.WriteAsync(this._writeBuffer, 0, count, cancellationToken);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A444 File Offset: 0x00008644
		private Task WriteIntegerValueAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.Integer, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteDigitsAsync(uvalue, negative, cancellationToken);
			}
			return this.WriteIntegerValueAsync(task, uvalue, negative, cancellationToken);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A478 File Offset: 0x00008678
		private Task WriteIntegerValueAsync(Task task, ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			JsonTextWriter.<WriteIntegerValueAsync>d__24 <WriteIntegerValueAsync>d__;
			<WriteIntegerValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteIntegerValueAsync>d__.<>4__this = this;
			<WriteIntegerValueAsync>d__.task = task;
			<WriteIntegerValueAsync>d__.uvalue = uvalue;
			<WriteIntegerValueAsync>d__.negative = negative;
			<WriteIntegerValueAsync>d__.cancellationToken = cancellationToken;
			<WriteIntegerValueAsync>d__.<>1__state = -1;
			<WriteIntegerValueAsync>d__.<>t__builder.Start<JsonTextWriter.<WriteIntegerValueAsync>d__24>(ref <WriteIntegerValueAsync>d__);
			return <WriteIntegerValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A4DC File Offset: 0x000086DC
		internal Task WriteIntegerValueAsync(long value, CancellationToken cancellationToken)
		{
			bool flag = value < 0L;
			if (flag)
			{
				value = -value;
			}
			return this.WriteIntegerValueAsync((ulong)value, flag, cancellationToken);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A4FF File Offset: 0x000086FF
		internal Task WriteIntegerValueAsync(ulong uvalue, CancellationToken cancellationToken)
		{
			return this.WriteIntegerValueAsync(uvalue, false, cancellationToken);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A50C File Offset: 0x0000870C
		private Task WriteEscapedStringAsync(string value, bool quote, CancellationToken cancellationToken)
		{
			return JavaScriptUtils.WriteEscapedJavaScriptStringAsync(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this, this._writeBuffer, cancellationToken);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A540 File Offset: 0x00008740
		public override Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WritePropertyNameAsync(name, cancellationToken);
			}
			return this.DoWritePropertyNameAsync(name, cancellationToken);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A55C File Offset: 0x0000875C
		internal Task DoWritePropertyNameAsync(string name, CancellationToken cancellationToken)
		{
			Task task = base.InternalWritePropertyNameAsync(name, cancellationToken);
			if (!task.IsCompletedSuccessfully())
			{
				return this.DoWritePropertyNameAsync(task, name, cancellationToken);
			}
			task = this.WriteEscapedStringAsync(name, this._quoteName, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync(':', cancellationToken);
			}
			return JavaScriptUtils.WriteCharAsync(task, this._writer, ':', cancellationToken);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A5BC File Offset: 0x000087BC
		private Task DoWritePropertyNameAsync(Task task, string name, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWritePropertyNameAsync>d__30 <DoWritePropertyNameAsync>d__;
			<DoWritePropertyNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWritePropertyNameAsync>d__.<>4__this = this;
			<DoWritePropertyNameAsync>d__.task = task;
			<DoWritePropertyNameAsync>d__.name = name;
			<DoWritePropertyNameAsync>d__.cancellationToken = cancellationToken;
			<DoWritePropertyNameAsync>d__.<>1__state = -1;
			<DoWritePropertyNameAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWritePropertyNameAsync>d__30>(ref <DoWritePropertyNameAsync>d__);
			return <DoWritePropertyNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A617 File Offset: 0x00008817
		public override Task WritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WritePropertyNameAsync(name, escape, cancellationToken);
			}
			return this.DoWritePropertyNameAsync(name, escape, cancellationToken);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A634 File Offset: 0x00008834
		internal Task DoWritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWritePropertyNameAsync>d__32 <DoWritePropertyNameAsync>d__;
			<DoWritePropertyNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWritePropertyNameAsync>d__.<>4__this = this;
			<DoWritePropertyNameAsync>d__.name = name;
			<DoWritePropertyNameAsync>d__.escape = escape;
			<DoWritePropertyNameAsync>d__.cancellationToken = cancellationToken;
			<DoWritePropertyNameAsync>d__.<>1__state = -1;
			<DoWritePropertyNameAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWritePropertyNameAsync>d__32>(ref <DoWritePropertyNameAsync>d__);
			return <DoWritePropertyNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A68F File Offset: 0x0000888F
		public override Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartArrayAsync(cancellationToken);
			}
			return this.DoWriteStartArrayAsync(cancellationToken);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A6A8 File Offset: 0x000088A8
		internal Task DoWriteStartArrayAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteStartAsync(JsonToken.StartArray, JsonContainerType.Array, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync('[', cancellationToken);
			}
			return this.DoWriteStartArrayAsync(task, cancellationToken);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A6E0 File Offset: 0x000088E0
		internal Task DoWriteStartArrayAsync(Task task, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteStartArrayAsync>d__35 <DoWriteStartArrayAsync>d__;
			<DoWriteStartArrayAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteStartArrayAsync>d__.<>4__this = this;
			<DoWriteStartArrayAsync>d__.task = task;
			<DoWriteStartArrayAsync>d__.cancellationToken = cancellationToken;
			<DoWriteStartArrayAsync>d__.<>1__state = -1;
			<DoWriteStartArrayAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteStartArrayAsync>d__35>(ref <DoWriteStartArrayAsync>d__);
			return <DoWriteStartArrayAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A733 File Offset: 0x00008933
		public override Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartObjectAsync(cancellationToken);
			}
			return this.DoWriteStartObjectAsync(cancellationToken);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A74C File Offset: 0x0000894C
		internal Task DoWriteStartObjectAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteStartAsync(JsonToken.StartObject, JsonContainerType.Object, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync('{', cancellationToken);
			}
			return this.DoWriteStartObjectAsync(task, cancellationToken);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A784 File Offset: 0x00008984
		internal Task DoWriteStartObjectAsync(Task task, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteStartObjectAsync>d__38 <DoWriteStartObjectAsync>d__;
			<DoWriteStartObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteStartObjectAsync>d__.<>4__this = this;
			<DoWriteStartObjectAsync>d__.task = task;
			<DoWriteStartObjectAsync>d__.cancellationToken = cancellationToken;
			<DoWriteStartObjectAsync>d__.<>1__state = -1;
			<DoWriteStartObjectAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteStartObjectAsync>d__38>(ref <DoWriteStartObjectAsync>d__);
			return <DoWriteStartObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A7D7 File Offset: 0x000089D7
		public override Task WriteStartConstructorAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartConstructorAsync(name, cancellationToken);
			}
			return this.DoWriteStartConstructorAsync(name, cancellationToken);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A7F4 File Offset: 0x000089F4
		internal Task DoWriteStartConstructorAsync(string name, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteStartConstructorAsync>d__40 <DoWriteStartConstructorAsync>d__;
			<DoWriteStartConstructorAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteStartConstructorAsync>d__.<>4__this = this;
			<DoWriteStartConstructorAsync>d__.name = name;
			<DoWriteStartConstructorAsync>d__.cancellationToken = cancellationToken;
			<DoWriteStartConstructorAsync>d__.<>1__state = -1;
			<DoWriteStartConstructorAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteStartConstructorAsync>d__40>(ref <DoWriteStartConstructorAsync>d__);
			return <DoWriteStartConstructorAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A847 File Offset: 0x00008A47
		public override Task WriteUndefinedAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteUndefinedAsync(cancellationToken);
			}
			return this.DoWriteUndefinedAsync(cancellationToken);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A860 File Offset: 0x00008A60
		internal Task DoWriteUndefinedAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync(JsonConvert.Undefined, cancellationToken);
			}
			return this.DoWriteUndefinedAsync(task, cancellationToken);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A89C File Offset: 0x00008A9C
		private Task DoWriteUndefinedAsync(Task task, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteUndefinedAsync>d__43 <DoWriteUndefinedAsync>d__;
			<DoWriteUndefinedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteUndefinedAsync>d__.<>4__this = this;
			<DoWriteUndefinedAsync>d__.task = task;
			<DoWriteUndefinedAsync>d__.cancellationToken = cancellationToken;
			<DoWriteUndefinedAsync>d__.<>1__state = -1;
			<DoWriteUndefinedAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteUndefinedAsync>d__43>(ref <DoWriteUndefinedAsync>d__);
			return <DoWriteUndefinedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A8EF File Offset: 0x00008AEF
		public override Task WriteWhitespaceAsync(string ws, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteWhitespaceAsync(ws, cancellationToken);
			}
			return this.DoWriteWhitespaceAsync(ws, cancellationToken);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A90A File Offset: 0x00008B0A
		internal Task DoWriteWhitespaceAsync(string ws, CancellationToken cancellationToken)
		{
			base.InternalWriteWhitespace(ws);
			return this._writer.WriteAsync(ws, cancellationToken);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A920 File Offset: 0x00008B20
		public override Task WriteValueAsync(bool value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A93B File Offset: 0x00008B3B
		internal Task DoWriteValueAsync(bool value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Boolean, JsonConvert.ToString(value), cancellationToken);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A94C File Offset: 0x00008B4C
		public override Task WriteValueAsync(bool? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A967 File Offset: 0x00008B67
		internal Task DoWriteValueAsync(bool? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A988 File Offset: 0x00008B88
		public override Task WriteValueAsync(byte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		public override Task WriteValueAsync(byte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A9BF File Offset: 0x00008BBF
		internal Task DoWriteValueAsync(byte? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A9E1 File Offset: 0x00008BE1
		public override Task WriteValueAsync([Nullable(2)] byte[] value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value != null)
			{
				return this.WriteValueNonNullAsync(value, cancellationToken);
			}
			return this.WriteNullAsync(cancellationToken);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000AA08 File Offset: 0x00008C08
		internal Task WriteValueNonNullAsync(byte[] value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<WriteValueNonNullAsync>d__54 <WriteValueNonNullAsync>d__;
			<WriteValueNonNullAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteValueNonNullAsync>d__.<>4__this = this;
			<WriteValueNonNullAsync>d__.value = value;
			<WriteValueNonNullAsync>d__.cancellationToken = cancellationToken;
			<WriteValueNonNullAsync>d__.<>1__state = -1;
			<WriteValueNonNullAsync>d__.<>t__builder.Start<JsonTextWriter.<WriteValueNonNullAsync>d__54>(ref <WriteValueNonNullAsync>d__);
			return <WriteValueNonNullAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000AA5B File Offset: 0x00008C5B
		public override Task WriteValueAsync(char value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000AA76 File Offset: 0x00008C76
		internal Task DoWriteValueAsync(char value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.String, JsonConvert.ToString(value), cancellationToken);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000AA87 File Offset: 0x00008C87
		public override Task WriteValueAsync(char? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000AAA2 File Offset: 0x00008CA2
		internal Task DoWriteValueAsync(char? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000AAC3 File Offset: 0x00008CC3
		public override Task WriteValueAsync(DateTime value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		internal Task DoWriteValueAsync(DateTime value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteValueAsync>d__60 <DoWriteValueAsync>d__;
			<DoWriteValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteValueAsync>d__.<>4__this = this;
			<DoWriteValueAsync>d__.value = value;
			<DoWriteValueAsync>d__.cancellationToken = cancellationToken;
			<DoWriteValueAsync>d__.<>1__state = -1;
			<DoWriteValueAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteValueAsync>d__60>(ref <DoWriteValueAsync>d__);
			return <DoWriteValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AB33 File Offset: 0x00008D33
		public override Task WriteValueAsync(DateTime? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AB4E File Offset: 0x00008D4E
		internal Task DoWriteValueAsync(DateTime? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000AB6F File Offset: 0x00008D6F
		public override Task WriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000AB8C File Offset: 0x00008D8C
		internal Task DoWriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteValueAsync>d__64 <DoWriteValueAsync>d__;
			<DoWriteValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteValueAsync>d__.<>4__this = this;
			<DoWriteValueAsync>d__.value = value;
			<DoWriteValueAsync>d__.cancellationToken = cancellationToken;
			<DoWriteValueAsync>d__.<>1__state = -1;
			<DoWriteValueAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteValueAsync>d__64>(ref <DoWriteValueAsync>d__);
			return <DoWriteValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000ABDF File Offset: 0x00008DDF
		public override Task WriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000ABFA File Offset: 0x00008DFA
		internal Task DoWriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000AC1B File Offset: 0x00008E1B
		public override Task WriteValueAsync(decimal value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000AC36 File Offset: 0x00008E36
		internal Task DoWriteValueAsync(decimal value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value), cancellationToken);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000AC46 File Offset: 0x00008E46
		public override Task WriteValueAsync(decimal? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000AC61 File Offset: 0x00008E61
		internal Task DoWriteValueAsync(decimal? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AC82 File Offset: 0x00008E82
		public override Task WriteValueAsync(double value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteValueAsync(value, false, cancellationToken);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AC9E File Offset: 0x00008E9E
		internal Task WriteValueAsync(double value, bool nullable, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, nullable), cancellationToken);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000ACBB File Offset: 0x00008EBB
		public override Task WriteValueAsync(double? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			return this.WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000ACEE File Offset: 0x00008EEE
		public override Task WriteValueAsync(float value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteValueAsync(value, false, cancellationToken);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AD0A File Offset: 0x00008F0A
		internal Task WriteValueAsync(float value, bool nullable, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, nullable), cancellationToken);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AD27 File Offset: 0x00008F27
		public override Task WriteValueAsync(float? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			return this.WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000AD5A File Offset: 0x00008F5A
		public override Task WriteValueAsync(Guid value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000AD78 File Offset: 0x00008F78
		internal Task DoWriteValueAsync(Guid value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteValueAsync>d__78 <DoWriteValueAsync>d__;
			<DoWriteValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteValueAsync>d__.<>4__this = this;
			<DoWriteValueAsync>d__.value = value;
			<DoWriteValueAsync>d__.cancellationToken = cancellationToken;
			<DoWriteValueAsync>d__.<>1__state = -1;
			<DoWriteValueAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteValueAsync>d__78>(ref <DoWriteValueAsync>d__);
			return <DoWriteValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000ADCB File Offset: 0x00008FCB
		public override Task WriteValueAsync(Guid? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000ADE6 File Offset: 0x00008FE6
		internal Task DoWriteValueAsync(Guid? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000AE07 File Offset: 0x00009007
		public override Task WriteValueAsync(int value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000AE23 File Offset: 0x00009023
		public override Task WriteValueAsync(int? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AE3E File Offset: 0x0000903E
		internal Task DoWriteValueAsync(int? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000AE60 File Offset: 0x00009060
		public override Task WriteValueAsync(long value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync(value, cancellationToken);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000AE7B File Offset: 0x0000907B
		public override Task WriteValueAsync(long? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000AE96 File Offset: 0x00009096
		internal Task DoWriteValueAsync(long? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000AEB7 File Offset: 0x000090B7
		public override Task WriteValueAsync([Nullable(2)] object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			return JsonWriter.WriteValueAsync(this, ConvertUtils.GetTypeCode(value.GetType()), value, cancellationToken);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000AEE8 File Offset: 0x000090E8
		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000AF04 File Offset: 0x00009104
		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000AF1F File Offset: 0x0000911F
		internal Task DoWriteValueAsync(sbyte? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AF41 File Offset: 0x00009141
		public override Task WriteValueAsync(short value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000AF5D File Offset: 0x0000915D
		public override Task WriteValueAsync(short? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000AF78 File Offset: 0x00009178
		internal Task DoWriteValueAsync(short? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000AF9A File Offset: 0x0000919A
		public override Task WriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AFB8 File Offset: 0x000091B8
		internal Task DoWriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (!task.IsCompletedSuccessfully())
			{
				return this.DoWriteValueAsync(task, value, cancellationToken);
			}
			if (value != null)
			{
				return this.WriteEscapedStringAsync(value, true, cancellationToken);
			}
			return this._writer.WriteAsync(JsonConvert.Null, cancellationToken);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B000 File Offset: 0x00009200
		private Task DoWriteValueAsync(Task task, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteValueAsync>d__96 <DoWriteValueAsync>d__;
			<DoWriteValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteValueAsync>d__.<>4__this = this;
			<DoWriteValueAsync>d__.task = task;
			<DoWriteValueAsync>d__.value = value;
			<DoWriteValueAsync>d__.cancellationToken = cancellationToken;
			<DoWriteValueAsync>d__.<>1__state = -1;
			<DoWriteValueAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteValueAsync>d__96>(ref <DoWriteValueAsync>d__);
			return <DoWriteValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B05B File Offset: 0x0000925B
		public override Task WriteValueAsync(TimeSpan value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B078 File Offset: 0x00009278
		internal Task DoWriteValueAsync(TimeSpan value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteValueAsync>d__98 <DoWriteValueAsync>d__;
			<DoWriteValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteValueAsync>d__.<>4__this = this;
			<DoWriteValueAsync>d__.value = value;
			<DoWriteValueAsync>d__.cancellationToken = cancellationToken;
			<DoWriteValueAsync>d__.<>1__state = -1;
			<DoWriteValueAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteValueAsync>d__98>(ref <DoWriteValueAsync>d__);
			return <DoWriteValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B0CB File Offset: 0x000092CB
		public override Task WriteValueAsync(TimeSpan? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B0E6 File Offset: 0x000092E6
		internal Task DoWriteValueAsync(TimeSpan? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B107 File Offset: 0x00009307
		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B123 File Offset: 0x00009323
		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B13E File Offset: 0x0000933E
		internal Task DoWriteValueAsync(uint? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B160 File Offset: 0x00009360
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync(value, cancellationToken);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000B17B File Offset: 0x0000937B
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000B196 File Offset: 0x00009396
		internal Task DoWriteValueAsync(ulong? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000B1B7 File Offset: 0x000093B7
		public override Task WriteValueAsync([Nullable(2)] Uri value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!(value == null))
			{
				return this.WriteValueNotNullAsync(value, cancellationToken);
			}
			return this.WriteNullAsync(cancellationToken);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000B1E4 File Offset: 0x000093E4
		internal Task WriteValueNotNullAsync(Uri value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteEscapedStringAsync(value.OriginalString, true, cancellationToken);
			}
			return this.WriteValueNotNullAsync(task, value, cancellationToken);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B21C File Offset: 0x0000941C
		internal Task WriteValueNotNullAsync(Task task, Uri value, CancellationToken cancellationToken)
		{
			JsonTextWriter.<WriteValueNotNullAsync>d__109 <WriteValueNotNullAsync>d__;
			<WriteValueNotNullAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteValueNotNullAsync>d__.<>4__this = this;
			<WriteValueNotNullAsync>d__.task = task;
			<WriteValueNotNullAsync>d__.value = value;
			<WriteValueNotNullAsync>d__.cancellationToken = cancellationToken;
			<WriteValueNotNullAsync>d__.<>1__state = -1;
			<WriteValueNotNullAsync>d__.<>t__builder.Start<JsonTextWriter.<WriteValueNotNullAsync>d__109>(ref <WriteValueNotNullAsync>d__);
			return <WriteValueNotNullAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B277 File Offset: 0x00009477
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B293 File Offset: 0x00009493
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B2AE File Offset: 0x000094AE
		internal Task DoWriteValueAsync(ushort? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public override Task WriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteCommentAsync(text, cancellationToken);
			}
			return this.DoWriteCommentAsync(text, cancellationToken);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B2EC File Offset: 0x000094EC
		internal Task DoWriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteCommentAsync>d__114 <DoWriteCommentAsync>d__;
			<DoWriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteCommentAsync>d__.<>4__this = this;
			<DoWriteCommentAsync>d__.text = text;
			<DoWriteCommentAsync>d__.cancellationToken = cancellationToken;
			<DoWriteCommentAsync>d__.<>1__state = -1;
			<DoWriteCommentAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteCommentAsync>d__114>(ref <DoWriteCommentAsync>d__);
			return <DoWriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B33F File Offset: 0x0000953F
		public override Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndArrayAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Array, cancellationToken);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B359 File Offset: 0x00009559
		public override Task WriteEndConstructorAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndConstructorAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Constructor, cancellationToken);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B373 File Offset: 0x00009573
		public override Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndObjectAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Object, cancellationToken);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B38D File Offset: 0x0000958D
		public override Task WriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteRawValueAsync(json, cancellationToken);
			}
			return this.DoWriteRawValueAsync(json, cancellationToken);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B3A8 File Offset: 0x000095A8
		internal Task DoWriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken)
		{
			base.UpdateScopeWithFinishedValue();
			Task task = base.AutoCompleteAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteRawAsync(json, cancellationToken);
			}
			return this.DoWriteRawValueAsync(task, json, cancellationToken);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B3E0 File Offset: 0x000095E0
		private Task DoWriteRawValueAsync(Task task, [Nullable(2)] string json, CancellationToken cancellationToken)
		{
			JsonTextWriter.<DoWriteRawValueAsync>d__120 <DoWriteRawValueAsync>d__;
			<DoWriteRawValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWriteRawValueAsync>d__.<>4__this = this;
			<DoWriteRawValueAsync>d__.task = task;
			<DoWriteRawValueAsync>d__.json = json;
			<DoWriteRawValueAsync>d__.cancellationToken = cancellationToken;
			<DoWriteRawValueAsync>d__.<>1__state = -1;
			<DoWriteRawValueAsync>d__.<>t__builder.Start<JsonTextWriter.<DoWriteRawValueAsync>d__120>(ref <DoWriteRawValueAsync>d__);
			return <DoWriteRawValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B43C File Offset: 0x0000963C
		internal char[] EnsureWriteBuffer(int length, int copyTo)
		{
			if (length < 35)
			{
				length = 35;
			}
			char[] writeBuffer = this._writeBuffer;
			if (writeBuffer == null)
			{
				return this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, length);
			}
			if (writeBuffer.Length >= length)
			{
				return writeBuffer;
			}
			char[] array = BufferUtils.RentBuffer(this._arrayPool, length);
			if (copyTo != 0)
			{
				Array.Copy(writeBuffer, array, copyTo);
			}
			BufferUtils.ReturnBuffer(this._arrayPool, writeBuffer);
			this._writeBuffer = array;
			return array;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B4A6 File Offset: 0x000096A6
		private Base64Encoder Base64Encoder
		{
			get
			{
				if (this._base64Encoder == null)
				{
					this._base64Encoder = new Base64Encoder(this._writer);
				}
				return this._base64Encoder;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B4C7 File Offset: 0x000096C7
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000B4CF File Offset: 0x000096CF
		[Nullable(2)]
		public IArrayPool<char> ArrayPool
		{
			[NullableContext(2)]
			get
			{
				return this._arrayPool;
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._arrayPool = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B4E6 File Offset: 0x000096E6
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000B4EE File Offset: 0x000096EE
		public int Indentation
		{
			get
			{
				return this._indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Indentation value must be greater than 0.");
				}
				this._indentation = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B506 File Offset: 0x00009706
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000B50E File Offset: 0x0000970E
		public char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				this._quoteChar = value;
				this.UpdateCharEscapeFlags();
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B532 File Offset: 0x00009732
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000B53A File Offset: 0x0000973A
		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				if (value != this._indentChar)
				{
					this._indentChar = value;
					this._indentChars = null;
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B553 File Offset: 0x00009753
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000B55B File Offset: 0x0000975B
		public bool QuoteName
		{
			get
			{
				return this._quoteName;
			}
			set
			{
				this._quoteName = value;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B564 File Offset: 0x00009764
		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this._writer = textWriter;
			this._quoteChar = '"';
			this._quoteName = true;
			this._indentChar = ' ';
			this._indentation = 2;
			this.UpdateCharEscapeFlags();
			this._safeAsync = (base.GetType() == typeof(JsonTextWriter));
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B5C8 File Offset: 0x000097C8
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B5D5 File Offset: 0x000097D5
		public override void Close()
		{
			base.Close();
			this.CloseBufferAndWriter();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B5E3 File Offset: 0x000097E3
		private void CloseBufferAndWriter()
		{
			if (this._writeBuffer != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._writeBuffer);
				this._writeBuffer = null;
			}
			if (base.CloseOutput)
			{
				TextWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Dispose();
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B61D File Offset: 0x0000981D
		public override void WriteStartObject()
		{
			base.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
			this._writer.Write('{');
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B634 File Offset: 0x00009834
		public override void WriteStartArray()
		{
			base.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
			this._writer.Write('[');
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B64B File Offset: 0x0000984B
		public override void WriteStartConstructor(string name)
		{
			base.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
			this._writer.Write("new ");
			this._writer.Write(name);
			this._writer.Write('(');
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B680 File Offset: 0x00009880
		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				this._writer.Write('}');
				return;
			case JsonToken.EndArray:
				this._writer.Write(']');
				return;
			case JsonToken.EndConstructor:
				this._writer.Write(')');
				return;
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token.ToString(), null);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B6EC File Offset: 0x000098EC
		public override void WritePropertyName(string name)
		{
			base.InternalWritePropertyName(name);
			this.WriteEscapedString(name, this._quoteName);
			this._writer.Write(':');
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B710 File Offset: 0x00009910
		public override void WritePropertyName(string name, bool escape)
		{
			base.InternalWritePropertyName(name);
			if (escape)
			{
				this.WriteEscapedString(name, this._quoteName);
			}
			else
			{
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
				this._writer.Write(name);
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
			}
			this._writer.Write(':');
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B781 File Offset: 0x00009981
		internal override void OnStringEscapeHandlingChanged()
		{
			this.UpdateCharEscapeFlags();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B789 File Offset: 0x00009989
		private void UpdateCharEscapeFlags()
		{
			this._charEscapeFlags = JavaScriptUtils.GetCharEscapeFlags(base.StringEscapeHandling, this._quoteChar);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B7A4 File Offset: 0x000099A4
		protected override void WriteIndent()
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			this._writer.Write(this._indentChars, 0, num2 + Math.Min(num, 12));
			while ((num -= 12) > 0)
			{
				this._writer.Write(this._indentChars, num2, Math.Min(num, 12));
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B808 File Offset: 0x00009A08
		private int SetIndentChars()
		{
			string newLine = this._writer.NewLine;
			int length = newLine.Length;
			bool flag = this._indentChars != null && this._indentChars.Length == 12 + length;
			if (flag)
			{
				for (int num = 0; num != length; num++)
				{
					if (newLine.get_Chars(num) != this._indentChars[num])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				this._indentChars = (newLine + new string(this._indentChar, 12)).ToCharArray();
			}
			return length;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B888 File Offset: 0x00009A88
		protected override void WriteValueDelimiter()
		{
			this._writer.Write(',');
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B897 File Offset: 0x00009A97
		protected override void WriteIndentSpace()
		{
			this._writer.Write(' ');
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B8A6 File Offset: 0x00009AA6
		private void WriteValueInternal(string value, JsonToken token)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		[NullableContext(2)]
		public override void WriteValue(object value)
		{
			base.WriteValue(value);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000B8BD File Offset: 0x00009ABD
		public override void WriteNull()
		{
			base.InternalWriteValue(JsonToken.Null);
			this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		public override void WriteUndefined()
		{
			base.InternalWriteValue(JsonToken.Undefined);
			this.WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000B8EB File Offset: 0x00009AEB
		[NullableContext(2)]
		public override void WriteRaw(string json)
		{
			base.InternalWriteRaw();
			this._writer.Write(json);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B8FF File Offset: 0x00009AFF
		[NullableContext(2)]
		public override void WriteValue(string value)
		{
			base.InternalWriteValue(JsonToken.String);
			if (value == null)
			{
				this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
				return;
			}
			this.WriteEscapedString(value, true);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000B922 File Offset: 0x00009B22
		private void WriteEscapedString(string value, bool quote)
		{
			this.EnsureWriteBuffer();
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this._arrayPool, ref this._writeBuffer);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B955 File Offset: 0x00009B55
		public override void WriteValue(int value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000B965 File Offset: 0x00009B65
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B976 File Offset: 0x00009B76
		public override void WriteValue(long value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B986 File Offset: 0x00009B86
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value, false);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B997 File Offset: 0x00009B97
		public override void WriteValue(float value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B9BA File Offset: 0x00009BBA
		public override void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B9F3 File Offset: 0x00009BF3
		public override void WriteValue(double value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000BA16 File Offset: 0x00009C16
		public override void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000BA4F File Offset: 0x00009C4F
		public override void WriteValue(bool value)
		{
			base.InternalWriteValue(JsonToken.Boolean);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000BA67 File Offset: 0x00009C67
		public override void WriteValue(short value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000BA77 File Offset: 0x00009C77
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BA87 File Offset: 0x00009C87
		public override void WriteValue(char value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000BA9F File Offset: 0x00009C9F
		public override void WriteValue(byte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000BAAF File Offset: 0x00009CAF
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BABF File Offset: 0x00009CBF
		public override void WriteValue(decimal value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public override void WriteValue(DateTime value)
		{
			base.InternalWriteValue(JsonToken.Date);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000BB64 File Offset: 0x00009D64
		private int WriteValueToBuffer(DateTime value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, value, default(TimeSpan?), value.Kind, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		[NullableContext(2)]
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Bytes);
			this._writer.Write(this._quoteChar);
			this.Base64Encoder.Encode(value, 0, value.Length);
			this.Base64Encoder.Flush();
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000BC20 File Offset: 0x00009E20
		public override void WriteValue(DateTimeOffset value)
		{
			base.InternalWriteValue(JsonToken.Date);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		private int WriteValueToBuffer(DateTimeOffset value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, (base.DateFormatHandling == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), 2, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000BD14 File Offset: 0x00009F14
		public override void WriteValue(Guid value)
		{
			base.InternalWriteValue(JsonToken.String);
			string text = value.ToString("D");
			this._writer.Write(this._quoteChar);
			this._writer.Write(text);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000BD64 File Offset: 0x00009F64
		public override void WriteValue(TimeSpan value)
		{
			base.InternalWriteValue(JsonToken.String);
			string text = value.ToString(null, CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(text);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000BDB5 File Offset: 0x00009FB5
		[NullableContext(2)]
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
			this.WriteEscapedString(value.OriginalString, true);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000BDDC File Offset: 0x00009FDC
		[NullableContext(2)]
		public override void WriteComment(string text)
		{
			base.InternalWriteComment();
			if (text != null && text.IndexOf("*/", 4) > -1)
			{
				foreach (string text2 in text.Split(new char[]
				{
					'\n'
				}))
				{
					this._writer.Write("//");
					this._writer.Write(text2);
					this._writer.Write("\n");
				}
				return;
			}
			this._writer.Write("/*");
			this._writer.Write(text);
			this._writer.Write("*/");
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000BE7E File Offset: 0x0000A07E
		public override void WriteWhitespace(string ws)
		{
			base.InternalWriteWhitespace(ws);
			this._writer.Write(ws);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000BE93 File Offset: 0x0000A093
		private void EnsureWriteBuffer()
		{
			if (this._writeBuffer == null)
			{
				this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, 35);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		private void WriteIntegerValue(long value)
		{
			if (value >= 0L && value <= 9L)
			{
				this._writer.Write((char)(48L + value));
				return;
			}
			bool flag = value < 0L;
			this.WriteIntegerValue((ulong)(flag ? (-(ulong)value) : value), flag);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		private void WriteIntegerValue(ulong value, bool negative)
		{
			if (!negative & value <= 9UL)
			{
				this._writer.Write((char)(48UL + value));
				return;
			}
			int num = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, num);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000BF3C File Offset: 0x0000A13C
		private int WriteNumberToBuffer(ulong value, bool negative)
		{
			if (value <= (ulong)-1)
			{
				return this.WriteNumberToBuffer((uint)value, negative);
			}
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				ulong num3 = value / 10UL;
				ulong num4 = value - num3 * 10UL;
				this._writeBuffer[--num2] = (char)(48UL + num4);
				value = num3;
			}
			while (value != 0UL);
			return num;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		private void WriteIntegerValue(int value)
		{
			if (value >= 0 && value <= 9)
			{
				this._writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			this.WriteIntegerValue((uint)(flag ? (-(uint)value) : value), flag);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		private void WriteIntegerValue(uint value, bool negative)
		{
			if (!negative & value <= 9U)
			{
				this._writer.Write((char)(48U + value));
				return;
			}
			int num = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, num);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C028 File Offset: 0x0000A228
		private int WriteNumberToBuffer(uint value, bool negative)
		{
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength((ulong)value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				uint num3 = value / 10U;
				uint num4 = value - num3 * 10U;
				this._writeBuffer[--num2] = (char)(48U + num4);
				value = num3;
			}
			while (value != 0U);
			return num;
		}

		// Token: 0x04000101 RID: 257
		private readonly bool _safeAsync;

		// Token: 0x04000102 RID: 258
		private const int IndentCharBufferSize = 12;

		// Token: 0x04000103 RID: 259
		private readonly TextWriter _writer;

		// Token: 0x04000104 RID: 260
		[Nullable(2)]
		private Base64Encoder _base64Encoder;

		// Token: 0x04000105 RID: 261
		private char _indentChar;

		// Token: 0x04000106 RID: 262
		private int _indentation;

		// Token: 0x04000107 RID: 263
		private char _quoteChar;

		// Token: 0x04000108 RID: 264
		private bool _quoteName;

		// Token: 0x04000109 RID: 265
		[Nullable(2)]
		private bool[] _charEscapeFlags;

		// Token: 0x0400010A RID: 266
		[Nullable(2)]
		private char[] _writeBuffer;

		// Token: 0x0400010B RID: 267
		[Nullable(2)]
		private IArrayPool<char> _arrayPool;

		// Token: 0x0400010C RID: 268
		[Nullable(2)]
		private char[] _indentChars;
	}
}

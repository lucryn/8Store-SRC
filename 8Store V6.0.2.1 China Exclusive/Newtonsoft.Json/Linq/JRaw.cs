using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C8 RID: 200
	[NullableContext(1)]
	[Nullable(0)]
	public class JRaw : JValue
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x000299AC File Offset: 0x00027BAC
		public static Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			JRaw.<CreateAsync>d__0 <CreateAsync>d__;
			<CreateAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JRaw>.Create();
			<CreateAsync>d__.reader = reader;
			<CreateAsync>d__.cancellationToken = cancellationToken;
			<CreateAsync>d__.<>1__state = -1;
			<CreateAsync>d__.<>t__builder.Start<JRaw.<CreateAsync>d__0>(ref <CreateAsync>d__);
			return <CreateAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000299F7 File Offset: 0x00027BF7
		public JRaw(JRaw other) : base(other, null)
		{
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00029A01 File Offset: 0x00027C01
		internal JRaw(JRaw other, [Nullable(2)] JsonCloneSettings settings) : base(other, settings)
		{
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00029A0B File Offset: 0x00027C0B
		[NullableContext(2)]
		public JRaw(object rawJson) : base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00029A18 File Offset: 0x00027C18
		public static JRaw Create(JsonReader reader)
		{
			JRaw result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					result = new JRaw(stringWriter.ToString());
				}
			}
			return result;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00029A80 File Offset: 0x00027C80
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JRaw(this, settings);
		}
	}
}

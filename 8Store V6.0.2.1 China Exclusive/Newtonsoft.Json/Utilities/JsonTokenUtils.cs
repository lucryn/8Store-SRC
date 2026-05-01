using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000068 RID: 104
	internal static class JsonTokenUtils
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x000165DE File Offset: 0x000147DE
		internal static bool IsEndToken(JsonToken token)
		{
			return token - JsonToken.EndObject <= 2;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000165EA File Offset: 0x000147EA
		internal static bool IsStartToken(JsonToken token)
		{
			return token - JsonToken.StartObject <= 2;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000165F5 File Offset: 0x000147F5
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			return token - JsonToken.Integer <= 5 || token - JsonToken.Date <= 1;
		}
	}
}

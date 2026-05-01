using System;
using System.Collections.Generic;
using MiniJSON;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000033 RID: 51
	public static class JsonHelper
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		public static bool TryParseJson<T>(string json, out T result, out string error)
		{
			result = default(T);
			error = null;
			if (string.IsNullOrWhiteSpace(json))
			{
				error = "Empty JSON string";
				return false;
			}
			try
			{
				Dictionary<string, object> dictionary = Json.Deserialize(json) as Dictionary<string, object>;
				if (dictionary != null)
				{
					string value = JsonConvert.SerializeObject(dictionary);
					result = JsonConvert.DeserializeObject<T>(value);
					return true;
				}
			}
			catch (Exception)
			{
			}
			bool result2;
			try
			{
				result = JsonConvert.DeserializeObject<T>(json);
				result2 = true;
			}
			catch (Exception ex)
			{
				error = ex.Message;
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000FC60 File Offset: 0x0000DE60
		public static string CleanJsonResponse(string rawResponse)
		{
			if (string.IsNullOrEmpty(rawResponse))
			{
				return rawResponse;
			}
			int num = rawResponse.IndexOf('{');
			if (num > 0)
			{
				return rawResponse.Substring(num);
			}
			int num2 = rawResponse.IndexOf('[');
			if (num2 > 0)
			{
				return rawResponse.Substring(num2);
			}
			return rawResponse;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		public static bool LooksLikeHtml(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			text = text.Trim();
			return text.StartsWith("<!DOCTYPE") || text.StartsWith("<html") || text.Contains("<script>") || text.Contains("function ") || text.Contains("const ") || text.Contains("import ");
		}
	}
}

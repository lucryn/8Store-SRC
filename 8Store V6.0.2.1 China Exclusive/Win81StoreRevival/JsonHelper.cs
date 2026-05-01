using System;
using System.Collections.Generic;
using System.Diagnostics;
using MiniJSON;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x0200002F RID: 47
	public static class JsonHelper
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public static bool TryParseJson<T>(string json, out T result, out string error)
		{
			result = default(T);
			error = null;
			bool flag = string.IsNullOrWhiteSpace(json);
			bool result2;
			if (flag)
			{
				error = "Empty JSON string";
				result2 = false;
			}
			else
			{
				try
				{
					Dictionary<string, object> dictionary = Json.Deserialize(json) as Dictionary<string, object>;
					bool flag2 = dictionary != null;
					if (flag2)
					{
						Debug.WriteLine("[JSON HELPER] Successfully parsed with MiniJSON");
						string value = JsonConvert.SerializeObject(dictionary);
						result = JsonConvert.DeserializeObject<T>(value);
						return true;
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(string.Format("[JSON HELPER] MiniJSON failed: {0}", new object[]
					{
						ex.Message
					}));
				}
				try
				{
					result = JsonConvert.DeserializeObject<T>(json);
					result2 = true;
				}
				catch (Exception ex2)
				{
					error = ex2.Message;
					Debug.WriteLine(string.Format("[JSON HELPER] Newtonsoft failed: {0}", new object[]
					{
						ex2.Message
					}));
					result2 = false;
				}
			}
			return result2;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public static string CleanJsonResponse(string rawResponse)
		{
			bool flag = string.IsNullOrEmpty(rawResponse);
			string result;
			if (flag)
			{
				result = rawResponse;
			}
			else
			{
				int num = rawResponse.IndexOf('{');
				bool flag2 = num > 0;
				if (flag2)
				{
					Debug.WriteLine(string.Format("[JSON CLEANER] Found JSON at position {0}, trimming {1} chars", new object[]
					{
						num,
						num
					}));
					result = rawResponse.Substring(num);
				}
				else
				{
					int num2 = rawResponse.IndexOf('[');
					bool flag3 = num2 > 0;
					if (flag3)
					{
						Debug.WriteLine(string.Format("[JSON CLEANER] Found JSON array at position {0}, trimming {1} chars", new object[]
						{
							num2,
							num2
						}));
						result = rawResponse.Substring(num2);
					}
					else
					{
						result = rawResponse;
					}
				}
			}
			return result;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000F188 File Offset: 0x0000D388
		public static bool LooksLikeHtml(string text)
		{
			bool flag = string.IsNullOrEmpty(text);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				text = text.Trim();
				result = (text.StartsWith("<!DOCTYPE") || text.StartsWith("<html") || text.Contains("<script>") || text.Contains("function ") || text.Contains("const ") || text.Contains("import "));
			}
			return result;
		}
	}
}

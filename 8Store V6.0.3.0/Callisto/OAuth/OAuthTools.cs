using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Security.Cryptography.Core;

namespace Callisto.OAuth
{
	// Token: 0x02000031 RID: 49
	public static class OAuthTools
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000BF39 File Offset: 0x0000A139
		static OAuthTools()
		{
			OAuthTools._random = new Random();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000BF5C File Offset: 0x0000A15C
		public static string GetNonce()
		{
			char[] array = new char[16];
			lock (OAuthTools._randomLock)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = "abcdefghijklmnopqrstuvwxyz1234567890".get_Chars(OAuthTools._random.Next(0, "abcdefghijklmnopqrstuvwxyz1234567890".Length));
				}
			}
			return new string(array);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public static string GetTimestamp()
		{
			return OAuthTools.GetTimestamp(DateTime.UtcNow);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public static string GetTimestamp(DateTime dateTime)
		{
			return dateTime.ToUnixTime().ToString();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		public static string UrlEncodeRelaxed(string value)
		{
			string text = Uri.EscapeDataString(value);
			return text.Replace("(", "(".PercentEncode()).Replace(")", ")".PercentEncode());
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000C03C File Offset: 0x0000A23C
		public static string UrlEncodeStrict(string value)
		{
			return value.Replace("%%", "%25%");
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000C060 File Offset: 0x0000A260
		public static string NormalizeRequestParameters(WebParameterCollection parameters)
		{
			WebParameterCollection collection = OAuthTools.SortParametersExcludingSignature(parameters);
			return collection.Concatenate("=", "&");
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C0AC File Offset: 0x0000A2AC
		public static WebParameterCollection SortParametersExcludingSignature(WebParameterCollection parameters)
		{
			WebParameterCollection webParameterCollection = new WebParameterCollection(parameters);
			IEnumerable<WebPair> parameters2 = Enumerable.Where<WebPair>(webParameterCollection, (WebPair n) => n.Name.EqualsIgnoreCase("oauth_signature"));
			webParameterCollection.RemoveAll(parameters2);
			webParameterCollection.ForEach(delegate(WebPair p)
			{
				p.Value = OAuthTools.UrlEncodeStrict(p.Value);
			});
			return webParameterCollection;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000C110 File Offset: 0x0000A310
		public static string ConstructRequestUrl(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = "{0}://{1}".FormatWith(new object[]
			{
				url.Scheme,
				url.Host
			});
			string text2 = ":{0}".FormatWith(new object[]
			{
				url.Port
			});
			bool flag = url.Scheme == "http" && url.Port == 80;
			bool flag2 = url.Scheme == "https" && url.Port == 443;
			stringBuilder.Append(text);
			stringBuilder.Append((!flag && !flag2) ? text2 : "");
			stringBuilder.Append(url.AbsolutePath);
			return stringBuilder.ToString();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		public static string ConcatenateRequestElements(WebMethod method, string url, WebParameterCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = method.ToUpper().Then("&");
			string text2 = OAuthTools.UrlEncodeRelaxed(OAuthTools.ConstructRequestUrl(url.AsUri())).Then("&");
			string text3 = OAuthTools.UrlEncodeRelaxed(OAuthTools.NormalizeRequestParameters(parameters));
			stringBuilder.Append(text);
			stringBuilder.Append(text2);
			stringBuilder.Append(text3);
			return stringBuilder.ToString();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000C26A File Offset: 0x0000A46A
		public static string GetSignature(OAuthSignatureMethod signatureMethod, string signatureBase, string consumerSecret)
		{
			return OAuthTools.GetSignature(signatureMethod, OAuthSignatureTreatment.Escaped, signatureBase, consumerSecret, null);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000C276 File Offset: 0x0000A476
		public static string GetSignature(OAuthSignatureMethod signatureMethod, OAuthSignatureTreatment signatureTreatment, string signatureBase, string consumerSecret)
		{
			return OAuthTools.GetSignature(signatureMethod, signatureTreatment, signatureBase, consumerSecret, null);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C282 File Offset: 0x0000A482
		public static string GetSignature(OAuthSignatureMethod signatureMethod, string signatureBase, string consumerSecret, string tokenSecret)
		{
			return OAuthTools.GetSignature(signatureMethod, OAuthSignatureTreatment.Escaped, consumerSecret, tokenSecret);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C290 File Offset: 0x0000A490
		public static string GetSignature(OAuthSignatureMethod signatureMethod, OAuthSignatureTreatment signatureTreatment, string signatureBase, string consumerSecret, string tokenSecret)
		{
			if (tokenSecret.IsNullOrBlank())
			{
				tokenSecret = string.Empty;
			}
			consumerSecret = OAuthTools.UrlEncodeRelaxed(consumerSecret);
			tokenSecret = OAuthTools.UrlEncodeRelaxed(tokenSecret);
			if (signatureMethod == OAuthSignatureMethod.HmacSha1)
			{
				MacAlgorithmProvider hashProvider = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
				string key = "{0}&{1}".FormatWith(new object[]
				{
					consumerSecret,
					tokenSecret
				});
				string text = signatureBase.HashWith(hashProvider, key);
				return (signatureTreatment == OAuthSignatureTreatment.Escaped) ? OAuthTools.UrlEncodeRelaxed(text) : text;
			}
			throw new NotImplementedException("Only HMAC-SHA1 is currently supported.");
		}

		// Token: 0x04000108 RID: 264
		private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

		// Token: 0x04000109 RID: 265
		private const string Digit = "1234567890";

		// Token: 0x0400010A RID: 266
		private const string Lower = "abcdefghijklmnopqrstuvwxyz";

		// Token: 0x0400010B RID: 267
		private const string Unreserved = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-._~";

		// Token: 0x0400010C RID: 268
		private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		// Token: 0x0400010D RID: 269
		private static readonly Random _random;

		// Token: 0x0400010E RID: 270
		private static readonly object _randomLock = new object();

		// Token: 0x0400010F RID: 271
		private static readonly Encoding _encoding = Encoding.UTF8;
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Win81StoreRevival
{
	// Token: 0x02000030 RID: 48
	public static class HttpClientHelper
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000F484 File Offset: 0x0000D684
		public static HttpClient GetClient()
		{
			if (HttpClientHelper._httpClient == null)
			{
				try
				{
					HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
					try
					{
						httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(1);
						httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(7);
					}
					catch (Exception)
					{
					}
					HttpClientHelper._httpClient = new HttpClient(httpBaseProtocolFilter);
					HttpClientHelper._httpClient.DefaultRequestHeaders.Add("User-Agent", "Win81StoreRevival/1.0");
					HttpClientHelper._httpClient.DefaultRequestHeaders.Add("X-Timeout", "30000");
				}
				catch (Exception)
				{
					HttpClientHelper._httpClient = new HttpClient();
					HttpClientHelper._httpClient.DefaultRequestHeaders.Add("User-Agent", "Win81StoreRevival/1.0");
				}
			}
			return HttpClientHelper._httpClient;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000F544 File Offset: 0x0000D744
		public static Task<bool> TestConnection(string url)
		{
			HttpClientHelper.<TestConnection>d__2 <TestConnection>d__;
			<TestConnection>d__.url = url;
			<TestConnection>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TestConnection>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TestConnection>d__.<>t__builder;
			<>t__builder.Start<HttpClientHelper.<TestConnection>d__2>(ref <TestConnection>d__);
			return <TestConnection>d__.<>t__builder.Task;
		}

		// Token: 0x04000141 RID: 321
		private static HttpClient _httpClient;
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Win81StoreRevival
{
	// Token: 0x0200002D RID: 45
	public static class HttpClientHelper
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public static HttpClient GetClient()
		{
			bool flag = HttpClientHelper._httpClient == null;
			if (flag)
			{
				try
				{
					HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
					try
					{
						httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(1);
						httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(7);
					}
					catch (Exception ex)
					{
						Debug.WriteLine("[HTTP CLIENT] Cannot add ignorable errors: " + ex.Message);
					}
					HttpClientHelper._httpClient = new HttpClient(httpBaseProtocolFilter);
					HttpClientHelper._httpClient.DefaultRequestHeaders.Add("User-Agent", "Win81StoreRevival/1.0");
					HttpClientHelper._httpClient.DefaultRequestHeaders.Add("X-Timeout", "30000");
				}
				catch (Exception ex2)
				{
					Debug.WriteLine("[HTTP CLIENT ERROR] " + ex2.Message);
					HttpClientHelper._httpClient = new HttpClient();
					HttpClientHelper._httpClient.DefaultRequestHeaders.Add("User-Agent", "Win81StoreRevival/1.0");
				}
			}
			return HttpClientHelper._httpClient;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		[DebuggerStepThrough]
		public static Task<bool> TestConnection(string url)
		{
			HttpClientHelper.<TestConnection>d__2 <TestConnection>d__ = new HttpClientHelper.<TestConnection>d__2();
			<TestConnection>d__.url = url;
			<TestConnection>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TestConnection>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TestConnection>d__.<>t__builder;
			<>t__builder.Start<HttpClientHelper.<TestConnection>d__2>(ref <TestConnection>d__);
			return <TestConnection>d__.<>t__builder.Task;
		}

		// Token: 0x0400013B RID: 315
		private static HttpClient _httpClient;
	}
}

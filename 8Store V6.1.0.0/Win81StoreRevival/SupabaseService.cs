using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Win81StoreRevival
{
	// Token: 0x02000016 RID: 22
	public static class SupabaseService
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000038AC File Offset: 0x00001AAC
		public static void SaveCredentials(string email, string password)
		{
			try
			{
				if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
				{
					string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
					SupabaseService._localSettings.Values["SavedEmail"] = email;
					SupabaseService._localSettings.Values["SavedPassword"] = text;
					SupabaseService._localSettings.Values["CredentialsSaved"] = true;
					SupabaseService._localSettings.Values["SaveTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
					SupabaseService.DebugCredentialsDebug();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003964 File Offset: 0x00001B64
		public static Tuple<string, string> LoadCredentials()
		{
			try
			{
				SupabaseService.DebugCredentialsDebug();
				if (SupabaseService._localSettings.Values["CredentialsSaved"] as bool? == true)
				{
					string text = SupabaseService._localSettings.Values["SavedEmail"] as string;
					string text2 = SupabaseService._localSettings.Values["SavedPassword"] as string;
					if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
					{
						byte[] array = Convert.FromBase64String(text2);
						string @string = Encoding.UTF8.GetString(array, 0, array.Length);
						return new Tuple<string, string>(text, @string);
					}
				}
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003A34 File Offset: 0x00001C34
		public static Task ApplyStatsFromReview(List<StoreApp> apps)
		{
			SupabaseService.<ApplyStatsFromReview>d__7 <ApplyStatsFromReview>d__;
			<ApplyStatsFromReview>d__.apps = apps;
			<ApplyStatsFromReview>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ApplyStatsFromReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ApplyStatsFromReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<ApplyStatsFromReview>d__7>(ref <ApplyStatsFromReview>d__);
			return <ApplyStatsFromReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003A7C File Offset: 0x00001C7C
		public static void DeleteCredentials()
		{
			try
			{
				SupabaseService._localSettings.Values["SavedEmail"] = null;
				SupabaseService._localSettings.Values["SavedPassword"] = null;
				SupabaseService._localSettings.Values["CredentialsSaved"] = null;
				SupabaseService._localSettings.Values["SaveTime"] = null;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003AF4 File Offset: 0x00001CF4
		private static void DebugCredentialsDebug()
		{
			try
			{
				object obj = SupabaseService._localSettings.Values["CredentialsSaved"];
				object obj2 = SupabaseService._localSettings.Values["SavedEmail"];
				object obj3 = SupabaseService._localSettings.Values["SavedPassword"];
			}
			catch
			{
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003B58 File Offset: 0x00001D58
		public static Task<bool> AutoLogin()
		{
			SupabaseService.<AutoLogin>d__10 <AutoLogin>d__;
			<AutoLogin>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AutoLogin>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <AutoLogin>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<AutoLogin>d__10>(ref <AutoLogin>d__);
			return <AutoLogin>d__.<>t__builder.Task;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003B98 File Offset: 0x00001D98
		public static Task<UserAccount> ExchangeProtocolCode(string code)
		{
			SupabaseService.<ExchangeProtocolCode>d__11 <ExchangeProtocolCode>d__;
			<ExchangeProtocolCode>d__.code = code;
			<ExchangeProtocolCode>d__.<>t__builder = AsyncTaskMethodBuilder<UserAccount>.Create();
			<ExchangeProtocolCode>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<UserAccount> <>t__builder = <ExchangeProtocolCode>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<ExchangeProtocolCode>d__11>(ref <ExchangeProtocolCode>d__);
			return <ExchangeProtocolCode>d__.<>t__builder.Task;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public static Task<UserAccount> SignInNoSave(string email, string password)
		{
			SupabaseService.<SignInNoSave>d__12 <SignInNoSave>d__;
			<SignInNoSave>d__.email = email;
			<SignInNoSave>d__.password = password;
			<SignInNoSave>d__.<>t__builder = AsyncTaskMethodBuilder<UserAccount>.Create();
			<SignInNoSave>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<UserAccount> <>t__builder = <SignInNoSave>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SignInNoSave>d__12>(ref <SignInNoSave>d__);
			return <SignInNoSave>d__.<>t__builder.Task;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003C30 File Offset: 0x00001E30
		private static Task<ApiResponse<T>> CallApi<T>(string action, object data = null)
		{
			SupabaseService.<CallApi>d__13<T> <CallApi>d__;
			<CallApi>d__.action = action;
			<CallApi>d__.data = data;
			<CallApi>d__.<>t__builder = AsyncTaskMethodBuilder<ApiResponse<T>>.Create();
			<CallApi>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<ApiResponse<T>> <>t__builder = <CallApi>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<CallApi>d__13<T>>(ref <CallApi>d__);
			return <CallApi>d__.<>t__builder.Task;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003C80 File Offset: 0x00001E80
		public static Task<string> TestConnection()
		{
			SupabaseService.<TestConnection>d__14 <TestConnection>d__;
			<TestConnection>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<TestConnection>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<string> <>t__builder = <TestConnection>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<TestConnection>d__14>(ref <TestConnection>d__);
			return <TestConnection>d__.<>t__builder.Task;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public static Task<UserAccount> SignUp(string email, string password, string username)
		{
			SupabaseService.<SignUp>d__15 <SignUp>d__;
			<SignUp>d__.email = email;
			<SignUp>d__.password = password;
			<SignUp>d__.username = username;
			<SignUp>d__.<>t__builder = AsyncTaskMethodBuilder<UserAccount>.Create();
			<SignUp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<UserAccount> <>t__builder = <SignUp>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SignUp>d__15>(ref <SignUp>d__);
			return <SignUp>d__.<>t__builder.Task;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003D18 File Offset: 0x00001F18
		public static Task<UserAccount> SignIn(string email, string password)
		{
			SupabaseService.<SignIn>d__16 <SignIn>d__;
			<SignIn>d__.email = email;
			<SignIn>d__.password = password;
			<SignIn>d__.<>t__builder = AsyncTaskMethodBuilder<UserAccount>.Create();
			<SignIn>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<UserAccount> <>t__builder = <SignIn>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SignIn>d__16>(ref <SignIn>d__);
			return <SignIn>d__.<>t__builder.Task;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003D68 File Offset: 0x00001F68
		public static void SignOut()
		{
			SupabaseService._localSettings.Values["SupabaseToken"] = null;
			SupabaseService._localSettings.Values["UserEmail"] = null;
			SupabaseService._localSettings.Values["UserId"] = null;
			SupabaseService._localSettings.Values["Username"] = null;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003DC9 File Offset: 0x00001FC9
		public static bool IsUserLoggedIn()
		{
			return !string.IsNullOrEmpty(SupabaseService._localSettings.Values["SupabaseToken"] as string);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003DEC File Offset: 0x00001FEC
		public static UserAccount GetCurrentUser()
		{
			if (!SupabaseService.IsUserLoggedIn())
			{
				return null;
			}
			return new UserAccount
			{
				Id = ((SupabaseService._localSettings.Values["UserId"] as string) ?? ""),
				Email = ((SupabaseService._localSettings.Values["UserEmail"] as string) ?? ""),
				Username = ((SupabaseService._localSettings.Values["Username"] as string) ?? "")
			};
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003E80 File Offset: 0x00002080
		private static void SaveUserSession(string token, UserAccount account)
		{
			SupabaseService._localSettings.Values["SupabaseToken"] = token;
			SupabaseService._localSettings.Values["UserEmail"] = account.Email;
			SupabaseService._localSettings.Values["UserId"] = account.Id;
			SupabaseService._localSettings.Values["Username"] = account.Username;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003EF0 File Offset: 0x000020F0
		private static string GetAuthToken()
		{
			return SupabaseService._localSettings.Values["SupabaseToken"] as string;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003F0C File Offset: 0x0000210C
		public static Task<List<Review>> GetReviews(string appId)
		{
			SupabaseService.<GetReviews>d__22 <GetReviews>d__;
			<GetReviews>d__.appId = appId;
			<GetReviews>d__.<>t__builder = AsyncTaskMethodBuilder<List<Review>>.Create();
			<GetReviews>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<List<Review>> <>t__builder = <GetReviews>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetReviews>d__22>(ref <GetReviews>d__);
			return <GetReviews>d__.<>t__builder.Task;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003F51 File Offset: 0x00002151
		public static string GetAuthTokenValue()
		{
			return (SupabaseService._localSettings.Values["SupabaseToken"] as string) ?? "";
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003F78 File Offset: 0x00002178
		public static Task<ReviewResult> SubmitReview(string appId, int rating, string comment)
		{
			SupabaseService.<SubmitReview>d__24 <SubmitReview>d__;
			<SubmitReview>d__.appId = appId;
			<SubmitReview>d__.rating = rating;
			<SubmitReview>d__.comment = comment;
			<SubmitReview>d__.<>t__builder = AsyncTaskMethodBuilder<ReviewResult>.Create();
			<SubmitReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<ReviewResult> <>t__builder = <SubmitReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SubmitReview>d__24>(ref <SubmitReview>d__);
			return <SubmitReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static Task<bool> DeleteReview(int reviewId)
		{
			SupabaseService.<DeleteReview>d__25 <DeleteReview>d__;
			<DeleteReview>d__.reviewId = reviewId;
			<DeleteReview>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <DeleteReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<DeleteReview>d__25>(ref <DeleteReview>d__);
			return <DeleteReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004018 File Offset: 0x00002218
		public static Task<List<Review>> GetUserReviews()
		{
			SupabaseService.<GetUserReviews>d__26 <GetUserReviews>d__;
			<GetUserReviews>d__.<>t__builder = AsyncTaskMethodBuilder<List<Review>>.Create();
			<GetUserReviews>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<List<Review>> <>t__builder = <GetUserReviews>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetUserReviews>d__26>(ref <GetUserReviews>d__);
			return <GetUserReviews>d__.<>t__builder.Task;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004058 File Offset: 0x00002258
		public static Task<List<StoreApp>> GetUserApps()
		{
			SupabaseService.<GetUserApps>d__27 <GetUserApps>d__;
			<GetUserApps>d__.<>t__builder = AsyncTaskMethodBuilder<List<StoreApp>>.Create();
			<GetUserApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<List<StoreApp>> <>t__builder = <GetUserApps>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetUserApps>d__27>(ref <GetUserApps>d__);
			return <GetUserApps>d__.<>t__builder.Task;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004098 File Offset: 0x00002298
		private static void NormalizeUserApp(StoreApp app)
		{
			if (app == null)
			{
				return;
			}
			if (app.ScreenshotUrls != null && app.ScreenshotUrls.Count > 0)
			{
				if (string.IsNullOrWhiteSpace(app.Screenshot1) && app.ScreenshotUrls.Count > 0)
				{
					app.Screenshot1 = app.ScreenshotUrls[0];
				}
				if (string.IsNullOrWhiteSpace(app.Screenshot2) && app.ScreenshotUrls.Count > 1)
				{
					app.Screenshot2 = app.ScreenshotUrls[1];
				}
				if (string.IsNullOrWhiteSpace(app.Screenshot3) && app.ScreenshotUrls.Count > 2)
				{
					app.Screenshot3 = app.ScreenshotUrls[2];
				}
			}
			if (string.IsNullOrWhiteSpace(app.DownloadUrl) && !string.IsNullOrWhiteSpace(app.PackageFileName))
			{
				app.DownloadUrl = "https://8store.modyleprojects.ru/files/" + app.Id + "/" + app.PackageFileName;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004188 File Offset: 0x00002388
		public static Task<Review> GetReview(int reviewId)
		{
			SupabaseService.<GetReview>d__29 <GetReview>d__;
			<GetReview>d__.reviewId = reviewId;
			<GetReview>d__.<>t__builder = AsyncTaskMethodBuilder<Review>.Create();
			<GetReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<Review> <>t__builder = <GetReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetReview>d__29>(ref <GetReview>d__);
			return <GetReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000041D0 File Offset: 0x000023D0
		public static Task<Review> GetUserReviewForApp(string appId)
		{
			SupabaseService.<GetUserReviewForApp>d__30 <GetUserReviewForApp>d__;
			<GetUserReviewForApp>d__.appId = appId;
			<GetUserReviewForApp>d__.<>t__builder = AsyncTaskMethodBuilder<Review>.Create();
			<GetUserReviewForApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<Review> <>t__builder = <GetUserReviewForApp>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetUserReviewForApp>d__30>(ref <GetUserReviewForApp>d__);
			return <GetUserReviewForApp>d__.<>t__builder.Task;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004215 File Offset: 0x00002415
		public static Task<string> GetDiscordAuthUrl()
		{
			return Task.FromResult<string>("https://8store.modyleprojects.ru/auth");
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004224 File Offset: 0x00002424
		public static Task<bool> ValidateToken()
		{
			SupabaseService.<ValidateToken>d__32 <ValidateToken>d__;
			<ValidateToken>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ValidateToken>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <ValidateToken>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<ValidateToken>d__32>(ref <ValidateToken>d__);
			return <ValidateToken>d__.<>t__builder.Task;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004264 File Offset: 0x00002464
		public static Task<Dictionary<string, ReviewStats>> GetAllReviewStats()
		{
			SupabaseService.<GetAllReviewStats>d__33 <GetAllReviewStats>d__;
			<GetAllReviewStats>d__.<>t__builder = AsyncTaskMethodBuilder<Dictionary<string, ReviewStats>>.Create();
			<GetAllReviewStats>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<Dictionary<string, ReviewStats>> <>t__builder = <GetAllReviewStats>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetAllReviewStats>d__33>(ref <GetAllReviewStats>d__);
			return <GetAllReviewStats>d__.<>t__builder.Task;
		}

		// Token: 0x0400004B RID: 75
		private const string ApiUrl = "https://8store.modyleprojects.ru/api/auth/index.php";

		// Token: 0x0400004C RID: 76
		private static ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

		// Token: 0x0400004D RID: 77
		private static Dictionary<string, ReviewStats> _cachedReviewStats = new Dictionary<string, ReviewStats>();

		// Token: 0x0400004E RID: 78
		private static DateTime _lastCacheTime = DateTime.MinValue;

		// Token: 0x0400004F RID: 79
		private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5.0);
	}
}

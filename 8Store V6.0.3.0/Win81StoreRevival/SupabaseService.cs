using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Win81StoreRevival
{
	// Token: 0x02000015 RID: 21
	public static class SupabaseService
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public static void SaveCredentials(string email, string password)
		{
			try
			{
				bool flag = string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password);
				if (flag)
				{
					Debug.WriteLine("[CREDENTIALS] Cannot save - email or password empty");
				}
				else
				{
					byte[] bytes = Encoding.UTF8.GetBytes(password);
					string text = Convert.ToBase64String(bytes);
					SupabaseService._localSettings.Values["SavedEmail"] = email;
					SupabaseService._localSettings.Values["SavedPassword"] = text;
					SupabaseService._localSettings.Values["CredentialsSaved"] = true;
					SupabaseService._localSettings.Values["SaveTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
					Debug.WriteLine("[CREDENTIALS] Credentials saved for: " + email);
					SupabaseService.DebugCredentialsDebug();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[CREDENTIALS ERROR] Failed to save credentials: " + ex.Message);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public static Tuple<string, string> LoadCredentials()
		{
			try
			{
				SupabaseService.DebugCredentialsDebug();
				bool flag = SupabaseService._localSettings.Values["CredentialsSaved"] as bool? == true;
				if (flag)
				{
					string text = SupabaseService._localSettings.Values["SavedEmail"] as string;
					string text2 = SupabaseService._localSettings.Values["SavedPassword"] as string;
					bool flag2 = !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						byte[] array = Convert.FromBase64String(text2);
						string @string = Encoding.UTF8.GetString(array, 0, array.Length);
						Debug.WriteLine("[CREDENTIALS] Loaded credentials for: " + text);
						return new Tuple<string, string>(text, @string);
					}
					Debug.WriteLine("[CREDENTIALS] Email or encrypted password is empty");
				}
				else
				{
					Debug.WriteLine("[CREDENTIALS] CredentialsSaved flag is false or null");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[CREDENTIALS ERROR] Failed to load credentials: " + ex.Message);
			}
			return null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003F14 File Offset: 0x00002114
		[DebuggerStepThrough]
		public static Task ApplyStatsFromReview(List<StoreApp> apps)
		{
			SupabaseService.<ApplyStatsFromReview>d__7 <ApplyStatsFromReview>d__ = new SupabaseService.<ApplyStatsFromReview>d__7();
			<ApplyStatsFromReview>d__.apps = apps;
			<ApplyStatsFromReview>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ApplyStatsFromReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ApplyStatsFromReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<ApplyStatsFromReview>d__7>(ref <ApplyStatsFromReview>d__);
			return <ApplyStatsFromReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003F5C File Offset: 0x0000215C
		public static void DeleteCredentials()
		{
			try
			{
				SupabaseService._localSettings.Values["SavedEmail"] = null;
				SupabaseService._localSettings.Values["SavedPassword"] = null;
				SupabaseService._localSettings.Values["CredentialsSaved"] = null;
				SupabaseService._localSettings.Values["SaveTime"] = null;
				Debug.WriteLine("[CREDENTIALS] Credentials deleted");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[CREDENTIALS ERROR] Failed to delete credentials: " + ex.Message);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003FFC File Offset: 0x000021FC
		private static void DebugCredentialsDebug()
		{
			try
			{
				bool? flag = SupabaseService._localSettings.Values["CredentialsSaved"] as bool?;
				string text = SupabaseService._localSettings.Values["SavedEmail"] as string;
				string text2 = SupabaseService._localSettings.Values["SavedPassword"] as string;
				Debug.WriteLine(string.Format("[CREDENTIALS DEBUG] SavedFlag: {0}", new object[]
				{
					flag
				}));
				Debug.WriteLine(string.Format("[CREDENTIALS DEBUG] Email saved: {0}", new object[]
				{
					string.IsNullOrEmpty(text) ? "NO" : "YES"
				}));
				Debug.WriteLine(string.Format("[CREDENTIALS DEBUG] Pass saved: {0}", new object[]
				{
					string.IsNullOrEmpty(text2) ? "NO" : "YES"
				}));
			}
			catch
			{
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000040F0 File Offset: 0x000022F0
		[DebuggerStepThrough]
		public static Task<bool> AutoLogin()
		{
			SupabaseService.<AutoLogin>d__10 <AutoLogin>d__ = new SupabaseService.<AutoLogin>d__10();
			<AutoLogin>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AutoLogin>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <AutoLogin>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<AutoLogin>d__10>(ref <AutoLogin>d__);
			return <AutoLogin>d__.<>t__builder.Task;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004130 File Offset: 0x00002330
		[DebuggerStepThrough]
		public static Task<UserAccount> SignInNoSave(string email, string password)
		{
			SupabaseService.<SignInNoSave>d__11 <SignInNoSave>d__ = new SupabaseService.<SignInNoSave>d__11();
			<SignInNoSave>d__.email = email;
			<SignInNoSave>d__.password = password;
			<SignInNoSave>d__.<>t__builder = AsyncTaskMethodBuilder<UserAccount>.Create();
			<SignInNoSave>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<UserAccount> <>t__builder = <SignInNoSave>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SignInNoSave>d__11>(ref <SignInNoSave>d__);
			return <SignInNoSave>d__.<>t__builder.Task;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004180 File Offset: 0x00002380
		[DebuggerStepThrough]
		private static Task<ApiResponse<T>> CallApi<T>(string action, object data = null)
		{
			SupabaseService.<CallApi>d__12<T> <CallApi>d__ = new SupabaseService.<CallApi>d__12<T>();
			<CallApi>d__.action = action;
			<CallApi>d__.data = data;
			<CallApi>d__.<>t__builder = AsyncTaskMethodBuilder<ApiResponse<T>>.Create();
			<CallApi>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<ApiResponse<T>> <>t__builder = <CallApi>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<CallApi>d__12<T>>(ref <CallApi>d__);
			return <CallApi>d__.<>t__builder.Task;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000041D0 File Offset: 0x000023D0
		[DebuggerStepThrough]
		public static Task<string> TestConnection()
		{
			SupabaseService.<TestConnection>d__13 <TestConnection>d__ = new SupabaseService.<TestConnection>d__13();
			<TestConnection>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<TestConnection>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<string> <>t__builder = <TestConnection>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<TestConnection>d__13>(ref <TestConnection>d__);
			return <TestConnection>d__.<>t__builder.Task;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004210 File Offset: 0x00002410
		[DebuggerStepThrough]
		public static Task<UserAccount> SignUp(string email, string password, string username)
		{
			SupabaseService.<SignUp>d__14 <SignUp>d__ = new SupabaseService.<SignUp>d__14();
			<SignUp>d__.email = email;
			<SignUp>d__.password = password;
			<SignUp>d__.username = username;
			<SignUp>d__.<>t__builder = AsyncTaskMethodBuilder<UserAccount>.Create();
			<SignUp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<UserAccount> <>t__builder = <SignUp>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SignUp>d__14>(ref <SignUp>d__);
			return <SignUp>d__.<>t__builder.Task;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004268 File Offset: 0x00002468
		[DebuggerStepThrough]
		public static Task<UserAccount> SignIn(string email, string password)
		{
			SupabaseService.<SignIn>d__15 <SignIn>d__ = new SupabaseService.<SignIn>d__15();
			<SignIn>d__.email = email;
			<SignIn>d__.password = password;
			<SignIn>d__.<>t__builder = AsyncTaskMethodBuilder<UserAccount>.Create();
			<SignIn>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<UserAccount> <>t__builder = <SignIn>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SignIn>d__15>(ref <SignIn>d__);
			return <SignIn>d__.<>t__builder.Task;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000042B8 File Offset: 0x000024B8
		public static void SignOut()
		{
			SupabaseService._localSettings.Values["SupabaseToken"] = null;
			SupabaseService._localSettings.Values["UserEmail"] = null;
			SupabaseService._localSettings.Values["UserId"] = null;
			SupabaseService._localSettings.Values["Username"] = null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004320 File Offset: 0x00002520
		public static bool IsUserLoggedIn()
		{
			string text = SupabaseService._localSettings.Values["SupabaseToken"] as string;
			bool flag = !string.IsNullOrEmpty(text);
			Debug.WriteLine(string.Format("[IS LOGGED IN] Token exists: {0}", new object[]
			{
				flag
			}));
			return flag;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004378 File Offset: 0x00002578
		public static UserAccount GetCurrentUser()
		{
			bool flag = !SupabaseService.IsUserLoggedIn();
			UserAccount result;
			if (flag)
			{
				Debug.WriteLine("[GET CURRENT USER] Not logged in");
				result = null;
			}
			else
			{
				UserAccount userAccount = new UserAccount
				{
					Id = ((SupabaseService._localSettings.Values["UserId"] as string) ?? ""),
					Email = ((SupabaseService._localSettings.Values["UserEmail"] as string) ?? ""),
					Username = ((SupabaseService._localSettings.Values["Username"] as string) ?? "")
				};
				Debug.WriteLine(string.Format("[GET CURRENT USER] Found: {0}", new object[]
				{
					userAccount.Username
				}));
				result = userAccount;
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000444C File Offset: 0x0000264C
		private static void SaveUserSession(string token, UserAccount account)
		{
			SupabaseService._localSettings.Values["SupabaseToken"] = token;
			SupabaseService._localSettings.Values["UserEmail"] = account.Email;
			SupabaseService._localSettings.Values["UserId"] = account.Id;
			SupabaseService._localSettings.Values["Username"] = account.Username;
			Debug.WriteLine(string.Format("[SAVE SESSION] Saved session for: {0}", new object[]
			{
				account.Username
			}));
			Debug.WriteLine(string.Format("[SAVE SESSION] Token saved: {0}", new object[]
			{
				!string.IsNullOrEmpty(token)
			}));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004508 File Offset: 0x00002708
		private static string GetAuthToken()
		{
			string text = SupabaseService._localSettings.Values["SupabaseToken"] as string;
			Debug.WriteLine(string.Format("[GET AUTH TOKEN] Token exists: {0}", new object[]
			{
				!string.IsNullOrEmpty(text)
			}));
			return text;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000455C File Offset: 0x0000275C
		[DebuggerStepThrough]
		public static Task<List<Review>> GetReviews(string appId)
		{
			SupabaseService.<GetReviews>d__21 <GetReviews>d__ = new SupabaseService.<GetReviews>d__21();
			<GetReviews>d__.appId = appId;
			<GetReviews>d__.<>t__builder = AsyncTaskMethodBuilder<List<Review>>.Create();
			<GetReviews>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<List<Review>> <>t__builder = <GetReviews>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetReviews>d__21>(ref <GetReviews>d__);
			return <GetReviews>d__.<>t__builder.Task;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000045A4 File Offset: 0x000027A4
		public static string GetAuthTokenValue()
		{
			return (SupabaseService._localSettings.Values["SupabaseToken"] as string) ?? "";
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000045D8 File Offset: 0x000027D8
		[DebuggerStepThrough]
		public static Task<ReviewResult> SubmitReview(string appId, int rating, string comment)
		{
			SupabaseService.<SubmitReview>d__23 <SubmitReview>d__ = new SupabaseService.<SubmitReview>d__23();
			<SubmitReview>d__.appId = appId;
			<SubmitReview>d__.rating = rating;
			<SubmitReview>d__.comment = comment;
			<SubmitReview>d__.<>t__builder = AsyncTaskMethodBuilder<ReviewResult>.Create();
			<SubmitReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<ReviewResult> <>t__builder = <SubmitReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<SubmitReview>d__23>(ref <SubmitReview>d__);
			return <SubmitReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004630 File Offset: 0x00002830
		[DebuggerStepThrough]
		public static Task<bool> DeleteReview(int reviewId)
		{
			SupabaseService.<DeleteReview>d__24 <DeleteReview>d__ = new SupabaseService.<DeleteReview>d__24();
			<DeleteReview>d__.reviewId = reviewId;
			<DeleteReview>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <DeleteReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<DeleteReview>d__24>(ref <DeleteReview>d__);
			return <DeleteReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004678 File Offset: 0x00002878
		[DebuggerStepThrough]
		public static Task<List<Review>> GetUserReviews()
		{
			SupabaseService.<GetUserReviews>d__25 <GetUserReviews>d__ = new SupabaseService.<GetUserReviews>d__25();
			<GetUserReviews>d__.<>t__builder = AsyncTaskMethodBuilder<List<Review>>.Create();
			<GetUserReviews>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<List<Review>> <>t__builder = <GetUserReviews>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetUserReviews>d__25>(ref <GetUserReviews>d__);
			return <GetUserReviews>d__.<>t__builder.Task;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000046B8 File Offset: 0x000028B8
		[DebuggerStepThrough]
		public static Task<Review> GetReview(int reviewId)
		{
			SupabaseService.<GetReview>d__26 <GetReview>d__ = new SupabaseService.<GetReview>d__26();
			<GetReview>d__.reviewId = reviewId;
			<GetReview>d__.<>t__builder = AsyncTaskMethodBuilder<Review>.Create();
			<GetReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<Review> <>t__builder = <GetReview>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetReview>d__26>(ref <GetReview>d__);
			return <GetReview>d__.<>t__builder.Task;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004700 File Offset: 0x00002900
		[DebuggerStepThrough]
		public static Task<Review> GetUserReviewForApp(string appId)
		{
			SupabaseService.<GetUserReviewForApp>d__27 <GetUserReviewForApp>d__ = new SupabaseService.<GetUserReviewForApp>d__27();
			<GetUserReviewForApp>d__.appId = appId;
			<GetUserReviewForApp>d__.<>t__builder = AsyncTaskMethodBuilder<Review>.Create();
			<GetUserReviewForApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<Review> <>t__builder = <GetUserReviewForApp>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetUserReviewForApp>d__27>(ref <GetUserReviewForApp>d__);
			return <GetUserReviewForApp>d__.<>t__builder.Task;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004748 File Offset: 0x00002948
		[DebuggerStepThrough]
		public static Task<bool> ValidateToken()
		{
			SupabaseService.<ValidateToken>d__28 <ValidateToken>d__ = new SupabaseService.<ValidateToken>d__28();
			<ValidateToken>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ValidateToken>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <ValidateToken>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<ValidateToken>d__28>(ref <ValidateToken>d__);
			return <ValidateToken>d__.<>t__builder.Task;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004788 File Offset: 0x00002988
		[DebuggerStepThrough]
		public static Task<Dictionary<string, ReviewStats>> GetAllReviewStats()
		{
			SupabaseService.<GetAllReviewStats>d__29 <GetAllReviewStats>d__ = new SupabaseService.<GetAllReviewStats>d__29();
			<GetAllReviewStats>d__.<>t__builder = AsyncTaskMethodBuilder<Dictionary<string, ReviewStats>>.Create();
			<GetAllReviewStats>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<Dictionary<string, ReviewStats>> <>t__builder = <GetAllReviewStats>d__.<>t__builder;
			<>t__builder.Start<SupabaseService.<GetAllReviewStats>d__29>(ref <GetAllReviewStats>d__);
			return <GetAllReviewStats>d__.<>t__builder.Task;
		}

		// Token: 0x0400005B RID: 91
		private const string ApiUrl = "https://8store.modyleprojects.ru/api/auth";

		// Token: 0x0400005C RID: 92
		private static ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

		// Token: 0x0400005D RID: 93
		private static Dictionary<string, ReviewStats> _cachedReviewStats = new Dictionary<string, ReviewStats>();

		// Token: 0x0400005E RID: 94
		private static DateTime _lastCacheTime = DateTime.MinValue;

		// Token: 0x0400005F RID: 95
		private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5.0);
	}
}

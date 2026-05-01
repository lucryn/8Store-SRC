using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Win81StoreRevival.Account;
using Win81StoreRevival.Common;
using Win81StoreRevival.Flyouts;
using Win81StoreRevival.Themes;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x0200004A RID: 74
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlTypeInfoProvider
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x00017C28 File Offset: 0x00015E28
		public IXamlType GetXamlTypeByType(Type type)
		{
			IXamlType xamlType;
			if (this._xamlTypeCacheByType.TryGetValue(type, ref xamlType))
			{
				return xamlType;
			}
			int num = this.LookupTypeIndexByType(type);
			if (num != -1)
			{
				xamlType = this.CreateXamlType(num);
			}
			if (xamlType != null)
			{
				this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
				this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
			}
			return xamlType;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00017C84 File Offset: 0x00015E84
		public IXamlType GetXamlTypeByName(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return null;
			}
			IXamlType xamlType;
			if (this._xamlTypeCacheByName.TryGetValue(typeName, ref xamlType))
			{
				return xamlType;
			}
			int num = this.LookupTypeIndexByName(typeName);
			if (num != -1)
			{
				xamlType = this.CreateXamlType(num);
			}
			if (xamlType != null)
			{
				this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
				this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
			}
			return xamlType;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00017CEC File Offset: 0x00015EEC
		public IXamlMember GetMemberByLongName(string longMemberName)
		{
			if (string.IsNullOrEmpty(longMemberName))
			{
				return null;
			}
			IXamlMember xamlMember;
			if (this._xamlMembers.TryGetValue(longMemberName, ref xamlMember))
			{
				return xamlMember;
			}
			xamlMember = this.CreateXamlMember(longMemberName);
			if (xamlMember != null)
			{
				this._xamlMembers.Add(longMemberName, xamlMember);
			}
			return xamlMember;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00017D30 File Offset: 0x00015F30
		private void InitTypeTables()
		{
			this._typeNameTable = new string[66];
			this._typeNameTable[0] = "Win81StoreRevival.Themes.Control";
			this._typeNameTable[1] = "Object";
			this._typeNameTable[2] = "Windows.UI.Xaml.Media.SolidColorBrush";
			this._typeNameTable[3] = "Win81StoreRevival.BooleanToVisibilityConverter";
			this._typeNameTable[4] = "Win81StoreRevival.DescriptionPlaceholderConverter";
			this._typeNameTable[5] = "Win81StoreRevival.RatingToRectangleConverter";
			this._typeNameTable[6] = "Win81StoreRevival.ZeroToVisibilityConverter";
			this._typeNameTable[7] = "Win81StoreRevival.StringToBitmapImageConverter";
			this._typeNameTable[8] = "Win81StoreRevival.AboutSettingsFlyout";
			this._typeNameTable[9] = "Windows.UI.Xaml.Controls.SettingsFlyout";
			this._typeNameTable[10] = "Windows.UI.Xaml.Controls.ContentControl";
			this._typeNameTable[11] = "Win81StoreRevival.Common.ColorConverter";
			this._typeNameTable[12] = "Win81StoreRevival.AccountPage";
			this._typeNameTable[13] = "Windows.UI.Xaml.Controls.Page";
			this._typeNameTable[14] = "Windows.UI.Xaml.Controls.UserControl";
			this._typeNameTable[15] = "Win81StoreRevival.Account.Discord";
			this._typeNameTable[16] = "Win81StoreRevival.Account.Login";
			this._typeNameTable[17] = "Win81StoreRevival.Account.Register";
			this._typeNameTable[18] = "Win81StoreRevival.AddRating";
			this._typeNameTable[19] = "String";
			this._typeNameTable[20] = "Win81StoreRevival.ReviewStats";
			this._typeNameTable[21] = "Win81StoreRevival.AllAppsPage";
			this._typeNameTable[22] = "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.AppCollection>";
			this._typeNameTable[23] = "System.Collections.ObjectModel.Collection`1<Win81StoreRevival.AppCollection>";
			this._typeNameTable[24] = "Win81StoreRevival.AppCollection";
			this._typeNameTable[25] = "System.Collections.Generic.List`1<String>";
			this._typeNameTable[26] = "Win81StoreRevival.AllAppsPage1";
			this._typeNameTable[27] = "Win81StoreRevival.AppDetailsPage";
			this._typeNameTable[28] = "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.Review>";
			this._typeNameTable[29] = "System.Collections.ObjectModel.Collection`1<Win81StoreRevival.Review>";
			this._typeNameTable[30] = "Win81StoreRevival.Review";
			this._typeNameTable[31] = "Boolean";
			this._typeNameTable[32] = "Int32";
			this._typeNameTable[33] = "System.DateTime";
			this._typeNameTable[34] = "System.ValueType";
			this._typeNameTable[35] = "System.Collections.Generic.List`1<Int32>";
			this._typeNameTable[36] = "System.Collections.Generic.List`1<Double>";
			this._typeNameTable[37] = "Double";
			this._typeNameTable[38] = "Win81StoreRevival.Common.NavigationHelper";
			this._typeNameTable[39] = "Windows.UI.Xaml.DependencyObject";
			this._typeNameTable[40] = "Win81StoreRevival.Common.ObservableDictionary";
			this._typeNameTable[41] = "Win81StoreRevival.AppReviewsPage";
			this._typeNameTable[42] = "Win81StoreRevival.CollectionsViewPage";
			this._typeNameTable[43] = "Win81StoreRevival.CosmeticFlyout";
			this._typeNameTable[44] = "Win81StoreRevival.DownloadsHub";
			this._typeNameTable[45] = "Win81StoreRevival.FatalErrorScreen";
			this._typeNameTable[46] = "Win81StoreRevival.FactoryResetFlyout";
			this._typeNameTable[47] = "Win81StoreRevival.FullScreenViewPage";
			this._typeNameTable[48] = "Win81StoreRevival.GeneralSettingsFlyout";
			this._typeNameTable[49] = "Win81StoreRevival.InstalledAppsPage";
			this._typeNameTable[50] = "Win81StoreRevival.LanguageSettingsFlyout";
			this._typeNameTable[51] = "Win81StoreRevival.Flyouts.LoginFlyout";
			this._typeNameTable[52] = "Win81StoreRevival.MainPage";
			this._typeNameTable[53] = "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.StoreApp>";
			this._typeNameTable[54] = "System.Collections.ObjectModel.Collection`1<Win81StoreRevival.StoreApp>";
			this._typeNameTable[55] = "Win81StoreRevival.StoreApp";
			this._typeNameTable[56] = "Win81StoreRevival.RatingSummary";
			this._typeNameTable[57] = "System.Collections.Generic.List`1<Win81StoreRevival.StoreApp>";
			this._typeNameTable[58] = "Win81StoreRevival.PopularPage";
			this._typeNameTable[59] = "Win81StoreRevival.PublisherAppsPage";
			this._typeNameTable[60] = "Win81StoreRevival.Flyouts.RegisterFlyout";
			this._typeNameTable[61] = "Win81StoreRevival.SearchResultsPage";
			this._typeNameTable[62] = "Win81StoreRevival.SettingsPage";
			this._typeNameTable[63] = "Windows.UI.Color";
			this._typeNameTable[64] = "Byte";
			this._typeNameTable[65] = "Win81StoreRevival.UpdatePage";
			this._typeTable = new Type[66];
			this._typeTable[0] = typeof(Control);
			this._typeTable[1] = typeof(object);
			this._typeTable[2] = typeof(SolidColorBrush);
			this._typeTable[3] = typeof(BooleanToVisibilityConverter);
			this._typeTable[4] = typeof(DescriptionPlaceholderConverter);
			this._typeTable[5] = typeof(RatingToRectangleConverter);
			this._typeTable[6] = typeof(ZeroToVisibilityConverter);
			this._typeTable[7] = typeof(StringToBitmapImageConverter);
			this._typeTable[8] = typeof(AboutSettingsFlyout);
			this._typeTable[9] = typeof(SettingsFlyout);
			this._typeTable[10] = typeof(ContentControl);
			this._typeTable[11] = typeof(ColorConverter);
			this._typeTable[12] = typeof(AccountPage);
			this._typeTable[13] = typeof(Page);
			this._typeTable[14] = typeof(UserControl);
			this._typeTable[15] = typeof(Discord);
			this._typeTable[16] = typeof(Login);
			this._typeTable[17] = typeof(Register);
			this._typeTable[18] = typeof(AddRating);
			this._typeTable[19] = typeof(string);
			this._typeTable[20] = typeof(ReviewStats);
			this._typeTable[21] = typeof(AllAppsPage);
			this._typeTable[22] = typeof(ObservableCollection<AppCollection>);
			this._typeTable[23] = typeof(Collection<AppCollection>);
			this._typeTable[24] = typeof(AppCollection);
			this._typeTable[25] = typeof(List<string>);
			this._typeTable[26] = typeof(AllAppsPage1);
			this._typeTable[27] = typeof(AppDetailsPage);
			this._typeTable[28] = typeof(ObservableCollection<Review>);
			this._typeTable[29] = typeof(Collection<Review>);
			this._typeTable[30] = typeof(Review);
			this._typeTable[31] = typeof(bool);
			this._typeTable[32] = typeof(int);
			this._typeTable[33] = typeof(DateTime);
			this._typeTable[34] = typeof(ValueType);
			this._typeTable[35] = typeof(List<int>);
			this._typeTable[36] = typeof(List<double>);
			this._typeTable[37] = typeof(double);
			this._typeTable[38] = typeof(NavigationHelper);
			this._typeTable[39] = typeof(DependencyObject);
			this._typeTable[40] = typeof(ObservableDictionary);
			this._typeTable[41] = typeof(AppReviewsPage);
			this._typeTable[42] = typeof(CollectionsViewPage);
			this._typeTable[43] = typeof(CosmeticFlyout);
			this._typeTable[44] = typeof(DownloadsHub);
			this._typeTable[45] = typeof(FatalErrorScreen);
			this._typeTable[46] = typeof(FactoryResetFlyout);
			this._typeTable[47] = typeof(FullScreenViewPage);
			this._typeTable[48] = typeof(GeneralSettingsFlyout);
			this._typeTable[49] = typeof(InstalledAppsPage);
			this._typeTable[50] = typeof(LanguageSettingsFlyout);
			this._typeTable[51] = typeof(LoginFlyout);
			this._typeTable[52] = typeof(MainPage);
			this._typeTable[53] = typeof(ObservableCollection<StoreApp>);
			this._typeTable[54] = typeof(Collection<StoreApp>);
			this._typeTable[55] = typeof(StoreApp);
			this._typeTable[56] = typeof(RatingSummary);
			this._typeTable[57] = typeof(List<StoreApp>);
			this._typeTable[58] = typeof(PopularPage);
			this._typeTable[59] = typeof(PublisherAppsPage);
			this._typeTable[60] = typeof(RegisterFlyout);
			this._typeTable[61] = typeof(SearchResultsPage);
			this._typeTable[62] = typeof(SettingsPage);
			this._typeTable[63] = typeof(Color);
			this._typeTable[64] = typeof(byte);
			this._typeTable[65] = typeof(UpdatePage);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000185C8 File Offset: 0x000167C8
		private int LookupTypeIndexByName(string typeName)
		{
			if (this._typeNameTable == null)
			{
				this.InitTypeTables();
			}
			for (int i = 0; i < this._typeNameTable.Length; i++)
			{
				if (string.CompareOrdinal(this._typeNameTable[i], typeName) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001860C File Offset: 0x0001680C
		private int LookupTypeIndexByType(Type type)
		{
			if (this._typeTable == null)
			{
				this.InitTypeTables();
			}
			for (int i = 0; i < this._typeTable.Length; i++)
			{
				if (type == this._typeTable[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00018648 File Offset: 0x00016848
		private object Activate_0_Control()
		{
			return new Control();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001864F File Offset: 0x0001684F
		private object Activate_3_BooleanToVisibilityConverter()
		{
			return new BooleanToVisibilityConverter();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00018656 File Offset: 0x00016856
		private object Activate_4_DescriptionPlaceholderConverter()
		{
			return new DescriptionPlaceholderConverter();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001865D File Offset: 0x0001685D
		private object Activate_5_RatingToRectangleConverter()
		{
			return new RatingToRectangleConverter();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00018664 File Offset: 0x00016864
		private object Activate_6_ZeroToVisibilityConverter()
		{
			return new ZeroToVisibilityConverter();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001866B File Offset: 0x0001686B
		private object Activate_7_StringToBitmapImageConverter()
		{
			return new StringToBitmapImageConverter();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00018672 File Offset: 0x00016872
		private object Activate_8_AboutSettingsFlyout()
		{
			return new AboutSettingsFlyout();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00018679 File Offset: 0x00016879
		private object Activate_11_ColorConverter()
		{
			return new ColorConverter();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00018680 File Offset: 0x00016880
		private object Activate_12_AccountPage()
		{
			return new AccountPage();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00018687 File Offset: 0x00016887
		private object Activate_15_Discord()
		{
			return new Discord();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001868E File Offset: 0x0001688E
		private object Activate_16_Login()
		{
			return new Login();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00018695 File Offset: 0x00016895
		private object Activate_17_Register()
		{
			return new Register();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001869C File Offset: 0x0001689C
		private object Activate_18_AddRating()
		{
			return new AddRating();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000186A3 File Offset: 0x000168A3
		private object Activate_20_ReviewStats()
		{
			return new ReviewStats();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000186AA File Offset: 0x000168AA
		private object Activate_21_AllAppsPage()
		{
			return new AllAppsPage();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000186B1 File Offset: 0x000168B1
		private object Activate_22_ObservableCollection()
		{
			return new ObservableCollection<AppCollection>();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000186B8 File Offset: 0x000168B8
		private object Activate_23_Collection()
		{
			return new Collection<AppCollection>();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000186BF File Offset: 0x000168BF
		private object Activate_24_AppCollection()
		{
			return new AppCollection();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000186C6 File Offset: 0x000168C6
		private object Activate_25_List()
		{
			return new List<string>();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000186CD File Offset: 0x000168CD
		private object Activate_26_AllAppsPage1()
		{
			return new AllAppsPage1();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000186D4 File Offset: 0x000168D4
		private object Activate_27_AppDetailsPage()
		{
			return new AppDetailsPage();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000186DB File Offset: 0x000168DB
		private object Activate_28_ObservableCollection()
		{
			return new ObservableCollection<Review>();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000186E2 File Offset: 0x000168E2
		private object Activate_29_Collection()
		{
			return new Collection<Review>();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000186E9 File Offset: 0x000168E9
		private object Activate_30_Review()
		{
			return new Review();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000186F0 File Offset: 0x000168F0
		private object Activate_35_List()
		{
			return new List<int>();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000186F7 File Offset: 0x000168F7
		private object Activate_36_List()
		{
			return new List<double>();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000186FE File Offset: 0x000168FE
		private object Activate_40_ObservableDictionary()
		{
			return new ObservableDictionary();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00018705 File Offset: 0x00016905
		private object Activate_41_AppReviewsPage()
		{
			return new AppReviewsPage();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001870C File Offset: 0x0001690C
		private object Activate_42_CollectionsViewPage()
		{
			return new CollectionsViewPage();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00018713 File Offset: 0x00016913
		private object Activate_43_CosmeticFlyout()
		{
			return new CosmeticFlyout();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001871A File Offset: 0x0001691A
		private object Activate_44_DownloadsHub()
		{
			return new DownloadsHub();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00018721 File Offset: 0x00016921
		private object Activate_45_FatalErrorScreen()
		{
			return new FatalErrorScreen();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00018728 File Offset: 0x00016928
		private object Activate_46_FactoryResetFlyout()
		{
			return new FactoryResetFlyout();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001872F File Offset: 0x0001692F
		private object Activate_47_FullScreenViewPage()
		{
			return new FullScreenViewPage();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00018736 File Offset: 0x00016936
		private object Activate_48_GeneralSettingsFlyout()
		{
			return new GeneralSettingsFlyout();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001873D File Offset: 0x0001693D
		private object Activate_49_InstalledAppsPage()
		{
			return new InstalledAppsPage();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00018744 File Offset: 0x00016944
		private object Activate_50_LanguageSettingsFlyout()
		{
			return new LanguageSettingsFlyout();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001874B File Offset: 0x0001694B
		private object Activate_51_LoginFlyout()
		{
			return new LoginFlyout();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00018752 File Offset: 0x00016952
		private object Activate_52_MainPage()
		{
			return new MainPage();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00018759 File Offset: 0x00016959
		private object Activate_53_ObservableCollection()
		{
			return new ObservableCollection<StoreApp>();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00018760 File Offset: 0x00016960
		private object Activate_54_Collection()
		{
			return new Collection<StoreApp>();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00018767 File Offset: 0x00016967
		private object Activate_55_StoreApp()
		{
			return new StoreApp();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001876E File Offset: 0x0001696E
		private object Activate_56_RatingSummary()
		{
			return new RatingSummary();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00018775 File Offset: 0x00016975
		private object Activate_57_List()
		{
			return new List<StoreApp>();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001877C File Offset: 0x0001697C
		private object Activate_58_PopularPage()
		{
			return new PopularPage();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00018783 File Offset: 0x00016983
		private object Activate_59_PublisherAppsPage()
		{
			return new PublisherAppsPage();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001878A File Offset: 0x0001698A
		private object Activate_60_RegisterFlyout()
		{
			return new RegisterFlyout();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00018791 File Offset: 0x00016991
		private object Activate_61_SearchResultsPage()
		{
			return new SearchResultsPage();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00018798 File Offset: 0x00016998
		private object Activate_62_SettingsPage()
		{
			return new SettingsPage();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001879F File Offset: 0x0001699F
		private object Activate_65_UpdatePage()
		{
			return new UpdatePage();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000187A8 File Offset: 0x000169A8
		private void VectorAdd_22_ObservableCollection(object instance, object item)
		{
			ICollection<AppCollection> collection = (ICollection<AppCollection>)instance;
			AppCollection appCollection = (AppCollection)item;
			collection.Add(appCollection);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000187C8 File Offset: 0x000169C8
		private void VectorAdd_23_Collection(object instance, object item)
		{
			ICollection<AppCollection> collection = (ICollection<AppCollection>)instance;
			AppCollection appCollection = (AppCollection)item;
			collection.Add(appCollection);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000187E8 File Offset: 0x000169E8
		private void VectorAdd_25_List(object instance, object item)
		{
			ICollection<string> collection = (ICollection<string>)instance;
			string text = (string)item;
			collection.Add(text);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00018808 File Offset: 0x00016A08
		private void VectorAdd_28_ObservableCollection(object instance, object item)
		{
			ICollection<Review> collection = (ICollection<Review>)instance;
			Review review = (Review)item;
			collection.Add(review);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00018828 File Offset: 0x00016A28
		private void VectorAdd_29_Collection(object instance, object item)
		{
			ICollection<Review> collection = (ICollection<Review>)instance;
			Review review = (Review)item;
			collection.Add(review);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00018848 File Offset: 0x00016A48
		private void VectorAdd_35_List(object instance, object item)
		{
			ICollection<int> collection = (ICollection<int>)instance;
			int num = (int)item;
			collection.Add(num);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00018868 File Offset: 0x00016A68
		private void VectorAdd_36_List(object instance, object item)
		{
			ICollection<double> collection = (ICollection<double>)instance;
			double num = (double)item;
			collection.Add(num);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00018888 File Offset: 0x00016A88
		private void MapAdd_40_ObservableDictionary(object instance, object key, object item)
		{
			IDictionary<string, object> dictionary = (IDictionary<string, object>)instance;
			string text = (string)key;
			dictionary.Add(text, item);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000188AC File Offset: 0x00016AAC
		private void VectorAdd_53_ObservableCollection(object instance, object item)
		{
			ICollection<StoreApp> collection = (ICollection<StoreApp>)instance;
			StoreApp storeApp = (StoreApp)item;
			collection.Add(storeApp);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000188CC File Offset: 0x00016ACC
		private void VectorAdd_54_Collection(object instance, object item)
		{
			ICollection<StoreApp> collection = (ICollection<StoreApp>)instance;
			StoreApp storeApp = (StoreApp)item;
			collection.Add(storeApp);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000188EC File Offset: 0x00016AEC
		private void VectorAdd_57_List(object instance, object item)
		{
			ICollection<StoreApp> collection = (ICollection<StoreApp>)instance;
			StoreApp storeApp = (StoreApp)item;
			collection.Add(storeApp);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001890C File Offset: 0x00016B0C
		private IXamlType CreateXamlType(int typeIndex)
		{
			XamlSystemBaseType result = null;
			string fullName = this._typeNameTable[typeIndex];
			Type type = this._typeTable[typeIndex];
			switch (typeIndex)
			{
			case 0:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.Activator = new Activator(this.Activate_0_Control);
				xamlUserType.AddMemberName("Accent");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 1:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 2:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 3:
			{
				XamlUserType xamlUserType2 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType2.Activator = new Activator(this.Activate_3_BooleanToVisibilityConverter);
				xamlUserType2.SetIsLocalType();
				result = xamlUserType2;
				break;
			}
			case 4:
			{
				XamlUserType xamlUserType3 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType3.Activator = new Activator(this.Activate_4_DescriptionPlaceholderConverter);
				xamlUserType3.SetIsLocalType();
				result = xamlUserType3;
				break;
			}
			case 5:
			{
				XamlUserType xamlUserType4 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType4.Activator = new Activator(this.Activate_5_RatingToRectangleConverter);
				xamlUserType4.SetIsLocalType();
				result = xamlUserType4;
				break;
			}
			case 6:
			{
				XamlUserType xamlUserType5 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType5.Activator = new Activator(this.Activate_6_ZeroToVisibilityConverter);
				xamlUserType5.SetIsLocalType();
				result = xamlUserType5;
				break;
			}
			case 7:
			{
				XamlUserType xamlUserType6 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType6.Activator = new Activator(this.Activate_7_StringToBitmapImageConverter);
				xamlUserType6.SetIsLocalType();
				result = xamlUserType6;
				break;
			}
			case 8:
			{
				XamlUserType xamlUserType7 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType7.Activator = new Activator(this.Activate_8_AboutSettingsFlyout);
				xamlUserType7.SetIsLocalType();
				result = xamlUserType7;
				break;
			}
			case 9:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 10:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 11:
			{
				XamlUserType xamlUserType8 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType8.Activator = new Activator(this.Activate_11_ColorConverter);
				xamlUserType8.SetIsLocalType();
				result = xamlUserType8;
				break;
			}
			case 12:
			{
				XamlUserType xamlUserType9 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType9.Activator = new Activator(this.Activate_12_AccountPage);
				xamlUserType9.SetIsLocalType();
				result = xamlUserType9;
				break;
			}
			case 13:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 14:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 15:
			{
				XamlUserType xamlUserType10 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
				xamlUserType10.Activator = new Activator(this.Activate_15_Discord);
				xamlUserType10.SetIsLocalType();
				result = xamlUserType10;
				break;
			}
			case 16:
			{
				XamlUserType xamlUserType11 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
				xamlUserType11.Activator = new Activator(this.Activate_16_Login);
				xamlUserType11.SetIsLocalType();
				result = xamlUserType11;
				break;
			}
			case 17:
			{
				XamlUserType xamlUserType12 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
				xamlUserType12.Activator = new Activator(this.Activate_17_Register);
				xamlUserType12.SetIsLocalType();
				result = xamlUserType12;
				break;
			}
			case 18:
			{
				XamlUserType xamlUserType13 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType13.Activator = new Activator(this.Activate_18_AddRating);
				xamlUserType13.AddMemberName("AppId");
				xamlUserType13.AddMemberName("LastRatings");
				xamlUserType13.SetIsLocalType();
				result = xamlUserType13;
				break;
			}
			case 19:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 20:
			{
				XamlUserType xamlUserType14 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType14.SetIsReturnTypeStub();
				xamlUserType14.SetIsLocalType();
				result = xamlUserType14;
				break;
			}
			case 21:
			{
				XamlUserType xamlUserType15 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType15.Activator = new Activator(this.Activate_21_AllAppsPage);
				xamlUserType15.AddMemberName("AllCollections");
				xamlUserType15.SetIsLocalType();
				result = xamlUserType15;
				break;
			}
			case 22:
			{
				XamlUserType xamlUserType16 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Win81StoreRevival.AppCollection>"));
				xamlUserType16.CollectionAdd = new AddToCollection(this.VectorAdd_22_ObservableCollection);
				xamlUserType16.SetIsReturnTypeStub();
				result = xamlUserType16;
				break;
			}
			case 23:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_23_Collection),
					CollectionAdd = new AddToCollection(this.VectorAdd_23_Collection)
				};
				break;
			case 24:
			{
				XamlUserType xamlUserType17 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType17.Activator = new Activator(this.Activate_24_AppCollection);
				xamlUserType17.AddMemberName("Name");
				xamlUserType17.AddMemberName("DisplayName");
				xamlUserType17.AddMemberName("AppIcons");
				xamlUserType17.AddMemberName("AppCount");
				xamlUserType17.SetIsLocalType();
				result = xamlUserType17;
				break;
			}
			case 25:
			{
				XamlUserType xamlUserType18 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType18.CollectionAdd = new AddToCollection(this.VectorAdd_25_List);
				xamlUserType18.SetIsReturnTypeStub();
				result = xamlUserType18;
				break;
			}
			case 26:
			{
				XamlUserType xamlUserType19 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType19.Activator = new Activator(this.Activate_26_AllAppsPage1);
				xamlUserType19.SetIsLocalType();
				result = xamlUserType19;
				break;
			}
			case 27:
			{
				XamlUserType xamlUserType20 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType20.Activator = new Activator(this.Activate_27_AppDetailsPage);
				xamlUserType20.AddMemberName("FeaturedReviews");
				xamlUserType20.AddMemberName("NavigationHelper");
				xamlUserType20.AddMemberName("DefaultViewModel");
				xamlUserType20.SetIsLocalType();
				result = xamlUserType20;
				break;
			}
			case 28:
			{
				XamlUserType xamlUserType21 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Win81StoreRevival.Review>"));
				xamlUserType21.CollectionAdd = new AddToCollection(this.VectorAdd_28_ObservableCollection);
				xamlUserType21.SetIsReturnTypeStub();
				result = xamlUserType21;
				break;
			}
			case 29:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_29_Collection),
					CollectionAdd = new AddToCollection(this.VectorAdd_29_Collection)
				};
				break;
			case 30:
			{
				XamlUserType xamlUserType22 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType22.Activator = new Activator(this.Activate_30_Review);
				xamlUserType22.AddMemberName("IsExpanded");
				xamlUserType22.AddMemberName("Id");
				xamlUserType22.AddMemberName("AppId");
				xamlUserType22.AddMemberName("UserId");
				xamlUserType22.AddMemberName("Username");
				xamlUserType22.AddMemberName("Rating");
				xamlUserType22.AddMemberName("Comment");
				xamlUserType22.AddMemberName("CreatedAt");
				xamlUserType22.AddMemberName("IsOwnReview");
				xamlUserType22.AddMemberName("ShowShortComment");
				xamlUserType22.AddMemberName("ShowReadMore");
				xamlUserType22.AddMemberName("CommentPreview");
				xamlUserType22.AddMemberName("StarBg");
				xamlUserType22.AddMemberName("StarList");
				xamlUserType22.AddMemberName("DateDisplay");
				xamlUserType22.SetIsLocalType();
				result = xamlUserType22;
				break;
			}
			case 31:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 32:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 33:
			{
				XamlUserType xamlUserType23 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.ValueType"));
				xamlUserType23.SetIsReturnTypeStub();
				result = xamlUserType23;
				break;
			}
			case 34:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				break;
			case 35:
			{
				XamlUserType xamlUserType24 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType24.CollectionAdd = new AddToCollection(this.VectorAdd_35_List);
				xamlUserType24.SetIsReturnTypeStub();
				result = xamlUserType24;
				break;
			}
			case 36:
			{
				XamlUserType xamlUserType25 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType25.CollectionAdd = new AddToCollection(this.VectorAdd_36_List);
				xamlUserType25.SetIsReturnTypeStub();
				result = xamlUserType25;
				break;
			}
			case 37:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 38:
			{
				XamlUserType xamlUserType26 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.DependencyObject"));
				xamlUserType26.SetIsReturnTypeStub();
				xamlUserType26.SetIsLocalType();
				result = xamlUserType26;
				break;
			}
			case 39:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 40:
			{
				XamlUserType xamlUserType27 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType27.DictionaryAdd = new AddToDictionary(this.MapAdd_40_ObservableDictionary);
				xamlUserType27.SetIsReturnTypeStub();
				xamlUserType27.SetIsLocalType();
				result = xamlUserType27;
				break;
			}
			case 41:
			{
				XamlUserType xamlUserType28 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType28.Activator = new Activator(this.Activate_41_AppReviewsPage);
				xamlUserType28.AddMemberName("IsUserLoggedIn");
				xamlUserType28.AddMemberName("NavigationHelper");
				xamlUserType28.AddMemberName("DefaultViewModel");
				xamlUserType28.SetIsLocalType();
				result = xamlUserType28;
				break;
			}
			case 42:
			{
				XamlUserType xamlUserType29 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType29.Activator = new Activator(this.Activate_42_CollectionsViewPage);
				xamlUserType29.AddMemberName("AllCollections");
				xamlUserType29.SetIsLocalType();
				result = xamlUserType29;
				break;
			}
			case 43:
			{
				XamlUserType xamlUserType30 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType30.Activator = new Activator(this.Activate_43_CosmeticFlyout);
				xamlUserType30.SetIsLocalType();
				result = xamlUserType30;
				break;
			}
			case 44:
			{
				XamlUserType xamlUserType31 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType31.Activator = new Activator(this.Activate_44_DownloadsHub);
				xamlUserType31.SetIsLocalType();
				result = xamlUserType31;
				break;
			}
			case 45:
			{
				XamlUserType xamlUserType32 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType32.Activator = new Activator(this.Activate_45_FatalErrorScreen);
				xamlUserType32.AddMemberName("DefaultViewModel");
				xamlUserType32.AddMemberName("NavigationHelper");
				xamlUserType32.SetIsLocalType();
				result = xamlUserType32;
				break;
			}
			case 46:
			{
				XamlUserType xamlUserType33 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType33.Activator = new Activator(this.Activate_46_FactoryResetFlyout);
				xamlUserType33.SetIsLocalType();
				result = xamlUserType33;
				break;
			}
			case 47:
			{
				XamlUserType xamlUserType34 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType34.Activator = new Activator(this.Activate_47_FullScreenViewPage);
				xamlUserType34.SetIsLocalType();
				result = xamlUserType34;
				break;
			}
			case 48:
			{
				XamlUserType xamlUserType35 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType35.Activator = new Activator(this.Activate_48_GeneralSettingsFlyout);
				xamlUserType35.SetIsLocalType();
				result = xamlUserType35;
				break;
			}
			case 49:
			{
				XamlUserType xamlUserType36 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType36.Activator = new Activator(this.Activate_49_InstalledAppsPage);
				xamlUserType36.SetIsLocalType();
				result = xamlUserType36;
				break;
			}
			case 50:
			{
				XamlUserType xamlUserType37 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType37.Activator = new Activator(this.Activate_50_LanguageSettingsFlyout);
				xamlUserType37.SetIsLocalType();
				result = xamlUserType37;
				break;
			}
			case 51:
			{
				XamlUserType xamlUserType38 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType38.Activator = new Activator(this.Activate_51_LoginFlyout);
				xamlUserType38.SetIsLocalType();
				result = xamlUserType38;
				break;
			}
			case 52:
			{
				XamlUserType xamlUserType39 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType39.Activator = new Activator(this.Activate_52_MainPage);
				xamlUserType39.AddMemberName("RandomApps");
				xamlUserType39.AddMemberName("TotalAppsCount");
				xamlUserType39.AddMemberName("UniquePublishersCount");
				xamlUserType39.SetIsLocalType();
				result = xamlUserType39;
				break;
			}
			case 53:
			{
				XamlUserType xamlUserType40 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Win81StoreRevival.StoreApp>"));
				xamlUserType40.CollectionAdd = new AddToCollection(this.VectorAdd_53_ObservableCollection);
				xamlUserType40.SetIsReturnTypeStub();
				result = xamlUserType40;
				break;
			}
			case 54:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_54_Collection),
					CollectionAdd = new AddToCollection(this.VectorAdd_54_Collection)
				};
				break;
			case 55:
			{
				XamlUserType xamlUserType41 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType41.Activator = new Activator(this.Activate_55_StoreApp);
				xamlUserType41.AddMemberName("Id");
				xamlUserType41.AddMemberName("AuthorId");
				xamlUserType41.AddMemberName("Name");
				xamlUserType41.AddMemberName("Publisher");
				xamlUserType41.AddMemberName("Version");
				xamlUserType41.AddMemberName("DownloadUrl");
				xamlUserType41.AddMemberName("IconUrl");
				xamlUserType41.AddMemberName("Description");
				xamlUserType41.AddMemberName("Featured");
				xamlUserType41.AddMemberName("Type");
				xamlUserType41.AddMemberName("Category");
				xamlUserType41.AddMemberName("AppType");
				xamlUserType41.AddMemberName("PackageFileName");
				xamlUserType41.AddMemberName("ScreenshotUrls");
				xamlUserType41.AddMemberName("Status");
				xamlUserType41.AddMemberName("TargetOS");
				xamlUserType41.AddMemberName("Rating");
				xamlUserType41.AddMemberName("RatingCount");
				xamlUserType41.AddMemberName("ReviewCount");
				xamlUserType41.AddMemberName("PendingUpdate");
				xamlUserType41.AddMemberName("Update");
				xamlUserType41.AddMemberName("Ratings");
				xamlUserType41.AddMemberName("ApproximateSizeText");
				xamlUserType41.AddMemberName("AppSummaryText");
				xamlUserType41.AddMemberName("StarBg");
				xamlUserType41.AddMemberName("StarList");
				xamlUserType41.AddMemberName("Screenshot1");
				xamlUserType41.AddMemberName("Screenshot2");
				xamlUserType41.AddMemberName("Screenshot3");
				xamlUserType41.AddMemberName("ReviewStats");
				xamlUserType41.AddMemberName("Screenshots");
				xamlUserType41.AddMemberName("RelatedApps");
				xamlUserType41.AddMemberName("SpotlightBanner");
				xamlUserType41.SetIsLocalType();
				result = xamlUserType41;
				break;
			}
			case 56:
			{
				XamlUserType xamlUserType42 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType42.SetIsReturnTypeStub();
				xamlUserType42.SetIsLocalType();
				result = xamlUserType42;
				break;
			}
			case 57:
			{
				XamlUserType xamlUserType43 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType43.CollectionAdd = new AddToCollection(this.VectorAdd_57_List);
				xamlUserType43.SetIsReturnTypeStub();
				result = xamlUserType43;
				break;
			}
			case 58:
			{
				XamlUserType xamlUserType44 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType44.Activator = new Activator(this.Activate_58_PopularPage);
				xamlUserType44.SetIsLocalType();
				result = xamlUserType44;
				break;
			}
			case 59:
			{
				XamlUserType xamlUserType45 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType45.Activator = new Activator(this.Activate_59_PublisherAppsPage);
				xamlUserType45.SetIsLocalType();
				result = xamlUserType45;
				break;
			}
			case 60:
			{
				XamlUserType xamlUserType46 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType46.Activator = new Activator(this.Activate_60_RegisterFlyout);
				xamlUserType46.SetIsLocalType();
				result = xamlUserType46;
				break;
			}
			case 61:
			{
				XamlUserType xamlUserType47 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType47.Activator = new Activator(this.Activate_61_SearchResultsPage);
				xamlUserType47.SetIsLocalType();
				result = xamlUserType47;
				break;
			}
			case 62:
			{
				XamlUserType xamlUserType48 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType48.Activator = new Activator(this.Activate_62_SettingsPage);
				xamlUserType48.SetIsLocalType();
				result = xamlUserType48;
				break;
			}
			case 63:
			{
				XamlUserType xamlUserType49 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.ValueType"));
				xamlUserType49.AddMemberName("A");
				xamlUserType49.AddMemberName("B");
				xamlUserType49.AddMemberName("G");
				xamlUserType49.AddMemberName("R");
				result = xamlUserType49;
				break;
			}
			case 64:
			{
				XamlUserType xamlUserType50 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.ValueType"));
				xamlUserType50.SetIsReturnTypeStub();
				result = xamlUserType50;
				break;
			}
			case 65:
			{
				XamlUserType xamlUserType51 = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType51.Activator = new Activator(this.Activate_65_UpdatePage);
				xamlUserType51.SetIsLocalType();
				result = xamlUserType51;
				break;
			}
			}
			return result;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00019816 File Offset: 0x00017A16
		private object get_0_Control_Accent(object instance)
		{
			return ((Control)instance).Accent;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00019823 File Offset: 0x00017A23
		private void set_0_Control_Accent(object instance, object Value)
		{
			((Control)instance).Accent = (SolidColorBrush)Value;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00019836 File Offset: 0x00017A36
		private object get_1_AddRating_AppId(object instance)
		{
			return ((AddRating)instance).AppId;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00019843 File Offset: 0x00017A43
		private void set_1_AddRating_AppId(object instance, object Value)
		{
			((AddRating)instance).AppId = (string)Value;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00019856 File Offset: 0x00017A56
		private object get_2_AddRating_LastRatings(object instance)
		{
			return ((AddRating)instance).LastRatings;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00019863 File Offset: 0x00017A63
		private void set_2_AddRating_LastRatings(object instance, object Value)
		{
			((AddRating)instance).LastRatings = (ReviewStats)Value;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00019876 File Offset: 0x00017A76
		private object get_3_AllAppsPage_AllCollections(object instance)
		{
			return ((AllAppsPage)instance).AllCollections;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00019883 File Offset: 0x00017A83
		private void set_3_AllAppsPage_AllCollections(object instance, object Value)
		{
			((AllAppsPage)instance).AllCollections = (ObservableCollection<AppCollection>)Value;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00019896 File Offset: 0x00017A96
		private object get_4_AppCollection_Name(object instance)
		{
			return ((AppCollection)instance).Name;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000198A3 File Offset: 0x00017AA3
		private void set_4_AppCollection_Name(object instance, object Value)
		{
			((AppCollection)instance).Name = (string)Value;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000198B6 File Offset: 0x00017AB6
		private object get_5_AppCollection_DisplayName(object instance)
		{
			return ((AppCollection)instance).DisplayName;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000198C3 File Offset: 0x00017AC3
		private void set_5_AppCollection_DisplayName(object instance, object Value)
		{
			((AppCollection)instance).DisplayName = (string)Value;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000198D6 File Offset: 0x00017AD6
		private object get_6_AppCollection_AppIcons(object instance)
		{
			return ((AppCollection)instance).AppIcons;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000198E3 File Offset: 0x00017AE3
		private void set_6_AppCollection_AppIcons(object instance, object Value)
		{
			((AppCollection)instance).AppIcons = (List<string>)Value;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000198F6 File Offset: 0x00017AF6
		private object get_7_AppCollection_AppCount(object instance)
		{
			return ((AppCollection)instance).AppCount;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00019903 File Offset: 0x00017B03
		private void set_7_AppCollection_AppCount(object instance, object Value)
		{
			((AppCollection)instance).AppCount = (string)Value;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00019916 File Offset: 0x00017B16
		private object get_8_AppDetailsPage_FeaturedReviews(object instance)
		{
			return ((AppDetailsPage)instance).FeaturedReviews;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00019923 File Offset: 0x00017B23
		private object get_9_Review_IsExpanded(object instance)
		{
			return ((Review)instance).IsExpanded;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00019935 File Offset: 0x00017B35
		private void set_9_Review_IsExpanded(object instance, object Value)
		{
			((Review)instance).IsExpanded = (bool)Value;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00019948 File Offset: 0x00017B48
		private object get_10_Review_Id(object instance)
		{
			return ((Review)instance).Id;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001995A File Offset: 0x00017B5A
		private void set_10_Review_Id(object instance, object Value)
		{
			((Review)instance).Id = (int)Value;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001996D File Offset: 0x00017B6D
		private object get_11_Review_AppId(object instance)
		{
			return ((Review)instance).AppId;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001997A File Offset: 0x00017B7A
		private void set_11_Review_AppId(object instance, object Value)
		{
			((Review)instance).AppId = (string)Value;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001998D File Offset: 0x00017B8D
		private object get_12_Review_UserId(object instance)
		{
			return ((Review)instance).UserId;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001999A File Offset: 0x00017B9A
		private void set_12_Review_UserId(object instance, object Value)
		{
			((Review)instance).UserId = (string)Value;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000199AD File Offset: 0x00017BAD
		private object get_13_Review_Username(object instance)
		{
			return ((Review)instance).Username;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000199BA File Offset: 0x00017BBA
		private void set_13_Review_Username(object instance, object Value)
		{
			((Review)instance).Username = (string)Value;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000199CD File Offset: 0x00017BCD
		private object get_14_Review_Rating(object instance)
		{
			return ((Review)instance).Rating;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000199DF File Offset: 0x00017BDF
		private void set_14_Review_Rating(object instance, object Value)
		{
			((Review)instance).Rating = (int)Value;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000199F2 File Offset: 0x00017BF2
		private object get_15_Review_Comment(object instance)
		{
			return ((Review)instance).Comment;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000199FF File Offset: 0x00017BFF
		private void set_15_Review_Comment(object instance, object Value)
		{
			((Review)instance).Comment = (string)Value;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00019A12 File Offset: 0x00017C12
		private object get_16_Review_CreatedAt(object instance)
		{
			return ((Review)instance).CreatedAt;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00019A24 File Offset: 0x00017C24
		private void set_16_Review_CreatedAt(object instance, object Value)
		{
			((Review)instance).CreatedAt = (DateTime)Value;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00019A37 File Offset: 0x00017C37
		private object get_17_Review_IsOwnReview(object instance)
		{
			return ((Review)instance).IsOwnReview;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00019A49 File Offset: 0x00017C49
		private void set_17_Review_IsOwnReview(object instance, object Value)
		{
			((Review)instance).IsOwnReview = (bool)Value;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00019A5C File Offset: 0x00017C5C
		private object get_18_Review_ShowShortComment(object instance)
		{
			return ((Review)instance).ShowShortComment;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00019A6E File Offset: 0x00017C6E
		private object get_19_Review_ShowReadMore(object instance)
		{
			return ((Review)instance).ShowReadMore;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00019A80 File Offset: 0x00017C80
		private object get_20_Review_CommentPreview(object instance)
		{
			return ((Review)instance).CommentPreview;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00019A8D File Offset: 0x00017C8D
		private object get_21_Review_StarBg(object instance)
		{
			return ((Review)instance).StarBg;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00019A9A File Offset: 0x00017C9A
		private object get_22_Review_StarList(object instance)
		{
			return ((Review)instance).StarList;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00019AA7 File Offset: 0x00017CA7
		private object get_23_Review_DateDisplay(object instance)
		{
			return ((Review)instance).DateDisplay;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00019AB4 File Offset: 0x00017CB4
		private object get_24_AppDetailsPage_NavigationHelper(object instance)
		{
			return ((AppDetailsPage)instance).NavigationHelper;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00019AC1 File Offset: 0x00017CC1
		private object get_25_AppDetailsPage_DefaultViewModel(object instance)
		{
			return ((AppDetailsPage)instance).DefaultViewModel;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00019ACE File Offset: 0x00017CCE
		private object get_26_AppReviewsPage_IsUserLoggedIn(object instance)
		{
			return ((AppReviewsPage)instance).IsUserLoggedIn;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00019AE0 File Offset: 0x00017CE0
		private void set_26_AppReviewsPage_IsUserLoggedIn(object instance, object Value)
		{
			((AppReviewsPage)instance).IsUserLoggedIn = (bool)Value;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00019AF3 File Offset: 0x00017CF3
		private object get_27_AppReviewsPage_NavigationHelper(object instance)
		{
			return ((AppReviewsPage)instance).NavigationHelper;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00019B00 File Offset: 0x00017D00
		private object get_28_AppReviewsPage_DefaultViewModel(object instance)
		{
			return ((AppReviewsPage)instance).DefaultViewModel;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00019B0D File Offset: 0x00017D0D
		private object get_29_CollectionsViewPage_AllCollections(object instance)
		{
			return ((CollectionsViewPage)instance).AllCollections;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00019B1A File Offset: 0x00017D1A
		private void set_29_CollectionsViewPage_AllCollections(object instance, object Value)
		{
			((CollectionsViewPage)instance).AllCollections = (ObservableCollection<AppCollection>)Value;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00019B2D File Offset: 0x00017D2D
		private object get_30_FatalErrorScreen_DefaultViewModel(object instance)
		{
			return ((FatalErrorScreen)instance).DefaultViewModel;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00019B3A File Offset: 0x00017D3A
		private object get_31_FatalErrorScreen_NavigationHelper(object instance)
		{
			return ((FatalErrorScreen)instance).NavigationHelper;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00019B47 File Offset: 0x00017D47
		private object get_32_MainPage_RandomApps(object instance)
		{
			return ((MainPage)instance).RandomApps;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00019B54 File Offset: 0x00017D54
		private void set_32_MainPage_RandomApps(object instance, object Value)
		{
			((MainPage)instance).RandomApps = (ObservableCollection<StoreApp>)Value;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00019B67 File Offset: 0x00017D67
		private object get_33_StoreApp_Id(object instance)
		{
			return ((StoreApp)instance).Id;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00019B74 File Offset: 0x00017D74
		private void set_33_StoreApp_Id(object instance, object Value)
		{
			((StoreApp)instance).Id = (string)Value;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00019B87 File Offset: 0x00017D87
		private object get_34_StoreApp_AuthorId(object instance)
		{
			return ((StoreApp)instance).AuthorId;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00019B94 File Offset: 0x00017D94
		private void set_34_StoreApp_AuthorId(object instance, object Value)
		{
			((StoreApp)instance).AuthorId = (string)Value;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00019BA7 File Offset: 0x00017DA7
		private object get_35_StoreApp_Name(object instance)
		{
			return ((StoreApp)instance).Name;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00019BB4 File Offset: 0x00017DB4
		private void set_35_StoreApp_Name(object instance, object Value)
		{
			((StoreApp)instance).Name = (string)Value;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00019BC7 File Offset: 0x00017DC7
		private object get_36_StoreApp_Publisher(object instance)
		{
			return ((StoreApp)instance).Publisher;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00019BD4 File Offset: 0x00017DD4
		private void set_36_StoreApp_Publisher(object instance, object Value)
		{
			((StoreApp)instance).Publisher = (string)Value;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00019BE7 File Offset: 0x00017DE7
		private object get_37_StoreApp_Version(object instance)
		{
			return ((StoreApp)instance).Version;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00019BF4 File Offset: 0x00017DF4
		private void set_37_StoreApp_Version(object instance, object Value)
		{
			((StoreApp)instance).Version = (string)Value;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00019C07 File Offset: 0x00017E07
		private object get_38_StoreApp_DownloadUrl(object instance)
		{
			return ((StoreApp)instance).DownloadUrl;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00019C14 File Offset: 0x00017E14
		private void set_38_StoreApp_DownloadUrl(object instance, object Value)
		{
			((StoreApp)instance).DownloadUrl = (string)Value;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00019C27 File Offset: 0x00017E27
		private object get_39_StoreApp_IconUrl(object instance)
		{
			return ((StoreApp)instance).IconUrl;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00019C34 File Offset: 0x00017E34
		private void set_39_StoreApp_IconUrl(object instance, object Value)
		{
			((StoreApp)instance).IconUrl = (string)Value;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00019C47 File Offset: 0x00017E47
		private object get_40_StoreApp_Description(object instance)
		{
			return ((StoreApp)instance).Description;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00019C54 File Offset: 0x00017E54
		private void set_40_StoreApp_Description(object instance, object Value)
		{
			((StoreApp)instance).Description = (string)Value;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00019C67 File Offset: 0x00017E67
		private object get_41_StoreApp_Featured(object instance)
		{
			return ((StoreApp)instance).Featured;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00019C79 File Offset: 0x00017E79
		private void set_41_StoreApp_Featured(object instance, object Value)
		{
			((StoreApp)instance).Featured = (bool)Value;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00019C8C File Offset: 0x00017E8C
		private object get_42_StoreApp_Type(object instance)
		{
			return ((StoreApp)instance).Type;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00019C99 File Offset: 0x00017E99
		private void set_42_StoreApp_Type(object instance, object Value)
		{
			((StoreApp)instance).Type = (string)Value;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00019CAC File Offset: 0x00017EAC
		private object get_43_StoreApp_Category(object instance)
		{
			return ((StoreApp)instance).Category;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00019CB9 File Offset: 0x00017EB9
		private void set_43_StoreApp_Category(object instance, object Value)
		{
			((StoreApp)instance).Category = (string)Value;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00019CCC File Offset: 0x00017ECC
		private object get_44_StoreApp_AppType(object instance)
		{
			return ((StoreApp)instance).AppType;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00019CD9 File Offset: 0x00017ED9
		private void set_44_StoreApp_AppType(object instance, object Value)
		{
			((StoreApp)instance).AppType = (string)Value;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00019CEC File Offset: 0x00017EEC
		private object get_45_StoreApp_PackageFileName(object instance)
		{
			return ((StoreApp)instance).PackageFileName;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00019CF9 File Offset: 0x00017EF9
		private void set_45_StoreApp_PackageFileName(object instance, object Value)
		{
			((StoreApp)instance).PackageFileName = (string)Value;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00019D0C File Offset: 0x00017F0C
		private object get_46_StoreApp_ScreenshotUrls(object instance)
		{
			return ((StoreApp)instance).ScreenshotUrls;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00019D19 File Offset: 0x00017F19
		private void set_46_StoreApp_ScreenshotUrls(object instance, object Value)
		{
			((StoreApp)instance).ScreenshotUrls = (List<string>)Value;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00019D2C File Offset: 0x00017F2C
		private object get_47_StoreApp_Status(object instance)
		{
			return ((StoreApp)instance).Status;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00019D39 File Offset: 0x00017F39
		private void set_47_StoreApp_Status(object instance, object Value)
		{
			((StoreApp)instance).Status = (string)Value;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00019D4C File Offset: 0x00017F4C
		private object get_48_StoreApp_TargetOS(object instance)
		{
			return ((StoreApp)instance).TargetOS;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00019D5E File Offset: 0x00017F5E
		private void set_48_StoreApp_TargetOS(object instance, object Value)
		{
			((StoreApp)instance).TargetOS = (int)Value;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00019D71 File Offset: 0x00017F71
		private object get_49_StoreApp_Rating(object instance)
		{
			return ((StoreApp)instance).Rating;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00019D83 File Offset: 0x00017F83
		private void set_49_StoreApp_Rating(object instance, object Value)
		{
			((StoreApp)instance).Rating = (double)Value;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00019D96 File Offset: 0x00017F96
		private object get_50_StoreApp_RatingCount(object instance)
		{
			return ((StoreApp)instance).RatingCount;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00019DA8 File Offset: 0x00017FA8
		private void set_50_StoreApp_RatingCount(object instance, object Value)
		{
			((StoreApp)instance).RatingCount = (int)Value;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00019DBB File Offset: 0x00017FBB
		private object get_51_StoreApp_ReviewCount(object instance)
		{
			return ((StoreApp)instance).ReviewCount;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00019DCD File Offset: 0x00017FCD
		private void set_51_StoreApp_ReviewCount(object instance, object Value)
		{
			((StoreApp)instance).ReviewCount = (int)Value;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00019DE0 File Offset: 0x00017FE0
		private object get_52_StoreApp_PendingUpdate(object instance)
		{
			return ((StoreApp)instance).PendingUpdate;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00019DF2 File Offset: 0x00017FF2
		private void set_52_StoreApp_PendingUpdate(object instance, object Value)
		{
			((StoreApp)instance).PendingUpdate = (bool)Value;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00019E05 File Offset: 0x00018005
		private object get_53_StoreApp_Update(object instance)
		{
			return ((StoreApp)instance).Update;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00019E12 File Offset: 0x00018012
		private void set_53_StoreApp_Update(object instance, object Value)
		{
			((StoreApp)instance).Update = Value;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00019E20 File Offset: 0x00018020
		private object get_54_StoreApp_Ratings(object instance)
		{
			return ((StoreApp)instance).Ratings;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00019E2D File Offset: 0x0001802D
		private void set_54_StoreApp_Ratings(object instance, object Value)
		{
			((StoreApp)instance).Ratings = (RatingSummary)Value;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00019E40 File Offset: 0x00018040
		private object get_55_StoreApp_ApproximateSizeText(object instance)
		{
			return ((StoreApp)instance).ApproximateSizeText;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00019E4D File Offset: 0x0001804D
		private void set_55_StoreApp_ApproximateSizeText(object instance, object Value)
		{
			((StoreApp)instance).ApproximateSizeText = (string)Value;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00019E60 File Offset: 0x00018060
		private object get_56_StoreApp_AppSummaryText(object instance)
		{
			return ((StoreApp)instance).AppSummaryText;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00019E6D File Offset: 0x0001806D
		private object get_57_StoreApp_StarBg(object instance)
		{
			return ((StoreApp)instance).StarBg;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00019E7A File Offset: 0x0001807A
		private object get_58_StoreApp_StarList(object instance)
		{
			return ((StoreApp)instance).StarList;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00019E87 File Offset: 0x00018087
		private object get_59_StoreApp_Screenshot1(object instance)
		{
			return ((StoreApp)instance).Screenshot1;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00019E94 File Offset: 0x00018094
		private void set_59_StoreApp_Screenshot1(object instance, object Value)
		{
			((StoreApp)instance).Screenshot1 = (string)Value;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00019EA7 File Offset: 0x000180A7
		private object get_60_StoreApp_Screenshot2(object instance)
		{
			return ((StoreApp)instance).Screenshot2;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00019EB4 File Offset: 0x000180B4
		private void set_60_StoreApp_Screenshot2(object instance, object Value)
		{
			((StoreApp)instance).Screenshot2 = (string)Value;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00019EC7 File Offset: 0x000180C7
		private object get_61_StoreApp_Screenshot3(object instance)
		{
			return ((StoreApp)instance).Screenshot3;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00019ED4 File Offset: 0x000180D4
		private void set_61_StoreApp_Screenshot3(object instance, object Value)
		{
			((StoreApp)instance).Screenshot3 = (string)Value;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00019EE7 File Offset: 0x000180E7
		private object get_62_StoreApp_ReviewStats(object instance)
		{
			return ((StoreApp)instance).ReviewStats;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00019EF4 File Offset: 0x000180F4
		private void set_62_StoreApp_ReviewStats(object instance, object Value)
		{
			((StoreApp)instance).ReviewStats = (ReviewStats)Value;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00019F07 File Offset: 0x00018107
		private object get_63_StoreApp_Screenshots(object instance)
		{
			return ((StoreApp)instance).Screenshots;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00019F14 File Offset: 0x00018114
		private object get_64_StoreApp_RelatedApps(object instance)
		{
			return ((StoreApp)instance).RelatedApps;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00019F21 File Offset: 0x00018121
		private object get_65_StoreApp_SpotlightBanner(object instance)
		{
			return ((StoreApp)instance).SpotlightBanner;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00019F2E File Offset: 0x0001812E
		private void set_65_StoreApp_SpotlightBanner(object instance, object Value)
		{
			((StoreApp)instance).SpotlightBanner = (string)Value;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00019F41 File Offset: 0x00018141
		private object get_66_MainPage_TotalAppsCount(object instance)
		{
			return ((MainPage)instance).TotalAppsCount;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00019F4E File Offset: 0x0001814E
		private void set_66_MainPage_TotalAppsCount(object instance, object Value)
		{
			((MainPage)instance).TotalAppsCount = (string)Value;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00019F61 File Offset: 0x00018161
		private object get_67_MainPage_UniquePublishersCount(object instance)
		{
			return ((MainPage)instance).UniquePublishersCount;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00019F6E File Offset: 0x0001816E
		private void set_67_MainPage_UniquePublishersCount(object instance, object Value)
		{
			((MainPage)instance).UniquePublishersCount = (string)Value;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00019F84 File Offset: 0x00018184
		private object get_68_Color_A(object instance)
		{
			return ((Color)instance).A;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00019FA4 File Offset: 0x000181A4
		private void set_68_Color_A(object instance, object Value)
		{
			((Color)instance).A = (byte)Value;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00019FC8 File Offset: 0x000181C8
		private object get_69_Color_B(object instance)
		{
			return ((Color)instance).B;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00019FE8 File Offset: 0x000181E8
		private void set_69_Color_B(object instance, object Value)
		{
			((Color)instance).B = (byte)Value;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001A00C File Offset: 0x0001820C
		private object get_70_Color_G(object instance)
		{
			return ((Color)instance).G;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001A02C File Offset: 0x0001822C
		private void set_70_Color_G(object instance, object Value)
		{
			((Color)instance).G = (byte)Value;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001A050 File Offset: 0x00018250
		private object get_71_Color_R(object instance)
		{
			return ((Color)instance).R;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001A070 File Offset: 0x00018270
		private void set_71_Color_R(object instance, object Value)
		{
			((Color)instance).R = (byte)Value;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001A094 File Offset: 0x00018294
		private IXamlMember CreateXamlMember(string longMemberName)
		{
			XamlMember xamlMember = null;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(longMemberName);
			if (num <= 2464009339U)
			{
				if (num <= 1432875812U)
				{
					if (num <= 401409987U)
					{
						if (num <= 183300940U)
						{
							if (num <= 146079971U)
							{
								if (num != 132968083U)
								{
									if (num == 146079971U)
									{
										if (longMemberName == "Win81StoreRevival.StoreApp.Screenshots")
										{
											XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
											xamlMember = new XamlMember(this, "Screenshots", "System.Collections.Generic.List`1<String>");
											xamlMember.Getter = new Getter(this.get_63_StoreApp_Screenshots);
											xamlMember.SetIsReadOnly();
										}
									}
								}
								else if (longMemberName == "Windows.UI.Color.B")
								{
									XamlUserType xamlUserType2 = (XamlUserType)this.GetXamlTypeByName("Windows.UI.Color");
									xamlMember = new XamlMember(this, "B", "Byte");
									xamlMember.Getter = new Getter(this.get_69_Color_B);
									xamlMember.Setter = new Setter(this.set_69_Color_B);
								}
							}
							else if (num != 149745702U)
							{
								if (num == 183300940U)
								{
									if (longMemberName == "Windows.UI.Color.G")
									{
										XamlUserType xamlUserType3 = (XamlUserType)this.GetXamlTypeByName("Windows.UI.Color");
										xamlMember = new XamlMember(this, "G", "Byte");
										xamlMember.Getter = new Getter(this.get_70_Color_G);
										xamlMember.Setter = new Setter(this.set_70_Color_G);
									}
								}
							}
							else if (longMemberName == "Windows.UI.Color.A")
							{
								XamlUserType xamlUserType4 = (XamlUserType)this.GetXamlTypeByName("Windows.UI.Color");
								xamlMember = new XamlMember(this, "A", "Byte");
								xamlMember.Getter = new Getter(this.get_68_Color_A);
								xamlMember.Setter = new Setter(this.set_68_Color_A);
							}
						}
						else if (num <= 272670822U)
						{
							if (num != 238632656U)
							{
								if (num == 272670822U)
								{
									if (longMemberName == "Win81StoreRevival.Review.IsOwnReview")
									{
										XamlUserType xamlUserType5 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
										xamlMember = new XamlMember(this, "IsOwnReview", "Boolean");
										xamlMember.Getter = new Getter(this.get_17_Review_IsOwnReview);
										xamlMember.Setter = new Setter(this.set_17_Review_IsOwnReview);
									}
								}
							}
							else if (longMemberName == "Win81StoreRevival.StoreApp.SpotlightBanner")
							{
								XamlUserType xamlUserType6 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "SpotlightBanner", "String");
								xamlMember.Getter = new Getter(this.get_65_StoreApp_SpotlightBanner);
								xamlMember.Setter = new Setter(this.set_65_StoreApp_SpotlightBanner);
							}
						}
						else if (num != 327335453U)
						{
							if (num != 394531623U)
							{
								if (num == 401409987U)
								{
									if (longMemberName == "Windows.UI.Color.R")
									{
										XamlUserType xamlUserType7 = (XamlUserType)this.GetXamlTypeByName("Windows.UI.Color");
										xamlMember = new XamlMember(this, "R", "Byte");
										xamlMember.Getter = new Getter(this.get_71_Color_R);
										xamlMember.Setter = new Setter(this.set_71_Color_R);
									}
								}
							}
							else if (longMemberName == "Win81StoreRevival.StoreApp.Rating")
							{
								XamlUserType xamlUserType8 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Rating", "Double");
								xamlMember.Getter = new Getter(this.get_49_StoreApp_Rating);
								xamlMember.Setter = new Setter(this.set_49_StoreApp_Rating);
							}
						}
						else if (longMemberName == "Win81StoreRevival.AppCollection.AppCount")
						{
							XamlUserType xamlUserType9 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppCollection");
							xamlMember = new XamlMember(this, "AppCount", "String");
							xamlMember.Getter = new Getter(this.get_7_AppCollection_AppCount);
							xamlMember.Setter = new Setter(this.set_7_AppCollection_AppCount);
						}
					}
					else if (num <= 737556564U)
					{
						if (num <= 644252747U)
						{
							if (num != 507016486U)
							{
								if (num == 644252747U)
								{
									if (longMemberName == "Win81StoreRevival.Review.Rating")
									{
										XamlUserType xamlUserType10 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
										xamlMember = new XamlMember(this, "Rating", "Int32");
										xamlMember.Getter = new Getter(this.get_14_Review_Rating);
										xamlMember.Setter = new Setter(this.set_14_Review_Rating);
									}
								}
							}
							else if (longMemberName == "Win81StoreRevival.Review.StarList")
							{
								XamlUserType xamlUserType11 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
								xamlMember = new XamlMember(this, "StarList", "System.Collections.Generic.List`1<Double>");
								xamlMember.Getter = new Getter(this.get_22_Review_StarList);
								xamlMember.SetIsReadOnly();
							}
						}
						else if (num != 674657043U)
						{
							if (num == 737556564U)
							{
								if (longMemberName == "Win81StoreRevival.FatalErrorScreen.NavigationHelper")
								{
									XamlUserType xamlUserType12 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.FatalErrorScreen");
									xamlMember = new XamlMember(this, "NavigationHelper", "Win81StoreRevival.Common.NavigationHelper");
									xamlMember.Getter = new Getter(this.get_31_FatalErrorScreen_NavigationHelper);
									xamlMember.SetIsReadOnly();
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.AppDetailsPage.DefaultViewModel")
						{
							XamlUserType xamlUserType13 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppDetailsPage");
							xamlMember = new XamlMember(this, "DefaultViewModel", "Win81StoreRevival.Common.ObservableDictionary");
							xamlMember.Getter = new Getter(this.get_25_AppDetailsPage_DefaultViewModel);
							xamlMember.SetIsReadOnly();
						}
					}
					else if (num <= 1025140903U)
					{
						if (num != 982078357U)
						{
							if (num == 1025140903U)
							{
								if (longMemberName == "Win81StoreRevival.MainPage.RandomApps")
								{
									XamlUserType xamlUserType14 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.MainPage");
									xamlMember = new XamlMember(this, "RandomApps", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.StoreApp>");
									xamlMember.Getter = new Getter(this.get_32_MainPage_RandomApps);
									xamlMember.Setter = new Setter(this.set_32_MainPage_RandomApps);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.StoreApp.AppType")
						{
							XamlUserType xamlUserType15 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "AppType", "String");
							xamlMember.Getter = new Getter(this.get_44_StoreApp_AppType);
							xamlMember.Setter = new Setter(this.set_44_StoreApp_AppType);
						}
					}
					else if (num != 1332824992U)
					{
						if (num != 1377840696U)
						{
							if (num == 1432875812U)
							{
								if (longMemberName == "Win81StoreRevival.StoreApp.Featured")
								{
									XamlUserType xamlUserType16 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
									xamlMember = new XamlMember(this, "Featured", "Boolean");
									xamlMember.Getter = new Getter(this.get_41_StoreApp_Featured);
									xamlMember.Setter = new Setter(this.set_41_StoreApp_Featured);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.AppCollection.AppIcons")
						{
							XamlUserType xamlUserType17 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppCollection");
							xamlMember = new XamlMember(this, "AppIcons", "System.Collections.Generic.List`1<String>");
							xamlMember.Getter = new Getter(this.get_6_AppCollection_AppIcons);
							xamlMember.Setter = new Setter(this.set_6_AppCollection_AppIcons);
						}
					}
					else if (longMemberName == "Win81StoreRevival.Review.UserId")
					{
						XamlUserType xamlUserType18 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
						xamlMember = new XamlMember(this, "UserId", "String");
						xamlMember.Getter = new Getter(this.get_12_Review_UserId);
						xamlMember.Setter = new Setter(this.set_12_Review_UserId);
					}
				}
				else if (num <= 2096118952U)
				{
					if (num <= 1814434223U)
					{
						if (num <= 1581248573U)
						{
							if (num != 1491758396U)
							{
								if (num == 1581248573U)
								{
									if (longMemberName == "Win81StoreRevival.AppReviewsPage.IsUserLoggedIn")
									{
										XamlUserType xamlUserType19 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppReviewsPage");
										xamlMember = new XamlMember(this, "IsUserLoggedIn", "Boolean");
										xamlMember.Getter = new Getter(this.get_26_AppReviewsPage_IsUserLoggedIn);
										xamlMember.Setter = new Setter(this.set_26_AppReviewsPage_IsUserLoggedIn);
									}
								}
							}
							else if (longMemberName == "Win81StoreRevival.StoreApp.Ratings")
							{
								XamlUserType xamlUserType20 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Ratings", "Win81StoreRevival.RatingSummary");
								xamlMember.Getter = new Getter(this.get_54_StoreApp_Ratings);
								xamlMember.Setter = new Setter(this.set_54_StoreApp_Ratings);
							}
						}
						else if (num != 1736797010U)
						{
							if (num == 1814434223U)
							{
								if (longMemberName == "Win81StoreRevival.StoreApp.DownloadUrl")
								{
									XamlUserType xamlUserType21 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
									xamlMember = new XamlMember(this, "DownloadUrl", "String");
									xamlMember.Getter = new Getter(this.get_38_StoreApp_DownloadUrl);
									xamlMember.Setter = new Setter(this.set_38_StoreApp_DownloadUrl);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.AllAppsPage.AllCollections")
						{
							XamlUserType xamlUserType22 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AllAppsPage");
							xamlMember = new XamlMember(this, "AllCollections", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.AppCollection>");
							xamlMember.Getter = new Getter(this.get_3_AllAppsPage_AllCollections);
							xamlMember.Setter = new Setter(this.set_3_AllAppsPage_AllCollections);
						}
					}
					else if (num <= 1945659464U)
					{
						if (num != 1892944984U)
						{
							if (num == 1945659464U)
							{
								if (longMemberName == "Win81StoreRevival.StoreApp.Description")
								{
									XamlUserType xamlUserType23 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
									xamlMember = new XamlMember(this, "Description", "String");
									xamlMember.Getter = new Getter(this.get_40_StoreApp_Description);
									xamlMember.Setter = new Setter(this.set_40_StoreApp_Description);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.StoreApp.PendingUpdate")
						{
							XamlUserType xamlUserType24 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "PendingUpdate", "Boolean");
							xamlMember.Getter = new Getter(this.get_52_StoreApp_PendingUpdate);
							xamlMember.Setter = new Setter(this.set_52_StoreApp_PendingUpdate);
						}
					}
					else if (num != 2048228270U)
					{
						if (num != 2054892703U)
						{
							if (num == 2096118952U)
							{
								if (longMemberName == "Win81StoreRevival.StoreApp.ScreenshotUrls")
								{
									XamlUserType xamlUserType25 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
									xamlMember = new XamlMember(this, "ScreenshotUrls", "System.Collections.Generic.List`1<String>");
									xamlMember.Getter = new Getter(this.get_46_StoreApp_ScreenshotUrls);
									xamlMember.Setter = new Setter(this.set_46_StoreApp_ScreenshotUrls);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.StoreApp.Update")
						{
							XamlUserType xamlUserType26 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "Update", "Object");
							xamlMember.Getter = new Getter(this.get_53_StoreApp_Update);
							xamlMember.Setter = new Setter(this.set_53_StoreApp_Update);
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Category")
					{
						XamlUserType xamlUserType27 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Category", "String");
						xamlMember.Getter = new Getter(this.get_43_StoreApp_Category);
						xamlMember.Setter = new Setter(this.set_43_StoreApp_Category);
					}
				}
				else if (num <= 2290410522U)
				{
					if (num <= 2207069585U)
					{
						if (num != 2190370494U)
						{
							if (num == 2207069585U)
							{
								if (longMemberName == "Win81StoreRevival.MainPage.TotalAppsCount")
								{
									XamlUserType xamlUserType28 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.MainPage");
									xamlMember = new XamlMember(this, "TotalAppsCount", "String");
									xamlMember.Getter = new Getter(this.get_66_MainPage_TotalAppsCount);
									xamlMember.Setter = new Setter(this.set_66_MainPage_TotalAppsCount);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.StoreApp.RatingCount")
						{
							XamlUserType xamlUserType29 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "RatingCount", "Int32");
							xamlMember.Getter = new Getter(this.get_50_StoreApp_RatingCount);
							xamlMember.Setter = new Setter(this.set_50_StoreApp_RatingCount);
						}
					}
					else if (num != 2290114797U)
					{
						if (num == 2290410522U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.Version")
							{
								XamlUserType xamlUserType30 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Version", "String");
								xamlMember.Getter = new Getter(this.get_37_StoreApp_Version);
								xamlMember.Setter = new Setter(this.set_37_StoreApp_Version);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.Review.StarBg")
					{
						XamlUserType xamlUserType31 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
						xamlMember = new XamlMember(this, "StarBg", "System.Collections.Generic.List`1<Int32>");
						xamlMember.Getter = new Getter(this.get_21_Review_StarBg);
						xamlMember.SetIsReadOnly();
					}
				}
				else if (num <= 2360826118U)
				{
					if (num != 2296821301U)
					{
						if (num == 2360826118U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.ApproximateSizeText")
							{
								XamlUserType xamlUserType32 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "ApproximateSizeText", "String");
								xamlMember.Getter = new Getter(this.get_55_StoreApp_ApproximateSizeText);
								xamlMember.Setter = new Setter(this.set_55_StoreApp_ApproximateSizeText);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Name")
					{
						XamlUserType xamlUserType33 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Name", "String");
						xamlMember.Getter = new Getter(this.get_35_StoreApp_Name);
						xamlMember.Setter = new Setter(this.set_35_StoreApp_Name);
					}
				}
				else if (num != 2374353683U)
				{
					if (num != 2437633725U)
					{
						if (num == 2464009339U)
						{
							if (longMemberName == "Win81StoreRevival.FatalErrorScreen.DefaultViewModel")
							{
								XamlUserType xamlUserType34 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.FatalErrorScreen");
								xamlMember = new XamlMember(this, "DefaultViewModel", "Win81StoreRevival.Common.ObservableDictionary");
								xamlMember.Getter = new Getter(this.get_30_FatalErrorScreen_DefaultViewModel);
								xamlMember.SetIsReadOnly();
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.AppReviewsPage.NavigationHelper")
					{
						XamlUserType xamlUserType35 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppReviewsPage");
						xamlMember = new XamlMember(this, "NavigationHelper", "Win81StoreRevival.Common.NavigationHelper");
						xamlMember.Getter = new Getter(this.get_27_AppReviewsPage_NavigationHelper);
						xamlMember.SetIsReadOnly();
					}
				}
				else if (longMemberName == "Win81StoreRevival.StoreApp.PackageFileName")
				{
					XamlUserType xamlUserType36 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
					xamlMember = new XamlMember(this, "PackageFileName", "String");
					xamlMember.Getter = new Getter(this.get_45_StoreApp_PackageFileName);
					xamlMember.Setter = new Setter(this.set_45_StoreApp_PackageFileName);
				}
			}
			else if (num <= 3400834889U)
			{
				if (num <= 3045844300U)
				{
					if (num <= 2747838537U)
					{
						if (num <= 2551942492U)
						{
							if (num != 2510026580U)
							{
								if (num == 2551942492U)
								{
									if (longMemberName == "Win81StoreRevival.StoreApp.AppSummaryText")
									{
										XamlUserType xamlUserType37 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
										xamlMember = new XamlMember(this, "AppSummaryText", "String");
										xamlMember.Getter = new Getter(this.get_56_StoreApp_AppSummaryText);
										xamlMember.SetIsReadOnly();
									}
								}
							}
							else if (longMemberName == "Win81StoreRevival.StoreApp.IconUrl")
							{
								XamlUserType xamlUserType38 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "IconUrl", "String");
								xamlMember.Getter = new Getter(this.get_39_StoreApp_IconUrl);
								xamlMember.Setter = new Setter(this.set_39_StoreApp_IconUrl);
							}
						}
						else if (num != 2604593215U)
						{
							if (num == 2747838537U)
							{
								if (longMemberName == "Win81StoreRevival.Review.CommentPreview")
								{
									XamlUserType xamlUserType39 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
									xamlMember = new XamlMember(this, "CommentPreview", "String");
									xamlMember.Getter = new Getter(this.get_20_Review_CommentPreview);
									xamlMember.SetIsReadOnly();
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.CollectionsViewPage.AllCollections")
						{
							XamlUserType xamlUserType40 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.CollectionsViewPage");
							xamlMember = new XamlMember(this, "AllCollections", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.AppCollection>");
							xamlMember.Getter = new Getter(this.get_29_CollectionsViewPage_AllCollections);
							xamlMember.Setter = new Setter(this.set_29_CollectionsViewPage_AllCollections);
						}
					}
					else if (num <= 2821660980U)
					{
						if (num != 2820051712U)
						{
							if (num == 2821660980U)
							{
								if (longMemberName == "Win81StoreRevival.Review.DateDisplay")
								{
									XamlUserType xamlUserType41 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
									xamlMember = new XamlMember(this, "DateDisplay", "String");
									xamlMember.Getter = new Getter(this.get_23_Review_DateDisplay);
									xamlMember.SetIsReadOnly();
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.AppCollection.Name")
						{
							XamlUserType xamlUserType42 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppCollection");
							xamlMember = new XamlMember(this, "Name", "String");
							xamlMember.Getter = new Getter(this.get_4_AppCollection_Name);
							xamlMember.Setter = new Setter(this.set_4_AppCollection_Name);
						}
					}
					else if (num != 2857607275U)
					{
						if (num != 3003527666U)
						{
							if (num == 3045844300U)
							{
								if (longMemberName == "Win81StoreRevival.AppDetailsPage.NavigationHelper")
								{
									XamlUserType xamlUserType43 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppDetailsPage");
									xamlMember = new XamlMember(this, "NavigationHelper", "Win81StoreRevival.Common.NavigationHelper");
									xamlMember.Getter = new Getter(this.get_24_AppDetailsPage_NavigationHelper);
									xamlMember.SetIsReadOnly();
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.AppReviewsPage.DefaultViewModel")
						{
							XamlUserType xamlUserType44 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppReviewsPage");
							xamlMember = new XamlMember(this, "DefaultViewModel", "Win81StoreRevival.Common.ObservableDictionary");
							xamlMember.Getter = new Getter(this.get_28_AppReviewsPage_DefaultViewModel);
							xamlMember.SetIsReadOnly();
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Id")
					{
						XamlUserType xamlUserType45 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Id", "String");
						xamlMember.Getter = new Getter(this.get_33_StoreApp_Id);
						xamlMember.Setter = new Setter(this.set_33_StoreApp_Id);
					}
				}
				else if (num <= 3234355249U)
				{
					if (num <= 3126643250U)
					{
						if (num != 3098851790U)
						{
							if (num == 3126643250U)
							{
								if (longMemberName == "Win81StoreRevival.StoreApp.StarList")
								{
									XamlUserType xamlUserType46 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
									xamlMember = new XamlMember(this, "StarList", "System.Collections.Generic.List`1<Double>");
									xamlMember.Getter = new Getter(this.get_58_StoreApp_StarList);
									xamlMember.SetIsReadOnly();
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.Review.ShowReadMore")
						{
							XamlUserType xamlUserType47 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
							xamlMember = new XamlMember(this, "ShowReadMore", "Boolean");
							xamlMember.Getter = new Getter(this.get_19_Review_ShowReadMore);
							xamlMember.SetIsReadOnly();
						}
					}
					else if (num != 3192891237U)
					{
						if (num == 3234355249U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.RelatedApps")
							{
								XamlUserType xamlUserType48 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "RelatedApps", "System.Collections.Generic.List`1<Win81StoreRevival.StoreApp>");
								xamlMember.Getter = new Getter(this.get_64_StoreApp_RelatedApps);
								xamlMember.SetIsReadOnly();
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.ReviewStats")
					{
						XamlUserType xamlUserType49 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "ReviewStats", "Win81StoreRevival.ReviewStats");
						xamlMember.Getter = new Getter(this.get_62_StoreApp_ReviewStats);
						xamlMember.Setter = new Setter(this.set_62_StoreApp_ReviewStats);
					}
				}
				else if (num <= 3346064568U)
				{
					if (num != 3341074448U)
					{
						if (num == 3346064568U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.Status")
							{
								XamlUserType xamlUserType50 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Status", "String");
								xamlMember.Getter = new Getter(this.get_47_StoreApp_Status);
								xamlMember.Setter = new Setter(this.set_47_StoreApp_Status);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.AppCollection.DisplayName")
					{
						XamlUserType xamlUserType51 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppCollection");
						xamlMember = new XamlMember(this, "DisplayName", "String");
						xamlMember.Getter = new Getter(this.get_5_AppCollection_DisplayName);
						xamlMember.Setter = new Setter(this.set_5_AppCollection_DisplayName);
					}
				}
				else if (num != 3350502032U)
				{
					if (num != 3367279651U)
					{
						if (num == 3400834889U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.Screenshot1")
							{
								XamlUserType xamlUserType52 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Screenshot1", "String");
								xamlMember.Getter = new Getter(this.get_59_StoreApp_Screenshot1);
								xamlMember.Setter = new Setter(this.set_59_StoreApp_Screenshot1);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Screenshot3")
					{
						XamlUserType xamlUserType53 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Screenshot3", "String");
						xamlMember.Getter = new Getter(this.get_61_StoreApp_Screenshot3);
						xamlMember.Setter = new Setter(this.set_61_StoreApp_Screenshot3);
					}
				}
				else if (longMemberName == "Win81StoreRevival.StoreApp.Screenshot2")
				{
					XamlUserType xamlUserType54 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
					xamlMember = new XamlMember(this, "Screenshot2", "String");
					xamlMember.Getter = new Getter(this.get_60_StoreApp_Screenshot2);
					xamlMember.Setter = new Setter(this.set_60_StoreApp_Screenshot2);
				}
			}
			else if (num <= 3652862178U)
			{
				if (num <= 3582523791U)
				{
					if (num <= 3438655830U)
					{
						if (num != 3408668029U)
						{
							if (num == 3438655830U)
							{
								if (longMemberName == "Win81StoreRevival.StoreApp.Publisher")
								{
									XamlUserType xamlUserType55 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
									xamlMember = new XamlMember(this, "Publisher", "String");
									xamlMember.Getter = new Getter(this.get_36_StoreApp_Publisher);
									xamlMember.Setter = new Setter(this.set_36_StoreApp_Publisher);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.StoreApp.ReviewCount")
						{
							XamlUserType xamlUserType56 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "ReviewCount", "Int32");
							xamlMember.Getter = new Getter(this.get_51_StoreApp_ReviewCount);
							xamlMember.Setter = new Setter(this.set_51_StoreApp_ReviewCount);
						}
					}
					else if (num != 3571775400U)
					{
						if (num == 3582523791U)
						{
							if (longMemberName == "Win81StoreRevival.Review.Id")
							{
								XamlUserType xamlUserType57 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
								xamlMember = new XamlMember(this, "Id", "Int32");
								xamlMember.Getter = new Getter(this.get_10_Review_Id);
								xamlMember.Setter = new Setter(this.set_10_Review_Id);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.Review.ShowShortComment")
					{
						XamlUserType xamlUserType58 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
						xamlMember = new XamlMember(this, "ShowShortComment", "Boolean");
						xamlMember.Getter = new Getter(this.get_18_Review_ShowShortComment);
						xamlMember.SetIsReadOnly();
					}
				}
				else if (num <= 3634082022U)
				{
					if (num != 3629566905U)
					{
						if (num == 3634082022U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.Type")
							{
								XamlUserType xamlUserType59 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Type", "String");
								xamlMember.Getter = new Getter(this.get_42_StoreApp_Type);
								xamlMember.Setter = new Setter(this.set_42_StoreApp_Type);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.AppDetailsPage.FeaturedReviews")
					{
						XamlUserType xamlUserType60 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppDetailsPage");
						xamlMember = new XamlMember(this, "FeaturedReviews", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.Review>");
						xamlMember.Getter = new Getter(this.get_8_AppDetailsPage_FeaturedReviews);
						xamlMember.SetIsReadOnly();
					}
				}
				else if (num != 3634737249U)
				{
					if (num != 3649417571U)
					{
						if (num == 3652862178U)
						{
							if (longMemberName == "Win81StoreRevival.AddRating.LastRatings")
							{
								XamlUserType xamlUserType61 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AddRating");
								xamlMember = new XamlMember(this, "LastRatings", "Win81StoreRevival.ReviewStats");
								xamlMember.Getter = new Getter(this.get_2_AddRating_LastRatings);
								xamlMember.Setter = new Setter(this.set_2_AddRating_LastRatings);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.Themes.Control.Accent")
					{
						XamlUserType xamlUserType62 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Themes.Control");
						xamlMember = new XamlMember(this, "Accent", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.Getter = new Getter(this.get_0_Control_Accent);
						xamlMember.Setter = new Setter(this.set_0_Control_Accent);
					}
				}
				else if (longMemberName == "Win81StoreRevival.MainPage.UniquePublishersCount")
				{
					XamlUserType xamlUserType63 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.MainPage");
					xamlMember = new XamlMember(this, "UniquePublishersCount", "String");
					xamlMember.Getter = new Getter(this.get_67_MainPage_UniquePublishersCount);
					xamlMember.Setter = new Setter(this.set_67_MainPage_UniquePublishersCount);
				}
			}
			else if (num <= 3834569107U)
			{
				if (num <= 3766276305U)
				{
					if (num != 3667113670U)
					{
						if (num == 3766276305U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.TargetOS")
							{
								XamlUserType xamlUserType64 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "TargetOS", "Int32");
								xamlMember.Getter = new Getter(this.get_48_StoreApp_TargetOS);
								xamlMember.Setter = new Setter(this.set_48_StoreApp_TargetOS);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.Review.Username")
					{
						XamlUserType xamlUserType65 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
						xamlMember = new XamlMember(this, "Username", "String");
						xamlMember.Getter = new Getter(this.get_13_Review_Username);
						xamlMember.Setter = new Setter(this.set_13_Review_Username);
					}
				}
				else if (num != 3788356465U)
				{
					if (num == 3834569107U)
					{
						if (longMemberName == "Win81StoreRevival.Review.CreatedAt")
						{
							XamlUserType xamlUserType66 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
							xamlMember = new XamlMember(this, "CreatedAt", "System.DateTime");
							xamlMember.Getter = new Getter(this.get_16_Review_CreatedAt);
							xamlMember.Setter = new Setter(this.set_16_Review_CreatedAt);
						}
					}
				}
				else if (longMemberName == "Win81StoreRevival.StoreApp.StarBg")
				{
					XamlUserType xamlUserType67 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
					xamlMember = new XamlMember(this, "StarBg", "System.Collections.Generic.List`1<Int32>");
					xamlMember.Getter = new Getter(this.get_57_StoreApp_StarBg);
					xamlMember.SetIsReadOnly();
				}
			}
			else if (num <= 4066399926U)
			{
				if (num != 4008149556U)
				{
					if (num == 4066399926U)
					{
						if (longMemberName == "Win81StoreRevival.AddRating.AppId")
						{
							XamlUserType xamlUserType68 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AddRating");
							xamlMember = new XamlMember(this, "AppId", "String");
							xamlMember.Getter = new Getter(this.get_1_AddRating_AppId);
							xamlMember.Setter = new Setter(this.set_1_AddRating_AppId);
						}
					}
				}
				else if (longMemberName == "Win81StoreRevival.StoreApp.AuthorId")
				{
					XamlUserType xamlUserType69 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
					xamlMember = new XamlMember(this, "AuthorId", "String");
					xamlMember.Getter = new Getter(this.get_34_StoreApp_AuthorId);
					xamlMember.Setter = new Setter(this.set_34_StoreApp_AuthorId);
				}
			}
			else if (num != 4095299412U)
			{
				if (num != 4129974495U)
				{
					if (num == 4149419283U)
					{
						if (longMemberName == "Win81StoreRevival.Review.IsExpanded")
						{
							XamlUserType xamlUserType70 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
							xamlMember = new XamlMember(this, "IsExpanded", "Boolean");
							xamlMember.Getter = new Getter(this.get_9_Review_IsExpanded);
							xamlMember.Setter = new Setter(this.set_9_Review_IsExpanded);
						}
					}
				}
				else if (longMemberName == "Win81StoreRevival.Review.Comment")
				{
					XamlUserType xamlUserType71 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
					xamlMember = new XamlMember(this, "Comment", "String");
					xamlMember.Getter = new Getter(this.get_15_Review_Comment);
					xamlMember.Setter = new Setter(this.set_15_Review_Comment);
				}
			}
			else if (longMemberName == "Win81StoreRevival.Review.AppId")
			{
				XamlUserType xamlUserType72 = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.Review");
				xamlMember = new XamlMember(this, "AppId", "String");
				xamlMember.Getter = new Getter(this.get_11_Review_AppId);
				xamlMember.Setter = new Setter(this.set_11_Review_AppId);
			}
			return xamlMember;
		}

		// Token: 0x04000224 RID: 548
		private Dictionary<string, IXamlType> _xamlTypeCacheByName = new Dictionary<string, IXamlType>();

		// Token: 0x04000225 RID: 549
		private Dictionary<Type, IXamlType> _xamlTypeCacheByType = new Dictionary<Type, IXamlType>();

		// Token: 0x04000226 RID: 550
		private Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();

		// Token: 0x04000227 RID: 551
		private string[] _typeNameTable;

		// Token: 0x04000228 RID: 552
		private Type[] _typeTable;
	}
}

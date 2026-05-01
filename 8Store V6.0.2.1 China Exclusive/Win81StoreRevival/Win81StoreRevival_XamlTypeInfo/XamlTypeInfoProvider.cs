using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Callisto.Callisto_XamlTypeInfo;
using Win81StoreRevival.Common;
using Win81StoreRevival.Flyouts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Win81StoreRevival_XamlTypeInfo
{
	// Token: 0x02000045 RID: 69
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlTypeInfoProvider
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00017790 File Offset: 0x00015990
		public IXamlType GetXamlTypeByType(Type type)
		{
			IXamlType xamlType;
			bool flag = this._xamlTypeCacheByType.TryGetValue(type, ref xamlType);
			IXamlType result;
			if (flag)
			{
				result = xamlType;
			}
			else
			{
				int num = this.LookupTypeIndexByType(type);
				bool flag2 = num != -1;
				if (flag2)
				{
					xamlType = this.CreateXamlType(num);
				}
				XamlUserType xamlUserType = xamlType as XamlUserType;
				bool flag3 = xamlType == null || (xamlUserType != null && xamlUserType.IsReturnTypeStub && !xamlUserType.IsLocalType);
				if (flag3)
				{
					IXamlType xamlType2 = this.CheckOtherMetadataProvidersForType(type);
					bool flag4 = xamlType2 != null;
					if (flag4)
					{
						bool flag5 = xamlType2.IsConstructible || xamlType == null;
						if (flag5)
						{
							xamlType = xamlType2;
						}
					}
				}
				bool flag6 = xamlType != null;
				if (flag6)
				{
					this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
					this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
				}
				result = xamlType;
			}
			return result;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00017870 File Offset: 0x00015A70
		public IXamlType GetXamlTypeByName(string typeName)
		{
			bool flag = string.IsNullOrEmpty(typeName);
			IXamlType result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IXamlType xamlType;
				bool flag2 = this._xamlTypeCacheByName.TryGetValue(typeName, ref xamlType);
				if (flag2)
				{
					result = xamlType;
				}
				else
				{
					int num = this.LookupTypeIndexByName(typeName);
					bool flag3 = num != -1;
					if (flag3)
					{
						xamlType = this.CreateXamlType(num);
					}
					XamlUserType xamlUserType = xamlType as XamlUserType;
					bool flag4 = xamlType == null || (xamlUserType != null && xamlUserType.IsReturnTypeStub && !xamlUserType.IsLocalType);
					if (flag4)
					{
						IXamlType xamlType2 = this.CheckOtherMetadataProvidersForName(typeName);
						bool flag5 = xamlType2 != null;
						if (flag5)
						{
							bool flag6 = xamlType2.IsConstructible || xamlType == null;
							if (flag6)
							{
								xamlType = xamlType2;
							}
						}
					}
					bool flag7 = xamlType != null;
					if (flag7)
					{
						this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
						this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
					}
					result = xamlType;
				}
			}
			return result;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00017964 File Offset: 0x00015B64
		public IXamlMember GetMemberByLongName(string longMemberName)
		{
			bool flag = string.IsNullOrEmpty(longMemberName);
			IXamlMember result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IXamlMember xamlMember;
				bool flag2 = this._xamlMembers.TryGetValue(longMemberName, ref xamlMember);
				if (flag2)
				{
					result = xamlMember;
				}
				else
				{
					xamlMember = this.CreateXamlMember(longMemberName);
					bool flag3 = xamlMember != null;
					if (flag3)
					{
						this._xamlMembers.Add(longMemberName, xamlMember);
					}
					result = xamlMember;
				}
			}
			return result;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000179C0 File Offset: 0x00015BC0
		private void InitTypeTables()
		{
			this._typeNameTable = new string[49];
			this._typeNameTable[0] = "Win81StoreRevival.BooleanToVisibilityConverter";
			this._typeNameTable[1] = "Object";
			this._typeNameTable[2] = "Win81StoreRevival.RatingToRectangleConverter";
			this._typeNameTable[3] = "Win81StoreRevival.ZeroToVisibilityConverter";
			this._typeNameTable[4] = "Win81StoreRevival.StringToBitmapImageConverter";
			this._typeNameTable[5] = "Win81StoreRevival.AboutSettingsFlyout";
			this._typeNameTable[6] = "Windows.UI.Xaml.Controls.SettingsFlyout";
			this._typeNameTable[7] = "Windows.UI.Xaml.Controls.ContentControl";
			this._typeNameTable[8] = "Win81StoreRevival.AccountPage";
			this._typeNameTable[9] = "Windows.UI.Xaml.Controls.Page";
			this._typeNameTable[10] = "Windows.UI.Xaml.Controls.UserControl";
			this._typeNameTable[11] = "Win81StoreRevival.AddRating";
			this._typeNameTable[12] = "String";
			this._typeNameTable[13] = "Win81StoreRevival.RatingSummary";
			this._typeNameTable[14] = "Win81StoreRevival.AllAppsPage";
			this._typeNameTable[15] = "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.AppCollection>";
			this._typeNameTable[16] = "System.Collections.ObjectModel.Collection`1<Win81StoreRevival.AppCollection>";
			this._typeNameTable[17] = "Win81StoreRevival.AppCollection";
			this._typeNameTable[18] = "System.Collections.Generic.List`1<String>";
			this._typeNameTable[19] = "Win81StoreRevival.AllAppsPage1";
			this._typeNameTable[20] = "Win81StoreRevival.AppDetailsPage";
			this._typeNameTable[21] = "Win81StoreRevival.Common.NavigationHelper";
			this._typeNameTable[22] = "Windows.UI.Xaml.DependencyObject";
			this._typeNameTable[23] = "Win81StoreRevival.Common.ObservableDictionary";
			this._typeNameTable[24] = "Win81StoreRevival.AppReviewsPage";
			this._typeNameTable[25] = "Boolean";
			this._typeNameTable[26] = "Win81StoreRevival.CollectionsViewPage";
			this._typeNameTable[27] = "Win81StoreRevival.CustomSettingsPage";
			this._typeNameTable[28] = "Win81StoreRevival.Dependencies";
			this._typeNameTable[29] = "Win81StoreRevival.DownloadsHub";
			this._typeNameTable[30] = "Win81StoreRevival.ExtendedSplash";
			this._typeNameTable[31] = "Win81StoreRevival.FactoryResetFlyout";
			this._typeNameTable[32] = "Win81StoreRevival.FullScreenViewPage";
			this._typeNameTable[33] = "Win81StoreRevival.GeneralSettingsFlyout";
			this._typeNameTable[34] = "Win81StoreRevival.InstalledAppsPage";
			this._typeNameTable[35] = "Win81StoreRevival.LanguageSettingsFlyout";
			this._typeNameTable[36] = "Win81StoreRevival.Flyouts.LoginFlyout";
			this._typeNameTable[37] = "Win81StoreRevival.MainPage";
			this._typeNameTable[38] = "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.StoreApp>";
			this._typeNameTable[39] = "System.Collections.ObjectModel.Collection`1<Win81StoreRevival.StoreApp>";
			this._typeNameTable[40] = "Win81StoreRevival.StoreApp";
			this._typeNameTable[41] = "Win81StoreRevival.ReviewStats";
			this._typeNameTable[42] = "System.Collections.Generic.List`1<Win81StoreRevival.StoreApp>";
			this._typeNameTable[43] = "Win81StoreRevival.PopularPage";
			this._typeNameTable[44] = "Win81StoreRevival.PublisherAppsPage";
			this._typeNameTable[45] = "Win81StoreRevival.Flyouts.RegisterFlyout";
			this._typeNameTable[46] = "Win81StoreRevival.SettingsPage";
			this._typeNameTable[47] = "Win81StoreRevival.UpdatePage";
			this._typeNameTable[48] = "Win81StoreRevival.WelcomePage";
			this._typeTable = new Type[49];
			this._typeTable[0] = typeof(BooleanToVisibilityConverter);
			this._typeTable[1] = typeof(object);
			this._typeTable[2] = typeof(RatingToRectangleConverter);
			this._typeTable[3] = typeof(ZeroToVisibilityConverter);
			this._typeTable[4] = typeof(StringToBitmapImageConverter);
			this._typeTable[5] = typeof(AboutSettingsFlyout);
			this._typeTable[6] = typeof(SettingsFlyout);
			this._typeTable[7] = typeof(ContentControl);
			this._typeTable[8] = typeof(AccountPage);
			this._typeTable[9] = typeof(Page);
			this._typeTable[10] = typeof(UserControl);
			this._typeTable[11] = typeof(AddRating);
			this._typeTable[12] = typeof(string);
			this._typeTable[13] = typeof(RatingSummary);
			this._typeTable[14] = typeof(AllAppsPage);
			this._typeTable[15] = typeof(ObservableCollection<AppCollection>);
			this._typeTable[16] = typeof(Collection<AppCollection>);
			this._typeTable[17] = typeof(AppCollection);
			this._typeTable[18] = typeof(List<string>);
			this._typeTable[19] = typeof(AllAppsPage1);
			this._typeTable[20] = typeof(AppDetailsPage);
			this._typeTable[21] = typeof(NavigationHelper);
			this._typeTable[22] = typeof(DependencyObject);
			this._typeTable[23] = typeof(ObservableDictionary);
			this._typeTable[24] = typeof(AppReviewsPage);
			this._typeTable[25] = typeof(bool);
			this._typeTable[26] = typeof(CollectionsViewPage);
			this._typeTable[27] = typeof(CustomSettingsPage);
			this._typeTable[28] = typeof(Dependencies);
			this._typeTable[29] = typeof(DownloadsHub);
			this._typeTable[30] = typeof(ExtendedSplash);
			this._typeTable[31] = typeof(FactoryResetFlyout);
			this._typeTable[32] = typeof(FullScreenViewPage);
			this._typeTable[33] = typeof(GeneralSettingsFlyout);
			this._typeTable[34] = typeof(InstalledAppsPage);
			this._typeTable[35] = typeof(LanguageSettingsFlyout);
			this._typeTable[36] = typeof(LoginFlyout);
			this._typeTable[37] = typeof(MainPage);
			this._typeTable[38] = typeof(ObservableCollection<StoreApp>);
			this._typeTable[39] = typeof(Collection<StoreApp>);
			this._typeTable[40] = typeof(StoreApp);
			this._typeTable[41] = typeof(ReviewStats);
			this._typeTable[42] = typeof(List<StoreApp>);
			this._typeTable[43] = typeof(PopularPage);
			this._typeTable[44] = typeof(PublisherAppsPage);
			this._typeTable[45] = typeof(RegisterFlyout);
			this._typeTable[46] = typeof(SettingsPage);
			this._typeTable[47] = typeof(UpdatePage);
			this._typeTable[48] = typeof(WelcomePage);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00018028 File Offset: 0x00016228
		private int LookupTypeIndexByName(string typeName)
		{
			bool flag = this._typeNameTable == null;
			if (flag)
			{
				this.InitTypeTables();
			}
			for (int i = 0; i < this._typeNameTable.Length; i++)
			{
				bool flag2 = string.CompareOrdinal(this._typeNameTable[i], typeName) == 0;
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00018088 File Offset: 0x00016288
		private int LookupTypeIndexByType(Type type)
		{
			bool flag = this._typeTable == null;
			if (flag)
			{
				this.InitTypeTables();
			}
			for (int i = 0; i < this._typeTable.Length; i++)
			{
				bool flag2 = type == this._typeTable[i];
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000180E0 File Offset: 0x000162E0
		private object Activate_0_BooleanToVisibilityConverter()
		{
			return new BooleanToVisibilityConverter();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000180F8 File Offset: 0x000162F8
		private object Activate_2_RatingToRectangleConverter()
		{
			return new RatingToRectangleConverter();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00018110 File Offset: 0x00016310
		private object Activate_3_ZeroToVisibilityConverter()
		{
			return new ZeroToVisibilityConverter();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00018128 File Offset: 0x00016328
		private object Activate_4_StringToBitmapImageConverter()
		{
			return new StringToBitmapImageConverter();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00018140 File Offset: 0x00016340
		private object Activate_5_AboutSettingsFlyout()
		{
			return new AboutSettingsFlyout();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00018158 File Offset: 0x00016358
		private object Activate_8_AccountPage()
		{
			return new AccountPage();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00018170 File Offset: 0x00016370
		private object Activate_11_AddRating()
		{
			return new AddRating();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00018188 File Offset: 0x00016388
		private object Activate_13_RatingSummary()
		{
			return new RatingSummary();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000181A0 File Offset: 0x000163A0
		private object Activate_14_AllAppsPage()
		{
			return new AllAppsPage();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000181B8 File Offset: 0x000163B8
		private object Activate_15_ObservableCollection()
		{
			return new ObservableCollection<AppCollection>();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000181D0 File Offset: 0x000163D0
		private object Activate_16_Collection()
		{
			return new Collection<AppCollection>();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000181E8 File Offset: 0x000163E8
		private object Activate_17_AppCollection()
		{
			return new AppCollection();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00018200 File Offset: 0x00016400
		private object Activate_18_List()
		{
			return new List<string>();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00018218 File Offset: 0x00016418
		private object Activate_19_AllAppsPage1()
		{
			return new AllAppsPage1();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00018230 File Offset: 0x00016430
		private object Activate_20_AppDetailsPage()
		{
			return new AppDetailsPage();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00018248 File Offset: 0x00016448
		private object Activate_23_ObservableDictionary()
		{
			return new ObservableDictionary();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00018260 File Offset: 0x00016460
		private object Activate_24_AppReviewsPage()
		{
			return new AppReviewsPage();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00018278 File Offset: 0x00016478
		private object Activate_26_CollectionsViewPage()
		{
			return new CollectionsViewPage();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00018290 File Offset: 0x00016490
		private object Activate_27_CustomSettingsPage()
		{
			return new CustomSettingsPage();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000182A8 File Offset: 0x000164A8
		private object Activate_28_Dependencies()
		{
			return new Dependencies();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000182C0 File Offset: 0x000164C0
		private object Activate_29_DownloadsHub()
		{
			return new DownloadsHub();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000182D8 File Offset: 0x000164D8
		private object Activate_31_FactoryResetFlyout()
		{
			return new FactoryResetFlyout();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000182F0 File Offset: 0x000164F0
		private object Activate_32_FullScreenViewPage()
		{
			return new FullScreenViewPage();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00018308 File Offset: 0x00016508
		private object Activate_33_GeneralSettingsFlyout()
		{
			return new GeneralSettingsFlyout();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00018320 File Offset: 0x00016520
		private object Activate_34_InstalledAppsPage()
		{
			return new InstalledAppsPage();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00018338 File Offset: 0x00016538
		private object Activate_35_LanguageSettingsFlyout()
		{
			return new LanguageSettingsFlyout();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00018350 File Offset: 0x00016550
		private object Activate_36_LoginFlyout()
		{
			return new LoginFlyout();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00018368 File Offset: 0x00016568
		private object Activate_37_MainPage()
		{
			return new MainPage();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00018380 File Offset: 0x00016580
		private object Activate_38_ObservableCollection()
		{
			return new ObservableCollection<StoreApp>();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00018398 File Offset: 0x00016598
		private object Activate_39_Collection()
		{
			return new Collection<StoreApp>();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000183B0 File Offset: 0x000165B0
		private object Activate_40_StoreApp()
		{
			return new StoreApp();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000183C8 File Offset: 0x000165C8
		private object Activate_41_ReviewStats()
		{
			return new ReviewStats();
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000183E0 File Offset: 0x000165E0
		private object Activate_42_List()
		{
			return new List<StoreApp>();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000183F8 File Offset: 0x000165F8
		private object Activate_43_PopularPage()
		{
			return new PopularPage();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00018410 File Offset: 0x00016610
		private object Activate_44_PublisherAppsPage()
		{
			return new PublisherAppsPage();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00018428 File Offset: 0x00016628
		private object Activate_45_RegisterFlyout()
		{
			return new RegisterFlyout();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00018440 File Offset: 0x00016640
		private object Activate_46_SettingsPage()
		{
			return new SettingsPage();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00018458 File Offset: 0x00016658
		private object Activate_47_UpdatePage()
		{
			return new UpdatePage();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00018470 File Offset: 0x00016670
		private object Activate_48_WelcomePage()
		{
			return new WelcomePage();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00018488 File Offset: 0x00016688
		private void VectorAdd_15_ObservableCollection(object instance, object item)
		{
			ICollection<AppCollection> collection = (ICollection<AppCollection>)instance;
			AppCollection appCollection = (AppCollection)item;
			collection.Add(appCollection);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000184AC File Offset: 0x000166AC
		private void VectorAdd_16_Collection(object instance, object item)
		{
			ICollection<AppCollection> collection = (ICollection<AppCollection>)instance;
			AppCollection appCollection = (AppCollection)item;
			collection.Add(appCollection);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000184D0 File Offset: 0x000166D0
		private void VectorAdd_18_List(object instance, object item)
		{
			ICollection<string> collection = (ICollection<string>)instance;
			string text = (string)item;
			collection.Add(text);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000184F4 File Offset: 0x000166F4
		private void MapAdd_23_ObservableDictionary(object instance, object key, object item)
		{
			IDictionary<string, object> dictionary = (IDictionary<string, object>)instance;
			string text = (string)key;
			dictionary.Add(text, item);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001851C File Offset: 0x0001671C
		private void VectorAdd_38_ObservableCollection(object instance, object item)
		{
			ICollection<StoreApp> collection = (ICollection<StoreApp>)instance;
			StoreApp storeApp = (StoreApp)item;
			collection.Add(storeApp);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00018540 File Offset: 0x00016740
		private void VectorAdd_39_Collection(object instance, object item)
		{
			ICollection<StoreApp> collection = (ICollection<StoreApp>)instance;
			StoreApp storeApp = (StoreApp)item;
			collection.Add(storeApp);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00018564 File Offset: 0x00016764
		private void VectorAdd_42_List(object instance, object item)
		{
			ICollection<StoreApp> collection = (ICollection<StoreApp>)instance;
			StoreApp storeApp = (StoreApp)item;
			collection.Add(storeApp);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00018588 File Offset: 0x00016788
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
				xamlUserType.Activator = new Activator(this.Activate_0_BooleanToVisibilityConverter);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 1:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 2:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.Activator = new Activator(this.Activate_2_RatingToRectangleConverter);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 3:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.Activator = new Activator(this.Activate_3_ZeroToVisibilityConverter);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 4:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.Activator = new Activator(this.Activate_4_StringToBitmapImageConverter);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 5:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType.Activator = new Activator(this.Activate_5_AboutSettingsFlyout);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 6:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 7:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 8:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_8_AccountPage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
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
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType.Activator = new Activator(this.Activate_11_AddRating);
				xamlUserType.AddMemberName("AppId");
				xamlUserType.AddMemberName("LastRatings");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 12:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 13:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.SetIsReturnTypeStub();
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 14:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_14_AllAppsPage);
				xamlUserType.AddMemberName("AllCollections");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 15:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Win81StoreRevival.AppCollection>"));
				xamlUserType.CollectionAdd = new AddToCollection(this.VectorAdd_15_ObservableCollection);
				xamlUserType.SetIsReturnTypeStub();
				result = xamlUserType;
				break;
			}
			case 16:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_16_Collection),
					CollectionAdd = new AddToCollection(this.VectorAdd_16_Collection)
				};
				break;
			case 17:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.Activator = new Activator(this.Activate_17_AppCollection);
				xamlUserType.AddMemberName("Name");
				xamlUserType.AddMemberName("AppIcons");
				xamlUserType.AddMemberName("AppCount");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 18:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.CollectionAdd = new AddToCollection(this.VectorAdd_18_List);
				xamlUserType.SetIsReturnTypeStub();
				result = xamlUserType;
				break;
			}
			case 19:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_19_AllAppsPage1);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 20:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_20_AppDetailsPage);
				xamlUserType.AddMemberName("NavigationHelper");
				xamlUserType.AddMemberName("DefaultViewModel");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 21:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.DependencyObject"));
				xamlUserType.SetIsReturnTypeStub();
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 22:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 23:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.DictionaryAdd = new AddToDictionary(this.MapAdd_23_ObservableDictionary);
				xamlUserType.SetIsReturnTypeStub();
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 24:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_24_AppReviewsPage);
				xamlUserType.AddMemberName("IsUserLoggedIn");
				xamlUserType.AddMemberName("NavigationHelper");
				xamlUserType.AddMemberName("DefaultViewModel");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 25:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 26:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_26_CollectionsViewPage);
				xamlUserType.AddMemberName("AllCollections");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 27:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_27_CustomSettingsPage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 28:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_28_Dependencies);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 29:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_29_DownloadsHub);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 30:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 31:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType.Activator = new Activator(this.Activate_31_FactoryResetFlyout);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 32:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_32_FullScreenViewPage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 33:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType.Activator = new Activator(this.Activate_33_GeneralSettingsFlyout);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 34:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_34_InstalledAppsPage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 35:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType.Activator = new Activator(this.Activate_35_LanguageSettingsFlyout);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 36:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType.Activator = new Activator(this.Activate_36_LoginFlyout);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 37:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_37_MainPage);
				xamlUserType.AddMemberName("MoreApps");
				xamlUserType.AddMemberName("RandomApps");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 38:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Win81StoreRevival.StoreApp>"));
				xamlUserType.CollectionAdd = new AddToCollection(this.VectorAdd_38_ObservableCollection);
				xamlUserType.SetIsReturnTypeStub();
				result = xamlUserType;
				break;
			}
			case 39:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_39_Collection),
					CollectionAdd = new AddToCollection(this.VectorAdd_39_Collection)
				};
				break;
			case 40:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.Activator = new Activator(this.Activate_40_StoreApp);
				xamlUserType.AddMemberName("Id");
				xamlUserType.AddMemberName("Name");
				xamlUserType.AddMemberName("Publisher");
				xamlUserType.AddMemberName("Version");
				xamlUserType.AddMemberName("DownloadUrl");
				xamlUserType.AddMemberName("IconUrl");
				xamlUserType.AddMemberName("Description");
				xamlUserType.AddMemberName("Featured");
				xamlUserType.AddMemberName("Type");
				xamlUserType.AddMemberName("Category");
				xamlUserType.AddMemberName("Screenshot1");
				xamlUserType.AddMemberName("Screenshot2");
				xamlUserType.AddMemberName("Screenshot3");
				xamlUserType.AddMemberName("ReviewStats");
				xamlUserType.AddMemberName("Screenshots");
				xamlUserType.AddMemberName("RelatedApps");
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 41:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.SetIsReturnTypeStub();
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 42:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				xamlUserType.CollectionAdd = new AddToCollection(this.VectorAdd_42_List);
				xamlUserType.SetIsReturnTypeStub();
				result = xamlUserType;
				break;
			}
			case 43:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_43_PopularPage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 44:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_44_PublisherAppsPage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 45:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.SettingsFlyout"));
				xamlUserType.Activator = new Activator(this.Activate_45_RegisterFlyout);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 46:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_46_SettingsPage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 47:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_47_UpdatePage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			case 48:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
				xamlUserType.Activator = new Activator(this.Activate_48_WelcomePage);
				xamlUserType.SetIsLocalType();
				result = xamlUserType;
				break;
			}
			}
			return result;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000190B0 File Offset: 0x000172B0
		private List<IXamlMetadataProvider> OtherProviders
		{
			get
			{
				bool flag = this._otherProviders == null;
				if (flag)
				{
					this._otherProviders = new List<IXamlMetadataProvider>();
					IXamlMetadataProvider xamlMetadataProvider = new XamlMetaDataProvider();
					this._otherProviders.Add(xamlMetadataProvider);
				}
				return this._otherProviders;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000190F8 File Offset: 0x000172F8
		private IXamlType CheckOtherMetadataProvidersForName(string typeName)
		{
			IXamlType result = null;
			foreach (IXamlMetadataProvider xamlMetadataProvider in this.OtherProviders)
			{
				IXamlType xamlType = xamlMetadataProvider.GetXamlType(typeName);
				bool flag = xamlType != null;
				if (flag)
				{
					bool isConstructible = xamlType.IsConstructible;
					if (isConstructible)
					{
						return xamlType;
					}
					result = xamlType;
				}
			}
			return result;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001917C File Offset: 0x0001737C
		private IXamlType CheckOtherMetadataProvidersForType(Type type)
		{
			IXamlType result = null;
			foreach (IXamlMetadataProvider xamlMetadataProvider in this.OtherProviders)
			{
				IXamlType xamlType = xamlMetadataProvider.GetXamlType(type);
				bool flag = xamlType != null;
				if (flag)
				{
					bool isConstructible = xamlType.IsConstructible;
					if (isConstructible)
					{
						return xamlType;
					}
					result = xamlType;
				}
			}
			return result;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00019200 File Offset: 0x00017400
		private object get_0_AddRating_AppId(object instance)
		{
			AddRating addRating = (AddRating)instance;
			return addRating.AppId;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00019220 File Offset: 0x00017420
		private void set_0_AddRating_AppId(object instance, object Value)
		{
			AddRating addRating = (AddRating)instance;
			addRating.AppId = (string)Value;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00019244 File Offset: 0x00017444
		private object get_1_AddRating_LastRatings(object instance)
		{
			AddRating addRating = (AddRating)instance;
			return addRating.LastRatings;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00019264 File Offset: 0x00017464
		private void set_1_AddRating_LastRatings(object instance, object Value)
		{
			AddRating addRating = (AddRating)instance;
			addRating.LastRatings = (RatingSummary)Value;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00019288 File Offset: 0x00017488
		private object get_2_AllAppsPage_AllCollections(object instance)
		{
			AllAppsPage allAppsPage = (AllAppsPage)instance;
			return allAppsPage.AllCollections;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000192A8 File Offset: 0x000174A8
		private void set_2_AllAppsPage_AllCollections(object instance, object Value)
		{
			AllAppsPage allAppsPage = (AllAppsPage)instance;
			allAppsPage.AllCollections = (ObservableCollection<AppCollection>)Value;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000192CC File Offset: 0x000174CC
		private object get_3_AppCollection_Name(object instance)
		{
			AppCollection appCollection = (AppCollection)instance;
			return appCollection.Name;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000192EC File Offset: 0x000174EC
		private void set_3_AppCollection_Name(object instance, object Value)
		{
			AppCollection appCollection = (AppCollection)instance;
			appCollection.Name = (string)Value;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00019310 File Offset: 0x00017510
		private object get_4_AppCollection_AppIcons(object instance)
		{
			AppCollection appCollection = (AppCollection)instance;
			return appCollection.AppIcons;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00019330 File Offset: 0x00017530
		private void set_4_AppCollection_AppIcons(object instance, object Value)
		{
			AppCollection appCollection = (AppCollection)instance;
			appCollection.AppIcons = (List<string>)Value;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00019354 File Offset: 0x00017554
		private object get_5_AppCollection_AppCount(object instance)
		{
			AppCollection appCollection = (AppCollection)instance;
			return appCollection.AppCount;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00019374 File Offset: 0x00017574
		private void set_5_AppCollection_AppCount(object instance, object Value)
		{
			AppCollection appCollection = (AppCollection)instance;
			appCollection.AppCount = (string)Value;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00019398 File Offset: 0x00017598
		private object get_6_AppDetailsPage_NavigationHelper(object instance)
		{
			AppDetailsPage appDetailsPage = (AppDetailsPage)instance;
			return appDetailsPage.NavigationHelper;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000193B8 File Offset: 0x000175B8
		private object get_7_AppDetailsPage_DefaultViewModel(object instance)
		{
			AppDetailsPage appDetailsPage = (AppDetailsPage)instance;
			return appDetailsPage.DefaultViewModel;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000193D8 File Offset: 0x000175D8
		private object get_8_AppReviewsPage_IsUserLoggedIn(object instance)
		{
			AppReviewsPage appReviewsPage = (AppReviewsPage)instance;
			return appReviewsPage.IsUserLoggedIn;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000193FC File Offset: 0x000175FC
		private void set_8_AppReviewsPage_IsUserLoggedIn(object instance, object Value)
		{
			AppReviewsPage appReviewsPage = (AppReviewsPage)instance;
			appReviewsPage.IsUserLoggedIn = (bool)Value;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00019420 File Offset: 0x00017620
		private object get_9_AppReviewsPage_NavigationHelper(object instance)
		{
			AppReviewsPage appReviewsPage = (AppReviewsPage)instance;
			return appReviewsPage.NavigationHelper;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00019440 File Offset: 0x00017640
		private object get_10_AppReviewsPage_DefaultViewModel(object instance)
		{
			AppReviewsPage appReviewsPage = (AppReviewsPage)instance;
			return appReviewsPage.DefaultViewModel;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00019460 File Offset: 0x00017660
		private object get_11_CollectionsViewPage_AllCollections(object instance)
		{
			CollectionsViewPage collectionsViewPage = (CollectionsViewPage)instance;
			return collectionsViewPage.AllCollections;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00019480 File Offset: 0x00017680
		private void set_11_CollectionsViewPage_AllCollections(object instance, object Value)
		{
			CollectionsViewPage collectionsViewPage = (CollectionsViewPage)instance;
			collectionsViewPage.AllCollections = (ObservableCollection<AppCollection>)Value;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000194A4 File Offset: 0x000176A4
		private object get_12_MainPage_MoreApps(object instance)
		{
			MainPage mainPage = (MainPage)instance;
			return mainPage.MoreApps;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000194C4 File Offset: 0x000176C4
		private void set_12_MainPage_MoreApps(object instance, object Value)
		{
			MainPage mainPage = (MainPage)instance;
			mainPage.MoreApps = (ObservableCollection<StoreApp>)Value;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000194E8 File Offset: 0x000176E8
		private object get_13_StoreApp_Id(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Id;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00019508 File Offset: 0x00017708
		private void set_13_StoreApp_Id(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Id = (string)Value;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001952C File Offset: 0x0001772C
		private object get_14_StoreApp_Name(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Name;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001954C File Offset: 0x0001774C
		private void set_14_StoreApp_Name(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Name = (string)Value;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00019570 File Offset: 0x00017770
		private object get_15_StoreApp_Publisher(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Publisher;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00019590 File Offset: 0x00017790
		private void set_15_StoreApp_Publisher(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Publisher = (string)Value;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000195B4 File Offset: 0x000177B4
		private object get_16_StoreApp_Version(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Version;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000195D4 File Offset: 0x000177D4
		private void set_16_StoreApp_Version(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Version = (string)Value;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000195F8 File Offset: 0x000177F8
		private object get_17_StoreApp_DownloadUrl(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.DownloadUrl;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00019618 File Offset: 0x00017818
		private void set_17_StoreApp_DownloadUrl(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.DownloadUrl = (string)Value;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001963C File Offset: 0x0001783C
		private object get_18_StoreApp_IconUrl(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.IconUrl;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001965C File Offset: 0x0001785C
		private void set_18_StoreApp_IconUrl(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.IconUrl = (string)Value;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00019680 File Offset: 0x00017880
		private object get_19_StoreApp_Description(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Description;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000196A0 File Offset: 0x000178A0
		private void set_19_StoreApp_Description(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Description = (string)Value;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000196C4 File Offset: 0x000178C4
		private object get_20_StoreApp_Featured(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Featured;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000196E8 File Offset: 0x000178E8
		private void set_20_StoreApp_Featured(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Featured = (bool)Value;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001970C File Offset: 0x0001790C
		private object get_21_StoreApp_Type(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Type;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001972C File Offset: 0x0001792C
		private void set_21_StoreApp_Type(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Type = (string)Value;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00019750 File Offset: 0x00017950
		private object get_22_StoreApp_Category(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Category;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00019770 File Offset: 0x00017970
		private void set_22_StoreApp_Category(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Category = (string)Value;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00019794 File Offset: 0x00017994
		private object get_23_StoreApp_Screenshot1(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Screenshot1;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000197B4 File Offset: 0x000179B4
		private void set_23_StoreApp_Screenshot1(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Screenshot1 = (string)Value;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000197D8 File Offset: 0x000179D8
		private object get_24_StoreApp_Screenshot2(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Screenshot2;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000197F8 File Offset: 0x000179F8
		private void set_24_StoreApp_Screenshot2(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Screenshot2 = (string)Value;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001981C File Offset: 0x00017A1C
		private object get_25_StoreApp_Screenshot3(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Screenshot3;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001983C File Offset: 0x00017A3C
		private void set_25_StoreApp_Screenshot3(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.Screenshot3 = (string)Value;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00019860 File Offset: 0x00017A60
		private object get_26_StoreApp_ReviewStats(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.ReviewStats;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00019880 File Offset: 0x00017A80
		private void set_26_StoreApp_ReviewStats(object instance, object Value)
		{
			StoreApp storeApp = (StoreApp)instance;
			storeApp.ReviewStats = (ReviewStats)Value;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000198A4 File Offset: 0x00017AA4
		private object get_27_StoreApp_Screenshots(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.Screenshots;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000198C4 File Offset: 0x00017AC4
		private object get_28_StoreApp_RelatedApps(object instance)
		{
			StoreApp storeApp = (StoreApp)instance;
			return storeApp.RelatedApps;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000198E4 File Offset: 0x00017AE4
		private object get_29_MainPage_RandomApps(object instance)
		{
			MainPage mainPage = (MainPage)instance;
			return mainPage.RandomApps;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00019904 File Offset: 0x00017B04
		private void set_29_MainPage_RandomApps(object instance, object Value)
		{
			MainPage mainPage = (MainPage)instance;
			mainPage.RandomApps = (ObservableCollection<StoreApp>)Value;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00019928 File Offset: 0x00017B28
		private IXamlMember CreateXamlMember(string longMemberName)
		{
			XamlMember xamlMember = null;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(longMemberName);
			if (num <= 2437633725U)
			{
				if (num <= 1581248573U)
				{
					if (num <= 674657043U)
					{
						if (num != 146079971U)
						{
							if (num != 327335453U)
							{
								if (num == 674657043U)
								{
									if (longMemberName == "Win81StoreRevival.AppDetailsPage.DefaultViewModel")
									{
										XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppDetailsPage");
										xamlMember = new XamlMember(this, "DefaultViewModel", "Win81StoreRevival.Common.ObservableDictionary");
										xamlMember.Getter = new Getter(this.get_7_AppDetailsPage_DefaultViewModel);
										xamlMember.SetIsReadOnly();
									}
								}
							}
							else if (longMemberName == "Win81StoreRevival.AppCollection.AppCount")
							{
								XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppCollection");
								xamlMember = new XamlMember(this, "AppCount", "String");
								xamlMember.Getter = new Getter(this.get_5_AppCollection_AppCount);
								xamlMember.Setter = new Setter(this.set_5_AppCollection_AppCount);
							}
						}
						else if (longMemberName == "Win81StoreRevival.StoreApp.Screenshots")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "Screenshots", "System.Collections.Generic.List`1<String>");
							xamlMember.Getter = new Getter(this.get_27_StoreApp_Screenshots);
							xamlMember.SetIsReadOnly();
						}
					}
					else if (num <= 1377840696U)
					{
						if (num != 1025140903U)
						{
							if (num == 1377840696U)
							{
								if (longMemberName == "Win81StoreRevival.AppCollection.AppIcons")
								{
									XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppCollection");
									xamlMember = new XamlMember(this, "AppIcons", "System.Collections.Generic.List`1<String>");
									xamlMember.Getter = new Getter(this.get_4_AppCollection_AppIcons);
									xamlMember.Setter = new Setter(this.set_4_AppCollection_AppIcons);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.MainPage.RandomApps")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.MainPage");
							xamlMember = new XamlMember(this, "RandomApps", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.StoreApp>");
							xamlMember.Getter = new Getter(this.get_29_MainPage_RandomApps);
							xamlMember.Setter = new Setter(this.set_29_MainPage_RandomApps);
						}
					}
					else if (num != 1432875812U)
					{
						if (num == 1581248573U)
						{
							if (longMemberName == "Win81StoreRevival.AppReviewsPage.IsUserLoggedIn")
							{
								XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppReviewsPage");
								xamlMember = new XamlMember(this, "IsUserLoggedIn", "Boolean");
								xamlMember.Getter = new Getter(this.get_8_AppReviewsPage_IsUserLoggedIn);
								xamlMember.Setter = new Setter(this.set_8_AppReviewsPage_IsUserLoggedIn);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Featured")
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Featured", "Boolean");
						xamlMember.Getter = new Getter(this.get_20_StoreApp_Featured);
						xamlMember.Setter = new Setter(this.set_20_StoreApp_Featured);
					}
				}
				else if (num <= 1971398645U)
				{
					if (num <= 1814434223U)
					{
						if (num != 1736797010U)
						{
							if (num == 1814434223U)
							{
								if (longMemberName == "Win81StoreRevival.StoreApp.DownloadUrl")
								{
									XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
									xamlMember = new XamlMember(this, "DownloadUrl", "String");
									xamlMember.Getter = new Getter(this.get_17_StoreApp_DownloadUrl);
									xamlMember.Setter = new Setter(this.set_17_StoreApp_DownloadUrl);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.AllAppsPage.AllCollections")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AllAppsPage");
							xamlMember = new XamlMember(this, "AllCollections", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.AppCollection>");
							xamlMember.Getter = new Getter(this.get_2_AllAppsPage_AllCollections);
							xamlMember.Setter = new Setter(this.set_2_AllAppsPage_AllCollections);
						}
					}
					else if (num != 1945659464U)
					{
						if (num == 1971398645U)
						{
							if (longMemberName == "Win81StoreRevival.MainPage.MoreApps")
							{
								XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.MainPage");
								xamlMember = new XamlMember(this, "MoreApps", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.StoreApp>");
								xamlMember.Getter = new Getter(this.get_12_MainPage_MoreApps);
								xamlMember.Setter = new Setter(this.set_12_MainPage_MoreApps);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Description")
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Description", "String");
						xamlMember.Getter = new Getter(this.get_19_StoreApp_Description);
						xamlMember.Setter = new Setter(this.set_19_StoreApp_Description);
					}
				}
				else if (num <= 2290410522U)
				{
					if (num != 2048228270U)
					{
						if (num == 2290410522U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.Version")
							{
								XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Version", "String");
								xamlMember.Getter = new Getter(this.get_16_StoreApp_Version);
								xamlMember.Setter = new Setter(this.set_16_StoreApp_Version);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Category")
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Category", "String");
						xamlMember.Getter = new Getter(this.get_22_StoreApp_Category);
						xamlMember.Setter = new Setter(this.set_22_StoreApp_Category);
					}
				}
				else if (num != 2296821301U)
				{
					if (num == 2437633725U)
					{
						if (longMemberName == "Win81StoreRevival.AppReviewsPage.NavigationHelper")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppReviewsPage");
							xamlMember = new XamlMember(this, "NavigationHelper", "Win81StoreRevival.Common.NavigationHelper");
							xamlMember.Getter = new Getter(this.get_9_AppReviewsPage_NavigationHelper);
							xamlMember.SetIsReadOnly();
						}
					}
				}
				else if (longMemberName == "Win81StoreRevival.StoreApp.Name")
				{
					XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
					xamlMember = new XamlMember(this, "Name", "String");
					xamlMember.Getter = new Getter(this.get_14_StoreApp_Name);
					xamlMember.Setter = new Setter(this.set_14_StoreApp_Name);
				}
			}
			else if (num <= 3192891237U)
			{
				if (num <= 2820051712U)
				{
					if (num != 2510026580U)
					{
						if (num != 2604593215U)
						{
							if (num == 2820051712U)
							{
								if (longMemberName == "Win81StoreRevival.AppCollection.Name")
								{
									XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppCollection");
									xamlMember = new XamlMember(this, "Name", "String");
									xamlMember.Getter = new Getter(this.get_3_AppCollection_Name);
									xamlMember.Setter = new Setter(this.set_3_AppCollection_Name);
								}
							}
						}
						else if (longMemberName == "Win81StoreRevival.CollectionsViewPage.AllCollections")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.CollectionsViewPage");
							xamlMember = new XamlMember(this, "AllCollections", "System.Collections.ObjectModel.ObservableCollection`1<Win81StoreRevival.AppCollection>");
							xamlMember.Getter = new Getter(this.get_11_CollectionsViewPage_AllCollections);
							xamlMember.Setter = new Setter(this.set_11_CollectionsViewPage_AllCollections);
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.IconUrl")
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "IconUrl", "String");
						xamlMember.Getter = new Getter(this.get_18_StoreApp_IconUrl);
						xamlMember.Setter = new Setter(this.set_18_StoreApp_IconUrl);
					}
				}
				else if (num <= 3003527666U)
				{
					if (num != 2857607275U)
					{
						if (num == 3003527666U)
						{
							if (longMemberName == "Win81StoreRevival.AppReviewsPage.DefaultViewModel")
							{
								XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppReviewsPage");
								xamlMember = new XamlMember(this, "DefaultViewModel", "Win81StoreRevival.Common.ObservableDictionary");
								xamlMember.Getter = new Getter(this.get_10_AppReviewsPage_DefaultViewModel);
								xamlMember.SetIsReadOnly();
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.Id")
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "Id", "String");
						xamlMember.Getter = new Getter(this.get_13_StoreApp_Id);
						xamlMember.Setter = new Setter(this.set_13_StoreApp_Id);
					}
				}
				else if (num != 3045844300U)
				{
					if (num == 3192891237U)
					{
						if (longMemberName == "Win81StoreRevival.StoreApp.ReviewStats")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "ReviewStats", "Win81StoreRevival.ReviewStats");
							xamlMember.Getter = new Getter(this.get_26_StoreApp_ReviewStats);
							xamlMember.Setter = new Setter(this.set_26_StoreApp_ReviewStats);
						}
					}
				}
				else if (longMemberName == "Win81StoreRevival.AppDetailsPage.NavigationHelper")
				{
					XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AppDetailsPage");
					xamlMember = new XamlMember(this, "NavigationHelper", "Win81StoreRevival.Common.NavigationHelper");
					xamlMember.Getter = new Getter(this.get_6_AppDetailsPage_NavigationHelper);
					xamlMember.SetIsReadOnly();
				}
			}
			else if (num <= 3400834889U)
			{
				if (num <= 3350502032U)
				{
					if (num != 3234355249U)
					{
						if (num == 3350502032U)
						{
							if (longMemberName == "Win81StoreRevival.StoreApp.Screenshot2")
							{
								XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
								xamlMember = new XamlMember(this, "Screenshot2", "String");
								xamlMember.Getter = new Getter(this.get_24_StoreApp_Screenshot2);
								xamlMember.Setter = new Setter(this.set_24_StoreApp_Screenshot2);
							}
						}
					}
					else if (longMemberName == "Win81StoreRevival.StoreApp.RelatedApps")
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
						xamlMember = new XamlMember(this, "RelatedApps", "System.Collections.Generic.List`1<Win81StoreRevival.StoreApp>");
						xamlMember.Getter = new Getter(this.get_28_StoreApp_RelatedApps);
						xamlMember.SetIsReadOnly();
					}
				}
				else if (num != 3367279651U)
				{
					if (num == 3400834889U)
					{
						if (longMemberName == "Win81StoreRevival.StoreApp.Screenshot1")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "Screenshot1", "String");
							xamlMember.Getter = new Getter(this.get_23_StoreApp_Screenshot1);
							xamlMember.Setter = new Setter(this.set_23_StoreApp_Screenshot1);
						}
					}
				}
				else if (longMemberName == "Win81StoreRevival.StoreApp.Screenshot3")
				{
					XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
					xamlMember = new XamlMember(this, "Screenshot3", "String");
					xamlMember.Getter = new Getter(this.get_25_StoreApp_Screenshot3);
					xamlMember.Setter = new Setter(this.set_25_StoreApp_Screenshot3);
				}
			}
			else if (num <= 3634082022U)
			{
				if (num != 3438655830U)
				{
					if (num == 3634082022U)
					{
						if (longMemberName == "Win81StoreRevival.StoreApp.Type")
						{
							XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
							xamlMember = new XamlMember(this, "Type", "String");
							xamlMember.Getter = new Getter(this.get_21_StoreApp_Type);
							xamlMember.Setter = new Setter(this.set_21_StoreApp_Type);
						}
					}
				}
				else if (longMemberName == "Win81StoreRevival.StoreApp.Publisher")
				{
					XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.StoreApp");
					xamlMember = new XamlMember(this, "Publisher", "String");
					xamlMember.Getter = new Getter(this.get_15_StoreApp_Publisher);
					xamlMember.Setter = new Setter(this.set_15_StoreApp_Publisher);
				}
			}
			else if (num != 3652862178U)
			{
				if (num == 4066399926U)
				{
					if (longMemberName == "Win81StoreRevival.AddRating.AppId")
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AddRating");
						xamlMember = new XamlMember(this, "AppId", "String");
						xamlMember.Getter = new Getter(this.get_0_AddRating_AppId);
						xamlMember.Setter = new Setter(this.set_0_AddRating_AppId);
					}
				}
			}
			else if (longMemberName == "Win81StoreRevival.AddRating.LastRatings")
			{
				XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Win81StoreRevival.AddRating");
				xamlMember = new XamlMember(this, "LastRatings", "Win81StoreRevival.RatingSummary");
				xamlMember.Getter = new Getter(this.get_1_AddRating_LastRatings);
				xamlMember.Setter = new Setter(this.set_1_AddRating_LastRatings);
			}
			return xamlMember;
		}

		// Token: 0x040001F9 RID: 505
		private Dictionary<string, IXamlType> _xamlTypeCacheByName = new Dictionary<string, IXamlType>();

		// Token: 0x040001FA RID: 506
		private Dictionary<Type, IXamlType> _xamlTypeCacheByType = new Dictionary<Type, IXamlType>();

		// Token: 0x040001FB RID: 507
		private Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();

		// Token: 0x040001FC RID: 508
		private string[] _typeNameTable = null;

		// Token: 0x040001FD RID: 509
		private Type[] _typeTable = null;

		// Token: 0x040001FE RID: 510
		private List<IXamlMetadataProvider> _otherProviders;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival.W80StoreRevival_XamlTypeInfo
{
	// Token: 0x02000017 RID: 23
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlTypeInfoProvider
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public IXamlType GetXamlTypeByType(Type type)
		{
			string typeName;
			IXamlType xamlTypeByName;
			if (this._xamlTypeToStandardName.TryGetValue(type, ref typeName))
			{
				xamlTypeByName = this.GetXamlTypeByName(typeName);
			}
			else
			{
				xamlTypeByName = this.GetXamlTypeByName(type.FullName);
			}
			return xamlTypeByName;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00010D38 File Offset: 0x0000EF38
		public IXamlType GetXamlTypeByName(string typeName)
		{
			IXamlType result;
			IXamlType xamlType;
			if (string.IsNullOrEmpty(typeName))
			{
				result = null;
			}
			else if (this._xamlTypes.TryGetValue(typeName, ref xamlType))
			{
				result = xamlType;
			}
			else
			{
				xamlType = this.CreateXamlType(typeName);
				if (xamlType != null)
				{
					this._xamlTypes.Add(typeName, xamlType);
				}
				result = xamlType;
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00010D98 File Offset: 0x0000EF98
		public IXamlMember GetMemberByLongName(string longMemberName)
		{
			IXamlMember result;
			IXamlMember xamlMember;
			if (string.IsNullOrEmpty(longMemberName))
			{
				result = null;
			}
			else if (this._xamlMembers.TryGetValue(longMemberName, ref xamlMember))
			{
				result = xamlMember;
			}
			else
			{
				xamlMember = this.CreateXamlMember(longMemberName);
				if (xamlMember != null)
				{
					this._xamlMembers.Add(longMemberName, xamlMember);
				}
				result = xamlMember;
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		private void AddToMapOfTypeToStandardName(Type t, string str)
		{
			if (!this._xamlTypeToStandardName.ContainsKey(t))
			{
				this._xamlTypeToStandardName.Add(t, str);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00010E28 File Offset: 0x0000F028
		private object Activate_1_UpdateW8()
		{
			return new UpdateW8();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00010E40 File Offset: 0x0000F040
		private object Activate_2_UpdatePage()
		{
			return new UpdatePage();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00010E58 File Offset: 0x0000F058
		private object Activate_3_CharmsSettingsFlyout()
		{
			return new CharmsSettingsFlyout();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00010E70 File Offset: 0x0000F070
		private object Activate_4_WelcomePage()
		{
			return new WelcomePage();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00010E88 File Offset: 0x0000F088
		private object Activate_5_SettingsPage()
		{
			return new SettingsPage();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		private object Activate_6_AllAppsPage()
		{
			return new AllAppsPage();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		private object Activate_7_AppDetailsPage()
		{
			return new AppDetailsPage();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		private object Activate_8_AboutFlyout()
		{
			return new AboutFlyout();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		private object Activate_9_MainPage()
		{
			return new MainPage();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00010F00 File Offset: 0x0000F100
		private IXamlType CreateXamlType(string typeName)
		{
			XamlSystemBaseType result = null;
			if (typeName != null)
			{
				if (<PrivateImplementationDetails>{A3A91DA2-38F6-4321-9787-FB35B90B83DF}.$$method0x60000ee-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(12);
					dictionary.Add("Windows.UI.Xaml.Controls.Page", 0);
					dictionary.Add("Windows.UI.Xaml.Controls.UserControl", 1);
					dictionary.Add("W80StoreRevival.ExtendedSplash", 2);
					dictionary.Add("W80StoreRevival.UpdateW8", 3);
					dictionary.Add("W80StoreRevival.UpdatePage", 4);
					dictionary.Add("W80StoreRevival.CharmsSettingsFlyout", 5);
					dictionary.Add("W80StoreRevival.WelcomePage", 6);
					dictionary.Add("W80StoreRevival.SettingsPage", 7);
					dictionary.Add("W80StoreRevival.AllAppsPage", 8);
					dictionary.Add("W80StoreRevival.AppDetailsPage", 9);
					dictionary.Add("W80StoreRevival.AboutFlyout", 10);
					dictionary.Add("W80StoreRevival.MainPage", 11);
					<PrivateImplementationDetails>{A3A91DA2-38F6-4321-9787-FB35B90B83DF}.$$method0x60000ee-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{A3A91DA2-38F6-4321-9787-FB35B90B83DF}.$$method0x60000ee-1.TryGetValue(typeName, ref num))
				{
					switch (num)
					{
					case 0:
						result = new XamlSystemBaseType(typeName, typeof(Page));
						break;
					case 1:
						result = new XamlSystemBaseType(typeName, typeof(UserControl));
						break;
					case 2:
					{
						XamlUserType xamlUserType = new XamlUserType(this, typeName, typeof(ExtendedSplash), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
						result = xamlUserType;
						break;
					}
					case 3:
						result = new XamlUserType(this, typeName, typeof(UpdateW8), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"))
						{
							Activator = new Activator(this.Activate_1_UpdateW8)
						};
						break;
					case 4:
						result = new XamlUserType(this, typeName, typeof(UpdatePage), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"))
						{
							Activator = new Activator(this.Activate_2_UpdatePage)
						};
						break;
					case 5:
						result = new XamlUserType(this, typeName, typeof(CharmsSettingsFlyout), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"))
						{
							Activator = new Activator(this.Activate_3_CharmsSettingsFlyout)
						};
						break;
					case 6:
						result = new XamlUserType(this, typeName, typeof(WelcomePage), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"))
						{
							Activator = new Activator(this.Activate_4_WelcomePage)
						};
						break;
					case 7:
						result = new XamlUserType(this, typeName, typeof(SettingsPage), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"))
						{
							Activator = new Activator(this.Activate_5_SettingsPage)
						};
						break;
					case 8:
						result = new XamlUserType(this, typeName, typeof(AllAppsPage), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"))
						{
							Activator = new Activator(this.Activate_6_AllAppsPage)
						};
						break;
					case 9:
						result = new XamlUserType(this, typeName, typeof(AppDetailsPage), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"))
						{
							Activator = new Activator(this.Activate_7_AppDetailsPage)
						};
						break;
					case 10:
						result = new XamlUserType(this, typeName, typeof(AboutFlyout), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"))
						{
							Activator = new Activator(this.Activate_8_AboutFlyout)
						};
						break;
					case 11:
						result = new XamlUserType(this, typeName, typeof(MainPage), this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"))
						{
							Activator = new Activator(this.Activate_9_MainPage)
						};
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00011250 File Offset: 0x0000F450
		private IXamlMember CreateXamlMember(string longMemberName)
		{
			return null;
		}

		// Token: 0x040000C7 RID: 199
		private Dictionary<string, IXamlType> _xamlTypes = new Dictionary<string, IXamlType>();

		// Token: 0x040000C8 RID: 200
		private Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();

		// Token: 0x040000C9 RID: 201
		private Dictionary<Type, string> _xamlTypeToStandardName = new Dictionary<Type, string>();
	}
}

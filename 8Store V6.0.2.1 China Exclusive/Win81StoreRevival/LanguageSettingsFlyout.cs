using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x02000030 RID: 48
	public sealed class LanguageSettingsFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000F200 File Offset: 0x0000D400
		public LanguageSettingsFlyout()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.LanguageSettingsFlyout_Loaded));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000595D File Offset: 0x00003B5D
		private void LanguageSettingsFlyout_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000F250 File Offset: 0x0000D450
		private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			bool flag = this.isinit;
			if (!flag)
			{
				ComboBox comboBox = sender as ComboBox;
				bool flag2 = comboBox == null;
				if (!flag2)
				{
					ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
					bool flag3 = comboBoxItem == null;
					if (!flag3)
					{
						string text = comboBoxItem.Content.ToString();
						this.ApplyLanguage(text);
						ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
						localSettings.Values["AppLanguage"] = text;
					}
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		private void ApplyLanguage(string lang)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(lang);
			if (num <= 1833221645U)
			{
				if (num <= 1161419880U)
				{
					if (num <= 445410746U)
					{
						if (num <= 92775655U)
						{
							if (num != 7891155U)
							{
								if (num == 92775655U)
								{
									if (lang == "हिंदी")
									{
										ApplicationLanguages.put_PrimaryLanguageOverride("hi-IN");
									}
								}
							}
							else if (lang == "русский")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("ru-RU");
							}
						}
						else if (num != 117082474U)
						{
							if (num == 445410746U)
							{
								if (lang == "Vietnamese")
								{
									ApplicationLanguages.put_PrimaryLanguageOverride("vi-VN");
								}
							}
						}
						else if (lang == "(BETA) עִברִית")
						{
							ApplicationLanguages.put_PrimaryLanguageOverride("he-IL");
						}
					}
					else if (num <= 463134907U)
					{
						if (num != 446995721U)
						{
							if (num == 463134907U)
							{
								if (lang == "English")
								{
									ApplicationLanguages.put_PrimaryLanguageOverride("en-US");
								}
							}
						}
						else if (lang == "polski")
						{
							ApplicationLanguages.put_PrimaryLanguageOverride("pl-PL");
						}
					}
					else if (num != 1017912785U)
					{
						if (num != 1157784315U)
						{
							if (num == 1161419880U)
							{
								if (lang == "繁體中文")
								{
									ApplicationLanguages.put_PrimaryLanguageOverride("zh-HK");
								}
							}
						}
						else if (lang == "eesti")
						{
							ApplicationLanguages.put_PrimaryLanguageOverride("et-EE");
						}
					}
					else if (lang == "แบบไทย")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("th-TH");
					}
				}
				else if (num <= 1577919298U)
				{
					if (num <= 1344165236U)
					{
						if (num != 1266556089U)
						{
							if (num == 1344165236U)
							{
								if (lang == "čeština")
								{
									ApplicationLanguages.put_PrimaryLanguageOverride("cs-CZ");
								}
							}
						}
						else if (lang == "bahasa Indonesia")
						{
							ApplicationLanguages.put_PrimaryLanguageOverride("id-ID");
						}
					}
					else if (num != 1409693518U)
					{
						if (num == 1577919298U)
						{
							if (lang == "português (brasil)")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("pt-BR");
							}
						}
					}
					else if (lang == "日本語")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("ja-JP");
					}
				}
				else if (num <= 1631459727U)
				{
					if (num != 1607481230U)
					{
						if (num == 1631459727U)
						{
							if (lang == "suomalainen")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("fi-FI");
							}
						}
					}
					else if (lang == "한국인")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("ko-KR");
					}
				}
				else if (num != 1821207258U)
				{
					if (num != 1833040324U)
					{
						if (num == 1833221645U)
						{
							if (lang == "española")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("es-ES");
							}
						}
					}
					else if (lang == "简体中文")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("zh-CN");
					}
				}
				else if (lang == "український")
				{
					ApplicationLanguages.put_PrimaryLanguageOverride("ua-UK");
				}
			}
			else if (num <= 3149044709U)
			{
				if (num <= 2367506747U)
				{
					if (num <= 1955131248U)
					{
						if (num != 1856371212U)
						{
							if (num == 1955131248U)
							{
								if (lang == "magyar")
								{
									ApplicationLanguages.put_PrimaryLanguageOverride("hu-HU");
								}
							}
						}
						else if (lang == "Italiano")
						{
							ApplicationLanguages.put_PrimaryLanguageOverride("it-IT");
						}
					}
					else if (num != 2336139382U)
					{
						if (num == 2367506747U)
						{
							if (lang == "(Beta) العرب")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("ar-SA");
							}
						}
					}
					else if (lang == "românească")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("ro-RO");
					}
				}
				else if (num <= 2535575693U)
				{
					if (num != 2402402807U)
					{
						if (num == 2535575693U)
						{
							if (lang == "svensk")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("sv-SE");
							}
						}
					}
					else if (lang == "ελληνικά")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("el-GR");
					}
				}
				else if (num != 2624245447U)
				{
					if (num != 2837995446U)
					{
						if (num == 3149044709U)
						{
							if (lang == "pilipino")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("fil-PH");
							}
						}
					}
					else if (lang == "français")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("fr-FR");
					}
				}
				else if (lang == "hrvatski")
				{
					ApplicationLanguages.put_PrimaryLanguageOverride("hr-HR");
				}
			}
			else if (num <= 3399016062U)
			{
				if (num <= 3247888583U)
				{
					if (num != 3203250941U)
					{
						if (num == 3247888583U)
						{
							if (lang == "хърватски")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("bg-BG");
							}
						}
					}
					else if (lang == "íslenska")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("is-IS");
					}
				}
				else if (num != 3248850370U)
				{
					if (num != 3329398512U)
					{
						if (num == 3399016062U)
						{
							if (lang == "Türkçe")
							{
								ApplicationLanguages.put_PrimaryLanguageOverride("tr-TR");
							}
						}
					}
					else if (lang == "slovenščina")
					{
						ApplicationLanguages.put_PrimaryLanguageOverride("sl-SL");
					}
				}
				else if (lang == "српски")
				{
					ApplicationLanguages.put_PrimaryLanguageOverride("sr-Latn");
				}
			}
			else if (num <= 3764007119U)
			{
				if (num != 3728477982U)
				{
					if (num == 3764007119U)
					{
						if (lang == "Slovák")
						{
							ApplicationLanguages.put_PrimaryLanguageOverride("sk-SK");
						}
					}
				}
				else if (lang == "dansk")
				{
					ApplicationLanguages.put_PrimaryLanguageOverride("da-DK");
				}
			}
			else if (num != 4095678947U)
			{
				if (num != 4125879278U)
				{
					if (num == 4187177300U)
					{
						if (lang == "Latviešu")
						{
							ApplicationLanguages.put_PrimaryLanguageOverride("lv-LV");
						}
					}
				}
				else if (lang == "norsk")
				{
					ApplicationLanguages.put_PrimaryLanguageOverride("nb-NO");
				}
			}
			else if (lang == "Deutsch")
			{
				ApplicationLanguages.put_PrimaryLanguageOverride("de-DE");
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		private void LanguageComboBox_Loaded(object sender, RoutedEventArgs e)
		{
			this.isinit = true;
			ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
			object obj;
			bool flag = localSettings.Values.TryGetValue("AppLanguage", ref obj);
			if (flag)
			{
				foreach (object obj2 in this.LanguageComboBox.Items)
				{
					ComboBoxItem comboBoxItem = (ComboBoxItem)obj2;
					bool flag2 = (string)comboBoxItem.Content == (string)obj;
					if (flag2)
					{
						this.LanguageComboBox.put_SelectedItem(comboBoxItem);
						break;
					}
				}
			}
			this.isinit = false;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000FB64 File Offset: 0x0000DD64
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///LanguageSettingsFlyout.xaml"), 0);
				this.LanguageComboBox = (ComboBox)base.FindName("LanguageComboBox");
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.LanguageComboBox_Loaded));
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.LanguageComboBox_SelectionChanged));
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000147 RID: 327
		private const string LanguageSettingsKey = "AppLanguage";

		// Token: 0x04000148 RID: 328
		private bool isinit = true;

		// Token: 0x04000149 RID: 329
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ComboBox LanguageComboBox;

		// Token: 0x0400014A RID: 330
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

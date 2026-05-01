using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Win81StoreRevival.Languages;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x02000034 RID: 52
	public sealed class LanguageSettingsFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000FD11 File Offset: 0x0000DF11
		public LanguageSettingsFlyout()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.LanguageSettingsFlyout_Loaded));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00004901 File Offset: 0x00002B01
		private void LanguageSettingsFlyout_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000FD54 File Offset: 0x0000DF54
		private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.isinit)
			{
				return;
			}
			LangItem langItem = this.LanguageComboBox.SelectedItem as LangItem;
			if (langItem == null)
			{
				return;
			}
			string tag = langItem.Tag;
			this.ApplyLanguage(tag);
			ApplicationData.Current.LocalSettings.Values["AppLanguage"] = tag;
			this.UpdLang();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		private void UpdLang()
		{
			string primaryLanguageOverride = ApplicationLanguages.PrimaryLanguageOverride;
			Frame frame = Window.Current.Content as Frame;
			if (frame != null)
			{
				FlowDirection flowDirection;
				frame.put_FlowDirection(flowDirection = ((primaryLanguageOverride.StartsWith("ar") || primaryLanguageOverride.StartsWith("he")) ? 1 : 0));
				base.put_FlowDirection(flowDirection);
				ResourceContext.GetForCurrentView().put_Languages(new string[]
				{
					primaryLanguageOverride
				});
				ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
				this.flyoutHeader.put_Title(forCurrentView.GetString("l/Title"));
				this.Lang1.put_Text(forCurrentView.GetString("Lang1/Text"));
				this.Lang2.put_Text(forCurrentView.GetString("Lang2/Text"));
				if (frame.Content != null)
				{
					Type type = frame.Content.GetType();
					frame.Navigate(type);
					frame.BackStack.RemoveAt(frame.BackStack.Count - 1);
				}
			}
			base.put_FlowDirection(frame.FlowDirection);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		private void ApplyLanguage(string lang)
		{
			ApplicationLanguages.put_PrimaryLanguageOverride(lang);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		private void LanguageComboBox_Loaded(object sender, RoutedEventArgs e)
		{
			this.isinit = true;
			this.LanguageComboBox.put_ItemsSource(Data.Languages);
			this.LanguageComboBox.put_DisplayMemberPath("Name");
			ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
			string saved = localSettings.Values.ContainsKey("AppLanguage") ? localSettings.Values["AppLanguage"].ToString() : "en-US";
			LangItem langItem = Enumerable.FirstOrDefault<LangItem>(Data.Languages, (LangItem x) => x.Tag == saved);
			if (langItem == null)
			{
				langItem = Data.Languages[0];
			}
			this.LanguageComboBox.put_SelectedItem(langItem);
			this.isinit = false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000FF60 File Offset: 0x0000E160
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///LanguageSettingsFlyout.xaml"), 0);
			this.flyoutHeader = (SettingsFlyout)base.FindName("flyoutHeader");
			this.Lang1 = (TextBlock)base.FindName("Lang1");
			this.LanguageComboBox = (ComboBox)base.FindName("LanguageComboBox");
			this.Lang2 = (TextBlock)base.FindName("Lang2");
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
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

		// Token: 0x04000156 RID: 342
		private const string LanguageSettingsKey = "AppLanguage";

		// Token: 0x04000157 RID: 343
		private bool isinit = true;

		// Token: 0x04000158 RID: 344
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private SettingsFlyout flyoutHeader;

		// Token: 0x04000159 RID: 345
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Lang1;

		// Token: 0x0400015A RID: 346
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ComboBox LanguageComboBox;

		// Token: 0x0400015B RID: 347
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Lang2;

		// Token: 0x0400015C RID: 348
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

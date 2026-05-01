using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Win81StoreRevival.Themes;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Win81StoreRevival
{
	// Token: 0x02000027 RID: 39
	public sealed class CosmeticFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public CosmeticFlyout()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.CosmeticFlyout_Loaded));
			this.LoadSettings();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CDA6 File Offset: 0x0000AFA6
		private void LoadSettings()
		{
			if (this._settings.Values.ContainsKey("DarkModeEnabled"))
			{
				this.DarkModeSwitch.put_IsOn((bool)this._settings.Values["DarkModeEnabled"]);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		private void DarkModeSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			if (!this._isInitialized)
			{
				return;
			}
			this._settings.Values["DarkModeEnabled"] = this.DarkModeSwitch.IsOn;
			Control.Current.ApplyTheme();
			this._themeWasApplied = true;
			this.UpdateFlyoutBackground();
			this.RefreshFlyoutAndCurrentPage();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CE3C File Offset: 0x0000B03C
		private void CosmeticFlyout_Loaded(object sender, RoutedEventArgs e)
		{
			this._isInitialized = false;
			try
			{
				if (this._settings.Values.ContainsKey("DarkModeEnabled"))
				{
					object obj = this._settings.Values["DarkModeEnabled"];
					this.DarkModeSwitch.put_IsOn(obj is bool && (bool)obj);
				}
				if (this._settings.Values.ContainsKey("UseClassicTitle"))
				{
					object obj2 = this._settings.Values["UseClassicTitle"];
					this.TitleToggle.put_IsOn(obj2 is bool && (bool)obj2);
				}
				if (this._settings.Values.ContainsKey("UseClassicTitleNavbar"))
				{
					object obj3 = this._settings.Values["UseClassicTitleNavbar"];
					this.Title1Toggle.put_IsOn(obj3 is bool && (bool)obj3);
				}
				string text = "#00AA4F";
				object obj4;
				if (this._settings.Values.TryGetValue("AccentColor", ref obj4))
				{
					string text2 = obj4 as string;
					if (!string.IsNullOrWhiteSpace(text2))
					{
						text = text2;
					}
				}
				SolidColorBrush brush = new SolidColorBrush(this.ParseHex(text));
				if (this.ColorPickerGrid != null)
				{
					foreach (object obj5 in this.ColorPickerGrid.Items)
					{
						Border border = obj5 as Border;
						if (border != null && border.Tag != null)
						{
							bool flag = border.Tag.ToString().Equals(text, 5);
							border.put_BorderBrush(new SolidColorBrush(flag ? Colors.White : Colors.Transparent));
						}
					}
				}
				Window.Current.Dispatcher.RunAsync(0, delegate()
				{
					if (Control.Current != null)
					{
						try
						{
							Control.Current.Accent = brush;
							this.put_HeaderBackground(brush);
						}
						catch
						{
						}
					}
					this.UpdateFlyoutBackground();
					if (this._themeWasApplied)
					{
						this._themeWasApplied = false;
					}
				});
			}
			catch (Exception)
			{
			}
			this._isInitialized = true;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D060 File Offset: 0x0000B260
		private void ColorPickerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Border border = (e.AddedItems.Count > 0) ? (e.AddedItems[0] as Border) : null;
			if (border == null)
			{
				return;
			}
			string text = border.Tag.ToString();
			SolidColorBrush solidColorBrush = new SolidColorBrush(this.ParseHex(text));
			this._settings.Values["AccentColor"] = text;
			Control.Current.Accent = solidColorBrush;
			base.put_HeaderBackground(solidColorBrush);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		private void TitleToggle_Toggled(object sender, RoutedEventArgs e)
		{
			this._settings.Values["UseClassicTitle"] = this.TitleToggle.IsOn;
			Frame frame = Window.Current.Content as Frame;
			if (frame != null)
			{
				MainPage mainPage = frame.Content as MainPage;
				if (mainPage != null)
				{
					mainPage.UpdateTitle();
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000D134 File Offset: 0x0000B334
		private void Title1Toggle_Toggled(object sender, RoutedEventArgs e)
		{
			this._settings.Values["UseClassicTitleNavbar"] = this.Title1Toggle.IsOn;
			Frame frame = Window.Current.Content as Frame;
			if (frame != null)
			{
				MainPage mainPage = frame.Content as MainPage;
				if (mainPage != null)
				{
					mainPage.UpdateTitleNB();
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000D190 File Offset: 0x0000B390
		private void RefreshFlyoutAndCurrentPage()
		{
			try
			{
				FrameworkElement frameworkElement = base.Content as FrameworkElement;
				if (frameworkElement != null)
				{
					frameworkElement.put_RequestedTheme(this.DarkModeSwitch.IsOn ? 2 : 1);
				}
			}
			catch
			{
			}
			Frame frame = Window.Current.Content as Frame;
			if (frame == null || frame.Content == null)
			{
				return;
			}
			Type type = frame.Content.GetType();
			frame.Navigate(type);
			if (frame.BackStack != null && frame.BackStack.Count > 0)
			{
				frame.BackStack.RemoveAt(frame.BackStack.Count - 1);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000D238 File Offset: 0x0000B438
		private void UpdateFlyoutBackground()
		{
			try
			{
				BitmapImage bitmapImage = Application.Current.Resources["FlyoutBackground"] as BitmapImage;
				if (bitmapImage != null)
				{
					ImageBrush imageBrush = new ImageBrush();
					imageBrush.put_ImageSource(bitmapImage);
					imageBrush.put_Stretch(3);
					base.put_Background(imageBrush);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D290 File Offset: 0x0000B490
		private Color ParseHex(string hex)
		{
			hex = hex.Replace("#", "");
			byte b = Convert.ToByte(hex.Substring(0, 2), 16);
			byte b2 = Convert.ToByte(hex.Substring(2, 2), 16);
			byte b3 = Convert.ToByte(hex.Substring(4, 2), 16);
			return Color.FromArgb(byte.MaxValue, b, b2, b3);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///CosmeticFlyout.xaml"), 0);
			this.DarkModeSwitch = (ToggleSwitch)base.FindName("DarkModeSwitch");
			this.TitleToggle = (ToggleSwitch)base.FindName("TitleToggle");
			this.Title1Toggle = (ToggleSwitch)base.FindName("Title1Toggle");
			this.ColorPickerGrid = (GridView)base.FindName("ColorPickerGrid");
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D374 File Offset: 0x0000B574
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.DarkModeSwitch_Toggled));
				break;
			}
			case 2:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.TitleToggle_Toggled));
				break;
			}
			case 3:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.Title1Toggle_Toggled));
				break;
			}
			case 4:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ColorPickerGrid_SelectionChanged));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000F7 RID: 247
		private ApplicationDataContainer _settings = ApplicationData.Current.LocalSettings;

		// Token: 0x040000F8 RID: 248
		private bool _isInitialized;

		// Token: 0x040000F9 RID: 249
		private bool _themeWasApplied;

		// Token: 0x040000FA RID: 250
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch DarkModeSwitch;

		// Token: 0x040000FB RID: 251
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch TitleToggle;

		// Token: 0x040000FC RID: 252
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch Title1Toggle;

		// Token: 0x040000FD RID: 253
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private GridView ColorPickerGrid;

		// Token: 0x040000FE RID: 254
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

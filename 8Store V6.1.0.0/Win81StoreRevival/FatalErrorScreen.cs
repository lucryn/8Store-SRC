using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Win81StoreRevival.Common;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200002C RID: 44
	public sealed class FatalErrorScreen : Page, IComponentConnector
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000E6EF File Offset: 0x0000C8EF
		public ObservableDictionary DefaultViewModel
		{
			get
			{
				return this.defaultViewModel;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000E6F7 File Offset: 0x0000C8F7
		public NavigationHelper NavigationHelper
		{
			get
			{
				return this.navigationHelper;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000E700 File Offset: 0x0000C900
		public FatalErrorScreen()
		{
			this.InitializeComponent();
			this._retryCount = 0;
			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += this.navigationHelper_LoadState;
			this.navigationHelper.SaveState += this.navigationHelper_SaveState;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00004901 File Offset: 0x00002B01
		private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00004901 File Offset: 0x00002B01
		private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E768 File Offset: 0x0000C968
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			this.navigationHelper.OnNavigatedTo(e);
			if (e.Parameter != null)
			{
				this.errorcode = e.Parameter.ToString();
			}
			this.UpdLocalization();
			this.BuildOfflineText();
			bool flag = this.CheckOfflineCache();
			this.offlineModeSect.put_Visibility(flag ? 0 : 1);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E7BF File Offset: 0x0000C9BF
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			this.navigationHelper.OnNavigatedFrom(e);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E7CD File Offset: 0x0000C9CD
		private void backButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Exit();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		private void Retry_Click(object sender, RoutedEventArgs e)
		{
			FatalErrorScreen.<Retry_Click>d__14 <Retry_Click>d__;
			<Retry_Click>d__.<>4__this = this;
			<Retry_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Retry_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Retry_Click>d__.<>t__builder;
			<>t__builder.Start<FatalErrorScreen.<Retry_Click>d__14>(ref <Retry_Click>d__);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E818 File Offset: 0x0000CA18
		private void UpdLocalization()
		{
			ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
			string @string = forCurrentView.GetString("cantconnect");
			string string2 = forCurrentView.GetString("cantconnectBtn");
			this.ErrorTextBlock.Inlines.Clear();
			int num3;
			for (int i = 0; i < @string.Length; i = num3 + 3)
			{
				int num = @string.IndexOf("{0}", i);
				int num2 = @string.IndexOf("{1}", i);
				num3 = -1;
				int num4 = -1;
				if (num != -1 && (num2 == -1 || num < num2))
				{
					num3 = num;
					num4 = 0;
				}
				else if (num2 != -1)
				{
					num3 = num2;
					num4 = 1;
				}
				if (num3 == -1)
				{
					ICollection<Inline> inlines = this.ErrorTextBlock.Inlines;
					Run run = new Run();
					run.put_Text(@string.Substring(i));
					inlines.Add(run);
					break;
				}
				if (num3 > i)
				{
					ICollection<Inline> inlines2 = this.ErrorTextBlock.Inlines;
					Run run2 = new Run();
					run2.put_Text(@string.Substring(i, num3 - i));
					inlines2.Add(run2);
				}
				if (num4 == 0)
				{
					Hyperlink hyperlink = new Hyperlink();
					Hyperlink hyperlink2 = hyperlink;
					WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>>(new Func<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>, EventRegistrationToken>(hyperlink2.add_Click), new Action<EventRegistrationToken>(hyperlink2.remove_Click), new TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>(this.Retry_Click));
					ICollection<Inline> inlines3 = hyperlink.Inlines;
					Run run3 = new Run();
					run3.put_Text(string2);
					inlines3.Add(run3);
					this.ErrorTextBlock.Inlines.Add(hyperlink);
				}
			}
			if (!string.IsNullOrEmpty(this.errorcode))
			{
				ICollection<Inline> inlines4 = this.ErrorTextBlock.Inlines;
				Run run4 = new Run();
				run4.put_Text(" (" + this.errorcode + ")");
				run4.put_FontSize(18.0);
				run4.put_Foreground(new SolidColorBrush(Colors.Gray));
				inlines4.Add(run4);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		private void BuildOfflineText()
		{
			ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
			string @string = forCurrentView.GetString("offlineModeTxt");
			string string2 = forCurrentView.GetString("offlineModeBtn");
			this.offlineModeSect.Inlines.Clear();
			if (@string.Contains("{0}"))
			{
				string[] array = @string.Split(new string[]
				{
					"{0}"
				}, 0);
				ICollection<Inline> inlines = this.offlineModeSect.Inlines;
				Run run = new Run();
				run.put_Text(array[0]);
				inlines.Add(run);
				Hyperlink hyperlink = new Hyperlink();
				Hyperlink hyperlink2 = hyperlink;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>>(new Func<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>, EventRegistrationToken>(hyperlink2.add_Click), new Action<EventRegistrationToken>(hyperlink2.remove_Click), new TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>(this.OfflineMode_Click));
				ICollection<Inline> inlines2 = hyperlink.Inlines;
				Run run2 = new Run();
				run2.put_Text(string2);
				inlines2.Add(run2);
				this.offlineModeSect.Inlines.Add(hyperlink);
				if (array.Length > 1)
				{
					ICollection<Inline> inlines3 = this.offlineModeSect.Inlines;
					Run run3 = new Run();
					run3.put_Text(array[1]);
					inlines3.Add(run3);
					return;
				}
			}
			else
			{
				ICollection<Inline> inlines4 = this.offlineModeSect.Inlines;
				Run run4 = new Run();
				run4.put_Text(@string);
				inlines4.Add(run4);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		private void OfflineMode_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage), "offline");
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000EB04 File Offset: 0x0000CD04
		private bool CheckOfflineCache()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				if (localSettings.Values.ContainsKey("IsCacheValid"))
				{
					object obj = localSettings.Values["IsCacheValid"];
					if (obj is bool && (bool)obj)
					{
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///FatalErrorScreen.xaml"), 0);
			this.pageRoot = (Page)base.FindName("pageRoot");
			this.ContentGrid = (Grid)base.FindName("ContentGrid");
			this.ErrorTextPanel = (StackPanel)base.FindName("ErrorTextPanel");
			this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
			this.offlineModeSect = (TextBlock)base.FindName("offlineModeSect");
			this.ErrorTextBlock = (TextBlock)base.FindName("ErrorTextBlock");
			this.cantconnectTxT = (Run)base.FindName("cantconnectTxT");
			this.backButton = (Button)base.FindName("backButton");
			this.pageTitle = (TextBlock)base.FindName("pageTitle");
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000EC60 File Offset: 0x0000CE60
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000124 RID: 292
		private NavigationHelper navigationHelper;

		// Token: 0x04000125 RID: 293
		private ObservableDictionary defaultViewModel = new ObservableDictionary();

		// Token: 0x04000126 RID: 294
		private int _retryCount;

		// Token: 0x04000127 RID: 295
		private string errorcode;

		// Token: 0x04000128 RID: 296
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x04000129 RID: 297
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid ContentGrid;

		// Token: 0x0400012A RID: 298
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel ErrorTextPanel;

		// Token: 0x0400012B RID: 299
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x0400012C RID: 300
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock offlineModeSect;

		// Token: 0x0400012D RID: 301
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ErrorTextBlock;

		// Token: 0x0400012E RID: 302
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Run cantconnectTxT;

		// Token: 0x0400012F RID: 303
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x04000130 RID: 304
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock pageTitle;

		// Token: 0x04000131 RID: 305
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

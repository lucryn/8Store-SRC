using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using Win81StoreRevival.Config;
using Win81StoreRevival.Languages;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x0200000D RID: 13
	public sealed class AboutSettingsFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002E3C File Offset: 0x0000103C
		public AboutSettingsFlyout()
		{
			this.InitializeComponent();
			this.TranslationsList.put_ItemsSource(Enumerable.ToList<LangItem>(Enumerable.Where<LangItem>(Data.Languages, (LangItem l) => l.Tag != "en-US" && !string.IsNullOrWhiteSpace(l.Authors))));
			this.SetVersionText();
			ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
			bool isDebugBuild = Config.IsDebugBuild;
			bool isPreviewBuild = Config.IsPreviewBuild;
			this.IndevversionTextBlock.put_Visibility((isDebugBuild || isPreviewBuild) ? 0 : 1);
			if (this.IndevversionTextBlock.Visibility == null)
			{
				if (isDebugBuild && isPreviewBuild)
				{
					string @string = forCurrentView.GetString("DevelopmentVersionInfo/Text");
					string string2 = forCurrentView.GetString("PreviewVersionInfo/Text");
					this.IndevversionTextBlock.put_Text(string.Format("{0}\n\n{1}", new object[]
					{
						string2,
						@string
					}));
					return;
				}
				string text = isPreviewBuild ? "PreviewVersionInfo/Text" : "DevelopmentVersionInfo/Text";
				this.IndevversionTextBlock.put_Text(forCurrentView.GetString(text));
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F30 File Offset: 0x00001130
		private void SetVersionText()
		{
			string version = Config.version;
			string text = string.Format("({0})", new object[]
			{
				"2601PUB_DU4"
			});
			this.VersionTextBlock.put_Text(string.Format("Build {0} {1}", new object[]
			{
				version,
				text
			}));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002F80 File Offset: 0x00001180
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///AboutSettingsFlyout.xaml"), 0);
			this.VersionTextBlock = (TextBlock)base.FindName("VersionTextBlock");
			this.IndevversionTextBlock = (TextBlock)base.FindName("IndevversionTextBlock");
			this.TranslationsList = (ItemsControl)base.FindName("TranslationsList");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FF0 File Offset: 0x000011F0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000018 RID: 24
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock VersionTextBlock;

		// Token: 0x04000019 RID: 25
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock IndevversionTextBlock;

		// Token: 0x0400001A RID: 26
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ItemsControl TranslationsList;

		// Token: 0x0400001B RID: 27
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

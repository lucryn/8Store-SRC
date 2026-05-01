using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Win81StoreRevival
{
	// Token: 0x02000019 RID: 25
	public sealed class AddRating : SettingsFlyout, IComponentConnector
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005041 File Offset: 0x00003241
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005049 File Offset: 0x00003249
		public string AppId { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005052 File Offset: 0x00003252
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000505A File Offset: 0x0000325A
		public ReviewStats LastRatings { get; set; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600011F RID: 287 RVA: 0x00005064 File Offset: 0x00003264
		// (remove) Token: 0x06000120 RID: 288 RVA: 0x0000509C File Offset: 0x0000329C
		public event EventHandler ReviewSubmitted;

		// Token: 0x06000121 RID: 289 RVA: 0x000050D1 File Offset: 0x000032D1
		public AddRating()
		{
			this.InitializeComponent();
			this.CheckAuthorization();
			this.UpdateWordCount();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000050F4 File Offset: 0x000032F4
		private void CheckAuthorization()
		{
			bool flag = SupabaseService.GetCurrentUser() != null;
			this.SubmitButton.put_IsEnabled(flag);
			if (!flag)
			{
				this.CommentTextBox.put_IsEnabled(false);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005128 File Offset: 0x00003328
		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			AddRating.<SubmitButton_Click>d__14 <SubmitButton_Click>d__;
			<SubmitButton_Click>d__.<>4__this = this;
			<SubmitButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SubmitButton_Click>d__.<>t__builder;
			<>t__builder.Start<AddRating.<SubmitButton_Click>d__14>(ref <SubmitButton_Click>d__);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005164 File Offset: 0x00003364
		private void StarsContainer_Tapped(object sender, TappedRoutedEventArgs e)
		{
			try
			{
				Point position = e.GetPosition(this.StarsContainer);
				double num = this.StarsContainer.ActualWidth;
				if (num <= 0.0)
				{
					num = 209.0;
				}
				int num2 = (int)Math.Ceiling(position.X / num * 5.0);
				if (num2 < 1)
				{
					num2 = 1;
				}
				if (num2 > 5)
				{
					num2 = 5;
				}
				this.selectedRating = num2;
				double starClipWidth = AddRating.GetStarClipWidth(this.selectedRating);
				this.StarsClip.put_Rect(new Rect(0.0, 0.0, starClipWidth, 100.0));
			}
			catch
			{
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000521C File Offset: 0x0000341C
		private static double GetStarClipWidth(int rating)
		{
			switch (rating)
			{
			case 1:
				return 41.0;
			case 2:
				return 83.0;
			case 3:
				return 125.0;
			case 4:
				return 167.0;
			case 5:
				return 209.0;
			default:
				return 0.0;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005282 File Offset: 0x00003482
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000528A File Offset: 0x0000348A
		private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.UpdateWordCount();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005294 File Offset: 0x00003494
		private void UpdateWordCount()
		{
			if (this.ReviewWordCountText == null)
			{
				return;
			}
			TextBox commentTextBox = this.CommentTextBox;
			int length = (((commentTextBox != null) ? commentTextBox.Text : null) ?? "").Length;
			this.ReviewWordCountText.put_Text(string.Format("{0}/1,000", new object[]
			{
				length
			}));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000052F0 File Offset: 0x000034F0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///AddRating.xaml"), 0);
			this.AuthWarning = (TextBlock)base.FindName("AuthWarning");
			this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
			this.SubmitButton = (Button)base.FindName("SubmitButton");
			this.CancelButton = (Button)base.FindName("CancelButton");
			this.CommentTextBox = (TextBox)base.FindName("CommentTextBox");
			this.ReviewWordCountText = (TextBlock)base.FindName("ReviewWordCountText");
			this.StarsContainer = (Grid)base.FindName("StarsContainer");
			this.EmptyStarsLayer = (TextBlock)base.FindName("EmptyStarsLayer");
			this.GreenStarsLayer = (TextBlock)base.FindName("GreenStarsLayer");
			this.StarsClip = (RectangleGeometry)base.FindName("StarsClip");
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000053FC File Offset: 0x000035FC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SubmitButton_Click));
				break;
			}
			case 2:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelButton_Click));
				break;
			}
			case 3:
			{
				TextBox textBox = (TextBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(textBox.add_TextChanged), new Action<EventRegistrationToken>(textBox.remove_TextChanged), new TextChangedEventHandler(this.CommentTextBox_TextChanged));
				break;
			}
			case 4:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StarsContainer_Tapped));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400005F RID: 95
		private int selectedRating = 5;

		// Token: 0x04000060 RID: 96
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock AuthWarning;

		// Token: 0x04000061 RID: 97
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000062 RID: 98
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button SubmitButton;

		// Token: 0x04000063 RID: 99
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CancelButton;

		// Token: 0x04000064 RID: 100
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox CommentTextBox;

		// Token: 0x04000065 RID: 101
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ReviewWordCountText;

		// Token: 0x04000066 RID: 102
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid StarsContainer;

		// Token: 0x04000067 RID: 103
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock EmptyStarsLayer;

		// Token: 0x04000068 RID: 104
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock GreenStarsLayer;

		// Token: 0x04000069 RID: 105
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private RectangleGeometry StarsClip;

		// Token: 0x0400006A RID: 106
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

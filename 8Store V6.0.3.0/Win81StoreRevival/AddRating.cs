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
	// Token: 0x02000016 RID: 22
	public sealed class AddRating : SettingsFlyout, IComponentConnector
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004800 File Offset: 0x00002A00
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004808 File Offset: 0x00002A08
		public string AppId { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004811 File Offset: 0x00002A11
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004819 File Offset: 0x00002A19
		public RatingSummary LastRatings { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000E6 RID: 230 RVA: 0x00004824 File Offset: 0x00002A24
		// (remove) Token: 0x060000E7 RID: 231 RVA: 0x0000485C File Offset: 0x00002A5C
		[DebuggerBrowsable(0)]
		public event EventHandler ReviewSubmitted;

		// Token: 0x060000E8 RID: 232 RVA: 0x00004891 File Offset: 0x00002A91
		public AddRating()
		{
			this.InitializeComponent();
			this.CheckAuthorization();
			this.UpdateWordCount();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000048B8 File Offset: 0x00002AB8
		private void CheckAuthorization()
		{
			UserAccount currentUser = SupabaseService.GetCurrentUser();
			bool flag = currentUser != null;
			this.SubmitButton.put_IsEnabled(flag);
			bool flag2 = !flag;
			if (flag2)
			{
				this.CommentTextBox.put_IsEnabled(false);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000048F8 File Offset: 0x00002AF8
		[DebuggerStepThrough]
		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			AddRating.<SubmitButton_Click>d__14 <SubmitButton_Click>d__ = new AddRating.<SubmitButton_Click>d__14();
			<SubmitButton_Click>d__.<>4__this = this;
			<SubmitButton_Click>d__.sender = sender;
			<SubmitButton_Click>d__.e = e;
			<SubmitButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SubmitButton_Click>d__.<>t__builder;
			<>t__builder.Start<AddRating.<SubmitButton_Click>d__14>(ref <SubmitButton_Click>d__);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004944 File Offset: 0x00002B44
		private void StarsContainer_Tapped(object sender, TappedRoutedEventArgs e)
		{
			try
			{
				Point position = e.GetPosition(this.StarsContainer);
				double num = this.StarsContainer.ActualWidth;
				bool flag = num <= 0.0;
				if (flag)
				{
					num = 209.0;
				}
				double num2 = position.X / num;
				int num3 = (int)Math.Ceiling(num2 * 5.0);
				bool flag2 = num3 < 1;
				if (flag2)
				{
					num3 = 1;
				}
				bool flag3 = num3 > 5;
				if (flag3)
				{
					num3 = 5;
				}
				this.selectedRating = num3;
				double starClipWidth = AddRating.GetStarClipWidth(this.selectedRating);
				this.StarsClip.put_Rect(new Rect(0.0, 0.0, starClipWidth, 100.0));
			}
			catch
			{
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004A1C File Offset: 0x00002C1C
		private static double GetStarClipWidth(int rating)
		{
			double result;
			switch (rating)
			{
			case 1:
				result = 41.0;
				break;
			case 2:
				result = 83.0;
				break;
			case 3:
				result = 125.0;
				break;
			case 4:
				result = 167.0;
				break;
			case 5:
				result = 209.0;
				break;
			default:
				result = 0.0;
				break;
			}
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004A93 File Offset: 0x00002C93
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004A9D File Offset: 0x00002C9D
		private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.UpdateWordCount();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004AA8 File Offset: 0x00002CA8
		private void UpdateWordCount()
		{
			bool flag = this.ReviewWordCountText == null;
			if (!flag)
			{
				TextBox commentTextBox = this.CommentTextBox;
				string text = ((commentTextBox != null) ? commentTextBox.Text : null) ?? "";
				int length = text.Length;
				this.ReviewWordCountText.put_Text(string.Format("{0}/1,000", new object[]
				{
					length
				}));
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004B10 File Offset: 0x00002D10
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
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
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004C24 File Offset: 0x00002E24
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

		// Token: 0x04000063 RID: 99
		private int selectedRating = 5;

		// Token: 0x04000064 RID: 100
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock AuthWarning;

		// Token: 0x04000065 RID: 101
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000066 RID: 102
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button SubmitButton;

		// Token: 0x04000067 RID: 103
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CancelButton;

		// Token: 0x04000068 RID: 104
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox CommentTextBox;

		// Token: 0x04000069 RID: 105
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ReviewWordCountText;

		// Token: 0x0400006A RID: 106
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid StarsContainer;

		// Token: 0x0400006B RID: 107
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock EmptyStarsLayer;

		// Token: 0x0400006C RID: 108
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock GreenStarsLayer;

		// Token: 0x0400006D RID: 109
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private RectangleGeometry StarsClip;

		// Token: 0x0400006E RID: 110
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

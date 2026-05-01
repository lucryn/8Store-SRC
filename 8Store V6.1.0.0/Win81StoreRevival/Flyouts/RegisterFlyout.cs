using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival.Flyouts
{
	// Token: 0x02000055 RID: 85
	public sealed class RegisterFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0001CA04 File Offset: 0x0001AC04
		public RegisterFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001CA14 File Offset: 0x0001AC14
		private void SignUp_Click(object sender, RoutedEventArgs e)
		{
			RegisterFlyout.<SignUp_Click>d__1 <SignUp_Click>d__;
			<SignUp_Click>d__.<>4__this = this;
			<SignUp_Click>d__.sender = sender;
			<SignUp_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SignUp_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SignUp_Click>d__.<>t__builder;
			<>t__builder.Start<RegisterFlyout.<SignUp_Click>d__1>(ref <SignUp_Click>d__);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001CA55 File Offset: 0x0001AC55
		private void SignInLink_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
			new LoginFlyout().Show();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001CA68 File Offset: 0x0001AC68
		private Task UpdateMainPageUI()
		{
			RegisterFlyout.<UpdateMainPageUI>d__3 <UpdateMainPageUI>d__;
			<UpdateMainPageUI>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateMainPageUI>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateMainPageUI>d__.<>t__builder;
			<>t__builder.Start<RegisterFlyout.<UpdateMainPageUI>d__3>(ref <UpdateMainPageUI>d__);
			return <UpdateMainPageUI>d__.<>t__builder.Task;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		private Task ShowMessage(string message, string title)
		{
			RegisterFlyout.<ShowMessage>d__4 <ShowMessage>d__;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<RegisterFlyout.<ShowMessage>d__4>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///RegisterFlyout.xaml"), 0);
			this.EmailBox = (TextBox)base.FindName("EmailBox");
			this.UsernameBox = (TextBox)base.FindName("UsernameBox");
			this.PasswordBox = (PasswordBox)base.FindName("PasswordBox");
			this.ConfirmPasswordBox = (PasswordBox)base.FindName("ConfirmPasswordBox");
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001CB80 File Offset: 0x0001AD80
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				if (connectionId == 2)
				{
					ButtonBase buttonBase = (ButtonBase)target;
					WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SignUp_Click));
				}
			}
			else
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SignInLink_Click));
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400024A RID: 586
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox EmailBox;

		// Token: 0x0400024B RID: 587
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox UsernameBox;

		// Token: 0x0400024C RID: 588
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox PasswordBox;

		// Token: 0x0400024D RID: 589
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox ConfirmPasswordBox;

		// Token: 0x0400024E RID: 590
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

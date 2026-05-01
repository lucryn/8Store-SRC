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
	// Token: 0x0200004F RID: 79
	public sealed class RegisterFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x0001AF69 File Offset: 0x00019169
		public RegisterFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001AF7C File Offset: 0x0001917C
		[DebuggerStepThrough]
		private void SignUp_Click(object sender, RoutedEventArgs e)
		{
			RegisterFlyout.<SignUp_Click>d__1 <SignUp_Click>d__ = new RegisterFlyout.<SignUp_Click>d__1();
			<SignUp_Click>d__.<>4__this = this;
			<SignUp_Click>d__.sender = sender;
			<SignUp_Click>d__.e = e;
			<SignUp_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SignUp_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SignUp_Click>d__.<>t__builder;
			<>t__builder.Start<RegisterFlyout.<SignUp_Click>d__1>(ref <SignUp_Click>d__);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001AFC8 File Offset: 0x000191C8
		private void SignInLink_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
			LoginFlyout loginFlyout = new LoginFlyout();
			loginFlyout.Show();
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001AFEC File Offset: 0x000191EC
		[DebuggerStepThrough]
		private Task UpdateMainPageUI()
		{
			RegisterFlyout.<UpdateMainPageUI>d__3 <UpdateMainPageUI>d__ = new RegisterFlyout.<UpdateMainPageUI>d__3();
			<UpdateMainPageUI>d__.<>4__this = this;
			<UpdateMainPageUI>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateMainPageUI>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateMainPageUI>d__.<>t__builder;
			<>t__builder.Start<RegisterFlyout.<UpdateMainPageUI>d__3>(ref <UpdateMainPageUI>d__);
			return <UpdateMainPageUI>d__.<>t__builder.Task;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001B034 File Offset: 0x00019234
		[DebuggerStepThrough]
		private Task ShowMessage(string message, string title)
		{
			RegisterFlyout.<ShowMessage>d__4 <ShowMessage>d__ = new RegisterFlyout.<ShowMessage>d__4();
			<ShowMessage>d__.<>4__this = this;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<RegisterFlyout.<ShowMessage>d__4>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001B08C File Offset: 0x0001928C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///RegisterFlyout.xaml"), 0);
				this.EmailBox = (TextBox)base.FindName("EmailBox");
				this.UsernameBox = (TextBox)base.FindName("UsernameBox");
				this.PasswordBox = (PasswordBox)base.FindName("PasswordBox");
				this.ConfirmPasswordBox = (PasswordBox)base.FindName("ConfirmPasswordBox");
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001B118 File Offset: 0x00019318
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

		// Token: 0x0400021C RID: 540
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox EmailBox;

		// Token: 0x0400021D RID: 541
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox UsernameBox;

		// Token: 0x0400021E RID: 542
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox PasswordBox;

		// Token: 0x0400021F RID: 543
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox ConfirmPasswordBox;

		// Token: 0x04000220 RID: 544
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

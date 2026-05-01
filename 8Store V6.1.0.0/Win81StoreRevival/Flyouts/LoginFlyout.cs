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
	// Token: 0x02000054 RID: 84
	public sealed class LoginFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x0001C832 File Offset: 0x0001AA32
		public LoginFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001C840 File Offset: 0x0001AA40
		private void SignIn_Click(object sender, RoutedEventArgs e)
		{
			LoginFlyout.<SignIn_Click>d__1 <SignIn_Click>d__;
			<SignIn_Click>d__.<>4__this = this;
			<SignIn_Click>d__.sender = sender;
			<SignIn_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SignIn_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SignIn_Click>d__.<>t__builder;
			<>t__builder.Start<LoginFlyout.<SignIn_Click>d__1>(ref <SignIn_Click>d__);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C881 File Offset: 0x0001AA81
		private void CreateAccount_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
			new RegisterFlyout().Show();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C894 File Offset: 0x0001AA94
		private Task UpdateMainPageUI()
		{
			LoginFlyout.<UpdateMainPageUI>d__3 <UpdateMainPageUI>d__;
			<UpdateMainPageUI>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateMainPageUI>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateMainPageUI>d__.<>t__builder;
			<>t__builder.Start<LoginFlyout.<UpdateMainPageUI>d__3>(ref <UpdateMainPageUI>d__);
			return <UpdateMainPageUI>d__.<>t__builder.Task;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
		private Task ShowMessage(string message, string title)
		{
			LoginFlyout.<ShowMessage>d__4 <ShowMessage>d__;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<LoginFlyout.<ShowMessage>d__4>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001C924 File Offset: 0x0001AB24
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///LoginFlyout.xaml"), 0);
			this.EmailBox = (TextBox)base.FindName("EmailBox");
			this.PasswordBox = (PasswordBox)base.FindName("PasswordBox");
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001C980 File Offset: 0x0001AB80
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				if (connectionId == 2)
				{
					ButtonBase buttonBase = (ButtonBase)target;
					WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SignIn_Click));
				}
			}
			else
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CreateAccount_Click));
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000247 RID: 583
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox EmailBox;

		// Token: 0x04000248 RID: 584
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox PasswordBox;

		// Token: 0x04000249 RID: 585
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

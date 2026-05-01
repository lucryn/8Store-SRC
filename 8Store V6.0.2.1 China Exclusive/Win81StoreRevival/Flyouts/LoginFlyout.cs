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
	// Token: 0x0200004E RID: 78
	public sealed class LoginFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0001AD12 File Offset: 0x00018F12
		public LoginFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001AD24 File Offset: 0x00018F24
		[DebuggerStepThrough]
		private void SignIn_Click(object sender, RoutedEventArgs e)
		{
			LoginFlyout.<SignIn_Click>d__1 <SignIn_Click>d__ = new LoginFlyout.<SignIn_Click>d__1();
			<SignIn_Click>d__.<>4__this = this;
			<SignIn_Click>d__.sender = sender;
			<SignIn_Click>d__.e = e;
			<SignIn_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SignIn_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SignIn_Click>d__.<>t__builder;
			<>t__builder.Start<LoginFlyout.<SignIn_Click>d__1>(ref <SignIn_Click>d__);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001AD70 File Offset: 0x00018F70
		private void CreateAccount_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
			RegisterFlyout registerFlyout = new RegisterFlyout();
			registerFlyout.Show();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001AD94 File Offset: 0x00018F94
		[DebuggerStepThrough]
		private Task UpdateMainPageUI()
		{
			LoginFlyout.<UpdateMainPageUI>d__3 <UpdateMainPageUI>d__ = new LoginFlyout.<UpdateMainPageUI>d__3();
			<UpdateMainPageUI>d__.<>4__this = this;
			<UpdateMainPageUI>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateMainPageUI>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateMainPageUI>d__.<>t__builder;
			<>t__builder.Start<LoginFlyout.<UpdateMainPageUI>d__3>(ref <UpdateMainPageUI>d__);
			return <UpdateMainPageUI>d__.<>t__builder.Task;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001ADDC File Offset: 0x00018FDC
		[DebuggerStepThrough]
		private Task FindAndUpdateMainPageInVisualTree()
		{
			LoginFlyout.<FindAndUpdateMainPageInVisualTree>d__4 <FindAndUpdateMainPageInVisualTree>d__ = new LoginFlyout.<FindAndUpdateMainPageInVisualTree>d__4();
			<FindAndUpdateMainPageInVisualTree>d__.<>4__this = this;
			<FindAndUpdateMainPageInVisualTree>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FindAndUpdateMainPageInVisualTree>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <FindAndUpdateMainPageInVisualTree>d__.<>t__builder;
			<>t__builder.Start<LoginFlyout.<FindAndUpdateMainPageInVisualTree>d__4>(ref <FindAndUpdateMainPageInVisualTree>d__);
			return <FindAndUpdateMainPageInVisualTree>d__.<>t__builder.Task;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001AE24 File Offset: 0x00019024
		[DebuggerStepThrough]
		private Task ShowMessage(string message, string title)
		{
			LoginFlyout.<ShowMessage>d__5 <ShowMessage>d__ = new LoginFlyout.<ShowMessage>d__5();
			<ShowMessage>d__.<>4__this = this;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<LoginFlyout.<ShowMessage>d__5>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001AE7C File Offset: 0x0001907C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///LoginFlyout.xaml"), 0);
				this.EmailBox = (TextBox)base.FindName("EmailBox");
				this.PasswordBox = (PasswordBox)base.FindName("PasswordBox");
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001AEDC File Offset: 0x000190DC
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

		// Token: 0x04000219 RID: 537
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox EmailBox;

		// Token: 0x0400021A RID: 538
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox PasswordBox;

		// Token: 0x0400021B RID: 539
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}

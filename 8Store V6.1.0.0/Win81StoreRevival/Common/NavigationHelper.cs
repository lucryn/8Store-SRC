using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival.Common
{
	// Token: 0x0200005C RID: 92
	[WebHostHidden]
	public class NavigationHelper : DependencyObject
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001CF35 File Offset: 0x0001B135
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001CF3D File Offset: 0x0001B13D
		private Page Page { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001CF46 File Offset: 0x0001B146
		private Frame Frame
		{
			get
			{
				return this.Page.Frame;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001CF54 File Offset: 0x0001B154
		public NavigationHelper(Page page)
		{
			this.Page = page;
			Page page2 = this.Page;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(page2.add_Loaded), new Action<EventRegistrationToken>(page2.remove_Loaded), delegate(object sender, RoutedEventArgs e)
			{
				if (this.Page.ActualHeight == Window.Current.Bounds.Height && this.Page.ActualWidth == Window.Current.Bounds.Width)
				{
					CoreDispatcher dispatcher = Window.Current.CoreWindow.Dispatcher;
					WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<CoreDispatcher, AcceleratorKeyEventArgs>>(new Func<TypedEventHandler<CoreDispatcher, AcceleratorKeyEventArgs>, EventRegistrationToken>(dispatcher.add_AcceleratorKeyActivated), new Action<EventRegistrationToken>(dispatcher.remove_AcceleratorKeyActivated), new TypedEventHandler<CoreDispatcher, AcceleratorKeyEventArgs>(this.CoreDispatcher_AcceleratorKeyActivated));
					CoreWindow coreWindow = Window.Current.CoreWindow;
					WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<CoreWindow, PointerEventArgs>>(new Func<TypedEventHandler<CoreWindow, PointerEventArgs>, EventRegistrationToken>(coreWindow.add_PointerPressed), new Action<EventRegistrationToken>(coreWindow.remove_PointerPressed), new TypedEventHandler<CoreWindow, PointerEventArgs>(this.CoreWindow_PointerPressed));
				}
			});
			page2 = this.Page;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(page2.add_Unloaded), new Action<EventRegistrationToken>(page2.remove_Unloaded), delegate(object sender, RoutedEventArgs e)
			{
				WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<CoreDispatcher, AcceleratorKeyEventArgs>>(new Action<EventRegistrationToken>(Window.Current.CoreWindow.Dispatcher.remove_AcceleratorKeyActivated), new TypedEventHandler<CoreDispatcher, AcceleratorKeyEventArgs>(this.CoreDispatcher_AcceleratorKeyActivated));
				WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<CoreWindow, PointerEventArgs>>(new Action<EventRegistrationToken>(Window.Current.CoreWindow.remove_PointerPressed), new TypedEventHandler<CoreWindow, PointerEventArgs>(this.CoreWindow_PointerPressed));
			});
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001CFD2 File Offset: 0x0001B1D2
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0001D005 File Offset: 0x0001B205
		public RelayCommand GoBackCommand
		{
			get
			{
				if (this._goBackCommand == null)
				{
					this._goBackCommand = new RelayCommand(delegate()
					{
						this.GoBack();
					}, () => this.CanGoBack());
				}
				return this._goBackCommand;
			}
			set
			{
				this._goBackCommand = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001D00E File Offset: 0x0001B20E
		public RelayCommand GoForwardCommand
		{
			get
			{
				if (this._goForwardCommand == null)
				{
					this._goForwardCommand = new RelayCommand(delegate()
					{
						this.GoForward();
					}, () => this.CanGoForward());
				}
				return this._goForwardCommand;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001D041 File Offset: 0x0001B241
		public virtual bool CanGoBack()
		{
			return this.Frame != null && this.Frame.CanGoBack;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001D058 File Offset: 0x0001B258
		public virtual bool CanGoForward()
		{
			return this.Frame != null && this.Frame.CanGoForward;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001D06F File Offset: 0x0001B26F
		public virtual void GoBack()
		{
			if (this.Frame != null && this.Frame.CanGoBack)
			{
				this.Frame.GoBack();
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001D091 File Offset: 0x0001B291
		public virtual void GoForward()
		{
			if (this.Frame != null && this.Frame.CanGoForward)
			{
				this.Frame.GoForward();
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
		{
			VirtualKey virtualKey = e.VirtualKey;
			if ((e.EventType == 4 || e.EventType == null) && (virtualKey == 37 || virtualKey == 39 || virtualKey == 166 || virtualKey == 167))
			{
				CoreWindow coreWindow = Window.Current.CoreWindow;
				CoreVirtualKeyStates coreVirtualKeyStates = 1;
				bool flag = (coreWindow.GetKeyState(18) & coreVirtualKeyStates) == coreVirtualKeyStates;
				bool flag2 = (coreWindow.GetKeyState(17) & coreVirtualKeyStates) == coreVirtualKeyStates;
				bool flag3 = (coreWindow.GetKeyState(16) & coreVirtualKeyStates) == coreVirtualKeyStates;
				bool flag4 = !flag && !flag2 && !flag3;
				bool flag5 = flag && !flag2 && !flag3;
				if ((virtualKey == 166 && flag4) || (virtualKey == 37 && flag5))
				{
					e.put_Handled(true);
					this.GoBackCommand.Execute(null);
					return;
				}
				if ((virtualKey == 167 && flag4) || (virtualKey == 39 && flag5))
				{
					e.put_Handled(true);
					this.GoForwardCommand.Execute(null);
				}
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
		private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
		{
			PointerPointProperties properties = e.CurrentPoint.Properties;
			if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed || properties.IsMiddleButtonPressed)
			{
				return;
			}
			bool isXButton1Pressed = properties.IsXButton1Pressed;
			bool isXButton2Pressed = properties.IsXButton2Pressed;
			if (isXButton1Pressed ^ isXButton2Pressed)
			{
				e.put_Handled(true);
				if (isXButton1Pressed)
				{
					this.GoBackCommand.Execute(null);
				}
				if (isXButton2Pressed)
				{
					this.GoForwardCommand.Execute(null);
				}
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000613 RID: 1555 RVA: 0x0001D210 File Offset: 0x0001B410
		// (remove) Token: 0x06000614 RID: 1556 RVA: 0x0001D248 File Offset: 0x0001B448
		public event LoadStateEventHandler LoadState;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000615 RID: 1557 RVA: 0x0001D280 File Offset: 0x0001B480
		// (remove) Token: 0x06000616 RID: 1558 RVA: 0x0001D2B8 File Offset: 0x0001B4B8
		public event SaveStateEventHandler SaveState;

		// Token: 0x06000617 RID: 1559 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		public void OnNavigatedTo(NavigationEventArgs e)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(this.Frame);
			this._pageKey = "Page-" + this.Frame.BackStackDepth;
			if (e.NavigationMode == null)
			{
				string text = this._pageKey;
				int num = this.Frame.BackStackDepth;
				while (dictionary.Remove(text))
				{
					num++;
					text = "Page-" + num;
				}
				if (this.LoadState != null)
				{
					this.LoadState(this, new LoadStateEventArgs(e.Parameter, null));
					return;
				}
			}
			else if (this.LoadState != null)
			{
				this.LoadState(this, new LoadStateEventArgs(e.Parameter, (Dictionary<string, object>)dictionary[this._pageKey]));
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		public void OnNavigatedFrom(NavigationEventArgs e)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(this.Frame);
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			if (this.SaveState != null)
			{
				this.SaveState(this, new SaveStateEventArgs(dictionary2));
			}
			dictionary[this._pageKey] = dictionary2;
		}

		// Token: 0x04000259 RID: 601
		private RelayCommand _goBackCommand;

		// Token: 0x0400025A RID: 602
		private RelayCommand _goForwardCommand;

		// Token: 0x0400025B RID: 603
		private string _pageKey;
	}
}

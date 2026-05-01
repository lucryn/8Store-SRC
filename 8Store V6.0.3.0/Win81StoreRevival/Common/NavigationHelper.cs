using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	// Token: 0x02000050 RID: 80
	[WebHostHidden]
	public class NavigationHelper : DependencyObject
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001B1A5 File Offset: 0x000193A5
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0001B1AD File Offset: 0x000193AD
		private Page Page { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001B1B8 File Offset: 0x000193B8
		private Frame Frame
		{
			get
			{
				return this.Page.Frame;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001B1D8 File Offset: 0x000193D8
		public NavigationHelper(Page page)
		{
			this.Page = page;
			Page page2 = this.Page;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(page2.add_Loaded), new Action<EventRegistrationToken>(page2.remove_Loaded), delegate(object sender, RoutedEventArgs e)
			{
				bool flag = this.Page.ActualHeight == Window.Current.Bounds.Height && this.Page.ActualWidth == Window.Current.Bounds.Width;
				if (flag)
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001B25C File Offset: 0x0001945C
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001B2A6 File Offset: 0x000194A6
		public RelayCommand GoBackCommand
		{
			get
			{
				bool flag = this._goBackCommand == null;
				if (flag)
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001B2B0 File Offset: 0x000194B0
		public RelayCommand GoForwardCommand
		{
			get
			{
				bool flag = this._goForwardCommand == null;
				if (flag)
				{
					this._goForwardCommand = new RelayCommand(delegate()
					{
						this.GoForward();
					}, () => this.CanGoForward());
				}
				return this._goForwardCommand;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001B2FC File Offset: 0x000194FC
		public virtual bool CanGoBack()
		{
			return this.Frame != null && this.Frame.CanGoBack;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001B324 File Offset: 0x00019524
		public virtual bool CanGoForward()
		{
			return this.Frame != null && this.Frame.CanGoForward;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001B34C File Offset: 0x0001954C
		public virtual void GoBack()
		{
			bool flag = this.Frame != null && this.Frame.CanGoBack;
			if (flag)
			{
				this.Frame.GoBack();
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001B380 File Offset: 0x00019580
		public virtual void GoForward()
		{
			bool flag = this.Frame != null && this.Frame.CanGoForward;
			if (flag)
			{
				this.Frame.GoForward();
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001B3B4 File Offset: 0x000195B4
		private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
		{
			VirtualKey virtualKey = e.VirtualKey;
			bool flag = (e.EventType == 4 || e.EventType == null) && (virtualKey == 37 || virtualKey == 39 || virtualKey == 166 || virtualKey == 167);
			if (flag)
			{
				CoreWindow coreWindow = Window.Current.CoreWindow;
				CoreVirtualKeyStates coreVirtualKeyStates = 1;
				bool flag2 = (coreWindow.GetKeyState(18) & coreVirtualKeyStates) == coreVirtualKeyStates;
				bool flag3 = (coreWindow.GetKeyState(17) & coreVirtualKeyStates) == coreVirtualKeyStates;
				bool flag4 = (coreWindow.GetKeyState(16) & coreVirtualKeyStates) == coreVirtualKeyStates;
				bool flag5 = !flag2 && !flag3 && !flag4;
				bool flag6 = flag2 && !flag3 && !flag4;
				bool flag7 = (virtualKey == 166 && flag5) || (virtualKey == 37 && flag6);
				if (flag7)
				{
					e.put_Handled(true);
					this.GoBackCommand.Execute(null);
				}
				else
				{
					bool flag8 = (virtualKey == 167 && flag5) || (virtualKey == 39 && flag6);
					if (flag8)
					{
						e.put_Handled(true);
						this.GoForwardCommand.Execute(null);
					}
				}
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001B4D0 File Offset: 0x000196D0
		private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
		{
			PointerPointProperties properties = e.CurrentPoint.Properties;
			bool flag = properties.IsLeftButtonPressed || properties.IsRightButtonPressed || properties.IsMiddleButtonPressed;
			if (!flag)
			{
				bool isXButton1Pressed = properties.IsXButton1Pressed;
				bool isXButton2Pressed = properties.IsXButton2Pressed;
				bool flag2 = isXButton1Pressed ^ isXButton2Pressed;
				if (flag2)
				{
					e.put_Handled(true);
					bool flag3 = isXButton1Pressed;
					if (flag3)
					{
						this.GoBackCommand.Execute(null);
					}
					bool flag4 = isXButton2Pressed;
					if (flag4)
					{
						this.GoForwardCommand.Execute(null);
					}
				}
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004C6 RID: 1222 RVA: 0x0001B554 File Offset: 0x00019754
		// (remove) Token: 0x060004C7 RID: 1223 RVA: 0x0001B58C File Offset: 0x0001978C
		[DebuggerBrowsable(0)]
		public event LoadStateEventHandler LoadState;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004C8 RID: 1224 RVA: 0x0001B5C4 File Offset: 0x000197C4
		// (remove) Token: 0x060004C9 RID: 1225 RVA: 0x0001B5FC File Offset: 0x000197FC
		[DebuggerBrowsable(0)]
		public event SaveStateEventHandler SaveState;

		// Token: 0x060004CA RID: 1226 RVA: 0x0001B634 File Offset: 0x00019834
		public void OnNavigatedTo(NavigationEventArgs e)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(this.Frame);
			this._pageKey = "Page-" + this.Frame.BackStackDepth;
			bool flag = e.NavigationMode == 0;
			if (flag)
			{
				string text = this._pageKey;
				int num = this.Frame.BackStackDepth;
				while (dictionary.Remove(text))
				{
					num++;
					text = "Page-" + num;
				}
				bool flag2 = this.LoadState != null;
				if (flag2)
				{
					this.LoadState(this, new LoadStateEventArgs(e.Parameter, null));
				}
			}
			else
			{
				bool flag3 = this.LoadState != null;
				if (flag3)
				{
					this.LoadState(this, new LoadStateEventArgs(e.Parameter, (Dictionary<string, object>)dictionary[this._pageKey]));
				}
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001B720 File Offset: 0x00019920
		public void OnNavigatedFrom(NavigationEventArgs e)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(this.Frame);
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			bool flag = this.SaveState != null;
			if (flag)
			{
				this.SaveState(this, new SaveStateEventArgs(dictionary2));
			}
			dictionary[this._pageKey] = dictionary2;
		}

		// Token: 0x04000222 RID: 546
		private RelayCommand _goBackCommand;

		// Token: 0x04000223 RID: 547
		private RelayCommand _goForwardCommand;

		// Token: 0x04000224 RID: 548
		private string _pageKey;
	}
}

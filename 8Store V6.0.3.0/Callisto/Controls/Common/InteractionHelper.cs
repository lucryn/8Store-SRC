using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Callisto.Controls.Common
{
	// Token: 0x02000004 RID: 4
	internal sealed class InteractionHelper
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002394 File Offset: 0x00000594
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000239C File Offset: 0x0000059C
		public Control Control { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023A5 File Offset: 0x000005A5
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000023AD File Offset: 0x000005AD
		public bool IsFocused { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000023B6 File Offset: 0x000005B6
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000023BE File Offset: 0x000005BE
		public bool IsPointerOver { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023C7 File Offset: 0x000005C7
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000023CF File Offset: 0x000005CF
		public bool IsPointerExited { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023D8 File Offset: 0x000005D8
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023E0 File Offset: 0x000005E0
		public bool IsReadOnly { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023E9 File Offset: 0x000005E9
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000023F1 File Offset: 0x000005F1
		public bool IsPointerPressed { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023FA File Offset: 0x000005FA
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002402 File Offset: 0x00000602
		private DateTime LastClickTime { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000240B File Offset: 0x0000060B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002413 File Offset: 0x00000613
		private Point LastClickPosition { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000241C File Offset: 0x0000061C
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002424 File Offset: 0x00000624
		public int ClickCount { get; private set; }

		// Token: 0x06000022 RID: 34 RVA: 0x00002430 File Offset: 0x00000630
		public InteractionHelper(Control control)
		{
			this.Control = control;
			this._updateVisualState = (control as IUpdateVisualState);
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(control.add_Loaded), new Action<EventRegistrationToken>(control.remove_Loaded), new RoutedEventHandler(this.OnLoaded));
			WindowsRuntimeMarshal.AddEventHandler<DependencyPropertyChangedEventHandler>(new Func<DependencyPropertyChangedEventHandler, EventRegistrationToken>(control.add_IsEnabledChanged), new Action<EventRegistrationToken>(control.remove_IsEnabledChanged), new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024A8 File Offset: 0x000006A8
		private void UpdateVisualState(bool useTransitions)
		{
			if (this._updateVisualState != null)
			{
				this._updateVisualState.UpdateVisualState(useTransitions);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024C0 File Offset: 0x000006C0
		public void UpdateVisualStateBase(bool useTransitions)
		{
			if (!this.Control.IsEnabled)
			{
				VisualStates.GoToState(this.Control, useTransitions, new string[]
				{
					"Disabled",
					"Normal"
				});
			}
			else if (this.IsReadOnly)
			{
				VisualStates.GoToState(this.Control, useTransitions, new string[]
				{
					"ReadOnly",
					"Normal"
				});
			}
			else if (this.IsPointerPressed)
			{
				VisualStates.GoToState(this.Control, useTransitions, new string[]
				{
					"PointerPressed",
					"PointerOver",
					"Normal"
				});
			}
			else if (this.IsPointerOver)
			{
				VisualStates.GoToState(this.Control, useTransitions, new string[]
				{
					"PointerOver",
					"Normal"
				});
			}
			else if (this.IsPointerExited)
			{
				VisualStates.GoToState(this.Control, useTransitions, new string[]
				{
					"PointerExited",
					"Normal"
				});
			}
			else
			{
				VisualStates.GoToState(this.Control, useTransitions, new string[]
				{
					"Normal"
				});
			}
			if (this.IsFocused)
			{
				VisualStates.GoToState(this.Control, useTransitions, new string[]
				{
					"Focused",
					"Unfocused"
				});
				return;
			}
			VisualStates.GoToState(this.Control, useTransitions, new string[]
			{
				"Unfocused"
			});
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002639 File Offset: 0x00000839
		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			this.UpdateVisualState(false);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002644 File Offset: 0x00000844
		private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(bool)e.NewValue)
			{
				this.IsPointerPressed = false;
				this.IsPointerOver = false;
				this.IsFocused = false;
			}
			this.UpdateVisualState(true);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000267C File Offset: 0x0000087C
		public void OnIsReadOnlyChanged(bool value)
		{
			this.IsReadOnly = value;
			if (!value)
			{
				this.IsPointerPressed = false;
				this.IsPointerOver = false;
				this.IsFocused = false;
			}
			this.UpdateVisualState(true);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026A4 File Offset: 0x000008A4
		public void OnApplyTemplateBase()
		{
			this.UpdateVisualState(false);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026B0 File Offset: 0x000008B0
		public bool AllowGotFocus(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			bool isEnabled = this.Control.IsEnabled;
			if (isEnabled)
			{
				this.IsFocused = true;
			}
			return isEnabled;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026E2 File Offset: 0x000008E2
		public void OnGotFocusBase()
		{
			this.UpdateVisualState(true);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026EC File Offset: 0x000008EC
		public bool AllowLostFocus(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			bool isEnabled = this.Control.IsEnabled;
			if (isEnabled)
			{
				this.IsFocused = false;
			}
			return isEnabled;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000271E File Offset: 0x0000091E
		public void OnLostFocusBase()
		{
			this.IsPointerPressed = false;
			this.UpdateVisualState(true);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002730 File Offset: 0x00000930
		public bool AllowPointerEnter(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			bool isEnabled = this.Control.IsEnabled;
			if (isEnabled)
			{
				this.IsPointerOver = true;
			}
			return isEnabled;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002762 File Offset: 0x00000962
		public void OnPointerEnterBase()
		{
			this.UpdateVisualState(true);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000276C File Offset: 0x0000096C
		public bool AllowPointerExit(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			bool isEnabled = this.Control.IsEnabled;
			if (isEnabled)
			{
				this.IsPointerOver = false;
				this.IsPointerPressed = false;
				this.IsPointerExited = true;
			}
			return isEnabled;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027AC File Offset: 0x000009AC
		public void OnPointerExitBase()
		{
			this.UpdateVisualState(true);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027B8 File Offset: 0x000009B8
		public bool AllowPointerPressed(PointerRoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			bool isEnabled = this.Control.IsEnabled;
			if (isEnabled)
			{
				DateTime utcNow = DateTime.UtcNow;
				Point position = e.GetCurrentPoint(this.Control).Position;
				double totalMilliseconds = (utcNow - this.LastClickTime).TotalMilliseconds;
				Point lastClickPosition = this.LastClickPosition;
				double num = position.X - lastClickPosition.X;
				double num2 = position.Y - lastClickPosition.Y;
				double num3 = num * num + num2 * num2;
				if (totalMilliseconds < 500.0 && num3 < 9.0)
				{
					this.ClickCount++;
				}
				else
				{
					this.ClickCount = 1;
				}
				this.LastClickTime = utcNow;
				this.LastClickPosition = position;
				this.IsPointerPressed = true;
			}
			else
			{
				this.ClickCount = 1;
			}
			return isEnabled;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002899 File Offset: 0x00000A99
		public void OnPointerPressedBase()
		{
			this.UpdateVisualState(true);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028A4 File Offset: 0x00000AA4
		public bool AllowPointerReleased(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			bool isEnabled = this.Control.IsEnabled;
			if (isEnabled)
			{
				this.IsPointerPressed = false;
			}
			return isEnabled;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028D6 File Offset: 0x00000AD6
		public void OnPointerReleasedBase()
		{
			this.UpdateVisualState(true);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028DF File Offset: 0x00000ADF
		public bool AllowKeyDown(KeyRoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return this.Control.IsEnabled;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028FA File Offset: 0x00000AFA
		public bool AllowKeyUp(KeyRoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return this.Control.IsEnabled;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002918 File Offset: 0x00000B18
		public static VirtualKey GetLogicalKey(FlowDirection flowDirection, VirtualKey originalKey)
		{
			VirtualKey result = originalKey;
			if (flowDirection == 1)
			{
				switch (originalKey)
				{
				case 37:
					result = 39;
					break;
				case 39:
					result = 37;
					break;
				}
			}
			return result;
		}

		// Token: 0x04000007 RID: 7
		private const double SequentialClickThresholdInMilliseconds = 500.0;

		// Token: 0x04000008 RID: 8
		private const double SequentialClickThresholdInPixelsSquared = 9.0;

		// Token: 0x04000009 RID: 9
		private IUpdateVisualState _updateVisualState;
	}
}

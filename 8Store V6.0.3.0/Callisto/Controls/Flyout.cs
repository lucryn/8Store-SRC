using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Callisto.Controls
{
	// Token: 0x0200000B RID: 11
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as Flyout.")]
	public sealed class Flyout : ContentControl, IDisposable
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003233 File Offset: 0x00001433
		public Popup HostPopup
		{
			get
			{
				return this._hostPopup;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000323C File Offset: 0x0000143C
		public Flyout()
		{
			base.put_DefaultStyleKey(typeof(Flyout));
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowActivatedEventHandler>(new Func<WindowActivatedEventHandler, EventRegistrationToken>(window.add_Activated), new Action<EventRegistrationToken>(window.remove_Activated), new WindowActivatedEventHandler(this.OnCurrentWindowActivated));
			Window window2 = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window2.add_SizeChanged), new Action<EventRegistrationToken>(window2.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnCurrentWindowSizeChanged));
			this.Placement = 10;
			this._windowBounds = Window.Current.Bounds;
			this._rootVisual = Window.Current.Content;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Loaded), new Action<EventRegistrationToken>(base.remove_Loaded), new RoutedEventHandler(this.OnLoaded));
			Popup popup = new Popup();
			popup.put_IsHitTestVisible(false);
			popup.put_Opacity(0.0);
			this._hostPopup = popup;
			Popup hostPopup = this._hostPopup;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(hostPopup.add_Closed), new Action<EventRegistrationToken>(hostPopup.remove_Closed), new EventHandler<object>(this.OnHostPopupClosed));
			Popup hostPopup2 = this._hostPopup;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(hostPopup2.add_Opened), new Action<EventRegistrationToken>(hostPopup2.remove_Opened), new EventHandler<object>(this.OnHostPopupOpened));
			this._hostPopup.put_IsLightDismissEnabled(true);
			this._hostPopup.put_Child(this);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000033B1 File Offset: 0x000015B1
		private void OnCurrentWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.IsOpen = false;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033BA File Offset: 0x000015BA
		private void OnCurrentWindowActivated(object sender, WindowActivatedEventArgs e)
		{
			if (e.WindowActivationState == 1)
			{
				this.IsOpen = false;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000343C File Offset: 0x0000163C
		private void OnHostPopupOpened(object sender, object e)
		{
			if (this._hostPopup.ActualHeight == 0.0 || this._hostPopup.ActualWidth == 0.0)
			{
				SizeChangedEventHandler updatePosition = null;
				updatePosition = delegate(object s, SizeChangedEventArgs eP)
				{
					if (eP.NewSize.Width != 0.0 && eP.NewSize.Height != 0.0)
					{
						this.OnHostPopupOpened(s, eP);
						WindowsRuntimeMarshal.RemoveEventHandler<SizeChangedEventHandler>(new Action<EventRegistrationToken>(this.remove_SizeChanged), updatePosition);
					}
				};
				WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(base.add_SizeChanged), new Action<EventRegistrationToken>(base.remove_SizeChanged), updatePosition);
			}
			this._hostPopup.put_HorizontalOffset(this.HorizontalOffset);
			this._hostPopup.put_VerticalOffset(this.VerticalOffset);
			base.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
			this.PerformPlacement(this.HorizontalOffset, this.VerticalOffset);
			if (this._hostPopup.Parent == null)
			{
				InputPane forCurrentView = InputPane.GetForCurrentView();
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView.add_Showing), new Action<EventRegistrationToken>(forCurrentView.remove_Showing), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneShowing));
				InputPane forCurrentView2 = InputPane.GetForCurrentView();
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView2.add_Hiding), new Action<EventRegistrationToken>(forCurrentView2.remove_Hiding), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneHiding));
			}
			Control control = base.Content as Control;
			if (control != null)
			{
				control.Focus(3);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003596 File Offset: 0x00001796
		private void OnInputPaneHiding(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			if (this._ihmFocusMoved)
			{
				Popup hostPopup = this._hostPopup;
				hostPopup.put_VerticalOffset(hostPopup.VerticalOffset + this._ihmOccludeHeight);
				this._ihmFocusMoved = false;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035C0 File Offset: 0x000017C0
		private void OnInputPaneShowing(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			FrameworkElement frameworkElement = FocusManager.GetFocusedElement() as FrameworkElement;
			if (frameworkElement != null)
			{
				GeneralTransform generalTransform = frameworkElement.TransformToVisual(Window.Current.Content);
				Rect rect = generalTransform.TransformBounds(new Rect(0.0, 0.0, frameworkElement.ActualWidth, frameworkElement.ActualHeight));
				if (rect.Bottom > this._windowBounds.Height - args.OccludedRect.Top)
				{
					this._ihmFocusMoved = true;
					this._ihmOccludeHeight = ((rect.Top < (double)((int)args.OccludedRect.Top)) ? rect.Top : args.OccludedRect.Top);
					Popup hostPopup = this._hostPopup;
					hostPopup.put_VerticalOffset(hostPopup.VerticalOffset - this._ihmOccludeHeight);
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003694 File Offset: 0x00001894
		private static Rect GetBounds(params Point[] interestPoints)
		{
			double num2;
			double num = num2 = interestPoints[0].X;
			double num4;
			double num3 = num4 = interestPoints[0].Y;
			for (int i = 1; i < interestPoints.Length; i++)
			{
				double x = interestPoints[i].X;
				double y = interestPoints[i].Y;
				if (x < num2)
				{
					num2 = x;
				}
				if (x > num)
				{
					num = x;
				}
				if (y < num4)
				{
					num4 = y;
				}
				if (y > num3)
				{
					num3 = y;
				}
			}
			return new Rect(num2, num4, num - num2 + 1.0, num3 - num4 + 1.0);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003730 File Offset: 0x00001930
		private Point PlacePopup(Rect window, Point[] target, Point[] flyout, PlacementMode placement)
		{
			Rect bounds = Flyout.GetBounds(target);
			Rect bounds2 = Flyout.GetBounds(flyout);
			double width = bounds2.Width;
			double height = bounds2.Height;
			if (placement == 4)
			{
				double num = Math.Max(0.0, target[0].X);
				double num2 = window.Width - Math.Min(window.Width, target[1].X + 1.0);
				if (num2 < width && num2 < num)
				{
					placement = 9;
				}
			}
			else if (placement == 9)
			{
				double num3 = window.Width - Math.Min(window.Width, target[1].X + 1.0);
				double num4 = Math.Max(0.0, target[0].X);
				if (num4 < width && num4 < num3)
				{
					placement = 4;
				}
			}
			else if (placement == 10)
			{
				double num5 = Math.Max(0.0, target[0].Y);
				double num6 = window.Height - Math.Min(window.Height, target[2].Y + 1.0);
				if (num5 < height && num5 < num6)
				{
					placement = 2;
				}
			}
			else if (placement == 2)
			{
				double num7 = Math.Max(0.0, target[0].Y);
				double num8 = window.Height - Math.Min(window.Height, target[2].Y + 1.0);
				if (num8 < height && num8 < num7)
				{
					placement = 10;
				}
			}
			PlacementMode placementMode = placement;
			Point[] array;
			switch (placementMode)
			{
			case 2:
				array = new Point[]
				{
					new Point(target[2].X, Math.Max(0.0, target[2].Y + 1.0)),
					new Point(target[3].X - width + 1.0, Math.Max(0.0, target[2].Y + 1.0)),
					new Point(0.0, Math.Max(0.0, target[2].Y + 1.0))
				};
				goto IL_56D;
			case 3:
				break;
			case 4:
				array = new Point[]
				{
					new Point(Math.Max(0.0, target[1].X + 1.0), target[1].Y),
					new Point(Math.Max(0.0, target[3].X + 1.0), target[3].Y - height + 1.0),
					new Point(Math.Max(0.0, target[1].X + 1.0), 0.0)
				};
				goto IL_56D;
			default:
				switch (placementMode)
				{
				case 9:
					array = new Point[]
					{
						new Point(Math.Min(window.Width, target[0].X) - width, target[1].Y),
						new Point(Math.Min(window.Width, target[2].X) - width, target[3].Y - height + 1.0),
						new Point(Math.Min(window.Width, target[0].X) - width, 0.0)
					};
					goto IL_56D;
				case 10:
					array = new Point[]
					{
						new Point(target[0].X, Math.Min(target[0].Y, window.Height) - height),
						new Point(target[1].X - width + 1.0, Math.Min(target[0].Y, window.Height) - height),
						new Point(0.0, Math.Min(target[0].Y, window.Height) - height)
					};
					goto IL_56D;
				}
				break;
			}
			array = new Point[]
			{
				new Point(0.0, 0.0)
			};
			IL_56D:
			double num9 = width * height;
			int num10 = 0;
			double num11 = 0.0;
			for (int i = 0; i < array.Length; i++)
			{
				Rect rect;
				rect..ctor(array[i].X, array[i].Y, width, height);
				rect.Intersect(window);
				double num12 = rect.Width * rect.Height;
				if (double.IsInfinity(num12))
				{
					num10 = array.Length - 1;
					break;
				}
				if (num12 > num11)
				{
					num10 = i;
					num11 = num12;
				}
				if (num12 == num9)
				{
					num10 = i;
					break;
				}
			}
			double num13 = array[0].X;
			double num14 = array[num10].Y;
			if (num10 > 1)
			{
				if (placement == 9 || placement == 4)
				{
					if (num14 != target[0].Y && num14 != target[1].Y && num14 + height != target[0].Y && num14 + height != target[1].Y)
					{
						double num15 = bounds.Top + bounds.Height / 2.0;
						if (num15 > 0.0 && num15 - 0.0 > window.Height - num15)
						{
							num14 = window.Height - height;
						}
						else
						{
							num14 = 0.0;
						}
					}
				}
				else if ((placement == 10 || placement == 2) && num13 != target[0].X && num13 != target[1].X && num13 + width != target[0].X && num13 + width != target[1].X)
				{
					double num16 = bounds.Left + bounds.Width / 2.0;
					if (num16 > 0.0 && num16 - 0.0 > window.Width - num16)
					{
						num13 = window.Width - width;
					}
					else
					{
						num13 = 0.0;
					}
				}
			}
			this._realizedPlacement = placement;
			return new Point(num13, num14);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003ED4 File Offset: 0x000020D4
		private Point[] GetTransformedPoints(FrameworkElement element, bool isRTL, FrameworkElement relativeTo)
		{
			Point[] array = new Point[4];
			if (element != null && relativeTo != null)
			{
				GeneralTransform generalTransform = relativeTo.TransformToVisual(this._rootVisual);
				array[0] = generalTransform.TransformPoint(new Point(0.0, 0.0));
				array[1] = generalTransform.TransformPoint(new Point(element.ActualWidth, 0.0));
				array[2] = generalTransform.TransformPoint(new Point(0.0, element.ActualHeight));
				array[3] = generalTransform.TransformPoint(new Point(element.ActualWidth, element.ActualHeight));
				FrameworkElement frameworkElement = this._rootVisual as FrameworkElement;
				bool flag = frameworkElement != null && frameworkElement.FlowDirection == 1;
			}
			return array;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003FBC File Offset: 0x000021BC
		private void PerformPlacement(double horizontalOffset, double verticalOffset)
		{
			double num = 0.0;
			double num2 = 0.0;
			PlacementMode placement = this.Placement;
			FrameworkElement frameworkElement = this.PlacementTarget as FrameworkElement;
			bool isRTL = frameworkElement != null && frameworkElement.FlowDirection == 1;
			if (frameworkElement != null && !frameworkElement.IsHitTestVisible)
			{
				return;
			}
			switch (placement)
			{
			case 2:
			case 4:
			case 9:
			case 10:
			{
				Point[] transformedPoints = this.GetTransformedPoints(frameworkElement, isRTL, frameworkElement);
				Point[] transformedPoints2 = this.GetTransformedPoints((FrameworkElement)this._hostPopup.Child, isRTL, frameworkElement);
				if (transformedPoints2[0].X > transformedPoints2[1].X)
				{
					return;
				}
				Point point = this.PlacePopup(this._windowBounds, transformedPoints, transformedPoints2, placement);
				num = point.X;
				num2 = point.Y;
				break;
			}
			case 7:
				throw new NotImplementedException("Mouse PlacementMode is not implemented.");
			}
			if (num < 0.0)
			{
				num = 0.0;
			}
			if (num2 < 0.0)
			{
				num2 = 0.0;
			}
			double num3 = this.CalculateHorizontalCenterOffset(num, ((FrameworkElement)this._hostPopup.Child).ActualWidth, frameworkElement.ActualWidth);
			double num4 = this.CalculateVerticalCenterOffset(num2, ((FrameworkElement)this._hostPopup.Child).ActualHeight, frameworkElement.ActualHeight);
			if (num3 < 0.0)
			{
				num3 = 5.0;
			}
			else if (num3 > this._windowBounds.Width || num3 + ((FrameworkElement)this._hostPopup.Child).ActualWidth > this._windowBounds.Width)
			{
				num3 = this._windowBounds.Width - ((FrameworkElement)this._hostPopup.Child).ActualWidth - 5.0;
			}
			UIElement uielement = this._hostPopup.Parent as UIElement;
			if (uielement != null)
			{
				GeneralTransform generalTransform = uielement.TransformToVisual(Window.Current.Content);
				Point point2 = generalTransform.TransformPoint(new Point(0.0, 0.0));
				num3 -= point2.X;
				num4 -= point2.Y;
			}
			this._hostPopup.put_HorizontalOffset(num3);
			this._hostPopup.put_VerticalOffset(num4);
			this._hostPopup.put_IsHitTestVisible(true);
			this._hostPopup.put_Opacity(1.0);
			Storyboard storyboard = new Storyboard();
			PopInThemeAnimation popInThemeAnimation = new PopInThemeAnimation();
			PlacementMode placement2 = this.Placement;
			switch (placement2)
			{
			case 2:
				popInThemeAnimation.put_FromVerticalOffset(-10.0);
				popInThemeAnimation.put_FromHorizontalOffset(0.0);
				break;
			case 3:
				break;
			case 4:
				popInThemeAnimation.put_FromVerticalOffset(0.0);
				popInThemeAnimation.put_FromHorizontalOffset(-10.0);
				break;
			default:
				switch (placement2)
				{
				case 9:
					popInThemeAnimation.put_FromVerticalOffset(0.0);
					popInThemeAnimation.put_FromHorizontalOffset(10.0);
					break;
				case 10:
					popInThemeAnimation.put_FromVerticalOffset(10.0);
					popInThemeAnimation.put_FromHorizontalOffset(0.0);
					break;
				}
				break;
			}
			Storyboard.SetTarget(popInThemeAnimation, this._hostPopup);
			storyboard.Children.Add(popInThemeAnimation);
			storyboard.Begin();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000432C File Offset: 0x0000252C
		private double CalculateHorizontalCenterOffset(double initialOffset, double flyoutWidth, double elementWidth)
		{
			double result;
			if (this._realizedPlacement == 10 || this._realizedPlacement == 2)
			{
				result = this.HorizontalOffset + initialOffset - (flyoutWidth / 2.0 - elementWidth / 2.0);
			}
			else
			{
				result = this.HorizontalOffset + initialOffset;
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004384 File Offset: 0x00002584
		private double CalculateVerticalCenterOffset(double initialOffset, double flyoutHeight, double elementHeight)
		{
			double p;
			if (this._realizedPlacement == 10 || this._realizedPlacement == 2)
			{
				p = this.VerticalOffset + initialOffset;
			}
			else
			{
				p = this.VerticalOffset + initialOffset - flyoutHeight / 2.0 + elementHeight / 2.0;
			}
			return this.CalculateGutter(p);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000043E4 File Offset: 0x000025E4
		private double CalculateGutter(double p)
		{
			double result = p;
			PlacementMode realizedPlacement = this._realizedPlacement;
			switch (realizedPlacement)
			{
			case 2:
				result = p + 4.0;
				break;
			case 3:
				break;
			case 4:
				result = p + 4.0;
				break;
			default:
				switch (realizedPlacement)
				{
				case 9:
					result = p - 4.0;
					break;
				case 10:
					result = p - 4.0;
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004458 File Offset: 0x00002658
		private void OnHostPopupClosed(object sender, object e)
		{
			WindowsRuntimeMarshal.RemoveEventHandler<WindowActivatedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_Activated), new WindowActivatedEventHandler(this.OnCurrentWindowActivated));
			WindowsRuntimeMarshal.RemoveEventHandler<WindowSizeChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnCurrentWindowSizeChanged));
			WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Action<EventRegistrationToken>(InputPane.GetForCurrentView().remove_Showing), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneShowing));
			WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Action<EventRegistrationToken>(InputPane.GetForCurrentView().remove_Hiding), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneHiding));
			if (this.Closed != null)
			{
				this.Closed.Invoke(this, e);
			}
			this.IsOpen = false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004505 File Offset: 0x00002705
		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			((Flyout)sender).put_IsHitTestVisible(true);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000072 RID: 114 RVA: 0x00004514 File Offset: 0x00002714
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x0000454C File Offset: 0x0000274C
		public event EventHandler<object> Closed;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004581 File Offset: 0x00002781
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00004593 File Offset: 0x00002793
		public Thickness HostMargin
		{
			get
			{
				return (Thickness)base.GetValue(Flyout.HostMarginProperty);
			}
			set
			{
				base.SetValue(Flyout.HostMarginProperty, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000045A6 File Offset: 0x000027A6
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000045B8 File Offset: 0x000027B8
		public bool IsOpen
		{
			get
			{
				return (bool)base.GetValue(Flyout.IsOpenProperty);
			}
			set
			{
				base.SetValue(Flyout.IsOpenProperty, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000045CB File Offset: 0x000027CB
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000045DD File Offset: 0x000027DD
		public UIElement PlacementTarget
		{
			get
			{
				return (UIElement)base.GetValue(Flyout.PlacementTargetProperty);
			}
			set
			{
				base.SetValue(Flyout.PlacementTargetProperty, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000045EB File Offset: 0x000027EB
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000045FD File Offset: 0x000027FD
		public PlacementMode Placement
		{
			get
			{
				return (PlacementMode)base.GetValue(Flyout.PlacementProperty);
			}
			set
			{
				base.SetValue(Flyout.PlacementProperty, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004610 File Offset: 0x00002810
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00004622 File Offset: 0x00002822
		public double HorizontalOffset
		{
			get
			{
				return (double)base.GetValue(Flyout.HorizontalOffsetProperty);
			}
			set
			{
				base.SetValue(Flyout.HorizontalOffsetProperty, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004635 File Offset: 0x00002835
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004647 File Offset: 0x00002847
		public double VerticalOffset
		{
			get
			{
				return (double)base.GetValue(Flyout.VerticalOffsetProperty);
			}
			set
			{
				base.SetValue(Flyout.VerticalOffsetProperty, value);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000465A File Offset: 0x0000285A
		public void Dispose()
		{
			if (this._hostPopup != null)
			{
				this._hostPopup.put_Child(null);
				this._hostPopup.put_ChildTransitions(null);
				base.put_Content(null);
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000036 RID: 54
		private const double GUTTER_BUFFER = 5.0;

		// Token: 0x04000037 RID: 55
		private const double GUTTER_BUFFER_ADJUSTED = 4.0;

		// Token: 0x04000038 RID: 56
		private Popup _hostPopup;

		// Token: 0x04000039 RID: 57
		private Rect _windowBounds;

		// Token: 0x0400003A RID: 58
		private UIElement _rootVisual;

		// Token: 0x0400003B RID: 59
		private PlacementMode _realizedPlacement;

		// Token: 0x0400003C RID: 60
		private bool _ihmFocusMoved;

		// Token: 0x0400003D RID: 61
		private double _ihmOccludeHeight;

		// Token: 0x0400003F RID: 63
		public static readonly DependencyProperty HostMarginProperty = DependencyProperty.Register("HostMargin", typeof(Thickness), typeof(Flyout), new PropertyMetadata(0));

		// Token: 0x04000040 RID: 64
		public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(Flyout), new PropertyMetadata(false, delegate(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (args.NewValue != args.OldValue)
			{
				Flyout flyout = (Flyout)obj;
				flyout._hostPopup.put_IsOpen((bool)args.NewValue);
			}
		}));

		// Token: 0x04000041 RID: 65
		public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register("PlacementTarget", typeof(UIElement), typeof(Flyout), null);

		// Token: 0x04000042 RID: 66
		public static readonly DependencyProperty PlacementProperty = DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(Flyout), null);

		// Token: 0x04000043 RID: 67
		public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(Flyout), new PropertyMetadata(0.0));

		// Token: 0x04000044 RID: 68
		public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset", typeof(double), typeof(Flyout), new PropertyMetadata(0.0));
	}
}

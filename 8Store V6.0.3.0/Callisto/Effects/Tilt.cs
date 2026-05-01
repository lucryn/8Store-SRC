using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Callisto.Effects
{
	// Token: 0x0200002B RID: 43
	public class Tilt : DependencyObject
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000A751 File Offset: 0x00008951
		private Tilt()
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A759 File Offset: 0x00008959
		public static bool GetIsTiltEnabled(DependencyObject source)
		{
			return (bool)source.GetValue(Tilt.IsTiltEnabledProperty);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A76B File Offset: 0x0000896B
		public static void SetIsTiltEnabled(DependencyObject source, bool value)
		{
			source.SetValue(Tilt.IsTiltEnabledProperty, value);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A77E File Offset: 0x0000897E
		public static bool GetSuppressTilt(DependencyObject source)
		{
			return (bool)source.GetValue(Tilt.SuppressTiltProperty);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A790 File Offset: 0x00008990
		public static void SetSuppressTilt(DependencyObject source, bool value)
		{
			source.SetValue(Tilt.SuppressTiltProperty, value);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A7A4 File Offset: 0x000089A4
		private static void OnIsTiltEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
		{
			FrameworkElement frameworkElement = target as FrameworkElement;
			if (frameworkElement != null)
			{
				if ((bool)args.NewValue)
				{
					WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(frameworkElement.add_PointerPressed), new Action<EventRegistrationToken>(frameworkElement.remove_PointerPressed), new PointerEventHandler(Tilt.TiltEffect_PointerPressed));
					return;
				}
				WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(frameworkElement.remove_PointerPressed), new PointerEventHandler(Tilt.TiltEffect_PointerPressed));
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A80F File Offset: 0x00008A0F
		private static void TiltEffect_PointerPressed(object sender, PointerRoutedEventArgs e)
		{
			Tilt.TryStartTiltEffect(sender as FrameworkElement, e);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A81D File Offset: 0x00008A1D
		private static void TiltEffect_PointerMoved(object sender, PointerRoutedEventArgs e)
		{
			Tilt.ContinueTiltEffect(sender as FrameworkElement, e);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A82B File Offset: 0x00008A2B
		private static void TiltEffect_PointerReleased(object sender, PointerRoutedEventArgs e)
		{
			Tilt.EndTiltEffect(Tilt.currentTiltElement);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A837 File Offset: 0x00008A37
		private static void TiltEffect_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
		{
			Tilt.EndTiltEffect(Tilt.currentTiltElement);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A844 File Offset: 0x00008A44
		private static void TryStartTiltEffect(FrameworkElement source, PointerRoutedEventArgs e)
		{
			if (source == null || source == null)
			{
				return;
			}
			Point position = e.GetCurrentPoint(source).Position;
			Point centerPoint;
			centerPoint..ctor(source.ActualWidth / 2.0, source.ActualHeight / 2.0);
			Point centerToCenterDelta = Tilt.GetCenterToCenterDelta(source, source);
			Tilt.BeginTiltEffect(source, position, centerPoint, centerToCenterDelta, e.Pointer);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A8AC File Offset: 0x00008AAC
		private static Point GetCenterToCenterDelta(FrameworkElement element, FrameworkElement container)
		{
			Point point;
			point..ctor(element.ActualWidth / 2.0, element.ActualHeight / 2.0);
			Point point2;
			point2..ctor(container.ActualWidth / 2.0, container.ActualHeight / 2.0);
			Point point3 = element.TransformToVisual(container).TransformPoint(point);
			return new Point(point2.X - point3.X, point2.Y - point3.Y);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A938 File Offset: 0x00008B38
		private static void BeginTiltEffect(FrameworkElement element, Point touchPoint, Point centerPoint, Point centerDelta, Pointer p)
		{
			if (Tilt.tiltReturnStoryboard != null)
			{
				Tilt.StopTiltReturnStoryboardAndCleanup();
			}
			if (!Tilt.PrepareControlForTilt(element, centerDelta, p))
			{
				return;
			}
			Tilt.currentTiltElement = element;
			Tilt.currentTiltElementCenter = centerPoint;
			Tilt.PrepareTiltReturnStoryboard(element);
			Tilt.ApplyTiltEffect(Tilt.currentTiltElement, touchPoint, Tilt.currentTiltElementCenter);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A974 File Offset: 0x00008B74
		private static bool PrepareControlForTilt(FrameworkElement element, Point centerDelta, Pointer p)
		{
			if (element.Projection != null || (element.RenderTransform != null && element.RenderTransform.GetType() != typeof(MatrixTransform)))
			{
				return false;
			}
			Tilt._originalCacheMode[element] = element.CacheMode;
			element.put_CacheMode(new BitmapCache());
			TranslateTransform translateTransform = new TranslateTransform();
			translateTransform.put_X(centerDelta.X);
			translateTransform.put_Y(centerDelta.Y);
			element.put_RenderTransform(translateTransform);
			PlaneProjection planeProjection = new PlaneProjection();
			planeProjection.put_GlobalOffsetX(-1.0 * centerDelta.X);
			planeProjection.put_GlobalOffsetY(-1.0 * centerDelta.Y);
			element.put_Projection(planeProjection);
			WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(element.add_PointerMoved), new Action<EventRegistrationToken>(element.remove_PointerMoved), new PointerEventHandler(Tilt.TiltEffect_PointerMoved));
			WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(element.add_PointerReleased), new Action<EventRegistrationToken>(element.remove_PointerReleased), new PointerEventHandler(Tilt.TiltEffect_PointerReleased));
			WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(element.add_PointerCaptureLost), new Action<EventRegistrationToken>(element.remove_PointerCaptureLost), new PointerEventHandler(Tilt.TiltEffect_PointerCaptureLost));
			element.CapturePointer(p);
			return true;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000AAAC File Offset: 0x00008CAC
		private static void RevertPrepareControlForTilt(FrameworkElement element)
		{
			WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(element.remove_PointerMoved), new PointerEventHandler(Tilt.TiltEffect_PointerMoved));
			WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(element.remove_PointerReleased), new PointerEventHandler(Tilt.TiltEffect_PointerReleased));
			WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(element.remove_PointerCaptureLost), new PointerEventHandler(Tilt.TiltEffect_PointerCaptureLost));
			element.put_Projection(null);
			element.put_RenderTransform(null);
			CacheMode cacheMode;
			if (Tilt._originalCacheMode.TryGetValue(element, ref cacheMode))
			{
				element.put_CacheMode(cacheMode);
				Tilt._originalCacheMode.Remove(element);
				return;
			}
			element.put_CacheMode(null);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000AB48 File Offset: 0x00008D48
		private static void PrepareTiltReturnStoryboard(FrameworkElement element)
		{
			if (Tilt.tiltReturnStoryboard == null)
			{
				Tilt.tiltReturnStoryboard = new Storyboard();
				Storyboard storyboard = Tilt.tiltReturnStoryboard;
				WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(storyboard.add_Completed), new Action<EventRegistrationToken>(storyboard.remove_Completed), new EventHandler<object>(Tilt.TiltReturnStoryboard_Completed));
				Tilt.tiltReturnXAnimation = new DoubleAnimation();
				Storyboard.SetTargetProperty(Tilt.tiltReturnXAnimation, "RotationX");
				Tilt.tiltReturnXAnimation.put_BeginTime(new TimeSpan?(Tilt.TiltReturnAnimationDelay));
				Tilt.tiltReturnXAnimation.put_To(new double?(0.0));
				Tilt.tiltReturnXAnimation.put_Duration(Tilt.TiltReturnAnimationDuration);
				Tilt.tiltReturnYAnimation = new DoubleAnimation();
				Storyboard.SetTargetProperty(Tilt.tiltReturnYAnimation, "RotationY");
				Tilt.tiltReturnYAnimation.put_BeginTime(new TimeSpan?(Tilt.TiltReturnAnimationDelay));
				Tilt.tiltReturnYAnimation.put_To(new double?(0.0));
				Tilt.tiltReturnYAnimation.put_Duration(Tilt.TiltReturnAnimationDuration);
				Tilt.tiltReturnZAnimation = new DoubleAnimation();
				Storyboard.SetTargetProperty(Tilt.tiltReturnZAnimation, "GlobalOffsetZ");
				Tilt.tiltReturnZAnimation.put_BeginTime(new TimeSpan?(Tilt.TiltReturnAnimationDelay));
				Tilt.tiltReturnZAnimation.put_To(new double?(0.0));
				Tilt.tiltReturnZAnimation.put_Duration(Tilt.TiltReturnAnimationDuration);
				Tilt.tiltReturnStoryboard.Children.Add(Tilt.tiltReturnXAnimation);
				Tilt.tiltReturnStoryboard.Children.Add(Tilt.tiltReturnYAnimation);
				Tilt.tiltReturnStoryboard.Children.Add(Tilt.tiltReturnZAnimation);
			}
			Storyboard.SetTarget(Tilt.tiltReturnXAnimation, element.Projection);
			Storyboard.SetTarget(Tilt.tiltReturnYAnimation, element.Projection);
			Storyboard.SetTarget(Tilt.tiltReturnZAnimation, element.Projection);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000AD10 File Offset: 0x00008F10
		private static void ContinueTiltEffect(FrameworkElement element, PointerRoutedEventArgs e)
		{
			if (element == null || element == null)
			{
				return;
			}
			Point position = e.GetCurrentPoint(element).Position;
			if (!new Rect(0.0, 0.0, Tilt.currentTiltElement.ActualWidth, Tilt.currentTiltElement.ActualHeight).Contains(position))
			{
				Tilt.PauseTiltEffect();
				return;
			}
			Tilt.ApplyTiltEffect(Tilt.currentTiltElement, position, Tilt.currentTiltElementCenter);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AD80 File Offset: 0x00008F80
		private static void EndTiltEffect(FrameworkElement element)
		{
			if (element != null)
			{
				WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(element.remove_PointerReleased), new PointerEventHandler(Tilt.TiltEffect_PointerPressed));
				WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(element.remove_PointerMoved), new PointerEventHandler(Tilt.TiltEffect_PointerMoved));
			}
			if (Tilt.tiltReturnStoryboard != null)
			{
				Tilt.wasPauseAnimation = false;
				if (Tilt.tiltReturnStoryboard.GetCurrentState() != null)
				{
					Tilt.tiltReturnStoryboard.Begin();
					return;
				}
			}
			else
			{
				Tilt.StopTiltReturnStoryboardAndCleanup();
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000ADF3 File Offset: 0x00008FF3
		private static void TiltReturnStoryboard_Completed(object sender, object e)
		{
			if (Tilt.wasPauseAnimation)
			{
				Tilt.ResetTiltEffect(Tilt.currentTiltElement);
				return;
			}
			Tilt.StopTiltReturnStoryboardAndCleanup();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AE0C File Offset: 0x0000900C
		private static void ResetTiltEffect(FrameworkElement element)
		{
			PlaneProjection planeProjection = element.Projection as PlaneProjection;
			planeProjection.put_RotationY(0.0);
			planeProjection.put_RotationX(0.0);
			planeProjection.put_GlobalOffsetZ(0.0);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AE52 File Offset: 0x00009052
		private static void StopTiltReturnStoryboardAndCleanup()
		{
			if (Tilt.tiltReturnStoryboard != null)
			{
				Tilt.tiltReturnStoryboard.Stop();
			}
			Tilt.RevertPrepareControlForTilt(Tilt.currentTiltElement);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000AE6F File Offset: 0x0000906F
		private static void PauseTiltEffect()
		{
			if (Tilt.tiltReturnStoryboard != null && !Tilt.wasPauseAnimation)
			{
				Tilt.tiltReturnStoryboard.Stop();
				Tilt.wasPauseAnimation = true;
				Tilt.tiltReturnStoryboard.Begin();
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AE99 File Offset: 0x00009099
		private static void ResetTiltReturnStoryboard()
		{
			Tilt.tiltReturnStoryboard.Stop();
			Tilt.wasPauseAnimation = false;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000AEAC File Offset: 0x000090AC
		private static void ApplyTiltEffect(FrameworkElement element, Point touchPoint, Point centerPoint)
		{
			Tilt.ResetTiltReturnStoryboard();
			Point point;
			point..ctor(Math.Min(Math.Max(touchPoint.X / (centerPoint.X * 2.0), 0.0), 1.0), Math.Min(Math.Max(touchPoint.Y / (centerPoint.Y * 2.0), 0.0), 1.0));
			if (double.IsNaN(point.X) || double.IsNaN(point.Y))
			{
				return;
			}
			double num = Math.Abs(point.X - 0.5);
			double num2 = Math.Abs(point.Y - 0.5);
			double num3 = (double)(-(double)Math.Sign(point.X - 0.5));
			double num4 = (double)Math.Sign(point.Y - 0.5);
			double num5 = num + num2;
			double num6 = (num + num2 > 0.0) ? (num / (num + num2)) : 0.0;
			double num7 = num5 * 0.3 * 180.0 / 3.141592653589793;
			double num8 = (1.0 - num5) * 25.0;
			PlaneProjection planeProjection = element.Projection as PlaneProjection;
			planeProjection.put_RotationY(num7 * num6 * num3);
			planeProjection.put_RotationX(num7 * (1.0 - num6) * num4);
			planeProjection.put_GlobalOffsetZ(-num8);
		}

		// Token: 0x040000F7 RID: 247
		private const double MaxAngle = 0.3;

		// Token: 0x040000F8 RID: 248
		private const double MaxDepression = 25.0;

		// Token: 0x040000F9 RID: 249
		private static Dictionary<DependencyObject, CacheMode> _originalCacheMode = new Dictionary<DependencyObject, CacheMode>();

		// Token: 0x040000FA RID: 250
		private static readonly TimeSpan TiltReturnAnimationDelay = TimeSpan.FromMilliseconds(200.0);

		// Token: 0x040000FB RID: 251
		private static readonly TimeSpan TiltReturnAnimationDuration = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x040000FC RID: 252
		private static FrameworkElement currentTiltElement;

		// Token: 0x040000FD RID: 253
		private static Storyboard tiltReturnStoryboard;

		// Token: 0x040000FE RID: 254
		private static DoubleAnimation tiltReturnXAnimation;

		// Token: 0x040000FF RID: 255
		private static DoubleAnimation tiltReturnYAnimation;

		// Token: 0x04000100 RID: 256
		private static DoubleAnimation tiltReturnZAnimation;

		// Token: 0x04000101 RID: 257
		private static Point currentTiltElementCenter;

		// Token: 0x04000102 RID: 258
		private static bool wasPauseAnimation = false;

		// Token: 0x04000103 RID: 259
		public static readonly DependencyProperty IsTiltEnabledProperty = DependencyProperty.RegisterAttached("IsTiltEnabled", typeof(bool), typeof(Tilt), new PropertyMetadata(false, new PropertyChangedCallback(Tilt.OnIsTiltEnabledChanged)));

		// Token: 0x04000104 RID: 260
		public static readonly DependencyProperty SuppressTiltProperty = DependencyProperty.RegisterAttached("SuppressTilt", typeof(bool), typeof(Tilt), null);
	}
}

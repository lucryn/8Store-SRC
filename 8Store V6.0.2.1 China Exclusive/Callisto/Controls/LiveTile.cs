using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Callisto.Controls
{
	// Token: 0x0200000C RID: 12
	[TemplatePart(Name = "Current", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "Translate", Type = typeof(TranslateTransform))]
	[TemplatePart(Name = "Next", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "Stack", Type = typeof(StackPanel))]
	[TemplatePart(Name = "Scroller", Type = typeof(FrameworkElement))]
	public sealed class LiveTile : Control
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004800 File Offset: 0x00002A00
		public LiveTile()
		{
			base.put_DefaultStyleKey(typeof(LiveTile));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Unloaded), new Action<EventRegistrationToken>(base.remove_Unloaded), new RoutedEventHandler(this.EpisodeFlipControl_Unloaded));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Loaded), new Action<EventRegistrationToken>(base.remove_Loaded), new RoutedEventHandler(this.EpisodeFlipControl_Loaded));
			WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(base.add_SizeChanged), new Action<EventRegistrationToken>(base.remove_SizeChanged), new SizeChangedEventHandler(this.EpisodeFlipControl_SizeChanged));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000048A4 File Offset: 0x00002AA4
		protected override void OnApplyTemplate()
		{
			this._scroller = (base.GetTemplateChild("Scroller") as FrameworkElement);
			this._currentElement = (base.GetTemplateChild("Current") as FrameworkElement);
			this._nextElement = (base.GetTemplateChild("Next") as FrameworkElement);
			this._translate = (base.GetTemplateChild("Translate") as TranslateTransform);
			this._stackPanel = (base.GetTemplateChild("Stack") as StackPanel);
			if (this._stackPanel != null)
			{
				if (this.Direction == LiveTile.SlideDirection.Up)
				{
					this._stackPanel.put_Orientation(0);
				}
				else
				{
					this._stackPanel.put_Orientation(1);
				}
			}
			if (this.ItemsSource != null)
			{
				this.Start();
			}
			base.OnApplyTemplate();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004960 File Offset: 0x00002B60
		private void EpisodeFlipControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this._currentElement != null && this._nextElement != null)
			{
				FrameworkElement currentElement = this._currentElement;
				double width;
				this._nextElement.put_Width(width = e.NewSize.Width);
				currentElement.put_Width(width);
				FrameworkElement currentElement2 = this._currentElement;
				double height;
				this._nextElement.put_Height(height = e.NewSize.Height);
				currentElement2.put_Height(height);
			}
			if (this._scroller != null)
			{
				if (this.Direction == LiveTile.SlideDirection.Up)
				{
					this._scroller.put_Height(e.NewSize.Height * 2.0);
				}
				else
				{
					this._scroller.put_Width(e.NewSize.Width * 2.0);
				}
			}
			RectangleGeometry rectangleGeometry = new RectangleGeometry();
			rectangleGeometry.put_Rect(new Rect(default(Point), e.NewSize));
			base.put_Clip(rectangleGeometry);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004A4F File Offset: 0x00002C4F
		private void EpisodeFlipControl_Loaded(object sender, RoutedEventArgs e)
		{
			if (this._timer != null)
			{
				this._timer.Start();
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004A64 File Offset: 0x00002C64
		private void EpisodeFlipControl_Unloaded(object sender, RoutedEventArgs e)
		{
			if (this._timer != null)
			{
				this._timer.Stop();
			}
			if (this._translate != null)
			{
				this._translate.put_Y(0.0);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004A95 File Offset: 0x00002C95
		private void timer_Tick(object sender, object e)
		{
			this._currentIndex++;
			this._timer.put_Interval(TimeSpan.FromSeconds((double)(LiveTile._randomizer.Next(5) + 5)));
			this.UpdateNextItem();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004B78 File Offset: 0x00002D78
		private void UpdateNextItem()
		{
			bool flag = false;
			if (this.ItemsSource is IEnumerable)
			{
				IEnumerator enumerator = (this.ItemsSource as IEnumerable).GetEnumerator();
				int num = 0;
				while (enumerator.MoveNext())
				{
					num++;
					if (num > 1)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			Storyboard sb = new Storyboard();
			if (this._translate != null)
			{
				DoubleAnimation doubleAnimation = new DoubleAnimation();
				doubleAnimation.put_Duration(new Duration(TimeSpan.FromMilliseconds(500.0)));
				doubleAnimation.put_From(new double?(0.0));
				if (this.Direction == LiveTile.SlideDirection.Up)
				{
					doubleAnimation.put_To(new double?(-base.ActualHeight));
				}
				else if (this.Direction == LiveTile.SlideDirection.Left)
				{
					doubleAnimation.put_To(new double?(-base.ActualWidth));
				}
				doubleAnimation.put_FillBehavior(0);
				DoubleAnimation doubleAnimation2 = doubleAnimation;
				CubicEase cubicEase = new CubicEase();
				cubicEase.put_EasingMode(0);
				doubleAnimation2.put_EasingFunction(cubicEase);
				Storyboard.SetTarget(doubleAnimation, this._translate);
				if (this.Direction == LiveTile.SlideDirection.Up)
				{
					Storyboard.SetTargetProperty(doubleAnimation, "Y");
				}
				else
				{
					Storyboard.SetTargetProperty(doubleAnimation, "X");
				}
				sb.Children.Add(doubleAnimation);
			}
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(sb.add_Completed), new Action<EventRegistrationToken>(sb.remove_Completed), delegate(object a, object b)
			{
				sb.Stop();
				if (this._translate != null)
				{
					TranslateTransform translate = this._translate;
					double num2;
					this._translate.put_Y(num2 = 0.0);
					translate.put_X(num2);
				}
				if (this._currentElement != null)
				{
					this._currentElement.put_DataContext(this.GetCurrent());
				}
				if (this._nextElement != null)
				{
					this._nextElement.put_DataContext(this.GetNext());
				}
			});
			sb.Begin();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004CEF File Offset: 0x00002EEF
		private object GetCurrent()
		{
			return this.GetItemAt(this._currentIndex);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004CFD File Offset: 0x00002EFD
		private object GetNext()
		{
			return this.GetItemAt(this._currentIndex + 1);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004D10 File Offset: 0x00002F10
		private object GetItemAt(int index)
		{
			if (this.ItemsSource != null)
			{
				if (this.ItemsSource is IList)
				{
					IList list = this.ItemsSource as IList;
					if (list.Count > 0)
					{
						index %= list.Count;
						return list[index];
					}
				}
				else if (this.ItemsSource is IEnumerable<object>)
				{
					IEnumerable<object> enumerable = this.ItemsSource as IEnumerable<object>;
					int num = Enumerable.Count<object>(enumerable);
					if (num > 0)
					{
						index %= num;
						return Enumerable.ElementAt<object>(enumerable, index);
					}
				}
			}
			return null;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004D8C File Offset: 0x00002F8C
		private void Start()
		{
			this._currentIndex = 0;
			if (this._currentElement != null)
			{
				this._currentElement.put_DataContext(this.GetCurrent());
			}
			if (this._nextElement != null)
			{
				this._nextElement.put_DataContext(this.GetNext());
			}
			if (this._timer == null)
			{
				DispatcherTimer dispatcherTimer = new DispatcherTimer();
				dispatcherTimer.put_Interval(TimeSpan.FromSeconds(1.0));
				this._timer = dispatcherTimer;
				DispatcherTimer timer = this._timer;
				WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timer.add_Tick), new Action<EventRegistrationToken>(timer.remove_Tick), new EventHandler<object>(this.timer_Tick));
				this._timer.put_Interval(TimeSpan.FromSeconds((double)(LiveTile._randomizer.Next(5) + 5)));
			}
			this._timer.Start();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004E54 File Offset: 0x00003054
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004E61 File Offset: 0x00003061
		public object ItemsSource
		{
			get
			{
				return base.GetValue(LiveTile.ItemsSourceProperty);
			}
			set
			{
				base.SetValue(LiveTile.ItemsSourceProperty, value);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004E70 File Offset: 0x00003070
		private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LiveTile liveTile = d as LiveTile;
			if (e.NewValue is IEnumerable)
			{
				if (liveTile._currentElement != null && liveTile._nextElement != null)
				{
					liveTile.Start();
					return;
				}
			}
			else if (liveTile._timer != null)
			{
				liveTile._timer.Stop();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004EBB File Offset: 0x000030BB
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004ECD File Offset: 0x000030CD
		public DataTemplate ItemTemplate
		{
			get
			{
				return (DataTemplate)base.GetValue(LiveTile.ItemTemplateProperty);
			}
			set
			{
				base.SetValue(LiveTile.ItemTemplateProperty, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004EDB File Offset: 0x000030DB
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004EED File Offset: 0x000030ED
		public LiveTile.SlideDirection Direction
		{
			get
			{
				return (LiveTile.SlideDirection)base.GetValue(LiveTile.SlideDirectionProperty);
			}
			set
			{
				base.SetValue(LiveTile.SlideDirectionProperty, value);
			}
		}

		// Token: 0x04000046 RID: 70
		private const string SCROLLER_PARTNAME = "Scroller";

		// Token: 0x04000047 RID: 71
		private const string CURRENT_PARTNAME = "Current";

		// Token: 0x04000048 RID: 72
		private const string NEXT_PARTNAME = "Next";

		// Token: 0x04000049 RID: 73
		private const string TRANSLATE_PARTNAME = "Translate";

		// Token: 0x0400004A RID: 74
		private const string STACK_PARTNAME = "Stack";

		// Token: 0x0400004B RID: 75
		private static Random _randomizer = new Random();

		// Token: 0x0400004C RID: 76
		private int _currentIndex;

		// Token: 0x0400004D RID: 77
		private DispatcherTimer _timer;

		// Token: 0x0400004E RID: 78
		private FrameworkElement _currentElement;

		// Token: 0x0400004F RID: 79
		private FrameworkElement _nextElement;

		// Token: 0x04000050 RID: 80
		private FrameworkElement _scroller;

		// Token: 0x04000051 RID: 81
		private TranslateTransform _translate;

		// Token: 0x04000052 RID: 82
		private StackPanel _stackPanel;

		// Token: 0x04000053 RID: 83
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(LiveTile), new PropertyMetadata(null, new PropertyChangedCallback(LiveTile.OnItemsSourcePropertyChanged)));

		// Token: 0x04000054 RID: 84
		public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(LiveTile), null);

		// Token: 0x04000055 RID: 85
		public static readonly DependencyProperty SlideDirectionProperty = DependencyProperty.Register("SlideDirection", typeof(LiveTile.SlideDirection), typeof(LiveTile), new PropertyMetadata(LiveTile.SlideDirection.Up));

		// Token: 0x0200000D RID: 13
		public enum SlideDirection
		{
			// Token: 0x04000057 RID: 87
			Up,
			// Token: 0x04000058 RID: 88
			Left
		}
	}
}

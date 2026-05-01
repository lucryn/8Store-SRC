using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Callisto.Controls.Common
{
	// Token: 0x02000005 RID: 5
	internal sealed class ItemsControlHelper
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000294D File Offset: 0x00000B4D
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002955 File Offset: 0x00000B55
		private ItemsControl ItemsControl { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002960 File Offset: 0x00000B60
		internal Panel ItemsHost
		{
			get
			{
				if (this._itemsHost == null && this.ItemsControl != null && this.ItemsControl.ItemContainerGenerator != null)
				{
					DependencyObject dependencyObject = this.ItemsControl.ItemContainerGenerator.ContainerFromIndex(0);
					if (dependencyObject != null)
					{
						this._itemsHost = (VisualTreeHelper.GetParent(dependencyObject) as Panel);
					}
				}
				return this._itemsHost;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029B8 File Offset: 0x00000BB8
		internal ScrollViewer ScrollHost
		{
			get
			{
				if (this._scrollHost == null)
				{
					Panel itemsHost = this.ItemsHost;
					if (itemsHost != null)
					{
						DependencyObject dependencyObject = itemsHost;
						while (dependencyObject != this.ItemsControl && dependencyObject != null)
						{
							ScrollViewer scrollViewer = dependencyObject as ScrollViewer;
							if (scrollViewer != null)
							{
								this._scrollHost = scrollViewer;
								break;
							}
							dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
						}
					}
				}
				return this._scrollHost;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A07 File Offset: 0x00000C07
		internal ItemsControlHelper(ItemsControl control)
		{
			this.ItemsControl = control;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A16 File Offset: 0x00000C16
		internal void OnApplyTemplate()
		{
			this._itemsHost = null;
			this._scrollHost = null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A28 File Offset: 0x00000C28
		internal static void PrepareContainerForItemOverride(DependencyObject element, Style parentItemContainerStyle)
		{
			Control control = element as Control;
			if (parentItemContainerStyle != null && control != null && control.Style == null)
			{
				control.SetValue(FrameworkElement.StyleProperty, parentItemContainerStyle);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A58 File Offset: 0x00000C58
		internal void UpdateItemContainerStyle(Style itemContainerStyle)
		{
			if (itemContainerStyle == null)
			{
				return;
			}
			Panel itemsHost = this.ItemsHost;
			if (itemsHost == null || itemsHost.Children == null)
			{
				return;
			}
			foreach (UIElement uielement in itemsHost.Children)
			{
				FrameworkElement frameworkElement = uielement as FrameworkElement;
				if (frameworkElement.Style == null)
				{
					frameworkElement.put_Style(itemContainerStyle);
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002ACC File Offset: 0x00000CCC
		internal void ScrollIntoView(FrameworkElement element)
		{
			ScrollViewer scrollHost = this.ScrollHost;
			if (scrollHost == null)
			{
				return;
			}
			GeneralTransform generalTransform = null;
			try
			{
				generalTransform = element.TransformToVisual(scrollHost);
			}
			catch (ArgumentException)
			{
				return;
			}
			Rect rect;
			rect..ctor(generalTransform.TransformPoint(default(Point)), generalTransform.TransformPoint(new Point(element.ActualWidth, element.ActualHeight)));
			double num = scrollHost.VerticalOffset;
			double num2 = 0.0;
			double viewportHeight = scrollHost.ViewportHeight;
			double bottom = rect.Bottom;
			if (viewportHeight < bottom)
			{
				num2 = bottom - viewportHeight;
				num += num2;
			}
			double top = rect.Top;
			if (top - num2 < 0.0)
			{
				num -= num2 - top;
			}
			scrollHost.ScrollToVerticalOffset(num);
			double num3 = scrollHost.HorizontalOffset;
			double num4 = 0.0;
			double viewportWidth = scrollHost.ViewportWidth;
			double right = rect.Right;
			if (viewportWidth < right)
			{
				num4 = right - viewportWidth;
				num3 += num4;
			}
			double left = rect.Left;
			if (left - num4 < 0.0)
			{
				num3 -= num4 - left;
			}
			scrollHost.ScrollToHorizontalOffset(num3);
		}

		// Token: 0x04000013 RID: 19
		private Panel _itemsHost;

		// Token: 0x04000014 RID: 20
		private ScrollViewer _scrollHost;
	}
}

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Callisto.Controls.Primitives
{
	// Token: 0x02000016 RID: 22
	public class LinearClipper : Clipper
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005CC5 File Offset: 0x00003EC5
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00005CD7 File Offset: 0x00003ED7
		public ExpandDirection ExpandDirection
		{
			get
			{
				return (ExpandDirection)base.GetValue(LinearClipper.ExpandDirectionProperty);
			}
			set
			{
				base.SetValue(LinearClipper.ExpandDirectionProperty, value);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005CEC File Offset: 0x00003EEC
		private static void OnExpandDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LinearClipper linearClipper = (LinearClipper)d;
			ExpandDirection oldValue = (ExpandDirection)e.OldValue;
			ExpandDirection newValue = (ExpandDirection)e.NewValue;
			linearClipper.OnExpandDirectionChanged(oldValue, newValue);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005D20 File Offset: 0x00003F20
		protected virtual void OnExpandDirectionChanged(ExpandDirection oldValue, ExpandDirection newValue)
		{
			this.ClipContent();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005D28 File Offset: 0x00003F28
		protected override void ClipContent()
		{
			if (this.ExpandDirection == ExpandDirection.Right)
			{
				double num = base.RenderSize.Width * base.RatioVisible;
				RectangleGeometry rectangleGeometry = new RectangleGeometry();
				rectangleGeometry.put_Rect(new Rect(0.0, 0.0, num, base.RenderSize.Height));
				base.put_Clip(rectangleGeometry);
				return;
			}
			if (this.ExpandDirection == ExpandDirection.Left)
			{
				double num2 = base.RenderSize.Width * base.RatioVisible;
				double num3 = base.RenderSize.Width - num2;
				RectangleGeometry rectangleGeometry2 = new RectangleGeometry();
				rectangleGeometry2.put_Rect(new Rect(num3, 0.0, num2, base.RenderSize.Height));
				base.put_Clip(rectangleGeometry2);
				return;
			}
			if (this.ExpandDirection == ExpandDirection.Up)
			{
				double num4 = base.RenderSize.Height * base.RatioVisible;
				double num5 = base.RenderSize.Height - num4;
				RectangleGeometry rectangleGeometry3 = new RectangleGeometry();
				rectangleGeometry3.put_Rect(new Rect(0.0, num5, base.RenderSize.Width, num4));
				base.put_Clip(rectangleGeometry3);
				return;
			}
			if (this.ExpandDirection == ExpandDirection.Down)
			{
				double num6 = base.RenderSize.Height * base.RatioVisible;
				RectangleGeometry rectangleGeometry4 = new RectangleGeometry();
				rectangleGeometry4.put_Rect(new Rect(0.0, 0.0, base.RenderSize.Width, num6));
				base.put_Clip(rectangleGeometry4);
			}
		}

		// Token: 0x0400006F RID: 111
		public static readonly DependencyProperty ExpandDirectionProperty = DependencyProperty.Register("ExpandDirection", typeof(ExpandDirection), typeof(LinearClipper), new PropertyMetadata(ExpandDirection.Right, new PropertyChangedCallback(LinearClipper.OnExpandDirectionChanged)));
	}
}

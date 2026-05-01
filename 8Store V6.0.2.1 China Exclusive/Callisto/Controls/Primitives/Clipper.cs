using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls.Primitives
{
	// Token: 0x02000013 RID: 19
	public abstract class Clipper : ContentControl
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000056BF File Offset: 0x000038BF
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000056D1 File Offset: 0x000038D1
		public double RatioVisible
		{
			get
			{
				return (double)base.GetValue(Clipper.RatioVisibleProperty);
			}
			set
			{
				base.SetValue(Clipper.RatioVisibleProperty, value);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000056E4 File Offset: 0x000038E4
		private static void OnRatioVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Clipper clipper = (Clipper)d;
			double oldValue = (double)e.OldValue;
			double newValue = (double)e.NewValue;
			clipper.OnRatioVisibleChanged(oldValue, newValue);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005718 File Offset: 0x00003918
		protected virtual void OnRatioVisibleChanged(double oldValue, double newValue)
		{
			if (newValue >= 0.0 && newValue <= 1.0)
			{
				this.ClipContent();
				return;
			}
			if (newValue < 0.0)
			{
				this.RatioVisible = 0.0;
				return;
			}
			if (newValue > 1.0)
			{
				this.RatioVisible = 1.0;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005784 File Offset: 0x00003984
		protected Clipper()
		{
			WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(base.add_SizeChanged), new Action<EventRegistrationToken>(base.remove_SizeChanged), delegate(object param0, SizeChangedEventArgs param1)
			{
				this.ClipContent();
			});
		}

		// Token: 0x060000C0 RID: 192
		protected abstract void ClipContent();

		// Token: 0x04000066 RID: 102
		public static readonly DependencyProperty RatioVisibleProperty = DependencyProperty.Register("RatioVisible", typeof(double), typeof(Clipper), new PropertyMetadata(1.0, new PropertyChangedCallback(Clipper.OnRatioVisibleChanged)));
	}
}

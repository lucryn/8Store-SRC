using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;

namespace Callisto.Controls
{
	// Token: 0x0200000A RID: 10
	public sealed class FlipViewIndicator : ListBox
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000030D4 File Offset: 0x000012D4
		public FlipViewIndicator()
		{
			base.put_DefaultStyleKey(typeof(FlipViewIndicator));
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000030EC File Offset: 0x000012EC
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000030FE File Offset: 0x000012FE
		public FlipView FlipView
		{
			get
			{
				return (FlipView)base.GetValue(FlipViewIndicator.FlipViewProperty);
			}
			set
			{
				base.SetValue(FlipViewIndicator.FlipViewProperty, value);
			}
		}

		// Token: 0x04000034 RID: 52
		public static readonly DependencyProperty FlipViewProperty = DependencyProperty.Register("FlipView", typeof(FlipView), typeof(FlipViewIndicator), new PropertyMetadata(null, delegate(DependencyObject depobj, DependencyPropertyChangedEventArgs args)
		{
			FlipViewIndicator fvi = (FlipViewIndicator)depobj;
			FlipView fv = (FlipView)args.NewValue;
			WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(fv.add_SelectionChanged), new Action<EventRegistrationToken>(fv.remove_SelectionChanged), delegate(object s, SelectionChangedEventArgs e)
			{
				fvi.put_ItemsSource(fv.ItemsSource);
			});
			fvi.put_ItemsSource(fv.ItemsSource);
			Binding binding = new Binding();
			binding.put_Mode(3);
			binding.put_Source(fv);
			binding.put_Path(new PropertyPath("SelectedItem"));
			fvi.SetBinding(Selector.SelectedItemProperty, binding);
		}));
	}
}

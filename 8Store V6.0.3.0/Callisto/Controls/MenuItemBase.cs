using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls
{
	// Token: 0x0200000F RID: 15
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as MenuFlyoutItem.")]
	public abstract class MenuItemBase : Control
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000053D7 File Offset: 0x000035D7
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000053E9 File Offset: 0x000035E9
		public Thickness MenuTextMargin
		{
			get
			{
				return (Thickness)base.GetValue(MenuItemBase.MenuTextMarginProperty);
			}
			set
			{
				base.SetValue(MenuItemBase.MenuTextMarginProperty, value);
			}
		}

		// Token: 0x0400005B RID: 91
		public static readonly DependencyProperty MenuTextMarginProperty = DependencyProperty.Register("MenuTextMargin", typeof(Thickness), typeof(MenuItemBase), null);
	}
}

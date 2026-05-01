using System;
using Windows.UI.Xaml;

namespace Callisto.Controls
{
	// Token: 0x02000012 RID: 18
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as ToggleMenuFlyoutItem.")]
	public sealed class ToggleMenuItem : MenuItem
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000565C File Offset: 0x0000385C
		public ToggleMenuItem()
		{
			base.put_DefaultStyleKey(typeof(ToggleMenuItem));
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005674 File Offset: 0x00003874
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005686 File Offset: 0x00003886
		public bool IsChecked
		{
			get
			{
				return (bool)base.GetValue(ToggleMenuItem.IsCheckedProperty);
			}
			set
			{
				base.SetValue(ToggleMenuItem.IsCheckedProperty, value);
			}
		}

		// Token: 0x04000065 RID: 101
		public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleMenuItem), null);
	}
}

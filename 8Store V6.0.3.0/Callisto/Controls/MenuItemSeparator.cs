using System;

namespace Callisto.Controls
{
	// Token: 0x02000011 RID: 17
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as MenuItemSeparator.")]
	public sealed class MenuItemSeparator : MenuItemBase
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x0000563D File Offset: 0x0000383D
		public MenuItemSeparator()
		{
			base.put_DefaultStyleKey(typeof(MenuItemSeparator));
			base.put_IsTabStop(false);
		}
	}
}

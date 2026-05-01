using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Callisto.Effects
{
	// Token: 0x0200002C RID: 44
	internal static class TreeHelpers
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000B1E4 File Offset: 0x000093E4
		public static IEnumerable<FrameworkElement> GetVisualAncestors(this FrameworkElement node)
		{
			for (FrameworkElement parent = node.GetVisualParent(); parent != null; parent = parent.GetVisualParent())
			{
				yield return parent;
			}
			yield break;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B201 File Offset: 0x00009401
		public static FrameworkElement GetVisualParent(this FrameworkElement node)
		{
			return VisualTreeHelper.GetParent(node) as FrameworkElement;
		}
	}
}

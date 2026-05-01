using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Callisto.Controls.Common
{
	// Token: 0x02000007 RID: 7
	internal static class VisualStates
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public static void GoToState(Control control, bool useTransitions, params string[] stateNames)
		{
			foreach (string text in stateNames)
			{
				if (VisualStateManager.GoToState(control, text, useTransitions))
				{
					return;
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C20 File Offset: 0x00000E20
		public static FrameworkElement GetImplementationRoot(DependencyObject dependencyObject)
		{
			if (1 != VisualTreeHelper.GetChildrenCount(dependencyObject))
			{
				return null;
			}
			return VisualTreeHelper.GetChild(dependencyObject, 0) as FrameworkElement;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C58 File Offset: 0x00000E58
		public static VisualStateGroup TryGetVisualStateGroup(DependencyObject dependencyObject, string groupName)
		{
			FrameworkElement implementationRoot = VisualStates.GetImplementationRoot(dependencyObject);
			if (implementationRoot == null)
			{
				return null;
			}
			return Enumerable.FirstOrDefault<VisualStateGroup>(Enumerable.Where<VisualStateGroup>(Enumerable.OfType<VisualStateGroup>(VisualStateManager.GetVisualStateGroups(implementationRoot)), (VisualStateGroup group) => string.CompareOrdinal(groupName, group.Name) == 0));
		}

		// Token: 0x04000016 RID: 22
		public const string GroupCommon = "CommonStates";

		// Token: 0x04000017 RID: 23
		public const string StateNormal = "Normal";

		// Token: 0x04000018 RID: 24
		public const string StateReadOnly = "ReadOnly";

		// Token: 0x04000019 RID: 25
		public const string StatePointerOver = "PointerOver";

		// Token: 0x0400001A RID: 26
		public const string StatePointerPressed = "PointerPressed";

		// Token: 0x0400001B RID: 27
		public const string StatePointerExited = "PointerExited";

		// Token: 0x0400001C RID: 28
		public const string StateDisabled = "Disabled";

		// Token: 0x0400001D RID: 29
		public const string GroupFocus = "FocusStates";

		// Token: 0x0400001E RID: 30
		public const string StateUnfocused = "Unfocused";

		// Token: 0x0400001F RID: 31
		public const string StateFocused = "Focused";

		// Token: 0x04000020 RID: 32
		public const string StateActive = "Active";

		// Token: 0x04000021 RID: 33
		public const string StateInactive = "Inactive";

		// Token: 0x04000022 RID: 34
		public const string GroupActive = "ActiveStates";

		// Token: 0x04000023 RID: 35
		public const string StateVisible = "Visible";

		// Token: 0x04000024 RID: 36
		public const string StateHidden = "Hidden";

		// Token: 0x04000025 RID: 37
		public const string GroupVisibility = "VisibilityStates";

		// Token: 0x04000026 RID: 38
		public const string StateUnwatermarked = "Unwatermarked";

		// Token: 0x04000027 RID: 39
		public const string StateWatermarked = "Watermarked";

		// Token: 0x04000028 RID: 40
		public const string GroupWatermark = "WatermarkStates";
	}
}

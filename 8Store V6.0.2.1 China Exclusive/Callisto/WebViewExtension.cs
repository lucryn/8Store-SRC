using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Callisto
{
	// Token: 0x02000030 RID: 48
	public static class WebViewExtension
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000BE80 File Offset: 0x0000A080
		public static string GetHtmlSource(WebView view)
		{
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}
			return (string)view.GetValue(WebViewExtension.HtmlSourceProperty);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public static void SetHtmlSource(WebView view, string value)
		{
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}
			view.SetValue(WebViewExtension.HtmlSourceProperty, value);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		private static void OnHtmlSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			WebView webView = o as WebView;
			if (webView == null)
			{
				throw new NotSupportedException("The HtmlSource attached dependency property is only valid for WebView instances.");
			}
			string text = e.NewValue as string;
			string text2 = string.IsNullOrEmpty(text) ? "<html></html>" : text;
			webView.NavigateToString(text2);
		}

		// Token: 0x04000107 RID: 263
		public static readonly DependencyProperty HtmlSourceProperty = DependencyProperty.RegisterAttached("HtmlSource", typeof(string), typeof(WebViewExtension), new PropertyMetadata(null, new PropertyChangedCallback(WebViewExtension.OnHtmlSourcePropertyChanged)));
	}
}

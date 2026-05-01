using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Win81StoreRevival.Themes
{
	// Token: 0x02000053 RID: 83
	public class Control : INotifyPropertyChanged
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001C3EB File Offset: 0x0001A5EB
		public static Control Current
		{
			get
			{
				Application application = Application.Current;
				object obj;
				if (application == null)
				{
					obj = null;
				}
				else
				{
					ResourceDictionary resources = application.Resources;
					obj = ((resources != null) ? resources["8S"] : null);
				}
				return (obj as Control) ?? Control._current;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001C41D File Offset: 0x0001A61D
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0001C428 File Offset: 0x0001A628
		public SolidColorBrush Accent
		{
			get
			{
				return this._accent;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (this._accentColor != value.Color)
				{
					this._accentColor = value.Color;
					this._accent = new SolidColorBrush(Control.AdjustForContrast(this._accentColor));
					this.OnPropertyChanged("Accent");
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001C47C File Offset: 0x0001A67C
		public Control()
		{
			try
			{
				string text = ApplicationData.Current.LocalSettings.Values["AccentColor"] as string;
				if (!string.IsNullOrWhiteSpace(text))
				{
					this.SetAccentColor(Control.ParseHex(text));
				}
				else
				{
					this._accent = new SolidColorBrush(Control.AdjustForContrast(this._accentColor));
				}
			}
			catch
			{
				this._accent = new SolidColorBrush(Control.AdjustForContrast(this._accentColor));
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060005DC RID: 1500 RVA: 0x0001C53C File Offset: 0x0001A73C
		// (remove) Token: 0x060005DD RID: 1501 RVA: 0x0001C574 File Offset: 0x0001A774
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060005DE RID: 1502 RVA: 0x0001C5A9 File Offset: 0x0001A7A9
		protected void OnPropertyChanged(string name)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001C5C8 File Offset: 0x0001A7C8
		private static Color ParseHex(string hex)
		{
			hex = (hex ?? "").Replace("#", "");
			if (hex.Length != 6)
			{
				return Color.FromArgb(byte.MaxValue, 0, 170, 79);
			}
			byte b = Convert.ToByte(hex.Substring(0, 2), 16);
			byte b2 = Convert.ToByte(hex.Substring(2, 2), 16);
			byte b3 = Convert.ToByte(hex.Substring(4, 2), 16);
			return Color.FromArgb(byte.MaxValue, b, b2, b3);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001C649 File Offset: 0x0001A849
		private void SetAccentColor(Color color)
		{
			this._accentColor = color;
			this._accent = new SolidColorBrush(Control.AdjustForContrast(color));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001C664 File Offset: 0x0001A864
		private static Color AdjustForContrast(Color color)
		{
			double num = (0.299 * (double)color.R + 0.587 * (double)color.G + 0.114 * (double)color.B) / 255.0;
			double num2 = 1.0;
			if (num > 0.92)
			{
				num2 = 0.52;
			}
			else if (num > 0.82)
			{
				num2 = 0.62;
			}
			else if (num > 0.72)
			{
				num2 = 0.74;
			}
			byte b = (byte)Math.Max(0.0, Math.Min(255.0, (double)color.R * num2));
			byte b2 = (byte)Math.Max(0.0, Math.Min(255.0, (double)color.G * num2));
			byte b3 = (byte)Math.Max(0.0, Math.Min(255.0, (double)color.B * num2));
			return Color.FromArgb(color.A, b, b2, b3);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001C790 File Offset: 0x0001A990
		public void ApplyTheme()
		{
			bool flag = (bool)(ApplicationData.Current.LocalSettings.Values["DarkModeEnabled"] ?? false);
			IList<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
			mergedDictionaries.Clear();
			string text = flag ? "Themes/Dark.xaml" : "Themes/Light.xaml";
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			resourceDictionary.put_Source(new Uri("ms-appx:///" + text));
			mergedDictionaries.Add(resourceDictionary);
			ResourceDictionary resourceDictionary2 = new ResourceDictionary();
			resourceDictionary2.put_Source(new Uri("ms-appx:///Themes/Styles.xaml"));
			mergedDictionaries.Add(resourceDictionary2);
		}

		// Token: 0x04000243 RID: 579
		private static Control _current = new Control();

		// Token: 0x04000244 RID: 580
		private Color _accentColor = Color.FromArgb(byte.MaxValue, 0, 170, 79);

		// Token: 0x04000245 RID: 581
		private SolidColorBrush _accent = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79));
	}
}

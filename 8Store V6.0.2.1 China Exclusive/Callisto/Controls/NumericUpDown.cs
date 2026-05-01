using System;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace Callisto.Controls
{
	// Token: 0x02000018 RID: 24
	public sealed class NumericUpDown : TextBox
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006323 File Offset: 0x00004523
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000632B File Offset: 0x0000452B
		public int Delay
		{
			get
			{
				return this._delay;
			}
			set
			{
				this._delay = value;
				this._incrementButton.put_Delay(this._delay);
				this._decrementButton.put_Delay(this._delay);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006356 File Offset: 0x00004556
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000635E File Offset: 0x0000455E
		public int Interval
		{
			get
			{
				return this._interval;
			}
			set
			{
				this._interval = value;
				this._incrementButton.put_Interval(this._interval);
				this._decrementButton.put_Interval(this._interval);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000638C File Offset: 0x0000458C
		public NumericUpDown()
		{
			base.put_DefaultStyleKey(typeof(NumericUpDown));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_LostFocus), new Action<EventRegistrationToken>(base.remove_LostFocus), new RoutedEventHandler(this.OnTextLostFocus));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_GotFocus), new Action<EventRegistrationToken>(base.remove_GotFocus), new RoutedEventHandler(this.OnTextGotFocus));
			WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(base.add_TextChanged), new Action<EventRegistrationToken>(base.remove_TextChanged), new TextChangedEventHandler(this.OnTextChanged));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000648C File Offset: 0x0000468C
		protected override void OnKeyDown(KeyRoutedEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Handled)
			{
				return;
			}
			switch (e.Key)
			{
			case 38:
				this.DoIncrement();
				e.put_Handled(true);
				return;
			case 39:
				break;
			case 40:
				this.DoDecrement();
				e.put_Handled(true);
				break;
			default:
				return;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000064E1 File Offset: 0x000046E1
		private void OnTextGotFocus(object sender, RoutedEventArgs e)
		{
			if (base.Text != null && base.SelectionLength == 0 && base.Text != null)
			{
				base.Select(0, base.Text.Length);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000650D File Offset: 0x0000470D
		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			this.ProcessUserInput();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006515 File Offset: 0x00004715
		private void OnTextLostFocus(object sender, RoutedEventArgs e)
		{
			this.ProcessUserInput();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006520 File Offset: 0x00004720
		private void ProcessUserInput()
		{
			if (base.Text != null && string.Compare(this._text, base.Text, 0) != 0)
			{
				int selectionStart = base.SelectionStart;
				this._text = base.Text;
				this.ApplyValue(this._text);
				if (selectionStart < base.Text.Length)
				{
					base.put_SelectionStart(selectionStart);
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006580 File Offset: 0x00004780
		private void ApplyValue(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					double num = double.Parse(text, CultureInfo.CurrentCulture);
					if (num >= this.Minimum && num <= this.Maximum)
					{
						this.Value = num;
					}
					this.SetTextBoxText();
					return;
				}
				catch
				{
					this.SetTextBoxText();
					return;
				}
			}
			this.Value = this.Minimum;
			base.put_Text(string.Empty);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000065F4 File Offset: 0x000047F4
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this._incrementButton = (base.GetTemplateChild("PART_IncrementButton") as RepeatButton);
			this._decrementButton = (base.GetTemplateChild("PART_DecrementButton") as RepeatButton);
			if (this._incrementButton != null)
			{
				RepeatButton incrementButton = this._incrementButton;
				WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(incrementButton.add_SizeChanged), new Action<EventRegistrationToken>(incrementButton.remove_SizeChanged), new SizeChangedEventHandler(NumericUpDown.ResizePartButton));
				this._incrementButton.put_Delay(this._delay);
				this._incrementButton.put_Interval(this._interval);
				RepeatButton incrementButton2 = this._incrementButton;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(incrementButton2.add_Click), new Action<EventRegistrationToken>(incrementButton2.remove_Click), new RoutedEventHandler(this.OnIncrementClicked));
			}
			if (this._decrementButton != null)
			{
				RepeatButton decrementButton = this._decrementButton;
				WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(new Func<SizeChangedEventHandler, EventRegistrationToken>(decrementButton.add_SizeChanged), new Action<EventRegistrationToken>(decrementButton.remove_SizeChanged), new SizeChangedEventHandler(NumericUpDown.ResizePartButton));
				this._decrementButton.put_Delay(this._delay);
				this._decrementButton.put_Interval(this._interval);
				RepeatButton decrementButton2 = this._decrementButton;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(decrementButton2.add_Click), new Action<EventRegistrationToken>(decrementButton2.remove_Click), new RoutedEventHandler(this.OnDecrementClicked));
			}
			this.SetValidIncrementDirection();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006753 File Offset: 0x00004953
		private void OnDecrementClicked(object sender, RoutedEventArgs e)
		{
			this.DoDecrement();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000675B File Offset: 0x0000495B
		private void DoDecrement()
		{
			if (this._canDecrement)
			{
				this.Value = (double)((decimal)this.Value - (decimal)this.Increment);
				this._requestedVal = this.Value;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000679A File Offset: 0x0000499A
		private void OnIncrementClicked(object sender, RoutedEventArgs e)
		{
			this.DoIncrement();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000067A2 File Offset: 0x000049A2
		private void DoIncrement()
		{
			if (this._canIncrement)
			{
				this.Value = (double)((decimal)this.Value + (decimal)this.Increment);
				this._requestedVal = this.Value;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000067E4 File Offset: 0x000049E4
		private static void ResizePartButton(object s, SizeChangedEventArgs e)
		{
			RepeatButton repeatButton = s as RepeatButton;
			if (repeatButton != null)
			{
				double width = repeatButton.Width;
				if (width != e.NewSize.Height)
				{
					repeatButton.put_Width(e.NewSize.Height);
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006828 File Offset: 0x00004A28
		private void SetValidIncrementDirection()
		{
			if (this.Value < this.Maximum)
			{
				VisualStateManager.GoToState(this, "IncrementEnabled", true);
				this._canIncrement = true;
			}
			if (this.Value > this.Minimum)
			{
				VisualStateManager.GoToState(this, "DecrementEnabled", true);
				this._canDecrement = true;
			}
			if (Math.Round(this.Value, this.DecimalPlaces) == Math.Round(this.Maximum, this.DecimalPlaces))
			{
				VisualStateManager.GoToState(this, "IncrementDisabled", true);
				this._canIncrement = false;
			}
			if (Math.Round(this.Value, this.DecimalPlaces) == Math.Round(this.Minimum, this.DecimalPlaces))
			{
				VisualStateManager.GoToState(this, "DecrementDisabled", true);
				this._canDecrement = false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000068E9 File Offset: 0x00004AE9
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000068FB File Offset: 0x00004AFB
		public double Minimum
		{
			get
			{
				return (double)base.GetValue(NumericUpDown.MinimumProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.MinimumProperty, value);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006910 File Offset: 0x00004B10
		private static void OnMinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)d;
			NumericUpDown.EnsureValidDoubleValue(d, e);
			if (numericUpDown._levelsFromRootCall == 0)
			{
				numericUpDown._requestedMin = (double)e.NewValue;
				numericUpDown._initialMin = (double)e.OldValue;
				numericUpDown._initialMax = numericUpDown.Maximum;
				numericUpDown._initialVal = numericUpDown.Value;
				numericUpDown._levelsFromRootCall++;
				if (numericUpDown.Minimum != numericUpDown._requestedMin)
				{
					numericUpDown.Minimum = numericUpDown._requestedMin;
				}
				numericUpDown._levelsFromRootCall--;
			}
			numericUpDown._levelsFromRootCall++;
			numericUpDown.CoerceMaximum();
			numericUpDown.CoerceValue();
			numericUpDown._levelsFromRootCall--;
			if (numericUpDown._levelsFromRootCall == 0)
			{
				numericUpDown.SetValidIncrementDirection();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000069D9 File Offset: 0x00004BD9
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000069EB File Offset: 0x00004BEB
		public double Maximum
		{
			get
			{
				return (double)base.GetValue(NumericUpDown.MaximumProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.MaximumProperty, value);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006A00 File Offset: 0x00004C00
		private static void OnMaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown.EnsureValidDoubleValue(d, e);
			NumericUpDown numericUpDown = d as NumericUpDown;
			if (numericUpDown._levelsFromRootCall == 0)
			{
				numericUpDown._requestedMax = (double)e.NewValue;
				numericUpDown._initialMax = (double)e.OldValue;
				numericUpDown._initialVal = numericUpDown.Value;
			}
			numericUpDown._levelsFromRootCall++;
			numericUpDown.CoerceMaximum();
			numericUpDown.CoerceValue();
			numericUpDown._levelsFromRootCall--;
			if (numericUpDown._levelsFromRootCall == 0)
			{
				numericUpDown.SetValidIncrementDirection();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006A87 File Offset: 0x00004C87
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006A99 File Offset: 0x00004C99
		public double Increment
		{
			get
			{
				return (double)base.GetValue(NumericUpDown.IncrementProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.IncrementProperty, value);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006AAC File Offset: 0x00004CAC
		private static void OnIncrementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)d;
			NumericUpDown.EnsureValidIncrementValue(d, e);
			if (numericUpDown._levelsFromRootCall == 0)
			{
				numericUpDown._requestedInc = (double)e.NewValue;
				numericUpDown._initialInc = (double)e.OldValue;
				numericUpDown._levelsFromRootCall++;
				if (numericUpDown.Increment != numericUpDown._requestedInc)
				{
					numericUpDown.Increment = numericUpDown._requestedInc;
				}
				numericUpDown._levelsFromRootCall--;
			}
			if (numericUpDown._levelsFromRootCall == 0)
			{
				double increment = numericUpDown.Increment;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006B36 File Offset: 0x00004D36
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00006B48 File Offset: 0x00004D48
		public int DecimalPlaces
		{
			get
			{
				return (int)base.GetValue(NumericUpDown.DecimalPlacesProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.DecimalPlacesProperty, value);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006B5C File Offset: 0x00004D5C
		private static void OnDecimalPlacesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown.EnsureValidDecimalPlacesValue(d, e);
			NumericUpDown numericUpDown = d as NumericUpDown;
			numericUpDown.OnDecimalPlacesChanged((int)e.OldValue, (int)e.NewValue);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006B94 File Offset: 0x00004D94
		private void OnDecimalPlacesChanged(int oldValue, int newValue)
		{
			this._formatString = string.Format(NumberFormatInfo.InvariantInfo, "F{0:D}", new object[]
			{
				newValue
			});
			this._levelsFromRootCall++;
			this.SetTextBoxText();
			this._levelsFromRootCall--;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006BEC File Offset: 0x00004DEC
		private string FormatValue()
		{
			return this.Value.ToString(this._formatString, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006C12 File Offset: 0x00004E12
		internal void SetTextBoxText()
		{
			if (base.Text != null)
			{
				this._text = (this.FormatValue() ?? string.Empty);
				base.put_Text(this._text);
				base.put_SelectionStart(this._text.Length);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006C4E File Offset: 0x00004E4E
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00006C60 File Offset: 0x00004E60
		public double Value
		{
			get
			{
				return (double)base.GetValue(NumericUpDown.ValueProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.ValueProperty, value);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006C74 File Offset: 0x00004E74
		private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)d;
			numericUpDown.OnValueChanged((double)e.NewValue);
			numericUpDown.SetValidIncrementDirection();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006C9F File Offset: 0x00004E9F
		private void OnValueChanged(double newValue)
		{
			this.SetTextBoxText();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006CA8 File Offset: 0x00004EA8
		private void CoerceMaximum()
		{
			double minimum = this.Minimum;
			double maximum = this.Maximum;
			if (this._requestedMax != maximum)
			{
				if (this._requestedMax >= minimum)
				{
					base.SetValue(NumericUpDown.MaximumProperty, this._requestedMax);
					return;
				}
				if (maximum != minimum)
				{
					base.SetValue(NumericUpDown.MaximumProperty, minimum);
					return;
				}
			}
			else if (maximum < minimum)
			{
				base.SetValue(NumericUpDown.MaximumProperty, minimum);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006D18 File Offset: 0x00004F18
		private void CoerceValue()
		{
			double minimum = this.Minimum;
			double maximum = this.Maximum;
			double value = this.Value;
			if (this._requestedVal != value)
			{
				if (this._requestedVal >= minimum && this._requestedVal <= maximum)
				{
					base.SetValue(NumericUpDown.ValueProperty, this._requestedVal);
					return;
				}
				if (this._requestedVal < minimum && value != minimum)
				{
					base.SetValue(NumericUpDown.ValueProperty, minimum);
					return;
				}
				if (this._requestedVal > maximum && value != maximum)
				{
					base.SetValue(NumericUpDown.ValueProperty, maximum);
					return;
				}
			}
			else
			{
				if (value < minimum)
				{
					base.SetValue(NumericUpDown.ValueProperty, minimum);
					return;
				}
				if (value > maximum)
				{
					base.SetValue(NumericUpDown.ValueProperty, maximum);
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006DD5 File Offset: 0x00004FD5
		private static bool IsValidDoubleValue(object value, out double number)
		{
			number = (double)value;
			return !double.IsNaN(number) && !double.IsInfinity(number) && number <= 7.922816251426434E+28 && number >= -7.922816251426434E+28;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006E10 File Offset: 0x00005010
		private static void EnsureValidDoubleValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown.EnsureValidDoubleValue(d, e.Property, e.OldValue, e.NewValue);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006E2C File Offset: 0x0000502C
		private static void EnsureValidDoubleValue(DependencyObject d, DependencyProperty property, object oldValue, object newValue)
		{
			NumericUpDown numericUpDown = d as NumericUpDown;
			double num;
			if (!NumericUpDown.IsValidDoubleValue(newValue, out num))
			{
				numericUpDown._levelsFromRootCall++;
				numericUpDown.SetValue(property, oldValue);
				numericUpDown._levelsFromRootCall--;
				string text = string.Format(CultureInfo.InvariantCulture, "Not a valid Double value", new object[]
				{
					newValue
				});
				throw new ArgumentException(text, "newValue");
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006E98 File Offset: 0x00005098
		private static void EnsureValidIncrementValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)d;
			double num;
			if (!NumericUpDown.IsValidDoubleValue(e.NewValue, out num) || num <= 0.0)
			{
				numericUpDown._levelsFromRootCall++;
				numericUpDown.SetValue(e.Property, e.OldValue);
				numericUpDown._levelsFromRootCall--;
				string text = string.Format(CultureInfo.InvariantCulture, "Not a valie Double value", new object[]
				{
					e.NewValue
				});
				throw new ArgumentException(text, "e");
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006F24 File Offset: 0x00005124
		private static void EnsureValidDecimalPlacesValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown numericUpDown = d as NumericUpDown;
			int num = (int)e.NewValue;
			if (num < 0 || num > 15)
			{
				numericUpDown._levelsFromRootCall++;
				numericUpDown.DecimalPlaces = (int)e.OldValue;
				numericUpDown._levelsFromRootCall--;
				string text = string.Format(CultureInfo.InvariantCulture, "Not a valid Double value", new object[]
				{
					e.NewValue
				});
				throw new ArgumentException(text, "e");
			}
		}

		// Token: 0x0400007D RID: 125
		private RepeatButton _incrementButton;

		// Token: 0x0400007E RID: 126
		private RepeatButton _decrementButton;

		// Token: 0x0400007F RID: 127
		private string _text;

		// Token: 0x04000080 RID: 128
		private bool _canIncrement;

		// Token: 0x04000081 RID: 129
		private bool _canDecrement;

		// Token: 0x04000082 RID: 130
		private int _delay = 500;

		// Token: 0x04000083 RID: 131
		private int _interval = 100;

		// Token: 0x04000084 RID: 132
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(0.0, new PropertyChangedCallback(NumericUpDown.OnMinimumPropertyChanged)));

		// Token: 0x04000085 RID: 133
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(100.0, new PropertyChangedCallback(NumericUpDown.OnMaximumPropertyChanged)));

		// Token: 0x04000086 RID: 134
		public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register("Increment", typeof(double), typeof(NumericUpDown), new PropertyMetadata(1.0, new PropertyChangedCallback(NumericUpDown.OnIncrementPropertyChanged)));

		// Token: 0x04000087 RID: 135
		public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, new PropertyChangedCallback(NumericUpDown.OnDecimalPlacesPropertyChanged)));

		// Token: 0x04000088 RID: 136
		private string _formatString = "F0";

		// Token: 0x04000089 RID: 137
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown), new PropertyMetadata(0.0, new PropertyChangedCallback(NumericUpDown.OnValuePropertyChanged)));

		// Token: 0x0400008A RID: 138
		private int _levelsFromRootCall;

		// Token: 0x0400008B RID: 139
		private double _initialInc = 1.0;

		// Token: 0x0400008C RID: 140
		private double _initialMin;

		// Token: 0x0400008D RID: 141
		private double _initialMax = 100.0;

		// Token: 0x0400008E RID: 142
		private double _initialVal;

		// Token: 0x0400008F RID: 143
		private double _requestedInc = 1.0;

		// Token: 0x04000090 RID: 144
		private double _requestedMin;

		// Token: 0x04000091 RID: 145
		private double _requestedMax = 100.0;

		// Token: 0x04000092 RID: 146
		private double _requestedVal;
	}
}

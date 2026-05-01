using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls
{
	// Token: 0x02000008 RID: 8
	public sealed class DropDownButton : Button
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002C9F File Offset: 0x00000E9F
		public DropDownButton()
		{
			base.put_DefaultStyleKey(typeof(DropDownButton));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CB7 File Offset: 0x00000EB7
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this._arrowGlyph = (base.GetTemplateChild("PART_ArrowGlyph") as TextBlock);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CD5 File Offset: 0x00000ED5
		protected override Size MeasureOverride(Size availableSize)
		{
			this.EvaluateArrowGlyph();
			return base.MeasureOverride(availableSize);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private void EvaluateArrowGlyph()
		{
			if (this._arrowGlyph == null)
			{
				return;
			}
			if (base.FontSize <= 12.0)
			{
				this._arrowGlyph.put_Text("");
			}
			if (base.FontSize >= 13.333 && base.FontSize < 21.333)
			{
				this._arrowGlyph.put_Text("");
			}
			if (base.FontSize >= 21.333 && base.FontSize < 40.0)
			{
				this._arrowGlyph.put_Text("");
			}
			if (base.FontSize > 40.0)
			{
				this._arrowGlyph.put_Text("");
			}
		}

		// Token: 0x04000029 RID: 41
		private const string PART_ARROW_GLYPH = "PART_ArrowGlyph";

		// Token: 0x0400002A RID: 42
		private const string ARROW_GLYPH_XSMALL = "";

		// Token: 0x0400002B RID: 43
		private const string ARROW_GLYPH_SMALL = "";

		// Token: 0x0400002C RID: 44
		private const string ARROW_GLYPH_MEDIUM = "";

		// Token: 0x0400002D RID: 45
		private const string ARROW_GLYPH_LARGE = "";

		// Token: 0x0400002E RID: 46
		private TextBlock _arrowGlyph;
	}
}

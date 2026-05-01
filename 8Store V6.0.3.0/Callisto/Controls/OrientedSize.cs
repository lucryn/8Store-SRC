using System;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls
{
	// Token: 0x02000022 RID: 34
	internal struct OrientedSize
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009203 File Offset: 0x00007403
		public Orientation Orientation
		{
			get
			{
				return this._orientation;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000920B File Offset: 0x0000740B
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00009213 File Offset: 0x00007413
		public double Direct
		{
			get
			{
				return this._direct;
			}
			set
			{
				this._direct = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000921C File Offset: 0x0000741C
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00009224 File Offset: 0x00007424
		public double Indirect
		{
			get
			{
				return this._indirect;
			}
			set
			{
				this._indirect = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000922D File Offset: 0x0000742D
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00009245 File Offset: 0x00007445
		public double Width
		{
			get
			{
				if (this.Orientation != 1)
				{
					return this.Indirect;
				}
				return this.Direct;
			}
			set
			{
				if (this.Orientation == 1)
				{
					this.Direct = value;
					return;
				}
				this.Indirect = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000925F File Offset: 0x0000745F
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009277 File Offset: 0x00007477
		public double Height
		{
			get
			{
				if (this.Orientation == 1)
				{
					return this.Indirect;
				}
				return this.Direct;
			}
			set
			{
				if (this.Orientation != 1)
				{
					this.Direct = value;
					return;
				}
				this.Indirect = value;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009291 File Offset: 0x00007491
		public OrientedSize(Orientation orientation)
		{
			this = new OrientedSize(orientation, 0.0, 0.0);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000092AC File Offset: 0x000074AC
		public OrientedSize(Orientation orientation, double width, double height)
		{
			this._orientation = orientation;
			this._direct = 0.0;
			this._indirect = 0.0;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x040000DA RID: 218
		private Orientation _orientation;

		// Token: 0x040000DB RID: 219
		private double _direct;

		// Token: 0x040000DC RID: 220
		private double _indirect;
	}
}

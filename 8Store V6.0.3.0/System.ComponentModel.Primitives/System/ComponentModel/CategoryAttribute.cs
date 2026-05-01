using System;

namespace System.ComponentModel
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(32767)]
	public class CategoryAttribute : Attribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002324 File Offset: 0x00000524
		public static CategoryAttribute Action
		{
			get
			{
				if (CategoryAttribute.s_action == null)
				{
					CategoryAttribute.s_action = new CategoryAttribute("Action");
				}
				return CategoryAttribute.s_action;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002347 File Offset: 0x00000547
		public static CategoryAttribute Appearance
		{
			get
			{
				if (CategoryAttribute.s_appearance == null)
				{
					CategoryAttribute.s_appearance = new CategoryAttribute("Appearance");
				}
				return CategoryAttribute.s_appearance;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000236A File Offset: 0x0000056A
		public static CategoryAttribute Asynchronous
		{
			get
			{
				if (CategoryAttribute.s_asynchronous == null)
				{
					CategoryAttribute.s_asynchronous = new CategoryAttribute("Asynchronous");
				}
				return CategoryAttribute.s_asynchronous;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000238D File Offset: 0x0000058D
		public static CategoryAttribute Behavior
		{
			get
			{
				if (CategoryAttribute.s_behavior == null)
				{
					CategoryAttribute.s_behavior = new CategoryAttribute("Behavior");
				}
				return CategoryAttribute.s_behavior;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023B0 File Offset: 0x000005B0
		public static CategoryAttribute Data
		{
			get
			{
				if (CategoryAttribute.s_data == null)
				{
					CategoryAttribute.s_data = new CategoryAttribute("Data");
				}
				return CategoryAttribute.s_data;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023D3 File Offset: 0x000005D3
		public static CategoryAttribute Default
		{
			get
			{
				if (CategoryAttribute.s_defAttr == null)
				{
					CategoryAttribute.s_defAttr = new CategoryAttribute();
				}
				return CategoryAttribute.s_defAttr;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023F1 File Offset: 0x000005F1
		public static CategoryAttribute Design
		{
			get
			{
				if (CategoryAttribute.s_design == null)
				{
					CategoryAttribute.s_design = new CategoryAttribute("Design");
				}
				return CategoryAttribute.s_design;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002414 File Offset: 0x00000614
		public static CategoryAttribute DragDrop
		{
			get
			{
				if (CategoryAttribute.s_dragDrop == null)
				{
					CategoryAttribute.s_dragDrop = new CategoryAttribute("DragDrop");
				}
				return CategoryAttribute.s_dragDrop;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002437 File Offset: 0x00000637
		public static CategoryAttribute Focus
		{
			get
			{
				if (CategoryAttribute.s_focus == null)
				{
					CategoryAttribute.s_focus = new CategoryAttribute("Focus");
				}
				return CategoryAttribute.s_focus;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000245A File Offset: 0x0000065A
		public static CategoryAttribute Format
		{
			get
			{
				if (CategoryAttribute.s_format == null)
				{
					CategoryAttribute.s_format = new CategoryAttribute("Format");
				}
				return CategoryAttribute.s_format;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000247D File Offset: 0x0000067D
		public static CategoryAttribute Key
		{
			get
			{
				if (CategoryAttribute.s_key == null)
				{
					CategoryAttribute.s_key = new CategoryAttribute("Key");
				}
				return CategoryAttribute.s_key;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000024A0 File Offset: 0x000006A0
		public static CategoryAttribute Layout
		{
			get
			{
				if (CategoryAttribute.s_layout == null)
				{
					CategoryAttribute.s_layout = new CategoryAttribute("Layout");
				}
				return CategoryAttribute.s_layout;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024C3 File Offset: 0x000006C3
		public static CategoryAttribute Mouse
		{
			get
			{
				if (CategoryAttribute.s_mouse == null)
				{
					CategoryAttribute.s_mouse = new CategoryAttribute("Mouse");
				}
				return CategoryAttribute.s_mouse;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000024E6 File Offset: 0x000006E6
		public static CategoryAttribute WindowStyle
		{
			get
			{
				if (CategoryAttribute.s_windowStyle == null)
				{
					CategoryAttribute.s_windowStyle = new CategoryAttribute("WindowStyle");
				}
				return CategoryAttribute.s_windowStyle;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002509 File Offset: 0x00000709
		public CategoryAttribute() : this("Default")
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002516 File Offset: 0x00000716
		public CategoryAttribute(string category)
		{
			this._categoryValue = category;
			this._localized = false;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000252C File Offset: 0x0000072C
		public string Category
		{
			get
			{
				if (!this._localized)
				{
					this._localized = true;
					string localizedString = this.GetLocalizedString(this._categoryValue);
					if (localizedString != null)
					{
						this._categoryValue = localizedString;
					}
				}
				return this._categoryValue;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002568 File Offset: 0x00000768
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			CategoryAttribute categoryAttribute = obj as CategoryAttribute;
			return categoryAttribute != null && this.Category.Equals(categoryAttribute.Category);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002598 File Offset: 0x00000798
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025A5 File Offset: 0x000007A5
		protected virtual string GetLocalizedString(string value)
		{
			return SR.GetResourceString("PropertyCategory" + value, null);
		}

		// Token: 0x04000007 RID: 7
		private static volatile CategoryAttribute s_action;

		// Token: 0x04000008 RID: 8
		private static volatile CategoryAttribute s_appearance;

		// Token: 0x04000009 RID: 9
		private static volatile CategoryAttribute s_asynchronous;

		// Token: 0x0400000A RID: 10
		private static volatile CategoryAttribute s_behavior;

		// Token: 0x0400000B RID: 11
		private static volatile CategoryAttribute s_data;

		// Token: 0x0400000C RID: 12
		private static volatile CategoryAttribute s_design;

		// Token: 0x0400000D RID: 13
		private static volatile CategoryAttribute s_dragDrop;

		// Token: 0x0400000E RID: 14
		private static volatile CategoryAttribute s_defAttr;

		// Token: 0x0400000F RID: 15
		private static volatile CategoryAttribute s_focus;

		// Token: 0x04000010 RID: 16
		private static volatile CategoryAttribute s_format;

		// Token: 0x04000011 RID: 17
		private static volatile CategoryAttribute s_key;

		// Token: 0x04000012 RID: 18
		private static volatile CategoryAttribute s_layout;

		// Token: 0x04000013 RID: 19
		private static volatile CategoryAttribute s_mouse;

		// Token: 0x04000014 RID: 20
		private static volatile CategoryAttribute s_windowStyle;

		// Token: 0x04000015 RID: 21
		private bool _localized;

		// Token: 0x04000016 RID: 22
		private string _categoryValue;
	}
}

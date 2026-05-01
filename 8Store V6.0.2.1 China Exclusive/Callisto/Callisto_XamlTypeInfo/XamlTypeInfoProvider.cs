using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Callisto.Controls;
using Callisto.Controls.Primitives;
using Callisto.Converters;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Callisto.Callisto_XamlTypeInfo
{
	// Token: 0x02000040 RID: 64
	[DebuggerNonUserCode]
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	internal class XamlTypeInfoProvider
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		public IXamlType GetXamlTypeByType(Type type)
		{
			IXamlType xamlType;
			if (this._xamlTypeCacheByType.TryGetValue(type, ref xamlType))
			{
				return xamlType;
			}
			int num = this.LookupTypeIndexByType(type);
			if (num != -1)
			{
				xamlType = this.CreateXamlType(num);
			}
			if (xamlType != null)
			{
				this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
				this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
			}
			return xamlType;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000CF40 File Offset: 0x0000B140
		public IXamlType GetXamlTypeByName(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return null;
			}
			IXamlType xamlType;
			if (this._xamlTypeCacheByName.TryGetValue(typeName, ref xamlType))
			{
				return xamlType;
			}
			int num = this.LookupTypeIndexByName(typeName);
			if (num != -1)
			{
				xamlType = this.CreateXamlType(num);
			}
			if (xamlType != null)
			{
				this._xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
				this._xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
			}
			return xamlType;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		public IXamlMember GetMemberByLongName(string longMemberName)
		{
			if (string.IsNullOrEmpty(longMemberName))
			{
				return null;
			}
			IXamlMember xamlMember;
			if (this._xamlMembers.TryGetValue(longMemberName, ref xamlMember))
			{
				return xamlMember;
			}
			xamlMember = this.CreateXamlMember(longMemberName);
			if (xamlMember != null)
			{
				this._xamlMembers.Add(longMemberName, xamlMember);
			}
			return xamlMember;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		private void InitTypeTables()
		{
			this._typeNameTable = new string[53];
			this._typeNameTable[0] = "Callisto.Converters.BrushToColorConverter";
			this._typeNameTable[1] = "Object";
			this._typeNameTable[2] = "Callisto.Converters.ColorBrightnessConverter";
			this._typeNameTable[3] = "Callisto.Converters.ColorContrastConverter";
			this._typeNameTable[4] = "Callisto.Controls.Menu";
			this._typeNameTable[5] = "Windows.UI.Xaml.Controls.Control";
			this._typeNameTable[6] = "System.Collections.ObjectModel.ObservableCollection`1<Callisto.Controls.MenuItemBase>";
			this._typeNameTable[7] = "System.Collections.ObjectModel.Collection`1<Callisto.Controls.MenuItemBase>";
			this._typeNameTable[8] = "Callisto.Controls.MenuItemBase";
			this._typeNameTable[9] = "Windows.UI.Xaml.Thickness";
			this._typeNameTable[10] = "Callisto.Controls.MenuItem";
			this._typeNameTable[11] = "String";
			this._typeNameTable[12] = "System.Windows.Input.ICommand";
			this._typeNameTable[13] = "Callisto.Controls.ToggleMenuItem";
			this._typeNameTable[14] = "Boolean";
			this._typeNameTable[15] = "Callisto.Controls.MenuItemSeparator";
			this._typeNameTable[16] = "Callisto.Controls.Flyout";
			this._typeNameTable[17] = "Windows.UI.Xaml.Controls.ContentControl";
			this._typeNameTable[18] = "Windows.UI.Xaml.Controls.Primitives.Popup";
			this._typeNameTable[19] = "Windows.UI.Xaml.UIElement";
			this._typeNameTable[20] = "Windows.UI.Xaml.Controls.Primitives.PlacementMode";
			this._typeNameTable[21] = "Double";
			this._typeNameTable[22] = "Callisto.Controls.SettingsFlyout";
			this._typeNameTable[23] = "Windows.UI.Xaml.Media.SolidColorBrush";
			this._typeNameTable[24] = "Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth";
			this._typeNameTable[25] = "System.Enum";
			this._typeNameTable[26] = "System.ValueType";
			this._typeNameTable[27] = "Windows.UI.Xaml.Media.ImageSource";
			this._typeNameTable[28] = "Callisto.Controls.LiveTile";
			this._typeNameTable[29] = "Windows.UI.Xaml.DataTemplate";
			this._typeNameTable[30] = "Callisto.Controls.LiveTile.SlideDirection";
			this._typeNameTable[31] = "Callisto.Controls.RatingItem";
			this._typeNameTable[32] = "Windows.UI.Xaml.Controls.Primitives.ButtonBase";
			this._typeNameTable[33] = "Callisto.Controls.Rating";
			this._typeNameTable[34] = "Windows.UI.Xaml.Controls.ItemsControl";
			this._typeNameTable[35] = "Int32";
			this._typeNameTable[36] = "Callisto.Controls.RatingSelectionMode";
			this._typeNameTable[37] = "Callisto.Controls.FlipViewIndicator";
			this._typeNameTable[38] = "Windows.UI.Xaml.Controls.ListBox";
			this._typeNameTable[39] = "Windows.UI.Xaml.Controls.FlipView";
			this._typeNameTable[40] = "Callisto.Controls.WatermarkTextBox";
			this._typeNameTable[41] = "Windows.UI.Xaml.Controls.TextBox";
			this._typeNameTable[42] = "Callisto.Controls.NumericUpDown";
			this._typeNameTable[43] = "Callisto.Controls.CustomDialog";
			this._typeNameTable[44] = "Windows.UI.Xaml.Visibility";
			this._typeNameTable[45] = "Callisto.Controls.DropDownButton";
			this._typeNameTable[46] = "Windows.UI.Xaml.Controls.Button";
			this._typeNameTable[47] = "Callisto.Controls.DynamicTextBlock";
			this._typeNameTable[48] = "Windows.UI.Xaml.TextWrapping";
			this._typeNameTable[49] = "Windows.UI.Xaml.LineStackingStrategy";
			this._typeNameTable[50] = "Callisto.Controls.Primitives.LinearClipper";
			this._typeNameTable[51] = "Callisto.Controls.Primitives.Clipper";
			this._typeNameTable[52] = "Callisto.Controls.ExpandDirection";
			this._typeTable = new Type[53];
			this._typeTable[0] = typeof(BrushToColorConverter);
			this._typeTable[1] = typeof(object);
			this._typeTable[2] = typeof(ColorBrightnessConverter);
			this._typeTable[3] = typeof(ColorContrastConverter);
			this._typeTable[4] = typeof(Menu);
			this._typeTable[5] = typeof(Control);
			this._typeTable[6] = typeof(ObservableCollection<MenuItemBase>);
			this._typeTable[7] = typeof(Collection<MenuItemBase>);
			this._typeTable[8] = typeof(MenuItemBase);
			this._typeTable[9] = typeof(Thickness);
			this._typeTable[10] = typeof(MenuItem);
			this._typeTable[11] = typeof(string);
			this._typeTable[12] = typeof(ICommand);
			this._typeTable[13] = typeof(ToggleMenuItem);
			this._typeTable[14] = typeof(bool);
			this._typeTable[15] = typeof(MenuItemSeparator);
			this._typeTable[16] = typeof(Flyout);
			this._typeTable[17] = typeof(ContentControl);
			this._typeTable[18] = typeof(Popup);
			this._typeTable[19] = typeof(UIElement);
			this._typeTable[20] = typeof(PlacementMode);
			this._typeTable[21] = typeof(double);
			this._typeTable[22] = typeof(SettingsFlyout);
			this._typeTable[23] = typeof(SolidColorBrush);
			this._typeTable[24] = typeof(SettingsFlyout.SettingsFlyoutWidth);
			this._typeTable[25] = typeof(Enum);
			this._typeTable[26] = typeof(ValueType);
			this._typeTable[27] = typeof(ImageSource);
			this._typeTable[28] = typeof(LiveTile);
			this._typeTable[29] = typeof(DataTemplate);
			this._typeTable[30] = typeof(LiveTile.SlideDirection);
			this._typeTable[31] = typeof(RatingItem);
			this._typeTable[32] = typeof(ButtonBase);
			this._typeTable[33] = typeof(Rating);
			this._typeTable[34] = typeof(ItemsControl);
			this._typeTable[35] = typeof(int);
			this._typeTable[36] = typeof(RatingSelectionMode);
			this._typeTable[37] = typeof(FlipViewIndicator);
			this._typeTable[38] = typeof(ListBox);
			this._typeTable[39] = typeof(FlipView);
			this._typeTable[40] = typeof(WatermarkTextBox);
			this._typeTable[41] = typeof(TextBox);
			this._typeTable[42] = typeof(NumericUpDown);
			this._typeTable[43] = typeof(CustomDialog);
			this._typeTable[44] = typeof(Visibility);
			this._typeTable[45] = typeof(DropDownButton);
			this._typeTable[46] = typeof(Button);
			this._typeTable[47] = typeof(DynamicTextBlock);
			this._typeTable[48] = typeof(TextWrapping);
			this._typeTable[49] = typeof(LineStackingStrategy);
			this._typeTable[50] = typeof(LinearClipper);
			this._typeTable[51] = typeof(Clipper);
			this._typeTable[52] = typeof(ExpandDirection);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		private int LookupTypeIndexByName(string typeName)
		{
			if (this._typeNameTable == null)
			{
				this.InitTypeTables();
			}
			for (int i = 0; i < this._typeNameTable.Length; i++)
			{
				if (string.CompareOrdinal(this._typeNameTable[i], typeName) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000D71C File Offset: 0x0000B91C
		private int LookupTypeIndexByType(Type type)
		{
			if (this._typeTable == null)
			{
				this.InitTypeTables();
			}
			for (int i = 0; i < this._typeTable.Length; i++)
			{
				if (type == this._typeTable[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D758 File Offset: 0x0000B958
		private object Activate_0_BrushToColorConverter()
		{
			return new BrushToColorConverter();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000D75F File Offset: 0x0000B95F
		private object Activate_2_ColorBrightnessConverter()
		{
			return new ColorBrightnessConverter();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000D766 File Offset: 0x0000B966
		private object Activate_3_ColorContrastConverter()
		{
			return new ColorContrastConverter();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D76D File Offset: 0x0000B96D
		private object Activate_4_Menu()
		{
			return new Menu();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000D774 File Offset: 0x0000B974
		private object Activate_6_ObservableCollection()
		{
			return new ObservableCollection<MenuItemBase>();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D77B File Offset: 0x0000B97B
		private object Activate_7_Collection()
		{
			return new Collection<MenuItemBase>();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000D782 File Offset: 0x0000B982
		private object Activate_10_MenuItem()
		{
			return new MenuItem();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000D789 File Offset: 0x0000B989
		private object Activate_13_ToggleMenuItem()
		{
			return new ToggleMenuItem();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000D790 File Offset: 0x0000B990
		private object Activate_15_MenuItemSeparator()
		{
			return new MenuItemSeparator();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000D797 File Offset: 0x0000B997
		private object Activate_16_Flyout()
		{
			return new Flyout();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D79E File Offset: 0x0000B99E
		private object Activate_22_SettingsFlyout()
		{
			return new SettingsFlyout();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000D7A5 File Offset: 0x0000B9A5
		private object Activate_28_LiveTile()
		{
			return new LiveTile();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		private object Activate_31_RatingItem()
		{
			return new RatingItem();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000D7B3 File Offset: 0x0000B9B3
		private object Activate_33_Rating()
		{
			return new Rating();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000D7BA File Offset: 0x0000B9BA
		private object Activate_37_FlipViewIndicator()
		{
			return new FlipViewIndicator();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000D7C1 File Offset: 0x0000B9C1
		private object Activate_40_WatermarkTextBox()
		{
			return new WatermarkTextBox();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		private object Activate_42_NumericUpDown()
		{
			return new NumericUpDown();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D7CF File Offset: 0x0000B9CF
		private object Activate_43_CustomDialog()
		{
			return new CustomDialog();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D7D6 File Offset: 0x0000B9D6
		private object Activate_45_DropDownButton()
		{
			return new DropDownButton();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D7DD File Offset: 0x0000B9DD
		private object Activate_47_DynamicTextBlock()
		{
			return new DynamicTextBlock();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		private object Activate_50_LinearClipper()
		{
			return new LinearClipper();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		private void VectorAdd_6_ObservableCollection(object instance, object item)
		{
			ICollection<MenuItemBase> collection = (ICollection<MenuItemBase>)instance;
			MenuItemBase menuItemBase = (MenuItemBase)item;
			collection.Add(menuItemBase);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000D810 File Offset: 0x0000BA10
		private void VectorAdd_7_Collection(object instance, object item)
		{
			ICollection<MenuItemBase> collection = (ICollection<MenuItemBase>)instance;
			MenuItemBase menuItemBase = (MenuItemBase)item;
			collection.Add(menuItemBase);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D834 File Offset: 0x0000BA34
		private IXamlType CreateXamlType(int typeIndex)
		{
			XamlSystemBaseType result = null;
			string fullName = this._typeNameTable[typeIndex];
			Type type = this._typeTable[typeIndex];
			switch (typeIndex)
			{
			case 0:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_0_BrushToColorConverter)
				};
				break;
			case 1:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 2:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_2_ColorBrightnessConverter)
				};
				break;
			case 3:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_3_ColorContrastConverter)
				};
				break;
			case 4:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Control"));
				xamlUserType.Activator = new Activator(this.Activate_4_Menu);
				xamlUserType.SetContentPropertyName("Callisto.Controls.Menu.Items");
				xamlUserType.AddMemberName("Items");
				result = xamlUserType;
				break;
			}
			case 5:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 6:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Callisto.Controls.MenuItemBase>"));
				xamlUserType.CollectionAdd = new AddToCollection(this.VectorAdd_6_ObservableCollection);
				xamlUserType.SetIsReturnTypeStub();
				result = xamlUserType;
				break;
			}
			case 7:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"))
				{
					Activator = new Activator(this.Activate_7_Collection),
					CollectionAdd = new AddToCollection(this.VectorAdd_7_Collection)
				};
				break;
			case 8:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Control"));
				xamlUserType.AddMemberName("MenuTextMargin");
				result = xamlUserType;
				break;
			}
			case 9:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 10:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Callisto.Controls.MenuItemBase"));
				xamlUserType.Activator = new Activator(this.Activate_10_MenuItem);
				xamlUserType.AddMemberName("Text");
				xamlUserType.AddMemberName("Command");
				xamlUserType.AddMemberName("CommandParameter");
				result = xamlUserType;
				break;
			}
			case 11:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 12:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, null);
				xamlUserType.SetIsReturnTypeStub();
				result = xamlUserType;
				break;
			}
			case 13:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Callisto.Controls.MenuItem"));
				xamlUserType.Activator = new Activator(this.Activate_13_ToggleMenuItem);
				xamlUserType.AddMemberName("IsChecked");
				result = xamlUserType;
				break;
			}
			case 14:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 15:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Callisto.Controls.MenuItemBase"))
				{
					Activator = new Activator(this.Activate_15_MenuItemSeparator)
				};
				break;
			case 16:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
				xamlUserType.Activator = new Activator(this.Activate_16_Flyout);
				xamlUserType.AddMemberName("HostMargin");
				xamlUserType.AddMemberName("HostPopup");
				xamlUserType.AddMemberName("IsOpen");
				xamlUserType.AddMemberName("PlacementTarget");
				xamlUserType.AddMemberName("Placement");
				xamlUserType.AddMemberName("HorizontalOffset");
				xamlUserType.AddMemberName("VerticalOffset");
				result = xamlUserType;
				break;
			}
			case 17:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 18:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 19:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 20:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 21:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 22:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
				xamlUserType.Activator = new Activator(this.Activate_22_SettingsFlyout);
				xamlUserType.AddMemberName("HeaderBrush");
				xamlUserType.AddMemberName("HeaderText");
				xamlUserType.AddMemberName("ContentBackgroundBrush");
				xamlUserType.AddMemberName("ContentForegroundBrush");
				xamlUserType.AddMemberName("HostPopup");
				xamlUserType.AddMemberName("IsOpen");
				xamlUserType.AddMemberName("FlyoutWidth");
				xamlUserType.AddMemberName("SmallLogoImageSource");
				result = xamlUserType;
				break;
			}
			case 23:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 24:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Enum"));
				xamlUserType.AddEnumValue("Narrow", SettingsFlyout.SettingsFlyoutWidth.Narrow);
				xamlUserType.AddEnumValue("Wide", SettingsFlyout.SettingsFlyoutWidth.Wide);
				result = xamlUserType;
				break;
			}
			case 25:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.ValueType"));
				result = xamlUserType;
				break;
			}
			case 26:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Object"));
				result = xamlUserType;
				break;
			}
			case 27:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 28:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Control"));
				xamlUserType.Activator = new Activator(this.Activate_28_LiveTile);
				xamlUserType.AddMemberName("ItemsSource");
				xamlUserType.AddMemberName("ItemTemplate");
				xamlUserType.AddMemberName("Direction");
				result = xamlUserType;
				break;
			}
			case 29:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 30:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Enum"));
				xamlUserType.AddEnumValue("Up", LiveTile.SlideDirection.Up);
				xamlUserType.AddEnumValue("Left", LiveTile.SlideDirection.Left);
				result = xamlUserType;
				break;
			}
			case 31:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Primitives.ButtonBase"));
				xamlUserType.Activator = new Activator(this.Activate_31_RatingItem);
				xamlUserType.AddMemberName("ReadOnlyFill");
				xamlUserType.AddMemberName("DisplayValue");
				xamlUserType.AddMemberName("PointerOverFill");
				xamlUserType.AddMemberName("PointerPressedFill");
				result = xamlUserType;
				break;
			}
			case 32:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 33:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ItemsControl"));
				xamlUserType.Activator = new Activator(this.Activate_33_Rating);
				xamlUserType.AddMemberName("PointerOverFill");
				xamlUserType.AddMemberName("PointerPressedFill");
				xamlUserType.AddMemberName("ReadOnlyFill");
				xamlUserType.AddMemberName("ItemCount");
				xamlUserType.AddMemberName("SelectionMode");
				xamlUserType.AddMemberName("Value");
				xamlUserType.AddMemberName("WeightedValue");
				result = xamlUserType;
				break;
			}
			case 34:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 35:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 36:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Enum"));
				xamlUserType.AddEnumValue("Continuous", RatingSelectionMode.Continuous);
				xamlUserType.AddEnumValue("Individual", RatingSelectionMode.Individual);
				result = xamlUserType;
				break;
			}
			case 37:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ListBox"));
				xamlUserType.Activator = new Activator(this.Activate_37_FlipViewIndicator);
				xamlUserType.AddMemberName("FlipView");
				result = xamlUserType;
				break;
			}
			case 38:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 39:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 40:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.TextBox"));
				xamlUserType.Activator = new Activator(this.Activate_40_WatermarkTextBox);
				xamlUserType.AddMemberName("Watermark");
				result = xamlUserType;
				break;
			}
			case 41:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 42:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.TextBox"));
				xamlUserType.Activator = new Activator(this.Activate_42_NumericUpDown);
				xamlUserType.AddMemberName("Delay");
				xamlUserType.AddMemberName("Interval");
				xamlUserType.AddMemberName("Minimum");
				xamlUserType.AddMemberName("Maximum");
				xamlUserType.AddMemberName("Increment");
				xamlUserType.AddMemberName("DecimalPlaces");
				xamlUserType.AddMemberName("Value");
				result = xamlUserType;
				break;
			}
			case 43:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
				xamlUserType.Activator = new Activator(this.Activate_43_CustomDialog);
				xamlUserType.AddMemberName("IsOpen");
				xamlUserType.AddMemberName("BackButtonVisibility");
				xamlUserType.AddMemberName("Title");
				xamlUserType.AddMemberName("BackButtonCommand");
				xamlUserType.AddMemberName("BackButtonCommandParameter");
				result = xamlUserType;
				break;
			}
			case 44:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 45:
				result = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.Button"))
				{
					Activator = new Activator(this.Activate_45_DropDownButton)
				};
				break;
			case 46:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 47:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
				xamlUserType.Activator = new Activator(this.Activate_47_DynamicTextBlock);
				xamlUserType.AddMemberName("Text");
				xamlUserType.AddMemberName("TextWrapping");
				xamlUserType.AddMemberName("LineHeight");
				xamlUserType.AddMemberName("LineStackingStrategy");
				result = xamlUserType;
				break;
			}
			case 48:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 49:
				result = new XamlSystemBaseType(fullName, type);
				break;
			case 50:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Callisto.Controls.Primitives.Clipper"));
				xamlUserType.Activator = new Activator(this.Activate_50_LinearClipper);
				xamlUserType.AddMemberName("ExpandDirection");
				result = xamlUserType;
				break;
			}
			case 51:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentControl"));
				xamlUserType.AddMemberName("RatioVisible");
				result = xamlUserType;
				break;
			}
			case 52:
			{
				XamlUserType xamlUserType = new XamlUserType(this, fullName, type, this.GetXamlTypeByName("System.Enum"));
				xamlUserType.AddEnumValue("Down", ExpandDirection.Down);
				xamlUserType.AddEnumValue("Up", ExpandDirection.Up);
				xamlUserType.AddEnumValue("Left", ExpandDirection.Left);
				xamlUserType.AddEnumValue("Right", ExpandDirection.Right);
				result = xamlUserType;
				break;
			}
			}
			return result;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000E22C File Offset: 0x0000C42C
		private object get_0_Menu_Items(object instance)
		{
			Menu menu = (Menu)instance;
			return menu.Items;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000E248 File Offset: 0x0000C448
		private object get_1_MenuItemBase_MenuTextMargin(object instance)
		{
			MenuItemBase menuItemBase = (MenuItemBase)instance;
			return menuItemBase.MenuTextMargin;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000E268 File Offset: 0x0000C468
		private void set_1_MenuItemBase_MenuTextMargin(object instance, object Value)
		{
			MenuItemBase menuItemBase = (MenuItemBase)instance;
			menuItemBase.MenuTextMargin = (Thickness)Value;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000E288 File Offset: 0x0000C488
		private object get_2_MenuItem_Text(object instance)
		{
			MenuItem menuItem = (MenuItem)instance;
			return menuItem.Text;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		private void set_2_MenuItem_Text(object instance, object Value)
		{
			MenuItem menuItem = (MenuItem)instance;
			menuItem.Text = (string)Value;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		private object get_3_MenuItem_Command(object instance)
		{
			MenuItem menuItem = (MenuItem)instance;
			return menuItem.Command;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		private void set_3_MenuItem_Command(object instance, object Value)
		{
			MenuItem menuItem = (MenuItem)instance;
			menuItem.Command = (ICommand)Value;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E300 File Offset: 0x0000C500
		private object get_4_MenuItem_CommandParameter(object instance)
		{
			MenuItem menuItem = (MenuItem)instance;
			return menuItem.CommandParameter;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E31C File Offset: 0x0000C51C
		private void set_4_MenuItem_CommandParameter(object instance, object Value)
		{
			MenuItem menuItem = (MenuItem)instance;
			menuItem.CommandParameter = Value;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E338 File Offset: 0x0000C538
		private object get_5_ToggleMenuItem_IsChecked(object instance)
		{
			ToggleMenuItem toggleMenuItem = (ToggleMenuItem)instance;
			return toggleMenuItem.IsChecked;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E358 File Offset: 0x0000C558
		private void set_5_ToggleMenuItem_IsChecked(object instance, object Value)
		{
			ToggleMenuItem toggleMenuItem = (ToggleMenuItem)instance;
			toggleMenuItem.IsChecked = (bool)Value;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E378 File Offset: 0x0000C578
		private object get_6_Flyout_HostMargin(object instance)
		{
			Flyout flyout = (Flyout)instance;
			return flyout.HostMargin;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000E398 File Offset: 0x0000C598
		private void set_6_Flyout_HostMargin(object instance, object Value)
		{
			Flyout flyout = (Flyout)instance;
			flyout.HostMargin = (Thickness)Value;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		private object get_7_Flyout_HostPopup(object instance)
		{
			Flyout flyout = (Flyout)instance;
			return flyout.HostPopup;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		private object get_8_Flyout_IsOpen(object instance)
		{
			Flyout flyout = (Flyout)instance;
			return flyout.IsOpen;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		private void set_8_Flyout_IsOpen(object instance, object Value)
		{
			Flyout flyout = (Flyout)instance;
			flyout.IsOpen = (bool)Value;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E414 File Offset: 0x0000C614
		private object get_9_Flyout_PlacementTarget(object instance)
		{
			Flyout flyout = (Flyout)instance;
			return flyout.PlacementTarget;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E430 File Offset: 0x0000C630
		private void set_9_Flyout_PlacementTarget(object instance, object Value)
		{
			Flyout flyout = (Flyout)instance;
			flyout.PlacementTarget = (UIElement)Value;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E450 File Offset: 0x0000C650
		private object get_10_Flyout_Placement(object instance)
		{
			Flyout flyout = (Flyout)instance;
			return flyout.Placement;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E470 File Offset: 0x0000C670
		private void set_10_Flyout_Placement(object instance, object Value)
		{
			Flyout flyout = (Flyout)instance;
			flyout.Placement = (PlacementMode)Value;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E490 File Offset: 0x0000C690
		private object get_11_Flyout_HorizontalOffset(object instance)
		{
			Flyout flyout = (Flyout)instance;
			return flyout.HorizontalOffset;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		private void set_11_Flyout_HorizontalOffset(object instance, object Value)
		{
			Flyout flyout = (Flyout)instance;
			flyout.HorizontalOffset = (double)Value;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		private object get_12_Flyout_VerticalOffset(object instance)
		{
			Flyout flyout = (Flyout)instance;
			return flyout.VerticalOffset;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		private void set_12_Flyout_VerticalOffset(object instance, object Value)
		{
			Flyout flyout = (Flyout)instance;
			flyout.VerticalOffset = (double)Value;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E510 File Offset: 0x0000C710
		private object get_13_SettingsFlyout_HeaderBrush(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.HeaderBrush;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E52C File Offset: 0x0000C72C
		private void set_13_SettingsFlyout_HeaderBrush(object instance, object Value)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			settingsFlyout.HeaderBrush = (SolidColorBrush)Value;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E54C File Offset: 0x0000C74C
		private object get_14_SettingsFlyout_HeaderText(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.HeaderText;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E568 File Offset: 0x0000C768
		private void set_14_SettingsFlyout_HeaderText(object instance, object Value)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			settingsFlyout.HeaderText = (string)Value;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E588 File Offset: 0x0000C788
		private object get_15_SettingsFlyout_ContentBackgroundBrush(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.ContentBackgroundBrush;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		private void set_15_SettingsFlyout_ContentBackgroundBrush(object instance, object Value)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			settingsFlyout.ContentBackgroundBrush = (SolidColorBrush)Value;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		private object get_16_SettingsFlyout_ContentForegroundBrush(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.ContentForegroundBrush;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		private void set_16_SettingsFlyout_ContentForegroundBrush(object instance, object Value)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			settingsFlyout.ContentForegroundBrush = (SolidColorBrush)Value;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E600 File Offset: 0x0000C800
		private object get_17_SettingsFlyout_HostPopup(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.HostPopup;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E61C File Offset: 0x0000C81C
		private object get_18_SettingsFlyout_IsOpen(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.IsOpen;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E63C File Offset: 0x0000C83C
		private void set_18_SettingsFlyout_IsOpen(object instance, object Value)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			settingsFlyout.IsOpen = (bool)Value;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E65C File Offset: 0x0000C85C
		private object get_19_SettingsFlyout_FlyoutWidth(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.FlyoutWidth;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E67C File Offset: 0x0000C87C
		private void set_19_SettingsFlyout_FlyoutWidth(object instance, object Value)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			settingsFlyout.FlyoutWidth = (SettingsFlyout.SettingsFlyoutWidth)Value;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E69C File Offset: 0x0000C89C
		private object get_20_SettingsFlyout_SmallLogoImageSource(object instance)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			return settingsFlyout.SmallLogoImageSource;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		private void set_20_SettingsFlyout_SmallLogoImageSource(object instance, object Value)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)instance;
			settingsFlyout.SmallLogoImageSource = (ImageSource)Value;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		private object get_21_LiveTile_ItemsSource(object instance)
		{
			LiveTile liveTile = (LiveTile)instance;
			return liveTile.ItemsSource;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		private void set_21_LiveTile_ItemsSource(object instance, object Value)
		{
			LiveTile liveTile = (LiveTile)instance;
			liveTile.ItemsSource = Value;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E710 File Offset: 0x0000C910
		private object get_22_LiveTile_ItemTemplate(object instance)
		{
			LiveTile liveTile = (LiveTile)instance;
			return liveTile.ItemTemplate;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E72C File Offset: 0x0000C92C
		private void set_22_LiveTile_ItemTemplate(object instance, object Value)
		{
			LiveTile liveTile = (LiveTile)instance;
			liveTile.ItemTemplate = (DataTemplate)Value;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E74C File Offset: 0x0000C94C
		private object get_23_LiveTile_Direction(object instance)
		{
			LiveTile liveTile = (LiveTile)instance;
			return liveTile.Direction;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E76C File Offset: 0x0000C96C
		private void set_23_LiveTile_Direction(object instance, object Value)
		{
			LiveTile liveTile = (LiveTile)instance;
			liveTile.Direction = (LiveTile.SlideDirection)Value;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E78C File Offset: 0x0000C98C
		private object get_24_RatingItem_ReadOnlyFill(object instance)
		{
			RatingItem ratingItem = (RatingItem)instance;
			return ratingItem.ReadOnlyFill;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		private void set_24_RatingItem_ReadOnlyFill(object instance, object Value)
		{
			RatingItem ratingItem = (RatingItem)instance;
			ratingItem.ReadOnlyFill = (SolidColorBrush)Value;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		private object get_25_RatingItem_DisplayValue(object instance)
		{
			RatingItem ratingItem = (RatingItem)instance;
			return ratingItem.DisplayValue;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		private object get_26_RatingItem_PointerOverFill(object instance)
		{
			RatingItem ratingItem = (RatingItem)instance;
			return ratingItem.PointerOverFill;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E804 File Offset: 0x0000CA04
		private void set_26_RatingItem_PointerOverFill(object instance, object Value)
		{
			RatingItem ratingItem = (RatingItem)instance;
			ratingItem.PointerOverFill = (SolidColorBrush)Value;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E824 File Offset: 0x0000CA24
		private object get_27_RatingItem_PointerPressedFill(object instance)
		{
			RatingItem ratingItem = (RatingItem)instance;
			return ratingItem.PointerPressedFill;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E840 File Offset: 0x0000CA40
		private void set_27_RatingItem_PointerPressedFill(object instance, object Value)
		{
			RatingItem ratingItem = (RatingItem)instance;
			ratingItem.PointerPressedFill = (SolidColorBrush)Value;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E860 File Offset: 0x0000CA60
		private object get_28_Rating_PointerOverFill(object instance)
		{
			Rating rating = (Rating)instance;
			return rating.PointerOverFill;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E87C File Offset: 0x0000CA7C
		private void set_28_Rating_PointerOverFill(object instance, object Value)
		{
			Rating rating = (Rating)instance;
			rating.PointerOverFill = (SolidColorBrush)Value;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E89C File Offset: 0x0000CA9C
		private object get_29_Rating_PointerPressedFill(object instance)
		{
			Rating rating = (Rating)instance;
			return rating.PointerPressedFill;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		private void set_29_Rating_PointerPressedFill(object instance, object Value)
		{
			Rating rating = (Rating)instance;
			rating.PointerPressedFill = (SolidColorBrush)Value;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		private object get_30_Rating_ReadOnlyFill(object instance)
		{
			Rating rating = (Rating)instance;
			return rating.ReadOnlyFill;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		private void set_30_Rating_ReadOnlyFill(object instance, object Value)
		{
			Rating rating = (Rating)instance;
			rating.ReadOnlyFill = (SolidColorBrush)Value;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000E914 File Offset: 0x0000CB14
		private object get_31_Rating_ItemCount(object instance)
		{
			Rating rating = (Rating)instance;
			return rating.ItemCount;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E934 File Offset: 0x0000CB34
		private void set_31_Rating_ItemCount(object instance, object Value)
		{
			Rating rating = (Rating)instance;
			rating.ItemCount = (int)Value;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E954 File Offset: 0x0000CB54
		private object get_32_Rating_SelectionMode(object instance)
		{
			Rating rating = (Rating)instance;
			return rating.SelectionMode;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000E974 File Offset: 0x0000CB74
		private void set_32_Rating_SelectionMode(object instance, object Value)
		{
			Rating rating = (Rating)instance;
			rating.SelectionMode = (RatingSelectionMode)Value;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000E994 File Offset: 0x0000CB94
		private object get_33_Rating_Value(object instance)
		{
			Rating rating = (Rating)instance;
			return rating.Value;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		private void set_33_Rating_Value(object instance, object Value)
		{
			Rating rating = (Rating)instance;
			rating.Value = (double)Value;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		private object get_34_Rating_WeightedValue(object instance)
		{
			Rating rating = (Rating)instance;
			return rating.WeightedValue;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		private object get_35_FlipViewIndicator_FlipView(object instance)
		{
			FlipViewIndicator flipViewIndicator = (FlipViewIndicator)instance;
			return flipViewIndicator.FlipView;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000EA10 File Offset: 0x0000CC10
		private void set_35_FlipViewIndicator_FlipView(object instance, object Value)
		{
			FlipViewIndicator flipViewIndicator = (FlipViewIndicator)instance;
			flipViewIndicator.FlipView = (FlipView)Value;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000EA30 File Offset: 0x0000CC30
		private object get_36_WatermarkTextBox_Watermark(object instance)
		{
			WatermarkTextBox watermarkTextBox = (WatermarkTextBox)instance;
			return watermarkTextBox.Watermark;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		private void set_36_WatermarkTextBox_Watermark(object instance, object Value)
		{
			WatermarkTextBox watermarkTextBox = (WatermarkTextBox)instance;
			watermarkTextBox.Watermark = Value;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000EA68 File Offset: 0x0000CC68
		private object get_37_NumericUpDown_Delay(object instance)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			return numericUpDown.Delay;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000EA88 File Offset: 0x0000CC88
		private void set_37_NumericUpDown_Delay(object instance, object Value)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			numericUpDown.Delay = (int)Value;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		private object get_38_NumericUpDown_Interval(object instance)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			return numericUpDown.Interval;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
		private void set_38_NumericUpDown_Interval(object instance, object Value)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			numericUpDown.Interval = (int)Value;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		private object get_39_NumericUpDown_Minimum(object instance)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			return numericUpDown.Minimum;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000EB08 File Offset: 0x0000CD08
		private void set_39_NumericUpDown_Minimum(object instance, object Value)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			numericUpDown.Minimum = (double)Value;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000EB28 File Offset: 0x0000CD28
		private object get_40_NumericUpDown_Maximum(object instance)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			return numericUpDown.Maximum;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000EB48 File Offset: 0x0000CD48
		private void set_40_NumericUpDown_Maximum(object instance, object Value)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			numericUpDown.Maximum = (double)Value;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000EB68 File Offset: 0x0000CD68
		private object get_41_NumericUpDown_Increment(object instance)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			return numericUpDown.Increment;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000EB88 File Offset: 0x0000CD88
		private void set_41_NumericUpDown_Increment(object instance, object Value)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			numericUpDown.Increment = (double)Value;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		private object get_42_NumericUpDown_DecimalPlaces(object instance)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			return numericUpDown.DecimalPlaces;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		private void set_42_NumericUpDown_DecimalPlaces(object instance, object Value)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			numericUpDown.DecimalPlaces = (int)Value;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		private object get_43_NumericUpDown_Value(object instance)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			return numericUpDown.Value;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000EC08 File Offset: 0x0000CE08
		private void set_43_NumericUpDown_Value(object instance, object Value)
		{
			NumericUpDown numericUpDown = (NumericUpDown)instance;
			numericUpDown.Value = (double)Value;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000EC28 File Offset: 0x0000CE28
		private object get_44_CustomDialog_IsOpen(object instance)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			return customDialog.IsOpen;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000EC48 File Offset: 0x0000CE48
		private void set_44_CustomDialog_IsOpen(object instance, object Value)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			customDialog.IsOpen = (bool)Value;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000EC68 File Offset: 0x0000CE68
		private object get_45_CustomDialog_BackButtonVisibility(object instance)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			return customDialog.BackButtonVisibility;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000EC88 File Offset: 0x0000CE88
		private void set_45_CustomDialog_BackButtonVisibility(object instance, object Value)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			customDialog.BackButtonVisibility = (Visibility)Value;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		private object get_46_CustomDialog_Title(object instance)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			return customDialog.Title;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		private void set_46_CustomDialog_Title(object instance, object Value)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			customDialog.Title = (string)Value;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		private object get_47_CustomDialog_BackButtonCommand(object instance)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			return customDialog.BackButtonCommand;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000ED00 File Offset: 0x0000CF00
		private void set_47_CustomDialog_BackButtonCommand(object instance, object Value)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			customDialog.BackButtonCommand = (ICommand)Value;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000ED20 File Offset: 0x0000CF20
		private object get_48_CustomDialog_BackButtonCommandParameter(object instance)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			return customDialog.BackButtonCommandParameter;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		private void set_48_CustomDialog_BackButtonCommandParameter(object instance, object Value)
		{
			CustomDialog customDialog = (CustomDialog)instance;
			customDialog.BackButtonCommandParameter = Value;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000ED58 File Offset: 0x0000CF58
		private object get_49_DynamicTextBlock_Text(object instance)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			return dynamicTextBlock.Text;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000ED74 File Offset: 0x0000CF74
		private void set_49_DynamicTextBlock_Text(object instance, object Value)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			dynamicTextBlock.Text = (string)Value;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000ED94 File Offset: 0x0000CF94
		private object get_50_DynamicTextBlock_TextWrapping(object instance)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			return dynamicTextBlock.TextWrapping;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		private void set_50_DynamicTextBlock_TextWrapping(object instance, object Value)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			dynamicTextBlock.TextWrapping = (TextWrapping)Value;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		private object get_51_DynamicTextBlock_LineHeight(object instance)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			return dynamicTextBlock.LineHeight;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		private void set_51_DynamicTextBlock_LineHeight(object instance, object Value)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			dynamicTextBlock.LineHeight = (double)Value;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000EE14 File Offset: 0x0000D014
		private object get_52_DynamicTextBlock_LineStackingStrategy(object instance)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			return dynamicTextBlock.LineStackingStrategy;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000EE34 File Offset: 0x0000D034
		private void set_52_DynamicTextBlock_LineStackingStrategy(object instance, object Value)
		{
			DynamicTextBlock dynamicTextBlock = (DynamicTextBlock)instance;
			dynamicTextBlock.LineStackingStrategy = (LineStackingStrategy)Value;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000EE54 File Offset: 0x0000D054
		private object get_53_Clipper_RatioVisible(object instance)
		{
			Clipper clipper = (Clipper)instance;
			return clipper.RatioVisible;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000EE74 File Offset: 0x0000D074
		private void set_53_Clipper_RatioVisible(object instance, object Value)
		{
			Clipper clipper = (Clipper)instance;
			clipper.RatioVisible = (double)Value;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000EE94 File Offset: 0x0000D094
		private object get_54_LinearClipper_ExpandDirection(object instance)
		{
			LinearClipper linearClipper = (LinearClipper)instance;
			return linearClipper.ExpandDirection;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		private void set_54_LinearClipper_ExpandDirection(object instance, object Value)
		{
			LinearClipper linearClipper = (LinearClipper)instance;
			linearClipper.ExpandDirection = (ExpandDirection)Value;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		private IXamlMember CreateXamlMember(string longMemberName)
		{
			XamlMember xamlMember = null;
			if (longMemberName != null)
			{
				if (<PrivateImplementationDetails>{FEEE20E7-4507-4DF0-937D-43F2941FEA3B}.$$method0x60002f6-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(55);
					dictionary.Add("Callisto.Controls.Menu.Items", 0);
					dictionary.Add("Callisto.Controls.MenuItemBase.MenuTextMargin", 1);
					dictionary.Add("Callisto.Controls.MenuItem.Text", 2);
					dictionary.Add("Callisto.Controls.MenuItem.Command", 3);
					dictionary.Add("Callisto.Controls.MenuItem.CommandParameter", 4);
					dictionary.Add("Callisto.Controls.ToggleMenuItem.IsChecked", 5);
					dictionary.Add("Callisto.Controls.Flyout.HostMargin", 6);
					dictionary.Add("Callisto.Controls.Flyout.HostPopup", 7);
					dictionary.Add("Callisto.Controls.Flyout.IsOpen", 8);
					dictionary.Add("Callisto.Controls.Flyout.PlacementTarget", 9);
					dictionary.Add("Callisto.Controls.Flyout.Placement", 10);
					dictionary.Add("Callisto.Controls.Flyout.HorizontalOffset", 11);
					dictionary.Add("Callisto.Controls.Flyout.VerticalOffset", 12);
					dictionary.Add("Callisto.Controls.SettingsFlyout.HeaderBrush", 13);
					dictionary.Add("Callisto.Controls.SettingsFlyout.HeaderText", 14);
					dictionary.Add("Callisto.Controls.SettingsFlyout.ContentBackgroundBrush", 15);
					dictionary.Add("Callisto.Controls.SettingsFlyout.ContentForegroundBrush", 16);
					dictionary.Add("Callisto.Controls.SettingsFlyout.HostPopup", 17);
					dictionary.Add("Callisto.Controls.SettingsFlyout.IsOpen", 18);
					dictionary.Add("Callisto.Controls.SettingsFlyout.FlyoutWidth", 19);
					dictionary.Add("Callisto.Controls.SettingsFlyout.SmallLogoImageSource", 20);
					dictionary.Add("Callisto.Controls.LiveTile.ItemsSource", 21);
					dictionary.Add("Callisto.Controls.LiveTile.ItemTemplate", 22);
					dictionary.Add("Callisto.Controls.LiveTile.Direction", 23);
					dictionary.Add("Callisto.Controls.RatingItem.ReadOnlyFill", 24);
					dictionary.Add("Callisto.Controls.RatingItem.DisplayValue", 25);
					dictionary.Add("Callisto.Controls.RatingItem.PointerOverFill", 26);
					dictionary.Add("Callisto.Controls.RatingItem.PointerPressedFill", 27);
					dictionary.Add("Callisto.Controls.Rating.PointerOverFill", 28);
					dictionary.Add("Callisto.Controls.Rating.PointerPressedFill", 29);
					dictionary.Add("Callisto.Controls.Rating.ReadOnlyFill", 30);
					dictionary.Add("Callisto.Controls.Rating.ItemCount", 31);
					dictionary.Add("Callisto.Controls.Rating.SelectionMode", 32);
					dictionary.Add("Callisto.Controls.Rating.Value", 33);
					dictionary.Add("Callisto.Controls.Rating.WeightedValue", 34);
					dictionary.Add("Callisto.Controls.FlipViewIndicator.FlipView", 35);
					dictionary.Add("Callisto.Controls.WatermarkTextBox.Watermark", 36);
					dictionary.Add("Callisto.Controls.NumericUpDown.Delay", 37);
					dictionary.Add("Callisto.Controls.NumericUpDown.Interval", 38);
					dictionary.Add("Callisto.Controls.NumericUpDown.Minimum", 39);
					dictionary.Add("Callisto.Controls.NumericUpDown.Maximum", 40);
					dictionary.Add("Callisto.Controls.NumericUpDown.Increment", 41);
					dictionary.Add("Callisto.Controls.NumericUpDown.DecimalPlaces", 42);
					dictionary.Add("Callisto.Controls.NumericUpDown.Value", 43);
					dictionary.Add("Callisto.Controls.CustomDialog.IsOpen", 44);
					dictionary.Add("Callisto.Controls.CustomDialog.BackButtonVisibility", 45);
					dictionary.Add("Callisto.Controls.CustomDialog.Title", 46);
					dictionary.Add("Callisto.Controls.CustomDialog.BackButtonCommand", 47);
					dictionary.Add("Callisto.Controls.CustomDialog.BackButtonCommandParameter", 48);
					dictionary.Add("Callisto.Controls.DynamicTextBlock.Text", 49);
					dictionary.Add("Callisto.Controls.DynamicTextBlock.TextWrapping", 50);
					dictionary.Add("Callisto.Controls.DynamicTextBlock.LineHeight", 51);
					dictionary.Add("Callisto.Controls.DynamicTextBlock.LineStackingStrategy", 52);
					dictionary.Add("Callisto.Controls.Primitives.Clipper.RatioVisible", 53);
					dictionary.Add("Callisto.Controls.Primitives.LinearClipper.ExpandDirection", 54);
					<PrivateImplementationDetails>{FEEE20E7-4507-4DF0-937D-43F2941FEA3B}.$$method0x60002f6-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{FEEE20E7-4507-4DF0-937D-43F2941FEA3B}.$$method0x60002f6-1.TryGetValue(longMemberName, ref num))
				{
					switch (num)
					{
					case 0:
					{
						XamlUserType xamlUserType = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Menu");
						xamlMember = new XamlMember(this, "Items", "System.Collections.ObjectModel.ObservableCollection`1<Callisto.Controls.MenuItemBase>");
						xamlMember.Getter = new Getter(this.get_0_Menu_Items);
						xamlMember.SetIsReadOnly();
						break;
					}
					case 1:
					{
						XamlUserType xamlUserType2 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.MenuItemBase");
						xamlMember = new XamlMember(this, "MenuTextMargin", "Windows.UI.Xaml.Thickness");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_1_MenuItemBase_MenuTextMargin);
						xamlMember.Setter = new Setter(this.set_1_MenuItemBase_MenuTextMargin);
						break;
					}
					case 2:
					{
						XamlUserType xamlUserType3 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.MenuItem");
						xamlMember = new XamlMember(this, "Text", "String");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_2_MenuItem_Text);
						xamlMember.Setter = new Setter(this.set_2_MenuItem_Text);
						break;
					}
					case 3:
					{
						XamlUserType xamlUserType4 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.MenuItem");
						xamlMember = new XamlMember(this, "Command", "System.Windows.Input.ICommand");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_3_MenuItem_Command);
						xamlMember.Setter = new Setter(this.set_3_MenuItem_Command);
						break;
					}
					case 4:
					{
						XamlUserType xamlUserType5 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.MenuItem");
						xamlMember = new XamlMember(this, "CommandParameter", "Object");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_4_MenuItem_CommandParameter);
						xamlMember.Setter = new Setter(this.set_4_MenuItem_CommandParameter);
						break;
					}
					case 5:
					{
						XamlUserType xamlUserType6 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.ToggleMenuItem");
						xamlMember = new XamlMember(this, "IsChecked", "Boolean");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_5_ToggleMenuItem_IsChecked);
						xamlMember.Setter = new Setter(this.set_5_ToggleMenuItem_IsChecked);
						break;
					}
					case 6:
					{
						XamlUserType xamlUserType7 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Flyout");
						xamlMember = new XamlMember(this, "HostMargin", "Windows.UI.Xaml.Thickness");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_6_Flyout_HostMargin);
						xamlMember.Setter = new Setter(this.set_6_Flyout_HostMargin);
						break;
					}
					case 7:
					{
						XamlUserType xamlUserType8 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Flyout");
						xamlMember = new XamlMember(this, "HostPopup", "Windows.UI.Xaml.Controls.Primitives.Popup");
						xamlMember.Getter = new Getter(this.get_7_Flyout_HostPopup);
						xamlMember.SetIsReadOnly();
						break;
					}
					case 8:
					{
						XamlUserType xamlUserType9 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Flyout");
						xamlMember = new XamlMember(this, "IsOpen", "Boolean");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_8_Flyout_IsOpen);
						xamlMember.Setter = new Setter(this.set_8_Flyout_IsOpen);
						break;
					}
					case 9:
					{
						XamlUserType xamlUserType10 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Flyout");
						xamlMember = new XamlMember(this, "PlacementTarget", "Windows.UI.Xaml.UIElement");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_9_Flyout_PlacementTarget);
						xamlMember.Setter = new Setter(this.set_9_Flyout_PlacementTarget);
						break;
					}
					case 10:
					{
						XamlUserType xamlUserType11 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Flyout");
						xamlMember = new XamlMember(this, "Placement", "Windows.UI.Xaml.Controls.Primitives.PlacementMode");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_10_Flyout_Placement);
						xamlMember.Setter = new Setter(this.set_10_Flyout_Placement);
						break;
					}
					case 11:
					{
						XamlUserType xamlUserType12 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Flyout");
						xamlMember = new XamlMember(this, "HorizontalOffset", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_11_Flyout_HorizontalOffset);
						xamlMember.Setter = new Setter(this.set_11_Flyout_HorizontalOffset);
						break;
					}
					case 12:
					{
						XamlUserType xamlUserType13 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Flyout");
						xamlMember = new XamlMember(this, "VerticalOffset", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_12_Flyout_VerticalOffset);
						xamlMember.Setter = new Setter(this.set_12_Flyout_VerticalOffset);
						break;
					}
					case 13:
					{
						XamlUserType xamlUserType14 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "HeaderBrush", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_13_SettingsFlyout_HeaderBrush);
						xamlMember.Setter = new Setter(this.set_13_SettingsFlyout_HeaderBrush);
						break;
					}
					case 14:
					{
						XamlUserType xamlUserType15 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "HeaderText", "String");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_14_SettingsFlyout_HeaderText);
						xamlMember.Setter = new Setter(this.set_14_SettingsFlyout_HeaderText);
						break;
					}
					case 15:
					{
						XamlUserType xamlUserType16 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "ContentBackgroundBrush", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_15_SettingsFlyout_ContentBackgroundBrush);
						xamlMember.Setter = new Setter(this.set_15_SettingsFlyout_ContentBackgroundBrush);
						break;
					}
					case 16:
					{
						XamlUserType xamlUserType17 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "ContentForegroundBrush", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_16_SettingsFlyout_ContentForegroundBrush);
						xamlMember.Setter = new Setter(this.set_16_SettingsFlyout_ContentForegroundBrush);
						break;
					}
					case 17:
					{
						XamlUserType xamlUserType18 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "HostPopup", "Windows.UI.Xaml.Controls.Primitives.Popup");
						xamlMember.Getter = new Getter(this.get_17_SettingsFlyout_HostPopup);
						xamlMember.SetIsReadOnly();
						break;
					}
					case 18:
					{
						XamlUserType xamlUserType19 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "IsOpen", "Boolean");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_18_SettingsFlyout_IsOpen);
						xamlMember.Setter = new Setter(this.set_18_SettingsFlyout_IsOpen);
						break;
					}
					case 19:
					{
						XamlUserType xamlUserType20 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "FlyoutWidth", "Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_19_SettingsFlyout_FlyoutWidth);
						xamlMember.Setter = new Setter(this.set_19_SettingsFlyout_FlyoutWidth);
						break;
					}
					case 20:
					{
						XamlUserType xamlUserType21 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.SettingsFlyout");
						xamlMember = new XamlMember(this, "SmallLogoImageSource", "Windows.UI.Xaml.Media.ImageSource");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_20_SettingsFlyout_SmallLogoImageSource);
						xamlMember.Setter = new Setter(this.set_20_SettingsFlyout_SmallLogoImageSource);
						break;
					}
					case 21:
					{
						XamlUserType xamlUserType22 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.LiveTile");
						xamlMember = new XamlMember(this, "ItemsSource", "Object");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_21_LiveTile_ItemsSource);
						xamlMember.Setter = new Setter(this.set_21_LiveTile_ItemsSource);
						break;
					}
					case 22:
					{
						XamlUserType xamlUserType23 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.LiveTile");
						xamlMember = new XamlMember(this, "ItemTemplate", "Windows.UI.Xaml.DataTemplate");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_22_LiveTile_ItemTemplate);
						xamlMember.Setter = new Setter(this.set_22_LiveTile_ItemTemplate);
						break;
					}
					case 23:
					{
						XamlUserType xamlUserType24 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.LiveTile");
						xamlMember = new XamlMember(this, "Direction", "Callisto.Controls.LiveTile.SlideDirection");
						xamlMember.Getter = new Getter(this.get_23_LiveTile_Direction);
						xamlMember.Setter = new Setter(this.set_23_LiveTile_Direction);
						break;
					}
					case 24:
					{
						XamlUserType xamlUserType25 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.RatingItem");
						xamlMember = new XamlMember(this, "ReadOnlyFill", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_24_RatingItem_ReadOnlyFill);
						xamlMember.Setter = new Setter(this.set_24_RatingItem_ReadOnlyFill);
						break;
					}
					case 25:
					{
						XamlUserType xamlUserType26 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.RatingItem");
						xamlMember = new XamlMember(this, "DisplayValue", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_25_RatingItem_DisplayValue);
						xamlMember.SetIsReadOnly();
						break;
					}
					case 26:
					{
						XamlUserType xamlUserType27 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.RatingItem");
						xamlMember = new XamlMember(this, "PointerOverFill", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_26_RatingItem_PointerOverFill);
						xamlMember.Setter = new Setter(this.set_26_RatingItem_PointerOverFill);
						break;
					}
					case 27:
					{
						XamlUserType xamlUserType28 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.RatingItem");
						xamlMember = new XamlMember(this, "PointerPressedFill", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_27_RatingItem_PointerPressedFill);
						xamlMember.Setter = new Setter(this.set_27_RatingItem_PointerPressedFill);
						break;
					}
					case 28:
					{
						XamlUserType xamlUserType29 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Rating");
						xamlMember = new XamlMember(this, "PointerOverFill", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_28_Rating_PointerOverFill);
						xamlMember.Setter = new Setter(this.set_28_Rating_PointerOverFill);
						break;
					}
					case 29:
					{
						XamlUserType xamlUserType30 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Rating");
						xamlMember = new XamlMember(this, "PointerPressedFill", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_29_Rating_PointerPressedFill);
						xamlMember.Setter = new Setter(this.set_29_Rating_PointerPressedFill);
						break;
					}
					case 30:
					{
						XamlUserType xamlUserType31 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Rating");
						xamlMember = new XamlMember(this, "ReadOnlyFill", "Windows.UI.Xaml.Media.SolidColorBrush");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_30_Rating_ReadOnlyFill);
						xamlMember.Setter = new Setter(this.set_30_Rating_ReadOnlyFill);
						break;
					}
					case 31:
					{
						XamlUserType xamlUserType32 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Rating");
						xamlMember = new XamlMember(this, "ItemCount", "Int32");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_31_Rating_ItemCount);
						xamlMember.Setter = new Setter(this.set_31_Rating_ItemCount);
						break;
					}
					case 32:
					{
						XamlUserType xamlUserType33 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Rating");
						xamlMember = new XamlMember(this, "SelectionMode", "Callisto.Controls.RatingSelectionMode");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_32_Rating_SelectionMode);
						xamlMember.Setter = new Setter(this.set_32_Rating_SelectionMode);
						break;
					}
					case 33:
					{
						XamlUserType xamlUserType34 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Rating");
						xamlMember = new XamlMember(this, "Value", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_33_Rating_Value);
						xamlMember.Setter = new Setter(this.set_33_Rating_Value);
						break;
					}
					case 34:
					{
						XamlUserType xamlUserType35 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Rating");
						xamlMember = new XamlMember(this, "WeightedValue", "Double");
						xamlMember.Getter = new Getter(this.get_34_Rating_WeightedValue);
						xamlMember.SetIsReadOnly();
						break;
					}
					case 35:
					{
						XamlUserType xamlUserType36 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.FlipViewIndicator");
						xamlMember = new XamlMember(this, "FlipView", "Windows.UI.Xaml.Controls.FlipView");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_35_FlipViewIndicator_FlipView);
						xamlMember.Setter = new Setter(this.set_35_FlipViewIndicator_FlipView);
						break;
					}
					case 36:
					{
						XamlUserType xamlUserType37 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.WatermarkTextBox");
						xamlMember = new XamlMember(this, "Watermark", "Object");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_36_WatermarkTextBox_Watermark);
						xamlMember.Setter = new Setter(this.set_36_WatermarkTextBox_Watermark);
						break;
					}
					case 37:
					{
						XamlUserType xamlUserType38 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.NumericUpDown");
						xamlMember = new XamlMember(this, "Delay", "Int32");
						xamlMember.Getter = new Getter(this.get_37_NumericUpDown_Delay);
						xamlMember.Setter = new Setter(this.set_37_NumericUpDown_Delay);
						break;
					}
					case 38:
					{
						XamlUserType xamlUserType39 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.NumericUpDown");
						xamlMember = new XamlMember(this, "Interval", "Int32");
						xamlMember.Getter = new Getter(this.get_38_NumericUpDown_Interval);
						xamlMember.Setter = new Setter(this.set_38_NumericUpDown_Interval);
						break;
					}
					case 39:
					{
						XamlUserType xamlUserType40 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.NumericUpDown");
						xamlMember = new XamlMember(this, "Minimum", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_39_NumericUpDown_Minimum);
						xamlMember.Setter = new Setter(this.set_39_NumericUpDown_Minimum);
						break;
					}
					case 40:
					{
						XamlUserType xamlUserType41 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.NumericUpDown");
						xamlMember = new XamlMember(this, "Maximum", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_40_NumericUpDown_Maximum);
						xamlMember.Setter = new Setter(this.set_40_NumericUpDown_Maximum);
						break;
					}
					case 41:
					{
						XamlUserType xamlUserType42 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.NumericUpDown");
						xamlMember = new XamlMember(this, "Increment", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_41_NumericUpDown_Increment);
						xamlMember.Setter = new Setter(this.set_41_NumericUpDown_Increment);
						break;
					}
					case 42:
					{
						XamlUserType xamlUserType43 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.NumericUpDown");
						xamlMember = new XamlMember(this, "DecimalPlaces", "Int32");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_42_NumericUpDown_DecimalPlaces);
						xamlMember.Setter = new Setter(this.set_42_NumericUpDown_DecimalPlaces);
						break;
					}
					case 43:
					{
						XamlUserType xamlUserType44 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.NumericUpDown");
						xamlMember = new XamlMember(this, "Value", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_43_NumericUpDown_Value);
						xamlMember.Setter = new Setter(this.set_43_NumericUpDown_Value);
						break;
					}
					case 44:
					{
						XamlUserType xamlUserType45 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.CustomDialog");
						xamlMember = new XamlMember(this, "IsOpen", "Boolean");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_44_CustomDialog_IsOpen);
						xamlMember.Setter = new Setter(this.set_44_CustomDialog_IsOpen);
						break;
					}
					case 45:
					{
						XamlUserType xamlUserType46 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.CustomDialog");
						xamlMember = new XamlMember(this, "BackButtonVisibility", "Windows.UI.Xaml.Visibility");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_45_CustomDialog_BackButtonVisibility);
						xamlMember.Setter = new Setter(this.set_45_CustomDialog_BackButtonVisibility);
						break;
					}
					case 46:
					{
						XamlUserType xamlUserType47 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.CustomDialog");
						xamlMember = new XamlMember(this, "Title", "String");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_46_CustomDialog_Title);
						xamlMember.Setter = new Setter(this.set_46_CustomDialog_Title);
						break;
					}
					case 47:
					{
						XamlUserType xamlUserType48 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.CustomDialog");
						xamlMember = new XamlMember(this, "BackButtonCommand", "System.Windows.Input.ICommand");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_47_CustomDialog_BackButtonCommand);
						xamlMember.Setter = new Setter(this.set_47_CustomDialog_BackButtonCommand);
						break;
					}
					case 48:
					{
						XamlUserType xamlUserType49 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.CustomDialog");
						xamlMember = new XamlMember(this, "BackButtonCommandParameter", "Object");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_48_CustomDialog_BackButtonCommandParameter);
						xamlMember.Setter = new Setter(this.set_48_CustomDialog_BackButtonCommandParameter);
						break;
					}
					case 49:
					{
						XamlUserType xamlUserType50 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.DynamicTextBlock");
						xamlMember = new XamlMember(this, "Text", "String");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_49_DynamicTextBlock_Text);
						xamlMember.Setter = new Setter(this.set_49_DynamicTextBlock_Text);
						break;
					}
					case 50:
					{
						XamlUserType xamlUserType51 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.DynamicTextBlock");
						xamlMember = new XamlMember(this, "TextWrapping", "Windows.UI.Xaml.TextWrapping");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_50_DynamicTextBlock_TextWrapping);
						xamlMember.Setter = new Setter(this.set_50_DynamicTextBlock_TextWrapping);
						break;
					}
					case 51:
					{
						XamlUserType xamlUserType52 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.DynamicTextBlock");
						xamlMember = new XamlMember(this, "LineHeight", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_51_DynamicTextBlock_LineHeight);
						xamlMember.Setter = new Setter(this.set_51_DynamicTextBlock_LineHeight);
						break;
					}
					case 52:
					{
						XamlUserType xamlUserType53 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.DynamicTextBlock");
						xamlMember = new XamlMember(this, "LineStackingStrategy", "Windows.UI.Xaml.LineStackingStrategy");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_52_DynamicTextBlock_LineStackingStrategy);
						xamlMember.Setter = new Setter(this.set_52_DynamicTextBlock_LineStackingStrategy);
						break;
					}
					case 53:
					{
						XamlUserType xamlUserType54 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Primitives.Clipper");
						xamlMember = new XamlMember(this, "RatioVisible", "Double");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_53_Clipper_RatioVisible);
						xamlMember.Setter = new Setter(this.set_53_Clipper_RatioVisible);
						break;
					}
					case 54:
					{
						XamlUserType xamlUserType55 = (XamlUserType)this.GetXamlTypeByName("Callisto.Controls.Primitives.LinearClipper");
						xamlMember = new XamlMember(this, "ExpandDirection", "Callisto.Controls.ExpandDirection");
						xamlMember.SetIsDependencyProperty();
						xamlMember.Getter = new Getter(this.get_54_LinearClipper_ExpandDirection);
						xamlMember.Setter = new Setter(this.set_54_LinearClipper_ExpandDirection);
						break;
					}
					}
				}
			}
			return xamlMember;
		}

		// Token: 0x04000136 RID: 310
		private Dictionary<string, IXamlType> _xamlTypeCacheByName = new Dictionary<string, IXamlType>();

		// Token: 0x04000137 RID: 311
		private Dictionary<Type, IXamlType> _xamlTypeCacheByType = new Dictionary<Type, IXamlType>();

		// Token: 0x04000138 RID: 312
		private Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();

		// Token: 0x04000139 RID: 313
		private string[] _typeNameTable;

		// Token: 0x0400013A RID: 314
		private Type[] _typeTable;
	}
}

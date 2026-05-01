using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

namespace Callisto.Controls
{
	// Token: 0x0200000E RID: 14
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as MenuFlyout.")]
	[ContentProperty(Name = "Items")]
	public sealed class Menu : Control
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004F9E File Offset: 0x0000319E
		public ObservableCollection<MenuItemBase> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004FA8 File Offset: 0x000031A8
		private void OnKeyDown(object sender, KeyRoutedEventArgs args)
		{
			VirtualKey key = args.Key;
			if (key != 13)
			{
				switch (key)
				{
				case 27:
					if (base.Parent.GetType() == typeof(Flyout))
					{
						((Flyout)base.Parent).IsOpen = false;
						return;
					}
					return;
				case 28:
				case 29:
				case 30:
				case 31:
				case 37:
				case 39:
					return;
				case 32:
					break;
				case 33:
				case 36:
					this.PageFocusedItem(true);
					return;
				case 34:
				case 35:
					this.PageFocusedItem(false);
					return;
				case 38:
					this.ChangeFocusedItem(false);
					return;
				case 40:
					this.ChangeFocusedItem(true);
					return;
				default:
					return;
				}
			}
			this.OnTapped(new TappedRoutedEventArgs());
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000505C File Offset: 0x0000325C
		private void PageFocusedItem(bool top)
		{
			int num = top ? 0 : (this._items.Count - 1);
			this._items[num].Focus(3);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005090 File Offset: 0x00003290
		private void ChangeFocusedItem(bool ascendIndex)
		{
			object focusedElement = FocusManager.GetFocusedElement();
			int nextItemIndex;
			if (focusedElement != null && (focusedElement == this._itemContainerList || focusedElement == this) && this._items.Count > 0)
			{
				nextItemIndex = this.GetNextItemIndex(-1, ascendIndex);
			}
			else
			{
				if (focusedElement == null || !(focusedElement is MenuItem) || !this._items.Contains((MenuItem)focusedElement))
				{
					return;
				}
				nextItemIndex = this.GetNextItemIndex(this._items.IndexOf((MenuItem)focusedElement), ascendIndex);
			}
			int num = nextItemIndex;
			MenuItemBase menuItemBase = this._items[num];
			if (menuItemBase is MenuItem)
			{
				menuItemBase.Focus(3);
				return;
			}
			num = this.GetNextItemIndex(num, ascendIndex);
			menuItemBase = this._items[num];
			while (menuItemBase != null && !(menuItemBase is MenuItem) && num != nextItemIndex)
			{
				num = this.GetNextItemIndex(num, ascendIndex);
				menuItemBase = this._items[num];
			}
			if (menuItemBase != null && menuItemBase is MenuItem)
			{
				menuItemBase.Focus(3);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005178 File Offset: 0x00003378
		private int GetNextItemIndex(int startIndex, bool ascending)
		{
			int num = startIndex + (ascending ? 1 : -1);
			if (num >= this._items.Count)
			{
				num = 0;
			}
			else if (num < 0)
			{
				num = this._items.Count - 1;
			}
			return num;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000051B4 File Offset: 0x000033B4
		public Menu()
		{
			base.put_DefaultStyleKey(typeof(Menu));
			base.AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(this.OnKeyDown), true);
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Loaded), new Action<EventRegistrationToken>(base.remove_Loaded), new RoutedEventHandler(this.OnLoaded));
			this._items = new ObservableCollection<MenuItemBase>();
			this._items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000523C File Offset: 0x0000343C
		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			((Menu)sender).put_IsHitTestVisible(true);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000524A File Offset: 0x0000344A
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this._itemContainerList = (base.GetTemplateChild("ItemList") as ItemsControl);
			this._itemContainerList.put_ItemsSource(this._items);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000527C File Offset: 0x0000347C
		private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				foreach (object obj in e.NewItems)
				{
					MenuItem menuItem = obj as MenuItem;
					if (menuItem != null)
					{
						WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(menuItem.add_Tapped), new Action<EventRegistrationToken>(menuItem.remove_Tapped), new TappedEventHandler(this.OnMenuItemSelected));
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (object obj2 in e.OldItems)
				{
					MenuItem menuItem2 = obj2 as MenuItem;
					if (menuItem2 != null)
					{
						WindowsRuntimeMarshal.RemoveEventHandler<TappedEventHandler>(new Action<EventRegistrationToken>(menuItem2.remove_Tapped), new TappedEventHandler(this.OnMenuItemSelected));
					}
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005380 File Offset: 0x00003580
		private void OnMenuItemSelected(object sender, TappedRoutedEventArgs args)
		{
			MenuItem menuItem = sender as MenuItem;
			if (menuItem == null)
			{
				return;
			}
			if (base.Parent is Flyout)
			{
				((Flyout)base.Parent).IsOpen = false;
			}
			if (menuItem.Command != null)
			{
				menuItem.Command.Execute(menuItem.CommandParameter);
			}
		}

		// Token: 0x04000059 RID: 89
		private ObservableCollection<MenuItemBase> _items;

		// Token: 0x0400005A RID: 90
		private ItemsControl _itemContainerList;
	}
}

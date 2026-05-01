using System;
using System.Windows.Input;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000062 RID: 98
	public class RelayCommand : ICommand
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000644 RID: 1604 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		// (remove) Token: 0x06000645 RID: 1605 RVA: 0x0001D808 File Offset: 0x0001BA08
		public event EventHandler CanExecuteChanged;

		// Token: 0x06000646 RID: 1606 RVA: 0x0001D83D File Offset: 0x0001BA3D
		public RelayCommand(Action execute) : this(execute, null)
		{
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001D847 File Offset: 0x0001BA47
		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
			this._canExecute = canExecute;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001D86B File Offset: 0x0001BA6B
		public bool CanExecute(object parameter)
		{
			return this._canExecute == null || this._canExecute.Invoke();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001D882 File Offset: 0x0001BA82
		public void Execute(object parameter)
		{
			this._execute.Invoke();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001D890 File Offset: 0x0001BA90
		public void RaiseCanExecuteChanged()
		{
			EventHandler canExecuteChanged = this.CanExecuteChanged;
			if (canExecuteChanged != null)
			{
				canExecuteChanged.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000263 RID: 611
		private readonly Action _execute;

		// Token: 0x04000264 RID: 612
		private readonly Func<bool> _canExecute;
	}
}

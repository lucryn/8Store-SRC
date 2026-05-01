using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000056 RID: 86
	public class RelayCommand : ICommand
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004F7 RID: 1271 RVA: 0x0001BC70 File Offset: 0x00019E70
		// (remove) Token: 0x060004F8 RID: 1272 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		[DebuggerBrowsable(0)]
		public event EventHandler CanExecuteChanged;

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001BCDD File Offset: 0x00019EDD
		public RelayCommand(Action execute) : this(execute, null)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			bool flag = execute == null;
			if (flag)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
			this._canExecute = canExecute;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001BD24 File Offset: 0x00019F24
		public bool CanExecute(object parameter)
		{
			return this._canExecute == null || this._canExecute.Invoke();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001BD4C File Offset: 0x00019F4C
		public void Execute(object parameter)
		{
			this._execute.Invoke();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001BD5C File Offset: 0x00019F5C
		public void RaiseCanExecuteChanged()
		{
			EventHandler canExecuteChanged = this.CanExecuteChanged;
			bool flag = canExecuteChanged != null;
			if (flag)
			{
				canExecuteChanged.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400022C RID: 556
		private readonly Action _execute;

		// Token: 0x0400022D RID: 557
		private readonly Func<bool> _canExecute;
	}
}

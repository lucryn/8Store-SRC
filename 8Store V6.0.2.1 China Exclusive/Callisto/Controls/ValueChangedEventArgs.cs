using System;

namespace Callisto.Controls
{
	// Token: 0x0200001C RID: 28
	public class ValueChangedEventArgs<T> : EventArgs
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000083C9 File Offset: 0x000065C9
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000083D1 File Offset: 0x000065D1
		public T OldValue { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000083DA File Offset: 0x000065DA
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000083E2 File Offset: 0x000065E2
		public T NewValue { get; private set; }

		// Token: 0x06000172 RID: 370 RVA: 0x000083EB File Offset: 0x000065EB
		public ValueChangedEventArgs(T oldValue, T newValue)
		{
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
	}
}

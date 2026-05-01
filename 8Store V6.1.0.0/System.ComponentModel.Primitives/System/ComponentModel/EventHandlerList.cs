using System;

namespace System.ComponentModel
{
	// Token: 0x0200000D RID: 13
	public sealed class EventHandlerList : IDisposable
	{
		// Token: 0x1700002F RID: 47
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					return listEntry.handler;
				}
				return null;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry.handler = value;
					return;
				}
				this._head = new EventHandlerList.ListEntry(key, value, this._head);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000028BC File Offset: 0x00000ABC
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Combine(listEntry.handler, value);
				return;
			}
			this._head = new EventHandlerList.ListEntry(key, value, this._head);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000028FC File Offset: 0x00000AFC
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			for (EventHandlerList.ListEntry listEntry = listToAddFrom._head; listEntry != null; listEntry = listEntry.next)
			{
				this.AddHandler(listEntry.key, listEntry.handler);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000292E File Offset: 0x00000B2E
		public void Dispose()
		{
			this._head = null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002938 File Offset: 0x00000B38
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry listEntry = this._head;
			while (listEntry != null && listEntry.key != key)
			{
				listEntry = listEntry.next;
			}
			return listEntry;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002964 File Offset: 0x00000B64
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Remove(listEntry.handler, value);
			}
		}

		// Token: 0x0400002D RID: 45
		private EventHandlerList.ListEntry _head;

		// Token: 0x0200001A RID: 26
		private sealed class ListEntry
		{
			// Token: 0x06000093 RID: 147 RVA: 0x00002C70 File Offset: 0x00000E70
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

			// Token: 0x0400004D RID: 77
			internal EventHandlerList.ListEntry next;

			// Token: 0x0400004E RID: 78
			internal object key;

			// Token: 0x0400004F RID: 79
			internal Delegate handler;
		}
	}
}

using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Provides data for the Error event.
	/// </summary>
	// Token: 0x0200008B RID: 139
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the current object the error event is being raised against.
		/// </summary>
		/// <value>The current object the error event is being raised against.</value>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001AB27 File Offset: 0x00018D27
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001AB2F File Offset: 0x00018D2F
		public object CurrentObject { get; private set; }

		/// <summary>
		/// Gets the error context.
		/// </summary>
		/// <value>The error context.</value>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001AB38 File Offset: 0x00018D38
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001AB40 File Offset: 0x00018D40
		public ErrorContext ErrorContext { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.ErrorEventArgs" /> class.
		/// </summary>
		/// <param name="currentObject">The current object.</param>
		/// <param name="errorContext">The error context.</param>
		// Token: 0x06000723 RID: 1827 RVA: 0x0001AB49 File Offset: 0x00018D49
		public ErrorEventArgs(object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}

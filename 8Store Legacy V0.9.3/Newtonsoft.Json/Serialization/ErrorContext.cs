using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Provides information surrounding an error.
	/// </summary>
	// Token: 0x0200008A RID: 138
	public class ErrorContext
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0001AA9C File Offset: 0x00018C9C
		internal ErrorContext(object originalObject, object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001AAC1 File Offset: 0x00018CC1
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001AAC9 File Offset: 0x00018CC9
		internal bool Traced { get; set; }

		/// <summary>
		/// Gets or sets the error.
		/// </summary>
		/// <value>The error.</value>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001AAD2 File Offset: 0x00018CD2
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001AADA File Offset: 0x00018CDA
		public Exception Error { get; private set; }

		/// <summary>
		/// Gets the original object that caused the error.
		/// </summary>
		/// <value>The original object that caused the error.</value>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001AAE3 File Offset: 0x00018CE3
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001AAEB File Offset: 0x00018CEB
		public object OriginalObject { get; private set; }

		/// <summary>
		/// Gets the member that caused the error.
		/// </summary>
		/// <value>The member that caused the error.</value>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001AAFC File Offset: 0x00018CFC
		public object Member { get; private set; }

		/// <summary>
		/// Gets the path of the JSON location where the error occurred.
		/// </summary>
		/// <value>The path of the JSON location where the error occurred.</value>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001AB05 File Offset: 0x00018D05
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001AB0D File Offset: 0x00018D0D
		public string Path { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Newtonsoft.Json.Serialization.ErrorContext" /> is handled.
		/// </summary>
		/// <value><c>true</c> if handled; otherwise, <c>false</c>.</value>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001AB16 File Offset: 0x00018D16
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001AB1E File Offset: 0x00018D1E
		public bool Handled { get; set; }
	}
}

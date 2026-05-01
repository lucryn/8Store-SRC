using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// Returns detailed information related to the <see cref="T:Newtonsoft.Json.Schema.ValidationEventHandler" />.
	/// </summary>
	// Token: 0x0200007E RID: 126
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x00018F6F File Offset: 0x0001716F
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Schema.JsonSchemaException" /> associated with the validation error.
		/// </summary>
		/// <value>The JsonSchemaException associated with the validation error.</value>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00018F89 File Offset: 0x00017189
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		/// <summary>
		/// Gets the path of the JSON location where the validation error occurred.
		/// </summary>
		/// <value>The path of the JSON location where the validation error occurred.</value>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00018F91 File Offset: 0x00017191
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		/// <summary>
		/// Gets the text description corresponding to the validation error.
		/// </summary>
		/// <value>The text description.</value>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00018F9E File Offset: 0x0001719E
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x0400026A RID: 618
		private readonly JsonSchemaException _ex;
	}
}

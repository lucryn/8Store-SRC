using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000BB RID: 187
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00026C27 File Offset: 0x00024E27
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00026C41 File Offset: 0x00024E41
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00026C49 File Offset: 0x00024E49
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00026C56 File Offset: 0x00024E56
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x040003A8 RID: 936
		private readonly JsonSchemaException _ex;
	}
}

using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000CB RID: 203
	public class JsonMergeSettings
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x00029B45 File Offset: 0x00027D45
		public JsonMergeSettings()
		{
			this._propertyNameComparison = 4;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00029B54 File Offset: 0x00027D54
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00029B5C File Offset: 0x00027D5C
		public MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return this._mergeArrayHandling;
			}
			set
			{
				if (value < MergeArrayHandling.Concat || value > MergeArrayHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeArrayHandling = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00029B78 File Offset: 0x00027D78
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00029B80 File Offset: 0x00027D80
		public MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return this._mergeNullValueHandling;
			}
			set
			{
				if (value < MergeNullValueHandling.Ignore || value > MergeNullValueHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeNullValueHandling = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00029B9C File Offset: 0x00027D9C
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00029BA4 File Offset: 0x00027DA4
		public StringComparison PropertyNameComparison
		{
			get
			{
				return this._propertyNameComparison;
			}
			set
			{
				if (value < 0 || value > 5)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._propertyNameComparison = value;
			}
		}

		// Token: 0x040003C3 RID: 963
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x040003C4 RID: 964
		private MergeNullValueHandling _mergeNullValueHandling;

		// Token: 0x040003C5 RID: 965
		private StringComparison _propertyNameComparison;
	}
}

using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000CA RID: 202
	public class JsonLoadSettings
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x00029ABC File Offset: 0x00027CBC
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00029AD9 File Offset: 0x00027CD9
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00029AE1 File Offset: 0x00027CE1
		public CommentHandling CommentHandling
		{
			get
			{
				return this._commentHandling;
			}
			set
			{
				if (value < CommentHandling.Ignore || value > CommentHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00029AFD File Offset: 0x00027CFD
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00029B05 File Offset: 0x00027D05
		public LineInfoHandling LineInfoHandling
		{
			get
			{
				return this._lineInfoHandling;
			}
			set
			{
				if (value < LineInfoHandling.Ignore || value > LineInfoHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lineInfoHandling = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00029B21 File Offset: 0x00027D21
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00029B29 File Offset: 0x00027D29
		public DuplicatePropertyNameHandling DuplicatePropertyNameHandling
		{
			get
			{
				return this._duplicatePropertyNameHandling;
			}
			set
			{
				if (value < DuplicatePropertyNameHandling.Replace || value > DuplicatePropertyNameHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._duplicatePropertyNameHandling = value;
			}
		}

		// Token: 0x040003C0 RID: 960
		private CommentHandling _commentHandling;

		// Token: 0x040003C1 RID: 961
		private LineInfoHandling _lineInfoHandling;

		// Token: 0x040003C2 RID: 962
		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;
	}
}

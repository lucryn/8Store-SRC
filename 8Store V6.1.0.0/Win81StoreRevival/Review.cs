using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x0200000E RID: 14
	public class Review : INotifyPropertyChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002FFC File Offset: 0x000011FC
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00003034 File Offset: 0x00001234
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000048 RID: 72 RVA: 0x00003069 File Offset: 0x00001269
		private void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003082 File Offset: 0x00001282
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000308A File Offset: 0x0000128A
		[JsonIgnore]
		public bool IsExpanded
		{
			get
			{
				return this._isExpanded;
			}
			set
			{
				this._isExpanded = value;
				this.OnPropertyChanged("IsExpanded");
				this.OnPropertyChanged("ShowShortComment");
				this.OnPropertyChanged("ShowReadMore");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000030B4 File Offset: 0x000012B4
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000030BC File Offset: 0x000012BC
		[JsonProperty("id")]
		public int Id { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000030C5 File Offset: 0x000012C5
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000030CD File Offset: 0x000012CD
		[JsonProperty("app_id")]
		public string AppId { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000030DE File Offset: 0x000012DE
		[JsonProperty("user_id")]
		public string UserId { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000030E7 File Offset: 0x000012E7
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000030EF File Offset: 0x000012EF
		[JsonProperty("username")]
		public string Username { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000030F8 File Offset: 0x000012F8
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003100 File Offset: 0x00001300
		[JsonProperty("rating")]
		public int Rating { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003109 File Offset: 0x00001309
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003111 File Offset: 0x00001311
		[JsonProperty("comment")]
		public string Comment { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000311A File Offset: 0x0000131A
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003122 File Offset: 0x00001322
		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000312B File Offset: 0x0000132B
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003133 File Offset: 0x00001333
		[JsonIgnore]
		public bool IsOwnReview { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000313C File Offset: 0x0000133C
		[JsonIgnore]
		public bool ShowShortComment
		{
			get
			{
				return this._isExpanded || !this.HasLongComment;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003151 File Offset: 0x00001351
		[JsonIgnore]
		public bool ShowReadMore
		{
			get
			{
				return !this._isExpanded && this.HasLongComment;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003163 File Offset: 0x00001363
		[JsonIgnore]
		private bool HasLongComment
		{
			get
			{
				return !string.IsNullOrEmpty(this.Comment) && this.Comment.Length > 150;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003186 File Offset: 0x00001386
		[JsonIgnore]
		public string CommentPreview
		{
			get
			{
				if (!this.HasLongComment)
				{
					return this.Comment;
				}
				return this.Comment.Substring(0, 150) + "...";
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000031B2 File Offset: 0x000013B2
		[JsonIgnore]
		public List<int> StarBg
		{
			get
			{
				List<int> list = new List<int>();
				list.Add(1);
				list.Add(1);
				list.Add(1);
				list.Add(1);
				list.Add(1);
				return list;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000031DC File Offset: 0x000013DC
		[JsonIgnore]
		public List<double> StarList
		{
			get
			{
				List<double> list = new List<double>();
				for (int i = 1; i <= 5; i++)
				{
					if (this.Rating >= i)
					{
						list.Add(1.0);
					}
					else if (this.Rating > i - 1)
					{
						list.Add((double)this.Rating - (double)(i - 1));
					}
					else
					{
						list.Add(0.0);
					}
				}
				return list;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003244 File Offset: 0x00001444
		[JsonIgnore]
		public string DateDisplay
		{
			get
			{
				return this.CreatedAt.ToString("MMM dd, yyyy");
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003264 File Offset: 0x00001464
		public void Expand()
		{
			this._isExpanded = true;
			this.OnPropertyChanged("ShowShortComment");
			this.OnPropertyChanged("ShowReadMore");
		}

		// Token: 0x0400001D RID: 29
		private bool _isExpanded;
	}
}

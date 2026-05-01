using System;
using System.Collections.Generic;

namespace Win81StoreRevival.Languages
{
	// Token: 0x02000057 RID: 87
	public static class Data
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0001CC38 File Offset: 0x0001AE38
		// Note: this type is marked as 'beforefieldinit'.
		static Data()
		{
			List<LangItem> list = new List<LangItem>();
			list.Add(new LangItem
			{
				Name = "English",
				Tag = "en-US"
			});
			list.Add(new LangItem
			{
				Name = "Русский",
				Tag = "ru-RU",
				Authors = "mssans & flyne01"
			});
			list.Add(new LangItem
			{
				Name = "中文 (繁體)",
				Tag = "zh-HK",
				Authors = "ChockingNetDude"
			});
			list.Add(new LangItem
			{
				Name = "中文 (简体)",
				Tag = "zh-CN",
				Authors = "ChockingNetDude"
			});
			list.Add(new LangItem
			{
				Name = "Français",
				Tag = "fr-FR",
				Authors = "airbenztv_ & noolamin"
			});
			list.Add(new LangItem
			{
				Name = "Italiano",
				Tag = "it-IT",
				Authors = "Milly_203r"
			});
			list.Add(new LangItem
			{
				Name = "Polski",
				Tag = "pl-PL",
				Authors = "Lusiek74"
			});
			list.Add(new LangItem
			{
				Name = "العربية",
				Tag = "ar-SA",
				Authors = "VBMeta, Sad Ghost Tech & mohammad aljafari"
			});
			Data.Languages = list;
		}

		// Token: 0x04000252 RID: 594
		public static List<LangItem> Languages;
	}
}

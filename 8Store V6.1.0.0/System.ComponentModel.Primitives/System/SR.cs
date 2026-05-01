using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.ComponentModel.Primitives;

namespace System
{
	// Token: 0x02000003 RID: 3
	internal static class SR
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ResourceManager ResourceManager
		{
			get
			{
				if (System.SR.s_resourceManager == null)
				{
					System.SR.s_resourceManager = new ResourceManager(System.SR.ResourceType);
				}
				return System.SR.s_resourceManager;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206D File Offset: 0x0000026D
		[MethodImpl(8)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000270
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, 4))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D7 File Offset: 0x000002D7
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002109 File Offset: 0x00000309
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1,
				p2
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002191 File Offset: 0x00000391
		internal static string PropertyCategoryAction
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryAction", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219E File Offset: 0x0000039E
		internal static string PropertyCategoryAppearance
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryAppearance", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021AB File Offset: 0x000003AB
		internal static string PropertyCategoryAsynchronous
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryAsynchronous", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		internal static string PropertyCategoryBehavior
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryBehavior", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C5 File Offset: 0x000003C5
		internal static string PropertyCategoryConfig
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryConfig", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021D2 File Offset: 0x000003D2
		internal static string PropertyCategoryData
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryData", null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021DF File Offset: 0x000003DF
		internal static string PropertyCategoryDDE
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryDDE", null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021EC File Offset: 0x000003EC
		internal static string PropertyCategoryDefault
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryDefault", null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F9 File Offset: 0x000003F9
		internal static string PropertyCategoryDesign
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryDesign", null);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002206 File Offset: 0x00000406
		internal static string PropertyCategoryDragDrop
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryDragDrop", null);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002213 File Offset: 0x00000413
		internal static string PropertyCategoryFocus
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryFocus", null);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002220 File Offset: 0x00000420
		internal static string PropertyCategoryFont
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryFont", null);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000222D File Offset: 0x0000042D
		internal static string PropertyCategoryFormat
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryFormat", null);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000223A File Offset: 0x0000043A
		internal static string PropertyCategoryKey
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryKey", null);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002247 File Offset: 0x00000447
		internal static string PropertyCategoryLayout
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryLayout", null);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002254 File Offset: 0x00000454
		internal static string PropertyCategoryList
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryList", null);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002261 File Offset: 0x00000461
		internal static string PropertyCategoryMouse
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryMouse", null);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000226E File Offset: 0x0000046E
		internal static string PropertyCategoryPosition
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryPosition", null);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000227B File Offset: 0x0000047B
		internal static string PropertyCategoryScale
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryScale", null);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002288 File Offset: 0x00000488
		internal static string PropertyCategoryText
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryText", null);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002295 File Offset: 0x00000495
		internal static string PropertyCategoryWindowStyle
		{
			get
			{
				return System.SR.GetResourceString("PropertyCategoryWindowStyle", null);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022A2 File Offset: 0x000004A2
		internal static Type ResourceType
		{
			get
			{
				return typeof(FxResources.System.ComponentModel.Primitives.SR);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager s_resourceManager;

		// Token: 0x04000002 RID: 2
		private const string s_resourcesName = "FxResources.System.ComponentModel.Primitives.SR";
	}
}

using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.ComponentModel.TypeConverter;

namespace System
{
	// Token: 0x02000004 RID: 4
	internal static class SR
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002155 File Offset: 0x00000355
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

		// Token: 0x06000007 RID: 7 RVA: 0x00002172 File Offset: 0x00000372
		[MethodImpl(8)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
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

		// Token: 0x06000009 RID: 9 RVA: 0x000021B8 File Offset: 0x000003B8
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

		// Token: 0x0600000A RID: 10 RVA: 0x000021DF File Offset: 0x000003DF
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

		// Token: 0x0600000B RID: 11 RVA: 0x00002211 File Offset: 0x00000411
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

		// Token: 0x0600000C RID: 12 RVA: 0x0000224C File Offset: 0x0000044C
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
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002299 File Offset: 0x00000499
		internal static string Array
		{
			get
			{
				return System.SR.GetResourceString("Array", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022A6 File Offset: 0x000004A6
		internal static string Collection
		{
			get
			{
				return System.SR.GetResourceString("Collection", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022B3 File Offset: 0x000004B3
		internal static string ConvertFromException
		{
			get
			{
				return System.SR.GetResourceString("ConvertFromException", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022C0 File Offset: 0x000004C0
		internal static string ConvertInvalidPrimitive
		{
			get
			{
				return System.SR.GetResourceString("ConvertInvalidPrimitive", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022CD File Offset: 0x000004CD
		internal static string ConvertToException
		{
			get
			{
				return System.SR.GetResourceString("ConvertToException", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022DA File Offset: 0x000004DA
		internal static string EnumConverterInvalidValue
		{
			get
			{
				return System.SR.GetResourceString("EnumConverterInvalidValue", null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022E7 File Offset: 0x000004E7
		internal static string ErrorInvalidEventHandler
		{
			get
			{
				return System.SR.GetResourceString("ErrorInvalidEventHandler", null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022F4 File Offset: 0x000004F4
		internal static string ErrorInvalidEventType
		{
			get
			{
				return System.SR.GetResourceString("ErrorInvalidEventType", null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002301 File Offset: 0x00000501
		internal static string ErrorInvalidPropertyType
		{
			get
			{
				return System.SR.GetResourceString("ErrorInvalidPropertyType", null);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000230E File Offset: 0x0000050E
		internal static string ErrorMissingEventAccessors
		{
			get
			{
				return System.SR.GetResourceString("ErrorMissingEventAccessors", null);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000231B File Offset: 0x0000051B
		internal static string ErrorMissingPropertyAccessors
		{
			get
			{
				return System.SR.GetResourceString("ErrorMissingPropertyAccessors", null);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002328 File Offset: 0x00000528
		internal static string ErrorPropertyAccessorException
		{
			get
			{
				return System.SR.GetResourceString("ErrorPropertyAccessorException", null);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002335 File Offset: 0x00000535
		internal static string InvalidMemberName
		{
			get
			{
				return System.SR.GetResourceString("InvalidMemberName", null);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002342 File Offset: 0x00000542
		internal static string InvalidNullArgument
		{
			get
			{
				return System.SR.GetResourceString("InvalidNullArgument", null);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000234F File Offset: 0x0000054F
		internal static string MetaExtenderName
		{
			get
			{
				return System.SR.GetResourceString("MetaExtenderName", null);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000235C File Offset: 0x0000055C
		internal static string none
		{
			get
			{
				return System.SR.GetResourceString("none", null);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002369 File Offset: 0x00000569
		internal static string Null
		{
			get
			{
				return System.SR.GetResourceString("Null", null);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002376 File Offset: 0x00000576
		internal static string NullableConverterBadCtorArg
		{
			get
			{
				return System.SR.GetResourceString("NullableConverterBadCtorArg", null);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002383 File Offset: 0x00000583
		internal static string Text
		{
			get
			{
				return System.SR.GetResourceString("Text", null);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002390 File Offset: 0x00000590
		internal static string TypeDescriptorAlreadyAssociated
		{
			get
			{
				return System.SR.GetResourceString("TypeDescriptorAlreadyAssociated", null);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000239D File Offset: 0x0000059D
		internal static string TypeDescriptorArgsCountMismatch
		{
			get
			{
				return System.SR.GetResourceString("TypeDescriptorArgsCountMismatch", null);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023AA File Offset: 0x000005AA
		internal static string TypeDescriptorProviderError
		{
			get
			{
				return System.SR.GetResourceString("TypeDescriptorProviderError", null);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023B7 File Offset: 0x000005B7
		internal static string TypeDescriptorUnsupportedRemoteObject
		{
			get
			{
				return System.SR.GetResourceString("TypeDescriptorUnsupportedRemoteObject", null);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023C4 File Offset: 0x000005C4
		internal static string TypeDescriptorExpectedElementType
		{
			get
			{
				return System.SR.GetResourceString("TypeDescriptorExpectedElementType", null);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023D1 File Offset: 0x000005D1
		internal static string TypeDescriptorSameAssociation
		{
			get
			{
				return System.SR.GetResourceString("TypeDescriptorSameAssociation", null);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023DE File Offset: 0x000005DE
		internal static Type ResourceType
		{
			get
			{
				return typeof(FxResources.System.ComponentModel.TypeConverter.SR);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager s_resourceManager;

		// Token: 0x04000002 RID: 2
		private const string s_resourcesName = "FxResources.System.ComponentModel.TypeConverter.SR";
	}
}

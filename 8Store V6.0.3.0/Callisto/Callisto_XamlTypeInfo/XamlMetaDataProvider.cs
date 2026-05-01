using System;
using System.CodeDom.Compiler;
using Windows.UI.Xaml.Markup;

namespace Callisto.Callisto_XamlTypeInfo
{
	// Token: 0x0200003F RID: 63
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	public sealed class XamlMetaDataProvider : IXamlMetadataProvider
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000CE8F File Offset: 0x0000B08F
		public IXamlType GetXamlType(Type type)
		{
			if (this._provider == null)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByType(type);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		public IXamlType GetXamlType(string fullName)
		{
			if (this._provider == null)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByName(fullName);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000CED1 File Offset: 0x0000B0D1
		public XmlnsDefinition[] GetXmlnsDefinitions()
		{
			return new XmlnsDefinition[0];
		}

		// Token: 0x04000135 RID: 309
		private XamlTypeInfoProvider _provider;
	}
}

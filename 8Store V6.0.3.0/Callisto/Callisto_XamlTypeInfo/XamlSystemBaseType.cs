using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml.Markup;

namespace Callisto.Callisto_XamlTypeInfo
{
	// Token: 0x02000041 RID: 65
	[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class XamlSystemBaseType : IXamlType
	{
		// Token: 0x06000327 RID: 807 RVA: 0x000103E5 File Offset: 0x0000E5E5
		public XamlSystemBaseType(string fullName, Type underlyingType)
		{
			this._fullName = fullName;
			this._underlyingType = underlyingType;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000103FB File Offset: 0x0000E5FB
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00010403 File Offset: 0x0000E603
		public Type UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0001040B File Offset: 0x0000E60B
		public virtual IXamlType BaseType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00010412 File Offset: 0x0000E612
		public virtual IXamlMember ContentProperty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00010419 File Offset: 0x0000E619
		public virtual IXamlMember GetMember(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00010420 File Offset: 0x0000E620
		public virtual bool IsArray
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00010427 File Offset: 0x0000E627
		public virtual bool IsCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0001042E File Offset: 0x0000E62E
		public virtual bool IsConstructible
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00010435 File Offset: 0x0000E635
		public virtual bool IsDictionary
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0001043C File Offset: 0x0000E63C
		public virtual bool IsMarkupExtension
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00010443 File Offset: 0x0000E643
		public virtual bool IsBindable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0001044A File Offset: 0x0000E64A
		public virtual bool IsReturnTypeStub
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00010451 File Offset: 0x0000E651
		public virtual IXamlType ItemType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00010458 File Offset: 0x0000E658
		public virtual IXamlType KeyType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001045F File Offset: 0x0000E65F
		public virtual object ActivateInstance()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010466 File Offset: 0x0000E666
		public virtual void AddToMap(object instance, object key, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001046D File Offset: 0x0000E66D
		public virtual void AddToVector(object instance, object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010474 File Offset: 0x0000E674
		public virtual void RunInitializer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001047B File Offset: 0x0000E67B
		public virtual object CreateFromString(string input)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400013B RID: 315
		private string _fullName;

		// Token: 0x0400013C RID: 316
		private Type _underlyingType;
	}
}

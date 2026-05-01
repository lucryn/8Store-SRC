using System;

namespace System.ComponentModel
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(32767)]
	public sealed class TypeConverterAttribute : Attribute
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00003F68 File Offset: 0x00002168
		public TypeConverterAttribute()
		{
			this._typeName = string.Empty;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003F7B File Offset: 0x0000217B
		public TypeConverterAttribute(Type type)
		{
			this._typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003F8F File Offset: 0x0000218F
		public TypeConverterAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003F9E File Offset: 0x0000219E
		public string ConverterTypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003FA8 File Offset: 0x000021A8
		public override bool Equals(object obj)
		{
			TypeConverterAttribute typeConverterAttribute = obj as TypeConverterAttribute;
			return typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName == this._typeName;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003FD2 File Offset: 0x000021D2
		public override int GetHashCode()
		{
			return this._typeName.GetHashCode();
		}

		// Token: 0x0400000C RID: 12
		private readonly string _typeName;

		// Token: 0x0400000D RID: 13
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();
	}
}

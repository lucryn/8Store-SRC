using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// The default serialization binder used when resolving and loading classes from type names.
	/// </summary>
	// Token: 0x02000088 RID: 136
	public class DefaultSerializationBinder : SerializationBinder
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0001A94C File Offset: 0x00018B4C
		private static Type GetTypeFromTypeNameKey(DefaultSerializationBinder.TypeNameKey typeNameKey)
		{
			string assemblyName = typeNameKey.AssemblyName;
			string typeName = typeNameKey.TypeName;
			if (assemblyName == null)
			{
				return Type.GetType(typeName);
			}
			Assembly assembly = Assembly.Load(new AssemblyName(assemblyName));
			if (assembly == null)
			{
				throw new JsonSerializationException("Could not load assembly '{0}'.".FormatWith(CultureInfo.InvariantCulture, assemblyName));
			}
			Type type = assembly.GetType(typeName);
			if (type == null)
			{
				throw new JsonSerializationException("Could not find type '{0}' in assembly '{1}'.".FormatWith(CultureInfo.InvariantCulture, typeName, assembly.FullName));
			}
			return type;
		}

		/// <summary>
		/// When overridden in a derived class, controls the binding of a serialized object to a type.
		/// </summary>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object.</param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object.</param>
		/// <returns>
		/// The type of the object the formatter creates a new instance of.
		/// </returns>
		// Token: 0x0600070A RID: 1802 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		public override Type BindToType(string assemblyName, string typeName)
		{
			return this._typeCache.Get(new DefaultSerializationBinder.TypeNameKey(assemblyName, typeName));
		}

		/// <summary>
		/// When overridden in a derived class, controls the binding of a serialized object to a type.
		/// </summary>
		/// <param name="serializedType">The type of the object the formatter creates a new instance of.</param>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object. </param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object. </param>
		// Token: 0x0600070B RID: 1803 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = IntrospectionExtensions.GetTypeInfo(serializedType).Assembly.FullName;
			typeName = serializedType.FullName;
		}

		// Token: 0x0400027B RID: 635
		internal static readonly DefaultSerializationBinder Instance = new DefaultSerializationBinder();

		// Token: 0x0400027C RID: 636
		private readonly ThreadSafeStore<DefaultSerializationBinder.TypeNameKey, Type> _typeCache = new ThreadSafeStore<DefaultSerializationBinder.TypeNameKey, Type>(new Func<DefaultSerializationBinder.TypeNameKey, Type>(DefaultSerializationBinder.GetTypeFromTypeNameKey));

		// Token: 0x02000089 RID: 137
		internal struct TypeNameKey : IEquatable<DefaultSerializationBinder.TypeNameKey>
		{
			// Token: 0x0600070E RID: 1806 RVA: 0x0001AA1B File Offset: 0x00018C1B
			public TypeNameKey(string assemblyName, string typeName)
			{
				this.AssemblyName = assemblyName;
				this.TypeName = typeName;
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x0001AA2B File Offset: 0x00018C2B
			public override int GetHashCode()
			{
				return ((this.AssemblyName != null) ? this.AssemblyName.GetHashCode() : 0) ^ ((this.TypeName != null) ? this.TypeName.GetHashCode() : 0);
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x0001AA5A File Offset: 0x00018C5A
			public override bool Equals(object obj)
			{
				return obj is DefaultSerializationBinder.TypeNameKey && this.Equals((DefaultSerializationBinder.TypeNameKey)obj);
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x0001AA72 File Offset: 0x00018C72
			public bool Equals(DefaultSerializationBinder.TypeNameKey other)
			{
				return this.AssemblyName == other.AssemblyName && this.TypeName == other.TypeName;
			}

			// Token: 0x0400027D RID: 637
			internal readonly string AssemblyName;

			// Token: 0x0400027E RID: 638
			internal readonly string TypeName;
		}
	}
}

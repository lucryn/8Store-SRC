using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Allows users to control class loading and mandate what class to load.
	/// </summary>
	// Token: 0x02000080 RID: 128
	public abstract class SerializationBinder
	{
		/// <summary>
		/// When overridden in a derived class, controls the binding of a serialized object to a type.
		/// </summary>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object.</param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object</param>
		/// <returns>The type of the object the formatter creates a new instance of.</returns>
		// Token: 0x060006C4 RID: 1732
		public abstract Type BindToType(string assemblyName, string typeName);

		/// <summary>
		/// When overridden in a derived class, controls the binding of a serialized object to a type.
		/// </summary>
		/// <param name="serializedType">The type of the object the formatter creates a new instance of.</param>
		/// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object.</param>
		/// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object.</param>
		// Token: 0x060006C5 RID: 1733 RVA: 0x00018FAB File Offset: 0x000171AB
		public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}
	}
}

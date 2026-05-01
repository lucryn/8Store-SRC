using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	[Nullable(0)]
	internal class DefaultReferenceResolver : IReferenceResolver
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x0001B23C File Offset: 0x0001943C
		private BidirectionalDictionary<string, object> GetMappings(object context)
		{
			JsonSerializerInternalBase jsonSerializerInternalBase = context as JsonSerializerInternalBase;
			if (jsonSerializerInternalBase == null)
			{
				JsonSerializerProxy jsonSerializerProxy = context as JsonSerializerProxy;
				if (jsonSerializerProxy == null)
				{
					throw new JsonException("The DefaultReferenceResolver can only be used internally.");
				}
				jsonSerializerInternalBase = jsonSerializerProxy.GetInternalSerializer();
			}
			return jsonSerializerInternalBase.DefaultReferenceMappings;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001B278 File Offset: 0x00019478
		public object ResolveReference(object context, string reference)
		{
			object result;
			this.GetMappings(context).TryGetByFirst(reference, out result);
			return result;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001B298 File Offset: 0x00019498
		public string GetReference(object context, object value)
		{
			BidirectionalDictionary<string, object> mappings = this.GetMappings(context);
			string text;
			if (!mappings.TryGetBySecond(value, out text))
			{
				this._referenceCount++;
				text = this._referenceCount.ToString(CultureInfo.InvariantCulture);
				mappings.Set(text, value);
			}
			return text;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001B2E0 File Offset: 0x000194E0
		public void AddReference(object context, string reference, object value)
		{
			this.GetMappings(context).Set(reference, value);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001B2F0 File Offset: 0x000194F0
		public bool IsReferenced(object context, object value)
		{
			string text;
			return this.GetMappings(context).TryGetBySecond(value, out text);
		}

		// Token: 0x0400027B RID: 635
		private int _referenceCount;
	}
}

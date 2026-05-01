using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000087 RID: 135
	internal class DefaultReferenceResolver : IReferenceResolver
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0001A868 File Offset: 0x00018A68
		private BidirectionalDictionary<string, object> GetMappings(object context)
		{
			JsonSerializerInternalBase jsonSerializerInternalBase;
			if (context is JsonSerializerInternalBase)
			{
				jsonSerializerInternalBase = (JsonSerializerInternalBase)context;
			}
			else
			{
				if (!(context is JsonSerializerProxy))
				{
					throw new JsonException("The DefaultReferenceResolver can only be used internally.");
				}
				jsonSerializerInternalBase = ((JsonSerializerProxy)context).GetInternalSerializer();
			}
			return jsonSerializerInternalBase.DefaultReferenceMappings;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public object ResolveReference(object context, string reference)
		{
			object result;
			this.GetMappings(context).TryGetByFirst(reference, out result);
			return result;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001A8D0 File Offset: 0x00018AD0
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

		// Token: 0x06000706 RID: 1798 RVA: 0x0001A918 File Offset: 0x00018B18
		public void AddReference(object context, string reference, object value)
		{
			this.GetMappings(context).Set(reference, value);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001A928 File Offset: 0x00018B28
		public bool IsReferenced(object context, object value)
		{
			string text;
			return this.GetMappings(context).TryGetBySecond(value, out text);
		}

		// Token: 0x0400027A RID: 634
		private int _referenceCount;
	}
}

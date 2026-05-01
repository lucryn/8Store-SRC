using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000046 RID: 70
	internal struct JsonPosition
	{
		// Token: 0x06000270 RID: 624 RVA: 0x00009A04 File Offset: 0x00007C04
		public JsonPosition(JsonContainerType type)
		{
			this.Type = type;
			this.HasIndex = JsonPosition.TypeHasIndex(type);
			this.Position = -1;
			this.PropertyName = null;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009A28 File Offset: 0x00007C28
		internal void WriteTo(StringBuilder sb)
		{
			switch (this.Type)
			{
			case JsonContainerType.Object:
				if (sb.Length > 0)
				{
					sb.Append(".");
				}
				sb.Append(this.PropertyName);
				return;
			case JsonContainerType.Array:
			case JsonContainerType.Constructor:
				sb.Append("[");
				sb.Append(this.Position);
				sb.Append("]");
				return;
			default:
				return;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009A99 File Offset: 0x00007C99
		internal static bool TypeHasIndex(JsonContainerType type)
		{
			return type == JsonContainerType.Array || type == JsonContainerType.Constructor;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009AA8 File Offset: 0x00007CA8
		internal static string BuildPath(IEnumerable<JsonPosition> positions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (JsonPosition jsonPosition in positions)
			{
				jsonPosition.WriteTo(stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009B00 File Offset: 0x00007D00
		internal static string FormatMessage(IJsonLineInfo lineInfo, string path, string message)
		{
			if (!message.EndsWith(Environment.NewLine))
			{
				message = message.Trim();
				if (!message.EndsWith("."))
				{
					message += ".";
				}
				message += " ";
			}
			message += "Path '{0}'".FormatWith(CultureInfo.InvariantCulture, path);
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				message += ", line {0}, position {1}".FormatWith(CultureInfo.InvariantCulture, lineInfo.LineNumber, lineInfo.LinePosition);
			}
			message += ".";
			return message;
		}

		// Token: 0x040000D9 RID: 217
		internal JsonContainerType Type;

		// Token: 0x040000DA RID: 218
		internal int Position;

		// Token: 0x040000DB RID: 219
		internal string PropertyName;

		// Token: 0x040000DC RID: 220
		internal bool HasIndex;
	}
}

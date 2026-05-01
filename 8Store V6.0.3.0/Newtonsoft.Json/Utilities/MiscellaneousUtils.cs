using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006E RID: 110
	[NullableContext(1)]
	[Nullable(0)]
	internal static class MiscellaneousUtils
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x00016C9B File Offset: 0x00014E9B
		[NullableContext(2)]
		[Conditional("DEBUG")]
		public static void Assert([DoesNotReturnIf(false)] bool condition, string message = null)
		{
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00016CA0 File Offset: 0x00014EA0
		[NullableContext(2)]
		public static bool ValueEquals(object objA, object objB)
		{
			if (objA == objB)
			{
				return true;
			}
			if (objA == null || objB == null)
			{
				return false;
			}
			if (objA.GetType() == objB.GetType())
			{
				return objA.Equals(objB);
			}
			if (ConvertUtils.IsInteger(objA) && ConvertUtils.IsInteger(objB))
			{
				return Convert.ToDecimal(objA, CultureInfo.CurrentCulture).Equals(Convert.ToDecimal(objB, CultureInfo.CurrentCulture));
			}
			return (objA is double || objA is float || objA is decimal) && (objB is double || objB is float || objB is decimal) && MathUtils.ApproxEquals(Convert.ToDouble(objA, CultureInfo.CurrentCulture), Convert.ToDouble(objB, CultureInfo.CurrentCulture));
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00016D50 File Offset: 0x00014F50
		public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string paramName, object actualValue, string message)
		{
			string text = message + Environment.NewLine + "Actual value was {0}.".FormatWith(CultureInfo.InvariantCulture, actualValue);
			return new ArgumentOutOfRangeException(paramName, text);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00016D80 File Offset: 0x00014F80
		public static string ToString([Nullable(2)] object value)
		{
			if (value == null)
			{
				return "{null}";
			}
			string text = value as string;
			if (text == null)
			{
				return value.ToString();
			}
			return "\"" + text + "\"";
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00016DB8 File Offset: 0x00014FB8
		public static int ByteArrayCompare(byte[] a1, byte[] a2)
		{
			int num = a1.Length.CompareTo(a2.Length);
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				int num2 = a1[i].CompareTo(a2[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return 0;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00016E00 File Offset: 0x00015000
		[return: Nullable(2)]
		public static string GetPrefix(string qualifiedName)
		{
			string result;
			string text;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out result, out text);
			return result;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00016E18 File Offset: 0x00015018
		public static string GetLocalName(string qualifiedName)
		{
			string text;
			string result;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out text, out result);
			return result;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016E30 File Offset: 0x00015030
		public static void GetQualifiedNameParts(string qualifiedName, [Nullable(2)] out string prefix, out string localName)
		{
			int num = StringUtils.IndexOf(qualifiedName, ':');
			if (num == -1 || num == 0 || qualifiedName.Length - 1 == num)
			{
				prefix = null;
				localName = qualifiedName;
				return;
			}
			prefix = qualifiedName.Substring(0, num);
			localName = qualifiedName.Substring(num + 1);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00016E74 File Offset: 0x00015074
		internal static RegexOptions GetRegexOptions(string optionsText)
		{
			RegexOptions regexOptions = 0;
			for (int i = 0; i < optionsText.Length; i++)
			{
				char c = optionsText.get_Chars(i);
				if (c <= 'm')
				{
					if (c != 'i')
					{
						if (c == 'm')
						{
							regexOptions |= 2;
						}
					}
					else
					{
						regexOptions |= 1;
					}
				}
				else if (c != 's')
				{
					if (c == 'x')
					{
						regexOptions |= 4;
					}
				}
				else
				{
					regexOptions |= 16;
				}
			}
			return regexOptions;
		}

		// Token: 0x0400023B RID: 571
		internal const string TrimWarning = "Newtonsoft.Json relies on reflection over types that may be removed when trimming.";

		// Token: 0x0400023C RID: 572
		internal const string AotWarning = "Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.";
	}
}

using System;
using System.Runtime.InteropServices;

namespace Callisto
{
	// Token: 0x0200002E RID: 46
	internal static class NumericExtensions
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public static bool IsNaN(this double value)
		{
			NumericExtensions.NanUnion nanUnion = new NumericExtensions.NanUnion
			{
				FloatingValue = value
			};
			ulong num = nanUnion.IntegerValue & 18442240474082181120UL;
			if (num != 9218868437227405312UL && num != 18442240474082181120UL)
			{
				return false;
			}
			ulong num2 = nanUnion.IntegerValue & 4503599627370495UL;
			return num2 != 0UL;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000BE29 File Offset: 0x0000A029
		public static bool IsGreaterThan(double left, double right)
		{
			return left > right && !NumericExtensions.AreClose(left, right);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000BE3C File Offset: 0x0000A03C
		public static bool AreClose(double left, double right)
		{
			if (left == right)
			{
				return true;
			}
			double num = (Math.Abs(left) + Math.Abs(right) + 10.0) * 2.220446049250313E-16;
			double num2 = left - right;
			return -num < num2 && num > num2;
		}

		// Token: 0x0200002F RID: 47
		[StructLayout(2)]
		private struct NanUnion
		{
			// Token: 0x04000105 RID: 261
			[FieldOffset(0)]
			internal double FloatingValue;

			// Token: 0x04000106 RID: 262
			[FieldOffset(0)]
			internal ulong IntegerValue;
		}
	}
}

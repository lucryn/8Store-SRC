using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004D RID: 77
	[NullableContext(1)]
	[Nullable(0)]
	internal static class BoxedPrimitives
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0001046F File Offset: 0x0000E66F
		internal static object Get(bool value)
		{
			if (!value)
			{
				return BoxedPrimitives.BooleanFalse;
			}
			return BoxedPrimitives.BooleanTrue;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00010480 File Offset: 0x0000E680
		internal static object Get(int value)
		{
			object result;
			switch (value)
			{
			case -1:
				result = BoxedPrimitives.Int32_M1;
				break;
			case 0:
				result = BoxedPrimitives.Int32_0;
				break;
			case 1:
				result = BoxedPrimitives.Int32_1;
				break;
			case 2:
				result = BoxedPrimitives.Int32_2;
				break;
			case 3:
				result = BoxedPrimitives.Int32_3;
				break;
			case 4:
				result = BoxedPrimitives.Int32_4;
				break;
			case 5:
				result = BoxedPrimitives.Int32_5;
				break;
			case 6:
				result = BoxedPrimitives.Int32_6;
				break;
			case 7:
				result = BoxedPrimitives.Int32_7;
				break;
			case 8:
				result = BoxedPrimitives.Int32_8;
				break;
			default:
				result = value;
				break;
			}
			return result;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00010518 File Offset: 0x0000E718
		internal static object Get(long value)
		{
			long num = value - -1L;
			if (num <= 9L)
			{
				switch ((uint)num)
				{
				case 0U:
					return BoxedPrimitives.Int64_M1;
				case 1U:
					return BoxedPrimitives.Int64_0;
				case 2U:
					return BoxedPrimitives.Int64_1;
				case 3U:
					return BoxedPrimitives.Int64_2;
				case 4U:
					return BoxedPrimitives.Int64_3;
				case 5U:
					return BoxedPrimitives.Int64_4;
				case 6U:
					return BoxedPrimitives.Int64_5;
				case 7U:
					return BoxedPrimitives.Int64_6;
				case 8U:
					return BoxedPrimitives.Int64_7;
				case 9U:
					return BoxedPrimitives.Int64_8;
				}
			}
			return value;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000105BD File Offset: 0x0000E7BD
		internal static object Get(decimal value)
		{
			return value;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000105C8 File Offset: 0x0000E7C8
		internal static object Get(double value)
		{
			if (value == 0.0)
			{
				if (!double.IsNegativeInfinity(1.0 / value))
				{
					return BoxedPrimitives.DoubleZero;
				}
				return BoxedPrimitives.DoubleNegativeZero;
			}
			else if (double.IsInfinity(value))
			{
				if (!double.IsPositiveInfinity(value))
				{
					return BoxedPrimitives.DoubleNegativeInfinity;
				}
				return BoxedPrimitives.DoublePositiveInfinity;
			}
			else
			{
				if (double.IsNaN(value))
				{
					return BoxedPrimitives.DoubleNaN;
				}
				return value;
			}
		}

		// Token: 0x0400017E RID: 382
		internal static readonly object BooleanTrue = true;

		// Token: 0x0400017F RID: 383
		internal static readonly object BooleanFalse = false;

		// Token: 0x04000180 RID: 384
		internal static readonly object Int32_M1 = -1;

		// Token: 0x04000181 RID: 385
		internal static readonly object Int32_0 = 0;

		// Token: 0x04000182 RID: 386
		internal static readonly object Int32_1 = 1;

		// Token: 0x04000183 RID: 387
		internal static readonly object Int32_2 = 2;

		// Token: 0x04000184 RID: 388
		internal static readonly object Int32_3 = 3;

		// Token: 0x04000185 RID: 389
		internal static readonly object Int32_4 = 4;

		// Token: 0x04000186 RID: 390
		internal static readonly object Int32_5 = 5;

		// Token: 0x04000187 RID: 391
		internal static readonly object Int32_6 = 6;

		// Token: 0x04000188 RID: 392
		internal static readonly object Int32_7 = 7;

		// Token: 0x04000189 RID: 393
		internal static readonly object Int32_8 = 8;

		// Token: 0x0400018A RID: 394
		internal static readonly object Int64_M1 = -1L;

		// Token: 0x0400018B RID: 395
		internal static readonly object Int64_0 = 0L;

		// Token: 0x0400018C RID: 396
		internal static readonly object Int64_1 = 1L;

		// Token: 0x0400018D RID: 397
		internal static readonly object Int64_2 = 2L;

		// Token: 0x0400018E RID: 398
		internal static readonly object Int64_3 = 3L;

		// Token: 0x0400018F RID: 399
		internal static readonly object Int64_4 = 4L;

		// Token: 0x04000190 RID: 400
		internal static readonly object Int64_5 = 5L;

		// Token: 0x04000191 RID: 401
		internal static readonly object Int64_6 = 6L;

		// Token: 0x04000192 RID: 402
		internal static readonly object Int64_7 = 7L;

		// Token: 0x04000193 RID: 403
		internal static readonly object Int64_8 = 8L;

		// Token: 0x04000194 RID: 404
		internal static readonly object DoubleNaN = double.NaN;

		// Token: 0x04000195 RID: 405
		internal static readonly object DoublePositiveInfinity = double.PositiveInfinity;

		// Token: 0x04000196 RID: 406
		internal static readonly object DoubleNegativeInfinity = double.NegativeInfinity;

		// Token: 0x04000197 RID: 407
		internal static readonly object DoubleZero = 0.0;

		// Token: 0x04000198 RID: 408
		internal static readonly object DoubleNegativeZero = --0.0;
	}
}

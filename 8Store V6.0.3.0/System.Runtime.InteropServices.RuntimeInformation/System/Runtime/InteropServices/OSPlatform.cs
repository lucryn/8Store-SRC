using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000007 RID: 7
	public struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000023CE File Offset: 0x000005CE
		public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023D5 File Offset: 0x000005D5
		public static OSPlatform OSX { get; } = new OSPlatform("OSX");

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023DC File Offset: 0x000005DC
		public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

		// Token: 0x06000013 RID: 19 RVA: 0x000023E3 File Offset: 0x000005E3
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyValue, "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002412 File Offset: 0x00000612
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000241A File Offset: 0x0000061A
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002428 File Offset: 0x00000628
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, 4);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002437 File Offset: 0x00000637
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000244F File Offset: 0x0000064F
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002466 File Offset: 0x00000666
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002477 File Offset: 0x00000677
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002481 File Offset: 0x00000681
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x0400000F RID: 15
		private readonly string _osPlatform;
	}
}

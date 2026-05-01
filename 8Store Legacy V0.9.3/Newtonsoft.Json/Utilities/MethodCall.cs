using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000CE RID: 206
	// (Invoke) Token: 0x060009CE RID: 2510
	internal delegate TResult MethodCall<T, TResult>(T target, params object[] args);
}

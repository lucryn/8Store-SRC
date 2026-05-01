using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000070 RID: 112
	[NullableContext(2)]
	[Nullable(0)]
	internal class ReflectionMember
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00016F8E File Offset: 0x0001518E
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00016F96 File Offset: 0x00015196
		public Type MemberType { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00016F9F File Offset: 0x0001519F
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00016FA7 File Offset: 0x000151A7
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Func<object, object> Getter { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00016FB0 File Offset: 0x000151B0
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00016FB8 File Offset: 0x000151B8
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Action<object, object> Setter { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }
	}
}

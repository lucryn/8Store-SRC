using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005C RID: 92
	internal class DynamicProxy<T>
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x00013993 File Offset: 0x00011B93
		public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return new string[0];
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001399B File Offset: 0x00011B9B
		public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000139A2 File Offset: 0x00011BA2
		public virtual bool TryConvert(T instance, ConvertBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000139A8 File Offset: 0x00011BA8
		public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000139AF File Offset: 0x00011BAF
		public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000139B2 File Offset: 0x00011BB2
		public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000139B5 File Offset: 0x00011BB5
		public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000139BC File Offset: 0x00011BBC
		public virtual bool TryGetMember(T instance, GetMemberBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000139C2 File Offset: 0x00011BC2
		public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000139C9 File Offset: 0x00011BC9
		public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000139D0 File Offset: 0x00011BD0
		public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000139D3 File Offset: 0x00011BD3
		public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000139D6 File Offset: 0x00011BD6
		public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, out object result)
		{
			result = null;
			return false;
		}
	}
}

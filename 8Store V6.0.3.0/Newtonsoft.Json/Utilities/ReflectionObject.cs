using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000071 RID: 113
	[NullableContext(1)]
	[Nullable(0)]
	internal class ReflectionObject
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00016FC9 File Offset: 0x000151C9
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> Creator { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00016FD1 File Offset: 0x000151D1
		public IDictionary<string, ReflectionMember> Members { get; }

		// Token: 0x0600058F RID: 1423 RVA: 0x00016FD9 File Offset: 0x000151D9
		private ReflectionObject([Nullable(new byte[]
		{
			2,
			1
		})] ObjectConstructor<object> creator)
		{
			this.Members = new Dictionary<string, ReflectionMember>();
			this.Creator = creator;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00016FF3 File Offset: 0x000151F3
		[return: Nullable(2)]
		public object GetValue(object target, string member)
		{
			return this.Members[member].Getter.Invoke(target);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001700C File Offset: 0x0001520C
		public void SetValue(object target, string member, [Nullable(2)] object value)
		{
			this.Members[member].Setter.Invoke(target, value);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00017026 File Offset: 0x00015226
		public Type GetType(string member)
		{
			return this.Members[member].MemberType;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00017039 File Offset: 0x00015239
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static ReflectionObject Create([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2735)] Type t, params string[] memberNames)
		{
			return ReflectionObject.Create(t, null, memberNames);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00017044 File Offset: 0x00015244
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static ReflectionObject Create([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2735)] Type t, [Nullable(2)] MethodBase creator, params string[] memberNames)
		{
			ReflectionDelegateFactory reflectionDelegateFactory = JsonTypeReflector.ReflectionDelegateFactory;
			ObjectConstructor<object> creator2 = null;
			if (creator != null)
			{
				creator2 = reflectionDelegateFactory.CreateParameterizedConstructor(creator);
			}
			else if (ReflectionUtils.HasDefaultConstructor(t, false))
			{
				Func<object> ctor = reflectionDelegateFactory.CreateDefaultConstructor<object>(t);
				creator2 = (([Nullable(new byte[]
				{
					1,
					2
				})] object[] args) => ctor.Invoke());
			}
			ReflectionObject reflectionObject = new ReflectionObject(creator2);
			int i = 0;
			while (i < memberNames.Length)
			{
				string text = memberNames[i];
				MemberInfo[] member = t.GetMember(text, BindingFlags.Instance | BindingFlags.Public);
				if (member.Length != 1)
				{
					throw new ArgumentException("Expected a single member with the name '{0}'.".FormatWith(CultureInfo.InvariantCulture, text));
				}
				MemberInfo memberInfo = Enumerable.Single<MemberInfo>(member);
				ReflectionMember reflectionMember = new ReflectionMember();
				MemberTypes memberTypes = memberInfo.MemberType();
				if (memberTypes == MemberTypes.Field)
				{
					goto IL_A4;
				}
				if (memberTypes != MemberTypes.Method)
				{
					if (memberTypes == MemberTypes.Property)
					{
						goto IL_A4;
					}
					throw new ArgumentException("Unexpected member type '{0}' for member '{1}'.".FormatWith(CultureInfo.InvariantCulture, memberInfo.MemberType(), memberInfo.Name));
				}
				else
				{
					MethodInfo methodInfo = (MethodInfo)memberInfo;
					if (methodInfo.IsPublic)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 0 && methodInfo.ReturnType != typeof(void))
						{
							MethodCall<object, object> call = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
							reflectionMember.Getter = ((object target) => call(target, new object[0]));
						}
						else if (parameters.Length == 1 && methodInfo.ReturnType == typeof(void))
						{
							MethodCall<object, object> call = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
							reflectionMember.Setter = delegate(object target, [Nullable(2)] object arg)
							{
								call(target, new object[]
								{
									arg
								});
							};
						}
					}
				}
				IL_1AF:
				reflectionMember.MemberType = ReflectionUtils.GetMemberUnderlyingType(memberInfo);
				reflectionObject.Members[text] = reflectionMember;
				i++;
				continue;
				IL_A4:
				if (ReflectionUtils.CanReadMemberValue(memberInfo, false))
				{
					reflectionMember.Getter = reflectionDelegateFactory.CreateGet<object>(memberInfo);
				}
				if (ReflectionUtils.CanSetMemberValue(memberInfo, false, false))
				{
					reflectionMember.Setter = reflectionDelegateFactory.CreateSet<object>(memberInfo);
					goto IL_1AF;
				}
				goto IL_1AF;
			}
			return reflectionObject;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EA RID: 234
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
	[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
	public class DiscriminatedUnionConverter : JsonConverter
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x0002F634 File Offset: 0x0002D834
		private static Type CreateUnionTypeLookup(Type t)
		{
			MethodCall<object, object> getUnionCases = FSharpUtils.Instance.GetUnionCases;
			object target = null;
			object[] array = new object[2];
			array[0] = t;
			object obj = Enumerable.First<object>((object[])getUnionCases(target, array));
			return (Type)FSharpUtils.Instance.GetUnionCaseInfoDeclaringType.Invoke(obj);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002F67C File Offset: 0x0002D87C
		private static DiscriminatedUnionConverter.Union CreateUnion(Type t)
		{
			MethodCall<object, object> preComputeUnionTagReader = FSharpUtils.Instance.PreComputeUnionTagReader;
			object target = null;
			object[] array = new object[2];
			array[0] = t;
			DiscriminatedUnionConverter.Union union = new DiscriminatedUnionConverter.Union((FSharpFunction)preComputeUnionTagReader(target, array), new List<DiscriminatedUnionConverter.UnionCase>());
			MethodCall<object, object> getUnionCases = FSharpUtils.Instance.GetUnionCases;
			object target2 = null;
			object[] array2 = new object[2];
			array2[0] = t;
			foreach (object obj in (object[])getUnionCases(target2, array2))
			{
				int tag = (int)FSharpUtils.Instance.GetUnionCaseInfoTag.Invoke(obj);
				string name = (string)FSharpUtils.Instance.GetUnionCaseInfoName.Invoke(obj);
				PropertyInfo[] fields = (PropertyInfo[])FSharpUtils.Instance.GetUnionCaseInfoFields(obj, new object[0]);
				MethodCall<object, object> preComputeUnionReader = FSharpUtils.Instance.PreComputeUnionReader;
				object target3 = null;
				object[] array4 = new object[2];
				array4[0] = obj;
				FSharpFunction fieldReader = (FSharpFunction)preComputeUnionReader(target3, array4);
				MethodCall<object, object> preComputeUnionConstructor = FSharpUtils.Instance.PreComputeUnionConstructor;
				object target4 = null;
				object[] array5 = new object[2];
				array5[0] = obj;
				DiscriminatedUnionConverter.UnionCase unionCase = new DiscriminatedUnionConverter.UnionCase(tag, name, fields, fieldReader, (FSharpFunction)preComputeUnionConstructor(target4, array5));
				union.Cases.Add(unionCase);
			}
			return union;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002F784 File Offset: 0x0002D984
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			Type key = DiscriminatedUnionConverter.UnionTypeLookupCache.Get(value.GetType());
			DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(key);
			int tag = (int)union.TagReader.Invoke(new object[]
			{
				value
			});
			DiscriminatedUnionConverter.UnionCase unionCase = Enumerable.Single<DiscriminatedUnionConverter.UnionCase>(union.Cases, (DiscriminatedUnionConverter.UnionCase c) => c.Tag == tag);
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Case") : "Case");
			writer.WriteValue(unionCase.Name);
			if (unionCase.Fields != null && unionCase.Fields.Length != 0)
			{
				object[] array = (object[])unionCase.FieldReader.Invoke(new object[]
				{
					value
				});
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Fields") : "Fields");
				writer.WriteStartArray();
				foreach (object value2 in array)
				{
					serializer.Serialize(writer, value2);
				}
				writer.WriteEndArray();
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002F8B4 File Offset: 0x0002DAB4
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			DiscriminatedUnionConverter.UnionCase unionCase = null;
			string caseName = null;
			JArray jarray = null;
			reader.ReadAndAssert();
			Func<DiscriminatedUnionConverter.UnionCase, bool> <>9__0;
			while (reader.TokenType == JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (string.Equals(text, "Case", 5))
				{
					reader.ReadAndAssert();
					DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(objectType);
					caseName = reader.Value.ToString();
					IEnumerable<DiscriminatedUnionConverter.UnionCase> cases = union.Cases;
					Func<DiscriminatedUnionConverter.UnionCase, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = ((DiscriminatedUnionConverter.UnionCase c) => c.Name == caseName));
					}
					unionCase = Enumerable.SingleOrDefault<DiscriminatedUnionConverter.UnionCase>(cases, func);
					if (unionCase == null)
					{
						throw JsonSerializationException.Create(reader, "No union type found with the name '{0}'.".FormatWith(CultureInfo.InvariantCulture, caseName));
					}
				}
				else
				{
					if (!string.Equals(text, "Fields", 5))
					{
						throw JsonSerializationException.Create(reader, "Unexpected property '{0}' found when reading union.".FormatWith(CultureInfo.InvariantCulture, text));
					}
					reader.ReadAndAssert();
					if (reader.TokenType != JsonToken.StartArray)
					{
						throw JsonSerializationException.Create(reader, "Union fields must been an array.");
					}
					jarray = (JArray)JToken.ReadFrom(reader);
				}
				reader.ReadAndAssert();
			}
			if (unionCase == null)
			{
				throw JsonSerializationException.Create(reader, "No '{0}' property with union name found.".FormatWith(CultureInfo.InvariantCulture, "Case"));
			}
			object[] array = new object[unionCase.Fields.Length];
			if (unionCase.Fields.Length != 0 && jarray == null)
			{
				throw JsonSerializationException.Create(reader, "No '{0}' property with union fields found.".FormatWith(CultureInfo.InvariantCulture, "Fields"));
			}
			if (jarray != null)
			{
				if (unionCase.Fields.Length != jarray.Count)
				{
					throw JsonSerializationException.Create(reader, "The number of field values does not match the number of properties defined by union '{0}'.".FormatWith(CultureInfo.InvariantCulture, caseName));
				}
				for (int i = 0; i < jarray.Count; i++)
				{
					JToken jtoken = jarray[i];
					PropertyInfo propertyInfo = unionCase.Fields[i];
					array[i] = jtoken.ToObject(propertyInfo.PropertyType, serializer);
				}
			}
			object[] args = new object[]
			{
				array
			};
			return unionCase.Constructor.Invoke(args);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
		public override bool CanConvert(Type objectType)
		{
			if (typeof(IEnumerable).IsAssignableFrom(objectType))
			{
				return false;
			}
			object[] array = Enumerable.ToArray<Attribute>(CustomAttributeExtensions.GetCustomAttributes(IntrospectionExtensions.GetTypeInfo(objectType), true));
			object[] array2 = array;
			bool flag = false;
			array = array2;
			for (int i = 0; i < array.Length; i++)
			{
				Type type = array[i].GetType();
				if (type.FullName == "Microsoft.FSharp.Core.CompilationMappingAttribute")
				{
					FSharpUtils.EnsureInitialized(type.Assembly());
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
			MethodCall<object, object> isUnion = FSharpUtils.Instance.IsUnion;
			object target = null;
			object[] array3 = new object[2];
			array3[0] = objectType;
			return (bool)isUnion(target, array3);
		}

		// Token: 0x04000423 RID: 1059
		private const string CasePropertyName = "Case";

		// Token: 0x04000424 RID: 1060
		private const string FieldsPropertyName = "Fields";

		// Token: 0x04000425 RID: 1061
		private static readonly ThreadSafeStore<Type, DiscriminatedUnionConverter.Union> UnionCache = new ThreadSafeStore<Type, DiscriminatedUnionConverter.Union>(new Func<Type, DiscriminatedUnionConverter.Union>(DiscriminatedUnionConverter.CreateUnion));

		// Token: 0x04000426 RID: 1062
		private static readonly ThreadSafeStore<Type, Type> UnionTypeLookupCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(DiscriminatedUnionConverter.CreateUnionTypeLookup));

		// Token: 0x020001EC RID: 492
		[Nullable(0)]
		internal class Union
		{
			// Token: 0x06000FE0 RID: 4064 RVA: 0x0004577F File Offset: 0x0004397F
			public Union(FSharpFunction tagReader, List<DiscriminatedUnionConverter.UnionCase> cases)
			{
				this.TagReader = tagReader;
				this.Cases = cases;
			}

			// Token: 0x040008A4 RID: 2212
			public readonly FSharpFunction TagReader;

			// Token: 0x040008A5 RID: 2213
			public readonly List<DiscriminatedUnionConverter.UnionCase> Cases;
		}

		// Token: 0x020001ED RID: 493
		[Nullable(0)]
		internal class UnionCase
		{
			// Token: 0x06000FE1 RID: 4065 RVA: 0x00045795 File Offset: 0x00043995
			public UnionCase(int tag, string name, PropertyInfo[] fields, FSharpFunction fieldReader, FSharpFunction constructor)
			{
				this.Tag = tag;
				this.Name = name;
				this.Fields = fields;
				this.FieldReader = fieldReader;
				this.Constructor = constructor;
			}

			// Token: 0x040008A6 RID: 2214
			public readonly int Tag;

			// Token: 0x040008A7 RID: 2215
			public readonly string Name;

			// Token: 0x040008A8 RID: 2216
			public readonly PropertyInfo[] Fields;

			// Token: 0x040008A9 RID: 2217
			public readonly FSharpFunction FieldReader;

			// Token: 0x040008AA RID: 2218
			public readonly FSharpFunction Constructor;
		}
	}
}

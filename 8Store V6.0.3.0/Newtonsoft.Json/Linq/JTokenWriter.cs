using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000D1 RID: 209
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x0002C335 File Offset: 0x0002A535
		[NullableContext(1)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002C359 File Offset: 0x0002A559
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002C361 File Offset: 0x0002A561
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002C378 File Offset: 0x0002A578
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002C399 File Offset: 0x0002A599
		public JTokenWriter()
		{
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002C3A1 File Offset: 0x0002A5A1
		public override void Flush()
		{
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002C3AB File Offset: 0x0002A5AB
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002C3BE File Offset: 0x0002A5BE
		[NullableContext(1)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002C3EC File Offset: 0x0002A5EC
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002C43D File Offset: 0x0002A63D
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002C450 File Offset: 0x0002A650
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002C465 File Offset: 0x0002A665
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002C46D File Offset: 0x0002A66D
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002C49A File Offset: 0x0002A69A
		private void AddRawValue(object value, JTokenType type, JsonToken token)
		{
			this.AddJValue(new JValue(value, type), token);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		internal void AddJValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent.TryAdd(value))
				{
					this._current = this._parent.Last;
					if (this._parent.Type == JTokenType.Property)
					{
						this._parent = this._parent.Parent;
						return;
					}
				}
			}
			else
			{
				this._value = (value ?? JValue.CreateNull());
				this._current = this._value;
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002C51C File Offset: 0x0002A71C
		public override void WriteValue(object value)
		{
			base.WriteValue(value);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002C525 File Offset: 0x0002A725
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddJValue(JValue.CreateNull(), JsonToken.Null);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002C53A File Offset: 0x0002A73A
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddJValue(JValue.CreateUndefined(), JsonToken.Undefined);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002C54F File Offset: 0x0002A74F
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddJValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002C565 File Offset: 0x0002A765
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddJValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002C57B File Offset: 0x0002A77B
		public override void WriteValue(string value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002C59C File Offset: 0x0002A79C
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002C5B3 File Offset: 0x0002A7B3
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002C5CA File Offset: 0x0002A7CA
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002C5F6 File Offset: 0x0002A7F6
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002C60C File Offset: 0x0002A80C
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002C622 File Offset: 0x0002A822
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Boolean);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002C639 File Offset: 0x0002A839
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002C650 File Offset: 0x0002A850
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002C668 File Offset: 0x0002A868
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString();
			this.AddJValue(new JValue(value2), JsonToken.String);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002C692 File Offset: 0x0002A892
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002C6A9 File Offset: 0x0002A8A9
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002C6C0 File Offset: 0x0002A8C0
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002C6D6 File Offset: 0x0002A8D6
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002C6FB File Offset: 0x0002A8FB
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002C712 File Offset: 0x0002A912
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value, JTokenType.Bytes), JsonToken.Bytes);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002C72B File Offset: 0x0002A92B
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002C742 File Offset: 0x0002A942
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002C759 File Offset: 0x0002A959
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002C770 File Offset: 0x0002A970
		[NullableContext(1)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken(null);
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = (jtoken as JContainer);
					this._value = (jtoken as JValue);
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x040003ED RID: 1005
		private JContainer _token;

		// Token: 0x040003EE RID: 1006
		private JContainer _parent;

		// Token: 0x040003EF RID: 1007
		private JValue _value;

		// Token: 0x040003F0 RID: 1008
		private JToken _current;
	}
}

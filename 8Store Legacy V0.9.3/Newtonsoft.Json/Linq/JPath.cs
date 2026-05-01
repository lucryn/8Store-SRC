using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200005E RID: 94
	internal class JPath
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00013A5E File Offset: 0x00011C5E
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00013A66 File Offset: 0x00011C66
		public List<object> Parts { get; private set; }

		// Token: 0x0600053A RID: 1338 RVA: 0x00013A6F File Offset: 0x00011C6F
		public JPath(string expression)
		{
			ValidationUtils.ArgumentNotNull(expression, "expression");
			this._expression = expression;
			this.Parts = new List<object>();
			this.ParseMain();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013A9C File Offset: 0x00011C9C
		private void ParseMain()
		{
			int num = this._currentIndex;
			bool flag = false;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression.get_Chars(this._currentIndex);
				char c2 = c;
				switch (c2)
				{
				case '(':
					goto IL_56;
				case ')':
					goto IL_94;
				default:
					if (c2 != '.')
					{
						switch (c2)
						{
						case '[':
							goto IL_56;
						case ']':
							goto IL_94;
						}
						if (flag)
						{
							throw new JsonException("Unexpected character following indexer: " + c);
						}
					}
					else
					{
						if (this._currentIndex > num)
						{
							string text = this._expression.Substring(num, this._currentIndex - num);
							this.Parts.Add(text);
						}
						num = this._currentIndex + 1;
						flag = false;
					}
					break;
				}
				IL_FC:
				this._currentIndex++;
				continue;
				IL_56:
				if (this._currentIndex > num)
				{
					string text2 = this._expression.Substring(num, this._currentIndex - num);
					this.Parts.Add(text2);
				}
				this.ParseIndexer(c);
				num = this._currentIndex + 1;
				flag = true;
				goto IL_FC;
				IL_94:
				throw new JsonException("Unexpected character while parsing path: " + c);
			}
			if (this._currentIndex > num)
			{
				string text3 = this._expression.Substring(num, this._currentIndex - num);
				this.Parts.Add(text3);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013BF8 File Offset: 0x00011DF8
		private void ParseIndexer(char indexerOpenChar)
		{
			this._currentIndex++;
			char c = (indexerOpenChar == '[') ? ']' : ')';
			int currentIndex = this._currentIndex;
			int num = 0;
			bool flag = false;
			while (this._currentIndex < this._expression.Length)
			{
				char c2 = this._expression.get_Chars(this._currentIndex);
				if (char.IsDigit(c2))
				{
					num++;
					this._currentIndex++;
				}
				else
				{
					if (c2 == c)
					{
						flag = true;
						break;
					}
					throw new JsonException("Unexpected character while parsing path indexer: " + c2);
				}
			}
			if (!flag)
			{
				throw new JsonException("Path ended with open indexer. Expected " + c);
			}
			if (num == 0)
			{
				throw new JsonException("Empty path indexer.");
			}
			string text = this._expression.Substring(currentIndex, num);
			this.Parts.Add(Convert.ToInt32(text, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00013CE4 File Offset: 0x00011EE4
		internal JToken Evaluate(JToken root, bool errorWhenNoMatch)
		{
			JToken jtoken = root;
			foreach (object obj in this.Parts)
			{
				string text = obj as string;
				if (text != null)
				{
					JObject jobject = jtoken as JObject;
					if (jobject != null)
					{
						jtoken = jobject[text];
						if (jtoken == null && errorWhenNoMatch)
						{
							throw new JsonException("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, text));
						}
					}
					else
					{
						if (errorWhenNoMatch)
						{
							throw new JsonException("Property '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, text, jtoken.GetType().Name));
						}
						return null;
					}
				}
				else
				{
					int num = (int)obj;
					JArray jarray = jtoken as JArray;
					JConstructor jconstructor = jtoken as JConstructor;
					if (jarray != null)
					{
						if (jarray.Count <= num)
						{
							if (errorWhenNoMatch)
							{
								throw new JsonException("Index {0} outside the bounds of JArray.".FormatWith(CultureInfo.InvariantCulture, num));
							}
							return null;
						}
						else
						{
							jtoken = jarray[num];
						}
					}
					else if (jconstructor != null)
					{
						if (jconstructor.Count <= num)
						{
							if (errorWhenNoMatch)
							{
								throw new JsonException("Index {0} outside the bounds of JConstructor.".FormatWith(CultureInfo.InvariantCulture, num));
							}
							return null;
						}
						else
						{
							jtoken = jconstructor[num];
						}
					}
					else
					{
						if (errorWhenNoMatch)
						{
							throw new JsonException("Index {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, num, jtoken.GetType().Name));
						}
						return null;
					}
				}
			}
			return jtoken;
		}

		// Token: 0x040001A2 RID: 418
		private readonly string _expression;

		// Token: 0x040001A3 RID: 419
		private int _currentIndex;
	}
}

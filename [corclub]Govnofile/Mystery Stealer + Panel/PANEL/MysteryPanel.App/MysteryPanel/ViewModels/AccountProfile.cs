using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace MysteryPanel.ViewModels
{
	// Token: 0x02000007 RID: 7
	public class AccountProfile
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000027BC File Offset: 0x000009BC
		public AccountProfile()
		{
			this._regex = new Regex("{(\\w*)}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			this._matchEvaluator = new MatchEvaluator(this.Evaluator);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000027E8 File Offset: 0x000009E8
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000027F0 File Offset: 0x000009F0
		[DataMember(Name = "URL")]
		public string URL { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000027F9 File Offset: 0x000009F9
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002801 File Offset: 0x00000A01
		[DataMember(Name = "HWID")]
		public string HWID { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000280A File Offset: 0x00000A0A
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002812 File Offset: 0x00000A12
		[DataMember(Name = "Login")]
		public string Login { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000281B File Offset: 0x00000A1B
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002823 File Offset: 0x00000A23
		[DataMember(Name = "Password")]
		public string Password { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002834 File Offset: 0x00000A34
		[DataMember(Name = "Browser")]
		public string Browser { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000283D File Offset: 0x00000A3D
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002845 File Offset: 0x00000A45
		[DataMember(Name = "ProfilePath")]
		public string ProfilePath { get; set; }

		// Token: 0x060000A3 RID: 163 RVA: 0x0000284E File Offset: 0x00000A4E
		public string FormatedString(string Format)
		{
			return this._regex.Replace(Format, this._matchEvaluator);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B88 File Offset: 0x00001D88
		private string Evaluator(Match match)
		{
			string empty = string.Empty;
			if (match.Groups.Count > 1)
			{
				string value = match.Groups[1].Value;
				if (string.IsNullOrWhiteSpace(value))
				{
					return empty;
				}
				foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DataMemberAttribute), true);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						object[] array = customAttributes;
						for (int j = 0; j < array.Length; j++)
						{
							if (((DataMemberAttribute)array[j]).Name == value)
							{
								return propertyInfo.GetValue(this) as string;
							}
						}
					}
				}
			}
			return empty;
		}

		// Token: 0x04000009 RID: 9
		private readonly Regex _regex;

		// Token: 0x0400000A RID: 10
		private readonly MatchEvaluator _matchEvaluator;
	}
}

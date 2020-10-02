#region

using System.Text;

#endregion

namespace KoiVM.AST
{
    public class ASTConstant : ASTExpression
    {
        public object Value
        {
            get;
            set;
        }

        public static void EscapeString(StringBuilder sb, string s, bool addQuotes)
        {
            if(s == null)
            {
                sb.Append("null");
                return;
            }

            if(addQuotes)
                sb.Append('"');

            foreach(char c in s)
                if (c >= 0x20)
                {
                    if (c != '\\' && c != '"')
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        sb.Append('\\');
                        sb.Append(c);
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '\a':
                            sb.Append(@"\a");
                            break;
                        case '\b':
                            sb.Append(@"\b");
                            break;
                        case '\f':
                            sb.Append(@"\f");
                            break;
                        case '\n':
                            sb.Append(@"\n");
                            break;
                        case '\r':
                            sb.Append(@"\r");
                            break;
                        case '\t':
                            sb.Append(@"\t");
                            break;
                        case '\v':
                            sb.Append(@"\v");
                            break;
                        default:
                            sb.Append(string.Format(@"\u{0:X4}", (int)c));
                            break;
                    }
                }

            if (addQuotes)
                sb.Append('"');
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            if (this.Value != null)
            {
                if (this.Value is string)
                    EscapeString(ret, (string)this.Value, true);
                else
                    ret.Append(this.Value);
            }
            else ret.Append("<<<NULL>>>");
            return ret.ToString();
        }
    }
}
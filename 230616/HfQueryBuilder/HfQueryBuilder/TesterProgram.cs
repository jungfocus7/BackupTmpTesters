using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;




namespace HfQueryBuilder
{
    public static class TesterProgram
    {
        public static void Main(string[] args)
        {
            HfQueryBuilder tqb = new HfQueryBuilder();
            tqb.AppendLine("SELECT NAME, AGE, GATE");
            tqb.AppendLine("FROM MYDATA");
            tqb.AppendLine("WHERE 1=1");

            tqb.AppendLine("    AND USER = '강현철'");
            tqb.AppendLine("    AND KNAME = '요쏘와인'");

            tqb.AppendLine("    AND BOOS = #{BOOSXXP}");            
            tqb.AddParam("BOOSXXP", "부스");

            tqb.AppendLine("    AND DRAMTICA = #{MEMBERSHIP}");
            tqb.AppendLine("    AND DRAMTICA = #{MEMBERSHIP}");
            tqb.AddParam("MEMBERSHIP", "학교");

            tqb.AppendLine("ORDER BY NUM DESC");

            (string query, string[] paramArr) = tqb.GetResult();
            (string query2, string[] paramArr2) = tqb.GetResult();
            (string query3, string[] paramArr3) = tqb.GetResult();
        }
    }




    public sealed class HfQueryBuilder
    {
        public HfQueryBuilder()
        {
            SqlBuffer = new StringBuilder();
            ParamsBuffer = new Dictionary<string, string>();
            m_params = new List<string>();
        }

        public StringBuilder SqlBuffer { get; private set; }
        public Dictionary<string, string> ParamsBuffer { get; private set; }
        private readonly List<string> m_params;


        public void Append(string txt)
        {
            SqlBuffer.Append(txt);
        }


        public void AppendLine(string txt)
        {
            SqlBuffer.AppendLine(txt);
        }


        public void AddParam(string key, string value)
        {
            if (ParamsBuffer.ContainsKey(key))
                ParamsBuffer[key] = value;
            else
                ParamsBuffer.Add(key, value);
        }


        private const string m_rpm = "?";
        private string MatchEvaluator(Match mt)
        {
            string tx = mt.Value;

            int l = tx.Length - 3;
            string key = tx.Substring(2, l);
            if (ParamsBuffer.ContainsKey(key))
                m_params.Add(ParamsBuffer[key]);

            return m_rpm;
        }


        private const string m_reg = "#{\\w+?}";
        public (string query, string[] paramArr) GetResult()
        {
            string rsql = SqlBuffer.ToString();
            if (string.IsNullOrWhiteSpace(rsql))
                return (null, null);

            m_params.Clear();
            string rs = Regex.Replace(rsql, m_reg, MatchEvaluator);
            return (rs, m_params.ToArray());
        }

    }

}

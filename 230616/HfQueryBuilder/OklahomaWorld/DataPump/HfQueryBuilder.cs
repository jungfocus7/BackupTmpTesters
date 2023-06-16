using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System;



namespace OklahomaWorld.DataPump
{
    public class HfQueryBuilder : IDisposable
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


        public virtual void Dispose()
        {
            if (SqlBuffer == null) return;
            SqlBuffer.Clear();
            SqlBuffer = null;
            ParamsBuffer.Clear();
            ParamsBuffer = null;
            m_params.Clear();
        }


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

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace OklahomaWorld.DataPump
{
    public sealed class HfSqlMaker : HfQueryBuilder
    {
        #region "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 1"
        private static bool CheckData(List<string[]> rlst)
        {
            if ((rlst == null) || (rlst.Count < 1))
                return false;

            bool rb = false;
            int ty = 0;
            foreach (string[] lsa in rlst)
            {
                int l = lsa.Length;
                if (ty == 0)
                {
                    ty = l;
                    continue;
                }

                if (ty == l)
                {
                    rb = true;
                    ty = l;
                }
                else
                {
                    rb = false;
                    break;
                }
            }

            return rb;
        }

        private static List<string[]> LoadData(string allText)
        {
            List<string[]> rlst = null;

            string[] lsa = allText.Split(new string[] { Constants.vbNewLine }, StringSplitOptions.None);
            foreach (string ls in lsa)
            {
                string[] csa = ls.Split(new string[] { Constants.vbTab }, StringSplitOptions.None);
                if (rlst == null)
                    rlst = new List<string[]>();
                rlst.Add(csa);
            }

            if (CheckData(rlst)) return rlst;
            else return null;
        }

        public static HfSqlMaker Create(string allText, string tnm)
        {
            if (string.IsNullOrWhiteSpace(allText))
                return null;

            List<string[]> rlst = LoadData(allText);
            if (rlst == null) return null;

            HfSqlMaker dataFlower = new HfSqlMaker()
            {
                LstData = rlst,
                TableName = tnm
            };
            return dataFlower;
        }


        private HfSqlMaker() { }

        public List<string[]> LstData { get; private set; }

        public string TableName { get; private set; }

        public string[] ColNames
        {
            get
            {
                return LstData[0];
            }
        }

        public IEnumerable<string[]> Rows
        {
            get
            {
                bool bFirst = true;
                foreach (string[] rsa in LstData)
                {
                    if (bFirst)
                    {
                        bFirst = false;
                        continue;
                    }
                    yield return rsa;
                }
            }
        }


        public override void Dispose()
        {
            if (LstData == null) return;
            LstData.Clear();
            LstData = null;
            base.Dispose();
        }

        #endregion



        /// <summary>
        /// 값의 대입을 체크
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string CheckAssign(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"{key} = NULL";
            else
                return $"{key} = '{value}'";
        }


        /// <summary>
        /// 값의 조건을 체크
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string CheckCond(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"{key} IS NULL";
            else
                return $"{key} = '{value}'";
        }


        /// <summary>
        /// 컬럼을 기준으로 루프
        /// </summary>
        private void LoopForColumns(string[] rsa, Action<string, string> cbf)
        {
            for (int l = ColNames.Length, i = 0; i < l; ++i)
            {
                string key = ColNames[i];
                string value = rsa[i];
                cbf(key, value);
            }
        }



        /// <summary>
        /// SELECT  쿼리 만들기
        /// </summary>
        public void MakeSelect()
        {
            /*
SELECT C01, C02, C03
FROM DATATABLE
WHERE 1 = 1
    AND C01 = 'R11'
    AND C02 = 'R12'
    AND C03 = 'R13'
;
            */

            foreach (string[] rsa in Rows)
            {
                AppendLine($"SELECT {string.Join(", ", ColNames)}");
                AppendLine($"FROM {TableName}");
                AppendLine("WHERE 1 = 1");
                for (int l = rsa.Length, i = 0; i < l; ++i)
                {
                    string key = ColNames[i];
                    string value = rsa[i];
                    AppendLine($"    AND {CheckCond(key, value)}");
                }

                AppendLine($";{Constants.vbNewLine}");
            }

        }


        /// <summary>
        /// UPDATE  쿼리 만들기
        /// </summary>
        public void MakeUpdate()
        {
            /*
UPDATE DATATABLE
SET C01 = 'R11'
    , C02 = 'R12'
    , C03 = 'R13'
WHERE 1 = 1
    AND C01 = 'R11'
    AND C02 = 'R12'
    AND C03 = 'R13'
;
            */
            
            foreach (string[] rsa in Rows)
            {
                AppendLine($"UPDATE {TableName}");
                for (int l = rsa.Length, i = 0; i < l; ++i)
                {
                    string key = ColNames[i];
                    string value = rsa[i];
                    if (i == 0)
                        AppendLine($"SET {CheckAssign(key, value)}");
                    else
                        AppendLine($"    , {CheckAssign(key, value)}");
                }

                AppendLine("WHERE 1 = 1");
                for (int l = rsa.Length, i = 0; i < l; ++i)
                {
                    string key = ColNames[i];
                    string value = rsa[i];
                    AppendLine($"    AND {CheckCond(key, value)}");
                }

                AppendLine($";{Constants.vbNewLine}");
            }

        }


        /// <summary>
        /// INSERT  쿼리 만들기
        /// </summary>
        public void MakeInsert()
        {
            /*
INSERT INTO DATATABLE
    (C01, C02, C03)
VALUES
    ('R11', 'R12', 'R13')
            */

            foreach (string[] rsa in Rows)
            {
                AppendLine($"INSERT INTO {TableName}");
                AppendLine($"    ({string.Join(", ", ColNames)})");
                AppendLine("VALUES");
                IEnumerable<string> ies = rsa.Select(tx => $"'{tx}'");
                AppendLine($"    ({string.Join(", ", ies)})");

                AppendLine($";{Constants.vbNewLine}");
            }

        }


        /// <summary>
        /// DELETE  쿼리 만들기
        /// </summary>
        public void MakeDelete()
        {
            /*
DELETE FROM DATATABLE
WHERE 1 = 1
    AND C01 = 'R11'
    AND C02 = 'R12'
    AND C03 = 'R13'
;
            */

            foreach (string[] rsa in Rows)
            {
                AppendLine($"DELETE FROM {TableName}");
                AppendLine("WHERE 1 = 1");
                for (int l = ColNames.Length, i = 0; i < l; ++i)
                {
                    string key = ColNames[i];
                    string value = rsa[i];
                    AppendLine($"    AND {CheckCond(key, value)}");
                }

                AppendLine($";{Constants.vbNewLine}");
            }

        }

    }



}



using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ConsoleApp1
{
    class Program
    {
        //public static IEnumerable<string> GetColumnNames(DataTable dt, Func<DataColumn, bool> cb = null)
        //{
        //    if (dt == null) yield break;

        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        if (cb == null)
        //            yield return dc.ColumnName;
        //        else if (cb(dc))
        //            yield return dc.ColumnName;
        //    }
        //}

        public static List<string> GetColumnNames(DataTable dt, Func<DataColumn, bool> cb = null)
        {
            if (dt == null) return null;

            List<string> rl = null;
            foreach (DataColumn dc in dt.Columns)
            {
                if ((cb == null) || cb(dc))
                {
                    if (rl == null)
                        rl = new List<string>();
                    rl.Add(dc.ColumnName);
                }
            }

            return rl;
        }

        public static List<string> GetValuesFromColumn(DataTable table, string columnName, Func<DataColumn, bool> callback = null, DataRowVersion ver = DataRowVersion.Default)
        {
            if (table == null) return null;

            List<string> rl = null;
            if (table.Columns.Contains(columnName))
            {
                foreach (DataRow row in table.Rows)
                {
                    if ((callback == null) || callback(dc))
                    {
                        if (rl == null)
                            rl = new List<string>();
                        rl.Add(dc.ColumnName);
                    }
                    row[columnName]
                }
            }


            List<string> rl = null;
            foreach (DataColumn dc in table.Columns)
            {
                if (dc.ColumnName == )

                if ((callback == null) || callback(dc))
                {
                    if (rl == null)
                        rl = new List<string>();
                    rl.Add(dc.ColumnName);
                }
            }

            return rl;
        }



        static void Main(string[] args)
        {
            DataTable dt = new DataTable("TEST");
            dt.Columns.AddRange(
                new DataColumn[] { new DataColumn("Num"), new DataColumn("Name"), new DataColumn("Jon") });
            dt.Rows.Add(new string[] { "01", "박종명", "유통업" });
            dt.Rows.Add(new string[] { "02", "임헌진", "건설업" });
            dt.Rows.Add(new string[] { "03", "정희범", "기술업" });
            dt.Rows.Add(new string[] { "04", "이중호", "교육계" });

            DataView dv = dt.DefaultView;

            DataRow dr = dt.Rows[0];

            DataRowView drv = dv[0];


            var t1 = GetColumnNames(dt);
            var t2 = GetColumnNames(dt, tx => tx.ColumnName[1] == 'o');





            int tn = int.MaxValue;
            double td = double.MaxValue;
            float tf = float.MaxValue;
            long tl = long.MaxValue;
            object t9 = "정범";
            object t8 = DateTime.Now;
            object t7 = "1234";
            object t6 = new StringBuilder();
            object t5 = true;

            //float f = ((object)30).;
            //Convert.ToInt32();

            {
                var a1 = ThNumCaster.ToInt(tn);
                var a2 = ThNumCaster.ToInt(td);
                var a3 = ThNumCaster.ToInt(tf);
                var a4 = ThNumCaster.ToInt(tl);
                var a9 = ThNumCaster.ToInt(t9);
                var a8 = ThNumCaster.ToInt(t8);
                var a7 = ThNumCaster.ToInt(t7);
                var a6 = ThNumCaster.ToInt(t6);
                var a5 = ThNumCaster.ToInt(t5);
                Console.WriteLine(">>>>>>>>>");
            }

            {
                var a1 = ThNumCaster.ToDouble(tn);
                var a2 = ThNumCaster.ToDouble(td);
                var a3 = ThNumCaster.ToDouble(tf);
                var a4 = ThNumCaster.ToDouble(tl);
                var a9 = ThNumCaster.ToDouble(t9);
                var a8 = ThNumCaster.ToDouble(t8);
                var a7 = ThNumCaster.ToDouble(t7);
                var a6 = ThNumCaster.ToDouble(t6);
                var a5 = ThNumCaster.ToDouble(t5);
                Console.WriteLine(">>>>>>>>>");
            }

            {
                var a1 = ThNumCaster.ToFloat(tn);
                var a2 = ThNumCaster.ToFloat(td);
                var a3 = ThNumCaster.ToFloat(tf);
                var a4 = ThNumCaster.ToFloat(tl);
                var a9 = ThNumCaster.ToFloat(t9);
                var a8 = ThNumCaster.ToFloat(t8);
                var a7 = ThNumCaster.ToFloat(t7);
                var a6 = ThNumCaster.ToFloat(t6);
                var a5 = ThNumCaster.ToFloat(t5);
                Console.WriteLine(">>>>>>>>>");
            }

            {
                var a1 = ThNumCaster.ToLong(tn);
                var a2 = ThNumCaster.ToLong(td);
                var a3 = ThNumCaster.ToLong(tf);
                var a4 = ThNumCaster.ToLong(tl);
                var a9 = ThNumCaster.ToLong(t9);
                var a8 = ThNumCaster.ToLong(t8);
                var a7 = ThNumCaster.ToLong(t7);
                var a6 = ThNumCaster.ToLong(t6);
                var a5 = ThNumCaster.ToLong(t5);
                Console.WriteLine(">>>>>>>>>");
            }

            {
                var a1 = ThNumCaster.ToBool(tn);
                var a2 = ThNumCaster.ToBool(td);
                var a3 = ThNumCaster.ToBool(tf);
                var a4 = ThNumCaster.ToBool(tl);
                var a9 = ThNumCaster.ToBool(t9);
                var a8 = ThNumCaster.ToBool(t8);
                var a7 = ThNumCaster.ToBool(t7);
                var a6 = ThNumCaster.ToBool(t6);
                var a5 = ThNumCaster.ToBool(t5);
                Console.WriteLine(">>>>>>>>>");
            }
        }

    }

}

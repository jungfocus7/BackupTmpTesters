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
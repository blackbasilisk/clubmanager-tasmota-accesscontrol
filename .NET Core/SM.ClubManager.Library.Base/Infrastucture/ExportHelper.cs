using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public class ExportHelper
    {
        /// <summary>
        /// Convert the datatable to csv text 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DatatableToCSV(DataTable dt)
        {
            long rowsCount = dt.Rows.Count;
            long columnsCount = dt.Columns.Count;
            StringBuilder sb = new StringBuilder();
                    
            for (int r = 0; r < rowsCount; r++)
            {
                for (int c = 0; c < columnsCount; c++)
                {                  
                    sb.Append(dt.Rows[r][c].ToString() + ",");
                }
                //remove the comma at end
                sb.Remove(sb.Length - 1, 1);
                //add newline
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();                    
        }

        public List<string> GetDataTableHeadersList(DataTable dt) 
        {
            if (dt.Rows.Count < 1)
                return null;

            List<string> list = new List<string>();
            list = GetDataTableHeaders(dt).Split(',').ToList();
            return list;
        }

        public string GetDataTableHeaders(DataTable dt) 
        {
            if (dt.Rows.Count < 1)
                return "";
            StringBuilder headers = new StringBuilder();

            for (int cn = 0; cn < dt.Columns.Count; cn++)
            {
                headers.Append(dt.Columns[cn].ColumnName + ",");
            }
            return headers.ToString().TrimEnd(',');
        }
    }
}

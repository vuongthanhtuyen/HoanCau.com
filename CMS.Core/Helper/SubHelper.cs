using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetCMS.Core.Helper
{
   public static class SubHelper
    {
        public static List<string> ExecuteTypedListString(this SqlQuery qry)
        {
            List<string> list = new List<string>();
            foreach (System.Data.DataRow row in qry.ExecuteDataSet().Tables[0].Rows)
            {
                list.Add((String)row[0]);
            }
            return list;
        }
    }
}

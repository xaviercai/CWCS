using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWCS.category
{
    public partial class CategoryBaseInfo : System.Web.UI.Page
    {
        public string strCategoryJsonSource = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTree = string.Empty;
            using (DataBase db = new DataBase())
            {
                DataTable dt = db.ExecuteDataTable("SELECT ID FROM CWCS_CATEGORY WHERE PARENT_ID IS NULL");
                foreach (DataRow dr in dt.Rows)
                {
                    strTree += GetJson(dr[0].ToString())+",";
                }
                strTree = strTree.Substring(0, strTree.Length - 1);
                strCategoryJsonSource = "[" + strTree + "]";
            }
        }
        protected string GetJson(string cid)
        {
            string retJson = "{id:'"+cid+"',text:";

            using (DataBase db = new DataBase())
            {
                DataTable dt = db.ExecuteDataTable("SELECT CATEGORY_NAME FROM CWCS_CATEGORY WHERE ID="+cid);
                if (dt.Rows.Count != 1)
                    return "";
                retJson+="'"+dt.Rows[0][0].ToString()+"',";
                dt = db.ExecuteDataTable("SELECT ID FROM CWCS_CATEGORY WHERE PARENT_ID="+cid);
                if (dt.Rows.Count == 0)
                    retJson += "leaf:true}";
                else
                {

                    retJson += "children:[";
                    foreach (DataRow dr in dt.Rows)
                    {
                        retJson += GetJson(dr[0].ToString())+",";
                    }
                    retJson = retJson.Substring(0, retJson.Length - 1);
                    retJson += "]}";
                }
            }

            return retJson;
        }
    }
}
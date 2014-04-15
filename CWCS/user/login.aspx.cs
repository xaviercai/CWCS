using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWCS.user
{
    public partial class login : System.Web.UI.Page
    {
        public string strLoginDataSource = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                if (Request.Form["USER_NAME"] == null || Request.Form["PASSWORD"] == null)
                {
                    Action_Result(false, "用户名或密码为空");
                    return;
                }
                string username = Request.Form["USER_NAME"];
                string pwd = Request.Form["PASSWORD"];
                CUserInfo userInfo = new CUserInfo(username, pwd);
                if (!userInfo.HasUser)
                {
                    Action_Result(false, "登陆失败");
                    return;
                }
                Session["CUserInfo"] = userInfo;
                Action_Result(true, "");
            }
        }
        protected void Action_Result(bool success, string msg)
        {
            object result= new { success = success, msg = msg };

            strLoginDataSource = result.ObjectToJSON();
            if (Request.QueryString["callback"] != null)
            {
                strLoginDataSource = Request.QueryString["callback"].ToString() + "(" + strLoginDataSource + ");";
            }
            Response.ContentType = "text/javascript";
        }
    }
}
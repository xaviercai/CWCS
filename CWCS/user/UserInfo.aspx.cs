using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWCS.user
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["table"] == null)
            {
                Response.End();
            }
            CUserInfo user = this.GetCurrentUser(false);
            if (user == null)
            {
                Response.End();
            }
            object userInfo;

            switch (Request.QueryString["table"].ToString())
            {
                case "FRAME":
                    userInfo = new { 
                        HasUser=user.HasUser,
                        NickName=user.NickName,
                        ID=user.ID,
                        UserName=user.UserName,
                        Mobile=user.Mobile,
                        Email=user.Email,
                        Comments=user.Comments
                    };
                    Response.Write(userInfo.ObjectToJSON());
                    break;
                case "":
                    break;

            }
            Response.End();
        }
    }
}
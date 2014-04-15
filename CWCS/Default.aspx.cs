using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CWCS
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(this.GetCurrentUser().NickName);
            if (Request.QueryString["id"] != null && Request.QueryString["text"] != null)
            {
                Response.Write(Request.QueryString["id"] + "_" + Request.QueryString["text"]);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data;

public class CSecRole
{
    public int ID;
    public String RoleName;
    public String Permission;
}

public class CUserInfo
{
    public bool HasUser;
    public int ID;
    public String UserName;
    public String NickName;
    public String Mobile;
    public String Email;
    public String Comments;
    public List<CSecRole> SecRoles = new List<CSecRole>();
    private Hashtable PermissionPool = new Hashtable();

    public CUserInfo()
    {
        HasUser = false;
    }
    public CUserInfo(String _username, String _pwd)
    {
        using (DataBase db = new DataBase())
        {
            db.AddParameter("USER_NAME", _username);
            db.AddParameter("PASSWORD", _pwd);
            DataTable dt = db.ExecuteDataTable("SELECT [ID],[USER_NAME],[NICK_NAME],[MOBILE],[EMAIL],[COMMENTS] "+
                                                "FROM [CWCS_USER] WHERE [USER_NAME]=@USER_NAME AND [PASSWORD]=@PASSWORD");
            if (dt.Rows.Count != 1)
            {
                HasUser = false;
                return;
            }
            ID = Convert.ToInt32(dt.Rows[0][0].ToString());
            UserName = dt.Rows[0][1].ToString();
            NickName = dt.Rows[0][2].ToString();
            Mobile = dt.Rows[0][3].ToString();
            Email = dt.Rows[0][4].ToString();
            Comments = dt.Rows[0][5].ToString();
            dt = db.ExecuteDataTable("SELECT A.[ID],A.[ROLE_NAME],A.[PERMISSION] "+
                                    "FROM CWCS_ROLE A,CWCS_ROLE_USER B " +
                                    "WHERE A.[ID]=B.[ROLE_ID] AND B.[USER_ID]="+ID);
            foreach (DataRow dr in dt.Rows)
            {
                CSecRole sRole = new CSecRole();
                sRole.ID = Convert.ToInt32(dr[0].ToString());
                sRole.RoleName = dr[1].ToString();
                sRole.Permission = dr[2].ToString();
                SecRoles.Add(sRole);
                AddPermission(sRole.Permission);
            }
            HasUser = true;

        }
    }

    private void AddPermission(String permission)
    {
        String[] temp = permission.Split(';');
        foreach (String str in temp)
        {
            try
            {
                PermissionPool.Add(str, str);
            }
            catch { }
        }
    }
    public bool hasPermission(String permissionID)
    {
        if (PermissionPool.Contains("all"))
            return true;
        return PermissionPool.Contains(permissionID);
    }

}

public static class UserInfoHelper
{
    public static CUserInfo GetCurrentUser(this System.Web.UI.Page page, bool bAutoEnd)
    {
        if (page.Session["CUserInfo"] == null)
        {
            page.Response.Redirect("/user/login.htm");

            CUserInfo userinfo = new CUserInfo();
            return userinfo;
        }
        else
        {
            return (CUserInfo)page.Session["CUserInfo"];
        }
    }
    public static CUserInfo GetCurrentUser(this System.Web.UI.Page page)
    {
        return GetCurrentUser(page, true);
    }

}
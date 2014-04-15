using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;


/// <summary>
///JSONHelper 的摘要说明
/// </summary>
public static class JSONHelper
{
    public static string ObjectToJSON(this object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }
    //public static string ToJSON(this object obj, int recursionDepth)
    //{
    //    JavaScriptSerializer serializer = new JavaScriptSerializer();
    //    serializer.RecursionLimit = recursionDepth;
    //    return serializer.Serialize(obj);
        
    //}
    public static string ToJSON(this DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("totalCount");
        jsonBuilder.Append("\":\"" + dt.Rows.Count + "\",\"data\":[");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":");

                if (dt.Rows[i][j].GetType().Name == "DateTime")
                {
                    DateTime d1 = new DateTime(1970, 1, 1);
                    DateTime d2 = (DateTime)dt.Rows[i][j];
                    TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
                    //jsonBuilder.Append(String.Format("{0:dd/MM/yy}", dt.Rows[i][j]));
                    jsonBuilder.Append("new Date(" + ts.TotalMilliseconds + ")");
                }
                else if (dt.Rows[i][j].GetType().Name == "Boolean")
                {
                    if (dt.Rows[i][j].ToString() == "True")
                        jsonBuilder.Append("\"Yes\"");
                    else if (dt.Rows[i][j].ToString() == "False")
                        jsonBuilder.Append("\"No\"");
                    else
                        jsonBuilder.Append("\"\"");
                }
                else
                    jsonBuilder.Append("\"" + dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("/", "\\/").Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ") + "\"");
                jsonBuilder.Append(",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        if (dt.Rows.Count != 0)
        {
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        }
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }
    public static string ToJSON(this DataTable dt,string totalcount)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("totalCount");
        jsonBuilder.Append("\":\"" + totalcount + "\",\"data\":[");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":");

                if (dt.Rows[i][j].GetType().Name == "DateTime")
                {
                    DateTime d1 = new DateTime(1970, 1, 1);
                    DateTime d2 = (DateTime)dt.Rows[i][j];
                    TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
                    //jsonBuilder.Append(String.Format("{0:dd/MM/yy}", dt.Rows[i][j]));
                    jsonBuilder.Append("new Date(" + ts.TotalMilliseconds + ")");
                }
                else
                    jsonBuilder.Append("\"" + dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("/", "\\/").Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ") + "\"");
                jsonBuilder.Append(",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        if (dt.Rows.Count != 0)
        {
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        }
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    } 

}

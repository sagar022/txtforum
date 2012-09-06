using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/*************
 * In this page, we are submitting the answer in our database
 * Here, the answer is come from que_desc.aspx.
 * ************/

public partial class que_desc2 : System.Web.UI.Page
{
    string ans,hashcode,query;
    int que_id;
    Blogic ob;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            //get answer from que_desc.aspx...
            ans = Request.QueryString["txtweb-message"];
            ans = ans.Replace("#E", "#e");
            ans = ans.Replace("#e", "<br />");

            que_id = int.Parse(Request.QueryString["que_id"]);
            hashcode = Request.QueryString["hashcode"];

            //get date & time, according to India..
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

            
            //query for insert answer in our database..
            query = string.Format("insert into ans_list (que_id,ans_desc,sub_by,date) values ({0},@answer,'{1}','{2}')", que_id, hashcode, dt);

            ob = new Blogic();
            SqlCommand cmd = ob.getCMD(query);
            cmd.Parameters.AddWithValue("@answer", ans);
            cmd.ExecuteNonQuery();
            Response.Redirect("que_desc.aspx?que_id=" + que_id + "&hashcode=" + hashcode);
            ob.CloseConnection();
        }
        catch
        {
            ob = new Blogic();
            Response.Write(ob.error_msg());
            ob.CloseConnection();
        }

        Response.Write("</body></html>");

    }
}
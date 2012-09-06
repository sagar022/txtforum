using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Data.SqlClient;

/**************
 * Here, we manipulate the data which comes
 * from new_que.aspx, and then insert it into database
 * with DateAndTime of India.
 * 
 * After that we fetch the question description which user asked
 * and redirect to the que_desc.aspx page to display the question
 * to the user.
 * 
 * *************/

public partial class new_que2 : System.Web.UI.Page
{
    string hashcode, full_msg, title, desc, query;
    Blogic ob;
    SqlCommand cmd;
    SqlDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            hashcode = Request.QueryString["hashcode"];

            full_msg = Request.QueryString["txtweb-message"];
            full_msg = full_msg.Replace("#T", "#t");
            full_msg = full_msg.Replace("#D", "#d");
            full_msg = full_msg.Replace("#E", "#e");

            //separate the question title from full_msg....
            title = full_msg.Substring(full_msg.IndexOf("#t") + 2, full_msg.IndexOf("#d") - (full_msg.IndexOf("#t") + 2));   //get title of the que.
            title = title.Replace("#e", " ");

            //separate the question description from full_msg....
            desc = full_msg.Substring(full_msg.IndexOf("#d") + 2);  // get description of the que.
            desc = desc.Replace("#e", "<br />");


            //insert current date & time according to India...
            TimeZoneInfo tji = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tji);


            //insert question in db.....
            query = string.Format("insert into que_list (title,que_desc,sub_by,sub_date) values (@title,@desc,@hashcode,'{0}')", dt);

            ob = new Blogic();
            cmd = ob.getCMD(query);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@hashcode", hashcode);

            cmd.ExecuteNonQuery();
            ob.CloseConnection();


            //print the question description
            query = string.Format("select top 1 que_id from que_list where sub_by='{0}' order by sub_date desc", hashcode);   //query for select latest question
            ob = new Blogic();                                                                                                //asked by user....
            cmd = ob.getCMD(query);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Redirect("./que_desc.aspx?que_id=" + dr.GetValue(0).ToString() + "&hashcode=" + hashcode);
            }

            ob.CloseConnection();


        }

        catch
        {
            //error message, if user doesn't ask question in a proper format...
            Response.Write("Please type in a proper format....<br /><br />"
                          + "Type, #t \"your que title\" #d \"your que description\"<br />"
                          + "Use, #e For line break or enter<br />"
                          + "Note: #t and #d both are very important, you can't submit question without it.<br /><br />");

            Response.Write("For asking a new question....<br /><br />"
                              + "<form action='./new_que2.aspx' method='get' class='txtweb-form'>"
                              + "<input type='hidden' name='hashcode' value='" + hashcode + "' />"
                              + "Title and Description<input type='text' name='txtweb-message' />"
                              + "<input type='submit' value='Submit' />");
        }

        Response.Write("</body></html>");

    }
}
        

       



        


        
        
        

  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/***********
 * In this page, user ask a new question.
 * Here, first we check user is already exist in database or not,
 * if he/she is not exist then redirect him/her to login.aspx.
 * 
 * From here, user content is pass to the new_que2.aspx page
 * 
 * **********/

public partial class new_que : System.Web.UI.Page
{
    string query;
    Blogic ob;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            string hashcode = Request.QueryString["txtweb-mobile"];

            //query for check user is exist or not..
            query = string.Format("select name from user_info where hashcode=@hashcode");

            ob = new Blogic();
            SqlCommand cmd = ob.getCMD(query);
            cmd.Parameters.AddWithValue("@hashcode", hashcode);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())    //if user is exist in database then proceed him/her to ask a question
            {
                Response.Write("Ask a new question....<br /><br />"
                          + "Type, #t \"your que title\" #d \"your que description\"<br />"     //<your que title> print ni ho rha h
                          + "Use, #e For line break or enter<br />"
                          + "Note: #t and #d both are very important, you can't submit question without it.<br /><br />");

                Response.Write("For asking a new question....<br /><br />"
                                  + "<form action='./new_que2.aspx' method='get' class='txtweb-form'>"
                                  + "<input type='hidden' name='hashcode' value='" + hashcode + "' />"
                                  + "Title and Description<input type='text' name='txtweb-message' />"
                                  + "<input type='submit' value='Submit' />");
                
            }
            else    //if user is not exist in database, then redirect him/her to login.aspx
            {
                Response.Redirect("./login.aspx?txtweb-mobile=" + hashcode);

            }
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
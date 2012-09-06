using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/***********
 * Here, first we check that user is already exist i
 * our database or not. If he/she is exist then we ask 
 * him/her to update his name. Otherwise, create his/her account
 * 
 * From here, user information is pass to the login2.aspx
 * **********/

public partial class login : System.Web.UI.Page
{
    string query;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            string hashcode = Request.QueryString["txtweb-mobile"];
            query = string.Format("select name from user_info where hashcode=@hashcode");
            Blogic ob = new Blogic();
            SqlCommand cmd = ob.getCMD(query);
            cmd.Parameters.AddWithValue("@hashcode", hashcode);
            SqlDataReader dr = cmd.ExecuteReader();


            if (!dr.Read())  //first time user
            {
                Response.Write("Create your account.....<br /><br />"
                               +"<form action='./login2.aspx' class='txtweb-form' method='get'>"
                               + "<input type='hidden' name='type' value='firstTime' />"
                               + "<input type='hidden' name='hashcode' value='" + hashcode + "' />"
                               + "Your name<input type='text' name='txtweb-message' />"
                               + "<input type='submit' value='submit' /></form><br /><br />"
                               +"Note: Your mobile hashcode is considered as password, so you don't need any password.");

            }

            else            //if user is already register, so update his/her name....
            {
                Response.Write("Welcome " + dr.GetValue(0).ToString() + ",<br /><br />"
                               + "For change your name<br /><br />"
                               + " <form action='./login2.aspx' class='txtweb-form' method='get'>"
                               + "<input type='hidden' name='hashcode' value='" + hashcode + "' />"
                               + "Your name<input type='text' name='txtweb-message' />"
                               + "<input type='submit' value='submit' /></form> ");

            }
        }

        catch
        {
            
            Blogic ob = new Blogic();
            Response.Write(ob.error_msg());     //print error_message...
            ob.CloseConnection();
        }

        Response.Write("</body></html>");

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


/************
 * Here, data comes from the login.aspx.
 * We use two sql queries (insert and update)
 * If user is already exist then
 * use update query for update his/her name,
 * otherwise use insert query for insert his/her
 * name in database.
 * 
 * **********/
public partial class login2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            string query, hashcode, name;
            hashcode = Request.QueryString["hashcode"];
            name = Request.QueryString["txtweb-message"];

            if (Request.QueryString["type"] == "firstTime")   //for first time user....
            {
                query = string.Format("insert into user_info (hashcode,name) values (@hashcode,@name)");
                Blogic ob = new Blogic();
                SqlCommand cmd = ob.getCMD(query);
                cmd.Parameters.AddWithValue("@hashcode", hashcode);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
                Response.Write("Congratulation, you are successfully logged in...");


            }
            else   // if user is already exist, so update his/her name
            {
                query = string.Format("update user_info set name=@name where hashcode=@hashcode");
                Blogic ob = new Blogic();
                SqlCommand cmd = ob.getCMD(query);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@hashcode", hashcode);
                cmd.ExecuteNonQuery();
                Response.Write("Your name is successfully updated...");

            }

            //provide links for different pages.....
            Response.Write("<br /><br />Enjoy @txtforum....<br />"
                      + "<a href='./login.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.login</a> : For login<br />"
                      + "<a href='./new_que.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.ask</a> : For ask new question<br />"
                      + "<a href='./get_que_list.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.que</a> : Get recent questions list<br />"
                      + "<a href='./get_quelist_me.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.que.me</a> : Get questions asked by you<br />"
                      + "<a href='./quelist_myans.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.ans.me</a> : Get questions list in which you gave the answers<br /><br />");

        }

        catch
        {
            Blogic ob = new Blogic();
            Response.Write(ob.error_msg());  //error message...
            ob.CloseConnection();
        }

        Response.Write("</body></html>");

    }
}
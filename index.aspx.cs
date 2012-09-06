using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/***********
 * This is the home page of the application.
 * Here, we just display the introduction of application
 * and give instructions of how to use it...
 * 
 * *********/
public partial class index : System.Web.UI.Page
{
    string hashcode;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            hashcode = Request.QueryString["txtweb-mobile"];

            Response.Write("Welcome to \"@txtforum\" where you can ask any type of technical/non-technical questions regarding txtweb.<br /><br />"
                          + "<a href='./login.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.login</a> : For login<br /><br />"
                          + "<a href='./new_que.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.ask</a> : For ask new question<br /><br />"
                          + "<a href='./get_que_list.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.que</a> : Get recent questions list<br /><br />"
                          + "<a href='./get_quelist_me.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.my.que</a> : Get questions asked by you<br /><br />"
                          + "<a href='./quelist_myans.aspx?txtweb-mobile=" + hashcode + "'>@txtforum.my.ans</a> : Get questions list in which you gave the answers<br /><br />");
        }
        catch
        {
            //display error message
            Blogic ob = new Blogic();
            Response.Write(ob.error_msg());
            ob.CloseConnection();
        }

        Response.Write("</body></html>");

    }
}
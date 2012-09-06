using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/*************
 * This page is same like a get_que_list.aspx.
 * In this page, we display the recent questions list
 * in which user gave answers.
 * 
 * Here, first we check that user is already exist in database
 * or not, if he/she is not exist so redirect to him/her to 
 * "login.aspx" for login.
 * 
 * We display top 5 recent questions to user,
 * after we give a link "More" for more questions
 * 
 * ***********/

public partial class quelist_myans : System.Web.UI.Page
{
    string query, hashcode;
    int index, next_index;
    Blogic ob;
    SqlCommand cmd;
    SqlDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            
            if (Request.QueryString["next_index"] == null)
            {
                hashcode = Request.QueryString["txtweb-mobile"];
            }
            else
            {
                hashcode = Request.QueryString["hashcode"];
            }

            //check either user is already exist or not, if he/she is not exist so redirect to him/her to login.aspx....
            query = string.Format("select name from user_info where hashcode=@hashcode");

            ob = new Blogic();
            cmd = ob.getCMD(query);
            cmd.Parameters.AddWithValue("@hashcode", hashcode);
            dr = cmd.ExecuteReader();

            if (dr.Read())   //if user is exist, display the que_list to him/her
            {
                if (Request.QueryString["next_index"] == null)
                {
                    hashcode = Request.QueryString["txtweb-mobile"];
                    index = 1;
                }
                else
                {
                    hashcode = Request.QueryString["hashcode"];
                    index = int.Parse(Request.QueryString["next_index"]);
                }

                //query for display recent questions in which user gave the answers.....
                query = string.Format("select * from (select row_number() over(order by ans_list.date desc) as row_num,ans_list.que_id,que_list.title,que_list.vote,que_list.spam from ans_list join que_list on ans_list.que_id=que_list.que_id where ans_list.sub_by='{0}') as list where row_num>={1} and row_num<={2}", hashcode, index, index + 4);

                ob = new Blogic();
                cmd = ob.getCMD(query);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Response.Write("<a href='./que_desc.aspx?que_id=" + dr.GetValue(1).ToString() + "&hashcode=" + hashcode + "' >"
                                  + dr.GetValue(2).ToString() + "</a><br />"                               //que_title
                                  + dr.GetValue(3).ToString() + " votes, "                                 //number of votes
                                  + dr.GetValue(4).ToString() + " spam<br /><br />");                      //number of spam


                }


                next_index = index + 5;

                Response.Write("<a href='./quelist_myans.aspx?next_index=" + next_index + "&hashcode=" + hashcode + "' class='txtweb-menu-for' accesskey='M''>More</a>");
            }
            else
            {
                Response.Redirect("./login.aspx?txtweb-mobile=" + hashcode);
            }
        }
        catch
        {
            ob = new Blogic();
            Response.Write(ob.error_msg());    //error msg...
            ob.CloseConnection();
        }

        Response.Write("</body></html>");
    }
}
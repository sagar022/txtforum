using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/**********
 * This page is divided into four parts...
 * 1st Part: We fetch question description from database,
 *           and then print it.
 * 2nd Part: Provide link for vote/spam.
 * 3rd Part: Fetch answer from database,
 *           and then print it.
 * 4rth Part: Take input for new answer.
 * 
 * 
 * *********/

public partial class que_desc : System.Web.UI.Page
{
    string query,query_for_vs,hashcode;
    int que_id;
    SqlCommand cmd;
    SqlDataReader dr;
    Blogic ob;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
        hashcode = Request.QueryString["hashcode"];
        que_id = int.Parse(Request.QueryString["que_id"]);


        /****  PART-1  ****/

        //query for que_description....
        query = string.Format("select que_list.title,que_list.que_desc,user_info.name from que_list join user_info on que_list.sub_by=user_info.hashcode where que_list.que_id={0}", que_id);

        ob = new Blogic();
        cmd = ob.getCMD(query);
        dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            //print que_description...
            Response.Write("Title: " + dr.GetValue(0).ToString()
                          + "<br /><br />Description: " + dr.GetValue(1).ToString()
                          + "<br />By: " + dr.GetValue(2).ToString()+"<br /><br />");
            
        }

        /***********************/



        /****  PART-2  ****/

        //query for check vote/spam......
        query_for_vs = string.Format("select vote,spam from vote_spam where que_id={0} and hashcode='{1}'", que_id, hashcode);
        Blogic ob_vs = new Blogic();
        SqlCommand command = ob_vs.getCMD(query_for_vs);
        SqlDataReader reader = command.ExecuteReader();

        
        //print link for vote/spam.....
        if (reader.Read())
        {
            if (reader.GetValue(0).ToString() == "False")
            {
                Response.Write("<a href='./vote_spam.aspx?que_id=" + que_id + "&hashcode=" + hashcode + "&query_type=update&type=vote' >Vote up this question<br /></a>");
            }

            if (reader.GetValue(1).ToString() == "False")
            {
                Response.Write("<a href='./vote_spam.aspx?que_id=" + que_id + "&hashcode=" + hashcode + "&query_type=update&type=spam'>Spam this question</a>");

            }
        }
        else
        {
            Response.Write("<a href='./vote_spam.aspx?que_id="+que_id+"&hashcode="+hashcode+"&query_type=insert&type=vote'>Vote up this question<br /></a>");
            Response.Write("<a href='./vote_spam.aspx?que_id=" + que_id + "&hashcode=" + hashcode + "&query_type=insert&type=spam'>Spam this question</a>");
        }

        Response.Write("<br />*****");
        
        ob_vs.CloseConnection();
        ob.CloseConnection();

        /***********************/



        /****  PART-3  ****/

        //query for print answers.......
        query = string.Format("select ans_list.ans_desc,user_info.name from ans_list join user_info on ans_list.sub_by=user_info.hashcode where que_id={0} order by ans_list.date", que_id);
        cmd = ob.getCMD(query);
        dr = cmd.ExecuteReader();


        Response.Write("<br /><br />Answers....<br /><br />");
        
        while (dr.Read())
        {
            //print answers
            Response.Write(dr.GetValue(0).ToString()
                          +"<br />By: "+dr.GetValue(1).ToString()+"<br /><br />");
        }
        ob.CloseConnection();

        /***********************/



        /****  PART-4  ****/ 

        //Take input for answer....
        Response.Write("<br /><br />Give your answer....<br />(Use #e for line break or enter)<br />"
                       + "<form action='./que_desc2.aspx' class='txtweb-form' method='get'>"
                       + "Your answer<input type='text' name='txtweb-message' />"
                       + "<input type='hidden' name='hashcode' value='" + hashcode + "' />"
                       + "<input type='hidden' name='que_id' value='" + que_id + "' />"
                       + "<input type='submit' value='submit' /></form>");
        }

        /***********************/

        catch
        {
            ob = new Blogic();
            Response.Write(ob.error_msg());  //error msg....
            ob.CloseConnection();
        }

        Response.Write("</body></html>");

    }
}
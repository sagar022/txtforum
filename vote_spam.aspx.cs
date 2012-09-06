using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/************
 * In this page, we manipulate with vote/spam.
 * 
 * Here, insert or update query is execute according to 
 * the condition.
 * 
 * ***********/

public partial class vote_spam : System.Web.UI.Page
{
    int que_id;
    string hashcode, type, query_type,query;
    Blogic ob;
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<html><head><meta name=\"txtweb-appkey\" content=\"9cf923ac-53bd-4388-85fa-f75bd432ba68\" /></head><body>");

        try
        {
            que_id = int.Parse(Request.QueryString["que_id"]);
            hashcode = Request.QueryString["hashcode"];
            type = Request.QueryString["type"];                          // vote/spam
            query_type = Request.QueryString["query_type"];              // insert/update

            switch (query_type)
            {
                //execute query according to the condition....
                case "insert":
                    query = string.Format("insert into vote_spam (que_id,hashcode,{0}) values ({1},'{2}','{3}')", type, que_id, hashcode, "True");
                    break;
                case "update":
                    query = string.Format("update vote_spam set {0}='True' where que_id={1} and hashcode='{2}'", type, que_id, hashcode);
                    break;
            }

            ob = new Blogic();
            cmd = ob.getCMD(query);
            cmd.ExecuteNonQuery();


            //increase vote/spam by 1, in que_list table in db....
            query = string.Format("update que_list set {0}={0}+1 where que_id={1}", type, que_id);
            cmd = ob.getCMD(query);
            cmd.ExecuteNonQuery();

            if (type == "vote")
            {
                Response.Write("Thank you, your vote is successfully submitted.... :)");
            }
            else
            {
                Response.Write("Thank you, your spam is considered....");
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
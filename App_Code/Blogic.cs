using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/*<summary>
  Blogic.cs, is a class file where a class Blogic is define.
 * In this class a constructor is used, where connection string is defined
 * and four functions are also defined
 * OpenConnection(), For open a connection
 * CloseConnection(), For close a connection
 * getCMD(), For return SqlCommand associate with connection string.
 * error_msg(), It returns error message.
 * 
</summary>*/

public class Blogic
{
    #region Data Member of the class
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataReader dr;
    #endregion

	public Blogic()
	{
        //connection string
        conn = new SqlConnection("Type your connection string here....");
		
	}

    #region To Open a Sql Connection
    public void OpenConnection()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
    }
    #endregion

    #region To close a Sql Connection
    public void CloseConnection()
    {
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
    #endregion

    
    #region It returns a SqlCommand
    public SqlCommand getCMD(string query)
    {
        OpenConnection();
        cmd = new SqlCommand(query, conn);
        return cmd;

    }
    #endregion

    #region Error Message
    public string error_msg()
    {
        return "Sorry, there is some problem occur. Please try after sometime.<br /><br />"
                           + "Regarding error, please inform the developer.<br />"
                           + "Sagar Kadam<br />Email id: sagar.kadam825@gmail.com";
    }
    #endregion




}
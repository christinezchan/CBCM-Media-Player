using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Xml;


namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("config.xml"));
            XmlNodeList folderList = xmlDoc.GetElementsByTagName("folder");
            XmlNodeList statusList = xmlDoc.GetElementsByTagName("status");

            String ID = Request.QueryString["ID"];
            String req = Request.QueryString["request"];          
            String name = Request.QueryString["name"];
            String sessionKey = Request.QueryString["sessionKey"];
            String rights = Request.QueryString["rights"];


            String filePath = folderList[0].InnerXml;
            String statusPath = statusList[0].InnerXml;
            String fileName = null;
            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            String timeFormated = String.Format("{0:yyyyMMdd-HHmmss}", time);
            String fileContent = req + "\r\n" + name + "\r\n" + timeFormated + "\r\n";


            using (SqlConnection connection = new SqlConnection("user id=doorapp;" +
                                "password=da2014;" +
                                "server=localhost;" +
                                "database=DOOR; " +
                                "connection timeout=30"))
            {                 
                connection.Open();

                if (ID == "00")
                {

                    try
                    {
                        SqlDataReader reader = null;

                        SqlParameter sessionParam = new SqlParameter("@session", SqlDbType.VarChar, 32);
                        sessionParam.Value = sessionKey;
                        SqlParameter requestParam = new SqlParameter("@request", SqlDbType.VarChar, 50);
                        requestParam.Value = req;

                        SqlCommand check = new SqlCommand("INSERT INTO CBCMGDOOR_LOG (SESSION_KEY, REQUEST) VALUES (@session, @request)" +
                                                          " SELECT SESSION_KEY FROM CBCMGDOOR_USERS", connection);

                        check.Parameters.Add(sessionParam);
                        check.Parameters.Add(requestParam);

                        reader = check.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["SESSION_KEY"].ToString() == sessionKey)
                            {                            
                                Response.Write(reader["SESSION_KEY"].ToString());
                                fileName = timeFormated + " " + name;
                                              
                                
                                using (StreamWriter file = new StreamWriter(filePath + fileName + @".txt"))
                                {
                                    file.Write(fileContent);
                                }
                                
                                check.Parameters.Add(sessionParam);
                                check.Parameters.Add(requestParam);

                                check.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }                
                }
                if (ID == "01")
                {
                    try
                    {
                        SqlDataReader reader = null;
                        SqlParameter param = new SqlParameter("@nameParam", SqlDbType.VarChar, 30);
                        param.Value = name;
                        SqlCommand check = new SqlCommand("SELECT * FROM CBCMGDOOR_USERRIGHTS WHERE USERNAME = @nameParam", connection);
                        check.Parameters.Add(param);
                        reader = check.ExecuteReader();
                        while (reader.Read())
                        {
                            Response.Write(reader["RIGHTS"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else if (ID == "02")
                {
                    try
                    {
                        SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 30);
                        nameParam.Value = name;
                            
                        SqlParameter rightsParam = new SqlParameter("@rights", SqlDbType.VarChar, 5);
                        rightsParam.Value = rights;

                        SqlParameter sessionParam = new SqlParameter("@sessionKey", SqlDbType.VarChar, 32);
                        sessionParam.Value = sessionKey;

                        SqlCommand insert = new SqlCommand("IF EXISTS (SELECT * FROM CBCMGDOOR_USERS WHERE USERNAME = @name)" + 
                                                            " UPDATE CBCMGDOOR_USERS SET SESSION_KEY = @sessionKey, RIGHTS = @rights WHERE USERNAME = @name" + 
                                                            " ELSE" + 
                                                            " INSERT INTO CBCMGDOOR_USERS (SESSION_KEY, USERNAME, RIGHTS) VALUES (@sessionKey, @name, @rights)", connection);

                        insert.Parameters.Add(nameParam);
                        insert.Parameters.Add(rightsParam);
                        insert.Parameters.Add(sessionParam);

                        insert.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else if (ID == "03")
                {
                    String status = System.IO.File.ReadAllText(statusPath);
                    Response.Write(status);
                }

                try
                {
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }                   
            }        
        }       
    }
}
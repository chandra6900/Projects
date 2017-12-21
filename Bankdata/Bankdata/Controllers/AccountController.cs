using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace Bankdata.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        static string connstrng = ConfigurationManager.ConnectionStrings["bankcon"].ConnectionString;
        public ActionResult LogIn()
        {
            ReadFromDb();
            return View();
        }

       
        public ActionResult LogOut()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string BankName,string cityName,string Address)
        {
            WriteToDb(BankName, cityName, Address);
        }

        private static void WriteToDb(string BankName, string cityName, string Address)
        {
            //string connstrng = ConfigurationManager.ConnectionStrings["bankcon"].ConnectionString;
            BankName = "'" + BankName + "'";
            cityName = "'" + cityName + "'";
            Address = "'" + Address + "'";
            using (SqlConnection con = new SqlConnection(connstrng))
            {
                SqlCommand sqlcmd = new SqlCommand("insert into tmphello (BankName,City,Address) values (" + BankName + "," + cityName + "," + Address + ");", con);
                con.Open();
                try
                {
                    sqlcmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Source);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private void ReadFromDb()
        {
            using (SqlConnection con = new SqlConnection(connstrng))
            {
                SqlCommand sqlcmd = new SqlCommand("select * from tmphello;", con);
                con.Open();
                try
                {
                    SqlDataReader rdr = sqlcmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        List<string[]> a = new List<string[]>();
                        while (rdr.Read())
                        {
                            string[] rcd = new string[3];
                            rcd[0] = rdr.GetString(1);
                            rcd[1] = rdr.GetString(2);
                            rcd[2] = rdr.GetString(3);
                            a.Add(rcd);
                        }
                        ViewData.Add("BankData", a);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Source);
                }
                finally
                {
                    con.Close();
                }
            }
        }

    }
}

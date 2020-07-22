using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace edwreportsmvc.Controllers
{
    public class HomeController : Controller
    {
       
        private SqlConnection GetConnection()
        {
            string connectionString = "Data Source=TSTONE_HP-LP;Initial Catalog=cardproplus;Integrated Security=true;";

            return new SqlConnection(connectionString);;
        }


        private DataSet GetData(String queryString)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection cnx = GetConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, cnx);

                adapter.Fill(ds, "resultsTable");
            }
            catch (Exception)
            {

            }

            return ds;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns A List Of Constituencies To Web Page
        /// </summary>
        /// <returns></returns>
        public JsonResult Constituencies()
        {
            string qry = @"SELECT [dept_id]
                          ,[dept_nm]
                      FROM [cardproplus].[dbo].[DEPARTMENT]";

            DataTable table = GetData(qry).Tables[0];

            var dict = table.AsEnumerable().ToDictionary<DataRow,string,string>(row=> row.Field<int>(0).ToString(),row=> row.Field<string>(1));
            
            return Json(dict,JsonRequestBehavior.AllowGet);
        }
    }
}
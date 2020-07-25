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
       
        public static SqlConnection GetConnection()
        {
            

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
            catch (Exception ex)
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
        public JsonResult Parishes()
        {
            string qry = @"[PARISH_ID]
                    ,[PARISH_NM]
                  FROM [electionwatch].[dbo].[BALLOTS_PARISH] order by [PARISH_NM]";

            DataTable table = GetData(qry).Tables[0];

            var dict = table.AsEnumerable().ToDictionary<DataRow, string, string>(row => row.Field<int>(0).ToString(), row => row.Field<string>(1));

            return Json(dict, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns A List Of Constituencies To Web Page
        /// </summary>
        /// <returns></returns>
        public JsonResult Constituencies()
        {
            string qry = @"SELECT [CNSTNCY_NBR]
                      ,[CNSTNCY_NM]
                  FROM [electionwatch].[dbo].[CNSTNCY] order by [CNSTNCY_NM]";

            DataTable table = GetData(qry).Tables[0];

            var dict = table.AsEnumerable().ToDictionary<DataRow,string,string>(row=> row.Field<int>(0).ToString(),row=> row.Field<string>(1));

            JsonResult jr = Json(dict, JsonRequestBehavior.AllowGet);
            return jr;
        }

        public string ApplicationTypes()
        { 
            string qry = @"select distinct APP_TYPE from [electionwatch].[dbo].[WORKERS]";
            return    ConvertDataTabletoString(GetData(qry).Tables[0]);
        }

        public string WorkerTypes()
        {
            string sqlQuery = "sp_EDW_Worker_Types";
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, GetConnection());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            return ConvertDataTabletoString(dt);
        }

        public string ConvertDataTabletoString(DataTable dt)
        {                   
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName.ToString(), dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
              
        }

    }
}

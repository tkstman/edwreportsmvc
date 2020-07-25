using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using edwreportsmvc.Controllers;

namespace edwreportsmvc.Reports
{
    public partial class PageReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RenderPerformanceReport();
            }
        }

        private void RenderPerformanceReport()
        {
            ReportViewerDisplay.Reset();

            SqlConnection con = HomeController.GetConnection();
            con.Open();

            string sqlQuery = "sp_EDW_User_Reports";

            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.AddWithValue("@StartDate", defaultStartDateTime);
            //da.SelectCommand.Parameters.AddWithValue("@EndDate", defaultEndDateTime);            
            da.Fill(dt);

            ReportViewerDisplay.LocalReport.DataSources.Clear();

            Microsoft.Reporting.WebForms.ReportDataSource rpdc = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt);

            ReportViewerDisplay.LocalReport.DataSources.Add(rpdc);

            ReportViewerDisplay.LocalReport.EnableExternalImages = true;
            ReportViewerDisplay.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportEdwWorkersProductivity.rdlc");
            ReportViewerDisplay.LocalReport.Refresh();

            throw new NotImplementedException();
        }
    }
}
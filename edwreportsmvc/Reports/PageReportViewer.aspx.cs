using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using edwreportsmvc.Controllers;
using Microsoft.Reporting.WebForms;

namespace edwreportsmvc.Reports
{
    public partial class PageReportViewer : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RenderReport();
            }
        }

        private void RenderReport()
        {
            try
            {
                System.Collections.Specialized.NameValueCollection variables = HttpUtility.ParseQueryString(Request.QueryString.ToString());

                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(
                   variables.AllKeys.ToDictionary(k => k, k => variables[k])
                );

                Dictionary<string, string> dictus = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);
                Dictionary<string, string> dictRes = new Dictionary<string, string>();

                foreach (var i in dictus)
                {
                    List<string> dictusVals = new List<string>();
                    string dictusString = "";
                    string[] temp;
                    temp = i.Value.Split(',');
                    Array.ForEach(temp, element => dictusVals.Add(element.Trim('\\', '"', '[', ']')) );
                    for (int x= 0; x < dictusVals.Count; x++)
                    {
                        if (!dictusVals[x].ToLower().Contains("all") )
                        {
                            if (x != dictusVals.Count - 1)
                            {
                                dictusString += dictusVals[x] + ",";
                            }
                            else
                            {
                                dictusString += dictusVals[x];
                            }                                                       
                        }
                        
                    }
                    dictRes.Add(i.Key, dictusString);
                }


                if (!String.IsNullOrEmpty(Request.QueryString["st"]) && !String.IsNullOrEmpty(Request.QueryString["end"]))
                {
                    RenderPerformanceReport(DateTime.ParseExact(Request.QueryString["st"], "yyyy-MM-dd", null), DateTime.ParseExact(Request.QueryString["end"], "yyyy-MM-dd", null)); //CAPTURE USER FROM QUERY STRING
                }
                else if (dictRes.Count() > 0)
                {
                    RenderEdWEntryReport(dictRes);
                }
                else
                {
                    Response.Redirect("http://kernel/");
                }
                
            }
            catch (Exception)
            {

            }
        }


        private void RenderEdWEntryReport(Dictionary<string, string> dictRes)
        {
            try
            {
                
                ReportViewerDisplay.Reset();
                

                string sqlQuery = "sp_EDW_Reports_TEST";
                SqlConnection con = HomeController.GetConnection();
                con.Open();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                //ADD PARAMETERS FOR LISTING REPORT
                DateTime defaultStartDateTime = dictRes.ContainsKey("dates") ? DateTime.ParseExact(dictRes["dates"].Split(',')[0], "yyyy-MM-dd", null) : DateTime.Now.AddYears(-1);
                DateTime defaultEndDateTime = dictRes.ContainsKey("dates") ? DateTime.ParseExact(dictRes["dates"].Split(',')[1], "yyyy-MM-dd", null) : DateTime.Now ;
                da.SelectCommand.Parameters.AddWithValue("@cnstncy_nbr", dictRes["const"]);
                da.SelectCommand.Parameters.AddWithValue("@app_type", dictRes["appType"]);
                da.SelectCommand.Parameters.AddWithValue("@worker_type", dictRes["workerType"]);
                da.SelectCommand.Parameters.AddWithValue("@status", dictRes["status"]);
                da.SelectCommand.Parameters.AddWithValue("@StartDate", defaultStartDateTime);
                da.SelectCommand.Parameters.AddWithValue("@EndDate", defaultEndDateTime);
                da.Fill(dt);

                ReportViewerDisplay.LocalReport.DataSources.Clear();


                Microsoft.Reporting.WebForms.ReportDataSource rpdc = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt);

                ReportViewerDisplay.LocalReport.DataSources.Add(rpdc);


                ReportViewerDisplay.LocalReport.EnableExternalImages = true;
                ReportViewerDisplay.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportEdwListing.rdlc");

                //ReportViewerDisplay.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Dates", new string[] { defaultStartDateTime.ToString(), defaultEndDateTime.ToString() }, true));

                ReportParameter stDate = new ReportParameter("stDate", defaultStartDateTime.ToString());
                ReportParameter eDate = new ReportParameter("eDate", defaultEndDateTime.ToString());
                ReportViewerDisplay.LocalReport.SetParameters(new ReportParameter[] { stDate, eDate });
                ReportViewerDisplay.LocalReport.Refresh();

            }
            catch (Exception)
            {

            }
        }

        private void RenderPerformanceReport(DateTime startdt, DateTime endingdt)
        {
            try
            {
                ReportViewerDisplay.Reset();

                SqlConnection con = HomeController.GetConnection();
                con.Open();

                string sqlQuery = "sp_EDW_User_Reports";

                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@StartDate", startdt);
                da.SelectCommand.Parameters.AddWithValue("@EndDate", endingdt);
                da.Fill(dt);

                ReportViewerDisplay.LocalReport.DataSources.Clear();

                Microsoft.Reporting.WebForms.ReportDataSource rpdc = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEDWReporting", dt);

                ReportViewerDisplay.LocalReport.DataSources.Add(rpdc);

                ReportViewerDisplay.LocalReport.EnableExternalImages = true;
                ReportViewerDisplay.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportEdwWorkersProductivity.rdlc");
                ReportViewerDisplay.LocalReport.Refresh();

            }
            catch (Exception )
            {

            }
        }

    }
}
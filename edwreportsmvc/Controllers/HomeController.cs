using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using edwreportsmvc.AppUser;


    namespace edwreportsmvc.Controllers
{
    public class HomeController : Controller
    {
        CurrentUser _currentUser = null;

        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=TSTONE_HP-LP;Initial Catalog=cardproplus;Integrated Security=true;";


            return new SqlConnection(connectionString);;
        }

        public static SqlConnection GetConnectionHelper()
        {
            string connectionString = "Data Source=TSTONE_HP-LP;Initial Catalog=cardproplus;Integrated Security=true;";


            return new SqlConnection(connectionString); ;
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

        private DataSet GetDataHelper(String queryString)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection cnx = GetConnectionHelper();
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
            passedUserDetails();
            return View();
        }

        public ActionResult LogOff()
        {
            try
            {                
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Clear();
                return Redirect("http://kernel");
            }
            catch (Exception)
            {
                return Redirect("http://kernel");
            }



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
            string qry = @"select distinct APP_TYPE from [electionwatch].[dbo].[WORKERS_MAIN] order by APP_TYPE";
            return    ConvertDataTabletoString(GetData(qry).Tables[0]);
        }

        public string Statuses()
        {
            string qry = @"select distinct STATUS_CD from [electionwatch].[dbo].[WORKERS_MAIN] order by STATUS_CD";
            return ConvertDataTabletoString(GetData(qry).Tables[0]);
        }

        public string WorkerTypes()
        {
            string sqlQuery = "sp_EDW_Worker_Types";
            System.Data.DataTable dt = new DataTable();
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


        #region USER SESSION TRANSFER AND MANAGEMENT SECTION

        /*
            *  PROCESS USER CREDENTIALS PASSED AS QUERYSTRING ON PAGE LOAD
            */
        private void passedUserDetails()
        {
            string queryStr = Request.QueryString["u"]; //CAPTURE USER FROM QUERY STRING
            string queryStrIp = Request.QueryString["i"];

            //SINCE USER NAME HAS BEEN RETRIEVED CHECK IF USER IS IN THE LOGGED IN TABLE WITH THE CURRENT IP ADDRESS
            if (!string.IsNullOrEmpty(queryStr) && !string.IsNullOrEmpty(queryStrIp))
            {
                Dictionary<int, object> resultsDict = iSLoggedIn(queryStr.ToUpper().ToString(), queryStrIp);
                if ((bool)resultsDict[0] == true)
                {
                    //IF USER HAS THE SAME IP ADDRESS THEN CREATE A SESSION VARIABLE WITH CURRENT USER'S DETAILS
                    string ipaddress = GetIPAddress();
                    if (ipaddress == ((DataRow)resultsDict[1])[1].ToString())
                    {
                        //GET THE USER'S PASSWORD AND CREATE THE USER OBJECT THEN CLEAR THE USER DETAILS FROM THE LOGGED IN TABLE.
                        try
                        {
                            string queryString = "Select psswrd from UNI_USER where usr_nm='" + queryStr.ToUpper().ToString() + "'";
                            DataSet ds = GetDataHelper(queryString);
                            generateUser(queryStr.ToUpper().ToString(), ds.Tables["resultsTable"].Rows[0][0].ToString());
                            //DELETE VALUE FROM THE DATABASE
                            /*if (!removeLoggedIn(queryStr.ToUpper().ToString(), ipaddress))
                            {
                                Response.Redirect("Default.aspx");
                            }
                            */


                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        Response.Redirect("http://kernel/?msg=" + "IP ADDRESSES ARE NOT EQUAL " + ipaddress + " | " + ((DataRow)resultsDict[1])[1].ToString());
                    }
                }
                else { Response.Redirect("http://kernel/?msg=" + "USER IS NOT FOUND IN DB"); }

            }
            else { Response.Redirect("http://kernel/?msg=" + "INVALID USER"); }
        }

        /*
            *  CHECK IF USER IS IN THE LOGGED IN TABLE
            */
        private Dictionary<int, object> iSLoggedIn(string username, string ipaddress)
        {
            Dictionary<int, object> resultsDict = new Dictionary<int, object>();
            resultsDict.Add(0, false);

            string queryString = "select * from UNI_LOGGED_IN where usr_nm='" + username.ToUpper().ToString() + "' and ip_address='" + replaceDotsInIp(ipaddress) + "'";
            try
            {
                DataSet ds = GetDataHelper(queryString);
                if (ds.Tables["resultsTable"].Rows.Count > 0)
                {
                    resultsDict[0] = true;
                    resultsDict.Add(1, ds.Tables["resultsTable"].Rows[0]);
                }

            }
            catch (Exception)
            { }

            return resultsDict;

        }

        /*
            * REPLACE DOTS OR UNDERSCORE IN IPADDRESS
        */
        private string replaceDotsInIp(string ip_address)
        {
            if (ip_address.Contains('.'))
            {
                ip_address = ip_address.Replace('.', '_');
            }
            else if (ip_address.Contains('_'))
            {
                ip_address = ip_address.Replace('_', '.');
            }
            return ip_address;
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            return ipAddress;
        }

        private string GetLocalIPv4()
        {
            System.Net.IPHostEntry host;
            string localIP = "";
            host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                localIP = ip.ToString();

                string[] temp = localIP.Split('.');

                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && temp[0] == "192")
                {
                    break;
                }
                else
                {
                    localIP = null;
                }
            }

            return localIP;
        }


        private bool removeLoggedIn(string username, string ipaddress)
        {
            bool state = false;
            try
            {
                SqlConnection con = HomeController.GetConnectionHelper();
                con.Open();
                string queryString = "Delete from UNI_LOGGED_IN where usr_nm='" + username + "' or ip_address='" + ipaddress + "'";
                SqlCommand cmd = new SqlCommand(queryString, con);
                int retVal = cmd.ExecuteNonQuery();
                con.Close();
                if (retVal != 0)
                    state = true;

            }
            catch
            {
                SqlConnection con = HomeController.GetConnectionHelper();
                con.Open();
                string queryString = "Delete from UNI_LOGGED_IN where usr_nm='" + username + "' or ip_address='" + ipaddress + "'";
                SqlCommand cmd = new SqlCommand(queryString, con);
                int retVal = cmd.ExecuteNonQuery();
                con.Close();
                if (retVal != 0)
                    state = true;
            }

            return state;
        }

        /*
            * POPULATE THE USER OBJECT WITH THE ATTRIBUTES OF THE USER 
            */
        private void generateUser(string username, string password)
        {
            DataClassesDataContext dc = new DataClassesDataContext();

            //
            // Get the app id and the app name
            //
            UNI_APP _app = new UNI_APP();
            string _appName = "";

            //
            // Get the app name from the web config and use it to get the app id from the database
            // If an error occurred retrieving it from the database then set it to the default name
            //
            try
            { _appName = System.Configuration.ConfigurationManager.AppSettings["DefaultAppName"].ToString(); }
            catch
            { _app.app_nm = "EDW REPORTS"; }

            // Get the app id from the database. 
            // If an error occurred then try get the app id from the web config file. If an error occured again then assign a default id of 1
            try
            { _app = (from _ap in dc.UNI_APPs where _ap.app_nm.ToUpper() == _appName select _ap).Single(); }
            catch
            {
                try
                { _app.app_id = int.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultAppId"].ToString()); }
                catch
                { _app.app_id = 4; }
            }

            try
            {

                string pwd = password;
                System.Collections.Generic.List<sp_electionwatch_helper_loginResult> _users = (dc.sp_electionwatch_helper_login(username.ToUpper(), pwd)).ToList();

                if (_users[0].usr_nm != null)
                {
                    CurrentUser _currentUser = new CurrentUser();

                    sp_electionwatch_helper_loginResult _user = (from u in _users where u.app_id == _app.app_id select u).Single();

                    if (_user.app_id == _app.app_id)
                    {
                        _currentUser.UserId = _user.usr_id;
                        _currentUser.Username = _user.usr_nm;
                        _currentUser.FirstName = _user.frst_nm.ToUpper();
                        _currentUser.LastName = _user.last_nm.ToUpper();
                        _currentUser.WorkLocationId = _user.wloc_id;
                        _currentUser.WorkLocationName = _user.wloc_nm;
                        _currentUser.ActiveElection = _user.elctn_nm.ToUpper();

                        try
                        { _currentUser.LastLoginDate = (DateTime)_user.last_login_dt; }
                        catch { }

                        // Get the user's constituencies
                        try
                        {
                            System.Collections.Generic.List<SP_GET_LOCATIONSResult> locations = (dc.SP_GET_LOCATIONS(_user.usr_nm, _user.app_id)).ToList();

                            foreach (SP_GET_LOCATIONSResult location in locations)
                            {
                                try
                                {
                                    if (_currentUser.WorkLocations.ContainsKey(location.wloc_id))
                                        continue;
                                    else
                                        _currentUser.WorkLocations.Add((int)location.wloc_id, location.wloc_nm);
                                }
                                catch
                                { }

                                try
                                {
                                    if (_currentUser.Constituencies.ContainsKey(location.cnstncy_nbr))
                                        continue;
                                    else
                                        _currentUser.Constituencies.Add((int)location.cnstncy_nbr, location.cnstncy_nm);
                                }
                                catch
                                { }
                            }

                            _currentUser.UserRoles.Add("cur", getUserRole(_currentUser.UserId, _app.app_id));//tks
                        }
                        catch { }

                        Session.Add("CurrentUser", _currentUser);
                    }


                }
                

            }
            catch (Exception)
            {
                
            }
        }

        /*
            * RETRIEVES THE USER'S ROLE
            */
        private int getUserRole(int userid, int appid)//tks
        {
            int app_id = appid;
            int role = 0;
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ELECTIONWATCH_HELPERConnectionString"].ToString());// HomeController.GetConnection();
                SqlDataAdapter appRetDa = new SqlDataAdapter("SELECT * FROM UNI_APP_USER_ROLE where usr_id=" + userid.ToString() + " and app_id = " + app_id + " order by role_id asc", con);
                var datTableCnt = new DataTable();
                appRetDa.Fill(datTableCnt);
                int cntApps = datTableCnt.Rows.Count;

                if (cntApps > 0)
                {
                    role = int.Parse(datTableCnt.Rows[0]["role_id"].ToString());
                }
                con.Close();
            }
            catch
            { }

            return role;
        }

        /*
            * DESTROYS THE CURRENT USER OBJECT AND SESSION SIGNING OUT THE USER
            */
        protected void lbtnSignInOut_Click(object sender, EventArgs e)
        {
            if (_currentUser== null)
            {
                Response.Redirect("Login.aspx?ReturnUrl=Default.aspx");
            }
            else
            {
                System.Web.Security.FormsAuthentication.SignOut();

                try
                {
                    string ipadd = GetIPAddress();
                    removeLoggedIn(_currentUser.Username, ipadd);
                    Session.Clear();
                    Response.Redirect("http://kernel/?logout=1");
                }
                catch
                {
                    Session.Clear();
                    Response.Redirect("http://kernel/?logout=1");
                }
            }
        }

        protected void lbtnDashBoard_Click(object sender, EventArgs e)
        {
            if (_currentUser != null)
            {
                Session.Clear();
                Response.Redirect("http://kernel/Default.aspx?usr=" + _currentUser.Username.ToString());
            }
            else
            {
                Session.Clear();
                Response.Redirect("http://kernel/"); //("http://localhost:14239/Default.aspx");
            }
        }


        /*
            * RETRIEVES THE STATIONS WITHIN THE CURRENT USER'S CONSTITUENCY
            */
        private DataSet getSPdata(string proc_name)
        {
            DataSet dssp = new DataSet();
            try
            {
                SqlConnection conn = HomeController.GetConnectionHelper();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = proc_name;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usr_nm", _currentUser.Username);
                cmd.Parameters.AddWithValue("@app", 4);
                cmd.Connection = conn;
                conn.Open();

                SqlDataAdapter dp2 = new SqlDataAdapter(cmd);

                dp2.Fill(dssp, "resultsTable");
                conn.Close();
            }
            catch (Exception)
            { }
            return dssp;
        }

        #endregion USER SESSION TRANSFER AND MANAGEMENT SECTION END


    }
}

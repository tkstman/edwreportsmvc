﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace edwreportsmvc.AppUser
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ELECTIONWATCH_HELPER")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUNI_APP(UNI_APP instance);
    partial void UpdateUNI_APP(UNI_APP instance);
    partial void DeleteUNI_APP(UNI_APP instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ELECTIONWATCH_HELPERConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_EDW_SRCH> VIEW_EDW_SRCHes
		{
			get
			{
				return this.GetTable<VIEW_EDW_SRCH>();
			}
		}
		
		public System.Data.Linq.Table<UNI_APP> UNI_APPs
		{
			get
			{
				return this.GetTable<UNI_APP>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_EDW_INFO", IsComposable=true)]
		public object SP_EDW_INFO([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(20)")] string trn)
		{
			return ((object)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), trn).ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.sp_electionwatch_helper_login")]
		public ISingleResult<sp_electionwatch_helper_loginResult> sp_electionwatch_helper_login([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(20)")] string usr_nm, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(MAX)")] string psswrd)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), usr_nm, psswrd);
			return ((ISingleResult<sp_electionwatch_helper_loginResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_GET_LOCATIONS")]
		public ISingleResult<SP_GET_LOCATIONSResult> SP_GET_LOCATIONS([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(20)")] string usr_nm, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> app)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), usr_nm, app);
			return ((ISingleResult<SP_GET_LOCATIONSResult>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_EDW_SRCH")]
	public partial class VIEW_EDW_SRCH
	{
		
		private int _WORKER_ID;
		
		private string _TRN;
		
		private string _WORKER_TYPE;
		
		private string _APP_TYPE;
		
		private string _STATUS_CD;
		
		private string _TITLE;
		
		private string _LAST_NM;
		
		private string _FRST_NM;
		
		private string _MID_NM;
		
		private string _DOB;
		
		private string _C_PHONE1;
		
		private string _C_PHONE2;
		
		private string _FULL_ADDRESS;
		
		private string _FULL_NAME;
		
		public VIEW_EDW_SRCH()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WORKER_ID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int WORKER_ID
		{
			get
			{
				return this._WORKER_ID;
			}
			set
			{
				if ((this._WORKER_ID != value))
				{
					this._WORKER_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TRN", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string TRN
		{
			get
			{
				return this._TRN;
			}
			set
			{
				if ((this._TRN != value))
				{
					this._TRN = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WORKER_TYPE", DbType="NVarChar(20)")]
		public string WORKER_TYPE
		{
			get
			{
				return this._WORKER_TYPE;
			}
			set
			{
				if ((this._WORKER_TYPE != value))
				{
					this._WORKER_TYPE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APP_TYPE", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string APP_TYPE
		{
			get
			{
				return this._APP_TYPE;
			}
			set
			{
				if ((this._APP_TYPE != value))
				{
					this._APP_TYPE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS_CD", DbType="NVarChar(27)")]
		public string STATUS_CD
		{
			get
			{
				return this._STATUS_CD;
			}
			set
			{
				if ((this._STATUS_CD != value))
				{
					this._STATUS_CD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TITLE", DbType="NVarChar(20)")]
		public string TITLE
		{
			get
			{
				return this._TITLE;
			}
			set
			{
				if ((this._TITLE != value))
				{
					this._TITLE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LAST_NM", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string LAST_NM
		{
			get
			{
				return this._LAST_NM;
			}
			set
			{
				if ((this._LAST_NM != value))
				{
					this._LAST_NM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FRST_NM", DbType="NVarChar(100)")]
		public string FRST_NM
		{
			get
			{
				return this._FRST_NM;
			}
			set
			{
				if ((this._FRST_NM != value))
				{
					this._FRST_NM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MID_NM", DbType="NVarChar(50)")]
		public string MID_NM
		{
			get
			{
				return this._MID_NM;
			}
			set
			{
				if ((this._MID_NM != value))
				{
					this._MID_NM = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOB", DbType="VarChar(30)")]
		public string DOB
		{
			get
			{
				return this._DOB;
			}
			set
			{
				if ((this._DOB != value))
				{
					this._DOB = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_C_PHONE1", DbType="NVarChar(10)")]
		public string C_PHONE1
		{
			get
			{
				return this._C_PHONE1;
			}
			set
			{
				if ((this._C_PHONE1 != value))
				{
					this._C_PHONE1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_C_PHONE2", DbType="NVarChar(20)")]
		public string C_PHONE2
		{
			get
			{
				return this._C_PHONE2;
			}
			set
			{
				if ((this._C_PHONE2 != value))
				{
					this._C_PHONE2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[FULL ADDRESS]", Storage="_FULL_ADDRESS", DbType="NVarChar(575)")]
		public string FULL_ADDRESS
		{
			get
			{
				return this._FULL_ADDRESS;
			}
			set
			{
				if ((this._FULL_ADDRESS != value))
				{
					this._FULL_ADDRESS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[FULL NAME]", Storage="_FULL_NAME", DbType="NVarChar(252) NOT NULL", CanBeNull=false)]
		public string FULL_NAME
		{
			get
			{
				return this._FULL_NAME;
			}
			set
			{
				if ((this._FULL_NAME != value))
				{
					this._FULL_NAME = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UNI_APP")]
	public partial class UNI_APP : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _app_id;
		
		private string _app_nm;
		
		private string _app_cd;
		
		private System.Nullable<bool> _active;
		
		private System.Nullable<int> _rank;
		
		private string _app_desc;
		
		private string _photo_path;
		
		private string _app_url;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onapp_idChanging(int value);
    partial void Onapp_idChanged();
    partial void Onapp_nmChanging(string value);
    partial void Onapp_nmChanged();
    partial void Onapp_cdChanging(string value);
    partial void Onapp_cdChanged();
    partial void OnactiveChanging(System.Nullable<bool> value);
    partial void OnactiveChanged();
    partial void OnrankChanging(System.Nullable<int> value);
    partial void OnrankChanged();
    partial void Onapp_descChanging(string value);
    partial void Onapp_descChanged();
    partial void Onphoto_pathChanging(string value);
    partial void Onphoto_pathChanged();
    partial void Onapp_urlChanging(string value);
    partial void Onapp_urlChanged();
    #endregion
		
		public UNI_APP()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_app_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int app_id
		{
			get
			{
				return this._app_id;
			}
			set
			{
				if ((this._app_id != value))
				{
					this.Onapp_idChanging(value);
					this.SendPropertyChanging();
					this._app_id = value;
					this.SendPropertyChanged("app_id");
					this.Onapp_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_app_nm", DbType="NVarChar(50)")]
		public string app_nm
		{
			get
			{
				return this._app_nm;
			}
			set
			{
				if ((this._app_nm != value))
				{
					this.Onapp_nmChanging(value);
					this.SendPropertyChanging();
					this._app_nm = value;
					this.SendPropertyChanged("app_nm");
					this.Onapp_nmChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_app_cd", DbType="NVarChar(20)")]
		public string app_cd
		{
			get
			{
				return this._app_cd;
			}
			set
			{
				if ((this._app_cd != value))
				{
					this.Onapp_cdChanging(value);
					this.SendPropertyChanging();
					this._app_cd = value;
					this.SendPropertyChanged("app_cd");
					this.Onapp_cdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_active", DbType="Bit")]
		public System.Nullable<bool> active
		{
			get
			{
				return this._active;
			}
			set
			{
				if ((this._active != value))
				{
					this.OnactiveChanging(value);
					this.SendPropertyChanging();
					this._active = value;
					this.SendPropertyChanged("active");
					this.OnactiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rank", DbType="Int")]
		public System.Nullable<int> rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				if ((this._rank != value))
				{
					this.OnrankChanging(value);
					this.SendPropertyChanging();
					this._rank = value;
					this.SendPropertyChanged("rank");
					this.OnrankChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_app_desc", DbType="NVarChar(100)")]
		public string app_desc
		{
			get
			{
				return this._app_desc;
			}
			set
			{
				if ((this._app_desc != value))
				{
					this.Onapp_descChanging(value);
					this.SendPropertyChanging();
					this._app_desc = value;
					this.SendPropertyChanged("app_desc");
					this.Onapp_descChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_photo_path", DbType="NVarChar(100)")]
		public string photo_path
		{
			get
			{
				return this._photo_path;
			}
			set
			{
				if ((this._photo_path != value))
				{
					this.Onphoto_pathChanging(value);
					this.SendPropertyChanging();
					this._photo_path = value;
					this.SendPropertyChanged("photo_path");
					this.Onphoto_pathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_app_url", DbType="NVarChar(100)")]
		public string app_url
		{
			get
			{
				return this._app_url;
			}
			set
			{
				if ((this._app_url != value))
				{
					this.Onapp_urlChanging(value);
					this.SendPropertyChanging();
					this._app_url = value;
					this.SendPropertyChanged("app_url");
					this.Onapp_urlChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class sp_electionwatch_helper_loginResult
	{
		
		private int _usr_id;
		
		private string _usr_nm;
		
		private string _frst_nm;
		
		private string _last_nm;
		
		private int _wloc_id;
		
		private string _wloc_nm;
		
		private string _app_nm;
		
		private int _app_id;
		
		private string _elctn_nm;
		
		private int _elctn_id;
		
		private System.Nullable<System.DateTime> _last_login_dt;
		
		public sp_electionwatch_helper_loginResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_usr_id", DbType="Int NOT NULL")]
		public int usr_id
		{
			get
			{
				return this._usr_id;
			}
			set
			{
				if ((this._usr_id != value))
				{
					this._usr_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_usr_nm", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string usr_nm
		{
			get
			{
				return this._usr_nm;
			}
			set
			{
				if ((this._usr_nm != value))
				{
					this._usr_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_frst_nm", DbType="NVarChar(50)")]
		public string frst_nm
		{
			get
			{
				return this._frst_nm;
			}
			set
			{
				if ((this._frst_nm != value))
				{
					this._frst_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_last_nm", DbType="NVarChar(50)")]
		public string last_nm
		{
			get
			{
				return this._last_nm;
			}
			set
			{
				if ((this._last_nm != value))
				{
					this._last_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_wloc_id", DbType="Int NOT NULL")]
		public int wloc_id
		{
			get
			{
				return this._wloc_id;
			}
			set
			{
				if ((this._wloc_id != value))
				{
					this._wloc_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_wloc_nm", DbType="NVarChar(100)")]
		public string wloc_nm
		{
			get
			{
				return this._wloc_nm;
			}
			set
			{
				if ((this._wloc_nm != value))
				{
					this._wloc_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_app_nm", DbType="NVarChar(50)")]
		public string app_nm
		{
			get
			{
				return this._app_nm;
			}
			set
			{
				if ((this._app_nm != value))
				{
					this._app_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_app_id", DbType="Int NOT NULL")]
		public int app_id
		{
			get
			{
				return this._app_id;
			}
			set
			{
				if ((this._app_id != value))
				{
					this._app_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_elctn_nm", DbType="NVarChar(100)")]
		public string elctn_nm
		{
			get
			{
				return this._elctn_nm;
			}
			set
			{
				if ((this._elctn_nm != value))
				{
					this._elctn_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_elctn_id", DbType="Int NOT NULL")]
		public int elctn_id
		{
			get
			{
				return this._elctn_id;
			}
			set
			{
				if ((this._elctn_id != value))
				{
					this._elctn_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_last_login_dt", DbType="DateTime")]
		public System.Nullable<System.DateTime> last_login_dt
		{
			get
			{
				return this._last_login_dt;
			}
			set
			{
				if ((this._last_login_dt != value))
				{
					this._last_login_dt = value;
				}
			}
		}
	}
	
	public partial class SP_GET_LOCATIONSResult
	{
		
		private int _wloc_id;
		
		private string _wloc_nm;
		
		private int _cnstncy_nbr;
		
		private string _cnstncy_nm;
		
		private int _region_nbr;
		
		private string _zone_nm;
		
		public SP_GET_LOCATIONSResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_wloc_id", DbType="Int NOT NULL")]
		public int wloc_id
		{
			get
			{
				return this._wloc_id;
			}
			set
			{
				if ((this._wloc_id != value))
				{
					this._wloc_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_wloc_nm", DbType="NVarChar(100)")]
		public string wloc_nm
		{
			get
			{
				return this._wloc_nm;
			}
			set
			{
				if ((this._wloc_nm != value))
				{
					this._wloc_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cnstncy_nbr", DbType="Int NOT NULL")]
		public int cnstncy_nbr
		{
			get
			{
				return this._cnstncy_nbr;
			}
			set
			{
				if ((this._cnstncy_nbr != value))
				{
					this._cnstncy_nbr = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cnstncy_nm", DbType="NVarChar(100)")]
		public string cnstncy_nm
		{
			get
			{
				return this._cnstncy_nm;
			}
			set
			{
				if ((this._cnstncy_nm != value))
				{
					this._cnstncy_nm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_region_nbr", DbType="Int NOT NULL")]
		public int region_nbr
		{
			get
			{
				return this._region_nbr;
			}
			set
			{
				if ((this._region_nbr != value))
				{
					this._region_nbr = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_zone_nm", DbType="NVarChar(20)")]
		public string zone_nm
		{
			get
			{
				return this._zone_nm;
			}
			set
			{
				if ((this._zone_nm != value))
				{
					this._zone_nm = value;
				}
			}
		}
	}
}
#pragma warning restore 1591

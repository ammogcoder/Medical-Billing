using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Parser;

namespace Spheris.Billing.Core.Domain
{
    public class ReportPair : ObjectBase<ReportPair>// IDataErrorInfo, INotifyPropertyChanged
    {
        public ReportPair()
        { }
        public ReportPair(ReportPair copyMe)
        {
            ReportedFieldName = copyMe.ReportedFieldName.ToString();
            DisplayedAs = copyMe.DisplayedAs.ToString();
        }

        public ReportPair(string reportedFieldName, string displayedAs)
        {
            ReportedFieldName = reportedFieldName;
            DisplayedAs = displayedAs;
        }

        public string ReportedFieldName
        {
            get;
            set;
        }

        public string DisplayedAs { get; set; }



        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public string HasError
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    string str = GetValidationError(property);
                    if (str != null)
                        return str;
                }
                return null;
            }
        }

        static readonly string[] ValidatedProperties = 
        { 
            "DisplayedAs"
        };

        public override string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            //Error = null;
            string error = null;

            if (propertyName == "DisplayedAs")
            {
#if NOT_ALLOW_NULLS
                if (String.IsNullOrEmpty(DisplayedAs) || DisplayedAs.Trim() == String.Empty)
                    error = "DisplayedAs string must have a valid value.";
#endif
            }
            return error;
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ReportPair;
            if (t == null)
                return false;
            if (this.DisplayedAs == t.DisplayedAs && 
                this.DisplayName == t.DisplayName && 
                this.ReportedFieldName == t.ReportedFieldName)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(DisplayedAs).CombineHashCode(DisplayName).CombineHashCode(ReportedFieldName);
        }


    }

    /// <summary>
    /// Fields are supplied from the view SPHRSBILLING.CSV_DETAILS
    /// Initially, apparently , all fields are to be "AVAILABLE" and not as
    /// Reported.  
    /// </summary>
    public class ReportTypePackage : IDataErrorInfo, INotifyPropertyChanged, IEquatable<ReportTypePackage>
    {
        #region Fields
        public string originalSql;
        #endregion


        public ReportTypePackage(ReportTypePackage copyMe)
        {
            if (copyMe == null)
                return;
            Parser = new SqlParser();
            ReportedFields = new ObservableCollection<ReportPair>();
            AvailableFields = new ObservableCollection<string>();
            

            foreach (ReportPair pair in copyMe.ReportedFields)
                ReportedFields.Add(new ReportPair(pair));

            foreach (string avail in copyMe.AvailableFields)
                AvailableFields.Add(avail.ToString());

            Id = copyMe.Id;
            MustEncrypt = copyMe.MustEncrypt;
            AllowDynamicMod = copyMe.AllowDynamicMod;
            DataDumpOnly = copyMe.DataDumpOnly;
            Description = copyMe.Description.ToString();
            ShortName = copyMe.ShortName.ToString();
            Sql = copyMe.Sql.ToString();
            ReConstructPackageListFromSql(null);
            //ReConstructSQLFromGridView();

            Table = copyMe.Table;
            //OrderByClause = copyMe.OrderByClause;
            originalSql = copyMe.originalSql;
        }

        public ReportTypePackage()
        {
            ReportedFields = new ObservableCollection<ReportPair>();
            AvailableFields = new ObservableCollection<string>();
            Parser = new SqlParser();
        }

        /// <summary>
        /// SPHRSBILLING.REPORT_TYPE 
        /// </summary>
        public ReportTypePackage(ReportTypeTable table)
        {
            Table = table;
            ReportedFields = new ObservableCollection<ReportPair>();
            AvailableFields = new ObservableCollection<string>();
            Parser = new SqlParser();
        }

        public SqlParser Parser
        {
            get;
            set;
        }

        public string SelectClause
        {
            get
            {
                if (Parser == null)
                    return null;
                return Parser.SelectClause;
            }
        }

        //public string orderByClause;
        public string OrderByClause
        {
            get
            {

                if (Parser == null || !Parser.IsDocumentInit)
                    return null;
                return Parser.OrderByClause;
            }
            set
            {
                //Parser.OrderByClause = value;
                ReConstructPackageListFromSql(value.ToString());
            }
        }
        public string whereClause;
        public string WhereClause
        {
            get
            {

                if (Parser == null || !Parser.IsDocumentInit)
                    return null;
                return Parser.WhereClause;
            }
            set
            {
                Parser.WhereClause = value;


            }
        }

#if FROM_CLAUSE 
        public string fromClause;
        public string FromClause
        {
            get
            {

                if (Parser == null || !Parser.IsDocumentInit)
                    return null;
                return Parser.FromClause;
            }
            set
            {
                Parser.FromClause = value;

            }
        }
#endif

        public ReportTypeTable Table { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// MUST_ENCRYPT - 
        /// If ReportedFields contains - PATIENT_ID, PATIENT_FIRST, PATIENT_LAST 
        /// then MustEncrypt must be true
        /// </summary>
        public bool MustEncrypt { get; set; }

        /// <summary>
        /// ALLOW_DYNAMIC_MODS Defaults to 'N' - Not the viewable
        /// </summary>
        public bool AllowDynamicMod { get; set; }

        /// <summary>
        /// Defaults to 'N' - Not the viewable
        /// </summary>
        public bool DataDumpOnly { get; set; }

        /// <summary>
        /// DESC - 
        /// </summary>
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                if (description != null)
                    description = description.Trim();
            }
        }

        /// <summary>
        /// The short name
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// The Sql statement - to be parsed in 
        /// </summary>
        public string sql;
        public string Sql
        {
            get
            {
                return sql;
            }
            set
            {
                if (sql == null)
                    originalSql = value;

                sql = value;
                OnPropertyChanged("Sql");
            }
        }

        /// <summary>
        /// Fields from the view SPHRSBILLING.CSV_DETAILS - Or fields from other tables??
        /// </summary>
        private ObservableCollection<string> availableFields;
        public ObservableCollection<string> AvailableFields
        {
            get
            {
                return availableFields;
            }
            set
            {
                availableFields = value;
            }
        }

        /// <summary>
        /// Fields from the view SPHRSBILLING.CSV_DETAILS which are used
        /// for selection
        /// </summary>
        public ObservableCollection<ReportPair> reportedFields ;
        public ObservableCollection<ReportPair> ReportedFields 
        {
            get
            { 
                return reportedFields ; 
            }

            set
            {
                reportedFields = value;
            }
        }

        public void EmptyReportedFields()
        {
            ReportedFields = new ObservableCollection<ReportPair>();
        }

        public bool ParsePackage()
        {
            try
            {
                Parser.Parse(Sql);
                OnPropertyChanged("OrderByClause");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool HasCsv
        {
            get
            {
                if (Parser.FromList != null)
                {
                    foreach (string from in Parser.FromList)
                    {
                        if (from.ToUpper().Contains("CSV_DETAILS"))
                            return true;
                    }
                }
                return false;
            }
        }

        public bool ReConstructPackageListFromSql(string newOrderBy)
        {
            try
            {

                //string tmpOrderby = (string.IsNullOrEmpty(OrderByClause)) ? "" : OrderByClause.ToString();
                Parser.Parse(Sql);
                EmptyReportedFields();

                for (int i = 0; i < Parser.SelectList.Count; i++)
                {
                    ReportedFields.Add(new ReportPair(Parser.SelectList[i], Parser.AsList[i]));
                }
                //////////////////////////
                Parser.SelectList = new List<string>();
                Parser.AsList = new List<string>();
                foreach (ReportPair reported in ReportedFields)
                {
                    Parser.SelectList.Add(reported.ReportedFieldName);
                    Parser.AsList.Add(reported.DisplayedAs);
                }

                if(!string.IsNullOrEmpty( newOrderBy ))
                    Parser.OrderByClause = newOrderBy;

                if (ReportedFields.Count > 0)
                    Sql = Parser.ReBuild(true);
                //////////////////////////
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ReConstructSQLFromGridView()
        {
            if (Parser == null || Parser.SelectList == null)
                return false;
            Parser.SelectList = new List<string>();
            Parser.AsList = new List<string>();
            foreach (ReportPair reported in ReportedFields)
            {
                Parser.SelectList.Add(reported.ReportedFieldName);
                Parser.AsList.Add(reported.DisplayedAs);
            }
            try
            {
                if (ReportedFields.Count > 0)
                    Sql = Parser.ReBuild(true);

                //////////////////////
                Parser.Parse(Sql);
                EmptyReportedFields();

                for (int i = 0; i < Parser.SelectList.Count; i++)
                {
                    ReportedFields.Add(new ReportPair(Parser.SelectList[i], Parser.AsList[i]));
                }

                /////////////////////
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        public string this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }
        #endregion

        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public string HasError
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    string str = GetValidationError(property);
                    if (str != null)
                        return str;
                }
                return null;
            }
        }

        public string FirstError
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    string msg = GetValidationError(property);
                    if (msg != null)
                        return msg;
                }
                return null;
            }
        }

        static readonly string[] ValidatedProperties = 
        { 
            "Sql",
            "Description",
            "ReportedFields"
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Sql":
                    error = this.ValidateSql();
                    break;

                case "Description":
                    error = this.ValidateDescription();
                    break;

                case "ReportedFields":
                    error = this.ValidateReportedFields();
                    break;
            }
            return error;
        }

        string ValidateReportedFields()
        {
            foreach (ReportPair pair in ReportedFields)
            {
                string str = pair.HasError;
                if (str != null)
                    return str;
            }
            return null;
        }

        string ValidateSql()
        {
            //TODO - Parser work
            if (string.IsNullOrEmpty(Sql) || Sql.Trim() == String.Empty)
                return "Sql statement is invalid";
            return null;
        }

        string ValidateDescription()
        {
            //TODO - What are imporoper characters?
            if (string.IsNullOrEmpty(Description) || Description.Trim() == String.Empty)
                return "Description must have proper value";
//#if WTF
            if (Table.IsDuplicate(this))
                return "The Key Description already exists";
//#endif
            return null;
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        public bool Equals(ReportTypePackage compareMe)
        {
            if (compareMe.Sql != Sql ||
                compareMe.Description != Description ||
                compareMe.AllowDynamicMod != AllowDynamicMod ||
                compareMe.MustEncrypt != MustEncrypt ||
                compareMe.ShortName != ShortName ||
                compareMe.OrderByClause != OrderByClause)
                return false;
            return true;// throw new NotImplementedException();
        }

        #region GetHashCode()
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        /// <a href="http://msdn.microsoft.com/en-us/library/system.object.gethashcode.aspx"/>
        public override int GetHashCode()
        {
            return 0.CombineHashCode(MustEncrypt)
                .CombineHashCode(AllowDynamicMod)
                .CombineHashCode(DataDumpOnly)
                .CombineHashCode(Description)
                .CombineHashCode(ShortName)
                .CombineHashCode(Sql);
        }
        #endregion

    }

    /// <summary>
    /// TABLE DEFINITION *****************************************************************
    /// </summary>
    public class ReportTypeTable : ObservableCollection<ReportTypePackage>, IDataErrorInfo
    {
        public ReportTypeTable(ReportTypeTable copyMe)
        {
            foreach (ReportTypePackage package in copyMe as ObservableCollection<ReportTypePackage>)
            {
                ReportTypePackage copyOf = new ReportTypePackage(package);
                Add(copyOf);
            }
        }

        public ReportTypeTable()
        {
        }

        public bool Load()
        {
            //TODO
            return false;
        }

        public bool Save()
        {
            //TODO
            return false;
        }

        /// <summary>
        /// Generate all the AsLists - the Select lists ...ect.
        /// </summary>
        public void ParseEntireTable()
        {
            foreach (ReportTypePackage package in this)
            {
                package.ReConstructPackageListFromSql(null);
            }
        }

        public bool PlacePackage(string desc, ReportTypePackage replace)
        {
            bool done = false;
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Description.ToUpper() == replace.Description.ToUpper())
                {
                    this[i] = replace;
                    done = true;
                    break;
                }
            }
            return done;
        }

        public bool IsDuplicate(ReportTypePackage testPackage)
        {
            bool exists = false;
            foreach (ReportTypePackage package in this)
            {
                if (package.Equals(testPackage) == true)
                    continue;
                if (package.Description.ToUpper() == testPackage.Description.ToUpper())
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        public ReportTypePackage GetPackage(int id)
        {
            foreach (ReportTypePackage package in this)
                if (package.Id == id)
                    return package;
            return null;
        }


        public string HasError
        {
            get
            {
                return CheckPackages();
            }
        }

        #region IDataErrorInfo Members

        public string CheckPackages()
        {
            foreach (ReportTypePackage package in this)
            {
                string error = package.HasError;
                if (error != null)
                    return error;
            }
            return null;
        }

        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        public string this[string propertyName]
        {
            get
            {
#if TABLE_LEVEL_VALIDATION
                return CheckPackages();
#endif
                return null;
            }
        }

        //Table level
        // Fresh list of all fields from the view
        private ObservableCollection<string> availableFields;
        public ObservableCollection<string> AvailableFields
        {
            get
            {
                return availableFields;
            }
            set
            {
                availableFields = value;
            }
        }
        #endregion
    }

    public class DetailInvoiceFileModel
    {
        private ReportTypeTable _ReportTypes;

        public ReportTypeTable ReportTypes 
        {
            get 
            {
                return _ReportTypes;
            }
            set
            {
                _ReportTypes = value;
                
            }
        }

        public DetailInvoiceFileModel()
        {

#if DEBUG
            ReportTypes = new ReportTypeTable();
            ReportTypePackage package1 = new ReportTypePackage(ReportTypes);

            package1.AvailableFields = new ObservableCollection<string>();
            package1.AvailableFields.Add("Pack1 Field1");
            package1.AvailableFields.Add("Pack1 Field2");
            package1.AvailableFields.Add("Pack1 Field3");
            package1.AvailableFields.Add("Pack1 Field4");

            package1.ReportedFields = new ObservableCollection<ReportPair>();
            package1.ReportedFields.Add(new ReportPair("ReportedField1", "As1"));
            package1.ReportedFields.Add(new ReportPair("ReportedField2", "As2"));
            package1.ReportedFields.Add(new ReportPair("ReportedField3", "As3"));
            package1.ReportedFields.Add(new ReportPair("ReportedField4", "As4"));
            package1.Sql = "Select field1,field2,field3 from table where x=y order by x";
            package1.Description = "Description 1";

            //ReportTypes.AddPackage(package1);
            ReportTypes.Add(package1);
            //////////////
            ReportTypePackage package2 = new ReportTypePackage(ReportTypes);
            package2.AvailableFields = new ObservableCollection<string>();
            package2.AvailableFields.Add("Pack2 Field1");
            package2.AvailableFields.Add("Pack2 Field2");
            package2.AvailableFields.Add("Pack2 Field3");
            package2.AvailableFields.Add("Pack2 Field4");

            package2.ReportedFields = new ObservableCollection<ReportPair>();
            package2.ReportedFields.Add(new ReportPair("ReportedField1", "As1"));
            package2.ReportedFields.Add(new ReportPair("ReportedField2", "As2"));
            package2.ReportedFields.Add(new ReportPair("ReportedField3", "As3"));
            package2.ReportedFields.Add(new ReportPair("ReportedField4", "As4"));
            package2.Sql = "Select field11,field22,field33 from table2 where x=y order by x";

            package2.Description = "Description 2";

            //ReportTypes.AddPackage(package2);
            ReportTypes.Add(package2);

#endif

#if CALLED_FROM_SOME_FORM
            DetailInvoiceFileDesignerForm mainWindow = new DetailInvoiceFileDesignerForm();

            UIHelper.InitializeWpfFormInterop(mainWindow);

            DetailInvoiceFileVM VM = new DetailInvoiceFileVM(this);
            mainWindow.DataContext = VM;

            // When the ViewModel asks to be closed, 
            // close the window.
            EventHandler handler = null;
            handler = delegate
            {
                VM.RequestClose -= handler;
                mainWindow.Close();
            };
            VM.RequestClose += handler;

            mainWindow.ShowDialog();
#endif
        }
    }
}

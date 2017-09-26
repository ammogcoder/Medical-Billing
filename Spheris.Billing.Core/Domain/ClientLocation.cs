using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Linq.Expressions;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// Keys are EXT_CLIENT,EXT_SYS
    /// Used for location - Keys on DEFAULT_INVOICE_GRP_ID
    /// </summary>
    public class ClientLocation : ObjectBase<ClientLocation> 
    {
        /// <summary>
        /// Special  for this item.  There is no ID for us
        /// to track as == 0 value.
        /// </summary>
        public bool IsAdding { get; set; }

        public bool IsSpoof { get; set; }

        
        public ClientLocation Clone()
        {
            return BaseClone<ClientLocation>();
        }
        

        /// <summary>
        /// EXT_SYS  
        /// VARCHAR(20)
        /// AllowNull n
        /// Ties to table EXT_SYS
        /// </summary>
        public string ExtSys
        {
            get
            {
                return _ExtSys;
            }
            set
            {
                _ExtSys = value;
                OnPropertyChanged("ExtSys");
            }
        }
        private string _ExtSys;

        /// <summary>
        /// EXT_CLIENT_KEY
        /// VARCHAR(20)
        /// AllowNull n
        /// Ties to Table EXT_WORK_TYPE
        /// </summary>
        public string ExtClientKey
        {
            get
            {
                return _ExtClientKey;
            }
            set
            {
                _ExtClientKey = value;
                OnPropertyChanged("ExtClientKey");
            }
        }
        private string _ExtClientKey;

        /// <summary>
        /// DEFAULT_INVOICE_GRP_ID
        /// NUMBER
        /// AllowNull Y
        /// </summary>
        public decimal? DefaultInvoiceGrpID
        {
            get
            {
                return _DefaultInvoiceGrpID;
            }
            set
            {
                _DefaultInvoiceGrpID = value;
                OnPropertyChanged("DefaultInvoiceGrpID");
            }
        }
        private decimal? _DefaultInvoiceGrpID;

        /// <summary>
        /// DESCR
        /// VARCHAR(100)
        /// AllowNull N
        /// </summary>
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
        }
        private string _Description;

        /// <summary>
        /// COST_CODE
        /// VARCHAR(10)
        /// AllowNull Y
        /// </summary>
        public string CostCode
        {
            get
            {
                return _CostCode;
            }
            set
            {
                _CostCode = value;
                OnPropertyChanged("CostCode");
            }
        }
        private string _CostCode;

        /// <summary>
        /// EXT_WORK_TYPE_SOURCE_DESCR
        /// VARCHAR(30)
        /// default: "DEFAULT"
        /// AllowNull N
        /// Ties to Table EXT_WORK_TYPE_SOURCE
        /// </summary>
        public string ExtWorkTypeSourceDescr
        {
            get
            {
                return _ExtWorkTypeSourceDescr;
            }
            set
            {
                _ExtWorkTypeSourceDescr = value;
                OnPropertyChanged("ExtWorkTypeSourceDescr");
            }
        }
        private string _ExtWorkTypeSourceDescr;

        /// <summary>
        /// OVERRIDE_KEY_SOURCE
        /// VARCHAR2(30)
        /// default: "DEFAULT"
        /// AllowNull Y
        /// Ties to Table OVERRIDE_KEY_SOURCE
        /// </summary>
        public string OverrideKeySource
        {
            get
            {
                return _OverrideKeySource;
            }
            set
            {
                _OverrideKeySource = value;
                OnPropertyChanged("OverrideKeySource");
            }
        }
        private string _OverrideKeySource;

        /// <summary>
        /// DEFAULT_TAT_THRESHOLD
        /// NUMBER
        /// default: 12
        /// AllowNull Y
        /// </summary>
        public decimal? DefaultTatThreshold
        {
            get
            {
                return _DefaultTatThreshold;
            }
            set
            {
                _DefaultTatThreshold = value;
                OnPropertyChanged("DefaultTatThreshold");
            }
        }
        private decimal? _DefaultTatThreshold;

        /// <summary>
        /// Not in the database
        /// </summary>
        public string DefaultInvoiceGrpName
        {
            get
            {
                return defaultInvoiceGrpName;
            }
            set
            {
                defaultInvoiceGrpName = value;
                OnPropertyChanged("DefaultInvoiceGrpName");
            }
        }
        private string defaultInvoiceGrpName;

        static readonly string[] Keys = 
        { 
            "ExtSys"
            ,"ExtClientKey"
            ,"Description"
        };

        public string ValidateKeys()
        {
            return Keys.FirstOrDefault(str => GetValidationError(str) != null) as string;
        }

        public override string GetValidationError(string propertyName)
        {
            switch(propertyName)
            {
                case "Description":
                    return (HasValue(Description)) ? null : "Description must be populated";
                case "ExtSys":
                    return (HasValue(ExtSys)) ? null : "ExtSys must be populated";
                case "ExtClientKey":
                    return (HasValue(ExtClientKey)) ? null : "ExtClientKey must be populated";
                default:
                    break;
            }
                   
            // TODO - Is edited?
            return null;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(ExtClientKey)
                .CombineHashCode(ExtSys);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ClientLocation;
            if (t == null)
                return false;
            if (ExtClientKey == t.ExtClientKey && this.ExtSys == t.ExtSys)
                return true;
            return false;
        }
   
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Linq.Expressions;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// select AO.DESCR ,A.INVOICE_ID,NVL(A.FOR_PERIOD_BEGINNING,I.BILL_PERIOD_START  ) 
    /// as BEGINS_ON from SPHRSBILLING.ADD_ON_CHG A 
    /// JOIN SPHRSBILLING.INVOICE I 
    /// ON I.INVOICE_ID=A.INVOICE_ID 
    /// JOIN SPHRSBILLING.ADD_ON_CHG_TYPE AO
    /// ON A.ADD_ON_CHG_TYPE_ID=AO.ADD_ON_CHG_TYPE_ID 
    /// WHERE I.INVOICE_GRP_ID=7132 
    /// </summary>
    public class ActiveSchedule : ObjectBase<ActiveSchedule>
    {

        public ActiveSchedule Clone()
        {
            return BaseClone<ActiveSchedule>();
        }

        private decimal _INVOICE_ID;
        public decimal INVOICE_ID
        {
            get
            {
                return _INVOICE_ID;
            }
            set
            {
                _INVOICE_ID = value;
                OnPropertyChanged("INVOICE_ID");
            }
        }
        private DateTime _BEGINS_ON;
        public DateTime BEGINS_ON
        {
            get
            {
                return _BEGINS_ON;
            }
            set
            {
                _BEGINS_ON = value;
                OnPropertyChanged("BEGINS_ON");
            }
        }
        private string _TYPE;
        public string TYPE
        {
            get
            {
                return _TYPE;
            }
            set
            {
                _TYPE = value;
                OnPropertyChanged("TYPE");
            }
        }


        public override string GetValidationError(string propertyName)
        {
            return null;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(INVOICE_ID).CombineHashCode(BEGINS_ON).CombineHashCode(TYPE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ActiveSchedule;
            if (t == null)
                return false;
            if (INVOICE_ID == t.INVOICE_ID 
                && BEGINS_ON == t.BEGINS_ON
                && TYPE == t.TYPE)
                return true;
            return false;
        }

    }
}




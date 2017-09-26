using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// A Note associated with a Contract
    /// </summary>
    public class ContractNote : INotifyPropertyChanged,IDataErrorInfo
    {
        #region Fields

        private int _id;
        private int _contractID;
        private DateTime _dateAdded;
        private string _addedBy;
        private string _note;

        #endregion

        #region ctor
        public ContractNote()
        { 
        }

        public ContractNote(int noteId, int contractId, DateTime added, string addedBy, string note)
        {
            _id = noteId;
            _contractID = contractId;
            _dateAdded = added;
            _addedBy = addedBy;
            _note = note;
            Modified = false;
        }

        #endregion

        #region Properties

        public bool Modified { get; set; }


        /// <summary>
        /// InvoiceGrpId of the note
        /// </summary>
        public int ID
        {
            get 
            { 
                return _id; 
            }
            set 
            { 
                _id = value; 
                OnPropertyChanged("Value"); 
            }
        }

        /// <summary>
        /// Owner/Contract InvoiceGrpId of the note
        /// </summary>
        public int ContractID
        {
            get 
            { 
                return _contractID; 
            }
            set 
            { 
                _contractID = value; 
                OnPropertyChanged("ContractID"); 
            }
        }

        /// <summary>
        /// Date the note was added
        /// </summary>
        public DateTime DateAdded
        {
            get 
            { 
                return _dateAdded;
            }
            set 
            { 
                _dateAdded = value; 
                OnPropertyChanged("DateAdded"); 
            }
        }

        /// <summary>
        /// Who added this note
        /// </summary>
        public string AddedBy
        {
            get 
            { 
                return _addedBy; 
            }
            set 
            { 
                _addedBy = value; 
                OnPropertyChanged("AddedBy"); 
            }
        }

        /// <summary>
        /// The note string itself
        /// </summary>
        public string Note
        {
            get 
            { 
                return _note; 
            }
            set 
            {
                if (_note != value && _note != null)
                {
                    Modified = true;
                }

                _note = value;
                OnPropertyChanged("Note"); 
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

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
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
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
            "Note"
        };

        private string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Note":
                    error = this.ValidateNote();
                    break;
            }
            return error;
        }

        private string ValidateNote()
        {
            if (string.IsNullOrEmpty(Note) || Note.Trim() == String.Empty || Note == "<Type Text Here>")
                return "Empty Note";
            return null;
        }


        #endregion
    }
}

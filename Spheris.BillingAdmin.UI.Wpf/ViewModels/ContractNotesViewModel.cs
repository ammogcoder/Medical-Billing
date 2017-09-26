using System;
using System.Collections.ObjectModel;
using Spheris.Billing.Core.Domain;
using System.ComponentModel;
using System.Windows.Input;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.BillingAdmin.UI.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel for editing of Notes associated to a contract.
    /// </summary>
    public class ContractNotesViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private int _contractId;
        private ContractNote _currentNote;
        private ObservableCollection<ContractNote> contractNotes;
        RelayCommand _closeCommand;
        bool IsBusy = false;
        #endregion

        #region ctor

        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        public ContractNotesViewModel(int contractId, string userId)
        {
            UserId = userId;
            _contractId = contractId;

            Target = Billing.Data.BillingDataFactory.NewInstance().CreateContractNoteRepository();

            #if MOCK
            ContractNotes = new ObservableCollection<ContractNote>();
            ContractNotes.Add(new ContractNote(1, 1, DateTime.Now, "Frank", "This is my note"));
            ContractNotes.Add(new ContractNote(2, 2, DateTime.Now, "Allen", "This is not my note"));
            #endif

            ContractNotes = Target.GetByContractID(contractId);
            if (ContractNotes.Count > 0)
                CurrentNote = ContractNotes[0];
            EnsureOneNote();

            SaveCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => !IsBusy,
                ExecuteDelegate = x => Save()
            };

            NewCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => AreNotesValid,
                ExecuteDelegate = x => NewNote()
            };

            RemoveCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => !IsBusy,
                ExecuteDelegate = x => Remove()
            };

            OnPropertyChanged("ContractNotes");
        }

        #endregion

        private IContractNoteRepository Target
        {
            get;
            set;
        }

        private void EnsureOneNote()
        { 
            if(ContractNotes.Count == 0)
                NewNote();
        }

        /// <summary>
        /// Save or Update the Note
        /// </summary>
        public void Save()
        {
            bool allValid = true;
            foreach (ContractNote note in ContractNotes)
            {
                if (note.IsValid)
                {
                    if (note.ID == 0)
                    {
                        note.DateAdded = DateTime.Now;
                        note.AddedBy = UserId;
//#if MOCK
                        Target.Add(note);
//#endif
                        note.Modified = false;

                    }
                    else if (note.Modified)
                    {
                        note.DateAdded = DateTime.Now;
//#if MOCK
                        Target.Update(note);
//#endif
                        note.Modified = false;
                    }
                }
                else
                    allValid = false;
            }
            EnsureOneNote();
            if (allValid)
                Modified = false;
        }

        /// <summary>
        /// Remove the active Note from the list
        /// </summary>
        public void Remove()
        {
            if (CurrentNote != null)
            {
                // If note as not just created - not residing in the database
                if (CurrentNote.ID != 0)
                {
                    IContractNoteRepository target = Billing.Data.BillingDataFactory.NewInstance().CreateContractNoteRepository();
                    target.Remove(CurrentNote);
                }
                ContractNotes.Remove(CurrentNote);

                if (ContractNotes.Count > 0)
                    CurrentNote = ContractNotes[0];
            }
            EnsureOneNote();
        }

        /// <summary>
        /// New Note
        /// </summary>
        public void NewNote()
        {
            CurrentNote = new ContractNote(0, _contractId, DateTime.Now, UserId, "<Type Text Here>");
            ContractNotes.Add( CurrentNote );
            IndexText = (ContractNotes.IndexOf(_currentNote) + 1).ToString() + " of " + ContractNotes.Count.ToString();
            OnPropertyChanged("ContractNotes");
        }


        #region Properties

        private bool modified;
        public bool Modified
        {
            get
            {
                return modified;
            }
            set 
            {
                modified = value;
                OnPropertyChanged("Modified");
            }
        }


        public string UserId
        {
            private get;
            set;
        }

        public  ObservableCollection<ContractNote> ContractNotes
        {
            get
            {
                foreach (ContractNote note in contractNotes)
                {
                    if (note.Modified == true)
                        Modified = true;
                }

                return contractNotes;
            }
            set
            {
                contractNotes = value;
                OnPropertyChanged("ContractNotes");
            }
        }

        public  ContractNote CurrentNote
        {
            get
            {
                return _currentNote;
            }
            set
            {
                _currentNote = value;

                IndexText = (ContractNotes.IndexOf(_currentNote) + 1).ToString() + " of " + ContractNotes.Count.ToString();
                OnPropertyChanged("CurrentNote");
                //OnPropertyChanged("IsValidNote");
            }
        }

        private string indexText;
        public string IndexText
        {
            get
            { 
                return indexText;
            }
            set
            {
                indexText = value;
                OnPropertyChanged("IndexText");
            }
        }

        public bool AreNotesValid
        {
            get
            {
                foreach(ContractNote note in ContractNotes)
                    if(!note.IsValid) 
                        return false;
                return true;
            }
        }

        /// <summary>
        /// New Command handler
        /// </summary>
        public ICommand NewCommand
        {
            private get;
            set;
        }

        /// <summary>
        /// Save Command handler
        /// </summary>
        public ICommand SaveCommand
        {
            private get;
            set;
        }

        /// <summary>
        /// Remove Command Handler
        /// </summary>
        public ICommand RemoveCommand
        {
            private get;
            set;
        }

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {

                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(param => OnRequestClose());
                

                return _closeCommand;
            }
        }
        
        #endregion

        public string Error
        {
            get
            {
                return null;
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return String.Empty;
            }
        }
    }
}

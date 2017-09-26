using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using GongSolutions.Wpf.DragDrop;
using System.ComponentModel.Composition;

namespace MedQuist.BillingAdmin.ViewModels
{

    [Export]
    public class DetailInvoiceFileVM : ViewModelBase, IDropTarget, IDataErrorInfo
    {

        #region Fields
        private const string newDescription = "<Type New Name>";
        private ReportTypePackage comparePackage;
        RelayCommand _closeCommand;
        bool CreatingNew = false;
        bool IsBusy = false;
        #endregion

        #region ctor


        [ImportingConstructor]
        public DetailInvoiceFileVM( )
        {
        }

        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        public DetailInvoiceFileVM(ReportTypeTable _Table)
        {
            Target = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceDetailRepository();

            table = Target.MakeReportTable();

            SelectedGrid = new ObservableCollection<object>();
            SelectedData = new ObservableCollection<object>();
            status = "Ok";
#if TOO_SLOW
            table.ParseEntireTable();
#endif
            SaveCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (HasErrorVM == null),
                ExecuteDelegate = x => Save()
            };

            NoSaveCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (HasErrorVM == null),
                ExecuteDelegate = x => NoSave()
            };

            CancelCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => !IsBusy,
                ExecuteDelegate = x => Cancel()
            };

            NewTypeCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (HasErrorVM == null),
                ExecuteDelegate = x => NewType()
            };

            RemoveTypeCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => !IsBusy,
                ExecuteDelegate = x => RemoveType()
            };

            SQLCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => !IsBusy,
                ExecuteDelegate = x => SwapMode()
            };

            ShiftLeftCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => !IsBusy,
                ExecuteDelegate = x => ShiftLeft()
            };

            ShiftRightCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => !IsBusy,
                ExecuteDelegate = x => ShiftRight()
            };

            Package = Table[0];
            //Package = Table[ Table.Count - 1 ];
        }
        #endregion

        #region DRAG&DROP
        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            //If dragging from Available -> Reported 
            if ((dropInfo.Data is string || dropInfo.Data is List<string>) && dropInfo.TargetItem is ReportPair)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }

            // If dragging from Reported to Available
            if ((dropInfo.Data is ReportPair || dropInfo.Data is List<ReportPair>) && !(dropInfo.TargetItem is ReportPair))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
            if (dropInfo.Data is List<string>)
            {
                foreach (string field in dropInfo.Data as List<string>)
                    MoveItemLeft(field);
            }
            if (dropInfo.Data is string)
                MoveItemLeft(dropInfo.Data as string);
            if (dropInfo.Data is ReportPair)
                MoveItemRight(dropInfo.Data as ReportPair);

            if (dropInfo.Data is List<ReportPair>)
                foreach (ReportPair pair in dropInfo.Data as List<ReportPair>)
                    MoveItemRight(pair);
        }
        
        void MoveItemRight(ReportPair pair)
        {
            Package.ReportedFields.Remove(pair);
        }

        void MoveItemLeft(string field)
        {
            foreach (ReportPair pair in package.ReportedFields)
            {
                if (pair.ReportedFieldName.ToLower() == field.ToLower())
                {
                    return;
                }
            }

            Package.ReportedFields.Add(new ReportPair(field, field));
        }

        public void ShiftLeft()
        {

            List<string> fields = new List<string>();

            // Cannot operate against selectedData in the foreach loop
            foreach (string item in SelectedData)
                fields.Add(item);
            foreach (string str in fields)
                MoveItemLeft(str);
        }

        public void ShiftRight()
        {
            List<ReportPair> pairs = new List<ReportPair>();

            // Cannot operate against selectedGrid in the foreach loop
            foreach (ReportPair pair in SelectedGrid)
                pairs.Add(pair);
            foreach (ReportPair pair in pairs)
                MoveItemRight(pair);
        }
        #endregion

        #region Properties

        private ReportTypePackage package;
        public ReportTypePackage Package
        {
            get
            {
                return package;
            }
            set
            {
                if (IsSimpleMode == System.Windows.Visibility.Visible)
                {
                    if (package != null)
                        package.ReConstructSQLFromGridView();

                    OnPropertyChanged("OrderByClause");
                }
                // We are in the the Grid Mode moving into the Edit mode
                // We need to reconstruct the SQL for any changes made to the 
                // reported fields list.
                else
                {
                    if (package != null)
                        package.ReConstructPackageListFromSql(null);
                    if(value != null)
                        value.ReConstructSQLFromGridView();
                }

                // They were generating a new invoiceGroup
                if (CreatingNew)
                    return;
                package = value;
                if (comparePackage == null || (package != null && comparePackage.Id != package.Id))
                {
                    package.ReConstructPackageListFromSql(null);
                    comparePackage = new ReportTypePackage(package);
                }

                OnPropertyChanged("Package");
                OnPropertyChanged("OrderByClause");
            }
        }

        /// <summary>
        /// If Visible, then show field selections
        /// </summary>
        private System.Windows.Visibility isSimpleMode;
        public System.Windows.Visibility IsSimpleMode
        {
            get
            {
                return isSimpleMode;
            }
            set
            {
                isSimpleMode = value;
                OnPropertyChanged("IsSimpleMode");
            }
        }


        /// <summary>
        /// If Edit, then show field selections
        /// </summary>
        public System.Windows.Visibility IsEditMode
        {
            get
            {
                if (IsSimpleMode == System.Windows.Visibility.Visible)
                    return System.Windows.Visibility.Hidden;
                else
                    return System.Windows.Visibility.Visible;
            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        private IInvoiceDetailRepositoryBase Target
        {
            get;
            set;
        }

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

        private ReportTypeTable table;
        public ReportTypeTable Table
        {
            get
            {
                return table;
            }
        }

        public ObservableCollection<object> SelectedGrid { get; set; }

        public ObservableCollection<object> SelectedData { get; set; }
      
        public string OrderByClause
        {
            get
            {
                if (Package == null)
                    return null;
                return Package.OrderByClause;
            }
            set
            {
                Package.OrderByClause = value;
                OnPropertyChanged("OrderByClause");
            }

        }
        #endregion

        #region Methods
        public void Save()
        {
            if (HasErrorVM == null)
            {
                if (package.Description == newDescription)
                {
                    MessageBox.Show("Invalid Description name.");
                    return;
                }

                CreatingNew = false;
                Target.Add(Package);
                Modified = false;
            }
        }

        public void NoSave()
        {
            Modified = false;
            // If Newly created and we want to not save it - remove
            // it from the table
            if (CreatingNew)
            {
                CreatingNew = false;
                Table.Remove(Package);
                Package = comparePackage;
            }
            else
                Table.PlacePackage(Package.Description, comparePackage);
        }

        public void Cancel()
        {
//            CreatingNew = 
//            Package = comparePackage;
        }

        public void CreateEmptyPackage()
        {
            comparePackage = new ReportTypePackage(Package);// Keep it dirty
            package = new ReportTypePackage(Package);
            package.Description = newDescription;

            if (Table.IsDuplicate(package))
            {
                System.Windows.MessageBox.Show("This type already exists");
                return;
            }

            Table.Add(package);
            package.Id = 0;
            package.Sql = "Select INVOICE_ID AS \"Invoice\" from SPHRSBILLING.CSV_DETAILS WHERE INVOICE_ID = :I_INVOICE_ID";
            package.ReConstructPackageListFromSql(null);
            OnPropertyChanged("Package");
            Modified = true;
       }

        public void RemoveType()
        {

            if (CreatingNew)
            {
                NoSave();
                return;
            }
            Modified = false;

            int id = Package.Id;

            int index = Table.IndexOf(package);

            // Can't remove last table. :(
            if (index == 0 && Table.Count == 1)
                return;

            if (index == 0)
                Package = Table[index + 1];
            else if (index == Table.Count - 1)
                Package = Table[index - 1];
            ReportTypePackage toRemove = Table.GetPackage(id);
            
            Table.Remove(toRemove);

            Target.Delete(toRemove);
            Modified = false;

        }

        public void NewType()
        {
            CreatingNew = true;
            CreateEmptyPackage();
        }

        public void SwapMode()
        {
            // We are in the Edit Mode - Moving to the Grid View - 
            // Rebuild the Sql from the Grid List
            // and display the Editable SQL statement
            if (IsSimpleMode == System.Windows.Visibility.Visible)
            {
                package.ReConstructSQLFromGridView();
                IsSimpleMode = System.Windows.Visibility.Hidden;
                Package = package;
            }
            // We are in the the Grid Mode moving into the Edit mode
            // We need to reconstruct the SQL for any changes made to the 
            // reported fields list.
            else
            {
                package.ReConstructPackageListFromSql(null);
                IsSimpleMode = System.Windows.Visibility.Visible;
            }
            OnPropertyChanged("OrderByClause");
            OnPropertyChanged("Package");
        }
        #endregion

        #region Commands

        public ICommand ShiftLeftCommand
        {
            private set;
            get;
        }

        public ICommand ShiftRightCommand
        {
            private set;
            get;
        }

        public ICommand SQLCommand
        {
            private set;
            get;
        }

        public ICommand NewTypeCommand
        {
            private set;
            get;
        }

        public ICommand RemoveTypeCommand
        {
            private set;
            get;
        }

        /// <summary>
        /// Save Command handler
        /// </summary>
        public ICommand SaveCommand
        {
            private get;
            set;
        }

        public ICommand CancelCommand
        {
            private get;
            set;
        }

        public ICommand NoSaveCommand
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

        /// <summary>
        /// Returns true if the Table is valid and can be saved.
        /// </summary>
        public string HasErrorVM
        {
            get
            {
                string hasError = Package.HasError;
                if (comparePackage.Equals(package) == false)
                    Modified = true;
                else
                    Modified = false;
                
                whatthe = hasError;
                if (hasError == null)
                    return null;// "";
                return hasError;
            }
        }

        private string whatthe_;
        public string whatthe
        {
            get
            {
                //whatthe_ = string.Format("{0}", dammit);
                return whatthe_;
            }
            set
            {
                whatthe_ = value;
                OnPropertyChanged("whatthe");
            }
        }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get
            {
                return (Table as IDataErrorInfo).Error;
            }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                if (propertyName == "Table")
                    Status = (Table as IDataErrorInfo)[propertyName];
                else if (propertyName == "Package" && Package != null)
                    Status = (Package as IDataErrorInfo)[propertyName];

                CommandManager.InvalidateRequerySuggested();
                return Status;
            }
        }

        #endregion // IDataErrorInfo Members

    }
}

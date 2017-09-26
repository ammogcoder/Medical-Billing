using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Spheris.Billing.Core.Domain;
using System.ComponentModel;
using System.Windows.Input;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Waf.Applications;
using System.Windows;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows.Forms;
using Spheris.Billing.Data;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// </summary>
    [Export]
    public class ReportLinksViewModel : ViewModel<IReportLinksView>, IDisposable 
    {
        #region Fields
        IReportLinksView thisView;
        object lastView;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        [ImportingConstructor]
        public ReportLinksViewModel(IReportLinksView view, IAccessSettings accessSettings)
            : base(view)
        {
            thisView = view;

            //
            //tatScheduleViewModel.View


            // 
                        
            //           
            //

            JumpToNoValidContract = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView( errNoValidContractViewModel.View) 
            };

            JumpToClientWorkType = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView(errClientWorkTypeViewModel.View)
            };

            JumpToBatchJob = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView(this.batchJobViewModel.View)
            };

            JumpToAuditLog = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView(this.auditLogViewModel.View)
            };

        }

        public void ResetView()
        {
            if (lastView == null)
                lastView = errNoValidContractViewModel.View;
            shellViewModel.RightView = lastView;
        }

        void SetRightView(object view)
        {
            lastView = view;
            shellViewModel.RightView = view;
        }

        #endregion

        #region Properties
    
        private string viewTip = "Select a Report";
        public string ViewTip
        {
            get
            {
                    return viewTip;
            }
            set
            {
                viewTip = value;
                RaisePropertyChanged("ViewTip");
            }
        }

        private bool enablePicker = true;
        public bool EnablePicker
        {
            get
            {
                return enablePicker;
            }
            set
            {
                if (enablePicker != value)
                {
                    enablePicker = value;
                    RaisePropertyChanged("EnablePicker");
                }
            }
        }

        public ReportsViewModel  reportsViewModel {get;set;}

        public ShellViewModel shellViewModel { get; set; }


        /// <summary>
        /// Occurs first time only
        /// </summary>
        private ErrNoValidContractViewModel _errNoValidContractViewModel;
        public ErrNoValidContractViewModel errNoValidContractViewModel 
        { 
            get
            {
                return _errNoValidContractViewModel;
            }
            set
            { 
                _errNoValidContractViewModel = value;
                //SetRightView(_errNoValidContractViewModel);
            }
        }

        //public ReportLinksViewModel reportLinksViewModel { get; set; }

        public ErrClientWorkTypeViewModel errClientWorkTypeViewModel { get; set; }

        public BatchJobViewModel batchJobViewModel { get; set; }

        public AuditLogViewModel auditLogViewModel { get; set; }

        public ICommand JumpToClientWorkType { private get; set; }

        public ICommand JumpToNoValidContract { private get; set; }

        public ICommand JumpToBatchJob { private get; set; }

        public ICommand JumpToAuditLog { private get; set; }

        #endregion


        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}

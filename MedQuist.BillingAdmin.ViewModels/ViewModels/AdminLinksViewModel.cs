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
    public class AdminLinksViewModel : ViewModel<IAdminLinksView>, IDisposable 
    {
        #region Fields
        IAdminLinksView thisView;
        object lastView;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        [ImportingConstructor]
        public AdminLinksViewModel(IAdminLinksView view, IAccessSettings accessSettings)
            : base(view)
        {
            thisView = view;

            //
            //tatScheduleViewModel.View


            // 
                        
            //           
            //

            VolumeAdjustmentCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView( volumeAdjustsmentViewModel.View) 
            };

            TatScheduleCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView(tatScheduleViewModel.View)
            };

             
            PasswordChangeCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView(this.passwordChangeViewModel.View)
            };
             
        }

        public void ResetView()
        {
            if (lastView == null)
                lastView = volumeAdjustsmentViewModel.View;
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


        public VolumeAdjustmentsViewModel volumeAdjustsmentViewModel { get; set; }

        public AdminTatScheduleViewModel tatScheduleViewModel { get; set; }

        public PasswordChangeViewModel passwordChangeViewModel { get; set; }

        public ICommand TatScheduleCommand { private get; set; }

        public ICommand VolumeAdjustmentCommand { private get; set; }

        public ICommand PasswordChangeCommand { private get; set; }

        #endregion


        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}

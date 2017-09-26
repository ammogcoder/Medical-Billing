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
    public class PasswordChangeViewModel : ViewModel<IPasswordChangeView>, IDisposable 
    {
        #region Fields
        IPasswordChangeView thisView;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        [ImportingConstructor]
        public PasswordChangeViewModel(IPasswordChangeView view, IAccessSettings accessSettings)
            : base(view)
        {
            thisView = view;

            /*
            VolumeAdjustmentCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => SetRightView( volumeAdjustsmentViewModel.View) 
            };
            */
        }

        #endregion

        #region Properties
    
        public ReportsViewModel  reportsViewModel {get;set;}

        public ICommand VolumeAdjustmentCommand { private get; set; }

        #endregion


        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}

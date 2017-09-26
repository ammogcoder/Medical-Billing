using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for Reports Section
    /// </summary>
    [Export]
    public class ReportsViewModel : ViewModel<IReportsView>, IDisposable
    {

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ReportsNote></param>
        [ImportingConstructor]
        public ReportsViewModel(IReportsView view)
            : base(view)
        {
        }
        #endregion

        public ReportLinksViewModel reportLinksViewModel { get; set; }

        #region Data Target

        #endregion

        private bool isValid = true;
        public bool IsValid
        {
            get { return isValid; }
            set
            {
                if (isValid != value)
                {
                    isValid = value;
                    RaisePropertyChanged("IsValid");
                }
            }
        }




        public void Dispose()
        {
        }
    }
}
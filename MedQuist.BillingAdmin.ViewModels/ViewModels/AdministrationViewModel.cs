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
    /// ViewModel for Administration Section
    /// </summary>
    [Export]
    public class AdministrationViewModel : ViewModel<IAdministrationView>, IDisposable
    {

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<AdministrationNote></param>
        [ImportingConstructor]
        public AdministrationViewModel(IAdministrationView view)
            : base(view)
        {
        }
        #endregion

        #region Properties


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
        #endregion

        private object tatScheduleView;
        public object TatScheduleView
        {
            get
            {
                return tatScheduleView;
            }
            set
            {
                tatScheduleView = value;
                RaisePropertyChanged("TatScheduleView");
            }
        }

        private object volumeAdjustmentsView;
        public object VolumeAdjustsmentView
        {
            get
            {
                return volumeAdjustmentsView;
            }
            set
            {
                volumeAdjustmentsView = value;
                RaisePropertyChanged("VolumeAdjustsmentView");
            }
        }


        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for VolumeAdj Section
    /// </summary>
    [Export]
    public class VolumeAdjViewModel : ViewModel<IVolumeAdjView>, IDisposable
    {

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<VolumeAdjNote></param>
        [ImportingConstructor]
        public VolumeAdjViewModel(IVolumeAdjView view)
            : base(view)
        {
        }
        #endregion
        public ContractListViewModel contractListViewModel
        { get; set; }


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
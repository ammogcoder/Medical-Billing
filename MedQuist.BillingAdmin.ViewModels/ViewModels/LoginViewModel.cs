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
    /// ViewModel for Login Section
    /// </summary>
    [Export]
    public class LoginViewModel : ViewModel<ILoginView>, IDisposable
    {

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<LoginNote></param>
        [ImportingConstructor]
        public LoginViewModel(ILoginView view)
            : base(view)
        {
        }
        #endregion


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
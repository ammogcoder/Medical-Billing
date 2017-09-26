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
    /// ViewModel for ClientDef Section
    /// </summary>
    [Export]
    public class ClientDefViewModel : ViewModel<IClientDefView>, IDisposable
    {

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ClientDefNote></param>
        [ImportingConstructor]
        public ClientDefViewModel(IClientDefView view)
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

        private object clientDefDetailsTab;
        public object ClientDefDetailsTab
        {
            get
            {
                return clientDefDetailsTab;
            }
            set
            {
                clientDefDetailsTab = value;
                RaisePropertyChanged("ClientDefDetailsTab");
            }
        }

        private object clientDefWorkTypeTab;
        public object ClientDefWorkTypeTab
        {
            get
            {
                return clientDefWorkTypeTab;
            }
            set
            {
                clientDefWorkTypeTab = value;
                RaisePropertyChanged("ClientDefWorkTypeTab");
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}
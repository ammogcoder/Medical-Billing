using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;

namespace MedQuist.ViewModels.Views
{
    public interface IClientDefWorkTypeView : IView
    {
        void SearchInvoicesByContract(decimal? contractId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;

namespace MedQuist.ViewModels.Views
{
    public interface IAdminTatScheduleView : IView
    {
        void Pop();
        void SetHeader(string header);
    }
}

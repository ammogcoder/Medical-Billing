using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;


namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// Manual Raise this property when changed
    /// </summary>
    public class SectionPair 
    {
        public IView LeftView;
        public IView RightView;
        public string MyTitle { get;set;}

        public SectionPair(IView left, IView right,string myTitle)
        {
            LeftView = left;
            RightView = right;
            MyTitle = myTitle;
        }

    }
}

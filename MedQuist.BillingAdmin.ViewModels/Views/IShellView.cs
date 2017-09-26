using System.Waf.Applications;
using System.ComponentModel.Composition;
using System.Windows;

namespace MedQuist.BillingAdmin.ViewModels.Views
{
    public interface IShellView : IView
    {
        void Show();
        //Window ShellWindow {get;}
        void Close();
    }
}

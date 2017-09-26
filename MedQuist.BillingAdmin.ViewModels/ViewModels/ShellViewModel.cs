using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
using MedQuist.BillingAdmin.ViewModels.Views;

using MedQuist.BillingAdmin.ViewModels;
using Spheris.Billing.Core.Domain;

using System.Collections.ObjectModel;
using System.ComponentModel;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using Spheris.Billing.Data;


namespace MedQuist.BillingAdmin.ViewModels
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : ViewModel<IShellView>
    {
        string userName;
        string dataSource;
        AssemblyName activeAssembly;

        #region ctor
        [ImportingConstructor]
        public ShellViewModel(IShellView view, IAccessSettings accessSettings)
            : base(view)
        {
            Sections = new ObservableCollection<SectionPair>();
            exitCommand = new DelegateCommand(Close);

            string output;
            IFreqRepository freqsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateFreqRepository();
#if ORACLE
            freqsTarget.ConnectionString.Segments.TryGetValue("User Id", out userName);
            freqsTarget.ConnectionString.Segments.TryGetValue("Data Source", out dataSource);
#else
            userName = "Walt";
            dataSource = "dbTest";
#endif
            activeAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            AppTitle = "Administration - MedQuist BillingAdmin - " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - " +
                userName.ToUpper() + "@" + dataSource + ".COM";
        }
        #endregion

        IAccessSettings AccessSettings { get; set; }

        #region Data Target

        #endregion

        private SectionPair selectedSection;
        public SectionPair SelectedSection
        {
            get
            {
                return selectedSection;
            }
            set
            {
                if (selectedSection != value)
                {
                    selectedSection = value;
                    if(selectedSection == null)
                    {
                        RightView = LeftView = null;
                    }
                    else
                    {
                        LeftView = (object)selectedSection.LeftView;
                        RightView = (object)selectedSection.RightView;
                    }

                    RaisePropertyChanged("SelectedSection");
                }
            }
        }

        private ObservableCollection<SectionPair> sections;
        public ObservableCollection<SectionPair> Sections
        {
            get
            {
                return sections;
            }
            set
            {
                if (sections != value)
                {
                    sections = value;
                    RaisePropertyChanged("Sections");
                }
            }
        }


        private object rightView;
        public object RightView
        {
            get
            {
                return rightView;
            }
            set
            {
                if (rightView != value)
                {
                    rightView = value;
                    RaisePropertyChanged("RightView");
                }
            }
        }

        private object leftView;
        public object LeftView
        {
            get
            {
                return leftView;
            }
            set
            {
                if (leftView != value)
                {
                    leftView = value;
                    RaisePropertyChanged("LeftView");
                }
            }
        }


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

        private double listWidth = 275;
        public double ListWidth
        {
            get
            {
                return listWidth;
            }
            set
            {
                listWidth = value;
                RaisePropertyChanged("ListWidth");
            }
        }

        private readonly DelegateCommand exitCommand;
        public ICommand ExitCommand 
        { 
            get 
            { 
                return exitCommand; 
            } 
        }

        #region Show the window
        public void Show()
        {
            SelectedSection = Sections[0];//Client Def default
            //Login screen first 
            //SelectedSection = Sections[0];
            //L/efv
            ViewCore.Show();
        }

        public void Close()
        {
            ViewCore.Close();
        }

        string appTitle;
        public string AppTitle
        {
            get
            {
                return appTitle;
            }
            set
            {
                appTitle = value;
                RaisePropertyChanged("AppTitle");
            }
        }

        #endregion
    }
}

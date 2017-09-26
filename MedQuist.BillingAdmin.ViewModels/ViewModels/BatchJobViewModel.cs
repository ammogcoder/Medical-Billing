using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;
//using Spheris.Billing;
using Spheris.Billing.Core.Domain;
using System.Windows.Input;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for BatchJob Section
    /// </summary>
    [Export]
    public class BatchJobViewModel : ViewModel<IBatchJobView>, IDisposable
    {
        private BatchJobType allJobs = new BatchJobType { BATCH_JOB_TYPE = string.Empty, DESCR = "*" };
        private IBatchJobView View;

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<BatchJobNote></param>
        [ImportingConstructor]
        public BatchJobViewModel(IBatchJobView view)
            : base(view)
        {
            View = view;
            BatchJobTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateBatchJobTypeRepository();
            BatchJobTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateBatchJobRepository();

            BatchJobTypes = BatchJobTypeTarget.GetBatchJobTypes();
            if(BatchJobTypes != null)
            BatchJobTypes.Insert(0, allJobs);

            SelectedBatchJobType = allJobs;

            RefreshCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => Refresh()
            };


        }
        #endregion


        #region Properties

        #region Data Targets
        public IBatchJobTypeRepository BatchJobTypeTarget { get; set; }
        public IBatchJobRepository BatchJobTarget { get; set; }

        #endregion

        private ObservableCollection<BatchJobType> batchJobTypes;
        public ObservableCollection<BatchJobType> BatchJobTypes
        {
            get
            {
                return batchJobTypes;
            }
            set
            {
                batchJobTypes = value;
                RaisePropertyChanged("BatchJobTypes");
            }
        }

        void MatchItemToType(BatchJob bj)
        {
            foreach (BatchJobType bjt in BatchJobTypes)
            {
                if (bjt.BATCH_JOB_TYPE == bj.BATCH_JOB_TYPE)
                {
                    bj.BatchJobTypeDescription = bjt.DESCR;
                }
            }
        
        }


        void SetBatchJobsDetails()
        {
            BatchJobs.ForEach((thisList, thisItem) => MatchItemToType(thisItem));
        }

        private ObservableCollection<BatchJob> batchJobs;
        public ObservableCollection<BatchJob> BatchJobs
        {
            get
            {
                return batchJobs;
            }
            set
            {
                batchJobs = value;
                SetBatchJobsDetails();
                RaisePropertyChanged("BatchJobs");
            }
        }

        private BatchJob selectedBatchJob;
        public BatchJob SelectedBatchJob
        {
            get
            {
                return selectedBatchJob;
            }
            set
            {
                selectedBatchJob = value;
                RaisePropertyChanged("SelectedBatchJob");
            }
        }

        void Refresh()
        {
            try
            {
                BatchJobs = BatchJobTarget.GetByFilter(FromDate, ThroughDate, SelectedBatchJobType.BATCH_JOB_TYPE);
            }
            catch (Exception x)
            {
                View.ShowMsg(x.ToString());
            }
        }

        private  bool showAllDatesCommand;
        public bool ShowAllDatesCommand
        {
            get
            {
                return showAllDatesCommand;
            }
            set
            {
                showAllDatesCommand = value;
                
                if (showAllDatesCommand == true)
                {
                    FromDate = null;
                    ThroughDate = null;
                }

                RaisePropertyChanged("ShowAllDatesCommand");
            }
        }

        public ICommand RefreshCommand
        {
            private get;
            set;
        }


        private BatchJobType selectedBatchJobType;
        public BatchJobType SelectedBatchJobType
        {
            get
            {
                return selectedBatchJobType;
            }
            set
            {
                selectedBatchJobType = value;
                RaisePropertyChanged("SelectedBatchJobType");
            }
        }

        private DateTime? throughDate = DateTime.Now;
        public DateTime? ThroughDate
        {
            get
            {
                return throughDate;
            }
            set
            {
                throughDate = value;
                RaisePropertyChanged("ThroughDate");
            }
        }

        private DateTime? fromDate = DateTime.Now.AddDays(-60);
        public DateTime? FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
                RaisePropertyChanged("FromDate");
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

        #endregion


        #region Dispose
        public void Dispose()
        {
        }
        #endregion

    }
}
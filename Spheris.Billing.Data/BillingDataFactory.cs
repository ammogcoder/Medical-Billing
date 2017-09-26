using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{
    public abstract class BillingDataFactory
    {
        private static BillingDataFactory _currentInstance;

        public static BillingDataFactory NewInstance()
        {
            if (_currentInstance == null)
            {
                // If we ever create a SQL Server provider, this could be hard-coded to avoid reflection once in production and possibly get some performance gains.
                string providerType = "Spheris.Billing.Data.OracleData.OracleDataFactory, Spheris.Billing.Data.OracleData";  //ConfigurationSettings.AppSettings["dataProviderType"];
                Type type = Type.GetType(providerType, true, true);
                _currentInstance = (BillingDataFactory)Activator.CreateInstance(type);
            }
            return _currentInstance;
        }

        public abstract BatchJobRepository CreateBatchJobRepository();
        public abstract InvoiceDetailFileTypeDal CreateInvoiceDetailFileTypeDal();
        public abstract UserDal CreateUserDal();
        public abstract AuditLogDal CreateAuditLogDal();
        public abstract IContractNoteRepository CreateContractNoteRepository();
        public abstract IInvoiceReportAddOnChargesQuery CreateInvoiceReportAddOnChargesQuery();
        public abstract IInvoiceReportTranscriptionLinesQuery CreateInvoiceReportTranscriptionLinesQuery();
        public abstract IInvoiceStyleColumnRepository CreateInvoiceStyleColumnRepository();
        public abstract IInvoiceReportParametersQuery CreateInvoiceReportParametersQuery();
        public abstract IInvoiceDetailReportTypeQuery CreateInvoiceDetailReportTypeQuery();
        public abstract IInvoiceGroupReportsInfoQuery CreateInvoiceGroupReportsInfoQuery();
        public abstract IWorkUnitRepository CreateWorkUnitRepository();
        public abstract IExtClientRepository CreateExtClientRepository();
        public abstract IExtWorkTypeRepository CreateExtWorkTypeRepository();

        public abstract IInvoiceDetailRepositoryBase CreateInvoiceDetailRepository();
        public abstract IInvoiceGroupRepository CreateInvoiceGroupRepository();
        public abstract IBillingSpecialistRepository CreateBillingSpecialistRepository();
        public abstract IDeliveryMethods CreateDeliveryMethodsRepository();
        public abstract IFreqRepository CreateFreqRepository();
        public abstract IPlatformRepository CreatePlatformRepository();
        public abstract IBrandsRepository CreateBrandRepository();
        public abstract IReportTypeRepository CreateReportTypeRepository();
        public abstract IInvoiceGrpReportRepository CreateInvoiceGrpReportRepository();
        public abstract IRemitToRepository CreateRemitToRepository();
        public abstract IInvoiceStyleRepository CreateInvoiceStyleRepository();
        public abstract IInvoiceStatusRepository CreateInvoiceStatusRepository();
        public abstract IInvoiceGrpStatusRepository CreateInvoiceGrpStatusRepository();
        public abstract IContractRepository CreateContractRepository();
        public abstract IPaymentTermsRepository CreatePaymentTermsRepository();
        public abstract IContractRateRepository CreateContractRateRepository();
        public abstract IContractRateAltRepository CreateContractRateAltRepository();
        public abstract IChargeMethodRepository CreateChargeMethodRepository();
        public abstract IStatCompMethodRepository CreateStatCompMethodRepository();
        public abstract IFaxCompMethodRepository CreateFaxCompMethodRepository();
        public abstract IExtSysRepository CreateExtSysRepository();
        public abstract IOverRideKeySourceRepository CreateOverRideKeySourceRepository();
        public abstract IExtWorkTypeSourceRepository CreateExtWorkTypeSourceRepository();
        public abstract IBatchJobTypeRepository CreateBatchJobTypeRepository();
        public abstract IErrNoValidContractRepository CreateErrNoValidContractTypeRepository();
        public abstract IErrClientErrorTypeRepository CreateErrClientErrorRepository();
        public abstract IErrClientWorkTypeRepository CreateErrClientWorkTypeRepository();
        public abstract ITatCompMethodRepository CreateTatCompMethodRepository();
        public abstract ITatSchedRepository CreateTatSchedRepository();
        public abstract ITatRateRepository CreateTatRateRepository();
        public abstract IContractTatSchedRepository CreateContractTatSchedRepository();

        public abstract IContractVolumeEvtRepository CreateContractVolumeEvtRepository();
        public abstract IVolumeEvtRateRepository CreateVolumeEvtRateRepository();
        public abstract IVolumeEvtTypeRepository CreateVolumeEvtTypeRepository();

        public abstract IAddOnChgTypeRepository CreateAddOnChgTypeRepository();
        public abstract IAdjTypeRepository CreateAdjTypeRepository();
        public abstract IScopeRuleRepository CreateScopeRuleRepository();
        public abstract IAddOnChgSchedRepository CreateAddOnChgSchedRepository();

        public abstract IQtyRuleRepository CreateQtyRuleRepository();
        public abstract IActiveScheduleRepository CreateActiveScheduleRepository();
        public abstract IInvoiceGrpOverrideRepository CreateInvoiceGrpOverrideRepository();
        public abstract IInvoiceRepository CreateInvoiceRepository();
        
    }
}

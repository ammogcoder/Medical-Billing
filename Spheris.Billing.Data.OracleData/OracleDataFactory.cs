using Spheris.Billing.Data.RepositoryBases;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleDataFactory : BillingDataFactory
    {
        public override BatchJobRepository CreateBatchJobRepository()
        {
            return new OracleBatchJobRepository();
        }

        public override InvoiceDetailFileTypeDal CreateInvoiceDetailFileTypeDal()
        {
            return new OracleInvoiceDetailFileTypeDal();
        }

        public override UserDal CreateUserDal()
        {
            return new OracleUserDal();
        }

        public override AuditLogDal CreateAuditLogDal()
        {
            return new OracleAuditLogDal();
        }

        public override IContractNoteRepository CreateContractNoteRepository()
        {
            return new OracleContractNoteRepository();
        }

        public override IInvoiceReportAddOnChargesQuery CreateInvoiceReportAddOnChargesQuery()
        {
            return new OracleInvoiceReportAddOnChargesQuery();
        }

        public override IInvoiceReportTranscriptionLinesQuery CreateInvoiceReportTranscriptionLinesQuery()
        {
            return new OracleInvoiceReportTranscriptionLinesQuery();
        }

        public override IInvoiceStyleColumnRepository CreateInvoiceStyleColumnRepository()
        {
            return new OracleInvoiceStyleColumnRepository();
        }

        public override IInvoiceReportParametersQuery CreateInvoiceReportParametersQuery()
        {
            return new OracleInvoiceReportParametersQuery();
        }

        public override IInvoiceDetailReportTypeQuery CreateInvoiceDetailReportTypeQuery()
        {
            return new OracleInvoiceDetailReportTypeQuery();
        }

        public override IInvoiceGroupReportsInfoQuery CreateInvoiceGroupReportsInfoQuery()
        {
            return new OracleInvoiceGroupReportsInfoQuery();
        }

        public override IWorkUnitRepository CreateWorkUnitRepository()
        {
            return new OracleWorkUnitRepository();
        }

        public override IExtClientRepository CreateExtClientRepository()
        {
            return new OracleClientLocationRepository();
        }

        public override IInvoiceDetailRepositoryBase CreateInvoiceDetailRepository()
        {
            return new OracleInvoiceDetailRepository();
        }

        public override IInvoiceGroupRepository CreateInvoiceGroupRepository()
        {
            return new OracleInvoiceGroupRepository();
        }

        public override IBillingSpecialistRepository CreateBillingSpecialistRepository()
        {
            return new OracleBillingSpecialistRepository();
        }

        public override IDeliveryMethods CreateDeliveryMethodsRepository()
        {
            return new OracleDeliveryMethodsRepositoryBase();
        }

        public override IFreqRepository CreateFreqRepository()
        {
            return new OracleFreqRepository();
        }

        public override IPlatformRepository CreatePlatformRepository()
        {
            return new OraclePlatformRepository();
        }

        public override IBrandsRepository CreateBrandRepository()
        {
            return new OracleBrandRepository();
        }

        public override IReportTypeRepository CreateReportTypeRepository()
        {
            return new OracleReportTypeRepository();
        }

        public override IInvoiceGrpReportRepository CreateInvoiceGrpReportRepository()
        {
            return new OracleInvoiceGrpReportRepository();
        }

        public override IRemitToRepository CreateRemitToRepository()
        {
            return new OracleRemitToRepository();
        }

        public override IInvoiceStyleRepository CreateInvoiceStyleRepository()
        {
            return new OracleInvoiceStyleRepository();
        }

        public override IInvoiceStatusRepository CreateInvoiceStatusRepository()
        {
            return new OracleInvoiceStatusRepository();
        }
 
        public override IInvoiceGrpStatusRepository CreateInvoiceGrpStatusRepository()
        {
            return new OracleInvoiceGrpStatusRepository();
        }

        public override IContractRepository CreateContractRepository()
        {
            return new OracleContractRepository();
        }

        public override IPaymentTermsRepository CreatePaymentTermsRepository()
        {
            return new OraclePaymentTermsRepository();
        }

        public override IContractRateRepository CreateContractRateRepository()
        {
            return new OracleContractRateRepository();
        }

        public override IContractRateAltRepository CreateContractRateAltRepository()
        {
            return new OracleContractRateAltRepository();
        }

        public override IChargeMethodRepository CreateChargeMethodRepository()
        {
            return new OracleChargeMethodRepository();
        }

        public override IStatCompMethodRepository CreateStatCompMethodRepository()
        {
            return new OracleStatCompMethodRepository();
        }

        public override IFaxCompMethodRepository CreateFaxCompMethodRepository()
        {
            return new OracleFaxCompMethodRepository();
        }

        public override IExtSysRepository CreateExtSysRepository()
        {
            return new OracleExtSysRepository();
        }

        public override IExtWorkTypeRepository CreateExtWorkTypeRepository()
        {
            return new OracleExtWorkTypeRepository();
        }

        public override IOverRideKeySourceRepository CreateOverRideKeySourceRepository()
        {
            return new OracleOverRideKeySourceRepository();
        }

        public override IExtWorkTypeSourceRepository CreateExtWorkTypeSourceRepository()
        {
            return new OracleExtWorkTypeSourceRepository();
        }

        public override IBatchJobTypeRepository CreateBatchJobTypeRepository()
        {
            return new OracleBatchJobTypeRepository();
        }

        public override IErrNoValidContractRepository CreateErrNoValidContractTypeRepository()
        {
            return new OracleErrNoValidContractRepository();
        }

        public override IErrClientErrorTypeRepository CreateErrClientErrorRepository()
        {
            return new OracleClientErrorTypeRepository();
        }

        public override IErrClientWorkTypeRepository CreateErrClientWorkTypeRepository()
        {
            return new OracleErrClientWorkTypeRepository();
        }

        public override ITatCompMethodRepository CreateTatCompMethodRepository()
        {
            return new OracleTatCompMethodRepository();
        }

        public override ITatSchedRepository CreateTatSchedRepository()
        {
            return new OracleTatSchedRepository();        
        }

        public override ITatRateRepository CreateTatRateRepository()
        {
            return new OracleTatRateRepository();
        }

        public override IContractTatSchedRepository CreateContractTatSchedRepository()
        {
            return new OracleContractTatSchedRepository();
        }

        public override IContractVolumeEvtRepository CreateContractVolumeEvtRepository()
        {
            return new OracleContractVolumeEvtRepository();
        }

        public override IVolumeEvtRateRepository CreateVolumeEvtRateRepository()
        {
            return new OracleVolumeEvtRateRepository();
        }

        public override IVolumeEvtTypeRepository CreateVolumeEvtTypeRepository()
        {
            return new OracleVolumeEvtTypeRepository();
        }



        public override IAddOnChgTypeRepository CreateAddOnChgTypeRepository()
        {
            return new OracleAddOnChgTypeRepository();
        }

        public override IAdjTypeRepository CreateAdjTypeRepository()
        {
            return new OracleAdjTypeRepository();
        }

        public override IScopeRuleRepository CreateScopeRuleRepository()
        {
            return new OracleScopeRuleRepository();
        }

        public override IAddOnChgSchedRepository CreateAddOnChgSchedRepository()
        {
            return new OracleAddOnChgSchedRepository();
        }

        public override IQtyRuleRepository CreateQtyRuleRepository()
        {
            return new OracleQtyRuleRepository();
        }

        public override IActiveScheduleRepository CreateActiveScheduleRepository()
        {
            return new OracleActiveScheduleRepository();
        }

        public override IInvoiceGrpOverrideRepository CreateInvoiceGrpOverrideRepository()
        {
            return new OracleInvoiceGrpOverrideRepository();
        }
    
        public override IInvoiceRepository CreateInvoiceRepository()
        {
            return new OracleInvoiceRepository();
        }
    }
}

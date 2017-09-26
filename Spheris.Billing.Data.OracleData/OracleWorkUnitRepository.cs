using Spheris.Common;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleWorkUnitRepository : WorkUnitRepository
    {
        public override IDbConnection OpenConnection(IDbConnection cnn = null)
        {
            if (cnn == null) cnn = new OracleConnection(ConnectionString.Value);
            base.OpenConnection(cnn);
            return cnn;
        }

        public override IDataReader Get(IDbConnection cnn, string platform, DateRange dateRange, string siteID = "")
        {

            string siteFilter = "";
            if (!String.IsNullOrEmpty(siteID))
            {
                siteFilter = String.Format("  AND   EXT_CLIENT_KEY in ('{0}') \r\n", siteID);         // TODO: If list then make sure single-quotes surround each number.
            }
            string sql = String.Format("SELECT  * \r\n"
                                       + "FROM    {0}.WORK_UNIT \r\n"
                                       + "WHERE   EXT_SYS = :EXT_SYS \r\n"
                                       + "  AND   TAT_ENDS_ON >= :LOWER_BOUND \r\n"
                                       + "  AND   TAT_ENDS_ON < :UPPER_BOUND \r\n"
                                       + "{1}"
                                       + "ORDER BY  cast(EXT_WORK_UNIT_KEY as number)", SchemaName, siteFilter);

            if (cnn == null) cnn = new OracleConnection(ConnectionString.Value);
            OpenConnection(cnn);
            using (OracleCommand cmd = new OracleCommand(sql, (OracleConnection)cnn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60 * 60;
                OracleHelper.AddCommandParameter(cmd, ":EXT_SYS", platform, OracleType.VarChar, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":LOWER_BOUND", dateRange.BeginsOn, OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":UPPER_BOUND", dateRange.EndsBefore, OracleType.DateTime, ParameterDirection.Input);
                return cmd.ExecuteReader();
            }
        }

        protected override void RowConverter(WorkUnit item, DataRow row)
        {
            throw new NotImplementedException();
        }

        public override WorkUnit RowConverter(IDataRecord row)
        {
            WorkUnit item = new WorkUnit();
            item.WorkUnitID = CastDbValue(row, "work_unit_id", true, "Int64");
            item.ExtSys = CastDbValue(row, "ext_sys");
            item.ExtWorkUnitKey = CastDbValue(row, "ext_work_unit_key", true, "String");
            item.ExtClientKey = CastDbValue(row, "ext_client_key", true, "String");
            item.ExtWorkType = CastDbValue(row, "ext_work_type", true, "String");
            item.WorkUnitStatus = CastDbValue(row, "work_unit_status");
            item.InvoiceID = CastDbValue(row, "invoice_id");
            item.ReverseRequestedOn = CastDbValue(row, "reverse_requested_on");
            item.ReverseRequestedBy = CastDbValue(row, "reverse_requested_by");
            item.ReversalReason = CastDbValue(row, "reversal_reason");
            item.ReversedOnInvoiceID = CastDbValue(row, "reversed_on_invoice_id");
            item.JobNbr = CastDbValue(row, "job_nbr", true, "String");
            item.Urgency = CastDbValue(row, "urgency");
            item.AttendingID = CastDbValue(row, "attending_id");
            item.AttendingName = CastDbValue(row, "attending_name");
            item.AttendingSpecialty = CastDbValue(row, "attending_specialty");
            item.MTID = CastDbValue(row, "mt_id");
            item.MTName = CastDbValue(row, "mt_name");
            item.DictatorID = CastDbValue(row, "dictator_id", true, "String");
            item.DictatorName = CastDbValue(row, "dictator_name");
            item.DictatorSpecialty = CastDbValue(row, "dictator_specialty");
            item.DictationSeconds = CastDbValue(row, "dictation_seconds");
            item.TatBeginsOn = CastDbValue(row, "tat_begins_on");
            item.TatEndsOn = CastDbValue(row, "tat_ends_on");
            item.PatientID = CastDbValue(row, "patient_id", true, "String");
            item.CharsTotal = CastDbValue(row, "chars_total", true, "Int32");
            item.CharsNoSpaces = CastDbValue(row, "chars_nospaces", true, "Int32");
            item.CharsMT = CastDbValue(row, "chars_mt");
            item.QtyBillable = CastDbValue(row, "qty_billable");
            item.LinesGross = CastDbValue(row, "lines_gross");
            item.Words = CastDbValue(row, "words");
            item.Paragraphs = CastDbValue(row, "paragraphs");
            item.Pages = CastDbValue(row, "pages");
            item.RevisionNbr = CastDbValue(row, "revision_nbr");
            item.HostInterfaceAnchor = CastDbValue(row, "host_interface_anchor");
            item.Status = CastDbValue(row, "status");
            item.StatusChangedOn = CastDbValue(row, "status_changed_on");
            item.DictationID = CastDbValue(row, "dictation_id", true, "String");
            item.Department = CastDbValue(row, "department");
            item.CustomerAcctNbr = CastDbValue(row, "customer_acct_nbr");
            item.InvoiceGrpOverrideKey = CastDbValue(row, "invoice_grp_override_key", true, "String");
            item.InsertedOn = CastDbValue(row, "inserted_on");
            item.RawCost = CastDbValue(row, "raw_cost");
            item.TatReductionCalculated = CastDbValue(row, "tat_reduction_calculated");
            item.TatReductionCharged = CastDbValue(row, "tat_reduction_charged");
            item.BaseRate = CastDbValue(row, "base_rate");
            item.TatRate = CastDbValue(row, "tat_rate");
            item.ClassPctOverTat = CastDbValue(row, "class_pct_over_tat");
            item.ChangedOn = CastDbValue(row, "changed_on");
            item.PatientFirst = CastDbValue(row, "patient_first");
            item.PatientLast = CastDbValue(row, "patient_last");
            item.BillableOn = CastDbValue(row, "billable_on");
            item.ReportID = CastDbValue(row, "report_id");
            item.FirstMTID = CastDbValue(row, "first_mt_id", true, "String");
            item.ParentWorkUnitID = CastDbValue(row, "parent_work_unit_id");
            item.EntryLocation = CastDbValue(row, "entry_loc");
            item.RateApplied = CastDbValue(row, "rate_applied");
            item.TatThresholdApplied = CastDbValue(row, "tat_threshold_applied");
            item.DictatorSRScore = CastDbValue(row, "dictator_sr_score");
            item.DocumentSRScore = CastDbValue(row, "document_sr_score");
            item.ServiceType = CastDbValue(row, "service_type");
            item.ServiceProvider = CastDbValue(row, "service_provider", true, "String");
            item.Custom1 = CastDbValue(row, "custom1", true, "String");
            item.Custom2 = CastDbValue(row, "custom2", true, "String");
            item.Custom3 = CastDbValue(row, "custom3", true, "String");
            item.VirtualLocationID = CastDbValue(row, "virtual_location_id");
            item.LocationName = CastDbValue(row, "location_name");
            item.ClientDicatatorID = CastDbValue(row, "client_dictator_id");
            item.ClientWorkTypeID = CastDbValue(row, "client_worktype_id");
            item.ServiceProviderName = CastDbValue(row, "service_provider_name");
            string val = CastDbValue(row, "dictating_physician_id");
            if (String.IsNullOrEmpty(val))
            {
                item.DictatingPhysicianID = null;
            }
            else
            {
                item.DictatingPhysicianID = int.Parse(val);
            }
            item.PatientCostCenter = CastDbValue(row, "pat_costcenter");
            item.PatientLocation = CastDbValue(row, "pat_location");
            item.PatientRoom = CastDbValue(row, "pat_room");
            item.PrintLocation = CastDbValue(row, "print_location");
            item.ActivityType = CastDbValue(row, "activity_type");
            item.ClinicCode = CastDbValue(row, "clinic_code");
            item.DepCount = CastDbValue(row, "dep_count");
            return item;
        }

        public override bool Add(WorkUnit item)
        {
            using (IDbConnection cnn = OpenConnection())
            {
                /*
                // Check for existance of an existing matching row.
                // TODO:  Figure out how to execute this and the insert in one DB call.
                int cnt = 0;
                using (IDbCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = String.Format(  "SELECT  COUNT(*) AS CNT "
                                                    + "FROM    {0}.WORK_UNIT "
                                                    + "WHERE   EXT_SYS = :EXT_SYS "
                                                    + "  AND   EXT_WORK_UNIT_KEY = :EXT_WORK_UNIT_KEY "
                                                    + "  AND   TAT_ENDS_ON >= :JDATE - 1 "
                                                    + "  AND   TAT_ENDS_ON <  :JDATE + 2"
                                                    , SchemaName);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = (OracleConnection)cnn;
                    OracleHelper.AddCommandParameter(cmd, ":EXT_SYS", item.ExtSys, OracleType.Char, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":EXT_WORK_UNIT_KEY", item.ExtWorkUnitKey, OracleType.VarChar, 60, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":JDATE", item.TatEndsOn, OracleType.DateTime, ParameterDirection.Input);
                    IDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    cnt = int.Parse(dr["CNT"].ToString());
                    dr.Close();
                    dr.Dispose();
                }
                if (cnt > 0)
                {
                    cnn.Close();
                    return false;
                }
                */

                // Add a new row.
                using (IDbCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = String.Format(
                                     "insert into {0}.work_unit( EXT_SYS                    ,EXT_WORK_UNIT_KEY              ,EXT_CLIENT_KEY "
                                    + "                     ,EXT_WORK_TYPE              ,WORK_UNIT_STATUS               ,JOB_NBR "
                                    + "                     ,URGENCY                    ,MT_ID                          ,FIRST_MT_ID "
                                    + "                     ,MT_NAME                    ,DICTATION_ID                   ,DICTATOR_ID "
                                    + "                     ,TAT_BEGINS_ON              ,TAT_ENDS_ON                    ,PATIENT_ID "
                                    + "                     ,CHARS_TOTAL                ,CHARS_NOSPACES "
                                    + "                     ,INVOICE_GRP_OVERRIDE_KEY   ,PATIENT_LAST                   ,ENTRY_LOC "
                                    + "                     ,DICTATOR_NAME              ,DEPARTMENT                     ,CUSTOMER_ACCT_NBR "
                                    + "             		,DICTATOR_SPECIALTY         ,LINES_GROSS            		,DICTATION_SECONDS "
                                    + "             		,SERVICE_PROVIDER           ,VIRTUAL_LOCATION_ID            ,LOCATION_NAME "
                                    + "                     ,CUSTOM1                    ,CUSTOM2                        ,CUSTOM3 "
                                    + "                     ,CLIENT_DICTATOR_ID         ,CLIENT_WORKTYPE_ID             ,SERVICE_PROVIDER_NAME "
                                    + "                     ,DICTATING_PHYSICIAN_ID     ,PAT_COSTCENTER                 ,PAT_LOC "
                                    + "                     ,PAT_ROOM                   ,DICTATOR_SR_SCORE              ,DOCUMENT_SR_SCORE "
                                    + "                     ,INSERTED_ON                ,CHANGED_ON                     ,ACTIVITY_TYPE "
                                    + "                     ,DEP_COUNT ) "
                                    + "values (             :EXT_SYS                    ,:EXT_WORK_UNIT_KEY             ,:EXT_CLIENT_KEY "
                                    + "                     ,:EXT_WORK_TYPE             ,:WORK_UNIT_STATUS              ,:JOB_NBR "
                                    + "                     ,:URGENCY                   ,:FIRST_MT_ID                   ,:FIRST_MT_ID "
                                    + "                     ,:MT_NAME                   ,:DICTATION_ID                  ,:DICTATOR_ID "
                                    + "                     ,:TAT_BEGINS_ON             ,:TAT_ENDS_ON                   ,:PATIENT_ID "
                                    + "                     ,:CHARS_TOTAL               ,:CHARS_NOSPACES "
                                    + "                     ,:INVOICE_GRP_OVERRIDE_KEY  ,:PATIENT_LAST                  ,:ENTRY_LOC "
                                    + "                     ,:DICTATOR_NAME             ,:DEPARTMENT                    ,:CUSTOMER_ACCT_NBR "
                                    + "                     ,:DICTATOR_SPECIALTY        ,:LINES_GROSS                   ,:DICTATION_SECONDS "
                                    + "                     ,:SERVICE_PROVIDER          ,:VIRTUAL_LOCATION_ID           ,:LOCATION_NAME "
                                    + "                     ,:CUSTOM1                   ,:CUSTOM2                       ,:CUSTOM3 "
                                    + "                     ,:CLIENT_DICTATOR_ID        ,:CLIENT_WORKTYPE_ID            ,:SERVICE_PROVIDER_NAME "
                                    + "                     ,:DICTATING_PHYSICIAN_ID    ,:PAT_COSTCENTER                ,:PAT_LOC "
                                    + "                     ,:PAT_ROOM                  ,:DICTATOR_SR_SCORE             ,:DOCUMENT_SR_SCORE "
                                    + "                     ,SYSDATE                    ,SYSDATE                        ,:ACTIVITY_TYPE "
                                    + "                     ,:DEP_COUNT ) "
                                    + "returning work_unit_id into :work_unit_id"
                                    , SchemaName);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = (OracleConnection)cnn;
                    OracleHelper.AddCommandParameter(cmd, ":EXT_SYS", item.ExtSys, OracleType.Char, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":EXT_WORK_UNIT_KEY", item.ExtWorkUnitKey, OracleType.VarChar, 60, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":EXT_CLIENT_KEY", item.ExtClientKey, OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":EXT_WORK_TYPE", item.ExtWorkType, OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":WORK_UNIT_STATUS", item.WorkUnitStatus, OracleType.Char, 2, ParameterDirection.Input);
                    if (item.WorkUnitStatus == null) ((OracleParameter)cmd.Parameters[":WORK_UNIT_STATUS"]).Value = "NT";
                    OracleHelper.AddCommandParameter(cmd, ":JOB_NBR", item.JobNbr, OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":URGENCY", item.Urgency, OracleType.Char, 1, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":FIRST_MT_ID", CheckNull(item.FirstMTID), OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":MT_NAME", CheckNull(item.MTName), OracleType.VarChar, 60, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DICTATION_ID", CheckNull(item.DictationID), OracleType.VarChar, 64, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DICTATOR_ID", CheckNull(item.DictatorID), OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":TAT_BEGINS_ON", item.TatBeginsOn, OracleType.DateTime, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":TAT_ENDS_ON", item.TatEndsOn, OracleType.DateTime, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":PATIENT_ID", CheckNull(item.PatientID), OracleType.VarChar, 40, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CHARS_TOTAL", CheckNull(item.CharsTotal), OracleType.Number, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CHARS_NOSPACES", CheckNull(item.CharsNoSpaces), OracleType.Number, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":INVOICE_GRP_OVERRIDE_KEY", CheckNull(item.InvoiceGrpOverrideKey), OracleType.VarChar, 80, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":PATIENT_LAST", CheckNull(item.PatientLast), OracleType.VarChar, 40, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":ENTRY_LOC", CheckNull(item.EntryLocation), OracleType.Char, 1, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DICTATOR_NAME", CheckNull(item.DictatorName), OracleType.VarChar, 60, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DEPARTMENT", CheckNull(item.Department), OracleType.VarChar, 50, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CUSTOMER_ACCT_NBR", CheckNull(item.CustomerAcctNbr), OracleType.VarChar, 50, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DICTATOR_SPECIALTY", CheckNull(item.DictatorSpecialty), OracleType.VarChar, 64, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":LINES_GROSS", CheckNull(item.LinesGross), OracleType.Number, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DICTATION_SECONDS", CheckNull(item.DictationSeconds), OracleType.Number, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":SERVICE_PROVIDER", CheckNull(item.ServiceProvider), OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":VIRTUAL_LOCATION_ID", CheckNull(item.VirtualLocationID), OracleType.Int32, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":LOCATION_NAME", CheckNull(item.LocationName), OracleType.VarChar, 50, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CUSTOM1", CheckNull(item.Custom1), OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CUSTOM2", CheckNull(item.Custom2), OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CUSTOM3", CheckNull(item.Custom3), OracleType.VarChar, 20, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CLIENT_DICTATOR_ID", CheckNull(item.ClientDicatatorID), OracleType.VarChar, 50, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":CLIENT_WORKTYPE_ID", CheckNull(item.ClientWorkTypeID), OracleType.Int32, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":SERVICE_PROVIDER_NAME", CheckNull(item.ServiceProviderName), OracleType.VarChar, 50, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DICTATING_PHYSICIAN_ID", CheckNull(item.DictatingPhysicianID), OracleType.Int32, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":PAT_COSTCENTER", CheckNull(item.PatientCostCenter), OracleType.VarChar, 255, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":PAT_LOC", CheckNull(item.PatientLocation), OracleType.VarChar, 255, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":PAT_ROOM", CheckNull(item.PatientRoom), OracleType.VarChar, 255, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":WORK_UNIT_ID", OracleType.Number, ParameterDirection.ReturnValue);
                    OracleHelper.AddCommandParameter(cmd, ":DICTATOR_SR_SCORE", CheckNull(item.DictatorSRScore), OracleType.Number, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DOCUMENT_SR_SCORE", CheckNull(item.DocumentSRScore), OracleType.Number, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":ACTIVITY_TYPE", CheckNull(item.ActivityType), OracleType.Char, 2, ParameterDirection.Input);
                    OracleHelper.AddCommandParameter(cmd, ":DEP_COUNT", CheckNull(item.DepCount), OracleType.Number, ParameterDirection.Input);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch(OracleException ex)
                    {
                        if (ex.ErrorCode == -2146232008 && ex.Code == 1)      // record already exists.
                        {
                            return false;
                        }
                        Console.WriteLine("ExtWorkUnitKey = {0}", item.ExtWorkUnitKey);
                        Console.WriteLine("ExtClientKey = {0}", item.ExtClientKey);
                        throw;
                    }
                    finally
                    {
                        cnn.Close();
                    }
                    item.WorkUnitID = long.Parse(((OracleParameter)cmd.Parameters[":work_unit_id"]).Value.ToString());       // Return the InvoiceGrpId of the new record.
                    return (true);
                }
            }
        }

        public override WorkUnit Get(string extSys, string extWorkUnitKey)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(WorkUnit item, DateRange range)
        {
            // If the record has already been billed then it cannot be deleted.
            if (item.InvoiceID > 0)
            {
                return false;
            }

            if (item.WorkUnitID == null) return false;
            return Remove(item.ExtSys, item.ExtWorkUnitKey, range);
        }

        public override bool Remove(string extSys, string extWorkUnitKey, DateRange range)
        {
            using (IDbConnection cnn = OpenConnection())
            using (IDbCommand cmd = new OracleCommand())
            {
                cmd.CommandText = String.Format(
                                  "delete {0}.work_unit \r\n"
                                + "where  tat_ends_on >= :begins_on \r\n"
                                + "  and  tat_ends_on <  :ends_before \r\n"
                                + "  and  ext_sys = :ext_sys \r\n"
                                + "  and  ext_work_unit_key = :ext_work_unit_key \r\n"
                                + "  and  invoice_id is null"
                                , SchemaName);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                OracleHelper.AddCommandParameter(cmd, ":begins_on", range.BeginsOn, OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ends_before", range.EndsBefore, OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_sys", extSys, OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_work_unit_key", extWorkUnitKey, OracleType.VarChar, 60, ParameterDirection.Input);
                int rowsEffected = cmd.ExecuteNonQuery();
                if (rowsEffected == 0) return false;
                cnn.Close();
            }
            return true;
        }

        public override bool Update(WorkUnit item, DateRange range)
        {
            using (IDbConnection cnn = OpenConnection())
            using (IDbCommand cmd = new OracleCommand())
            {
                cmd.CommandText = String.Format(
                                  "UPDATE {0}.WORK_UNIT SET \r\n"
                                + "  EXT_CLIENT_KEY = :EXT_CLIENT_KEY \r\n"
                                + "  ,EXT_WORK_TYPE = :EXT_WORK_TYPE \r\n"
                                + "  ,WORK_UNIT_STATUS = :WORK_UNIT_STATUS \r\n"
                                + "  ,JOB_NBR = :JOB_NBR \r\n"
                                + "  ,URGENCY = :URGENCY \r\n"
                                + "  ,MT_ID = :FIRST_MT_ID \r\n"
                                + "  ,FIRST_MT_ID = :FIRST_MT_ID \r\n"
                                + "  ,MT_NAME = :MT_NAME \r\n"
                                + "  ,DICTATION_ID = :DICTATION_ID "
                                + "  ,DICTATOR_ID = :DICTATOR_ID "
                                + "  ,TAT_BEGINS_ON = :TAT_BEGINS_ON "
                                + "  ,TAT_ENDS_ON = :TAT_ENDS_ON "
                                + "  ,PATIENT_ID = :PATIENT_ID "
                    //+ "  ,CHARS_TOTAL = :CHARS_TOTAL "
                                + "  ,CHARS_NOSPACES = :CHARS_NOSPACES "
                    //+ "  ,INVOICE_GRP_OVERRIDE_KEY = :INVOICE_GRP_OVERRIDE_KEY "
                                + "  ,PATIENT_LAST = :PATIENT_LAST "
                    //+ "  ,ENTRY_LOC = :ENTRY_LOC "
                                + "  ,DICTATOR_NAME = :DICTATOR_NAME "
                                + "  ,DEPARTMENT = :DEPARTMENT "
                    //+ "  ,CUSTOMER_ACCT_NBR = :CUSTOMER_ACCT_NBR "
                                + "  ,DICTATOR_SPECIALTY = :DICTATOR_SPECIALTY \r\n"
                    //+ "  ,LINES_GROSS = :LINES_GROSS "
                                + "  ,DICTATION_SECONDS = :DICTATION_SECONDS "
                                + "  ,DICTATOR_SR_SCORE = :DICTATOR_SR_SCORE "
                                + "  ,DOCUMENT_SR_SCORE = :DOCUMENT_SR_SCORE "
                                + "  ,SERVICE_PROVIDER = :SERVICE_PROVIDER "
                    //+ "  ,VIRTUAL_LOCATION_ID = :VIRTUAL_LOCATION_ID "
                    //+ "  ,LOCATION_NAME = :LOCATION_NAME "
                    //+ "  ,CLIENT_DICTATOR_ID = :CLIENT_DICTATOR_ID "
                    //+ "  ,CLIENT_WORKTYPE_ID = :CLIENT_WORKTYPE_ID "
                                + "  ,SERVICE_PROVIDER_NAME = :SERVICE_PROVIDER_NAME "
                                + "  ,CUSTOM1 = :CUSTOM1 "
                                + "  ,CUSTOM2 = :CUSTOM2 "
                                + "  ,CUSTOM3 = :CUSTOM3 "
                    //+ "  ,DICTATING_PHYSICIAN_ID = :DICTATING_PHYSICIAN_ID "
                    //+ "  ,PAT_COSTCENTER = :PAT_COSTCENTER "
                    //+ "  ,PAT_LOC = :PAT_LOC "
                    //+ "  ,PAT_ROOM = :PAT_ROOM "
                                + "  ,ACTIVITY_TYPE = :ACTIVITY_TYPE \r\n"
                                + "  ,CHANGED_ON = SYSDATE "
                                + "  ,DEP_COUNT = :DEP_COUNT \r\n"
                                + "WHERE EXT_SYS = :EXT_SYS \r\n"
                                + "  AND EXT_WORK_UNIT_KEY = :EXT_WORK_UNIT_KEY \r\n"
                                + "  AND TAT_ENDS_ON >= :RANGE_BEGIN \r\n"
                                + "  AND TAT_ENDS_ON <  :RANGE_END \r\n"
                                + "  AND INVOICE_ID IS NULL"
                                , SchemaName);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                OracleHelper.AddCommandParameter(cmd, ":EXT_CLIENT_KEY", item.ExtClientKey, OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":EXT_WORK_TYPE", item.ExtWorkType, OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":WORK_UNIT_STATUS", item.WorkUnitStatus, OracleType.Char, 2, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":JOB_NBR", item.JobNbr, OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":URGENCY", item.Urgency, OracleType.Char, 1, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":FIRST_MT_ID", CheckNull(item.FirstMTID), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":MT_NAME", CheckNull(item.MTName), OracleType.VarChar, 60, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DICTATION_ID", CheckNull(item.DictationID), OracleType.VarChar, 64, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DICTATOR_ID", CheckNull(item.DictatorID), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":TAT_BEGINS_ON", CheckNull(item.TatBeginsOn), OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":TAT_ENDS_ON", CheckNull(item.TatEndsOn), OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":PATIENT_ID", CheckNull(item.PatientID), OracleType.VarChar, 40, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":CHARS_TOTAL", CheckNull(item.CharsTotal), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":CHARS_NOSPACES", CheckNull(item.CharsNoSpaces), OracleType.Number, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":INVOICE_GRP_OVERRIDE_KEY", CheckNull(item.InvoiceGrpOverrideKey), OracleType.VarChar, 80, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":PATIENT_LAST", CheckNull(item.PatientLast), OracleType.VarChar, 40, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":ENTRY_LOC", CheckNull(item.EntryLocation), OracleType.Char, 1, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DICTATOR_NAME", CheckNull(item.DictatorName), OracleType.VarChar, 60, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DEPARTMENT", CheckNull(item.Department), OracleType.VarChar, 50, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":CUSTOMER_ACCT_NBR", CheckNull(item.CustomerAcctNbr), OracleType.VarChar, 50, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DICTATOR_SPECIALTY", CheckNull(item.DictatorSpecialty), OracleType.VarChar, 64, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":LINES_GROSS", CheckNull(item.LinesGross, OracleType.Number), ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DICTATION_SECONDS", CheckNull(item.DictationSeconds), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DICTATOR_SR_SCORE", CheckNull(item.DictatorSRScore), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DOCUMENT_SR_SCORE", CheckNull(item.DocumentSRScore), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":SERVICE_PROVIDER", CheckNull(item.ServiceProvider), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":CUSTOM1", CheckNull(item.Custom1), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":CUSTOM2", CheckNull(item.Custom2), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":CUSTOM3", CheckNull(item.Custom3), OracleType.VarChar, 20, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":VIRTUAL_LOCATION_ID", CheckNull(item.VirtualLocationID), OracleType.Int32, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":LOCATION_NAME", CheckNull(item.LocationName), OracleType.VarChar, 50, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":CLIENT_DICTATOR_ID", CheckNull(item.ClientDicatatorID), OracleType.VarChar, 50, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":CLIENT_WORKTYPE_ID", CheckNull(item.ClientWorkTypeID), OracleType.Int32, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":SERVICE_PROVIDER_NAME", CheckNull(item.ServiceProviderName), OracleType.VarChar, 50, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":DICTATING_PHYSICIAN_ID", CheckNull(item.DictatingPhysicianID), OracleType.Int32, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":PAT_COSTCENTER", CheckNull(item.PatientCostCenter), OracleType.VarChar, 255, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":PAT_LOC", CheckNull(item.PatientLocation), OracleType.VarChar, 255, ParameterDirection.Input);
                //OracleHelper.AddCommandParameter(cmd, ":PAT_ROOM", CheckNull(item.PatientRoom), OracleType.VarChar, 255, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ACTIVITY_TYPE", CheckNull(item.ActivityType), OracleType.Char, 2, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DEP_COUNT", CheckNull(item.DepCount), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":EXT_SYS", item.ExtSys, OracleType.Char, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":EXT_WORK_UNIT_KEY", item.ExtWorkUnitKey, OracleType.VarChar, 60, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":RANGE_BEGIN", range.BeginsOn, OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":RANGE_END", range.EndsBefore, OracleType.DateTime, ParameterDirection.Input);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ExtWorkUnitKey = {0}", item.ExtWorkUnitKey);
                    Console.WriteLine("ExtClientKey = {0}", item.ExtClientKey);
                    throw;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return true;
        }
    }
}

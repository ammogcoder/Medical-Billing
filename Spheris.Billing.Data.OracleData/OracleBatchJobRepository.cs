using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace Spheris.Billing.Data.OracleData
{
    public class OracleBatchJobRepository : BatchJobRepository
    {
        public OracleBatchJobRepository() { }

        public override int Add(BatchJob newItem)
        {
            using(OracleConnection cnn = new OracleConnection(base.ConnectionString.Value))
            using(OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = String.Format(
                                  "insert into {0}.batch_job \r\n"
                                + "( submitted_on               ,completed_on               ,batch_status \r\n"
                                + "  ,submission_type           ,submitted_by               ,err_msg \r\n"
                                + "  ,comments                  ,batch_job_type             ,timespan_ge \r\n"
                                + "  ,timespan_lt               ,nbr_sel                    ,nbr_ins \r\n"
                                + "  ,nbr_upd                   ,nbr_del                    ,nbr_reject \r\n"
                                + "  ,ext_sys) \r\n"
                                + "values \r\n"
                                + "( sysdate                    ,:completed_on              ,:batch_status \r\n"
                                + "  ,:submission_type          ,user                       ,:err_msg \r\n"
                                + "  ,:comments                 ,:batch_job_type            ,:timespan_ge \r\n"
                                + "  ,:timespan_lt              ,:nbr_sel                   ,:nbr_ins \r\n"
                                + "  ,:nbr_upd                  ,:nbr_del                   ,:nbr_reject \r\n"
                                + "  ,:ext_sys) \r\n"
                                + "returning batch_job_id into :batch_job_id"
                                , base.SchemaName);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                OracleHelper.AddCommandParameter(cmd, ":completed_on", CheckNull(newItem.COMPLETED_ON, new DateTime(1900, 1, 1)), OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":batch_status", CheckNull(newItem.BATCH_STATUS), OracleType.Char, 4, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":submission_type", CheckNull(newItem.SUBMISSION_TYPE), OracleType.Char, 1, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":err_msg", CheckNull(newItem.ERR_MSG), OracleType.VarChar, 4000, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":comments", CheckNull(newItem.COMMENTS), OracleType.VarChar, 200, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":batch_job_type", CheckNull(newItem.BATCH_JOB_TYPE), OracleType.Char, 4, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":timespan_ge", CheckNull(newItem.TIMESPAN_GE), OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":timespan_lt", CheckNull(newItem.TIMESPAN_LT), OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_sel", CheckNull(newItem.NBR_SEL, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_ins", CheckNull(newItem.NBR_INS, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_upd", CheckNull(newItem.NBR_UPD, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_del", CheckNull(newItem.NBR_DEL, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_reject", CheckNull(newItem.NBR_REJECT, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_sys", CheckNull(newItem.EXT_SYS), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":batch_job_id", OracleType.Number, ParameterDirection.ReturnValue);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                return int.Parse(cmd.Parameters[":batch_job_id"].Value.ToString());       // Return the InvoiceGrpId of the new record.
            }
        }

        public override bool Update(BatchJob batch)
        {
            using (OracleConnection cnn = new OracleConnection(ConnectionString.Value))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = String.Format(
                                  "update {0}.batch_job set \r\n"
                                + "  completed_on = :completed_on \r\n"
                                + "  ,batch_status = :batch_status \r\n"
                                + "  ,err_msg = :err_msg \r\n"
                                + "  ,comments = :comments \r\n"
                                + "  ,nbr_sel = :nbr_sel \r\n"
                                + "  ,nbr_ins = :nbr_ins \r\n"
                                + "  ,nbr_upd = :nbr_upd \r\n"
                                + "  ,nbr_del = :nbr_del \r\n"
                                + "  ,nbr_reject = :nbr_reject \r\n"
                                + "where batch_job_id = :batch_job_id"
                                , SchemaName);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                OracleHelper.AddCommandParameter(cmd, ":completed_on", CheckNull(batch.COMPLETED_ON, DateTime.Now), OracleType.DateTime, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":batch_status", CheckNull(batch.BATCH_STATUS), OracleType.Char, 4, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":err_msg", CheckNull(batch.ERR_MSG), OracleType.VarChar, 4000, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":comments", CheckNull(batch.COMMENTS), OracleType.VarChar, 200, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_sel", CheckNull(batch.NBR_SEL, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_ins", CheckNull(batch.NBR_INS, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_upd", CheckNull(batch.NBR_UPD, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_del", CheckNull(batch.NBR_DEL, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":nbr_reject", CheckNull(batch.NBR_REJECT, 0), OracleType.Number, 9, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":batch_job_id", batch.BATCH_JOB_ID, OracleType.Number, ParameterDirection.Input);
                cnn.Open();
                int recsEffected = cmd.ExecuteNonQuery();
                cnn.Close();
                return (recsEffected > 0);
            }
        }

        public override BatchJob FindById(int id)
        {
            throw new NotImplementedException();
        }

        public override List<BatchJob> GetAll()
        {
            throw new NotImplementedException();
        }

        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override List<BatchJob> ConvertDataReaderToList(IDataReader reader)
        {
            decimal val;
            List<BatchJob> list = new List<BatchJob>();

            while (reader.Read())
            {
                BatchJob item = new BatchJob();
                item.COMPLETED_ON = DateTime.Parse(reader["completed_on"].ToString());
                item.BATCH_STATUS = reader["batch_status"].ToString();
                item.SUBMISSION_TYPE = reader["submission_type"].ToString();
                item.SUBMITTED_BY = reader["submitted_by"].ToString();
                item.ERR_MSG = reader["err_msg"].ToString();
                item.COMMENTS = reader["comments"].ToString();
                item.BATCH_JOB_TYPE = reader["batch_job_type"].ToString();
                if(!String.IsNullOrEmpty(reader["timespan_ge"].ToString()))
                {
                    item.TIMESPAN_GE = DateTime.Parse(reader["timespan_ge"].ToString());
                }
                if(!String.IsNullOrEmpty(reader["timespan_lt"].ToString()))
                {
                    item.TIMESPAN_LT = DateTime.Parse(reader["timespan_lt"].ToString());
                }
                item.NBR_SEL = CastDbValueDecimalOrZero(reader,"NBR_SEL");
                item.NBR_INS = CastDbValueDecimalOrZero(reader,"NBR_INS");
                item.NBR_UPD = CastDbValueDecimalOrZero(reader,"NBR_UPD");
                item.NBR_DEL = CastDbValueDecimalOrZero(reader,"NBR_DEL");
                item.NBR_REJECT = CastDbValueDecimalOrZero(reader,"NBR_REJECT");

                item.EXT_SYS = reader["ext_sys"].ToString();
                item.BATCH_JOB_ID = int.Parse(reader["batch_job_id"].ToString());
                list.Add(item);
            }
            return list;
        }

        public override BatchJob GetLatestCompleted(string batchJobType)
        {
            List<BatchJob> items = FindLatestCompleted(batchJobType);
            if( items.Count == 0 )
            {
                return null;
            }
            else
            {
                return items[0];
            }
        }

        public override List<BatchJob> GetLatestCompleted()
        {
            return FindLatestCompleted(null);
        }

        private List<BatchJob> FindLatestCompleted(string batchJobType)
        {
            using (OracleConnection cnn = new OracleConnection(base.ConnectionString.Value))
            using (OracleCommand cmd = new OracleCommand())
            {
                string typeFilter = "";
                if (!String.IsNullOrEmpty(batchJobType))
                {
                    typeFilter = String.Format("where bj.batch_job_type = '{0}'", batchJobType);
                }

                cmd.CommandText = String.Format(
                                  "select   bj.submitted_on        ,bj.completed_on        ,bj.submission_type "
                                + "        ,bj.submitted_by        ,bj.err_msg             ,bj.comments "
                                + "        ,bj.batch_job_type      ,bj.timespan_ge         ,bj.timespan_lt "
                                + "        ,bj.nbr_sel             ,bj.nbr_ins             ,bj.nbr_del "
                                + "        ,bj.nbr_reject          ,bj.nbr_upd             ,bj.ext_sys "
                                + "        ,bj.batch_status        ,bj.batch_job_id "
                                + "from    {0}.batch_job bj "
                                + "join   (select batch_job_type, max(completed_on) as completed_on "
                                + "        from   {0}.batch_job "
                                + "        where  batch_status = 'COMP' "
                                + "          and  completed_on is not null "
                                + "        group by batch_job_type) c "
                                + "  on    c.batch_job_type = bj.batch_job_type "
                                + " and    c.completed_on = bj.completed_on "
                                + "{1} "
                                + "order by bj.batch_job_type"
                                , base.SchemaName
                                , typeFilter);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                cnn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                return ConvertDataReaderToList(dr);
            }
        }

        protected override void RowConverter(BatchJob r, DataRow record)
        {
            try
            {
                //CastDbValueRow(record, "BEGIN_ON", true, "DateTime");

                r.BATCH_JOB_ID = CastDbValueRow(record, "BATCH_JOB_ID", true, "decimal");
                r.BATCH_JOB_TYPE = CastDbValueRow(record, "BATCH_JOB_TYPE");
                r.BATCH_STATUS = CastDbValueRow(record, "BATCH_STATUS");
                r.COMMENTS = CastDbValueRow(record, "COMMENTS");
                r.COMPLETED_ON = CastDbValueRow(record, "COMPLETED_ON", true, "DateTime");
                r.ERR_MSG = CastDbValueRow(record, "ERR_MSG");
                r.EXT_SYS = CastDbValueRow(record, "EXT_SYS");

                r.NBR_DEL = CastDbValueRowDecimalOrZero(record, "NBR_DEL");
                r.NBR_INS = CastDbValueRowDecimalOrZero(record, "NBR_INS");
                r.NBR_REJECT = CastDbValueRowDecimalOrZero(record, "NBR_REJECT");
                r.NBR_SEL = CastDbValueRowDecimalOrZero(record, "NBR_SEL");
                r.NBR_UPD = CastDbValueRowDecimalOrZero(record, "NBR_UPD");

                r.SUBMISSION_TYPE = CastDbValueRow(record, "SUBMISSION_TYPE");
                r.SUBMITTED_BY = CastDbValueRow(record, "SUBMITTED_BY");
                r.SUBMITTED_ON = CastDbValueRow(record, "SUBMITTED_ON", true, "DateTime");
                r.TIMESPAN_GE = CastDbValueRow(record, "TIMESPAN_GE", true, "DateTime");
                r.TIMESPAN_LT = CastDbValueRow(record, "TIMESPAN_LT", true, "DateTime");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ObservableCollection<BatchJob> GetByFilter(DateTime? from, DateTime? through, string jobTypeFilter)
        {
            ObservableCollection<BatchJob> batchJobs;
            try
            {
                string sql;
                // SUBMITTED_ON
                // COMPLETED_ON
                if (through == null)
                    through = DateTime.Now;
                if (String.IsNullOrEmpty(jobTypeFilter)  && from != null && through != null)
                    sql = String.Format("Select * from {0}.BATCH_JOB where SUBMITTED_ON >= :SUBMITTED_ON and COMPLETED_ON < :COMPLETED_ON", SchemaName);
                else if( from != null && through != null && !String.IsNullOrEmpty(jobTypeFilter) )
                    sql = String.Format("Select * from {0}.BATCH_JOB where SUBMITTED_ON >=:SUBMITTED_ON and COMPLETED_ON < :COMPLETED_ON and BATCH_JOB_TYPE=:BATCH_JOB_TYPE", SchemaName);
                else if (!String.IsNullOrEmpty(jobTypeFilter))
                    sql = String.Format("Select * from {0}.BATCH_JOB where BATCH_JOB_TYPE=:BATCH_JOB_TYPE", SchemaName);
                else
                    sql = String.Format("Select * from {0}.BATCH_JOB", SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                if(from  != null)
                    parameters.Add(OracleHelper.CreateParameter(":SUBMITTED_ON", CheckNull(from), OracleType.DateTime, ParameterDirection.Input));
                if(through != null && from != null)
                    parameters.Add(OracleHelper.CreateParameter(":COMPLETED_ON", CheckNull(through), OracleType.DateTime, ParameterDirection.Input));
                if( !String.IsNullOrEmpty(jobTypeFilter))
                    parameters.Add(OracleHelper.CreateParameter(":BATCH_JOB_TYPE", CheckNull(jobTypeFilter), OracleType.VarChar , ParameterDirection.Input));

                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                batchJobs = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return batchJobs;
        }
    }
}

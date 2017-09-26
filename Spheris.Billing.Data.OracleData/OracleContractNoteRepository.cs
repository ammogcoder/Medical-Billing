using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleContractNoteRepository : ContractNoteRepositoryBase
    {
        public override void Remove(ContractNote item)
        {
            string sql = String.Format("delete {0}.contract_note where contract_note_id = :contract_note_id", SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":contract_note_id", item.ID, OracleType.Int32, ParameterDirection.Input) };
            OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, p);
        }

        public override ObservableCollection<ContractNote> GetByContractID(int contractID)
        {
            string sql = String.Format( "select  contract_note_id, contract_id, added_dt, added_by, note \r\n"
                                      + "from    {0}.contract_note \r\n"
                                      + "where   contract_id = :contract_id"
                                      , SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":contract_id", contractID, OracleType.Int32, ParameterDirection.Input) };
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            return ConvertDataTableToObservableCollection(dt);
        }

        public override ContractNote Get(ContractNote item)
        {
            string sql = String.Format("select  contract_note_id, contract_id, added_dt, added_by, note \r\n"
                                      + "from    {0}.contract_note \r\n"
                                      + "where   contract_note_id = :contract_note_id"
                                      , SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":contract_note_id", item.ID, OracleType.Int32, ParameterDirection.Input) };
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            if (dt.Rows.Count > 0)
            {
                return ConvertDataTableToList(dt)[0];
            }
            else
            {
                throw new RowNotInTableException("The note was not found.");
            }
        }

        public override void Add(ContractNote item)
        {
            string sql = String.Format( "insert into {0}.contract_note "
                                      + "  (contract_note_id "
                                      + "  ,contract_id                        ,added_dt "
                                      + "  ,added_by                           ,note) "
                                      + "values "
                                      + "  ({0}.contract_note_id.nextval "
                                      + "  ,:contract_id                       ,:added_dt"
                                      + "  ,:added_by                               ,:note) "
                                      + "returning contract_note_id into :contract_note_id"
                                      , SchemaName);
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(OracleHelper.CreateParameter(":contract_note_id", null, OracleType.Int32, ParameterDirection.InputOutput));
            parameters.Add(OracleHelper.CreateParameter(":contract_id", item.ContractID, OracleType.Int32, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":added_dt", item.DateAdded, OracleType.Timestamp, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":added_by", item.AddedBy, OracleType.NVarChar, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":note", item.Note, OracleType.VarChar, ParameterDirection.Input));
            OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            item.ID = int.Parse(parameters[0].Value.ToString());
        }

        public override void Update(ContractNote item)
        {
            if (item.ID == 0)
            {
                throw new ArgumentNullException("Item.InvoiceGrpId", "An item InvoiceGrpId was provided for the update.");
            }
            try
            {
                string sql = String.Format("update {0}.contract_note set "
                                          + "  contract_id = :contract_id "
                                          + "  ,added_dt = sysdate "
                                          + "  ,added_by = user "
                                          + "  ,note = :note "
                                          + "where contract_note_id = :contract_note_id"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":contract_note_id", item.ID, OracleType.Int32, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":contract_id", item.ContractID, OracleType.Int32, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":note", item.Note, OracleType.VarChar, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        protected override void RowConverter(ContractNote item, DataRow row)
        {
            item.AddedBy = row["added_by"].ToString();
            item.ContractID = int.Parse(row["contract_id"].ToString());
            item.DateAdded = DateTime.Parse(row["added_dt"].ToString());
            item.ID = int.Parse(row["contract_note_id"].ToString());
            item.Note = row["note"].ToString();
        }
    }
}

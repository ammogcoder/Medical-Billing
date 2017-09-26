using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceDetailFileTypeDal : Spheris.Billing.Data.InvoiceDetailFileTypeDal
    {
        private List<InvoiceDetailFileTypeItem> GetFileTypes(string id)
        {
            // See if an InvoiceGrpId was passed
            string whereSql = "";
            if (!string.IsNullOrEmpty(id))
            {
                whereSql = "where   file_type = :fileType;";
            }

            using(OracleConnection cnn = new OracleConnection(base.ConnectionString.Value))
            using(OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("select  file_type, descr, is_available, extension, sql \r\n"
                                              + "from    {0}.file_type \r\n"
                                              + "{1} \r\n"
                                              + "order by file_type;"
                                              ,base.SchemaName
                                              ,whereSql);
                if (!String.IsNullOrEmpty(id))
                {
                    OracleHelper.AddCommandParameter(cmd, ":fileType", id, OracleType.VarChar);
                }
                cmd.Connection = cnn; 
                cnn.Open();
                return ConvertDataReaderToList(cmd.ExecuteReader());
            }
        }

        protected List<InvoiceDetailFileTypeItem> ConvertDataReaderToList(OracleDataReader dr)
        {
            List<InvoiceDetailFileTypeItem> items = new List<InvoiceDetailFileTypeItem>();
            while (dr.Read())
            {
                InvoiceDetailFileTypeItem itm = new InvoiceDetailFileTypeItem();
                itm.Description = dr["descr"].ToString();
                itm.FileExtension = dr["extension"].ToString();
                itm.Id = dr["file_type"].ToString();
                itm.IsAvailable = (dr["file_type"].ToString() == "Y");
                itm.Sql = dr["sql"].ToString();
                items.Add(itm);
            }
            return items;
        }

        public override InvoiceDetailFileTypeItem FindById(string id)
        {
            // TODO: Raise an exception if there is more than one item returned.

            // Return the first match.
            return (GetFileTypes(id))[0];
        }

        public override string Save(InvoiceDetailFileTypeItem item)
        {
            using (OracleConnection cnn = new OracleConnection(base.ConnectionString.Value))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("declare \r\n"
                                              + "  cnt number; \r\n"
                                              + "begin \r\n"
                                              + "  select  count(*) \r\n"
                                              + "  into    cnt \r\n"
                                              + "  from    {0}.file_type \r\n"
                                              + "  where file_type = :fileType; \r\n"
                                              + "\r\n"
                                              + "  if( cnt = 0 ) then \r\n"
                                              + "    insert into {0}.file_type (file_type, descr, is_available, extension, sql) \r\n"
                                              + "    values (:fileType, :descr, :isAvailable, :fileExtension, :sql); \r\n"
                                              + "  else \r\n"
                                              + "    update {0}.file_type \r\n"
                                              + "    set descr = :descr \r\n"
                                              + "       ,is_available = :isAvailable \r\n"
                                              + "       ,extension = :fileExtension \r\n"
                                              + "       ,sql = :sql \r\n"
                                              + "    where file_type = :fileType; \r\n"
                                              + "  end if; \r\n"
                                              + "end; \r\n"
                                              + "commit;"
                                              , base.SchemaName);
                OracleHelper.AddCommandParameter(cmd, ":fileType", item.Id, OracleType.Char, 4);
                OracleHelper.AddCommandParameter(cmd, ":descr", item.Description, OracleType.VarChar, 100);
                char isAvailable = item.IsAvailable ? 'Y' : 'N';
                OracleHelper.AddCommandParameter(cmd, ":isAvailable", isAvailable, OracleType.Char, 1);
                OracleHelper.AddCommandParameter(cmd, ":fileExtension", item.FileExtension, OracleType.VarChar, 4);
                OracleHelper.AddCommandParameter(cmd, ":sql", item.Sql, OracleType.VarChar, 4000);
                cmd.Connection = cnn;
                cnn.Open();
                cmd.ExecuteNonQuery();
                return item.Id;
            }
        }

        public override string[] GetAllSqlColumnNames()
        {
            throw new NotImplementedException();
        }

        public override string[] GetSqlColumnNames(bool inUse)
        {
            throw new NotImplementedException();
        }

        public override List<InvoiceDetailFileTypeItem> GetAll()
        {
            return GetFileTypes(null);
        }

        public override void Delete(string id)
        {
            using (OracleConnection cnn = new OracleConnection(base.ConnectionString.Value))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("delete from {0}.file_type \r\n"
                                              + "where file_type = :fileType; \r\n"
                                              + "commit;"
                                              , base.SchemaName);
                OracleHelper.AddCommandParameter(cmd, ":fileType", id, OracleType.Char, 4);
                cmd.Connection = cnn;
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Timers;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceGroupRepository : InvoiceGroupRepositoryBase
    {
        public override void Remove(InvoiceGroup item)
        {
            string sql = String.Format("delete {0}.INVOICE_GRP where INVOICE_GRP_ID = :INVOICE_GRP_ID", SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":INVOICE_GRP_ID", item.InvoiceGrpId, OracleType.Int32, ParameterDirection.Input) };
            OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, p);
        }


        // delivery name contract 
        //select * from  sphrsbilling.bill_specialist

        private string Where(string where,string clause)
        {
            if (clause == null)
                return where;
            if (string.IsNullOrEmpty(where))
                return " WHERE " + clause;
            return where + " AND " + clause;
        }

        public override ObservableCollection<InvoiceGroup> FetchNullContracts()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<InvoiceGroup> groups = null;
            try
            {

                string sql = string.Format("select * from {0}.invoice_grp where contract_id is null order by descr",SchemaName);
                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                groups = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return groups;
        }




        public override ObservableCollection<InvoiceGroup> FetchGroups(BillingSpecialist specialist,DeliveryMethod deliveryMethod,string contractFilter,string descFilter  )
        {
            ObservableCollection<InvoiceGroup> groups = null;
            try
            {
                string where = string.Empty;
                if (descFilter != null)
                {
                    int output;
                    string descWhere = String.Format(" UPPER (ig.DESCR) LIKE '%{0}%'", descFilter.ToUpper());

                    if (int.TryParse(descFilter.Trim(), out output))
                        descWhere += String.Format(" OR ig.INVOICE_GRP_ID={0}", output);

                    descWhere = descWhere.Insert(0, "(");
                    descWhere += ")";
                    where = Where(where, descWhere);
                }
                if (deliveryMethod != null)
                    where = Where(where, String.Format(" IG.DELIVERY_METHOD='{0}'", deliveryMethod.TheDeliveryMethod) );

                if (specialist != null)
                    //where = Where(where, String.Format(" IG.Id='{0}'", specialist.Id));
                    where = Where(where, String.Format(" IG.BILL_SPECIALIST_ID='{0}'", specialist.Id));

                if (contractFilter != null)
                {
                    int output;
                    string conWhere = String.Format(" UPPER(CON.DESCR) LIKE '%{0}%'", contractFilter.ToUpper());

                    if (int.TryParse(contractFilter.Trim(), out output))
                        conWhere += String.Format(" OR ig.CONTRACT_ID={0}", output);
                    conWhere = conWhere.Insert(0, "(");
                    conWhere += ")";
                    where = Where(where, conWhere);
                }


                string sql = string.Format("select  ig.* ,con.descr\r\n"
                                         + " ,nvl(case when ig.encryption_optout = 'Y' then 'N' \r\n"
                                         + "  when ist.must_encrypt = 'Y' or r.must_encrypt = 'Y' then 'Y' else 'N' end, 'N') \r\n"
                                         + " as must_encrypt \r\n"
                                         + "from    sphrsbilling.invoice_grp ig \r\n"
                                         + "left join sphrsbilling.invoice_style ist \r\n"
                                         + "  on    ist.invoice_style = ig.invoice_style \r\n"
                                         + "left join sphrsbilling.contract con \r\n"
                                         + "  on    con.contract_id = ig.contract_id \r\n"
                                         + "left join (select invoice_grp_id, max(rt.must_encrypt) as must_encrypt \r\n"
                                         + "           from   sphrsbilling.invoice_grp_report igr \r\n"
                                         + "           join   sphrsbilling.report_type rt \r\n"
                                         + "             on   rt.report_type_id = igr.report_type_id \r\n"
                                         + "           where  rt.must_encrypt = 'Y' \r\n"
                                         + "           group by invoice_grp_id) r \r\n"
                                         + "  on    r.invoice_grp_id = ig.invoice_grp_id");
                if (!string.IsNullOrEmpty(where ))
                    sql += where;
                OracleParameter[] p = null;
#if TIMEIT
                DateTime Start = DateTime.Now;
#endif
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
#if TIMEIT
                DateTime End = DateTime.Now;
                TimeSpan CallTime = End - Start;

                System.Diagnostics.Debug.WriteLine("Query Time: + " + CallTime.Milliseconds.ToString());
#endif
                groups = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return groups;
            //return null;
        }

        public override InvoiceGroup Get(InvoiceGroup item)
        {
            string sql = String.Format("select * from {0}.INVOICE_GRP where INVOICE_GRP_ID={1}",SchemaName,item.InvoiceGrpId);
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
            if (dt.Rows.Count > 0)
            {
                RowConverter(item, dt.Rows[0]);
                return item;
            }
            else
            {
                throw new RowNotInTableException("Row not found.");
            }
        }

        /// <summary>
        /// Table: INVOICE_GRP
        /// 
        ///                        pk  null?   type                 Default
        /// ---------------------------------------------------------------------
        /// InvoiceGrpId        INVOICE_GRP_ID      1	1	N	NUMBER		
        /// Description         DESCR	            2		N	VARCHAR2(150 Byte)
        /// ChangedOn           CHANGED_ON	        3		N	DATE	                SYSDATE
        /// ChangedBy           CHANGED_BY	        4		N	VARCHAR2(255 Byte)	    SYS_CONTEXT('USERENV', 'OS_USER')
        /// UseDst              USE_DST	            5		N	CHAR(1 Byte)	        'Y'
        /// BillingFrequency    BILLING_FREQ	    6		N	CHAR(4 Byte)	        'MNTH'
        /// TimeZONE            TIMEZONE	        7		N	VARCHAR2(4 Byte)	    'CST'
        /// GLDistributionNumbe GL_DISTR_NBR	    8		N	VARCHAR2(15 Byte)	    '?'
        /// BillingSpecialistID BILL_SPECIALIST_ID	9		N	NUMBER	    0
        /// LegacyCustomerNumbe LEGACY_CUSTOMER_NBR	10		Y	VARCHAR2(10 Byte)
        /// ContractID          CONTRACT_ID	        11		Y	NUMBER
        /// InvoiceStyle        INVOICE_STYLE	    12		Y	CHAR(4 Byte)	        'DEFM'
        /// Brand               BRAND	            13		Y	CHAR(5 Byte)	        'MEDQ'
        /// DefaultBillFilePath DEFAULT_BILL_FILE_PATH 14	Y	VARCHAR2(255 Byte)
        /// InvoiceGroupStatus  INVOICE_GRP_STATUS	15		N	CHAR(4 Byte)	        'ACTV'
        /// GPCustomerNumber    GP_CUSTOMER_NBR	    16		N	VARCHAR2(15 Byte)	    '?'
        /// DoNotBillBefore     DO_NOT_BILL_BEFORE	17		Y	DATE
        /// AttnLineHeading     ATTN_LINE_HEADING	18		Y	VARCHAR2(20 Byte)
        /// AttnLine            ATTN_LINE	        19		Y	VARCHAR2(44 Byte)
        /// RemitToID           REMIT_TO_ID	        20		N	NUMBER	                2
        /// PrimaryPlatform     PRIMARY_PLATFORM	21		Y	VARCHAR2(5 Byte)
        /// DeliveryMethod      DELIVERY_METHOD	    22		N	CHAR(5 Byte)	        'PAPER'
        /// BillEmail           BILL_EMAIL	        23		Y	VARCHAR2(256 Byte)
        /// IsBWInvoiceStyle    BW_INVOICE_STYLE	24		N	CHAR(1 Byte)	        'N'
        /// EncryptionOptout    ENCRYPTION_OPTOUT	25		N	CHAR(1 Byte)	        'N'
        /// 
        /// </summary>
        public override void Add(InvoiceGroup item)
        {
            item.ChangedOn = DateTime.Now;
            try
            {
                string sql = String.Format("insert into {0}.invoice_grp "
                + "  ( "
                    /* 0 */                + " INVOICE_GRP_ID  ,     "  // InvoiceGrpId       
                    /* 1 */                + " DESCR,	            "  // Description        
                    /* 2 */                + " BILLING_FREQ,	     "  // BillingFrequency   
                    /* 3 */                + " GL_DISTR_NBR,	     "  // GLDistributionNumbe
                    /* 4 */                + " BILL_SPECIALIST_ID,	 "  // BillingSpecialistID
                    /* 5 */                + " LEGACY_CUSTOMER_NBR,	 "  // LegacyCustomerNumbe
                    /* 6 */                + " CONTRACT_ID	         ,"  // ContractID         
                    /* 7 */                + " INVOICE_STYLE	     ,"  // InvoiceStyle       
                    /* 8 */                + " BRAND	             ,"  // Brand              
                    /* 9 */                + " DEFAULT_BILL_FILE_PATH ,"  // DefaultBillFilePath
                    /* 10*/                + " INVOICE_GRP_STATUS	 ,"  // InvoiceGroupStatus 
                    /* 11*/                + " GP_CUSTOMER_NBR	     ,"  // GPCustomerNumber   
                    /* 12*/                + " ATTN_LINE_HEADING	 ,"  // AttnLineHeading    
                    /* 13*/                + " ATTN_LINE	         ,"  // AttnLine           
                    /* 14*/                + " REMIT_TO_ID	         ,"  // RemitToID          
                    /* 15*/                + " PRIMARY_PLATFORM	 ,"  // PrimaryPlatform    
                    /* 16*/                + " DELIVERY_METHOD	     ,"  // DeliveryMethod     
                    /* 17*/                + " BILL_EMAIL	         ,"  // BillEmail          
                    /* 18*/                + " BW_INVOICE_STYLE	 ,"  // IsBWInvoiceStyle   
                    /* 19*/                + " ENCRYPTION_OPTOUT	 "   // EncryptionOptout   
                + " )                  "
                + " VALUES             "
                + " ( "
                    /* 0 */                + " {0}.INVOICE_GRP_ID.NEXTVAL, "
                    /* 1 */                + " :I_DESCR,   "
                    /* 2 */                + " :I_BILLING_FREQ,    "
                    /* 3 */                + " :I_GL_DISTR_NBR,    "
                    /* 4 */                + " :I_BILL_SPECIALIST_ID,	 "
                    /* 5 */                + " :I_LEGACY_CUSTOMER_NBR,	  "
                    /* 6 */                + " :I_CONTRACT_ID	         ,"
                    /* 7 */                + " :I_INVOICE_STYLE	     ,"
                    /* 8 */                + " :I_BRAND	             ,"
                    /* 9 */                + " :I_DEFAULT_BILL_FILE_PATH,"
                    /* 10*/                + " :I_INVOICE_GRP_STATUS	, "
                    /* 11*/                + " :I_GP_CUSTOMER_NBR	    , "
                    /* 12*/                + " :I_ATTN_LINE_HEADING	, "
                    /* 13*/                + " :I_ATTN_LINE	        , "
                    /* 14*/                + " :I_REMIT_TO_ID	        , "
                    /* 15*/                + " :I_PRIMARY_PLATFORM	 ,"
                    /* 16*/                + " :I_DELIVERY_METHOD	  ,   "
                    /* 17*/                + " :I_BILL_EMAIL	      ,   "
                    /* 18*/                + " :I_BW_INVOICE_STYLE	 ,"
                    /* 19*/                + " :I_ENCRYPTION_OPTOUT	 "
                                           + ")  returning INVOICE_GRP_ID into :NEWID"
                , SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                /* 0 */parameters.Add(OracleHelper.CreateParameter(":NEWID", OracleType.Number, ParameterDirection.InputOutput));
                /* 1 */parameters.Add(OracleHelper.CreateParameter(":I_DESCR", CheckNull(item.Description), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(150 Byte)
                /* 2 */parameters.Add(OracleHelper.CreateParameter(":I_BILLING_FREQ", CheckNull(item.BillingFrequency), OracleType.Char, ParameterDirection.Input));//  CHAR(4 Byte)	   
                /* 3 */parameters.Add(OracleHelper.CreateParameter(":I_GL_DISTR_NBR", CheckNull(item.GLDistributionNumber), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(15 Byte)	
                /* 4 */parameters.Add(OracleHelper.CreateParameter(":I_BILL_SPECIALIST_ID", item.BillingSpecialistID, OracleType.Number, ParameterDirection.Input));//  NUMBER	    0
                /* 5 */parameters.Add(OracleHelper.CreateParameter(":I_LEGACY_CUSTOMER_NBR", CheckNull(item.LegacyCustomerNumber), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(10 Byte)
                /* 6 */parameters.Add(OracleHelper.CreateParameter(":I_CONTRACT_ID", CheckNull(item.ContractID), OracleType.Number, ParameterDirection.Input));//  NUMBER
                /* 7 */parameters.Add(OracleHelper.CreateParameter(":I_INVOICE_STYLE", CheckNull(item.InvoiceStyle), OracleType.Char, ParameterDirection.Input));//  CHAR(4 Byte)	    
                /* 8 */parameters.Add(OracleHelper.CreateParameter(":I_BRAND", item.Brand, OracleType.Char, ParameterDirection.Input));//  CHAR(5 Byte)	    
                /* 9 */parameters.Add(OracleHelper.CreateParameter(":I_DEFAULT_BILL_FILE_PATH", CheckNull(item.DefaultBillFilePath), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(255 Byte)
                /* 10*/parameters.Add(OracleHelper.CreateParameter(":I_INVOICE_GRP_STATUS", item.InvoiceGroupStatus, OracleType.Char, ParameterDirection.Input));//  CHAR(4 Byte)	   
                /* 11*/parameters.Add(OracleHelper.CreateParameter(":I_GP_CUSTOMER_NBR", CheckNull(item.GPCustomerNumber), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(15 Byte)	
                /* 12*/parameters.Add(OracleHelper.CreateParameter(":I_ATTN_LINE_HEADING", CheckNull(item.AttnLineHeading), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(20 Byte)
                /* 13*/parameters.Add(OracleHelper.CreateParameter(":I_ATTN_LINE", CheckNull(item.AttnLine), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(44 Byte) 
                /* 14*/parameters.Add(OracleHelper.CreateParameter(":I_REMIT_TO_ID", item.RemitToID, OracleType.Number, ParameterDirection.Input));//  NUMBER	           
                /* 15*/parameters.Add(OracleHelper.CreateParameter(":I_PRIMARY_PLATFORM", CheckNull(item.PrimaryPlatform), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(5 Byte)
                /* 16*/parameters.Add(OracleHelper.CreateParameter(":I_DELIVERY_METHOD", CheckNull(item.DeliveryMethod), OracleType.Char, ParameterDirection.Input));//  CHAR(5 Byte)	   
                /* 17*/parameters.Add(OracleHelper.CreateParameter(":I_BILL_EMAIL", CheckNull(item.BillEmail), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(256 Byte) 
                /* 18*/parameters.Add(OracleHelper.CreateParameter(":I_BW_INVOICE_STYLE", (item.IsBWInvoiceStyle) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));//  CHAR(1 Byte)	   
                /* 19*/parameters.Add(OracleHelper.CreateParameter(":I_ENCRYPTION_OPTOUT", (item.EncryptionOptout) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));//  CHAR(1 Byte)	   


                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                item.InvoiceGrpId = int.Parse(parameters[0].Value.ToString());

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                string err = sysEx.ToString();
                throw sysEx;
            }

        }


        public override void Update(InvoiceGroup item)
        {
            try
            {


                string sql = string.Format("UPDATE {0}.INVOICE_GRP "
                                  + "SET     DESCR = :I_DESCR "
                                  + "       ,BILLING_FREQ = :I_BILLING_FREQ "
                                  + "       ,GL_DISTR_NBR = :I_GL_DISTR_NBR "
                                  + "       ,BILL_SPECIALIST_ID = :I_BILL_SPECIALIST_ID "
                                  + "       ,LEGACY_CUSTOMER_NBR = :I_LEGACY_CUSTOMER_NBR "
                                  + "       ,CONTRACT_ID = :I_CONTRACT_ID "
                                  + "       ,INVOICE_STYLE = :I_INVOICE_STYLE "
                                  + "       ,BRAND = :I_BRAND "
                                  + "       ,DEFAULT_BILL_FILE_PATH = :I_DEFAULT_BILL_FILE_PATH "
                                  + "       ,INVOICE_GRP_STATUS = :I_INVOICE_GRP_STATUS "
                                  + "       ,GP_CUSTOMER_NBR = :I_GP_CUSTOMER_NBR "
                                  + "       ,ATTN_LINE_HEADING = :I_ATTN_LINE_HEADING "
                                  + "       ,ATTN_LINE = :I_ATTN_LINE "
                                  + "       ,REMIT_TO_ID = :I_REMIT_TO_ID "
                                  + "       ,INVOICE_GRP_ID = :I_INVOICE_GRP_ID "
                                  + "       ,PRIMARY_PLATFORM = :I_PRIMARY_PLATFORM "
                                  + "       ,BILL_EMAIL = :I_BILL_EMAIL "
                                  + "       ,DELIVERY_METHOD = :I_DELIVERY_METHOD "
                                  + "       ,BW_INVOICE_STYLE = :I_BW_INVOICE_STYLE "
                                  + "       ,ENCRYPTION_OPTOUT = :I_ENCRYPTION_OPTOUT "
                                  + "WHERE  INVOICE_GRP_ID = :I_INVOICE_GRP_ID", SchemaName);


                List<OracleParameter> parameters = new List<OracleParameter>();
                /* 1 */
                parameters.Add(OracleHelper.CreateParameter(":I_DESCR", CheckNull(item.Description), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(150 Byte)
                /* 2 */
                parameters.Add(OracleHelper.CreateParameter(":I_BILLING_FREQ", CheckNull(item.BillingFrequency), OracleType.Char, ParameterDirection.Input));//  CHAR(4 Byte)	   
                /* 3 */
                parameters.Add(OracleHelper.CreateParameter(":I_GL_DISTR_NBR", CheckNull(item.GLDistributionNumber), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(15 Byte)	
                /* 4 */
                parameters.Add(OracleHelper.CreateParameter(":I_BILL_SPECIALIST_ID", item.BillingSpecialistID, OracleType.Number, ParameterDirection.Input));//  NUMBER	    0
                /* 5 */
                parameters.Add(OracleHelper.CreateParameter(":I_LEGACY_CUSTOMER_NBR", CheckNull(item.LegacyCustomerNumber), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(10 Byte)
                /* 6 */
                parameters.Add(OracleHelper.CreateParameter(":I_CONTRACT_ID", CheckNull(item.ContractID), OracleType.Number, ParameterDirection.Input));//  NUMBER
                /* 7 */
                parameters.Add(OracleHelper.CreateParameter(":I_INVOICE_STYLE", CheckNull(item.InvoiceStyle), OracleType.Char, ParameterDirection.Input));//  CHAR(4 Byte)	    
                /* 8 */
                parameters.Add(OracleHelper.CreateParameter(":I_BRAND", item.Brand, OracleType.Char, ParameterDirection.Input));//  CHAR(5 Byte)	    
                /* 9 */
                parameters.Add(OracleHelper.CreateParameter(":I_DEFAULT_BILL_FILE_PATH", CheckNull(item.DefaultBillFilePath), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(255 Byte)
                /* 10*/
                parameters.Add(OracleHelper.CreateParameter(":I_INVOICE_GRP_STATUS", item.InvoiceGroupStatus, OracleType.Char, ParameterDirection.Input));//  CHAR(4 Byte)	   
                /* 11*/
                parameters.Add(OracleHelper.CreateParameter(":I_GP_CUSTOMER_NBR", CheckNull(item.GPCustomerNumber), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(15 Byte)	
                /* 12*/
                parameters.Add(OracleHelper.CreateParameter(":I_ATTN_LINE_HEADING", CheckNull(item.AttnLineHeading), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(20 Byte)
                /* 13*/
                parameters.Add(OracleHelper.CreateParameter(":I_ATTN_LINE", CheckNull(item.AttnLine), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(44 Byte) 
                /* 14*/
                parameters.Add(OracleHelper.CreateParameter(":I_REMIT_TO_ID", item.RemitToID, OracleType.Number, ParameterDirection.Input));//  NUMBER	           
                /* 15*/
                parameters.Add(OracleHelper.CreateParameter(":I_PRIMARY_PLATFORM", CheckNull(item.PrimaryPlatform), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(5 Byte)
                /* 16*/
                parameters.Add(OracleHelper.CreateParameter(":I_DELIVERY_METHOD", CheckNull(item.DeliveryMethod), OracleType.Char, ParameterDirection.Input));//  CHAR(5 Byte)	   
                /* 17*/
                parameters.Add(OracleHelper.CreateParameter(":I_BILL_EMAIL", CheckNull(item.BillEmail), OracleType.VarChar, ParameterDirection.Input));//  VARCHAR2(256 Byte) 
                /* 18*/
                parameters.Add(OracleHelper.CreateParameter(":I_BW_INVOICE_STYLE", (item.IsBWInvoiceStyle) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));//  CHAR(1 Byte)	   
                /* 19*/
                parameters.Add(OracleHelper.CreateParameter(":I_ENCRYPTION_OPTOUT", (item.EncryptionOptout) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));//  CHAR(1 Byte)	   

                parameters.Add(OracleHelper.CreateParameter(":I_INVOICE_GRP_ID", item.InvoiceGrpId, OracleType.Number, ParameterDirection.Input));//  NUMBER	    0

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static int cycles = 0;

        protected override void RowConverter(InvoiceGroup item, DataRow row)
        {
            int parsed = 0;
            try
            {
                cycles++;
                
                item.AttnLine = row["ATTN_LINE"].ToString();
                item.AttnLineHeading = row["ATTN_LINE_HEADING"].ToString();
                item.BillEmail = row["BILL_EMAIL"].ToString();
                item.BillingFrequency = row["BILLING_FREQ"].ToString();

                //if (int.TryParse(row["Id"].ToString(), out parsed))
                if (int.TryParse(row["BILL_SPECIALIST_ID"].ToString(), out parsed))
                    item.BillingSpecialistID = parsed;
                item.Brand = row["BRAND"].ToString();
                item.DefaultBillFilePath = row["DEFAULT_BILL_FILE_PATH"].ToString();
                item.ChangedBy = row["CHANGED_BY"].ToString();
                var asdf = row["CHANGED_ON"];

                DateTime tmp;
                if (DateTime.TryParse(row["CHANGED_ON"].ToString(), out tmp))
                    item.ChangedOn = DateTime.Parse(row["CHANGED_ON"].ToString());


                if (int.TryParse(row["CONTRACT_ID"].ToString(), out parsed))
                    item.ContractID = parsed;
                item.DeliveryMethod = row["DELIVERY_METHOD"].ToString();
                item.Description = row["DESCR"].ToString();

                if (DateTime.TryParse(row["DO_NOT_BILL_BEFORE"].ToString(), out tmp))
                    DateTime.Parse(row["DO_NOT_BILL_BEFORE"].ToString());

                item.GLDistributionNumber = row["GL_DISTR_NBR"].ToString();
                item.GPCustomerNumber = row["GP_CUSTOMER_NBR"].ToString();
                if (int.TryParse(row["INVOICE_GRP_ID"].ToString(), out parsed))
                    item.InvoiceGrpId = parsed;
                item.InvoiceGroupStatus = row["INVOICE_GRP_STATUS"].ToString();
                item.InvoiceStyle = row["INVOICE_STYLE"].ToString();
                item.IsBWInvoiceStyle = (row["BW_INVOICE_STYLE"].ToString() == "Y") ? true : false;
                item.LegacyCustomerNumber = row["LEGACY_CUSTOMER_NBR"].ToString();
                item.PrimaryPlatform = row["PRIMARY_PLATFORM"].ToString();
                if (int.TryParse(row["REMIT_TO_ID"].ToString(), out parsed))
                    item.RemitToID = parsed;
                item.TimeZone = row["TIMEZONE"].ToString();
                item.UseDst = (row["USE_DST"].ToString() == "Y") ? true : false;
                item.EncryptionOptout = (row["ENCRYPTION_OPTOUT"].ToString() == "Y") ? true : false;

                //OracleInvoiceGrpReportRepository reportsRepository = new OracleInvoiceGrpReportRepository();
                //item.InvoiceGrpReports = reportsRepository.GetById(item.InvoiceGrpId);
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
        }
    }
}

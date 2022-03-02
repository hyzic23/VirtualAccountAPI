using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using VirtualAccountAutomation.Infrastructure.DataAccess;
using VirtualAccountAutomation.Infrastructure.Interfaces;
using VirtualAccountAutomation.Infrastructure.Models;

namespace VirtualAccountAutomation.Infrastructure.Repository
{
    public class VirtualAccountRepository : DataHelper, IVirtualAccountRepository
    {
        private readonly IConfiguration configuration;

        public VirtualAccountRepository(IConfiguration configuration) : base(configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(VirtualAccountRequest request)
        {
            request.CreateDate = System.DateTime.Now;
            request.AggregatorId = Guid.NewGuid().ToString();
            var rowAffected = 0;
            using(var connection = CreateConnection())
            {

                  string sQuery = "zsp_virtual_acct_automation";
                  connection.Open();
                  //var transaction = connection.BeginTransaction();

                 var parameters = new DynamicParameters();
                     parameters.Add("@AggregatorId", request.AggregatorId);
                     parameters.Add("@AggregatorName", request.AggregatorName);
                     parameters.Add("@CreatedBy", request.CreatedBy);
                     parameters.Add("@CreateDate", request.CreateDate);
                     parameters.Add("@Status", request.Status);
                     parameters.Add("@GenericAcctName", request.GenericAcctName);
                     parameters.Add("@SettlementAcctNo", request.SettlementAcctNo);
                     parameters.Add("@AcctClassCode", request.AcctClassCode);
                     parameters.Add("@EnableAutoSweep", request.EnableAutoSweep);
                     parameters.Add("@MerchantId", request.MerchantId);
                     parameters.Add("@BaseRim", request.BaseRim);
                     parameters.Add("@NoOfAcctRequested", request.NoOfAcctRequested);
                     parameters.Add("@NoOfAcctCreated", request.NoOfAcctCreated);
                     parameters.Add("@BatchId", request.BatchId);
                     parameters.Add("@BatchLastName", request.BatchLastName);
                     parameters.Add("@StartSeqNo", request.StartSeqNo);
                     parameters.Add("@EndSeqNo", request.EndSeqNo);
                     parameters.Add("@BVNAcct", request.BVNAcct);
                     parameters.Add("@ApprovedByPOS", request.ApprovedByPOS);
                     //parameters.Add("@ApprovedPOSDate", request.ApprovedPOSDate);
                        if(request.ApprovedPOSDate == null)
                        {
                            parameters.Add("@ApprovedPOSDate", null);
                        }
                        else
                        {
                            parameters.Add("@ApprovedPOSDate", request.ApprovedPOSDate);
                        }
                        parameters.Add("@ApprovedByEbizness", request.ApprovedByEbizness);
                        parameters.Add("@ApprovedEbizDate", request.ApprovedEbizDate);
                        // if(request.ApprovedPOSDate == )
                        // {
                        //     parameters.Add("@ApprovedEbizDate", null);
                        // }
                        // else
                        // {
                        //     parameters.Add("@ApprovedEbizDate", request.ApprovedEbizDate);
                        // }
                     parameters.Add("@ApprovedByFinal", request.ApprovedByFinal);
                        //parameters.Add("@ApprovedFinalDate", request.ApprovedFinalDate);
                        if(request.ApprovedFinalDate == null)
                        {
                            parameters.Add("@ApprovedFinalDate", null);
                        }
                        else
                        {
                            parameters.Add("@ApprovedFinalDate", request.ApprovedFinalDate);
                        }
                     parameters.Add("@IsActive", "True");
                     parameters.Add("@Opt", "Insert");
           
                     rowAffected = await connection.ExecuteAsync(sQuery, parameters, null, 0, commandType : CommandType.StoredProcedure);
                     if(rowAffected > 0)
                     {
                        //transaction.Commit();
                        rowAffected = 1;
                     }
                     else
                     {
                        //transaction.Rollback();
                        rowAffected = 0;
                     }
                    
            }
                    return rowAffected;
        }

        public async Task<int> DeleteAsync(string id)
        {
            //var query = "DELETE FROM zib_virtual_acct_automation WHERE AggregatorId = @AggregatorId";
            var query = "UPDATE zib_virtual_acct_automation Set IsActive = False WHERE AggregatorId = @AggregatorId";
           
            var parameters = new DynamicParameters();
            parameters.Add("AggregatorId", id, System.Data.DbType.String);

            using(var connection = CreateConnection())
            {
                return (await connection.ExecuteAsync(query, parameters));
            }
        }

        public async Task<IEnumerable<VirtualAccountRequest>> GetAllAsync()
        {
                var query = " Select AggregatorId, AggregatorName, CreatedBy, CreateDate, Status, GenericAcctName, SettlementAcctNo, AcctClassCode, ";
                    query += " EnableAutoSweep, MerchantId, BaseRim, NoOfAcctRequested, NoOfAcctCreated, BatchId, BatchLastName, StartSeqNo, EndSeqNo, ";
                    query += " BVNAcct, ApprovedByPOS, ApprovedPOSDate, ApprovedByEbizness, ApprovedEbizDate, ApprovedByFinal, ApprovedFinalDate from zib_virtual_acct_automation";
                    query += " WHERE IsActive = @IsActive";

                        var parameters = new DynamicParameters();
                            parameters.Add("IsActive", "True", System.Data.DbType.String);

                    
                    using(var connection = CreateConnection())
                    {
                        return (await connection.QueryAsync<VirtualAccountRequest>(query, parameters)).ToList();
                    }
        }


        //Get By AggragatorId
        public async Task<VirtualAccountRequest> GetByIdAsync(string AggregatorId)
        {
            var query = " Select AggregatorId, AggregatorName, CreatedBy, CreateDate, Status, GenericAcctName, SettlementAcctNo, AcctClassCode, ";
               query += " EnableAutoSweep, MerchantId, BaseRim, NoOfAcctRequested, NoOfAcctCreated, BatchId, BatchLastName, StartSeqNo, EndSeqNo, ";
               query += " BVNAcct, ApprovedByPOS, ApprovedPOSDate, ApprovedByEbizness, ApprovedEbizDate, ApprovedByFinal, ApprovedFinalDate from zib_virtual_acct_automation";
               query += " WHERE AggregatorId = @AggregatorId";

               var parameters = new DynamicParameters();
                   parameters.Add("AggregatorId", AggregatorId, System.Data.DbType.String);


                using(var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<VirtualAccountRequest>(query, parameters));
                }
        }


        // public async Task<VirtualAccountRequest> GetByIdAsync(int MerchantId)
        // {
        //     var query = " Select AggregatorId, AggregatorName, CreatedBy, CreateDate, Status, GenericAcctName, SettlementAcctNo, AcctClassCode, ";
        //        query += " EnableAutoSweep, MerchantId, BaseRim, NoOfAcctRequested, NoOfAcctCreated, BatchId, BatchLastName, StartSeqNo, EndSeqNo, ";
        //        query += " BVNAcct, ApprovedByPOS, ApprovedPOSDate, ApprovedByEbizness, ApprovedEbizDate, ApprovedByFinal, ApprovedFinalDate from zib_virtual_acct_automation";
        //        query += " WHERE MerchantId = @MerchantId";

        //        var parameters = new DynamicParameters();
        //            parameters.Add("MerchantId", MerchantId, System.Data.DbType.String);


        //         using(var connection = CreateConnection())
        //         {
        //             return (await connection.QueryFirstOrDefaultAsync<VirtualAccountRequest>(query, parameters));
        //         }
        // }

        public async Task<int> UpdateAsync(VirtualAccountRequest request)
        {
              var query =  " Update zib_virtual_acct_automation Set AggregatorId = @AggregatorId, AggregatorName = @AggregatorName,";
                 query +=  " CreatedBy = @CreatedBy, CreateDate = @CreateDate,  Status = @Status, GenericAcctName = @GenericAcctName,  SettlementAcctNo = @SettlementAcctNo, AcctClassCode = @AcctClassCode,  Status = @Status, EnableAutoSweep = @EnableAutoSweep,";
                 query +=  " MerchantId = @MerchantId, BaseRim = @BaseRim,  NoOfAcctRequested = @NoOfAcctRequested, NoOfAcctCreated = @NoOfAcctCreated,  BatchId = @BatchId, BatchLastName = @BatchLastName,  StartSeqNo = @StartSeqNo, EndSeqNo = @EndSeqNo,";
                 query +=  " BVNAcct = @BVNAcct, ApprovedByPOS = @ApprovedByPOS, ApprovedPOSDate = @ApprovePOSDate,  ApprovedByEbizness = @ApprovedByEbizness, ApprovedEbizDate = @ApprovedEbizDate,  ApprovedByFinal = @ApprovedByFinal ";
                 query +=  " WHERE MerchantId = @MerchantId";

                var parameters = new DynamicParameters();
                    parameters.Add("AggregatorId", request.AggregatorId, System.Data.DbType.String);
                    parameters.Add("AggregatorName", request.AggregatorName, System.Data.DbType.String);
                    parameters.Add("CreatedBy", request.CreatedBy, System.Data.DbType.String);
                    parameters.Add("CreateDate", request.CreateDate, System.Data.DbType.DateTime);
                    parameters.Add("Status", request.Status, System.Data.DbType.String);
                    parameters.Add("GenericAcctName", request.GenericAcctName, System.Data.DbType.String);
                    parameters.Add("SettlementAcctNo", request.SettlementAcctNo, System.Data.DbType.String);
                    parameters.Add("AcctClassCode", request.AcctClassCode, System.Data.DbType.String);
                    parameters.Add("EnableAutoSweep", request.EnableAutoSweep, System.Data.DbType.Boolean);
                    parameters.Add("MerchantId", request.MerchantId, System.Data.DbType.String);
                    parameters.Add("BaseRim", request.BaseRim, System.Data.DbType.String);
                    parameters.Add("NoOfAcctRequested", request.NoOfAcctRequested, System.Data.DbType.Int16);
                    parameters.Add("NoOfAcctCreated", request.NoOfAcctCreated, System.Data.DbType.Int16);
                    parameters.Add("BatchId", request.BatchId, System.Data.DbType.String);
                    parameters.Add("BatchLastName", request.BatchLastName, System.Data.DbType.String);
                    parameters.Add("StartSeqNo", request.StartSeqNo, System.Data.DbType.Int16);
                    parameters.Add("EndSeqNo", request.EndSeqNo, System.Data.DbType.Int16);
                    parameters.Add("BVNAcct", request.BVNAcct, System.Data.DbType.String);
                    parameters.Add("ApprovedByPOS", request.ApprovedByPOS, System.Data.DbType.String);
                    parameters.Add("ApprovePOSDate", request.ApprovedPOSDate, System.Data.DbType.DateTime);
                    parameters.Add("ApprovedByEbizness", request.ApprovedByEbizness, System.Data.DbType.String);
                    parameters.Add("ApprovedEbizDate", request.ApprovedEbizDate, System.Data.DbType.DateTime);
                    parameters.Add("ApprovedByFinal", request.ApprovedByFinal, System.Data.DbType.String);
                    parameters.Add("ApprovedFinalDate", request.ApprovedFinalDate, System.Data.DbType.DateTime);


                using(var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
        }
    }
}
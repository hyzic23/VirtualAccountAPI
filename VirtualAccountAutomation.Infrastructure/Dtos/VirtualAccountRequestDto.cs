using System;

namespace VirtualAccountAutomation.Infrastructure.Dtos
{
    public class VirtualAccountRequestDto
    {
        public int Id { get; set; }
        public string AggregatorId { get; set; }
        public string AggregatorName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string GenericAcctName { get; set; }
        public string SettlementAcctNo { get; set; }
        public int AcctClassCode { get; set; }
        public Boolean EnableAutoSweep { get; set; }
        public string MerchantId { get; set; }
        public string BaseRim { get; set; }
        public int NoOfAcctRequested { get; set; }
        public int NoOfAcctCreated { get; set; }
        public string BatchId { get; set; }
        public string BatchLastName { get; set; }
        public int StartSeqNo { get; set; }
        public int EndSeqNo { get; set; }
        public string BVNAcct { get; set; }
        public string ApprovedByPOS { get; set; }
        public DateTime? ApprovedPOSDate { get; set; }
        public string ApprovedByEbizness { get; set; }
        public DateTime? ApprovedEbizDate { get; set; }
        public string ApprovedByFinal { get; set; }
        public DateTime? ApprovedFinalDate { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
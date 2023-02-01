
using PM_Case_Managemnt_API.Models.Common;
using System.ComponentModel.DataAnnotations;


namespace PM_Case_Managemnt_API.Models.PM
{
    public class ActivityProgress : CommonModel
    {
        
        //public ActivityProgress()
        //{
        //    ProgressAttachments = new HashSet<ProgressAttachment>();
        //}
        
        public Guid ActivityId { get; set; }
       
        //public virtual Activity Activity { get; set; }
        public float ActualBudget { get; set; }
        public float ActualWorked { get; set; }
        public Guid EmployeeValueId { get; set; }
      //  public virtual Employee EmployeeValue { get; set; }
        public Guid quarterId { get; set; }
       // public virtual ActivityTargetDivision quarter { get; set; }

        public string DocumentPath { get; set; }

        public string FinanceDocumentPath { get; set; }

        public approvalStatus isApprovedByCoordinator { get; set; }

        public approvalStatus isApprovedByFinance { get; set; }

        public approvalStatus isApprovedByDirector { get; set; }


        public string FinanceApprovalRemark { get; set; }
        public string CoordinatorApprovalRemark { get; set; }
        public string DirectorApprovalRemark { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
   
        public ProgressStatus progressStatus { get; set; }
    }
    public enum ProgressStatus
    {
        SimpleProgress,
        Finalize
    }

	public enum approvalStatus
	{
		pending ,
		approved ,
		rejected
	}
}

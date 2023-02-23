
using PM_Case_Managemnt_API.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PM_Case_Managemnt_API.Models.PM
{
    public class ActivityProgress : CommonModel
    {
        public ActivityProgress()
        {
            ProgressAttachments = new HashSet<ProgressAttachment>();
        }

        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public float ActualBudget { get; set; }
        public float ActualWorked { get; set; }
        public Guid EmployeeValueId { get; set; }
        public virtual Employee EmployeeValue { get; set; } = null!;
        public Guid QuarterId { get; set; }
        public virtual ActivityTargetDivision quarter { get; set; } = null!;
        public string DocumentPath { get; set; } = null!;
        public string FinanceDocumentPath { get; set; } = null!;
        public approvalStatus IsApprovedByCoordinator { get; set; }
        public approvalStatus IsApprovedByFinance { get; set; }
        public approvalStatus IsApprovedByDirector { get; set; }
        public string FinanceApprovalRemark { get; set; } = null!;
        public string CoordinatorApprovalRemark { get; set; } = null!;
        public string DirectorApprovalRemark { get; set; } = null!;
        public string Lat { get; set; } = null!;
        public string Lng { get; set; } = null!;
        public ProgressStatus progressStatus { get; set; }

     
        public  ICollection<ProgressAttachment> ProgressAttachments { get; set; }
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

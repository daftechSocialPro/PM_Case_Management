

using System.ComponentModel.DataAnnotations.Schema;

namespace PM_Case_Managemnt_API.Models.Common
{
    public class ProgramBudgetYear:CommonModel
    {

   
        public string Name { get; set; }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

        [ForeignKey("ProgramBudgetYearId")]
        public virtual ICollection<BudgetYear> BudgetYears { get; set; }
    }
}

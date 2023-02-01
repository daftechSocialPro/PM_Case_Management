﻿
using PM_Case_Managemnt_API.Models.Common;


namespace PM_Case_Managemnt_API.Models.PM
{
   public class Programs : CommonModel
    {

        public string ProgramName { get; set; }     
        public float ProgramPlannedBudget { get; set; }
        public Guid ProgramBudgetYearId { get; set; }
        public ProgramBudgetYear ProgramBudgetYear { get; set; }   

    }
}

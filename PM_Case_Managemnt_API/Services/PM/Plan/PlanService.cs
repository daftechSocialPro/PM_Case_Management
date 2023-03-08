using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Models.PM;

namespace PM_Case_Managemnt_API.Services.PM.Plan
{
    public class PlanService : IPlanService
    {

        private readonly DBContext _dBContext;
        public PlanService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> CreatePlan(PlanDto plan)
        {

            var Plans = new PM_Case_Managemnt_API.Models.PM.Plan
            {
                Id = Guid.NewGuid(),
                BudgetYearId = plan.BudgetYearId,
                HasTask = plan.HasTask,
                PlanName = plan.PlanName,
                PlanWeight = plan.PlanWeight,
                PlandBudget = plan.PlandBudget,
                ProgramId = plan.ProgramId,
                ProjectType = plan.ProjectType == 0 ? ProjectType.Capital : ProjectType.Regular,
                Remark = plan.Remark,
                StructureId = plan.StructureId,
                ProjectManagerId = plan.ProjectManagerId,
                FinanceId = plan.FinanceId,
                CreatedAt = DateTime.Now,

            };
            await _dBContext.AddAsync(Plans);
            await _dBContext.SaveChangesAsync();
            return 1;

        }

        public async Task<List<PlanViewDto>> GetPlans( Guid ? programId)
        
        
        {

            var plans =programId!=null? _dBContext.Plans.Include(x => x.Structure).Include(x => x.ProjectManager).Include(x => x.Finance).Where(x => x.ProgramId == programId):
                _dBContext.Plans.Include(x => x.Structure).Include(x => x.ProjectManager).Include(x => x.Finance);


            return await (from p in plans             
                          
                          select new PlanViewDto
                          {

                              Id = p.Id,
                              PlanName = p.PlanName,
                              PlanWeight = p.PlanWeight,
                              PlandBudget = p.PlandBudget,
                              StructureName = p.Structure.StructureName,
                              RemainingBudget =p.PlandBudget- _dBContext.Tasks.Where(x=>x.PlanId ==p.Id).Sum(x=>x.PlanedBudget),
                              ProjectManager = p.ProjectManager.FullName,
                              FinanceManager = p.Finance.FullName,
                              Director = _dBContext.Employees.Where(x => x.Position == Models.Common.Position.Director&&x.OrganizationalStructureId== p.StructureId).FirstOrDefault().FullName,
                              ProjectType = p.ProjectType.ToString(),
                              NumberOfTask = _dBContext.Tasks.Count(x=>x.PlanId==p.Id),
                              NumberOfActivities = _dBContext.Activities.Include(x=>x.ActivityParent.Task.Plan).Where(x=>x.PlanId==p.Id||x.Task.PlanId==p.Id||x.ActivityParent.Task.PlanId==p.Id).Count(),
                              NumberOfTaskCompleted = _dBContext.Activities.Include(x => x.ActivityParent.Task.Plan).Where(x => x.Status ==Status.Finalized && (x.PlanId == p.Id || x.Task.PlanId == p.Id || x.ActivityParent.Task.PlanId == p.Id)).Count(),
                              HasTask = p.HasTask,


                          }).ToListAsync();




        }
        public async Task<PlanSingleViewDto> GetSinglePlan(Guid planId)
        {


            var tasks = (from t in _dBContext.Tasks.Where(x => x.PlanId == planId)
                        select new TaskVIewDto
                        {
                            Id= t.Id,
                            TaskName = t.TaskDescription,
                            TaskWeight = t.Weight,
                            NumberofActivities =0,
                            FinishedActivitiesNo= 0,
                            TerminatedActivitiesNo= 0,
                            StartDate= DateTime.Now,
                            EndDate= DateTime.Now,
                            NumberOfMembers= 0,
                            HasActivity= t.HasActivityParent,
                            PlannedBudget  = t.PlanedBudget
                        }).ToList();


     
                return await( from p in _dBContext.Plans.Where(x=>x.Id == planId)
                       select new PlanSingleViewDto
                       {
                           Id = p.Id,
                           PlanName = p.PlanName,
                           PlanWeight = p.PlanWeight,
                           PlannedBudget = p.PlandBudget,
                           RemainingBudget = p.PlandBudget - tasks.Sum(x=>x.PlannedBudget),
                           RemainingWeight = float.Parse( (100.0 - tasks.Sum(x=>x.TaskWeight)).ToString()),
                           EndDate = p.PeriodEndAt.ToString(),
                           StartDate = p.PeriodStartAt.ToString(),
                           Tasks = tasks

                       }).FirstOrDefaultAsync();
            





        }


    }
}

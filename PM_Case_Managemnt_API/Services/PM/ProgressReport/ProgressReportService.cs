using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Models.PM;
using System.ComponentModel.DataAnnotations.Schema;
using static PM_Case_Managemnt_API.Controllers.Case.AffairForMobileController;
using static PM_Case_Managemnt_API.Services.PM.ProgressReport.ProgressReportService;

namespace PM_Case_Managemnt_API.Services.PM.ProgressReport
{
    public class ProgressReportService : IProgressReportService
    {

        private readonly DBContext _dBContext;
        public ProgressReportService(DBContext context)
        {
            _dBContext = context;
        }


        public async Task<List<DiagramDto>> GetDirectorLevelPerformance(Guid? BranchId)
        {

            var orgStructures = _dBContext.OrganizationalStructures.Include(x => x.ParentStructure).ToList();
            var employess = _dBContext.Employees.ToList();
            var childs = new List<DiagramDto>();

            var parentStructure = orgStructures.FirstOrDefault(x => x.ParentStructureId == null);
            var BudgetYear = _dBContext.BudgetYears.Single(x => x.RowStatus == RowStatus.Active);




            var DiagramDro = new DiagramDto()
            {


                data = new
                {
                    name = parentStructure.StructureName,
                    weight = "  ( " + GetContribution(parentStructure.Id,parentStructure.Weight, BudgetYear.Id) + "% ) ",
                    head = employess.FirstOrDefault(x => x.OrganizationalStructureId == parentStructure.Id && x.Position == Position.Director)?.Title + " " +
                                           employess.FirstOrDefault(x => x.OrganizationalStructureId == parentStructure.Id && x.Position == Position.Director)?.FullName

                },
                label = parentStructure.StructureName,
                expanded = true,
                type = "organization",
                styleClass = GetContribution(parentStructure.Id, parentStructure.Weight, BudgetYear.Id) < 25.00 ? "bg-danger text-white" :"bg-success text-white",
                id = parentStructure.Id,
                order = parentStructure.Order,
                children = new List<DiagramDto>()

            };

            childs.Add(DiagramDro);

            var remainingStractures = orgStructures.Where(x => x.ParentStructureId != null).OrderBy(x => x.Order).Select(x => x.ParentStructureId).Distinct();

            foreach (var items in remainingStractures)
            {
                var children = orgStructures.Where(x => x.ParentStructureId == items).Select(x => new DiagramDto
                {
                    data = new
                    {
                        name = x.StructureName,
                        weight = "  ( Performance: " + GetContribution(x.Id, x.Weight, BudgetYear.Id) + " ) ",
                        head = employess.FirstOrDefault(x => x.OrganizationalStructureId == x.Id && x.Position == Position.Director)?.Title + " " +
                                       employess.FirstOrDefault(x => x.OrganizationalStructureId == x.Id && x.Position == Position.Director)?.FullName

                    },

                    label = x.StructureName,
                    expanded = true,
                    type = "organization",
                    styleClass = GetContribution(x.Id, x.Weight, BudgetYear.Id)<25.00 ? "bg-danger text-white" : "bg-success text-white",
                    id = x.Id,
                    parentId = x.ParentStructureId,
                    order = x.Order,
                    children = new List<DiagramDto>()
                }).ToList();


                childs.AddRange(children);


            }
            for (var j = childs.Max(x => x.order); j >= 0; j--)
            {
                var childList = childs.Where(x => x.order == j).ToList();
                foreach (var item in childList)
                {

                    var org = childs.FirstOrDefault(x => x.id == item.parentId);

                    if (org != null)
                    {
                        org.children.Add(item);
                    }


                }
            }
            List<DiagramDto> result = new List<DiagramDto>();

            if (childs.Any())
            {
                result.Add(childs[0]);
            }
            return result;



        }


        public async Task<PlanReportByProgramDto> PlanReportByProgram(string BudgetYear, string ReportBy)
        {
            PlanReportByProgramDto prbp = new PlanReportByProgramDto();
           
            List<ProgramViewModel> ProgramViewModelList = new List<ProgramViewModel>();
            try
            {
                if (BudgetYear != null)
                {

                    int budgetYearPlan = Convert.ToInt32(BudgetYear);
                    var BudgetYearValue = _dBContext.BudgetYears.Single(x => x.Year == budgetYearPlan);
                    var ProgLists = _dBContext.Programs.Where(x => x.ProgramBudgetYearId == BudgetYearValue.ProgramBudgetYearId).OrderBy(x => x.CreatedAt).ToList();
                    string MeasurementName = "";
                    foreach (var items in ProgLists)
                    {
                        ProgramViewModel programView = new ProgramViewModel();
                        programView.ProgramName = items.ProgramName;
                        programView.ProgramPlanViewModels = new List<ProgramPlanViewModel>();
                        var plansLst = _dBContext.Plans.Where(x => x.ProgramId == items.Id).OrderBy(x => x.CreatedAt).ToList();
                        foreach (var planItems in plansLst)
                        {
                            ProgramPlanViewModel ProgramPlanData = new ProgramPlanViewModel();
                            ProgramPlanData.FiscalPlanPrograms = new List<FiscalPlanProgram>();
                            ProgramPlanData.ProgramName = items.ProgramName;
                            if (!planItems.HasTask)
                            {
                                var PlanActivities = _dBContext.Activities.Include(x => x.UnitOfMeasurement).SingleOrDefault(x => x.PlanId == planItems.Id);
                                if (PlanActivities != null)
                                {
                                    ProgramPlanData.MeasurementUnit = PlanActivities.UnitOfMeasurement.Name;
                                    ProgramPlanData.PlanNAme = planItems.PlanName;

                                    

                                    ProgramPlanData.TotalBirr = planItems.PlandBudget;
                                    ProgramPlanData.TotalGoal = planItems.Activities.Sum(x => x.Goal);
                                    var TargetActivities = _dBContext.ActivityTargetDivisions.Where(x => x.ActivityId == PlanActivities.Id).OrderBy(x => x.Order).ToList();
                                    foreach (var tarItems in TargetActivities)
                                    {
                                        FiscalPlanProgram progs = new FiscalPlanProgram();
                                        progs.RowORder = tarItems.Order;
                                        progs.FinancialValue = tarItems.TargetBudget;
                                        progs.fisicalValue = tarItems.Target;
                                        ProgramPlanData.FiscalPlanPrograms.Add(progs);
                                    }
                                }

                                programView.ProgramPlanViewModels.Add(ProgramPlanData);
                            }
                            else
                            {

                                var TasksOnPlan = _dBContext.Tasks.Where(x => x.PlanId == planItems.Id).OrderBy(x => x.CreatedAt).ToList();


                                float TotalBirr = 0;

                                List<FiscalPlanProgram> fsForPlan = new List<FiscalPlanProgram>();
                                foreach (var taskitems in TasksOnPlan)
                                {
                                    if (taskitems.HasActivityParent == false)
                                    {

                                        var TaskActivities = _dBContext.Activities.Include(x => x.UnitOfMeasurement).FirstOrDefault(x => x.TaskId == taskitems.Id);
                                        if (TaskActivities != null)
                                        {


                                            TotalBirr += taskitems.PlanedBudget;
                                            if (MeasurementName == "")
                                            {
                                                MeasurementName = TaskActivities.UnitOfMeasurement.Name;
                                            }
                                            var TargetForTasks = _dBContext.ActivityTargetDivisions.Include(x => x.Activity).Include(x => x.Activity).ThenInclude(x => x.Task).Where(x => x.ActivityId == TaskActivities.Id).OrderBy(x => x.Order).ToList();
                                            foreach (var tarItems in TargetForTasks)
                                            {
                                                if (!fsForPlan.Where(x => x.RowORder == tarItems.Order).Any())
                                                {
                                                    FiscalPlanProgram progs = new FiscalPlanProgram();
                                                    progs.RowORder = tarItems.Order;
                                                    progs.FinancialValue = tarItems.TargetBudget;
                                                    progs.fisicalValue = (tarItems.Target * tarItems.Activity.Weight) / _dBContext.Activities.Where(x=>x.TaskId== tarItems.Activity.TaskId).Sum(x=>x.Weight);
                                                    //taskwe (float)tarItems.Activity.Task.Weight
                                                    progs.fisicalValue = (UInt32)Math.Round(progs.fisicalValue, 2);

                                                    fsForPlan.Add(progs);
                                                }
                                                else
                                                {
                                                    var AddFT = fsForPlan.Where(x => x.RowORder == tarItems.Order).FirstOrDefault();
                                                    AddFT.FinancialValue += tarItems.TargetBudget;
                                                    AddFT.fisicalValue += (tarItems.Target * tarItems.Activity.Weight) / _dBContext.Activities.Where(x => x.TaskId == tarItems.Activity.TaskId).Sum(x => x.Weight);

                                                }

                                            }
                                        }
                                    }
                                    else
                                    {

                                        var ParentActivities = _dBContext.ActivityParents.Where(x => x.TaskId == taskitems.Id).ToList();


                                        TotalBirr += (float)ParentActivities.Sum(x => x.PlanedBudget);
                                        foreach (var pAct in ParentActivities)
                                        {
                                            foreach (var SubAct in pAct.Activities)
                                            {
                                                var TargetForTasks = _dBContext.ActivityTargetDivisions.Include(x => x.Activity).Include(x => x.Activity).ThenInclude(x => x.ActivityParent).Where(x => x.ActivityId == SubAct.Id).OrderBy(x => x.Order).ToList();
                                                foreach (var tarItems in TargetForTasks)
                                                {
                                                    if (!fsForPlan.Where(x => x.RowORder == tarItems.Order).Any())
                                                    {
                                                        FiscalPlanProgram progs = new FiscalPlanProgram();
                                                        progs.RowORder = tarItems.Order;
                                                        progs.FinancialValue = tarItems.TargetBudget;
                                                        progs.fisicalValue = (tarItems.Target * tarItems.Activity.Weight) / (float)tarItems.Activity.ActivityParent.Task.Weight;
                                                        progs.fisicalValue = (UInt32)Math.Round(progs.fisicalValue, 2);

                                                        fsForPlan.Add(progs);
                                                    }
                                                    else
                                                    {
                                                        var AddFT = fsForPlan.Where(x => x.RowORder == tarItems.Order).FirstOrDefault();
                                                        AddFT.FinancialValue += tarItems.TargetBudget == 0 ? 0 : tarItems.TargetBudget;
                                                        AddFT.fisicalValue += (tarItems.Target * tarItems.Activity.Weight) / (float)tarItems.Activity.ActivityParent.Task.Weight;
                                                        AddFT.fisicalValue = (UInt32)Math.Round(AddFT.fisicalValue, 2);

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                ProgramPlanData.PlanNAme = planItems.PlanName;
                                ProgramPlanData.ProgramName = items.ProgramName;

                                ProgramPlanData.TotalBirr = TotalBirr;
                                ProgramPlanData.TotalGoal = fsForPlan.Sum(x => x.fisicalValue);
                                ProgramPlanData.MeasurementUnit = MeasurementName;
                                ProgramPlanData.FiscalPlanPrograms.AddRange(fsForPlan);
                                programView.ProgramPlanViewModels.Add(ProgramPlanData);
                            }
                        }
                        ProgramViewModelList.Add(programView);
                    }

                    var MonthDeclarator = ProgramViewModelList.Where(x => x.ProgramPlanViewModels.Count() > 0).First().ProgramPlanViewModels.Where(x => x.FiscalPlanPrograms.Count>0).First().FiscalPlanPrograms.ToList();
                   

                    if (MonthDeclarator.Any())
                    {
                        if (ReportBy == "Quarter")
                        {
                            int j = 0;
                            for (int i = 0; i < 4; i++)
                            {
                                j = j + 3;
                                foreach (var progs in ProgramViewModelList)
                                {
                                    foreach (var plns in progs.ProgramPlanViewModels)
                                    {
                                        var newMonth = plns.FiscalPlanPrograms.Where(x => x.RowORder <= j && x.MonthName == null);
                                        FiscalPlanProgram planProgram = new FiscalPlanProgram();
                                        planProgram.RowORder = i + 1;
                                        planProgram.MonthName = "Quarter" + " " + planProgram.RowORder;
                                        planProgram.fisicalValue = newMonth.Sum(x => x.fisicalValue);
                                        planProgram.FinancialValue = newMonth.Sum(x => x.FinancialValue);
                                        plns.FiscalPlanPrograms.Add(planProgram);
                                        plns.FiscalPlanPrograms.RemoveRange(0, newMonth.Count());
                                    }
                                }

                            }
                            MonthDeclarator = ProgramViewModelList.Where(x => x.ProgramPlanViewModels.Count() > 0).First().ProgramPlanViewModels.Where(x => x.FiscalPlanPrograms != null).First().FiscalPlanPrograms.ToList();
                        }
                        else
                        {
                            int newI = 0;
                            for (int i = 1; i <= MonthDeclarator.Count(); i++)
                            {
                                int h = 0;
                                if (i >= 7)
                                {
                                    h = i - 6;
                                }

                                else
                                {
                                    h = i + 6;
                                }

                                System.Globalization.DateTimeFormatInfo mfi = new
                                    System.Globalization.DateTimeFormatInfo();
                                string strMonthName = mfi.GetMonthName(h).ToString();
                                MonthDeclarator.Find(x => x.RowORder == i);
                                MonthDeclarator[newI].MonthName = strMonthName;
                                newI++;
                            }
                        }


                    }


                    prbp.MonthCounts = MonthDeclarator.ToList();

                }


                prbp.ProgramViewModels =  ProgramViewModelList.ToList();
               



                return prbp;
            }
            catch(Exception e)
            {

                return prbp;
            }

          
           
        }


      

        public float GetContribution(Guid structureId,float weight, Guid budetyearId)
        {
            float performance = 0;
            float contribution = 0;
            float Progress = 0;
            var Plans = _dBContext.Plans
                .Include(x => x.Activities)
                .Include(x => x.Tasks).ThenInclude(a => a.Activities)
                .Include(x => x.Tasks).ThenInclude(a => a.ActivitiesParents).ThenInclude(a => a.Activities)
                .Where(x => x.StructureId == structureId && x.BudgetYearId == budetyearId).ToList();

            float Pro_BeginingPlan = 0;
            float Pro_ActualPlan = 0;
            float Pro_Goal = 0;


            foreach (var planItems in Plans)
            {
                float BeginingPlan = 0;
                float ActualPlan = 0;
                float Goal = 0;
                var Tasks = planItems.Tasks;

                if (planItems.HasTask)
                {
                    foreach (var taskItems in Tasks)
                    {


                        if (taskItems.HasActivityParent)
                        {

                            foreach (var actparent in taskItems.ActivitiesParents)
                            {
                                var Activities = actparent.Activities;
                                float BeginingPercent = 0;
                                float ActualWorkedPercent = 0;
                                float GoalPercent = 0;
                                if (!Activities.Any())
                                {
                                    Goal = Goal + planItems.PlanWeight;
                                }
                                foreach (var activityItems in Activities)
                                {
                                    BeginingPercent = BeginingPercent + ((activityItems.Begining * activityItems.Weight) / Activities.Sum(x => x.Weight));
                                    ActualWorkedPercent = ActualWorkedPercent + ((activityItems.ActualWorked * activityItems.Weight) / Activities.Sum(x => x.Weight));
                                    GoalPercent = GoalPercent + ((activityItems.Goal * activityItems.Weight) / Activities.Sum(x => x.Weight));

                                }
                                float taskItemsWeight = actparent.Weight == null ? 0 : (float)actparent.Weight;
                                BeginingPlan = BeginingPlan + ((BeginingPercent * taskItemsWeight) / (float)taskItems.Weight);
                                ActualPlan = ActualPlan + ((ActualWorkedPercent * taskItemsWeight) / (float)taskItems.Weight);
                                Goal = Goal + ((GoalPercent * taskItemsWeight) / (float)taskItems.Weight);
                            }



                        }
                        else
                        {
                            var Activities = taskItems.Activities;
                            float BeginingPercent = 0;
                            float ActualWorkedPercent = 0;
                            float GoalPercent = 0;
                            if (!Activities.Any())
                            {
                                Goal = Goal + planItems.PlanWeight;
                            }
                            foreach (var activityItems in Activities)
                            {
                                BeginingPercent = BeginingPercent + ((activityItems.Begining * activityItems.Weight) / Activities.Sum(x => x.Weight));
                                ActualWorkedPercent = ActualWorkedPercent + ((activityItems.ActualWorked * activityItems.Weight) / Activities.Sum(x => x.Weight));
                                GoalPercent = GoalPercent + ((activityItems.Goal * activityItems.Weight) / Activities.Sum(x => x.Weight));

                            }
                            float taskItemsWeight = taskItems.Weight == null ? 0 : (float)taskItems.Weight;
                            BeginingPlan = BeginingPlan + ((BeginingPercent * taskItemsWeight) / planItems.PlanWeight);
                            ActualPlan = ActualPlan + ((ActualWorkedPercent * taskItemsWeight) / planItems.PlanWeight);
                            Goal = Goal + ((GoalPercent * taskItemsWeight) / planItems.PlanWeight);
                        }
                    }
                }

                else
                {
                    var Activities = planItems.Activities;
                    float BeginingPercent = 0;
                    float ActualWorkedPercent = 0;
                    float GoalPercent = 0;
                    if (!Activities.Any())
                    {
                        Goal = Goal + planItems.PlanWeight;
                    }
                    foreach (var activityItems in Activities)
                    {
                        BeginingPercent = BeginingPercent + ((activityItems.Begining * activityItems.Weight) / Activities.Sum(x => x.Weight));
                        ActualWorkedPercent = ActualWorkedPercent + ((activityItems.ActualWorked * activityItems.Weight) / Activities.Sum(x => x.Weight));
                        GoalPercent = GoalPercent + ((activityItems.Goal * activityItems.Weight) / Activities.Sum(x => x.Weight));

                    }
                    //float taskItemsWeight = taskItems.Weight == null ? 0 : (float)taskItems.Weight;
                    BeginingPlan = BeginingPlan + BeginingPercent;
                    ActualPlan = ActualPlan + ActualWorkedPercent;
                    Goal = Goal + GoalPercent;

                }
                Pro_BeginingPlan = Pro_BeginingPlan + ((BeginingPlan * (float)planItems.PlanWeight) / 100);
                Pro_ActualPlan = Pro_ActualPlan + ((ActualPlan * (float)planItems.PlanWeight) / 100);
                Pro_Goal = Pro_Goal + ((Goal * (float)planItems.PlanWeight) / 100);
            }
            if (Pro_ActualPlan > 0)
            {
                if (Pro_ActualPlan == Pro_Goal)
                {
                    Progress = 100;
                }
                else
                {
                    float Nominator = Pro_ActualPlan;
                    float Denominator = Pro_Goal;
                    Progress = (Nominator / Denominator) * 100;
                }
            }
            else Progress = 0;
            //   Progress.Progress = Progress.Progress = ((ActualPlan - BeginingPlan) / (Progress.Weight - BeginingPlan)) * 100; ;


            performance = ((float)(Progress));
            contribution = (performance * weight) / 100;
            performance = (float)Math.Round(performance, 2);



            contribution = (float)Math.Round(contribution, 2);

            return contribution;
        }


        public class PlanReportByProgramDto
        {
        public    List<ProgramViewModel> ProgramViewModels{ get; set; }
         public   List<FiscalPlanProgram> MonthCounts { get; set; }
        }
        public class ProgramViewModel
        {
            public string ProgramName { get; set; }

            public List<ProgramPlanViewModel> ProgramPlanViewModels { get; set; }
        }

        public class ProgramPlanViewModel
        {

            public string ProgramName { get; set; }

            public string PlanNAme { get; set; }

            public string MeasurementUnit { get; set; }

            public float TotalGoal { get; set; }

            public float TotalBirr { get; set; }


            public List<FiscalPlanProgram> FiscalPlanPrograms { get; set; }

        }

        public class FiscalPlanProgram
        {
            public string PlanNAme { get; set; }


            public int RowORder { get; set; }
            public string MonthName { get; set; }
            public float fisicalValue { get; set; }
            public float FinancialValue { get; set; }

        }
    }
}

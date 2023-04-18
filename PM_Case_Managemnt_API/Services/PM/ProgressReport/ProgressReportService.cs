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

        public async Task<PlanReportDetailDto> StructureReportByProgram(string BudgetYear, string ProgramId, string ReportBy)
        {
            PlanReportDetailDto planReportDetailDto = new PlanReportDetailDto();
            try
            {

               
                if (BudgetYear != null)
                {
                    var MonthDeclarator = new List<ActivityTargetDivisionReport>();
                    List<QuarterMonth> qm = new List<QuarterMonth>();
                    List<ProgramWithStructure> progWithStructure = new List<ProgramWithStructure>();
                    Guid newProgram = Guid.Parse(ProgramId);
                    var StrucsinProg = _dBContext.Plans.Where(x => x.ProgramId == newProgram).Select(x => x.StructureId).Distinct();
                    var Structures = _dBContext.OrganizationalStructures.Where(x => StrucsinProg.Contains(x.Id)).ToList();
                    foreach (var stItems in Structures)
                    {
                        ProgramWithStructure progwithStu = new ProgramWithStructure();
                        progwithStu.StrutureName = stItems.StructureName;
                        progwithStu.StructurePlans = new List<StructurePlan>();
                        var plansinStruc = _dBContext.Plans
                            .Include(x=>x.Tasks).ThenInclude(x=>x.ActivitiesParents).ThenInclude(x=>x.Activities).ThenInclude(x => x.UnitOfMeasurement)
                            .Include(x => x.Tasks).ThenInclude(x => x.Activities).ThenInclude(x => x.UnitOfMeasurement)
                            .Include(x => x.Activities).ThenInclude(x=>x.UnitOfMeasurement)
                            .Include(x => x.Tasks).ThenInclude(x => x.ActivitiesParents).ThenInclude(x => x.Activities).ThenInclude(x => x.ActivityTargetDivisions)
                            .Include(x => x.Tasks).ThenInclude(x => x.Activities).ThenInclude(x => x.ActivityTargetDivisions)
                            .Include(x => x.Activities).ThenInclude(x => x.ActivityTargetDivisions)
                            .Where(x => x.StructureId == stItems.Id && x.ProgramId == newProgram).ToList().OrderBy(x => x.CreatedAt);
                        foreach (var Plitems in plansinStruc)
                        {
                            StructurePlan StrPlan = new StructurePlan();
                            StrPlan.PlanName = Plitems.PlanName;
                            if (Plitems.HasTask)
                            {
                                StrPlan.PlanTasks = new List<PlanTask>();
                                foreach (var taskPl in Plitems.Tasks.OrderBy(x => x.CreatedAt))
                                {
                                    PlanTask pT = new PlanTask();
                                    pT.TaskName = taskPl.TaskDescription;
                                    if (taskPl.HasActivityParent)
                                    {
                                        pT.TaskActivities = new List<TaskActivity>();
                                        var ActInTask = taskPl.ActivitiesParents.ToList().OrderBy(x => x.CreatedAt);
                                        foreach (var parentAct in ActInTask)
                                        {
                                            TaskActivity taskActivity = new TaskActivity();
                                            taskActivity.ActivityName = parentAct.ActivityParentDescription;
                                            if (parentAct.HasActivity)
                                            {
                                                taskActivity.ActSubActivity = new List<ActSubActivity>();
                                                foreach (var subActs in parentAct.Activities.OrderBy(x => x.CreatedAt))
                                                {
                                                    ActSubActivity actSub = new ActSubActivity();
                                                    actSub.SubActivityDescription = subActs.ActivityDescription;
                                                    if (actSub != null)
                                                    {
                                                        actSub.Weight = subActs.Weight;
                                                        actSub.Target = subActs.Goal;
                                                        actSub.UnitOfMeasurement = subActs.UnitOfMeasurement.Name;
                                                        actSub.subActivityTargetDivision = new List<ActivityTargetDivisionReport>();
                                                        foreach (var tp in subActs.ActivityTargetDivisions)
                                                        {
                                                            ActivityTargetDivisionReport PTar = new ActivityTargetDivisionReport();
                                                            PTar.Order = tp.Order;
                                                            PTar.TargetValue = tp.Target;
                                                            actSub.subActivityTargetDivision.Add(PTar);
                                                            if (!MonthDeclarator.Where(x => x.Order == tp.Order).Any())
                                                            {
                                                                MonthDeclarator.Add(PTar);
                                                            }

                                                        }
                                                    }

                                                    taskActivity.ActSubActivity.Add(actSub);
                                                }
                                            }
                                            else
                                            {
                                                taskActivity.ActivityTargetDivision = new List<ActivityTargetDivisionReport>();
                                                var PActTarDiv = parentAct.Activities.FirstOrDefault();
                                                if (PActTarDiv != null)
                                                {
                                                    taskActivity.Weight = PActTarDiv.Weight;
                                                    taskActivity.UnitOfMeasurement = PActTarDiv.UnitOfMeasurement.Name;
                                                    taskActivity.Target = PActTarDiv.Goal;
                                                    foreach (var tp in PActTarDiv.ActivityTargetDivisions)
                                                    {
                                                        ActivityTargetDivisionReport PTar = new ActivityTargetDivisionReport();
                                                        PTar.Order = tp.Order;
                                                        PTar.TargetValue = tp.Target;
                                                        taskActivity.ActivityTargetDivision.Add(PTar);
                                                        if (!MonthDeclarator.Where(x => x.Order == tp.Order).Any())
                                                        {
                                                            MonthDeclarator.Add(PTar);
                                                        }
                                                    }
                                                }
                                            }
                                            pT.TaskActivities.Add(taskActivity);
                                        }
                                    }
                                    else
                                    {
                                        pT.TaskTargetDivision = new List<ActivityTargetDivisionReport>();
                                        var tasTarDiv = taskPl.Activities.FirstOrDefault();
                                        if (tasTarDiv != null)
                                        {
                                            pT.Weight = tasTarDiv.Weight;
                                            pT.UnitOfMeasurement = tasTarDiv.UnitOfMeasurement.Name;
                                            pT.Target = tasTarDiv.Goal;
                                            foreach (var tp in tasTarDiv.ActivityTargetDivisions)
                                            {
                                                ActivityTargetDivisionReport PTar = new ActivityTargetDivisionReport();
                                                PTar.Order = tp.Order;
                                                PTar.TargetValue = tp.Target;
                                                pT.TaskTargetDivision.Add(PTar);
                                                if (!MonthDeclarator.Where(x => x.Order == tp.Order).Any())
                                                {
                                                    MonthDeclarator.Add(PTar);
                                                }
                                            }
                                        }
                                    }
                                    StrPlan.PlanTasks.Add(pT);
                                }
                            }
                            else
                            {
                                StrPlan.PlanTargetDivision = new List<ActivityTargetDivisionReport>();
                                var targetDiv = Plitems.Activities.FirstOrDefault();
                                if (targetDiv != null)
                                {
                                    StrPlan.Weight = targetDiv.Weight;
                                    StrPlan.UnitOfMeasurement = targetDiv.UnitOfMeasurement.Name;
                                    StrPlan.Target = targetDiv.Goal;
                                    foreach (var tP in targetDiv.ActivityTargetDivisions)
                                    {

                                        ActivityTargetDivisionReport PTar = new ActivityTargetDivisionReport();
                                        PTar.Order = tP.Order;
                                        PTar.TargetValue = tP.Target;
                                        StrPlan.PlanTargetDivision.Add(PTar);
                                        if (!MonthDeclarator.Where(x => x.Order == tP.Order).Any())
                                        {
                                            MonthDeclarator.Add(PTar);
                                        }
                                    }
                                }
                            }

                            progwithStu.StructurePlans.Add(StrPlan);
                        }

                        progWithStructure.Add(progwithStu);
                    }

              
                    if (MonthDeclarator.Any())
                    {
                        if (ReportBy == "Quarter")
                        {
                            MonthDeclarator = new List<ActivityTargetDivisionReport>();
                            int j = 0;
                            for (int i = 0; i < 4; i++)
                            {
                                j = j + 3;
                                foreach (var stu in progWithStructure)
                                {
                                    foreach (var progs in stu.StructurePlans)
                                    {
                                        if (progs.PlanTargetDivision != null)
                                        {
                                            var newMonth = progs.PlanTargetDivision.Where(x => x.Order <= j && x.MonthName == null);
                                            ActivityTargetDivisionReport planProgram = new ActivityTargetDivisionReport();
                                            planProgram.Order = i + 1;
                                            planProgram.MonthName = "Quarter" + " " + planProgram.Order;
                                            planProgram.TargetValue = newMonth.Sum(x => x.TargetValue);
                                            progs.PlanTargetDivision.Add(planProgram);
                                            progs.PlanTargetDivision.RemoveRange(0, newMonth.Count());
                                            if (!MonthDeclarator.Where(x => x.Order == planProgram.Order).Any())
                                            {
                                                MonthDeclarator.Add(planProgram);
                                            }
                                        }
                                        else
                                        {
                                            foreach (var task in progs.PlanTasks)
                                            {
                                                if (task.TaskTargetDivision != null)
                                                {
                                                    var newMonthTask = task.TaskTargetDivision.Where(x => x.Order <= j && x.MonthName == null);
                                                    ActivityTargetDivisionReport taskProg = new ActivityTargetDivisionReport();
                                                    taskProg.Order = i + 1;
                                                    taskProg.MonthName = "Quarter" + " " + taskProg.Order;
                                                    taskProg.TargetValue = newMonthTask.Sum(x => x.TargetValue);
                                                    task.TaskTargetDivision.Add(taskProg);
                                                    task.TaskTargetDivision.RemoveRange(0, newMonthTask.Count());
                                                    if (!MonthDeclarator.Where(x => x.Order == taskProg.Order).Any())
                                                    {
                                                        MonthDeclarator.Add(taskProg);
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (var activ in task.TaskActivities)
                                                    {
                                                        if (activ.ActivityTargetDivision != null)
                                                        {
                                                            var newMonthAct = activ.ActivityTargetDivision.Where(x => x.Order <= j && x.MonthName == null);
                                                            ActivityTargetDivisionReport taskProg = new ActivityTargetDivisionReport();
                                                            taskProg.Order = i + 1;
                                                            taskProg.MonthName = "Quarter" + " " + taskProg.Order;
                                                            taskProg.TargetValue = newMonthAct.Sum(x => x.TargetValue);
                                                            activ.ActivityTargetDivision.Add(taskProg);
                                                            activ.ActivityTargetDivision.RemoveRange(0, newMonthAct.Count());
                                                            if (!MonthDeclarator.Where(x => x.Order == taskProg.Order).Any())
                                                            {
                                                                MonthDeclarator.Add(taskProg);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            foreach (var subAct in activ.ActSubActivity)
                                                            {
                                                                if (subAct.subActivityTargetDivision != null)
                                                                {
                                                                    var newMonthSAct = subAct.subActivityTargetDivision.Where(x => x.Order <= j && x.MonthName == null);
                                                                    ActivityTargetDivisionReport taskProg = new ActivityTargetDivisionReport();
                                                                    taskProg.Order = i + 1;
                                                                    taskProg.MonthName = "Quarter" + " " + taskProg.Order;
                                                                    taskProg.TargetValue = newMonthSAct.Sum(x => x.TargetValue);
                                                                    subAct.subActivityTargetDivision.Add(taskProg);
                                                                    subAct.subActivityTargetDivision.RemoveRange(0, newMonthSAct.Count());
                                                                    if (!MonthDeclarator.Where(x => x.Order == taskProg.Order).Any())
                                                                    {
                                                                        MonthDeclarator.Add(taskProg);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                         
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
                                MonthDeclarator.Find(x => x.Order == i);
                                MonthDeclarator[newI].MonthName = strMonthName;
                                newI++;
                            }
                        }


                    }


                    planReportDetailDto.MonthCounts = MonthDeclarator.ToList();
                    planReportDetailDto.ProgramWithStructure = progWithStructure.ToList();
                }

                return planReportDetailDto;
            


            }
            catch (Exception e)
            {
                return planReportDetailDto;
            }


        }

        public async Task<PlannedReport> PlanReports(string BudgetYea, Guid selectStructureId, string ReportBy)
        {

            PlannedReport plannedReport = new PlannedReport();

            try
            {

                var BudgetYear = _dBContext.BudgetYears.Single(x => x.Year.ToString() == BudgetYea);

                var allPlans = _dBContext.Plans
                      .Include(x => x.Tasks).ThenInclude(x => x.ActivitiesParents).ThenInclude(x => x.Activities).ThenInclude(x => x.UnitOfMeasurement)
                                 .Include(x => x.Tasks).ThenInclude(x => x.Activities).ThenInclude(x => x.UnitOfMeasurement)
                                 .Include(x => x.Activities).ThenInclude(x => x.UnitOfMeasurement)
                                 .Include(x => x.Tasks).ThenInclude(x => x.ActivitiesParents).ThenInclude(x => x.Activities).ThenInclude(x => x.ActivityTargetDivisions)
                                 .Include(x => x.Tasks).ThenInclude(x => x.Activities).ThenInclude(x => x.ActivityTargetDivisions)
                                 .Include(x => x.Activities).ThenInclude(x => x.ActivityTargetDivisions)

                     .Where(x => x.StructureId == selectStructureId && x.BudgetYearId == BudgetYear.Id).OrderBy(c => c.CreatedAt).ToList();

                List<PlansLst> plansLsts = new List<PlansLst>();

                List<QuarterMonth> QuarterMonth = new List<QuarterMonth>();

                foreach (var plansItems in allPlans)
                {
                    PlansLst plns = new PlansLst();
                    plns.PlanName = plansItems.PlanName;
                    plns.Weight = plansItems.PlanWeight;
                    plns.PlRemark = plansItems.Remark;
                    plns.HasTask = plansItems.HasTask;
                    if (plansItems.HasTask)
                    {
                        List<TaskLst> taskLsts = new List<TaskLst>();

                        var Taskes = plansItems.Tasks.OrderBy(x => x.CreatedAt);
                        foreach (var taskItems in Taskes)
                        {
                            TaskLst taskLst = new TaskLst();
                            taskLst.TaskDescription = taskItems.TaskDescription;
                            taskLst.TaskWeight = taskItems.Weight;
                            taskLst.TRemark = taskItems.Remark;
                            taskLst.HasActParent = taskItems.HasActivityParent;

                            if (taskItems.HasActivityParent)
                            {

                                List<ActParentLst> actparentlsts = new List<ActParentLst>();
                                foreach (var actparent in taskItems.ActivitiesParents)
                                {
                                    ActParentLst actparentlst = new ActParentLst();
                                    actparentlst.ActParentDescription = actparent.ActivityParentDescription;
                                    actparentlst.ActParentWeight = actparent.Weight;
                                    actparentlst.ActpRemark = actparent.Remark;
                                    if (actparent.Activities!=null)
                                    {

                                        List<ActivityLst> activityLsts = new List<ActivityLst>();
                                        foreach (var ActItems in actparent.Activities.Where(x => x.ActivityTargetDivisions != null))
                                        {
                                            ActivityLst lst = new ActivityLst();
                                            lst.ActivityDescription = ActItems.ActivityDescription;
                                            lst.Begining = ActItems.Begining;
                                            lst.MeasurementUnit = ActItems.UnitOfMeasurement.Name.ToString();
                                            lst.Target = ActItems.Goal;
                                            lst.Weight = ActItems.Weight;
                                            lst.Remark = ActItems.Remark;
                                            List<PlanOcc> planOccs = new List<PlanOcc>();

                                            var byQuarter = ActItems.ActivityTargetDivisions.OrderBy(x => x.Order).ToList();
                                            if (!QuarterMonth.Any())
                                            {

                                                int value = ReportBy == reporttype.Quarter.ToString() ? 4 : 12;
                                                if (ReportBy == reporttype.Quarter.ToString())
                                                {
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        var quar = _dBContext.QuarterSettings.Single(x => x.QuarterOrder == i);


                                                        if (quar.StartMonth > 4)
                                                        {
                                                            quar.StartMonth = quar.StartMonth - 4;
                                                        }

                                                        else
                                                        {
                                                            quar.StartMonth = quar.StartMonth + 8;
                                                        }


                                                        if (quar.EndMonth > 4)
                                                        {
                                                            quar.EndMonth = quar.EndMonth - 4;
                                                        }

                                                        else
                                                        {
                                                            quar.EndMonth = quar.EndMonth + 8;
                                                        }



                                                        System.Globalization.DateTimeFormatInfo mfi = new
                                                            System.Globalization.DateTimeFormatInfo();
                                                        string fromG = mfi.GetMonthName(quar.StartMonth).ToString();

                                                        string toG = mfi.GetMonthName(quar.EndMonth).ToString();




                                                        QuarterMonth quarterMonths = new QuarterMonth();
                                                        quarterMonths.MonthName = "Quarter" + (i + 1);
                                                        QuarterMonth.Add(quarterMonths);
                                                    }
                                                    plannedReport.pMINT = 4;
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 12; i++)
                                                    {
                                                        int k = 0;
                                                        if (i >= 7)
                                                        {
                                                            k = i - 6;
                                                        }

                                                        else
                                                        {
                                                            k = i + 6;
                                                        }

                                                        System.Globalization.DateTimeFormatInfo mfi = new
                                                            System.Globalization.DateTimeFormatInfo();
                                                        string strMonthName = mfi.GetMonthName(k).ToString();
                                                        QuarterMonth quarterMonths = new QuarterMonth();
                                                        quarterMonths.MonthName = strMonthName;
                                                        QuarterMonth.Add(quarterMonths);
                                                    }
                                                    plannedReport.pMINT = 12;
                                                }

                                                plannedReport.planDuration = QuarterMonth;
                                            }

                                            if (ReportBy == reporttype.Quarter.ToString())
                                            {

                                                for (int i = 0; i < 12; i += 3)
                                                {

                                                    PlanOcc planO = new PlanOcc();
                                                    planO.PlanTarget = byQuarter[i].Target + byQuarter[i + 1].Target + byQuarter[i + 2].Target;
                                                    planOccs.Add(planO);
                                                }


                                            }
                                            else
                                            {
                                                foreach (var itemQ in byQuarter)
                                                {
                                                    var progresslist = ActItems.ActProgress.Where(x => x.QuarterId == itemQ.Id).ToList();
                                                    PlanOcc planO = new PlanOcc();
                                                    planO.PlanTarget = itemQ.Target;
                                                    planOccs.Add(planO);
                                                }
                                            }
                                            lst.Plans = planOccs;
                                            activityLsts.Add(lst);
                                        }
                                        actparentlst.activityLsts = activityLsts;
                                        actparentlsts.Add(actparentlst);
                                    }
                                    else if (actparent.Activities.Any() && actparent.Activities.FirstOrDefault().targetDivision != null)
                                    {
                                        List<PlanOcc> planoccPlan = new List<PlanOcc>();
                                        var Pocu = actparent.Activities.FirstOrDefault();
                                        if (Pocu != null)
                                        {
                                            actparentlst.Target = Pocu.Goal;
                                            actparentlst.ActualWorked = Pocu.ActualWorked;
                                            actparentlst.MeasurementUnit = Pocu.UnitOfMeasurement.Name;
                                            actparentlst.Begining = Pocu.Begining;
                                            var byQuarter = Pocu.ActivityTargetDivisions.OrderBy(x => x.Order).ToList();
                                            if (!QuarterMonth.Any())
                                            {

                                                int value = ReportBy == reporttype.Quarter.ToString() ? 4 : 12;
                                                if (ReportBy == reporttype.Quarter.ToString())
                                                {
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        var quar = _dBContext.QuarterSettings.Single(x => x.QuarterOrder == i);


                                                        if (quar.StartMonth > 4)
                                                        {
                                                            quar.StartMonth = quar.StartMonth - 4;
                                                        }

                                                        else
                                                        {
                                                            quar.StartMonth = quar.StartMonth + 8;
                                                        }


                                                        if (quar.EndMonth > 4)
                                                        {
                                                            quar.EndMonth = quar.EndMonth - 4;
                                                        }

                                                        else
                                                        {
                                                            quar.EndMonth = quar.EndMonth + 8;
                                                        }



                                                        System.Globalization.DateTimeFormatInfo mfi = new
                                                            System.Globalization.DateTimeFormatInfo();
                                                        string fromG = mfi.GetMonthName(quar.StartMonth).ToString();

                                                        string toG = mfi.GetMonthName(quar.EndMonth).ToString();




                                                        QuarterMonth quarterMonths = new QuarterMonth();
                                                        quarterMonths.MonthName = "Quarter" + (i + 1);
                                                        QuarterMonth.Add(quarterMonths);
                                                    }
                                                    plannedReport.pMINT = 4;
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 12; i++)
                                                    {
                                                        int k = 0;
                                                        if (i >= 7)
                                                        {
                                                            k = i - 6;
                                                        }

                                                        else
                                                        {
                                                            k = i + 6;
                                                        }

                                                        System.Globalization.DateTimeFormatInfo mfi = new
                                                            System.Globalization.DateTimeFormatInfo();
                                                        string strMonthName = mfi.GetMonthName(k).ToString();
                                                        //int fromG = XAPI.EthiopicDateTime.GetGregorianMonth(9, k, 1984);
                                                        //DateTime date = new DateTime(1984, fromG, 9);
                                                        QuarterMonth quarterMonths = new QuarterMonth();
                                                        quarterMonths.MonthName = strMonthName;
                                                        QuarterMonth.Add(quarterMonths);
                                                    }
                                                    plannedReport.pMINT = 12;
                                                }
                                                plannedReport.planDuration = QuarterMonth;
                                            }
                                            if (ReportBy == reporttype.Quarter.ToString())
                                            {

                                                for (int i = 0; i < 12; i += 3)
                                                {

                                                    PlanOcc planO = new PlanOcc();
                                                    planO.PlanTarget = byQuarter[i].Target + byQuarter[i + 1].Target + byQuarter[i + 2].Target;
                                                    planoccPlan.Add(planO);
                                                }
                                            }
                                            else
                                            {
                                                foreach (var itemQ in byQuarter)
                                                {
                                                    var progresslist = Pocu.ActProgress.Where(x => x.QuarterId == itemQ.Id).ToList();
                                                    PlanOcc planO = new PlanOcc();
                                                    planO.PlanTarget = itemQ.Target;
                                                    planoccPlan.Add(planO);
                                                }
                                            }
                                            actparentlst.ActDivisions = planoccPlan;

                                        }

                                        // actparentlst.Add(plns);

                                        actparentlsts.Add(actparentlst);
                                    }

                                }
                                taskLst.ActParentLst = actparentlsts;
                                taskLsts.Add(taskLst);

                            }
                            else if (taskItems.Activities.Any() && taskItems.Activities.FirstOrDefault().ActivityTargetDivisions != null)
                            {

                                var Acti = taskItems.Activities.FirstOrDefault();
                                if (Acti != null)
                                {
                                    taskLst.Begining = Acti.Begining;
                                    taskLst.MeasurementUnit = Acti.UnitOfMeasurement.Name;
                                    taskLst.Target = Acti.Goal;
                                    List<PlanOcc> planOccs = new List<PlanOcc>();
                                    var byQuarter = Acti.ActivityTargetDivisions.OrderBy(x => x.Order).ToList();
                                    if (!QuarterMonth.Any())
                                    {

                                        int value = ReportBy == reporttype.Quarter.ToString() ? 4 : 12;
                                        if (ReportBy == reporttype.Quarter.ToString())
                                        {
                                            for (int i = 0; i < 4; i++)
                                            {
                                                var quar = _dBContext.QuarterSettings.Single(x => x.QuarterOrder == i);

                                                if (quar.StartMonth > 4)
                                                {
                                                    quar.StartMonth = quar.StartMonth - 4;
                                                }

                                                else
                                                {
                                                    quar.StartMonth = quar.StartMonth + 8;
                                                }


                                                if (quar.EndMonth > 4)
                                                {
                                                    quar.EndMonth = quar.EndMonth - 4;
                                                }

                                                else
                                                {
                                                    quar.EndMonth = quar.EndMonth + 8;
                                                }



                                                System.Globalization.DateTimeFormatInfo mfi = new
                                                    System.Globalization.DateTimeFormatInfo();
                                                string fromG = mfi.GetMonthName(quar.StartMonth).ToString();

                                                string toG = mfi.GetMonthName(quar.EndMonth).ToString();




                                                QuarterMonth quarterMonths = new QuarterMonth();
                                                quarterMonths.MonthName = "Quarter" + (i + 1);
                                                QuarterMonth.Add(quarterMonths);
                                            }
                                            plannedReport.pMINT = 4;
                                        }
                                        else
                                        {
                                            for (int i = 1; i <= 12; i++)
                                            {
                                                int k = 0;
                                                if (i >= 7)
                                                {
                                                    k = i - 6;
                                                }

                                                else
                                                {
                                                    k = i + 6;
                                                }

                                                System.Globalization.DateTimeFormatInfo mfi = new
                                                    System.Globalization.DateTimeFormatInfo();
                                                string strMonthName = mfi.GetMonthName(k).ToString();
                                                //int fromG = XAPI.EthiopicDateTime.GetGregorianMonth(9, k, 1984);
                                                //DateTime date = new DateTime(1984, fromG, 9);
                                                QuarterMonth quarterMonths = new QuarterMonth();
                                                quarterMonths.MonthName = strMonthName;
                                                QuarterMonth.Add(quarterMonths);
                                            }
                                            plannedReport.pMINT = 12;
                                        }
                                       plannedReport.planDuration = QuarterMonth;
                                    }
                                    if (ReportBy == reporttype.Quarter.ToString())
                                    {

                                        for (int i = 0; i < 12; i += 3)
                                        {

                                            PlanOcc planO = new PlanOcc();
                                            planO.PlanTarget = byQuarter[i].Target + byQuarter[i + 1].Target + byQuarter[i + 2].Target;
                                            planOccs.Add(planO);
                                        }


                                    }
                                    else
                                    {
                                        foreach (var itemQ in byQuarter)
                                        {
                                            var progresslist = Acti.ActProgress.Where(x => x.QuarterId == itemQ.Id).ToList();
                                            PlanOcc planO = new PlanOcc();
                                            planO.PlanTarget = itemQ.Target;
                                            planOccs.Add(planO);
                                        }
                                    }
                                    taskLst.TaskDivisions = planOccs;
                                    taskLsts.Add(taskLst);
                                }
                            }
                        }
                        plns.taskLsts = taskLsts;
                        plansLsts.Add(plns);
                    }
                    else if (plansItems.Activities.Any() && plansItems.Activities.FirstOrDefault().ActivityTargetDivisions != null)
                    {

                        List<PlanOcc> planoccPlan = new List<PlanOcc>();
                        var Pocu = plansItems.Activities.FirstOrDefault();
                        if (Pocu != null)
                        {
                            plns.Target = Pocu.Goal;
                            plns.ActualWorked = Pocu.ActualWorked;
                            plns.MeasurementUnit = Pocu.UnitOfMeasurement.Name;
                            plns.Begining = Pocu.Begining;
                            var byQuarter = Pocu.ActivityTargetDivisions.OrderBy(x => x.Order).ToList();
                            if (!QuarterMonth.Any())
                            {

                                int value = ReportBy == reporttype.Quarter.ToString() ? 4 : 12;
                                if (ReportBy == reporttype.Quarter.ToString())
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        var quar = _dBContext.QuarterSettings.Single(x => x.QuarterOrder == i);
                                        //DateTime fromG = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(9, quar.StartMonth, 1984));
                                        //DateTime toG = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(9, quar.EndMonth, 1984));


                                        if (quar.StartMonth > 4)
                                        {
                                            quar.StartMonth = quar.StartMonth - 4;
                                        }

                                        else
                                        {
                                            quar.StartMonth = quar.StartMonth + 8;
                                        }


                                        if (quar.EndMonth > 4)
                                        {
                                            quar.EndMonth = quar.EndMonth - 4;
                                        }

                                        else
                                        {
                                            quar.EndMonth = quar.EndMonth + 8;
                                        }



                                        System.Globalization.DateTimeFormatInfo mfi = new
                                            System.Globalization.DateTimeFormatInfo();
                                        string fromG = mfi.GetMonthName(quar.StartMonth).ToString();

                                        string toG = mfi.GetMonthName(quar.EndMonth).ToString();




                                        QuarterMonth quarterMonths = new QuarterMonth();
                                        quarterMonths.MonthName = "Quarter " + (i + 1);
                                        QuarterMonth.Add(quarterMonths);
                                    }
                                    plannedReport.pMINT = 4;
                                }
                                else
                                {
                                    for (int i = 1; i <= 12; i++)
                                    {
                                        int k = 0;
                                        if (i >= 7)
                                        {
                                            k = i - 6;
                                        }

                                        else
                                        {
                                            k = i + 6;
                                        }

                                        System.Globalization.DateTimeFormatInfo mfi = new
                                            System.Globalization.DateTimeFormatInfo();
                                        string strMonthName = mfi.GetMonthName(k).ToString();
                                        //int fromG = XAPI.EthiopicDateTime.GetGregorianMonth(9, k, 1984);
                                        //DateTime date = new DateTime(1984, fromG, 9);
                                        QuarterMonth quarterMonths = new QuarterMonth();
                                        quarterMonths.MonthName = strMonthName;
                                        QuarterMonth.Add(quarterMonths);
                                    }
                                    plannedReport.pMINT = 12;
                                }
                                plannedReport.planDuration = QuarterMonth;
                            }
                            if (ReportBy == reporttype.Quarter.ToString())
                            {

                                for (int i = 0; i < 12; i += 3)
                                {

                                    PlanOcc planO = new PlanOcc();
                                    planO.PlanTarget = byQuarter[i].Target + byQuarter[i + 1].Target + byQuarter[i + 2].Target;
                                    planoccPlan.Add(planO);
                                }
                            }
                            else
                            {
                                foreach (var itemQ in byQuarter)
                                {
                                    var progresslist = Pocu.ActProgress.Where(x => x.QuarterId == itemQ.Id).ToList();
                                    PlanOcc planO = new PlanOcc();
                                    planO.PlanTarget = itemQ.Target;
                                    planoccPlan.Add(planO);
                                }
                            }
                            plns.PlanDivision = planoccPlan;

                        }

                        plansLsts.Add(plns);
                    }
                }

                plannedReport.PlansLst = plansLsts;

                return plannedReport;
            }
            catch (Exception e)
            {
                return plannedReport;
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


        //structure

        public class PlanReportDetailDto
        {
            public List<ProgramWithStructure> ProgramWithStructure { get; set; }
            public List<ActivityTargetDivisionReport> MonthCounts { get; set; }
        }
        public class ProgramWithStructure
        {
            public string StrutureName { get; set; }
            public List<StructurePlan> StructurePlans { get; set; }
        }

        public class StructurePlan
        {
            public string PlanName { get; set; }
            public float? Weight { get; set; }

            public string UnitOfMeasurement { get; set; }

            public float? Target { get; set; }
            public List<PlanTask> PlanTasks { get; set; }
            public List<ActivityTargetDivisionReport> PlanTargetDivision { get; set; }
        }

        public class PlanTask
        {
            public string TaskName { get; set; }
            public float? Weight { get; set; }

            public string UnitOfMeasurement { get; set; }

            public float? Target { get; set; }

            public List<TaskActivity> TaskActivities { get; set; }
            public List<ActivityTargetDivisionReport> TaskTargetDivision { get; set; }
        }

        public class TaskActivity
        {
            public string ActivityName { get; set; }
            public float? Weight { get; set; }

            public string UnitOfMeasurement { get; set; }

            public float? Target { get; set; }
            public List<ActSubActivity> ActSubActivity { get; set; }

            public List<ActivityTargetDivisionReport> ActivityTargetDivision { get; set; }

        }

        public class ActSubActivity
        {
            public string SubActivityDescription { get; set; }
            public float Weight { get; set; }

            public string UnitOfMeasurement { get; set; }

            public float Target { get; set; }

            public List<ActivityTargetDivisionReport> subActivityTargetDivision { get; set; }
        }

        public class ActivityTargetDivisionReport
        {
            public int Order { get; set; }
            public string MonthName { get; set; }
            public float TargetValue { get; set; }
        }
public class QuarterMonth
{
    public string MonthName { get; set; }


}
        // planned report 



        public class PlannedReport
        {
           public List<PlansLst> PlansLst { get; set; }
           public int pMINT { get; set; }
           public   List<QuarterMonth> planDuration { get; set; }
        }
        public class PlansLst
        {
            public string PlanName { get; set; }
            public float Weight { get; set; }
            public string PlRemark { get; set; }
            public bool HasTask { get; set; }
            public float? Begining { get; set; }
            public float? Target { get; set; }
            public float? ActualWorked { get; set; }
            public float? Progress { get; set; }
            public string MeasurementUnit { get; set; }

            public List<TaskLst> taskLsts { get; set; }
            public List<PlanOcc> PlanDivision { get; set; }
        }

        public class TaskLst
        {
            public string TaskDescription { get; set; }
            public float? TaskWeight { get; set; }
            public string TRemark { get; set; }
            public bool HasActParent { get; set; }
            public float? Begining { get; set; }
            public float? Target { get; set; }
            public float? ActualWorked { get; set; }
            public string MeasurementUnit { get; set; }
            public float? Progress { get; set; }
            public List<ActParentLst> ActParentLst { get; set; }
            public List<PlanOcc> TaskDivisions { get; set; }
        }

        public class ActParentLst
        {
            public string ActParentDescription { get; set; }
            public float? ActParentWeight { get; set; }
            public string ActpRemark { get; set; }
            public string MeasurementUnit { get; set; }

            public float? Begining { get; set; }
            public float? Target { get; set; }
            public float? ActualWorked { get; set; }
            public float? Progress { get; set; }
            public List<ActivityLst> activityLsts { get; set; }
            public List<PlanOcc> ActDivisions { get; set; }
        }
        public class ActivityLst
        {
            public string ActivityDescription { get; set; }
            public float Weight { get; set; }
            public string MeasurementUnit { get; set; }

            public float Begining { get; set; }
            public float Target { get; set; }

            public string Remark { get; set; }

            public float ActualWorked { get; set; }
            public float Progress { get; set; }

            public List<PlanOcc> Plans { get; set; }
        }

        public class PlanOcc
        {
            public float PlanTarget { get; set; }
            public float ActualWorked { get; set; }
            public float Percentile { get; set; }
        }

        public enum reporttype
        {
            Quarter,
            Monthly
        }
    }
}

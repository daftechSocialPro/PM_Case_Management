using Azure.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Models.PM;
using PMCaseManagemntAPI.Migrations.DB;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PM_Case_Managemnt_API.Services.PM.Activity
{
    public class ActivityService : IActivityService
    {
        private readonly DBContext _dBContext;
        public ActivityService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> AddActivityDetails(ActivityDetailDto activityDetail)
        {
            ActivityParent activityParent = new ActivityParent();
            activityParent.Id = Guid.NewGuid();
            activityParent.CreatedAt = DateTime.Now;
            activityParent.CreatedBy = activityDetail.CreatedBy;
            activityParent.ActivityParentDescription = activityDetail.ActivityDescription;
            activityParent.HasActivity = activityDetail.HasActivity;
            activityParent.TaskId = activityDetail.TaskId;
            await _dBContext.AddAsync(activityParent);


            foreach (var item in activityDetail.ActivityDetails)
            {


                PM_Case_Managemnt_API.Models.PM.Activity activity = new PM_Case_Managemnt_API.Models.PM.Activity();
                activity.Id = Guid.NewGuid();
                activity.CreatedAt = DateTime.Now;
                activity.CreatedBy = activityParent.CreatedBy;
                activity.ActivityParentId = activityParent.Id;
                activity.ActivityDescription = item.SubActivityDesctiption;
                activity.ActivityType = item.ActivityType == 0 ? ActivityType.Office_Work : ActivityType.Fild_Work;
                activity.Begining = item.PreviousPerformance;
                if (item.CommiteeId != null)
                {
                    activity.CommiteeId = item.CommiteeId;
                }
                activity.FieldWork = item.FieldWork;
                activity.Goal = item.Goal;
                activity.OfficeWork = item.OfficeWork;
                activity.PlanedBudget = item.PlannedBudget;
                activity.UnitOfMeasurementId = item.UnitOfMeasurement;
                activity.Weight = item.Weight;
                if (!string.IsNullOrEmpty(item.StartDate))
                {
                    string[] startDate = item.StartDate.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    DateTime ShouldStartPeriod = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(startDate[0]), Int32.Parse(startDate[1]), Int32.Parse(startDate[2])));
                    activity.ShouldStat = ShouldStartPeriod;
                }

                if (!string.IsNullOrEmpty(item.EndDate))
                {

                    string[] endDate = item.EndDate.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    DateTime ShouldEnd = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(endDate[0]), Int32.Parse(endDate[1]), Int32.Parse(endDate[2])));
                    activity.ShouldEnd = ShouldEnd;
                }
                await _dBContext.Activities.AddAsync(activity);
                await _dBContext.SaveChangesAsync();
                if (item.Employees != null)
                {
                    foreach (var employee in item.Employees)
                    {
                        if (!string.IsNullOrEmpty(employee))
                        {
                            EmployeesAssignedForActivities EAFA = new EmployeesAssignedForActivities
                            {
                                CreatedAt = DateTime.Now,
                                CreatedBy = activityParent.CreatedBy,
                                RowStatus = RowStatus.Active,
                                Id = Guid.NewGuid(),

                                ActivityId = activity.Id,
                                EmployeeId = Guid.Parse(employee),
                            };
                            await _dBContext.EmployeesAssignedForActivities.AddAsync(EAFA);
                            await _dBContext.SaveChangesAsync();
                        }
                    }
                }

            }



            var Task = await _dBContext.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(activityDetail.TaskId));
            if (Task != null)
            {
                var plan = _dBContext.Plans.FirstOrDefaultAsync(x => x.Id.Equals(Task.PlanId)).Result;
                if (plan != null)
                {
                    var ActParent = _dBContext.ActivityParents.Find(activityParent.Id);
                    var Activities = _dBContext.Activities.Where(x => x.ActivityParentId == activityParent.Id);
                    if (ActParent != null && Activities != null)
                    {
                        ActParent.ShouldStartPeriod = Activities.Min(x => x.ShouldStat);
                        ActParent.ShouldEnd = Activities.Max(x => x.ShouldEnd);
                        ActParent.Weight = Activities.Sum(x => x.Weight);
                        _dBContext.SaveChanges();
                    }
                    var ActParents = _dBContext.ActivityParents.Where(x => x.TaskId == Task.Id).ToList();
                    if (Task != null && ActParents != null)
                    {
                        Task.ShouldStartPeriod = ActParents.Min(x => x.ShouldStartPeriod);
                        Task.ShouldEnd = ActParents.Max(x => x.ShouldEnd);
                        Task.Weight = ActParents.Sum(x => x.Weight);
                        _dBContext.SaveChanges();
                    }
                    var tasks = _dBContext.Tasks.Where(x => x.PlanId == plan.Id).ToList();
                    plan.PeriodStartAt = tasks.Min(x => x.ShouldStartPeriod);
                    plan.PeriodEndAt = tasks.Max(x => x.ShouldEnd);
                    _dBContext.SaveChanges();
                }
            }
            return 1;
        }


        public async Task<int> AddTargetActivities(ActivityTargetDivisionDto targetDivisions)
        {

            foreach (var target in targetDivisions.TargetDivisionDtos)
            {

                var targetDivision = new ActivityTargetDivision
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = targetDivisions.CreatedBy,
                    CreatedAt = DateTime.Now,
                    ActivityId = targetDivisions.ActiviyId,
                    Order = target.Order + 1,
                    Target = target.Target,
                    TargetBudget = target.TargetBudget,

                };

                await _dBContext.ActivityTargetDivisions.AddAsync(targetDivision);
                await _dBContext.SaveChangesAsync();
            }





            return 1;

        }

        public async Task<int> AddProgress(AddProgressActivityDto activityProgress)
        {

            var activityProgress2 = new ActivityProgress
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                FinanceDocumentPath = activityProgress.FinacncePath,
                QuarterId = activityProgress.QuarterId,
                ActualBudget = activityProgress.ActualBudget,
                ActualWorked = activityProgress.ActualWorked,
                progressStatus = int.Parse(activityProgress.ProgressStatus) == 0 ? ProgressStatus.SimpleProgress : ProgressStatus.Finalize,
                Remark = activityProgress.Remark,
                ActivityId = activityProgress.ActivityId,
                CreatedBy = activityProgress.CreatedBy,
                EmployeeValueId = activityProgress.EmployeeValueId,
                Lat = activityProgress.lat,
                Lng = activityProgress.lng,
            };

            await _dBContext.ActivityProgresses.AddAsync(activityProgress2);
            await _dBContext.SaveChangesAsync();

            foreach (var file in activityProgress.DcoumentPath)
            {

                var attachment = new ProgressAttachment()
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = activityProgress.CreatedBy,
                    CreatedAt = DateTime.Now,
                    RowStatus = RowStatus.Active,
                    FilePath = file,
                    ActivityProgressId = activityProgress2.Id
                };
                await _dBContext.ProgressAttachments.AddAsync(attachment);
                await _dBContext.SaveChangesAsync();

            }

            var ac = _dBContext.Activities.Find(activityProgress2.ActivityId);
            ac.Status = activityProgress2.progressStatus == ProgressStatus.SimpleProgress ? Status.OnProgress : Status.Finalized;
            if (ac.ActualStart == null)
            {
                ac.ActualStart = DateTime.Now;
            }
            if (activityProgress2.progressStatus == ProgressStatus.Finalize)
            {
                ac.ActualEnd = DateTime.Now;
            }
            ac.ActualWorked += activityProgress2.ActualWorked;
            ac.ActualBudget = _dBContext.ActivityProgresses.Where(x => x.ActivityId == ac.Id && x.IsApprovedByManager == approvalStatus.approved && x.IsApprovedByDirector == approvalStatus.approved && x.IsApprovedByFinance == approvalStatus.approved).Sum(x => x.ActualBudget);


            await _dBContext.SaveChangesAsync();





            return 1;
        }


        public async Task<List<ProgressViewDto>> ViewProgress(Guid actId)
        {


            var progressView = await (from p in _dBContext.ActivityProgresses.Where(x => x.ActivityId == actId)
                                      select new ProgressViewDto
                                      {
                                          Id = p.Id,
                                          ActalWorked = p.ActualWorked,
                                          UsedBudget = p.ActualBudget,
                                          Remark = p.Remark,
                                          IsApprovedByManager = p.IsApprovedByManager.ToString(),
                                          IsApprovedByFinance = p.IsApprovedByFinance.ToString(),
                                          IsApprovedByDirector = p.IsApprovedByDirector.ToString(),
                                          ManagerApprovalRemark = p.CoordinatorApprovalRemark,
                                          FinanceApprovalRemark = p.FinanceApprovalRemark,
                                          DirectorApprovalRemark = p.DirectorApprovalRemark,
                                          FinanceDocument = p.FinanceDocumentPath,
                                          Documents = _dBContext.ProgressAttachments.Where(x => x.ActivityProgressId == p.Id).Select(y => y.FilePath).ToArray(),
                                          CreatedAt = p.CreatedAt

                                      }).ToListAsync();

            return progressView;




        }

        public async Task<List<ActivityViewDto>> GetAssignedActivity(Guid employeeId)
        {

            var employeeAssigned = _dBContext.EmployeesAssignedForActivities.Where(x => x.EmployeeId == employeeId).Select(x => x.ActivityId).ToList();
            List<ActivityViewDto> assignedActivities =
                await (from e in _dBContext.Activities.Include(x => x.UnitOfMeasurement)
                       where employeeAssigned.Contains(e.Id)
                       select new ActivityViewDto
                       {
                           Id = e.Id,
                           Name = e.ActivityDescription,
                           PlannedBudget = e.PlanedBudget,
                           ActivityType = e.ActivityType.ToString(),
                           Weight = e.Weight,
                           Begining = e.Begining,
                           Target = e.Goal,
                           UnitOfMeasurment = e.UnitOfMeasurement.Name,
                           OverAllPerformance = 0,
                           StartDate = e.ShouldStat.ToString(),
                           EndDate = e.ShouldEnd.ToString(),
                           Members = _dBContext.EmployeesAssignedForActivities.Include(x => x.Employee).Where(x => x.ActivityId == e.Id).Select(y => new SelectListDto
                           {
                               Id = y.Id,
                               Name = y.Employee.FullName,
                               Photo = y.Employee.Photo,
                               EmployeeId = y.EmployeeId.ToString(),

                           }).ToList(),
                           MonthPerformance = _dBContext.ActivityTargetDivisions.Where(x => x.ActivityId == e.Id).OrderBy(x => x.Order).Select(y => new MonthPerformanceViewDto
                           {
                               Id = y.Id,
                               Order = y.Order,
                               Planned = y.Target,
                               Actual = 0,
                               Percentage = (0) * 100

                           }).ToList()


                       }
                                    ).ToListAsync();

            return assignedActivities;
        }

        public async Task<List<ActivityViewDto>> GetActivtiesForApproval(Guid employeeId)
        {


            var not = (from p in _dBContext.Plans.Where(x => (x.FinanceId == employeeId || x.ProjectManagerId == employeeId))
                       join a in _dBContext.Activities on p.Id equals a.PlanId
                       join ap in _dBContext.ActivityProgresses on a.Id equals ap.ActivityId
                       select new
                       {

                           ap.Id,
                       }).Union(from p in _dBContext.Plans.Where(x => (x.FinanceId == employeeId || x.ProjectManagerId == employeeId))
                                join t in _dBContext.Tasks on p.Id equals t.PlanId
                                join a in _dBContext.Activities on t.Id equals a.TaskId
                                join ap in _dBContext.ActivityProgresses on a.Id equals ap.ActivityId
                                select new
                                {
                                    ap.Id,
                                }).Union(from p in _dBContext.Plans.Where(x => (x.FinanceId == employeeId || x.ProjectManagerId == employeeId))
                                         join t in _dBContext.Tasks on p.Id equals t.PlanId
                                         join ac in _dBContext.ActivityParents on t.Id equals ac.TaskId
                                         join a in _dBContext.Activities on ac.Id equals a.ActivityParentId
                                         join ap in _dBContext.ActivityProgresses on a.Id equals ap.ActivityId
                                         select new
                                         {
                                             ap.Id,
                                         }).ToList();



            List<ActivityViewDto> actDtos = new List<ActivityViewDto>();



            foreach (var activitprogress in not)
            {

                var activityViewDtos = (from e in _dBContext.ActivityProgresses.Include(x => x.Activity.ActivityParent.Task.Plan.Structure).Where(a => a.Id == activitprogress.Id && (a.IsApprovedByManager == approvalStatus.pending || a.IsApprovedByDirector == approvalStatus.pending || a.IsApprovedByFinance == approvalStatus.pending))
                                            // join ae in _dBContext.EmployeesAssignedForActivities.Include(x=>x.Employee) on e.Id equals ae.ActivityId
                                        select new ActivityViewDto
                                        {
                                            Id = e.Activity.Id,
                                            Name = e.Activity.ActivityDescription,
                                            PlannedBudget = e.Activity.PlanedBudget,
                                            ActivityType = e.Activity.ActivityType.ToString(),
                                            Weight = e.Activity.Weight,
                                            Begining = e.Activity.Begining,
                                            Target = e.Activity.Goal,
                                            UnitOfMeasurment = e.Activity.UnitOfMeasurement.Name,
                                            OverAllPerformance = 0,
                                            StartDate = e.Activity.ShouldStat.ToString(),
                                            EndDate = e.Activity.ShouldEnd.ToString(),
                                            Members = _dBContext.EmployeesAssignedForActivities.Include(x => x.Employee).Where(x => x.ActivityId == e.ActivityId).Select(y => new SelectListDto
                                            {
                                                Id = y.Id,
                                                Name = y.Employee.FullName,
                                                Photo = y.Employee.Photo,
                                                EmployeeId = y.EmployeeId.ToString(),

                                            }).ToList(),
                                            MonthPerformance = _dBContext.ActivityTargetDivisions.Where(x => x.ActivityId == e.ActivityId).OrderBy(x => x.Order).Select(y => new MonthPerformanceViewDto
                                            {
                                                Id = y.Id,
                                                Order = y.Order,
                                                Planned = y.Target,
                                                Actual = 0,
                                                Percentage = (0) * 100

                                            }).ToList(),

                                            ProgresscreatedAt = e.CreatedAt.ToString(),
                                            IsFinance = e.Activity.Plan.FinanceId == employeeId || e.Activity.Task.Plan.FinanceId == employeeId || e.Activity.ActivityParent.Task.Plan.FinanceId == employeeId ? true : false,
                                            IsProjectManager = e.Activity.Plan.ProjectManagerId == employeeId || e.Activity.Task.Plan.ProjectManagerId == employeeId || e.Activity.ActivityParent.Task.Plan.ProjectManagerId == employeeId ? true : false,
                                            IsDirector = _dBContext.EmployeesStructures.Include(x => x.OrganizationalStructure).Any(x => (x.EmployeeId == employeeId && x.Position == Position.Director) && (x.OrganizationalStructureId == e.Activity.Plan.StructureId || x.OrganizationalStructureId == e.Activity.Task.Plan.StructureId || x.OrganizationalStructureId == e.Activity.ActivityParent.Task.Plan.StructureId))


                                        }
                                   ).ToList();
                actDtos.AddRange(activityViewDtos);
            }



            return actDtos.DistinctBy(x => x.Id).ToList();




        //}
    }


}


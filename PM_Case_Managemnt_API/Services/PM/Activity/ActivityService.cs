﻿using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Models.PM;

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
            await _dBContext.SaveChangesAsync();

            foreach (var item in activityDetail.ActivityDetails)
            {

              
               PM_Case_Managemnt_API.Models.PM.Activity activity = new PM_Case_Managemnt_API.Models.PM.Activity();
                activity.Id = Guid.NewGuid();
                activity.CreatedAt = DateTime.Now;
                activity.CreatedBy = activityParent.CreatedBy;
                activity.ActivityParentId = activityParent.Id;
                activity.ActivityDescription = item.SubActivityDesctiption;
                activity.ActivityType = item.ActivityType;
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
                _dBContext.Activities.Add(activity);
                _dBContext.SaveChanges();
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
                            _dBContext.EmployeesAssignedForActivities.Add(EAFA);
                            _dBContext.SaveChanges();
                        }
                    }
                }

            }

            var Task = _dBContext.Tasks.Find(activityDetail.TaskId);
            var plan = _dBContext.Plans.Find(Task.PlanId);
            //if (plan != null)
            //{
            //    var ActParent = _dBContext.ActivityParents.Find(activityParent.Id);
            //    ActParent.ShouldStartPeriod = ActParent.act.Min(x => x.ShouldStat);
            //    ActParent.ShouldEnd = ActParent.Activities.Max(x => x.ShouldEnd);
            //    ActParent.Weight = ActParent.Activities.Sum(x => x.Weight);
            //    //_db.Entry(Task).State = EntityState.Modified;
            //    _dBContext.SaveChanges();
            //    if (Task != null)
            //    {
            //        Task.ShouldStartPeriod = Task.ActivitiesParents.Min(x => x.ShouldStartPeriod);
            //        Task.ShouldEnd = Task.ActivitiesParents.Max(x => x.ShouldEnd);
            //        Task.Weight = Task.ActivitiesParents.Sum(x => x.Weight);
            //        // _db.Entry(Task).State = EntityState.Modified;
            //        _dBContext.SaveChanges();
            //    }
            //    plan.PeriodStartAt = plan.Tasks.Min(x => x.ShouldStartPeriod);
            //    plan.PeriodEndAt = plan.Tasks.Max(x => x.ShouldEnd);
            //    // _db.Entry(plan).State = EntityState.Modified;
            //    _dBContext.SaveChanges();
            //}

            return 1;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
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
          

            foreach (var item in activityDetail.ActivityDetails)
            {

              
               PM_Case_Managemnt_API.Models.PM.Activity activity = new PM_Case_Managemnt_API.Models.PM.Activity();
                activity.Id = Guid.NewGuid();
                activity.CreatedAt = DateTime.Now;
                activity.CreatedBy = activityParent.CreatedBy;
                activity.ActivityParentId = activityParent.Id;
                activity.ActivityDescription = item.SubActivityDesctiption;
                activity.ActivityType =  item.ActivityType==0 ?ActivityType.Office_Work:ActivityType.Fild_Work;
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
                            await  _dBContext.SaveChangesAsync();
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
                    if(ActParent != null && Activities!=null)
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
    }
}

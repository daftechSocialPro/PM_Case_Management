using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Models.PM;
using static PM_Case_Managemnt_API.Services.Common.Dashoboard.DashboardService;
using System.Net;
using PM_Case_Managemnt_API.Data;

namespace PM_Case_Managemnt_API.Controllers.PM
{
    [Route("api/[controller]")]
    [ApiController]
    public class PmForMobileController : ControllerBase
    {

        private readonly DBContext _db ;
        private readonly AuthenticationContext _onContext ;
        List<AboutToExpireProjects> aboutToExpireProjects = new List<AboutToExpireProjects>();
        public Guid structureId = Guid.Empty;

        public PmForMobileController(DBContext db , AuthenticationContext onContext)
        {
            _db = db;
            _onContext = onContext;

        }


        [HttpPost]
        [Route("Chat")]
       

        public IActionResult add_chat(string taskId, Guid employeeId, string description)
        {


            var userId = _onContext.ApplicationUsers.Where(x => x.EmployeesId == employeeId).FirstOrDefault().Id;
            try
            {

                var taskmeo = new TaskMemo();
                taskmeo.TaskId = Guid.Parse(taskId);
                taskmeo.EmployeeId = employeeId;
                taskmeo.Description = description;
                taskmeo.CreatedBy = Guid.Parse(userId);
                taskmeo.CreatedAt = DateTime.Now;
                taskmeo.RowStatus = RowStatus.Active;
                taskmeo.Id = Guid.NewGuid();
                _db.TaskMemos.Add(taskmeo);
                _db.SaveChanges();               

                return Ok();
            }
            catch (Exception ex) {          
                      
            return BadRequest(ex.Message);
            
            }
        }

        [HttpPost]
        [Route("ChatReply")]
       

        public IActionResult Chat_reply(string taskMemoId, string description, Guid employeeId)

        {

            try
            {
                var userId = _onContext.ApplicationUsers.Where(x => x.EmployeesId == employeeId).FirstOrDefault().Id;
                var taskmemoreply = new TaskMemoReply();
                taskmemoreply.TaskMemoId = Guid.Parse(taskMemoId);
                taskmemoreply.EmployeeId = employeeId;
                taskmemoreply.Description = description;

                taskmemoreply.CreatedBy = Guid.Parse(userId);
                taskmemoreply.CreatedAt = DateTime.Now;
                taskmemoreply.RowStatus = RowStatus.Active;
                taskmemoreply.Id = Guid.NewGuid();
                _db.TaskMemoReplies.Add(taskmemoreply);
                _db.SaveChanges();


               
                return Ok();
            }
            catch(Exception ex)
            {


                return BadRequest(ex.Message);
            }
        }




        //     [HttpPost]
        //     [Route("Request_Termination")]


        //     public async Task<HttpResponseMessage> Request_Termination(string activityId, string employeeId, string terminationReason)
        //     {

        //         var employee = _db.Employees.Find(Guid.Parse(employeeId));
        //         var empstrat = employee.StructureId;
        //         var employeeD = _db.Employees.Where(x => x.StructureId == empstrat && x.EmployeeMemberShipLevel == EmployeeMemberShipLevel.Head).FirstOrDefault();
        //         ActivityTerminationHistories acttermination = new ActivityTerminationHistories();
        //         acttermination.Id = Guid.NewGuid();
        //         acttermination.ActivityId = Guid.Parse(activityId);
        //         acttermination.FromEmployeeId = Guid.Parse(employeeId);
        //         acttermination.ApprovedByDirectorId = employeeD.Id;
        //         acttermination.TerminationReason = terminationReason;

        //         //Destination folder 
        //         string uploadFolder = HttpContext.Current.Server.MapPath("~/Content/TerminationDocument/");
        //         uploadFolder = uploadFolder.Replace('\\', '/');
        //         //uploadFolder = uploadFolder.Replace("BSC/BSC", "BSC");

        //         MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(uploadFolder);
        //         MultipartFileStreamProvider multipartFileStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);


        //         if (!streamProvider.FileData.Any())
        //         {
        //             acttermination.DocumentPath = "No Document Attached";
        //         }

        //         if (streamProvider.FileData.Any())
        //         {
        //             int i = 0;
        //             // Get the file names.
        //             foreach (MultipartFileData file in streamProvider.FileData.Where(x => x.Headers.ContentDisposition.Name == "\"finance\""))
        //             {

        //                 if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
        //                 {
        //                     return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
        //                 }
        //                 string fileName = file.Headers.ContentDisposition.FileName;
        //                 if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
        //                 {
        //                     fileName = fileName.Trim('"');
        //                 }
        //                 if (fileName.Contains(@"/") || fileName.Contains(@"\"))
        //                 {
        //                     fileName = Path.GetFileName(fileName);
        //                 }
        //                 string[] fn = fileName.Split('.');
        //                 fileName = acttermination.Id + "." + fn[1];
        //                 File.Move(file.LocalFileName, Path.Combine(uploadFolder, fileName));



        //                 acttermination.DocumentPath = fileName;


        //             }

        //             acttermination.CreatedById = employeeId;
        //             acttermination.CreatedDateTime = DateTime.Now;
        //             acttermination.RowStatus = RowStatus.Active;
        //             _db.ActivityTerminationHistories.Add(acttermination);
        //             _db.SaveChanges();


        //         }




        //         HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //         return response;

        //     }


        //     [HttpPost]
        //     [Route("Approval_Request")]


        //     public HttpResponseMessage Approval_Request(Approval_Type approval_Type, bool status, string ProgressId, string remark)
        //     {
        //         var progress = _db.ActivityProgresses.Find(Guid.Parse(ProgressId));

        //         approvalStatus approvalstatus;
        //         if (status)
        //         {
        //             approvalstatus = approvalStatus.approved;
        //         }
        //         else
        //         {
        //             approvalstatus = approvalStatus.rejected;
        //         }


        //         if (approval_Type == Approval_Type.Finance)
        //         {
        //             progress.isApprovedByFinance = approvalstatus;
        //             progress.FinanceApprovalRemark = remark;
        //         }

        //         else if (approval_Type == Approval_Type.ProjectCordinator)
        //         {
        //             progress.isApprovedByCoordinator = approvalstatus;
        //             progress.CoordinatorApprovalRemark = remark;
        //         }

        //         else if (approval_Type == Approval_Type.Director)
        //         {
        //             progress.isApprovedByDirector = approvalstatus;
        //             progress.DirectorApprovalRemark = remark;
        //         }

        //         _db.Entry(progress).State = EntityState.Modified;
        //         _db.SaveChanges();

        //         HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //         return response;

        //     }

        //     public enum Approval_Type
        //     {
        //         Finance,
        //         ProjectCordinator,
        //         Director
        //     }


        //     [HttpPost]
        //     [Route("Add_ProgressWC")]

        //     public async Task<HttpResponseMessage> Add_ProgressWC(ProgressStatus pros, string quarterId, string activityId, float ActualBudget, float ActualWorked, string EmployeeValueId, string Remark, float lat = 0, float lng = 0)
        //     {

        //         Guid EmployeeId = Guid.Parse(EmployeeValueId);
        //         Guid ActivityId = Guid.Parse(activityId);

        //         ActivityProgress activityProgress = new ActivityProgress();
        //         activityProgress.Id = Guid.NewGuid();
        //         activityProgress.quarterId = Guid.Parse(quarterId);
        //         activityProgress.EmployeeValueId = EmployeeId;
        //         activityProgress.CreatedById = EmployeeValueId;
        //         activityProgress.CreatedDateTime = DateTime.Now;
        //         activityProgress.RowStatus = RowStatus.Active;
        //         activityProgress.ActivityId = ActivityId;
        //         activityProgress.ActualBudget = ActualBudget;
        //         activityProgress.ActualWorked = ActualWorked;
        //         activityProgress.Remark = Remark;
        //         activityProgress.progressStatus = pros;
        //         activityProgress.Lat = lat.ToString(); ;
        //         activityProgress.Lng = lng.ToString();

        //         if (ModelState.IsValid)
        //         {


        //             // Verify that this is an HTML Form file upload request


        //             //Destination folder 
        //             string uploadFolder = HttpContext.Current.Server.MapPath("~/Content/ProgressAttachments/");
        //             uploadFolder = uploadFolder.Replace('\\', '/');

        //             MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(uploadFolder);
        //             MultipartFileStreamProvider multipartFileStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);

        //             if (streamProvider.FileData.Any(x => x.Headers.ContentDisposition.Name == "\"finance\""))
        //             {
        //                 var finance = streamProvider.FileData.Where(x => x.Headers.ContentDisposition.Name == "\"finance\"").FirstOrDefault();
        //                 if (string.IsNullOrEmpty(finance.Headers.ContentDisposition.FileName))
        //                 {
        //                     return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
        //                 }
        //                 string fileName = finance.Headers.ContentDisposition.FileName;
        //                 if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
        //                 {
        //                     fileName = fileName.Trim('"');
        //                 }
        //                 if (fileName.Contains(@"/") || fileName.Contains(@"\"))
        //                 {
        //                     fileName = Path.GetFileName(fileName);
        //                 }
        //                 string[] fn = fileName.Split('.');
        //                 fileName = activityProgress.Id + "-Finance" + "." + fn[1];
        //                 File.Move(finance.LocalFileName, Path.Combine(uploadFolder, fileName));
        //                 activityProgress.financeDocumentPath = fileName;
        //             }
        //             else
        //             {
        //                 activityProgress.financeDocumentPath = "No Document Attached";
        //             }




        //             if (!streamProvider.FileData.Any())
        //             {
        //                 activityProgress.DocumentPath = "No Document Attached";
        //             }
        //             _db.ActivityProgresses.Add(activityProgress);
        //             _db.SaveChanges();

        //             if (streamProvider.FileData.Any())
        //             {
        //                 int i = 0;
        //                 // Get the file names.
        //                 foreach (MultipartFileData file in streamProvider.FileData.Where(x => x.Headers.ContentDisposition.Name == "\"file\""))
        //                 {
        //                     var attachment = new ProgressAttachment();

        //                     attachment.ActivityProgress = activityProgress;
        //                     attachment.Id = Guid.NewGuid();
        //                     attachment.CreatedById = activityProgress.CreatedById;
        //                     attachment.CreatedDateTime = DateTime.Now;
        //                     attachment.RowStatus = RowStatus.Active;
        //                     attachment.ActivityProgressId = activityProgress.Id;


        //                     if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
        //                     {
        //                         return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
        //                     }
        //                     string fileName = file.Headers.ContentDisposition.FileName;
        //                     if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
        //                     {
        //                         fileName = fileName.Trim('"');
        //                     }
        //                     if (fileName.Contains(@"/") || fileName.Contains(@"\"))
        //                     {
        //                         fileName = Path.GetFileName(fileName);
        //                     }
        //                     string[] fn = fileName.Split('.');
        //                     fileName = attachment.Id + "." + fn[1];
        //                     File.Move(file.LocalFileName, Path.Combine(uploadFolder, fileName));



        //                     attachment.FilePath = fileName;

        //                     _db.ProgressAttachment.Add(attachment);
        //                     _db.SaveChanges();
        //                     i++;

        //                 }



        //             }

        //         }



        //         var ac = _db.Activities.Find(activityProgress.ActivityId);
        //         ac.Status = pros == ProgressStatus.SimpleProgress ? Status.OnProgress : Status.Finalized;
        //         if (ac.ActualStart == null)
        //         {
        //             ac.ActualStart = DateTime.Now;
        //         }
        //         if (pros == ProgressStatus.Finalize)
        //         {
        //             ac.ActualEnd = DateTime.Now;
        //         }
        //         ac.ActualWorked += activityProgress.ActualWorked;
        //         ac.ActualBudget = ac.Progress.Where(x => x.isApprovedByCoordinator == approvalStatus.approved && x.isApprovedByDirector == approvalStatus.approved && x.isApprovedByFinance == approvalStatus.approved).Sum(x => x.ActualBudget);
        //         _db.SaveChanges();


        //         HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //         return response;

        //     }





        //     [Route("get-Notification")]

        //     public IHttpActionResult getNotification(Guid empId)
        //     {
        //         var currentUser = _db.Employees.Find(empId);
        //         var notfiyplans = _db.Plans.Where(x => (x.FinanceId == currentUser.Id || x.ProjectCordinatorId == currentUser.Id || (x.StructureId == currentUser.StructureId && currentUser.EmployeeMemberShipLevel == EmployeeMemberShipLevel.Head))
        //&& (x.Activities.Any(z => z.Progress.Any(a => a.isApprovedByCoordinator == approvalStatus.pending || a.isApprovedByDirector == approvalStatus.pending || a.isApprovedByFinance == approvalStatus.pending)))

        //||
        //(x.Tasks.Any(q => q.ActivitiesParents.Any(g => g.Activities.Any(z => z.Progress.Any(a => a.isApprovedByCoordinator == approvalStatus.pending || a.isApprovedByDirector == approvalStatus.pending || a.isApprovedByFinance == approvalStatus.pending)))))
        //||
        //(x.Tasks.Any(y => y.Activities.Any(z => z.Progress.Any(a => a.isApprovedByCoordinator == approvalStatus.pending || a.isApprovedByDirector == approvalStatus.pending || a.isApprovedByFinance == approvalStatus.pending))))).ToList();




        //         List<NotificationViewModel> notifications = new List<NotificationViewModel>();

        //         foreach (var notifyplan in notfiyplans)
        //         {

        //             if (notifyplan.HasTask)
        //             {

        //                 foreach (var task in notifyplan.Tasks)
        //                 {
        //                     if (task.HasActivityParent)
        //                     {

        //                         foreach (var actparent in task.ActivitiesParents)
        //                         {
        //                             foreach (var act in actparent.Activities)
        //                             {
        //                                 foreach (var progress in act.Progress)
        //                                 {
        //                                     if (progress.isApprovedByCoordinator == approvalStatus.pending || progress.isApprovedByDirector == approvalStatus.pending || progress.isApprovedByFinance == approvalStatus.pending)
        //                                     {
        //                                         NotificationViewModel notifyApproval = new NotificationViewModel();
        //                                         notifyApproval.PorgressId = progress.Id;
        //                                         notifyApproval.ProgamName = notifyplan.Program.ProgramName;
        //                                         notifyApproval.PlanName = notifyplan.PlanName;
        //                                         notifyApproval.TaskName = task.TaskDescription;
        //                                         notifyApproval.ActivityName = act.ActivityDescription;
        //                                         notifyApproval.ActivityId = act.Id;
        //                                         notifyApproval.TaskId = progress.Activity.ActivityParent.TaskId;
        //                                         notifyApproval.NotificationType = NotificationType.Approval;
        //                                         notifications.Add(notifyApproval);
        //                                     }
        //                                 }
        //                             }
        //                         }

        //                     }
        //                     else
        //                     {
        //                         foreach (var act in task.Activities)
        //                         {
        //                             foreach (var progress in act.Progress)
        //                             {
        //                                 if (progress.isApprovedByCoordinator == approvalStatus.pending || progress.isApprovedByDirector == approvalStatus.pending || progress.isApprovedByFinance == approvalStatus.pending)
        //                                 {
        //                                     NotificationViewModel notifyApproval = new NotificationViewModel();
        //                                     notifyApproval.PorgressId = progress.Id;
        //                                     notifyApproval.ProgamName = notifyplan.Program.ProgramName;
        //                                     notifyApproval.PlanName = notifyplan.PlanName;
        //                                     notifyApproval.TaskName = task.TaskDescription;
        //                                     notifyApproval.ActivityName = act.ActivityDescription;
        //                                     notifyApproval.ActivityId = act.Id;
        //                                     notifyApproval.TaskId = progress.Activity.TaskId;
        //                                     notifyApproval.NotificationType = NotificationType.Approval;
        //                                     notifications.Add(notifyApproval);
        //                                 }
        //                             }
        //                         }
        //                     }
        //                 }
        //             }
        //             else
        //             {
        //                 foreach (var act in notifyplan.Activities)
        //                 {
        //                     foreach (var progress in act.Progress)
        //                     {
        //                         if (progress.isApprovedByCoordinator == approvalStatus.pending || progress.isApprovedByDirector == approvalStatus.pending || progress.isApprovedByFinance == approvalStatus.pending)
        //                         {
        //                             NotificationViewModel notifyApproval = new NotificationViewModel();
        //                             notifyApproval.PorgressId = progress.Id;
        //                             notifyApproval.ProgamName = notifyplan.Program.ProgramName;
        //                             notifyApproval.PlanName = notifyplan.PlanName;
        //                             notifyApproval.TaskName = "--------------";
        //                             notifyApproval.ActivityName = act.ActivityDescription;
        //                             notifyApproval.ActivityId = act.Id;
        //                             notifyApproval.TaskId = progress.Activity.TaskId;
        //                             notifyApproval.NotificationType = NotificationType.Approval;
        //                             notifications.Add(notifyApproval);
        //                         }
        //                     }
        //                 }
        //             }




        //         }
        //         var Structure_Hierarchy = _db.OrganizationalStructures.Single(x => x.Id == currentUser.StructureId);
        //         if (Structure_Hierarchy.ParentStructureId != null)
        //         {
        //             structureId = Structure_Hierarchy.Id;
        //         }
        //         ExpiredItems();
        //         var notfiyplans2 = aboutToExpireProjects.ToList().OrderBy(x => x.DirectorName).ThenByDescending(x => x.CurrentAchivement);



        //         foreach (var act in notfiyplans2)
        //         {

        //             NotificationViewModel notifyApproval = new NotificationViewModel();
        //             notifyApproval.ProgamName = act.programName;
        //             notifyApproval.PlanName = act.ProjectName;
        //             notifyApproval.TaskName = act.TaskName;
        //             notifyApproval.ActivityId = act.activityId;
        //             notifyApproval.ActivityName = act.ActivityName;
        //             notifyApproval.TaskId = act.TaskId;
        //             notifyApproval.NotificationType = NotificationType.LateProgress;
        //             notifications.Add(notifyApproval);
        //         }


        //         if (notifications == null)
        //         {
        //             return NotFound();
        //         }

        //         return Ok(notifications);

        //     }


        //     public void ExpiredItems()
        //     {
        //         var BudgetYear = _db.BudgetYears.Single(x => x.RowStatus == RowStatus.Active);
        //         int Month = XAPI.EthiopicDateTime.GetEthiopicMonth(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        //         var quarterset = _db.QuarterSettings.Single(x => x.StartMonth == Month || x.EndMonth == Month || x.StartMonth == Month - 1);

        //         if (quarterset.EndMonth / 3 + quarterset.StartMonth <= Month)
        //         {
        //             var AboutToExpireList = _db.ActivityTargetDivision.Where(x => x.order == quarterset.QuarterOrder && x.target != 0).ToList();
        //             if (structureId != Guid.Empty)
        //             {
        //                 AboutToExpireList = AboutToExpireList.Where(x => (x.activity.ActivityParentId != null ? x.activity.ActivityParent.Task.Plan.StructureId : x.activity.TaskId != null ? x.activity.Task.Plan.StructureId : x.activity.Plan.StructureId) == structureId).ToList();
        //             }
        //             foreach (var items in AboutToExpireList)
        //             {
        //                 AboutToExpireProjects aboutToExpire = new AboutToExpireProjects();
        //                 var Progress = _db.ActivityProgresses.Where(x => x.quarterId == items.Id);
        //                 if (Progress.Any())
        //                 {
        //                     if (Progress.Sum(x => x.ActualWorked) < items.target)
        //                     {
        //                         aboutToExpire.DirectorName = items.activity.ActivityParentId != null ? items.activity.ActivityParent.Task.Plan.Structure.StructureName : items.activity.TaskId != null ? items.activity.Task.Plan.Structure.StructureName : items.activity.Plan.Structure.StructureName;
        //                         aboutToExpire.ProjectName = items.activity.ActivityParentId != null ? items.activity.ActivityParent.Task.Plan.PlanName : items.activity.TaskId != null ? items.activity.Task.Plan.PlanName : items.activity.Plan.PlanName;
        //                         aboutToExpire.programName = items.activity.ActivityParentId != null ? (items.activity.ActivityParent.Task.Plan.ProgramId != null ? items.activity.ActivityParent.Task.Plan.Program.ProgramName : "---------") : items.activity.TaskId != null ? (items.activity.Task.Plan.ProgramId != null ? items.activity.Task.Plan.Program.ProgramName : "----------") : (items.activity.Plan.ProgramId != null ? items.activity.Task.Plan.Program.ProgramName : "---------");

        //                         aboutToExpire.activityId = items.activity.Id;
        //                         aboutToExpire.TaskId = items.activity.TaskId;
        //                         aboutToExpire.TaskName = items.activity.Task.TaskDescription;
        //                         aboutToExpire.TaskName = items.activity.ActivityParentId != null ? items.activity.ActivityParent.Task.TaskDescription : items.activity.TaskId != null ? items.activity.Task.TaskDescription : items.activity.Plan.PlanName;

        //                         aboutToExpire.CurrentAchivement = Progress.Sum(x => x.ActualWorked);
        //                         aboutToExpire.RequiredAchivement = items.target;
        //                         aboutToExpireProjects.Add(aboutToExpire);
        //                     }
        //                 }
        //                 else
        //                 {
        //                     aboutToExpire.DirectorName = items.activity.ActivityParentId != null ? items.activity.ActivityParent.Task.Plan.Structure.StructureName : items.activity.TaskId != null ? items.activity.Task.Plan.Structure.StructureName : items.activity.Plan.Structure.StructureName;
        //                     aboutToExpire.ProjectName = items.activity.ActivityParentId != null ? items.activity.ActivityParent.Task.Plan.PlanName : items.activity.TaskId != null ? items.activity.Task.Plan.PlanName : items.activity.Plan.PlanName;
        //                     aboutToExpire.programName = items.activity.ActivityParentId != null ? (items.activity.ActivityParent.Task.Plan.ProgramId != null ? items.activity.ActivityParent.Task.Plan.Program.ProgramName : "---------") : items.activity.TaskId != null ? (items.activity.Task.Plan.ProgramId != null ? items.activity.Task.Plan.Program.ProgramName : "----------") : (items.activity.Plan.ProgramId != null ? items.activity.Task.Plan.Program.ProgramName : "---------");
        //                     aboutToExpire.activityId = items.activity.Id;
        //                     aboutToExpire.TaskId = items.activity.ActivityParentId != null ? items.activity.ActivityParentId : items.activity.TaskId != null ? items.activity.TaskId : items.activity.PlanId;
        //                     aboutToExpire.TaskName = items.activity.ActivityParentId != null ? items.activity.ActivityParent.Task.TaskDescription : items.activity.TaskId != null ? items.activity.Task.TaskDescription : items.activity.Plan.PlanName;
        //                     aboutToExpire.ActivityName = items.activity.ActivityDescription != null ? items.activity.ActivityDescription : items.activity.ActivityParent.ActivityParentDescription;
        //                     aboutToExpire.CurrentAchivement = 0;
        //                     aboutToExpire.RequiredAchivement = items.target;
        //                     aboutToExpireProjects.Add(aboutToExpire);
        //                 }
        //             }
        //         }
        //     }


        //     [Route("get-cordinating-activity")]

        //     public IHttpActionResult getCordinatingAactivity(Guid employeeId)
        //     {
        //         var employee = _db.Employees.Find(employeeId);
        //         var activities = _db.Activities.Where(x => (x.ActivityParentId != null ? x.ActivityParent.Task.Plan.ProjectCordinatorId : x.TaskId != null ? x.Task.Plan.ProjectCordinatorId : x.Plan.ProjectCordinatorId) == employeeId || (x.ActivityParentId != null ? x.ActivityParent.Task.Plan.FinanceId : x.TaskId != null ? x.Task.Plan.FinanceId : x.Plan.FinanceId) == employeeId || ((x.ActivityParentId != null ? x.ActivityParent.Task.Plan.StructureId : x.TaskId != null ? x.Task.Plan.StructureId : x.Plan.StructureId) == employee.StructureId && employee.EmployeeMemberShipLevel == EmployeeMemberShipLevel.Head)).ToList();

        //         var empheadstruct = _db.Employees.Where(x => x.StructureId == employee.StructureId && x.EmployeeMemberShipLevel == EmployeeMemberShipLevel.Head).FirstOrDefault();


        //         var ActVm = new List<ActivityViewModel>();
        //         foreach (var a in activities)
        //         {
        //             ActivityViewModel act = new ActivityViewModel();
        //             act.ProgramName = a.ActivityParentId != null ? (a.ActivityParent.Task.Plan.ProgramId != null ? a.ActivityParent.Task.Plan.Program.ProgramName : "-------------") : a.TaskId != null ? (a.Task.Plan.ProgramId != null ? a.Task.Plan.Program.ProgramName : "-----------") : (a.Plan.ProgramId != null ? a.Plan.Program.ProgramName : "------------");
        //             act.PlanName = a.ActivityParentId != null ? a.ActivityParent.Task.Plan.PlanName : a.TaskId != null ? a.Task.Plan.PlanName : a.Plan.PlanName;
        //             act.TaskName = a.ActivityParentId != null ? a.ActivityParent.Task.TaskDescription : a.TaskId != null ? a.Task.TaskDescription : "--------";
        //             act.ActivityName = a.ActivityParentId != null ? a.ActivityParent.ActivityParentDescription : a.ActivityDescription;
        //             act.TaskId = (a.ActivityParentId != null ? a.ActivityParentId : a.TaskId != null ? a.TaskId : a.PlanId).ToString();
        //             act.ActivityId = a.Id;
        //             act.ShouldStart = a.ShouldStat;
        //             act.ShouldEnd = a.ShouldEnd;
        //             act.ActualEnd = a.ActualEnd;
        //             act.ActualStart = a.ActualStart;
        //             act.PlannedBudget = a.PlanedBudget;
        //             act.ActualBudget = a.ActualBudget;
        //             act.ActualWorked = a.ActualWorked;
        //             if (a.CommiteeId != null)
        //             {
        //                 act.comitteeName = a.Commitee.CommiteeName;

        //                 act.commiteemember = new List<TaskMemberVm>();

        //                 foreach (var comitemem in a.Commitee.employee)
        //                 {
        //                     TaskMemberVm cmember = new TaskMemberVm();
        //                     cmember.EmployeeId = comitemem.EmployeeId.ToString();
        //                     cmember.EmployeeName = comitemem.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + comitemem.Employee.EmployeeFullName;
        //                     cmember.ImagePath = comitemem.Employee.Image;

        //                     act.commiteemember.Add(cmember);
        //                 }


        //             }

        //             act.status = a.Status;
        //             act.ActivityWeight = a.Weight;
        //             act.Begining = a.Begining;
        //             act.Goal = a.Goal;

        //             if (act.ActualWorked > 0)
        //             {
        //                 if (act.ActualWorked == act.Goal)
        //                 {
        //                     act.Percentage = 100;
        //                 }
        //                 else
        //                 {

        //                     float Nominator = (float)act.ActualWorked;
        //                     float Denominator = (float)act.Goal;
        //                     act.Percentage = (Nominator / Denominator) * 100;
        //                 }
        //             }
        //             else act.Percentage = 0;






        //             act.Progress = new List<Actprogress>();
        //             foreach (var p in a.Progress)
        //             {
        //                 Actprogress l = new Actprogress();
        //                 l.PorgressId = p.Id;
        //                 l.ActualBudget = p.ActualBudget;
        //                 l.ActualWorked = p.ActualWorked;
        //                 l.DocumentPath = p.DocumentPath;
        //                 l.FinanceDocumentPath = p.financeDocumentPath;
        //                 l.IsApprovedByCoordinator = p.isApprovedByCoordinator;
        //                 l.IsApprovedByFinance = p.isApprovedByFinance;
        //                 l.IsApprovedByDirector = p.isApprovedByDirector;
        //                 l.Remark = p.Remark;
        //                 l.SentTime = p.CreatedDateTime;
        //                 l.FinanceId = p.Activity.ActivityParentId != null ? p.Activity.ActivityParent.Task.Plan.FinanceId.ToString() : a.TaskId != null ? p.Activity.Task.Plan.FinanceId.ToString() : p.Activity.Plan.FinanceId.ToString();
        //                 l.DirectorId = empheadstruct.Id.ToString();
        //                 l.ProjectCordinatorId = p.Activity.ActivityParentId != null ? p.Activity.ActivityParent.Task.Plan.ProjectCordinatorId.ToString() : a.TaskId != null ? p.Activity.Task.Plan.ProjectCordinatorId.ToString() : p.Activity.Plan.ProjectCordinatorId.ToString();


        //                 l.ProgressAttacment = new List<ActProgressAttachment>();
        //                 foreach (var q in p.ProgressAttachments)
        //                 {
        //                     ActProgressAttachment r = new ActProgressAttachment();
        //                     r.FilePath = q.FilePath;
        //                     l.ProgressAttacment.Add(r);

        //                 }
        //                 var currentuser = _onContext.Users.Find(p.CreatedById);
        //                 var emp = _db.Employees.Find(p.EmployeeValueId);
        //                 l.submittedBy = new TaskMemberVm
        //                 {
        //                     EmployeeId = emp.Id.ToString(),
        //                     EmployeeName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + emp.EmployeeFullName,
        //                     ImagePath = emp.Image

        //                 };


        //                 act.Progress.Add(l);
        //             }
        //             act.AssignedEmployee = new List<TaskMemberVm>();

        //             foreach (var e in a.assignedEmploye)
        //             {
        //                 TaskMemberVm h = new TaskMemberVm();

        //                 h.EmployeeId = e.EmployeeId.ToString();
        //                 h.EmployeeName = e.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + e.Employee.EmployeeFullName;
        //                 h.ImagePath = e.Employee.Image;
        //                 act.AssignedEmployee.Add(h);
        //             }

        //             ActVm.Add(act);
        //         }

        //         if (ActVm == null)
        //         {
        //             return NotFound();
        //         }

        //         return Ok(ActVm);
        //     }

        //     [Route("get-tasks")]

        //     public IHttpActionResult GetTasks(Guid employeeId)
        //     {

        //         var tasks = _db.Taskes.Where(x => x.TaskMember.Any(y => y.EmployeeId == employeeId)).ToList();

        //         var plans = _db.Plans.Where(x => x.TaskMember.Any(y => y.EmployeeId == employeeId));



        //         var tasksVm = new List<TaskViewModel>();
        //         foreach (var t in plans)
        //         {
        //             TaskViewModel task = new TaskViewModel();

        //             task.TaskId = t.Id.ToString();
        //             task.ProgramName = t.Program.ProgramName;
        //             task.PlanName = t.PlanName;
        //             task.TaskName = "-----------";
        //             task.TaskId = "-------------";
        //             task.ShouldStart = t.Activities.Any() ? t.Activities.FirstOrDefault().ShouldStat : DateTime.Now;
        //             task.ActualStart = t.Activities.Any() ? t.Activities.FirstOrDefault()?.ActualStart : DateTime.Now;
        //             task.ShouldEnd = t.Activities?.FirstOrDefault()?.ShouldEnd;
        //             task.ActualEnd = t.Activities?.FirstOrDefault()?.ActualEnd;
        //             task.PlannedBudget = t.PlanedBudget;
        //             task.ActualBudget = t.Activities?.FirstOrDefault()?.ActualBudget;

        //             task.ActualWorked = t.Activities?.FirstOrDefault()?.ActualWorked;
        //             task.TaskWeight = t.Activities?.FirstOrDefault()?.Weight;
        //             task.TaskGoal = t.Activities?.FirstOrDefault()?.Goal;

        //             task.TaskMember = new List<TaskMemberVm>();
        //             foreach (var taskm in t.TaskMember)
        //             {
        //                 TaskMemberVm taskmember = new TaskMemberVm();
        //                 taskmember.EmployeeId = taskm.EmployeeId.ToString();
        //                 taskmember.EmployeeName = taskm.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + taskm.Employee.EmployeeFullName;
        //                 taskmember.ImagePath = taskm.Employee.Image;
        //                 task.TaskMember.Add(taskmember);
        //             }
        //             task.TaskMemo = new List<TaskMemoVm>();

        //             foreach (var taskmemo in t.TaskMemos.OrderBy(x => x.CreatedDateTime))
        //             {
        //                 TaskMemoVm taskm = new TaskMemoVm();
        //                 taskm.TaskMemoId = taskmemo.Id.ToString();
        //                 taskm.EmployeeId = taskmemo.EmployeeId.ToString();
        //                 taskm.EmployeeName = taskmemo.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + taskmemo.Employee.EmployeeFullName;
        //                 taskm.ImagePath = taskmemo.Employee.Image;
        //                 taskm.Message = taskmemo.Description;
        //                 taskm.SentTime = taskmemo.CreatedDateTime;
        //                 taskm.TaskMemoReplay = new List<TaskMemoReplayVm>();
        //                 foreach (var taskmemoreplay in taskmemo.Replies.OrderBy(x => x.CreatedDateTime))
        //                 {
        //                     TaskMemoReplayVm taskmreplay = new TaskMemoReplayVm();
        //                     taskmreplay.TaskMemoReplayId = taskmemoreplay.Id.ToString();
        //                     taskmreplay.EmployeeId = taskmemoreplay.EmployeeId.ToString();
        //                     taskmreplay.EmployeeName = taskmemoreplay.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + taskmemoreplay.Employee.EmployeeFullName;
        //                     taskmreplay.ImagePath = taskmemoreplay.Employee.Image;
        //                     taskmreplay.Message = taskmemoreplay.Description;
        //                     taskmreplay.SentTime = taskmemoreplay.CreatedDateTime;
        //                     taskm.TaskMemoReplay.Add(taskmreplay);
        //                 }

        //                 task.TaskMemo.Add(taskm);
        //             }

        //             task.AssignedActivitty = new List<ActivityViewModel>();



        //             foreach (var a in t.Activities.Where(x => x.assignedEmploye.Any(y => y.EmployeeId == employeeId) && x.targetDivision != null))
        //             {
        //                 ActivityViewModel act = new ActivityViewModel();
        //                 act.ProgramName = a.Plan.Program.ProgramName;
        //                 act.PlanName = a.Plan.PlanName;
        //                 act.TaskName = "----------";
        //                 act.TaskId = "---------";
        //                 act.ActivityName = a.ActivityDescription;
        //                 act.TaskId = a.TaskId.ToString();
        //                 act.ActivityId = a.Id;
        //                 act.ShouldStart = a.ShouldStat;
        //                 act.ShouldEnd = a.ShouldEnd;
        //                 act.ActualEnd = a.ActualEnd;
        //                 act.ActualStart = a.ActualStart;
        //                 act.PlannedBudget = a.PlanedBudget;
        //                 act.ActualBudget = a.ActualBudget;
        //                 act.ActualWorked = a.ActualWorked;
        //                 if (a.CommiteeId != null)
        //                 {
        //                     act.comitteeName = a.Commitee.CommiteeName;

        //                     act.commiteemember = new List<TaskMemberVm>();

        //                     foreach (var comitemem in a.Commitee.employee)
        //                     {
        //                         TaskMemberVm cmember = new TaskMemberVm();
        //                         cmember.EmployeeId = comitemem.EmployeeId.ToString();
        //                         cmember.EmployeeName = comitemem.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + comitemem.Employee.EmployeeFullName;
        //                         cmember.ImagePath = comitemem.Employee.Image;

        //                         act.commiteemember.Add(cmember);
        //                     }


        //                 }

        //                 act.status = a.Status;
        //                 act.ActivityWeight = a.Weight;
        //                 act.Begining = a.Begining;
        //                 act.Goal = a.Goal;

        //                 if (act.ActualWorked > 0)
        //                 {
        //                     if (act.ActualWorked == act.Goal)
        //                     {
        //                         act.Percentage = 100;
        //                     }
        //                     else
        //                     {

        //                         float Nominator = (float)act.ActualWorked;
        //                         float Denominator = (float)act.Goal;
        //                         act.Percentage = (Nominator / Denominator) * 100;
        //                     }
        //                 }
        //                 else act.Percentage = 0;


        //                 act.TargetDivisionVM = new List<TargetDivisionVM>();
        //                 foreach (var o in a.targetdivison.OrderBy(x => x.order))
        //                 {
        //                     var tar = new TargetDivisionVM();
        //                     tar.targetDivisionId = o.Id;
        //                     System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //                     int h = 0;
        //                     var i = o.order;

        //                     if (i >= 7)
        //                     {
        //                         h = i - 6;
        //                     }

        //                     else
        //                     {
        //                         h = i + 6;
        //                     }
        //                     string strMonthName = mfi.GetMonthName(h).ToString();
        //                     tar.target = a.targetDivision == TargetDivision.Monthly ? BaseController.Translate(strMonthName) + "( " + o.target + " %)" : "Quarter " + o.order.ToString() + " (OverAll " + o.target + " %)";
        //                     act.TargetDivisionVM.Add(tar);
        //                 }


        //                 act.Progress = new List<Actprogress>();
        //                 foreach (var p in a.Progress)
        //                 {
        //                     Actprogress l = new Actprogress();
        //                     l.PorgressId = p.Id;
        //                     l.ActualBudget = p.ActualBudget;
        //                     l.ActualWorked = p.ActualWorked;
        //                     l.DocumentPath = p.DocumentPath;
        //                     l.FinanceDocumentPath = p.financeDocumentPath;
        //                     l.IsApprovedByCoordinator = p.isApprovedByCoordinator;
        //                     l.IsApprovedByFinance = p.isApprovedByFinance;
        //                     l.IsApprovedByDirector = p.isApprovedByDirector;
        //                     l.Remark = p.Remark;
        //                     l.SentTime = p.CreatedDateTime;

        //                     var currentuser = _onContext.Users.Find(p.CreatedById);
        //                     var emp = _db.Employees.Find(p.EmployeeValueId);
        //                     l.submittedBy = new TaskMemberVm
        //                     {
        //                         EmployeeId = emp.Id.ToString(),
        //                         EmployeeName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + emp.EmployeeFullName,
        //                         ImagePath = emp.Image

        //                     };

        //                     l.ProgressAttacment = new List<ActProgressAttachment>();
        //                     foreach (var q in p.ProgressAttachments)
        //                     {
        //                         ActProgressAttachment r = new ActProgressAttachment();
        //                         r.FilePath = q.FilePath;
        //                         l.ProgressAttacment.Add(r);

        //                     }


        //                     act.Progress.Add(l);
        //                 }
        //                 act.AssignedEmployee = new List<TaskMemberVm>();

        //                 foreach (var e in a.assignedEmploye)
        //                 {
        //                     TaskMemberVm h = new TaskMemberVm();

        //                     h.EmployeeId = e.EmployeeId.ToString();
        //                     h.EmployeeName = e.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + e.Employee.EmployeeFullName;
        //                     h.ImagePath = e.Employee.Image;
        //                     act.AssignedEmployee.Add(h);
        //                 }

        //                 task.AssignedActivitty.Add(act);
        //             }



        //             task.NotAssignedActivity = new List<ActivityViewModel>();

        //             foreach (var a in t.Activities.Where(x => !x.assignedEmploye.Any(y => y.EmployeeId == employeeId)))
        //             {
        //                 ActivityViewModel act = new ActivityViewModel();
        //                 act.ProgramName = a.Plan.ProgramId != null ? a.Plan.Program.ProgramName : "--------";
        //                 act.PlanName = a.Plan.PlanName;
        //                 act.TaskName = "--------";
        //                 act.ActivityName = a.ActivityDescription;
        //                 act.TaskId = a.PlanId.ToString();
        //                 act.ActivityId = a.Id;
        //                 act.ShouldStart = a.ShouldStat;
        //                 act.ShouldEnd = a.ShouldEnd;
        //                 act.ActualEnd = a.ActualEnd;
        //                 act.ActualStart = a.ActualStart;
        //                 act.PlannedBudget = a.PlanedBudget;
        //                 act.ActualBudget = a.ActualBudget;
        //                 act.ActualWorked = a.ActualWorked;
        //                 if (a.CommiteeId != null)
        //                 {
        //                     act.comitteeName = a.Commitee.CommiteeName;

        //                     act.commiteemember = new List<TaskMemberVm>();

        //                     foreach (var comitemem in a.Commitee.employee)
        //                     {
        //                         TaskMemberVm cmember = new TaskMemberVm();
        //                         cmember.EmployeeId = comitemem.EmployeeId.ToString();
        //                         cmember.EmployeeName = comitemem.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + comitemem.Employee.EmployeeFullName;
        //                         cmember.ImagePath = comitemem.Employee.Image;

        //                         act.commiteemember.Add(cmember);
        //                     }


        //                 }

        //                 act.status = a.Status;
        //                 act.ActivityWeight = a.Weight;
        //                 act.Begining = a.Begining;
        //                 act.Goal = a.Goal;

        //                 if (act.ActualWorked > 0)
        //                 {
        //                     if (act.ActualWorked == act.Goal)
        //                     {
        //                         act.Percentage = 100;
        //                     }
        //                     else
        //                     {

        //                         float Nominator = (float)act.ActualWorked;
        //                         float Denominator = (float)act.Goal;
        //                         act.Percentage = (Nominator / Denominator) * 100;
        //                     }
        //                 }
        //                 else act.Percentage = 0;


        //                 act.Progress = new List<Actprogress>();
        //                 foreach (var p in a.Progress)
        //                 {
        //                     Actprogress l = new Actprogress();
        //                     l.PorgressId = p.Id;
        //                     l.ActualBudget = p.ActualBudget;
        //                     l.ActualWorked = p.ActualWorked;
        //                     l.DocumentPath = p.DocumentPath;
        //                     l.FinanceDocumentPath = p.financeDocumentPath;
        //                     l.IsApprovedByCoordinator = p.isApprovedByCoordinator;
        //                     l.IsApprovedByFinance = p.isApprovedByFinance;
        //                     l.IsApprovedByDirector = p.isApprovedByDirector;
        //                     l.Remark = p.Remark;
        //                     l.SentTime = p.CreatedDateTime;
        //                     var currentuser = _onContext.Users.Find(p.CreatedById);
        //                     var emp = _db.Employees.Find(p.EmployeeValueId);
        //                     l.submittedBy = new TaskMemberVm
        //                     {
        //                         EmployeeId = emp.Id.ToString(),
        //                         EmployeeName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + emp.EmployeeFullName,
        //                         ImagePath = emp.Image

        //                     };
        //                     l.ProgressAttacment = new List<ActProgressAttachment>();
        //                     foreach (var q in p.ProgressAttachments)
        //                     {
        //                         ActProgressAttachment r = new ActProgressAttachment();
        //                         r.FilePath = q.FilePath;
        //                         l.ProgressAttacment.Add(r);

        //                     }


        //                     act.Progress.Add(l);
        //                 }
        //                 act.AssignedEmployee = new List<TaskMemberVm>();

        //                 foreach (var e in a.assignedEmploye)
        //                 {
        //                     TaskMemberVm h = new TaskMemberVm();

        //                     h.EmployeeId = e.EmployeeId.ToString();
        //                     h.EmployeeName = e.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + e.Employee.EmployeeFullName;
        //                     h.ImagePath = e.Employee.Image;
        //                     act.AssignedEmployee.Add(h);
        //                 }

        //                 task.NotAssignedActivity.Add(act);
        //             }




        //             tasksVm.Add(task);
        //         }
        //         foreach (var t in tasks)
        //         {
        //             TaskViewModel task = new TaskViewModel();

        //             task.TaskId = t.Id.ToString();
        //             task.ProgramName = t.Plan.ProgramId != null ? t.Plan.Program.ProgramName : "--------------";
        //             task.PlanName = t.Plan.PlanName;
        //             task.TaskName = t.TaskDescription;
        //             task.ShouldStart = t.ShouldStartPeriod;
        //             task.ActualStart = t.ActuallStart;
        //             task.ShouldEnd = t.ShouldEnd;
        //             task.ActualEnd = t.ActualEnd;
        //             task.PlannedBudget = t.PlanedBudget;
        //             task.ActualBudget = t.ActualBudget;

        //             task.ActualWorked = t.ActualWorked;
        //             task.TaskWeight = t.Weight;
        //             task.TaskGoal = t.Goal;

        //             task.TaskMember = new List<TaskMemberVm>();
        //             foreach (var taskm in t.TaskMember)
        //             {
        //                 TaskMemberVm taskmember = new TaskMemberVm();
        //                 taskmember.EmployeeId = taskm.EmployeeId.ToString();
        //                 taskmember.EmployeeName = taskm.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + taskm.Employee.EmployeeFullName;
        //                 taskmember.ImagePath = taskm.Employee.Image;
        //                 task.TaskMember.Add(taskmember);
        //             }
        //             task.TaskMemo = new List<TaskMemoVm>();

        //             foreach (var taskmemo in t.TaskMemos.OrderBy(x => x.CreatedDateTime))
        //             {
        //                 TaskMemoVm taskm = new TaskMemoVm();
        //                 taskm.TaskMemoId = taskmemo.Id.ToString();
        //                 taskm.EmployeeId = taskmemo.EmployeeId.ToString();
        //                 taskm.EmployeeName = taskmemo.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + taskmemo.Employee.EmployeeFullName;
        //                 taskm.ImagePath = taskmemo.Employee.Image;
        //                 taskm.Message = taskmemo.Description;
        //                 taskm.SentTime = taskmemo.CreatedDateTime;
        //                 taskm.TaskMemoReplay = new List<TaskMemoReplayVm>();
        //                 foreach (var taskmemoreplay in taskmemo.Replies.OrderBy(x => x.CreatedDateTime))
        //                 {
        //                     TaskMemoReplayVm taskmreplay = new TaskMemoReplayVm();
        //                     taskmreplay.TaskMemoReplayId = taskmemoreplay.Id.ToString();
        //                     taskmreplay.EmployeeId = taskmemoreplay.EmployeeId.ToString();
        //                     taskmreplay.EmployeeName = taskmemoreplay.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + taskmemoreplay.Employee.EmployeeFullName;
        //                     taskmreplay.ImagePath = taskmemoreplay.Employee.Image;
        //                     taskmreplay.Message = taskmemoreplay.Description;
        //                     taskmreplay.SentTime = taskmemoreplay.CreatedDateTime;
        //                     taskm.TaskMemoReplay.Add(taskmreplay);
        //                 }

        //                 task.TaskMemo.Add(taskm);
        //             }
        //             if (t.HasActivityParent)
        //             {
        //                 task.AssignedActivitty = new List<ActivityViewModel>();
        //                 task.NotAssignedActivity = new List<ActivityViewModel>();
        //                 foreach (var actaparent in t.ActivitiesParents)
        //                 {


        //                     foreach (var a in actaparent.Activities.Where(x => x.assignedEmploye.Any(y => y.EmployeeId == employeeId) && x.targetDivision != null))
        //                     {
        //                         ActivityViewModel act = new ActivityViewModel();
        //                         act.ProgramName = a.ActivityParent.Task.Plan.Program.ProgramName;
        //                         act.PlanName = a.ActivityParent.Task.Plan.PlanName;
        //                         act.TaskName = a.ActivityParent.Task.TaskDescription;
        //                         act.ActivityName = a.ActivityDescription;
        //                         act.TaskId = a.ActivityParent.TaskId.ToString();
        //                         act.ActivityId = a.Id;
        //                         act.ShouldStart = a.ShouldStat;
        //                         act.ShouldEnd = a.ShouldEnd;
        //                         act.ActualEnd = a.ActualEnd;
        //                         act.ActualStart = a.ActualStart;
        //                         act.PlannedBudget = a.PlanedBudget;
        //                         act.ActualBudget = a.ActualBudget;
        //                         act.ActualWorked = a.ActualWorked;
        //                         if (a.CommiteeId != null)
        //                         {
        //                             act.comitteeName = a.Commitee.CommiteeName;

        //                             act.commiteemember = new List<TaskMemberVm>();

        //                             foreach (var comitemem in a.Commitee.employee)
        //                             {
        //                                 TaskMemberVm cmember = new TaskMemberVm();
        //                                 cmember.EmployeeId = comitemem.EmployeeId.ToString();
        //                                 cmember.EmployeeName = comitemem.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + comitemem.Employee.EmployeeFullName;
        //                                 cmember.ImagePath = comitemem.Employee.Image;

        //                                 act.commiteemember.Add(cmember);
        //                             }


        //                         }

        //                         act.status = a.Status;
        //                         act.ActivityWeight = a.Weight;
        //                         act.Begining = a.Begining;
        //                         act.Goal = a.Goal;

        //                         if (act.ActualWorked > 0)
        //                         {
        //                             if (act.ActualWorked == act.Goal)
        //                             {
        //                                 act.Percentage = 100;
        //                             }
        //                             else
        //                             {

        //                                 float Nominator = (float)act.ActualWorked;
        //                                 float Denominator = (float)act.Goal;
        //                                 act.Percentage = (Nominator / Denominator) * 100;
        //                             }
        //                         }
        //                         else act.Percentage = 0;


        //                         act.TargetDivisionVM = new List<TargetDivisionVM>();
        //                         foreach (var o in a.targetdivison.OrderBy(x => x.order))
        //                         {
        //                             var tar = new TargetDivisionVM();
        //                             tar.targetDivisionId = o.Id;
        //                             System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //                             int h = 0;
        //                             var i = o.order;

        //                             if (i >= 7)
        //                             {
        //                                 h = i - 6;
        //                             }

        //                             else
        //                             {
        //                                 h = i + 6;
        //                             }
        //                             string strMonthName = mfi.GetMonthName(h).ToString();
        //                             tar.target = a.targetDivision == TargetDivision.Monthly ? BaseController.Translate(strMonthName) + "( " + o.target + " %)" : "Quarter " + o.order.ToString() + " (OverAll " + o.target + " %)";
        //                             act.TargetDivisionVM.Add(tar);
        //                         }


        //                         act.Progress = new List<Actprogress>();
        //                         foreach (var p in a.Progress)
        //                         {
        //                             Actprogress l = new Actprogress();
        //                             l.PorgressId = p.Id;
        //                             l.ActualBudget = p.ActualBudget;
        //                             l.ActualWorked = p.ActualWorked;
        //                             l.DocumentPath = p.DocumentPath;
        //                             l.FinanceDocumentPath = p.financeDocumentPath;
        //                             l.IsApprovedByCoordinator = p.isApprovedByCoordinator;
        //                             l.IsApprovedByFinance = p.isApprovedByFinance;
        //                             l.IsApprovedByDirector = p.isApprovedByDirector;
        //                             l.Remark = p.Remark;
        //                             l.SentTime = p.CreatedDateTime;

        //                             var currentuser = _onContext.Users.Find(p.CreatedById);
        //                             var emp = _db.Employees.Find(p.EmployeeValueId);
        //                             l.submittedBy = new TaskMemberVm
        //                             {
        //                                 EmployeeId = emp.Id.ToString(),
        //                                 EmployeeName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + emp.EmployeeFullName,
        //                                 ImagePath = emp.Image

        //                             };

        //                             l.ProgressAttacment = new List<ActProgressAttachment>();
        //                             foreach (var q in p.ProgressAttachments)
        //                             {
        //                                 ActProgressAttachment r = new ActProgressAttachment();
        //                                 r.FilePath = q.FilePath;
        //                                 l.ProgressAttacment.Add(r);

        //                             }


        //                             act.Progress.Add(l);
        //                         }
        //                         act.AssignedEmployee = new List<TaskMemberVm>();

        //                         foreach (var e in a.assignedEmploye)
        //                         {
        //                             TaskMemberVm h = new TaskMemberVm();

        //                             h.EmployeeId = e.EmployeeId.ToString();
        //                             h.EmployeeName = e.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + e.Employee.EmployeeFullName;
        //                             h.ImagePath = e.Employee.Image;
        //                             act.AssignedEmployee.Add(h);
        //                         }

        //                         task.AssignedActivitty.Add(act);
        //                     }





        //                     foreach (var a in actaparent.Activities.Where(x => !x.assignedEmploye.Any(y => y.EmployeeId == employeeId)))
        //                     {
        //                         ActivityViewModel act = new ActivityViewModel();
        //                         act.ProgramName = a.ActivityParent.Task.Plan.ProgramId != null ? a.ActivityParent.Task.Plan.Program.ProgramName : "------------";
        //                         act.PlanName = a.ActivityParent.Task.Plan.PlanName;
        //                         act.TaskName = a.ActivityParent.Task.TaskDescription;
        //                         act.ActivityName = a.ActivityDescription;
        //                         act.TaskId = a.TaskId.ToString();
        //                         act.ActivityId = a.Id;
        //                         act.ShouldStart = a.ShouldStat;
        //                         act.ShouldEnd = a.ShouldEnd;
        //                         act.ActualEnd = a.ActualEnd;
        //                         act.ActualStart = a.ActualStart;
        //                         act.PlannedBudget = a.PlanedBudget;
        //                         act.ActualBudget = a.ActualBudget;
        //                         act.ActualWorked = a.ActualWorked;
        //                         if (a.CommiteeId != null)
        //                         {
        //                             act.comitteeName = a.Commitee.CommiteeName;

        //                             act.commiteemember = new List<TaskMemberVm>();

        //                             foreach (var comitemem in a.Commitee.employee)
        //                             {
        //                                 TaskMemberVm cmember = new TaskMemberVm();
        //                                 cmember.EmployeeId = comitemem.EmployeeId.ToString();
        //                                 cmember.EmployeeName = comitemem.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + comitemem.Employee.EmployeeFullName;
        //                                 cmember.ImagePath = comitemem.Employee.Image;

        //                                 act.commiteemember.Add(cmember);
        //                             }


        //                         }

        //                         act.status = a.Status;
        //                         act.ActivityWeight = a.Weight;
        //                         act.Begining = a.Begining;
        //                         act.Goal = a.Goal;

        //                         if (act.ActualWorked > 0)
        //                         {
        //                             if (act.ActualWorked == act.Goal)
        //                             {
        //                                 act.Percentage = 100;
        //                             }
        //                             else
        //                             {

        //                                 float Nominator = (float)act.ActualWorked;
        //                                 float Denominator = (float)act.Goal;
        //                                 act.Percentage = (Nominator / Denominator) * 100;
        //                             }
        //                         }
        //                         else act.Percentage = 0;


        //                         act.Progress = new List<Actprogress>();
        //                         foreach (var p in a.Progress)
        //                         {
        //                             Actprogress l = new Actprogress();
        //                             l.PorgressId = p.Id;
        //                             l.ActualBudget = p.ActualBudget;
        //                             l.ActualWorked = p.ActualWorked;
        //                             l.DocumentPath = p.DocumentPath;
        //                             l.FinanceDocumentPath = p.financeDocumentPath;
        //                             l.IsApprovedByCoordinator = p.isApprovedByCoordinator;
        //                             l.IsApprovedByFinance = p.isApprovedByFinance;
        //                             l.IsApprovedByDirector = p.isApprovedByDirector;
        //                             l.Remark = p.Remark;
        //                             l.SentTime = p.CreatedDateTime;
        //                             var currentuser = _onContext.Users.Find(p.CreatedById);
        //                             var emp = _db.Employees.Find(p.EmployeeValueId);
        //                             l.submittedBy = new TaskMemberVm
        //                             {
        //                                 EmployeeId = emp.Id.ToString(),
        //                                 EmployeeName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + emp.EmployeeFullName,
        //                                 ImagePath = emp.Image

        //                             };
        //                             l.ProgressAttacment = new List<ActProgressAttachment>();
        //                             foreach (var q in p.ProgressAttachments)
        //                             {
        //                                 ActProgressAttachment r = new ActProgressAttachment();
        //                                 r.FilePath = q.FilePath;
        //                                 l.ProgressAttacment.Add(r);

        //                             }


        //                             act.Progress.Add(l);
        //                         }
        //                         act.AssignedEmployee = new List<TaskMemberVm>();

        //                         foreach (var e in a.assignedEmploye)
        //                         {
        //                             TaskMemberVm h = new TaskMemberVm();

        //                             h.EmployeeId = e.EmployeeId.ToString();
        //                             h.EmployeeName = e.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + e.Employee.EmployeeFullName;
        //                             h.ImagePath = e.Employee.Image;
        //                             act.AssignedEmployee.Add(h);
        //                         }

        //                         task.NotAssignedActivity.Add(act);
        //                     }
        //                 }

        //             }
        //             else
        //             {
        //                 task.AssignedActivitty = new List<ActivityViewModel>();

        //                 foreach (var a in t.Activities.Where(x => x.assignedEmploye.Any(y => y.EmployeeId == employeeId) && x.targetDivision != null))
        //                 {
        //                     ActivityViewModel act = new ActivityViewModel();
        //                     act.ProgramName = a.Task.Plan.Program.ProgramName;
        //                     act.PlanName = a.Task.Plan.PlanName;
        //                     act.TaskName = a.Task.TaskDescription;
        //                     act.ActivityName = a.ActivityDescription;
        //                     act.TaskId = a.TaskId.ToString();
        //                     act.ActivityId = a.Id;
        //                     act.ShouldStart = a.ShouldStat;
        //                     act.ShouldEnd = a.ShouldEnd;
        //                     act.ActualEnd = a.ActualEnd;
        //                     act.ActualStart = a.ActualStart;
        //                     act.PlannedBudget = a.PlanedBudget;
        //                     act.ActualBudget = a.ActualBudget;
        //                     act.ActualWorked = a.ActualWorked;
        //                     if (a.CommiteeId != null)
        //                     {
        //                         act.comitteeName = a.Commitee.CommiteeName;

        //                         act.commiteemember = new List<TaskMemberVm>();

        //                         foreach (var comitemem in a.Commitee.employee)
        //                         {
        //                             TaskMemberVm cmember = new TaskMemberVm();
        //                             cmember.EmployeeId = comitemem.EmployeeId.ToString();
        //                             cmember.EmployeeName = comitemem.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + comitemem.Employee.EmployeeFullName;
        //                             cmember.ImagePath = comitemem.Employee.Image;

        //                             act.commiteemember.Add(cmember);
        //                         }


        //                     }

        //                     act.status = a.Status;
        //                     act.ActivityWeight = a.Weight;
        //                     act.Begining = a.Begining;
        //                     act.Goal = a.Goal;

        //                     if (act.ActualWorked > 0)
        //                     {
        //                         if (act.ActualWorked == act.Goal)
        //                         {
        //                             act.Percentage = 100;
        //                         }
        //                         else
        //                         {

        //                             float Nominator = (float)act.ActualWorked;
        //                             float Denominator = (float)act.Goal;
        //                             act.Percentage = (Nominator / Denominator) * 100;
        //                         }
        //                     }
        //                     else act.Percentage = 0;


        //                     act.TargetDivisionVM = new List<TargetDivisionVM>();
        //                     foreach (var o in a.targetdivison.OrderBy(x => x.order))
        //                     {
        //                         var tar = new TargetDivisionVM();
        //                         tar.targetDivisionId = o.Id;
        //                         System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //                         int h = 0;
        //                         var i = o.order;

        //                         if (i >= 7)
        //                         {
        //                             h = i - 6;
        //                         }

        //                         else
        //                         {
        //                             h = i + 6;
        //                         }
        //                         string strMonthName = mfi.GetMonthName(h).ToString();
        //                         tar.target = a.targetDivision == TargetDivision.Monthly ? BaseController.Translate(strMonthName) + "( " + o.target + " %)" : "Quarter " + o.order.ToString() + " (OverAll " + o.target + " %)";
        //                         act.TargetDivisionVM.Add(tar);
        //                     }


        //                     act.Progress = new List<Actprogress>();
        //                     foreach (var p in a.Progress)
        //                     {
        //                         Actprogress l = new Actprogress();
        //                         l.PorgressId = p.Id;
        //                         l.ActualBudget = p.ActualBudget;
        //                         l.ActualWorked = p.ActualWorked;
        //                         l.DocumentPath = p.DocumentPath;
        //                         l.FinanceDocumentPath = p.financeDocumentPath;
        //                         l.IsApprovedByCoordinator = p.isApprovedByCoordinator;
        //                         l.IsApprovedByFinance = p.isApprovedByFinance;
        //                         l.IsApprovedByDirector = p.isApprovedByDirector;
        //                         l.Remark = p.Remark;
        //                         l.SentTime = p.CreatedDateTime;

        //                         var currentuser = _onContext.Users.Find(p.CreatedById);
        //                         var emp = _db.Employees.Find(p.EmployeeValueId);
        //                         l.submittedBy = new TaskMemberVm
        //                         {
        //                             EmployeeId = emp.Id.ToString(),
        //                             EmployeeName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + emp.EmployeeFullName,
        //                             ImagePath = emp.Image

        //                         };

        //                         l.ProgressAttacment = new List<ActProgressAttachment>();
        //                         foreach (var q in p.ProgressAttachments)
        //                         {
        //                             ActProgressAttachment r = new ActProgressAttachment();
        //                             r.FilePath = q.FilePath;
        //                             l.ProgressAttacment.Add(r);

        //                         }


        //                         act.Progress.Add(l);
        //                     }
        //                     act.AssignedEmployee = new List<TaskMemberVm>();

        //                     foreach (var e in a.assignedEmploye)
        //                     {
        //                         TaskMemberVm h = new TaskMemberVm();

        //                         h.EmployeeId = e.EmployeeId.ToString();
        //                         h.EmployeeName = e.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + e.Employee.EmployeeFullName;
        //                         h.ImagePath = e.Employee.Image;
        //                         act.AssignedEmployee.Add(h);
        //                     }

        //                     task.AssignedActivitty.Add(act);
        //                 }



        //                 task.NotAssignedActivity = new List<ActivityViewModel>();

        //                 foreach (var a in t.Activities.Where(x => !x.assignedEmploye.Any(y => y.EmployeeId == employeeId)))
        //                 {
        //                     ActivityViewModel act = new ActivityViewModel();
        //                     act.ProgramName = a.Task.Plan.Program.ProgramName;
        //                     act.PlanName = a.Task.Plan.PlanName;
        //                     act.TaskName = a.Task.TaskDescription;
        //                     act.ActivityName = a.ActivityDescription;
        //                     act.TaskId = a.TaskId.ToString();
        //                     act.ActivityId = a.Id;
        //                     act.ShouldStart = a.ShouldStat;
        //                     act.ShouldEnd = a.ShouldEnd;
        //                     act.ActualEnd = a.ActualEnd;
        //                     act.ActualStart = a.ActualStart;
        //                     act.PlannedBudget = a.PlanedBudget;
        //                     act.ActualBudget = a.ActualBudget;
        //                     act.ActualWorked = a.ActualWorked;
        //                     if (a.CommiteeId != null)
        //                     {
        //                         act.comitteeName = a.Commitee.CommiteeName;

        //                         act.commiteemember = new List<TaskMemberVm>();

        //                         foreach (var comitemem in a.Commitee.employee)
        //                         {
        //                             TaskMemberVm cmember = new TaskMemberVm();
        //                             cmember.EmployeeId = comitemem.EmployeeId.ToString();
        //                             cmember.EmployeeName = comitemem.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + comitemem.Employee.EmployeeFullName;
        //                             cmember.ImagePath = comitemem.Employee.Image;

        //                             act.commiteemember.Add(cmember);
        //                         }


        //                     }

        //                     act.status = a.Status;
        //                     act.ActivityWeight = a.Weight;
        //                     act.Begining = a.Begining;
        //                     act.Goal = a.Goal;

        //                     if (act.ActualWorked > 0)
        //                     {
        //                         if (act.ActualWorked == act.Goal)
        //                         {
        //                             act.Percentage = 100;
        //                         }
        //                         else
        //                         {

        //                             float Nominator = (float)act.ActualWorked;
        //                             float Denominator = (float)act.Goal;
        //                             act.Percentage = (Nominator / Denominator) * 100;
        //                         }
        //                     }
        //                     else act.Percentage = 0;


        //                     act.Progress = new List<Actprogress>();
        //                     foreach (var p in a.Progress)
        //                     {
        //                         Actprogress l = new Actprogress();
        //                         l.PorgressId = p.Id;
        //                         l.ActualBudget = p.ActualBudget;
        //                         l.ActualWorked = p.ActualWorked;
        //                         l.DocumentPath = p.DocumentPath;
        //                         l.FinanceDocumentPath = p.financeDocumentPath;
        //                         l.IsApprovedByCoordinator = p.isApprovedByCoordinator;
        //                         l.IsApprovedByFinance = p.isApprovedByFinance;
        //                         l.IsApprovedByDirector = p.isApprovedByDirector;
        //                         l.Remark = p.Remark;
        //                         l.SentTime = p.CreatedDateTime;
        //                         var currentuser = _onContext.Users.Find(p.CreatedById);
        //                         var emp = _db.Employees.Find(p.EmployeeValueId);
        //                         l.submittedBy = new TaskMemberVm
        //                         {
        //                             EmployeeId = emp.Id.ToString(),
        //                             EmployeeName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + emp.EmployeeFullName,
        //                             ImagePath = emp.Image

        //                         };
        //                         l.ProgressAttacment = new List<ActProgressAttachment>();
        //                         foreach (var q in p.ProgressAttachments)
        //                         {
        //                             ActProgressAttachment r = new ActProgressAttachment();
        //                             r.FilePath = q.FilePath;
        //                             l.ProgressAttacment.Add(r);

        //                         }


        //                         act.Progress.Add(l);
        //                     }
        //                     act.AssignedEmployee = new List<TaskMemberVm>();

        //                     foreach (var e in a.assignedEmploye)
        //                     {
        //                         TaskMemberVm h = new TaskMemberVm();

        //                         h.EmployeeId = e.EmployeeId.ToString();
        //                         h.EmployeeName = e.Employee.HREmployeeNameTitle.HREmployeeNameTitleName + " " + e.Employee.EmployeeFullName;
        //                         h.ImagePath = e.Employee.Image;
        //                         act.AssignedEmployee.Add(h);
        //                     }

        //                     task.NotAssignedActivity.Add(act);
        //                 }


        //             }

        //             tasksVm.Add(task);
        //         }

        //         if (tasks == null)
        //         {
        //             return NotFound();
        //         }

        //         return Ok(tasksVm);
        //     }


        [HttpGet]
        [Route("login")]

        public IActionResult GetEmployee(string UserName, string Password, string DeviceId)
        {

           try
            {
                var employee = _db.Employees.Where(x => x.MobileUsersMacaddress == DeviceId).FirstOrDefault();
                string device = "";
                if (employee != null)
                {
                    device = employee.MobileUsersMacaddress;
                }

                if (!string.IsNullOrEmpty(device))
                {

                    if (employee.UserName == UserName && employee.Password == Password)
                    {
                        employeeViewModel emp = new employeeViewModel
                        {
                            Id = employee.Id,
                            EmployeeFullName = employee.Title + " " + employee.FullName,
                            EmployeeAmharicFullName = employee.FullName,
                            PhoneNumber = employee.PhoneNumber,
                            Image = employee.Photo,
                            UserName = employee.UserName,
                            MemberShipLevel = employee.Position.ToString(),
                            structure = employee.OrganizationalStructure.StructureName
                        };



                        return Ok(emp);
                    }
                    else
                    {

                        return Ok("UserName or Password Incorrect");
                    }
                }
                else
                {

                    return Ok("Your Mobile Phone is not Authorized");
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);

            }


        }
        //public class NotFoundWithMessageResult : IActionResult
        //{
        //    private string message;

        //    public NotFoundWithMessageResult(string message)
        //    {
        //        this.message = message;
        //    }

        //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        //        response.Content = new StringContent(message);
        //        return System.Threading.Tasks.Task.FromResult(response);
        //    }
        //}



        public class employeeViewModel
        {

            public Guid Id { get; set; }
            public string EmployeeFullName { get; set; }
            public string EmployeeAmharicFullName { get; set; }
            public string PhoneNumber { get; set; }
            public string Image { get; set; }
            public string UserName { get; set; }

            public string MemberShipLevel { get; set; }
            public string structure { get; set; }


        }
        public class TaskViewModel
        {
            public string TaskId { get; set; }
            public string ProgramName { get; set; }
            public string PlanName { get; set; }
            public string TaskName { get; set; }
            public DateTime? ShouldStart { get; set; }
            public DateTime? ActualStart { get; set; }
            public DateTime? ShouldEnd { get; set; }
            public DateTime? ActualEnd { get; set; }
            public float PlannedBudget { get; set; }
            public float? ActualBudget { get; set; }
            public float? ActualWorked { get; set; }
            public float? TaskWeight { get; set; }
            public float? TaskGoal { get; set; }

            public List<ActivityViewModel> AssignedActivitty { get; set; }
            public List<ActivityViewModel> NotAssignedActivity { get; set; }

            public List<TaskMemberVm> TaskMember { get; set; }
            public List<TaskMemoVm> TaskMemo { get; set; }

        }

        public class ActivityViewModel
        {
            public string ProgramName { get; set; }
            public string PlanName { get; set; }
            public string TaskName { get; set; }
            public string ActivityName { get; set; }
            public string TaskId { get; set; }
            public Guid ActivityId { get; set; }
            public DateTime ShouldStart { get; set; }
            public DateTime? ActualStart { get; set; }
            public DateTime ShouldEnd { get; set; }
            public DateTime? ActualEnd { get; set; }
            public float PlannedBudget { get; set; }
            public float? ActualBudget { get; set; }
            public float? ActualWorked { get; set; }
            public string comitteeName { get; set; }
            public List<TaskMemberVm> commiteemember { get; set; }
            public Status status { get; set; }

            public float ActivityWeight { get; set; }
            public float Begining { get; set; }
            public float Goal { get; set; }
            public float Percentage { get; set; }

            public List<TargetDivisionVM> TargetDivisionVM { get; set; }
            public List<Actprogress> Progress { get; set; }
            public List<TaskMemberVm> AssignedEmployee { get; set; }



        }

        public class TaskMemberVm
        {
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string ImagePath { get; set; }
        }
        public class TaskMemoVm
        {
            public string TaskMemoId { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string ImagePath { get; set; }
            public string Message { get; set; }
            public DateTime SentTime { get; set; }
            public List<TaskMemoReplayVm> TaskMemoReplay { get; set; }

        }
        public class TaskMemoReplayVm
        {
            public string TaskMemoReplayId { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string ImagePath { get; set; }
            public string Message { get; set; }
            public DateTime SentTime { get; set; }
        }
        public class Actprogress
        {
            public Guid PorgressId { get; set; }
            public TaskMemberVm submittedBy { get; set; }
            public DateTime SentTime { get; set; }
            public float ActualBudget;
            public float ActualWorked { get; set; }
            public string DocumentPath { get; set; }
            public string FinanceDocumentPath { get; set; }
            public approvalStatus IsApprovedByCoordinator { get; set; }
            public approvalStatus IsApprovedByFinance { get; set; }
            public approvalStatus IsApprovedByDirector { get; set; }
            public string Remark { get; set; }

            public string FinanceId { get; set; }
            public string DirectorId { get; set; }
            public string ProjectCordinatorId { get; set; }


            public List<ActProgressAttachment> ProgressAttacment { get; set; }
        }
        public class ActProgressAttachment
        {
            public string FilePath { get; set; }


        }


        public class NotificationViewModel
        {
            public Guid? TaskId { get; set; }
            public Guid PorgressId { get; set; }
            public string ProgamName { get; set; }
            public string PlanName { get; set; }
            public string TaskName { get; set; }
            public string ActivityName { get; set; }
            public Guid? ActivityId { get; set; }
            public NotificationType NotificationType { get; set; }


        }
        public enum NotificationType
        {
            Approval,
            LateProgress
        }
        public class TargetDivisionVM
        {
            public Guid targetDivisionId { get; set; }
            public string target { get; set; }

        }
    }

}

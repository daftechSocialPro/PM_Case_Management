using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Helpers;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffairForMobileController : ControllerBase
    {

        private readonly DBContext _db;
        private readonly AuthenticationContext _onContext;
        private readonly ISMSHelper _smshelper;


        public AffairForMobileController(DBContext dbContext, AuthenticationContext onContext, ISMSHelper sMSHelper)
        {
            _db = dbContext;
            _onContext = onContext;
            _smshelper = sMSHelper;
        }


        [HttpPost]
        [Route("login")]

        public Tbluser login([FromBody] Tbluser user)
        {
            Employee employee = _db.Employees.Include(x=>x.OrganizationalStructure).FirstOrDefault(e => e.UserName == user.UserName && e.Password == user.Password);


            Tbluser userView = new Tbluser();
            if (employee != null)
            {
                userView.employeeID = employee.Id.ToString();
                userView.UserName = employee.UserName;
                userView.Password = employee.Password;
                userView.StructureName = employee.OrganizationalStructure.StructureName;
                userView.fullName = employee.Title + employee.FullName;
                userView.imagePath = employee.Photo;
                userView.userRole = employee.Position.ToString();

            }



            return userView;
        }

      


//        [HttpPost]
//        [Route("get-affairs")]
//        public List<ActiveAffairsViewModel> GetAffair([FromBody] Tbluser emp)
//        {

//            Guid empId = Guid.Parse(emp.employeeID);
//            //r currentUser = DbContext.ApplicationUsers.Find(User.Identity.GetUserId());  ActiveAffairsViewModel
//            var allAffairHistory = _db.CaseHistories
//                .Include(x => x.Case)
//                .Include(x => x.FromEmployee)
//                .Include(x => x.FromStructure)
//                .OrderByDescending(x => x.CreatedAt)
//                .Where(x => x.AffairHistoryStatus != AffairHistoryStatus.Completed
//                && x.AffairHistoryStatus != AffairHistoryStatus.Revert
//                && x.AffairHistoryStatus != AffairHistoryStatus.Transfered
//                            && x.ToEmployeeId == empId).ToList();
//            var result = new List<ActiveAffairsViewModel>();
//            if (allAffairHistory != null)
//            {



//                foreach (var aff in allAffairHistory)
//                {
//                    List<string> documents = new List<string>();
//                    var docs = Documents(aff);
//                    docs.ForEach(file =>
//                    {
//                        documents.Add(file);
//                    });

//                    var history = new ActiveAffairsViewModel
//                    {
//                        AffairId = aff.AffairId,
//                        AffairHistoryStatus = aff.AffairHistoryStatus.ToString(),
//                        ReciverType = aff.ReciverType.ToString(),
//                        AffairType = aff.Affair.AffairType.AffairTypeTitle,
//                        Remark = aff.Remark,
//                        FromStructure = aff.FromStructure.StructureName,
//                        AffairNumber = aff.Affair.AffairNumber,
//                        FromEmplyee = aff.FromEmployee.EmployeeFullName,
//                        Subject = aff.Affair.LetterSubject,
//                        Applicant = aff.Affair.Applicant != null ? aff.Affair.Applicant.ApplicantName : aff.Affair.Employee.EmployeeFullName,
//                        CreatedAt = aff.CreatedDateTime.ToShortDateString(),
//                        HistoryId = aff.Id,
//                        Document = documents,

//                    };

//                    if (!aff.SecreateryNeeded)
//                    {
//                        history.confirmedSecratary = aff.IsConfirmedBySeretery ? "Confirmed by " + aff.ToEmployee.EmployeeFullName : "Not Confirmed ";
//                    }
//                    else
//                    {
//                        history.confirmedSecratary = aff.IsConfirmedBySeretery ? "Confirmed by secretery" : "Not Confirmed ";
//                    }

//                    result.Add(history);
//                }
//            }

//            return result;
//        }


//        [HttpPost]
//        [Route("get-appointments")]
//        public List<appointment> getAppointmenr([FromBody] Tbluser emp)
//        {
//            Guid empId = Guid.Parse(emp.employeeID);
//            var appointements = _db.AppointementWithCalender.Where(x => x.EmployeeId == empId).Include(a => a.Affair).OrderByDescending(x => x.AppointementDate).ToList();

//            var Events = new List<appointment>();
//            appointements.ForEach(a =>
//            {
//                appointment ev = new appointment();


//                ev.description = "Appointment with " + a?.Affair?.Applicant?.ApplicantName + " at " + a.Time + " Affair Number " + a.Affair.AffairNumber;


//                ev.appointmentDate = a.AppointementDate.ToString("yyyy-MM-dd'T'HH:mm:ss");

//                ev.name = string.IsNullOrEmpty(a.Remark) ? "Appointment " : a.Remark;


//                Events.Add(ev);
//            });

//            return Events;

//        }


//        [HttpPost]
//        [Route("get-affairHis")]
//        public List<CaseHistories> getAffairHis([FromBody] CaseHistories affairHis)
//        {
//            Guid affairId = Guid.Parse(affairHis.affairId);

//            var af = _db.Cases.Find(affairId);


//            var affairHistory = _db.CaseHistories
//                .Include(a => a.Case)
//                .Include(a => a.FromStructure)
//                .Include(a => a.FromEmployee)
//                .Include(a => a.ToStructure)
//                .Include(a => a.ToEmployee)
//                .Include(a => a.CaseType)

//                .Where(x => x.AffairId == affairId)
//                .OrderByDescending(x => x.CreatedDateTime)
//                .ThenBy(x => x.ReciverType);


//            List<CaseHistories> Histories = new List<CaseHistories>();


//            foreach (var his in affairHistory)
//            {

//                CaseHistories history = new CaseHistories();
//                history.affairId = his.AffairId.ToString();


//                if (his.AffairHistoryStatus == AffairHistoryStatus.Completed)
//                {
//                    history.status = "Completed";
//                }
//                else if (his.AffairHistoryStatus == AffairHistoryStatus.Pend && his.ReciverType != ReciverType.Cc)
//                {
//                    history.status = "Not Seen";
//                }
//                else if (his.AffairHistoryStatus == AffairHistoryStatus.Seen && his.ReciverType != ReciverType.Cc)
//                {
//                    history.status = "Seen";
//                }
//                else if (his.AffairHistoryStatus == AffairHistoryStatus.Transfered)
//                {
//                    history.status = "Transfered";
//                }
//                else if (his.AffairHistoryStatus == AffairHistoryStatus.Revert)
//                {
//                    history.status = "Reverted";
//                }
//                history.affairHisId = his.Id.ToString();
//                history.affairType = his.ReciverType.ToString();
//                history.employeeId = affairHis.employeeId;
//                history.fromStructure = his.FromStructure.StructureName;
//                history.fromEmployee = his.FromEmployee.EmployeeFullName;
//                history.toStructure = his.ToStructure.StructureName;
//                history.toEmployeeId = his.ToEmployeeId.ToString();
//                history.toEmployee = his.ToEmployee.EmployeeFullName;
//                history.messageStatus = (his.IsSmsSent != true && his.ReciverType == ReciverType.Orginal) ? "Not Sent" : "Sent";

//                string givenDate = his.CreatedDateTime.ToString("dd/MM/yyyy");
//                string[] givenDateToArray = givenDate.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
//                var CreatedDateTime = XAPI.EthiopicDateTime.GetEthiopicDate(Int32.Parse(givenDateToArray[0]), Int32.Parse(givenDateToArray[1]), Int32.Parse(givenDateToArray[2]));

//                history.datetime = CreatedDateTime + " " + his.CreatedDateTime.ToString("h:mm");


//                var affairTypes = _db.CaseTypes.Where(x => x.ParentCaseTypeId == af.AffairTypeId).ToList();
//                foreach (var childaffair in affairTypes)
//                {
//                    int childcount = his.childOrder;

//                    if (childaffair.OrderNumber == childcount)
//                    {
//                        history.title = childaffair.AffairTypeTitle;
//                    }


//                }
//                history.title += " " + his.ReciverType;


//                Histories.Add(history);
//            }




//            return Histories;

//        }

//        [HttpPost]
//        [Route("get-affairWaiting")]
//        public List<ActiveAffairsViewModel> getAffairList([FromBody] Tbluser affairWait)
//        {
//            var currentUser = Guid.Parse(affairWait.employeeID);

//            var allAffairHistory = _db.CaseHistories
//            .Include(x => x.Affair)
//            .Include(x => x.FromEmployee)
//            .Include(x => x.FromStructure)
//            .OrderByDescending(x => x.CreatedDateTime)
//            .Where(x => x.AffairHistoryStatus == AffairHistoryStatus.Waiting
//                        && x.ToEmployeeId == currentUser).ToList();

//            var result = new List<ActiveAffairsViewModel>();
//            if (allAffairHistory != null)
//            {



//                foreach (var aff in allAffairHistory)
//                {
//                    List<string> documents = new List<string>();
//                    var docs = Documents(aff);
//                    docs.ForEach(file =>
//                    {
//                        documents.Add(file);
//                    });

//                    var history = new ActiveAffairsViewModel
//                    {
//                        AffairId = aff.AffairId,
//                        AffairHistoryStatus = aff.AffairHistoryStatus.ToString(),
//                        ReciverType = aff.ReciverType.ToString(),
//                        AffairType = aff.Affair.AffairType.AffairTypeTitle,
//                        Remark = aff.Remark,
//                        FromStructure = aff.FromStructure.StructureName,
//                        AffairNumber = aff.Affair.AffairNumber,
//                        FromEmplyee = aff.FromEmployee.EmployeeFullName,
//                        Subject = aff.Affair.LetterSubject,
//                        Applicant = aff.Affair.Applicant != null ? aff.Affair.Applicant.ApplicantName : aff.Affair.Employee.EmployeeFullName,
//                        CreatedAt = aff.CreatedDateTime.ToShortDateString(),
//                        HistoryId = aff.Id,
//                        Document = documents
//                    };
//                    if (!aff.SecreateryNeeded)
//                    {
//                        history.confirmedSecratary = aff.IsConfirmedBySeretery ? "Confirmed by " + aff.ToEmployee.EmployeeFullName : "Not Confirmed ";
//                    }
//                    else
//                    {
//                        history.confirmedSecratary = aff.IsConfirmedBySeretery ? "Confirmed by secretery" : "Not Confirmed ";
//                    }
//                    result.Add(history);
//                }
//            }

//            return result;
//        }


//        [HttpPost]
//        [Route("Add-to-WaitingList")]
//        public string addToWaitingList([FromBody] CaseHistories affirHistory)
//        {

//            try
//            {

//                var currentUser = Guid.Parse(affirHistory.employeeId);
//                var affairId = Guid.Parse(affirHistory.affairId);
//                var affairHisId = Guid.Parse(affirHistory.affairHisId);
//                var history = _db.CaseHistories.Find(affairHisId);
//                history.AffairHistoryStatus = AffairHistoryStatus.Waiting;
//                history.SeenDateTime = null;
//                _db.CaseHistories.Attach(history);
//                _db.Entry(history).Property(c => c.AffairHistoryStatus).IsModified = true;
//                _db.Entry(history).Property(c => c.SeenDateTime).IsModified = true;
//                _db.SaveChanges();

//                return "Successfully added to Waiting List";

//            }
//            catch (Exception e)
//            {


//                return "Something went Wrong";
//            }










//        }

//        [HttpPost]
//        [Route("make-appointment")]
//        public string makeappointments([FromBody] makeappointment appointment)
//        {

//            try
//            {
//                var currentUser = Guid.Parse(appointment.employeeId);
//                var empId = _onContext.ApplicationUsers.Where(x => x.EmployeesId == currentUser).FirstOrDefault().Id;
//                var affairId = Guid.Parse(appointment.affairId);
//                //if (!string.IsNullOrEmpty(appointment.executionDate))
//                //{
//                //    string[] startDate = appointment.executionDate.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
//                //    appointment.executionDate = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(startDate[0]), Int32.Parse(startDate[1]), Int32.Parse(startDate[2]))).ToString();
//                //}
//                var appointement = new AppointementWithCalender
//                {
//                    Id = Guid.NewGuid(),
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = empId,
//                    CaseId = affairId,
//                    EmployeeId = currentUser,
//                    RowStatus = RowStatus.Active,
//                    Remark = "",
//                    AppointementDate = DateTime.Parse(appointment.executionDate),
//                    Time = appointment.executionTime
//                };

//                var affair = _db.Cases.Find(affairId);
//                var employee = _db.Employees.Find(currentUser);
//                if (affair != null)
//                {
//                    var applicant = _db.Applicants.Find(affair.ApplicantId);

//                    #region send SMS
//                    var ApplicantName = affair.Applicant != null ? affair.Applicant.ApplicantName : affair.Employee.EmployeeFullName;
//                    var smsForclient = ApplicantName + " ለጉዳይ ቁጥር፡ " + affair.AffairNumber + "\n በ " + appointment.executionDate + " ቀን በ " + appointment.executionTime +
//                        " ሰዐት በቢሮ ቁጥር፡" + employee.Structure.OfficeNumber + " ይገኙ";

//                    var phoneNumber = affair.Applicant != null ? affair.Applicant.PhoneNumber.ToString() : affair.Employee.PhoneNumber;
//                    if (phoneNumber != null && phoneNumber.StartsWith("251"))
//                    {
//                        var phone = phoneNumber.Split('-');
//                        if (phone.Length > 2)
//                        {
//                            phoneNumber = "0" + phone[1] + phone[2];
//                        }
//                    }

//                    var result = MessageSender(phoneNumber, smsForclient, empId);

//                    if (affair.PhoneNumber2 != null)
//                        result = MessageSender(affair.PhoneNumber2, smsForclient, empId);


//                    if (result)
//                    {

//                        var affairmessages = new AffairMessages
//                        {
//                            Id = Guid.NewGuid(),
//                            CreatedDateTime = DateTime.Now,
//                            CreatedById = empId,

//                            AffairId = affairId,
//                            MessageFrom = MessageFrom.Appointment,
//                            MessageBody = smsForclient,
//                            Messagestatus = true
//                        };

//                        _db.AffairMessages.Add(affairmessages);
//                        _db.SaveChanges();
//                    }
//                    else
//                    {
//                        var affairmessages = new AffairMessages
//                        {
//                            Id = Guid.NewGuid(),
//                            CreatedDateTime = DateTime.Now,
//                            CreatedById = empId,

//                            AffairId = affairId,
//                            MessageFrom = MessageFrom.Appointment,
//                            MessageBody = smsForclient,
//                            Messagestatus = false
//                        };

//                        _db.AffairMessages.Add(affairmessages);
//                        _db.SaveChanges();
//                    }


//                    //while (!result)
//                    //{
//                    //    result = MessageSender(phoneNumber, smsForclient, User.Identity.GetUserId());
//                    //}
//                    #endregion
//                    _db.AppointemnetWithCalenders.Add(appointement);
//                    _db.SaveChanges();
//                }





//                return "Successfully appointed ";
//            }
//            catch (Exception e)
//            {


//                return "Something went wrong";

//            }


//        }


//        [HttpPost]
//        [Route("complete-affair")]
//        public string completeAffairs([FromBody] completeAffair completeAffair)
//        {

//            try

//            {

//                Guid affairHIsId = Guid.Parse(completeAffair.affairHisId);
//                var currentUser = Guid.Parse(completeAffair.employeeId);
//                var empId = _onContext.ApplicationUsers.Where(x => x.EmployeesId == currentUser).FirstOrDefault().Id;

//                var selectedHistory = _db.CaseHistories.Find(affairHIsId);
//                selectedHistory.AffairHistoryStatus = AffairHistoryStatus.Completed;
//                selectedHistory.CompletedDateTime = DateTime.Now;
//                selectedHistory.Remark = completeAffair.Remark;
//                var currentAffair = _db.Cases.Include(x => x.Applicant).Include(x => x.Employee).FirstOrDefault(x => x.Id == selectedHistory.AffairId);
//                var currentHist =
//                    _db.CaseHistories.Include(x => x.Affair).Include(x => x.ToStructure).FirstOrDefault(x => x.Id == selectedHistory.Id);
//                if (currentAffair != null)
//                {
//                    if (currentHist != null)
//                    {
//                        //var smsForclient = currentAffair.Applicant.ApplicantName + "\nበጉዳይ ቁጥር፡" + currentAffair.AffairNumber + "\nየተመዘገበ ጉዳዮ በ፡" + currentHist.ToStructure.StructureName + " ተጠናቋል\nየቢሮ ቁጥር:" + currentHist.ToStructure.OfficeNumber;
//                        //var currentAffair = affair;
//                        var name = currentAffair.Applicant != null ? currentAffair.Applicant.ApplicantName : currentAffair.Employee.EmployeeFullName;
//                        var smsForclient = name + "\nበጉዳይ ቁጥር፡" + currentAffair.AffairNumber + "\nየተመዘገበ ጉዳዮ በ፡" + currentHist.ToStructure.StructureName + " ተጠናቋል\nየቢሮ ቁጥር:" + currentHist.ToStructure.OfficeNumber;
//                        var phoneNumber = currentAffair.Applicant != null ? currentAffair.Applicant.PhoneNumber.ToString() : currentAffair.Employee.PhoneNumber;
//                        if (phoneNumber != null && phoneNumber.StartsWith("251"))
//                        {
//                            var phone = phoneNumber.Split('-');
//                            if (phone.Length > 2)
//                            {
//                                phoneNumber = "0" + phone[1] + phone[2];
//                            }
//                        }
//                        var result = MessageSender(phoneNumber, smsForclient, empId);
//                        selectedHistory.IsSmsSent = result;
//                        if (currentAffair.PhoneNumber2 != null)
//                            result = MessageSender(currentAffair.PhoneNumber2.ToString(), smsForclient, empId);




//                        if (result)
//                        {

//                            var affairmessages = new CaseMessages
//                            {
//                                Id = Guid.NewGuid(),
//                                CreatedAt = DateTime.Now,
//                                CreatedBy = empId,
//                                CaseId = selectedHistory.CaseId,
//                                MessageFrom = MessageFrom.Complete,
//                                MessageBody = smsForclient,
//                                Messagestatus = true
//                            };

//                            _db.CaseMessages.Add(affairmessages);
//                            _db.SaveChanges();
//                        }
//                        else
//                        {
//                            var affairmessages = new CaseMessages
//                            {
//                                Id = Guid.NewGuid(),
//                                CreatedAt = DateTime.Now,
//                                CreatedBy = empId,
//                                CaseId = selectedHistory.CaseId,
//                                MessageFrom = MessageFrom.Complete,
//                                MessageBody = smsForclient,
//                                Messagestatus = false
//                            };

//                            _db.AffairMessages.Add(affairmessages);
//                            _db.SaveChanges();
//                        }

//                        //while (!result)
//                        //{
//                        //    result = affairC.MessageSender(phoneNumber, smsForclient, User.Identity.GetUserId());
//                        //}
//                    }
//                }
//                _db.CaseHistories.Attach(selectedHistory);
//                _db.Entry(selectedHistory).Property(x => x.AffairHistoryStatus).IsModified = true;
//                _db.Entry(selectedHistory).Property(x => x.CompletedDateTime).IsModified = true;
//                _db.Entry(selectedHistory).Property(x => x.Remark).IsModified = true;
//                _db.Entry(selectedHistory).Property(x => x.IsSmsSent).IsModified = true;
//                _db.SaveChanges();
//                var selectedAffair = _db.Cases.Find(selectedHistory.CaseId);
//                selectedAffair.CompletedAt = DateTime.Now;
//                selectedAffair.AffairStatus = AffairStatus.Completed;
//                _db.Cases.Attach(selectedAffair);
//                _db.Entry(selectedAffair).Property(x => x.CompletedAt).IsModified = true;
//                _db.Entry(selectedAffair).Property(x => x.AffairStatus).IsModified = true;
//                _db.SaveChanges();

//                return "Successfully complete an affair";
//            }
//            catch (Exception e)
//            {
//                return "Something Went Wrong";
//            }
//        }

//        [HttpPost]
//        [Route("Revert-affair")]

//        public string RevertAffair([FromBody] completeAffair completeAffair)
//        {
//            try
//            {
//                var currentUser = Guid.Parse(completeAffair.employeeId);
//                var Employee = _db.Employees.Find(currentUser);
//                var affairHisId = Guid.Parse(completeAffair.affairHisId);
//                var empId = _onContext.ApplicationUsers.Where(x => x.EmployeesId == currentUser).FirstOrDefault().Id;
//                var selectedHistory = _db.CaseHistories.Find(affairHisId);
//                selectedHistory.AffairHistoryStatus = AffairHistoryStatus.Revert;
//                selectedHistory.RevertedAt = DateTime.Now;
//                selectedHistory.Remark = "";

//                _db.CaseHistories.Attach(selectedHistory);
//                _db.Entry(selectedHistory).Property(x => x.AffairHistoryStatus).IsModified = true;
//                _db.Entry(selectedHistory).Property(x => x.RevertedAt).IsModified = true;
//                _db.Entry(selectedHistory).Property(x => x.Remark).IsModified = true;
//                _db.SaveChanges();
//                var newHistory = new AffairHistory
//                {
//                    Id = Guid.NewGuid(),
//                    CreatedDateTime = DateTime.Now,
//                    CreatedById = empId,
//                    RowStatus = RowStatus.Active,
//                    FromEmployeeId = currentUser,
//                    FromStructureId = Employee.OrganizationalStructureId,
//                    ToEmployeeId = selectedHistory.FromEmployeeId,
//                    ToStructureId = selectedHistory.FromStructureId,
//                    Remark = "",
//                    AffairId = selectedHistory.CaseId,
//                    childOrder = selectedHistory.childOrder += 1,
//                    ReciverType = ReciverType.Orginal,
//                    Document = "No Document To Attach"
//                };
//                _db.CaseHistories.Add(newHistory);
//                _db.SaveChanges();
//                var currentAffair = _db.Cases.Include(x => x.Applicant).FirstOrDefault(x => x.Id == selectedHistory.CaseId);
//                var name = currentAffair.Applicant != null ? currentAffair.Applicant.ApplicantName : currentAffair.Employee.EmployeeFullName;
//                var smsForclient = name + "\nበጉዳይ ቁጥር፡" + currentAffair.AffairNumber + "\nየተመዘገበ ጉዳዮ በ፡" + selectedHistory.ToStructure.StructureName + " ወደኋላ ተመልሷል  \nየቢሮ ቁጥር:" + selectedHistory.ToStructure.OfficeNumber;
//                var phoneNumber = currentAffair.Applicant != null ? currentAffair.Applicant.PhoneNumber.ToString() : currentAffair.Employee.PhoneNumber;
//                if (phoneNumber != null && phoneNumber.StartsWith("251"))
//                {
//                    var phone = phoneNumber.Split('-');
//                    if (phone.Length > 2)
//                    {
//                        phoneNumber = "0" + phone[1] + phone[2];
//                    }
//                }
//                var result = MessageSender(phoneNumber, smsForclient, empId);
//                selectedHistory.IsSmsSent = result;
//                if (currentAffair.PhoneNumber2 != null)
//                    result = MessageSender(currentAffair.PhoneNumber2.ToString(), smsForclient, empId);


//                if (result)
//                {

//                    var affairmessages = new AffairMessages
//                    {
//                        Id = Guid.NewGuid(),
//                        CreatedDateTime = DateTime.Now,
//                        CreatedById = empId,

//                        AffairId = selectedHistory.AffairId,
//                        MessageFrom = MessageFrom.Revert,
//                        MessageBody = smsForclient,
//                        Messagestatus = true
//                    };

//                    _db.AffairMessages.Add(affairmessages);
//                    _db.SaveChanges();
//                }
//                else
//                {
//                    var affairmessages = new AffairMessages
//                    {
//                        Id = Guid.NewGuid(),
//                        CreatedDateTime = DateTime.Now,
//                        CreatedById = empId,

//                        AffairId = selectedHistory.AffairId,
//                        MessageFrom = MessageFrom.Revert,
//                        MessageBody = smsForclient,
//                        Messagestatus = false
//                    };

//                    _db.AffairMessages.Add(affairmessages);
//                    _db.SaveChanges();
//                }


//                return "Successfully reverted ";

//            }

//            catch
//            {

//                return "Something went wrong";
//            }
//        }

//        [HttpPost]
//        [Route("get-results")]
//        public results getResults([FromBody] results affairhis)
//        {


//            var historyDetailId = Guid.Parse(affairhis.historyDetailId);
//            var Results = new results();
//            var affairHistory = _db.CaseHistories.Find(historyDetailId);
//            var affairTypeId = affairHistory.Affair.AffairTypeId;

//            var affairtypes = _db.AffairTypes.Where(x => x.ParentAffairTypeId == affairTypeId);

//            var fileSetting = _db.FileSettings.Where(x => x.AffairTypeId == affairTypeId).ToList();

//            foreach (var affairtype in affairtypes)
//            {
//                fileSetting.AddRange(_db.FileSettings.Where(x => x.AffairTypeId == affairtype.Id).ToList());
//            }

//            if (fileSetting.FirstOrDefault().AffairType.ParentAffairTypeId == null)
//            {

//                Results.affairType = fileSetting.FirstOrDefault().AffairType.AffairTypeTitle;
//            }
//            else
//            {

//                Results.affairType = fileSetting.FirstOrDefault().AffairType.ParentAffairType.AffairTypeTitle;
//            }

//            foreach (var childaffair in affairtypes)
//            {
//                int childcount = affairHistory.childOrder + 1;

//                if (childaffair.OrderNumber == childcount)
//                {

//                    Results.currentState = childaffair.AffairTypeTitle;


//                    foreach (var file in fileSetting)
//                    {

//                        if (childaffair.Id == file.AffairType.Id)
//                        {
//                            Results.neededDocuments += file.FileName;
//                        }
//                    }
//                }
//                if (childaffair.OrderNumber == childcount + 1)
//                {

//                    Results.nextState = childaffair.AffairTypeTitle;



//                }

//            }

//            Results.employees = new List<EmployeeViewModel>();
//            Results.structures = new List<StructureViewModel>();


//            var employees = _db.Employees.ToList();
//            var structures = _db.OrganizationalStructures.ToList();



//            foreach (var emp in employees)
//            {
//                EmployeeViewModel emps = new EmployeeViewModel
//                {

//                    empId = emp.Id.ToString(),
//                    empName = emp.HREmployeeNameTitle.HREmployeeNameTitleName + " " + emp.EmployeeFullName + " (" + emp.Structure.StructureName + ")"

//                };
//                Results.employees.Add(emps);
//            }

//            foreach (var str in structures)
//            {

//                StructureViewModel strucs = new StructureViewModel
//                {

//                    strucutreId = str.Id.ToString(),
//                    structureName = str.StructureName
//                };

//                Results.structures.Add(strucs);


//            }

//            return Results;

//        }


//        [HttpPost]
//        [Route("Transfer-affair")]
//        public async Task<string> transferAffairs()
//        {

//            //Destination folder 
//            string uploadFolder = HttpContext.Current.Server.MapPath("~/Content/ActivityHistoryDocument/");
//            uploadFolder = uploadFolder.Replace('\\', '/');
//            //uploadFolder = uploadFolder.Replace("BSC/BSC", "BSC");

//            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(uploadFolder);
//            MultipartFileStreamProvider multipartFileStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);

//            string formdata = streamProvider.FormData.ToString();
//            string[] value = formdata.Split('&');
//            string replacable = "%22";
//            string empIdd = value[0].Split('=')[1].Replace(replacable, "");
//            string affairHisIdd = value[1].Split('=')[1].Replace(replacable, "");
//            string toEmployeeId = value[2].Split('=')[1].Replace(replacable, "");
//            string toStructureId = value[3].Split('=')[1].Replace(replacable, "");
//            string remark = value[4].Split('=')[1].Replace(replacable, "");


//            try
//            {



//                var currentUser = Guid.Parse(empIdd);
//                var Employee = _db.Employees.Find(currentUser);

//                var empId = _onContext.ApplicationUsers.Where(x => x.EmployeesId == currentUser).FirstOrDefault().Id;
//                var affairHisId = Guid.Parse(affairHisIdd);
//                var currentLastHistory = _db.CaseHistories.Find(affairHisId);

//                currentLastHistory.AffairHistoryStatus = AffairHistoryStatus.Transfered;
//                currentLastHistory.TransferedDateTime = DateTime.Now;
//                _db.CaseHistories.Attach(currentLastHistory);
//                _db.Entry(currentLastHistory).Property(c => c.AffairHistoryStatus).IsModified = true;
//                _db.Entry(currentLastHistory).Property(c => c.TransferedDateTime).IsModified = true;
//                _db.SaveChanges();



//                Guid? structureId = new Guid();
//                Guid employeeId = new Guid();

//                if (toEmployeeId != null || toEmployeeId != "")
//                {
//                    employeeId = Guid.Parse(toEmployeeId);
//                    structureId = _db.Employees.Find(employeeId).OrganizationalStructureId;
//                }
//                else
//                {

//                    structureId = Guid.Parse(toStructureId);



//                    employeeId = _db.Employees.Where(x => x.OrganizationalStructureId == structureId && x.EmployeeMemberShipLevel == EmployeeMemberShipLevel.Head).FirstOrDefault().Id;
//                }




//                var newHistory = new AffairHistory
//                {
//                    Id = Guid.NewGuid(),
//                    CreatedDateTime = DateTime.Now,
//                    CreatedById = empId,
//                    RowStatus = RowStatus.Active,
//                    FromEmployeeId = currentUser,
//                    FromStructureId = Employee.StructureId,
//                    ToEmployeeId = employeeId,
//                    ToStructureId = structureId,
//                    Remark = remark,
//                    AffairId = currentLastHistory.AffairId,
//                    ReciverType = ReciverType.Orginal,
//                    CaseTypeId = null,
//                    childOrder = currentLastHistory.childOrder + 1
//                };


//                newHistory.SecreateryNeeded = (toEmployeeId != null || toEmployeeId != "") ? true : false;
//                //if (letterFile == null)
//                //{
//                //    newHistory.Document = "No Document Attached";
//                //}
//                //else
//                //{
//                //    var photoinfo = new FileInfo(Path.GetFileName(letterFile.FileName));
//                //    var fileExtension = photoinfo.Extension;
//                //    var path = Path.Combine(Server.MapPath("~/Content/ActivityHistoryDocument/"),
//                //        newHistory.Id + fileExtension);
//                //    letterFile.SaveAs(path);
//                //    newHistory.Document = newHistory.Id + fileExtension;
//                //}

//                //must be attached
//                _db.CaseHistories.Add(newHistory);
//                _db.SaveChanges();






//                if (streamProvider.FileData.Any())
//                {
//                    int i = 0;
//                    // Get the file names.
//                    foreach (MultipartFileData file in streamProvider.FileData.Where(x => x.Headers.ContentDisposition.Name == "\"File\""))
//                    {
//                        var attachment = new AffairHistoryAttachment()
//                        {
//                            Id = Guid.NewGuid(),
//                            CreatedById = empId,
//                            CreatedDateTime = DateTime.Now,
//                            RowStatus = RowStatus.Active,

//                            AffairHistoryId = newHistory.Id
//                        };



//                        if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
//                        {
//                            return "This request is not properly formatted";
//                        }
//                        string fileName = file.Headers.ContentDisposition.FileName;
//                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
//                        {
//                            fileName = fileName.Trim('"');
//                        }
//                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
//                        {
//                            fileName = Path.GetFileName(fileName);
//                        }
//                        string[] fn = fileName.Split('.');
//                        fileName = attachment.Id + "FromMobile" + "." + fn[1];
//                        File.Move(file.LocalFileName, Path.Combine(uploadFolder, fileName));



//                        attachment.FilePath = fileName;


//                        //
//                        _db.AffairHistoryAttachment.Add(attachment);
//                        _db.SaveChanges();
//                        i++;

//                    }



//                }

//                //int i = 1;
//                //foreach (var file in letterFile)
//                //{
//                //    var photoinfo = new FileInfo(fileName: Path.GetFileName(file.FileName));
//                //    var fileExtension = photoinfo.Extension;
//                //    var path = Path.Combine(Server.MapPath("~/Content/ActivityHistoryDocument/"), newHistory.Id + "-" + i + fileExtension);
//                //    file.SaveAs(path);
//                //    var attachment = new AffairHistoryAttachment()
//                //    {
//                //        Id = Guid.NewGuid(),
//                //        CreatedById = User.Identity.GetUserId(),
//                //        CreatedDateTime = DateTime.Now,
//                //        RowStatus = RowStatus.Active,
//                //        FilePath = newHistory.Id + "-" + i + fileExtension,
//                //        AffairHistoryId = newHistory.Id
//                //    };
//                //    Db.AffairHistoryAttachment.Add(attachment);
//                //    Db.SaveChanges();
//                //    i++;
//                //}

//                var currentAffair = _db.Affairs.Include(x => x.Applicant).Include(x => x.Employee).FirstOrDefault(x => x.Id == newHistory.AffairId);
//                var currentHist =
//                    _db.CaseHistories.Include(x => x.ToStructure).Include(x => x.Affair).FirstOrDefault(x => x.Id == newHistory.Id);
//                if (currentAffair != null)
//                {
//                    if (currentHist != null)
//                    {
//                        //var smsForclient = currentAffair.Applicant.ApplicantName + "\nበጉዳይ ቁጥር፡" + currentAffair.AffairNumber + "\nየተመዘገበ ጉዳዮ ለ " + currentHist.ToStructure.StructureName + "ተላልፏል\nየቢሮ ቁጥር:" + currentHist.ToStructure.OfficeNumber;

//                        //var currentAffair = affair;
//                        var name = currentAffair.Applicant != null ? currentAffair.Applicant.ApplicantName : currentAffair.Employee.EmployeeFullName;
//                        var smsForclient = name + "\nበጉዳይ ቁጥር፡" + currentAffair.AffairNumber + "\nየተመዘገበ ጉዳዮ ለ " + currentHist.ToStructure.StructureName + " ተላልፏል\nየቢሮ ቁጥር:" + currentHist.ToStructure.OfficeNumber;
//                        var phoneNumber = currentAffair.Applicant != null ? currentAffair.Applicant.PhoneNumber.ToString() : currentAffair.Employee.PhoneNumber;
//                        if (phoneNumber != null && phoneNumber.StartsWith("251"))
//                        {
//                            var phone = phoneNumber.Split('-');
//                            if (phone.Length > 2)
//                            {
//                                phoneNumber = "0" + phone[1] + phone[2];
//                            }
//                        }

//                        var result = MessageSender(phoneNumber, smsForclient, empId);
//                        newHistory.IsSmsSent = result;
//                        if (currentAffair.PhoneNumber2 != null)
//                            result = MessageSender(currentAffair.PhoneNumber2.ToString(), smsForclient, empId);


//                        if (result)
//                        {

//                            var affairmessages = new AffairMessages
//                            {
//                                Id = Guid.NewGuid(),
//                                CreatedDateTime = DateTime.Now,
//                                CreatedById = empId,

//                                AffairId = currentLastHistory.AffairId,
//                                MessageFrom = MessageFrom.Transfer,
//                                MessageBody = smsForclient,
//                                Messagestatus = true
//                            };

//                            _db.AffairMessages.Add(affairmessages);
//                            _db.SaveChanges();
//                        }
//                        else
//                        {
//                            var affairmessages = new AffairMessages
//                            {
//                                Id = Guid.NewGuid(),
//                                CreatedDateTime = DateTime.Now,
//                                CreatedById = empId,

//                                AffairId = currentLastHistory.AffairId,
//                                MessageFrom = MessageFrom.Transfer,
//                                MessageBody = smsForclient,
//                                Messagestatus = false
//                            };

//                            _db.AffairMessages.Add(affairmessages);
//                            _db.SaveChanges();
//                        }

//                        //while (!result)
//                        //{
//                        //    result = affairC.MessageSender(phoneNumber, smsForclient, User.Identity.GetUserId());
//                        //}
//                    }
//                }


//                return "Successfully Transfred";

//            }
//            catch (Exception e)
//            {



//                return e.Message;


//            }



//        }

//        [HttpPost]
//        [Route("send-message")]
//        public string SendSMS([FromBody] messageViewModel message)
//        {


//            try
//            {

//                var currentUser = Guid.Parse(message.employeeId);
//                var empId = _onContext.ApplicationUsers.Where(x => x.EmployeesId == currentUser).FirstOrDefault().Id;
//                Guid affairId = Guid.Parse(message.affairId);
//                var appointement = new Appointement
//                {
//                    Id = Guid.NewGuid(),
//                    CreatedAt = DateTime.Now,
//                    CreatedBy = empId.ToString(),
//                    CaseId = affairId,
//                    EmployeeId = currentUser,
//                    RowStatus = RowStatus.Active,
//                    Remark = message.message
//                };
//                var affair = _db.Affairs.Find(affairId);

//                if (affair != null)
//                {

//                    var applicant = _db.Applicants.Find(affair.ApplicantId);
//                    if (applicant != null)
//                    {
//                        #region send SMS
//                        var smsForclient = "Reg No " + affair.AffairNumber + " " + message.message;
//                        var result = MessageSender(applicant.PhoneNumber, smsForclient, empId.ToString());
//                        //while (!result)
//                        //{
//                        //    result = MessageSender(applicant.PhoneNumber, smsForclient, User.Identity.GetUserId());
//                        //}

//                        if (result)
//                        {
//ca
//                            var affairmessages = new CaseMessages
//                            {
//                                Id = Guid.NewGuid(),
//                                CreatedDateTime = DateTime.Now,
//                                CreatedById = empId,

//                                AffairId = affairId,
//                                MessageFrom = MessageFrom.Custom_text,
//                                MessageBody = smsForclient,
//                                Messagestatus = true
//                            };

//                            _db.AffairMessages.Add(affairmessages);
//                            _db.SaveChanges();
//                        }
//                        else
//                        {
//                            var affairmessages = new AffairMessages
//                            {
//                                Id = Guid.NewGuid(),
//                                CreatedDateTime = DateTime.Now,
//                                CreatedById = empId,

//                                AffairId = affairId,
//                                MessageFrom = MessageFrom.Custom_text,
//                                MessageBody = smsForclient,
//                                Messagestatus = false
//                            };

//                            _db.AffairMessages.Add(affairmessages);
//                            _db.SaveChanges();
//                        }
//                        appointement.IsSmsSent = result;
//                        if (affair.PhoneNumber2 != null)
//                            result = MessageSender(affair.PhoneNumber2, smsForclient, User.Identity.GetUserId());
//                        #endregion
//                        _db.Appointements.Add(appointement);
//                        _db.SaveChanges();
//                    }
//                }

//                return "successfully Sent ";
//            }
//            catch (Exception e)
//            {
//                string sdf = e.Message;
//                return "Sending message was not successfull ";

//            }







//        }


//        [HttpPost]
//        [Route("get-notification")]
//        public NotificationCount getNotfi([FromBody] Tbluser NotCount)
//        {


//            var notficationCout = new NotificationCount();

//            notficationCout.affairCount = GetAffair(NotCount).Count(x => x.AffairHistoryStatus == "Seen");
//            notficationCout.waitingListCount = getAffairList(NotCount).Count();
//            notficationCout.appointmentCount = getAppointmenr(NotCount).Count(x => x.appointmentDate.Split('T')[0] == DateTime.Now.ToString("yyyy-MM-dd"));




//            return notficationCout;


//        }
//        public bool MessageSender(string reciver, string message, string UserId, Guid? orgId = null)
//        {


//            // reciver = "0937637310";
//            var ApplicationUsersList = _onContext.ApplicationUsers.ToList();
//            var employee = ApplicationUsersList.FirstOrDefault(x => x.Id == UserId);
//            var department = _db.Employees.Include(x => x.Structure).FirstOrDefault(x => x.Id == employee.EmployeeId);
//            if (orgId != null)
//            {
//                department = _db.Employees.Include(x => x.Structure).FirstOrDefault(x => x.StructureId == orgId);
//            }
//            var messeageSetting = _db.MessageSettings.FirstOrDefault();
//            //            if (messeageSetting != null && messeageSetting.SettingType == SettingType.DONGLE)
//            //            {
//            //                SMS sms = new SMS(messeageSetting.ComPort, messeageSetting.BaudRate);
//            //                var test = sms.SendSMSByDongle(reciver, message);
//            //                //if (test == "Ok")
//            //                //{
//            //                //    return true;
//            //                //}
//            //                //else
//            //                //{
//            //                //    return false;
//            //                //}
//            //                   
//            //            }
//            //            else
//            {
//                if (messeageSetting != null)
//                {

//                    try
//                    {

//                        // Create a request using a URL that can receive a post. 
//                        var oganizationId = _db.OrganizationProfiles.FirstOrDefault();
//                        if (oganizationId != null)
//                        {
//                            var coder = department.Structure.SMSCode;
//                            if (coder == null) coder = "DAFT";
//                            // messeageSetting.IPAddress = "192.168.1.10:8313";
//                            //messeageSetting.IPAddress = messeageSetting.IPAddress+":8313";
//                            WebRequest request =
//                                WebRequest.Create(
//                                    "http://" + messeageSetting.IPAddress + "/api/SmsSender?orgId=" + coder +
//                                    "&message=" +
//                                    message + "&recipantNumber=" + reciver);
//                            // Set the Method property of the request to POST.
//                            //  request.Timeout = 100000;
//                            request.Method = "POST";
//                            //   request.Timeout = Timeout.Infinite;
//                            // Create POST data and convert it to a byte array.
//                            string postData = "This is a test that posts this string to a Web server.";
//                            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
//                            // Set the ContentType property of the WebRequest.
//                            request.ContentType = "application/x-www-form-urlencoded";
//                            // Set the ContentLength property of the WebRequest.
//                            request.ContentLength = byteArray.Length;
//                            // Get the request stream.
//                            Stream dataStream = request.GetRequestStream();
//                            // Write the data to the request stream.
//                            dataStream.Write(byteArray, 0, byteArray.Length);
//                            // Close the Stream object.
//                            dataStream.Close();
//                            // Get the response.
//                            WebResponse response = request.GetResponse();
//                            // Display the status.
//                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
//                            // Get the stream containing content returned by the server.
//                            dataStream = response.GetResponseStream();
//                            // Open the stream using a StreamReader for easy access.
//                            StreamReader reader = new StreamReader(dataStream);
//                            // Read the content.
//                            string responseFromServer = reader.ReadToEnd();
//                            // Display the content.
//                            Console.WriteLine(responseFromServer);
//                            if (responseFromServer == "\"False\"")
//                            {
//                                reader.Close();
//                                dataStream.Close();
//                                response.Close();
//                                //var newMessage = MessageSender(reciver, message, UserId, orgId);
//                                //if (newMessage == true)
//                                //{
//                                //    return newMessage;
//                                //}
//                                return false;
//                            }
//                            // Clean up the streams.
//                            reader.Close();
//                            dataStream.Close();
//                            response.Close();
//                            return true;
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        return false;
//                    }
//                }
//            }
//            return false;

//        }


        public class NotificationCount
        {
            public int affairCount { get; set; }
            public int appointmentCount { get; set; }
            public int waitingListCount { get; set; }
            public string employeeId { get; set; }

        }
        public class Tbluser
        {
            public string employeeID { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string StructureName { get; set; }
            public string fullName { get; set; }
            public string imagePath { get; set; }
            public string userRole { get; set; }


        }
        public class results
        {
            public string historyDetailId { get; set; }
            public string affairType { get; set; }
            public string nextState { get; set; }
            public string currentState { get; set; }
            public string neededDocuments { get; set; }
            public List<EmployeeViewModel> employees { get; set; }
            public List<StructureViewModel> structures { get; set; }
        }
        public class transferAffair
        {

            public string empIdd { get; set; }
            public string affairHisIdd { get; set; }
            public string toEmployeeId { get; set; }
            public string toStructureId { get; set; }
            public string remark { get; set; }
            public string caseTypeId { get; set; }


        }
        public class StructureViewModel
        {
            public string structureName { get; set; }
            public string strucutreId { get; set; }
        }
        public class EmployeeViewModel
        {
            public string empName { get; set; }
            public string empId { get; set; }
        }

        public class completeAffair
        {
            public string employeeId { get; set; }
            public string affairHisId { get; set; }
            public string Remark { get; set; }
        }
        public class makeappointment
        {
            public string employeeId { get; set; }
            public string executionDate { get; set; }
            public string executionTime { get; set; }
            public string affairId { get; set; }


        }

        public class CaseHistories
        {
            public string affairHisId { get; set; }
            public string employeeId { get; set; }
            public string toEmployeeId { get; set; }
            public string affairType { get; set; }
            public string affairId { get; set; }
            public string datetime { get; set; }
            public string title { get; set; }
            public string fromStructure { get; set; }
            public string toStructure { get; set; }
            public string fromEmployee { get; set; }
            public string toEmployee { get; set; }
            public string status { get; set; }
            public string messageStatus { get; set; }
        }

        public class appointment
        {

            public string description { get; set; }
            public string appointmentDate { get; set; }
            public string name { get; set; }
        }


        public class messageViewModel
        {

            public string message { get; set; }
            public string affairId { get; set; }
            public string employeeId { get; set; }

        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.Models.Common;
using System.Net;
using System.Text;
using System.IO.Ports;
using PM_Case_Managemnt_API.Models.Auth;
using PM_Case_Managemnt_API.Models.CaseModel;
using Microsoft.Net.Http.Headers;

namespace PM_Case_Managemnt_API.Helpers
{
    public class SMSHelper : ISMSHelper
    {
        private readonly AuthenticationContext _authenticationContext;
        private readonly DBContext _dbContext;

        public SMSHelper(AuthenticationContext authenticationContext, DBContext dBContext)
        {
            _authenticationContext = authenticationContext;
            _dbContext = dBContext;
        }
        public async Task<bool> MessageSender(string reciver, string message, string UserId, Guid? orgId = null)
        {
            // reciver = "0937637310";
            ApplicationUser user = await _authenticationContext.ApplicationUsers.Where(x => x.Id.Equals(UserId)).FirstAsync();
            Employee employee = _dbContext.Employees.Include(x => x.OrganizationalStructure.OrganizationBranch.OrganizationProfile).FirstOrDefault(x => x.Id == user.EmployeesId);
            if (orgId != null)
                employee = _dbContext.Employees.Include(x => x.OrganizationalStructure).FirstOrDefault(x => x.OrganizationalStructureId == orgId);

            try
            {
                // Create a request using a URL that can receive a post. 
                OrganizationProfile oganizationProfile = _dbContext.OrganizationProfile.FirstOrDefault();
                string ipAddress = "192.168.1.10:8313";
                if (oganizationProfile != null)
                {
                    string coder = employee.OrganizationalStructure.OrganizationBranch.OrganizationProfile.SmsCode.ToString();
                    coder = "DAFT";
                    string uri = $"http://{ipAddress}/api/SmsSender?orgId={coder}&message={message}&recipantNumber={reciver}";

                    byte[] byteArray = Encoding.UTF8.GetBytes(message);
                    using (HttpClient c = new HttpClient())
                    {
                        Uri apiUri = new Uri(uri);
                        ByteArrayContent body = new ByteArrayContent(byteArray, 0, byteArray.Length);
                        MultipartFormDataContent multiPartFormData = new MultipartFormDataContent
                                {
                                    body
                                };
                        HttpResponseMessage result = await c.PostAsync(apiUri, multiPartFormData);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public async Task<bool> UnlimittedMessageSender(string reciver, string message, string UserId, Guid? orgId = null)
        {
            try
            {
                //reciver = "0937637310";
                ApplicationUser user = await _authenticationContext.ApplicationUsers.Where(x => x.Id.Equals(UserId)).FirstAsync();
                Employee employee = _dbContext.Employees.Include(x => x.OrganizationalStructure.OrganizationBranch.OrganizationProfile).FirstOrDefault(x => x.Id == user.EmployeesId);
                if (orgId != null)
                    employee = _dbContext.Employees.Include(x => x.OrganizationalStructure).FirstOrDefault(x => x.OrganizationalStructureId == orgId);


                OrganizationProfile oganizationProfile = _dbContext.OrganizationProfile.FirstOrDefault();
                string ipAddress = "192.168.1.10:8313";
                if (oganizationProfile != null)
                {
                    string coder = employee.OrganizationalStructure.OrganizationBranch.OrganizationProfile.SmsCode.ToString();
                    coder = "DAFT";
                    string uri = $"http://{ipAddress}/api/SmsSender?orgId={coder}&message={message}&recipantNumber={reciver}";

                    byte[] byteArray = Encoding.UTF8.GetBytes(message);
                    using (HttpClient c = new HttpClient())
                    {
                        Uri apiUri = new Uri(uri);
                        ByteArrayContent body = new ByteArrayContent(byteArray, 0, byteArray.Length);
                        MultipartFormDataContent multiPartFormData = new MultipartFormDataContent
                                {
                                    body
                                };
                        try
                        {
                            HttpResponseMessage result = await c.PostAsync(apiUri, multiPartFormData);
                            if (!result.IsSuccessStatusCode)
                            {
                                bool newMessage = await MessageSender(reciver, message, UserId, orgId);
                                if (!newMessage)
                                    return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            bool newMessage = await MessageSender(reciver, message, UserId, orgId);
                            if (!newMessage)
                                return false;
                            return true;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> SendSmsForCase(string message, Guid caseId, Guid caseHistoryId, string userId, MessageFrom messageFrom)
        {
            try
            {
                Case currentCase = await _dbContext.Cases.Include(x => x.Applicant).Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == caseId);
                CaseHistory currentHistory = await _dbContext.CaseHistories.Include(x => x.ToStructure).Include(x => x.Case).FirstOrDefaultAsync(x => x.Id == caseHistoryId);
                bool result = false;
                if (currentCase != null)
                {
                    if (currentHistory != null)
                    {
                        string name = currentCase.Applicant != null ? currentCase.Applicant.ApplicantName : currentCase.Employee.FullName;
                        string phoneNumber = currentCase.Applicant != null ? currentCase.Applicant.PhoneNumber.ToString() : currentCase.Employee.PhoneNumber;
                        if (phoneNumber != null && phoneNumber.StartsWith("251"))
                        {
                            var phone = phoneNumber.Split('-');
                            if (phone.Length > 2)
                            {
                                phoneNumber = "0" + phone[1] + phone[2];
                            }
                        }
                        result = await UnlimittedMessageSender(phoneNumber, message, userId);
                        currentHistory.IsSmsSent = result;
                        if (currentCase.PhoneNumber2 != null && !result)
                            result = await MessageSender(currentCase.PhoneNumber2.ToString(), message, userId);
                    }
                }


                CaseMessages caseMessages = new CaseMessages()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    CaseId = caseId,
                    CreatedBy = Guid.Parse(userId),
                    MessageBody = message,
                    MessageFrom = messageFrom,
                    Messagestatus = result,
                    RowStatus = RowStatus.Active
                };

                await _dbContext.CaseMessages.AddAsync(caseMessages);
                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

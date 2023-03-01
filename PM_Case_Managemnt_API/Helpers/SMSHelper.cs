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
    public class SMSHelper: ISMSHelper
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
            //var employee = usersList.FirstOrDefault(x => x.Id == UserId);
            Employee employee = _dbContext.Employees.Include(x => x.OrganizationalStructure.OrganizationBranch.OrganizationProfile).FirstOrDefault(x => x.Id == user.EmployeesId);
            if (orgId != null)
                employee = _dbContext.Employees.Include(x => x.OrganizationalStructure).FirstOrDefault(x => x.OrganizationalStructureId == orgId);
            //var messeageSetting = Db.MessageSettings.FirstOrDefault();
            //            if (messeageSetting != null && messeageSetting.SettingType == SettingType.DONGLE)
            //            {
            //                SMS sms = new SMS(messeageSetting.ComPort, messeageSetting.BaudRate);
            //                var test = sms.SendSMSByDongle(reciver, message);
            //                //if (test == "Ok")
            //                //{
            //                //    return true;
            //                //}
            //                //else
            //                //{
            //                //    return false;
            //                //}
            //                   
            //            }
            //            else
            {
                //if (messeageSetting != null)
                {

                    try
                    {

                        // Create a request using a URL that can receive a post. 
                        OrganizationProfile oganizationProfile = _dbContext.OrganizationProfile.FirstOrDefault();
                        string ipAddress = "192.168.1.10:8313";
                        if (oganizationProfile != null)
                        {
                            string coder = employee.OrganizationalStructure.OrganizationBranch.OrganizationProfile.SmsCode.ToString();
                            coder = "DAFT";
                            //if (coder == null) coder = "DAFT";
                            // messeageSetting.IPAddress = "192.168.1.10:8313";
                            //messeageSetting.IPAddress = messeageSetting.IPAddress+":8313";
                            // Change static IP with ip from messageSettings Table.
                            string baseAddress = "http://" + ipAddress;
                            string relAddress =  "/api/SmsSender?orgId=" + coder + "&message=" + message + "&recipantNumber=" + reciver;
                            WebRequest request = WebRequest.Create(baseAddress);
                            //WebRequest.Create(
                            //        "http://" + messeageSetting.IPAddress + "/api/SmsSender?orgId=" + coder +
                            //        "&message=" +
                            //        message + "&recipantNumber=" + reciver);


                            // Set the Method property of the request to POST.
                            //  request.Timeout = 100000;
                            request.Method = "POST";
                            //   request.Timeout = Timeout.Infinite;
                            // Create POST data and convert it to a byte array.


                            byte[] byteArray = Encoding.UTF8.GetBytes(message);
                            //using(HttpClient c = new HttpClient())
                            //{
                            //    var apiUri  = new Uri(baseAddress + relAddress);
                            //    var body = new ByteArrayContent(byteArray, 0, byteArray.Length);
                            //    var multiPartFormData = new MultipartFormDataContent
                            //    {
                            //        body
                            //    };
                            //    var result = await c.PostAsync(apiUri, multiPartFormData); 
                            //}
                            //HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, relAddress);
                            //byte[] byteArray = Encoding.UTF8.GetBytes(message);
                            //var body= new ByteArrayContent(byteArray, 0, byteArray.Length);
                    
                            
                            
                            string postData = "This is a test that posts this string to a Web server.";
                            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                            // Set the ContentType property of the WebRequest.
                            request.ContentType = "application/x-www-form-urlencoded";
                            // Set the ContentLength property of the WebRequest.
                            request.ContentLength = byteArray.Length;
                            // Get the request stream.
                            Stream dataStream = request.GetRequestStream();
                            // Write the data to the request stream.
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            // Close the Stream object.
                            dataStream.Close();
                            // Get the response.
                            WebResponse response = request.GetResponse();
                            // Display the status.
                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                            // Get the stream containing content returned by the server.
                            dataStream = response.GetResponseStream();
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream);
                            // Read the content.
                            string responseFromServer = reader.ReadToEnd();
                            // Display the content.
                            Console.WriteLine(responseFromServer);

                            reader.Close();
                            dataStream.Close();
                            response.Close();

                            if (responseFromServer == "\"False\"".ToLower())
                            {
                                //var newMessage = MessageSender(reciver, message, UserId, orgId);
                                //if (newMessage == true)
                                //{
                                //    return newMessage;
                                //}
                                return false;
                            }
                            // Clean up the streams.
                            //reader.Close();
                            //dataStream.Close();
                            //response.Close();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;

        }


        public async Task<bool> UnlimettedMessageSender(string reciver, string message, string UserId, Guid? orgId = null)
        {
            //reciver = "0937637310";
            ApplicationUser user = await _authenticationContext.ApplicationUsers.Where(x => x.Id.Equals(UserId)).FirstAsync();
            //var employee = usersList.FirstOrDefault(x => x.Id == UserId);
            Employee employee = _dbContext.Employees.Include(x => x.OrganizationalStructure.OrganizationBranch.OrganizationProfile).FirstOrDefault(x => x.Id == user.EmployeesId);
            if (orgId != null)
            {
                employee = _dbContext.Employees.Include(x => x.OrganizationalStructure).FirstOrDefault(x => x.OrganizationalStructureId == orgId);
            }
            //var messeageSetting = _dbContext.MessageSettings.FirstOrDefault();
            {
                //if (messeageSetting != null)
                {

                    try
                    {

                        // Create a request using a URL that can receive a post. 
                        var oganizationId = _dbContext.OrganizationProfile.FirstOrDefault();
                        if (oganizationId != null)
                        {
                            string coder = employee.OrganizationalStructure.OrganizationBranch.OrganizationProfile.SmsCode.ToString();
                            string ipAddress = "192.168.1.10:8313";
                            coder = "DAFT";
                            string url = "http://" + ipAddress + "/SmsCenter/api/SmsSender?orgId=" + coder +
                                    "&message=" +
                                    message + "&recipantNumber=" + reciver;

                            //var str = "http://" + messeageSetting.IPAddress + "/SmsCenter/api/SmsSender?orgId=" + coder +
                            //        "&message=" +
                            //        message + "&recipantNumber=" + reciver;

                            //if (coder == null) coder = "DAFT";
                            WebRequest request = WebRequest.Create(url);
                            // Set the Method property of the request to POST.
                            //  request.Timeout = 100000;
                            request.Method = "POST";
                            //   request.Timeout = Timeout.Infinite;
                            // Create POST data and convert it to a byte array.
                            string postData = "This is a test that posts this string to a Web server.";
                            byte[] byteArray = Encoding.UTF8.GetBytes(message);
                            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                            // Set the ContentType property of the WebRequest.
                            request.ContentType = "application/x-www-form-urlencoded";
                            // Set the ContentLength property of the WebRequest.
                            request.ContentLength = byteArray.Length;
                            // Get the request stream.
                            Stream dataStream = request.GetRequestStream();
                            // Write the data to the request stream.
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            // Close the Stream object.
                            dataStream.Close();
                            // Get the response.
                            WebResponse response = request.GetResponse();
                            // Display the status.
                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                            // Get the stream containing content returned by the server.
                            dataStream = response.GetResponseStream();
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream);
                            // Read the content.
                            string responseFromServer = reader.ReadToEnd();
                            // Display the content.
                            Console.WriteLine(responseFromServer);
                            reader.Close();
                            dataStream.Close();
                            response.Close();
                            if (responseFromServer == "\"False\"".ToLower())
                            {
                                //reader.Close();
                                //dataStream.Close();
                                //response.Close();
                                bool newMessage = await MessageSender(reciver, message, UserId, orgId);
                                if (newMessage == true)
                                {
                                    return newMessage;
                                }
                                return false;
                            }
                            // Clean up the streams.
                            //reader.Close();
                            //dataStream.Close();
                            //response.Close();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public async Task<bool> SendSmsForCase(Guid caseId, Guid caseHistoryId, string userId)
        {
            try
            {
                Case currentCase = await _dbContext.Cases.Include(x => x.Applicant).Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == caseId);
                CaseHistory currentHistory = await _dbContext.CaseHistories.Include(x => x.ToStructure).Include(x => x.Case).FirstOrDefaultAsync(x => x.Id == caseHistoryId);
                if (currentCase != null)
                {
                    if (currentHistory != null)
                    {
                        string name = currentCase.Applicant != null ? currentCase.Applicant.ApplicantName : currentCase.Employee.FullName;
                        string smsForclient = name + "\nበጉዳይ ቁጥር፡" + currentCase.CaseNumber + "\nየተመዘገበ ጉዳዮ ለ " + currentHistory.ToStructure.StructureName
                            + " ተላልፏል\nየቢሮ ቁጥር:" + currentHistory.ToStructure.StructureName;
                        string phoneNumber = currentCase.Applicant != null ? currentCase.Applicant.PhoneNumber.ToString() : currentCase.Employee.PhoneNumber;
                        if (phoneNumber != null && phoneNumber.StartsWith("251"))
                        {
                            var phone = phoneNumber.Split('-');
                            if (phone.Length > 2)
                            {
                                phoneNumber = "0" + phone[1] + phone[2];
                            }
                        }
                        bool result = await MessageSender(phoneNumber, smsForclient, userId);
                        currentHistory.IsSmsSent = result;
                        if (currentCase.PhoneNumber2 != null)
                            result = await MessageSender(currentCase.PhoneNumber2.ToString(), smsForclient, userId);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

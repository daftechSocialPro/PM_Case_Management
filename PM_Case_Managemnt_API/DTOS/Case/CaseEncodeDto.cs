using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Models.CaseModel;

namespace PM_Case_Managemnt_API.DTOS.CaseDto
{
    public class CaseEncodePostDto
    {
        public string CaseNumber { get; set; }
        public Guid? ApplicantId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string LetterNumber { get; set; }
        public string LetterSubject { get; set; }
        public Guid CaseTypeId { get; set; }
        public string DocumentPath { get; set; }
        //public DateTime? CompletedAt { get; set; }
        public AffairStatus AffairStatus { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Representative { get; set; }
        public bool IsArchived { get; set; }
        public bool SMSStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public string Remark { get; set; }
    }
}

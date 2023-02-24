using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;

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
        //public string DocumentPath { get; set; }
        //public DateTime? CompletedAt { get; set; }
        public AffairStatus AffairStatus { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Representative { get; set; }
        public bool IsArchived { get; set; }
        public bool SMSStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public string Remark { get; set; }
        public IFormFile[]? Files { get; set; }
    }
    public class CaseEncodeGetDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CaseNumber { get; set; }
        public Guid? ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string LetterNumber { get; set; }
        public string LetterSubject { get; set; }
        public Guid CaseTypeId { get; set; }
        public virtual CaseType CaseType { get; set; }
        public AffairStatus AffairStatus { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Representative { get; set; }
        public bool IsArchived { get; set; }
        public bool SMSStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public string Remark { get; set; }
    }
}

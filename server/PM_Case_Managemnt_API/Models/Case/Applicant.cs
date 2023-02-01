using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Models.CaseModel
{
    public class Applicant : CommonModel
    {
  
        public string ApplicantName { get; set; }
   
        public string PhoneNumber { get; set; }

   
        public string Email { get; set; }
        
        public string CustomerIdentityNumber { get; set; }   
      

        public ApplicantType ApplicantType { get; set; }


    }

    public enum ApplicantType
    {
        Organization,
        Indivisual
    }
}

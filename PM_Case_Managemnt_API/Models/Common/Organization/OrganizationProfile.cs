
namespace PM_Case_Managemnt_API.Models.Common
{
    public class OrganizationProfile : CommonModel
    {

        public string OrganizationNameEnglish { get; set; }

        public string OrganizationNameInLocalLanguage { get; set; }
 
        public string Logo { get; set; }
    
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int SmsCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}

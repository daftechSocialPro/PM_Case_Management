

namespace PM_Case_Managemnt_API.Models.Common
{
    public class Employee : CommonModel
    {

    
        public string Photo { get; set; }

        public string Title { get; set; }

        public string FullName { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }



    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Position
    {
        Director,
        Secertary,
        Member
    }
}

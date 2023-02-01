



using System.ComponentModel;

namespace PM_Case_Managemnt_API.Models.Common
{
    public class OrganizationBranch :CommonModel
    {
        public virtual OrganizationProfile  OrganizationProfile { get; set; }

        public Guid OrganizationProfileId { get; set; }


        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool IsHeadOffice { get; set; }

    }
}

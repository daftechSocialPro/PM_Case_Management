

using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Models.PM
{
    public class Commitees : CommonModel
    {
        //public Commitees()
        //{
        //    employee = new HashSet<CommitesEmployees>();
        //}
        public string CommiteeName { get; set; }


        public virtual ICollection<CommitesEmployees> employee { get; set; }

    }
}

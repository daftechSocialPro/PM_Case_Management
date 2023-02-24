
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Models.CaseModel
{
    public class FilesInformation : CommonModel
    {

        public string FilePath { get; set; } = null!;
        public Filelookup FileLookup { get; set; }        
        public Guid ParentId { get; set; }       
        public string FileDescription { get; set; } = null!;
        public Guid FileSettingId { get; set; }
        public virtual FileSetting FileSetting { get; set; } = null!;
        public string filetype { get; set; }
    }

    public enum Filelookup
    {
        Case
    }


}

namespace PM_Case_Managemnt_API.DTOS
{
    public class MobileDTOs
    {



    }

    public class ActiveAffairsViewModel
    {
        public Guid HistoryId { get; set; }
        public string Applicant { get; set; }
        public Guid AffairId { get; set; }
        public string AffairNumber { get; set; }
        public string AffairType { get; set; }
        public string Subject { get; set; }
        public string FromEmplyee { get; set; }
        public string FromStructure { get; set; }
        public string Remark { get; set; }
        public string ReciverType { get; set; }
        public string AffairHistoryStatus { get; set; }
        public string CreatedAt { get; set; }
        public List<string> Document { get; set; }
        public string confirmedSecratary { get; set; }

    }
}

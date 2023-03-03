using PM_Case_Managemnt_API.Models.CaseModel;

namespace PM_Case_Managemnt_API.DTOS.Case
{
    public class CaseMessagesPostDto
    {
        public Guid Id { get; set; }
        public Guid? CaseId { get; set; }
        public MessageFrom MessageFrom { get; set; }
        public string MessageBody { get; set; } = null!;
        public bool Messagestatus { get; set; }
        public Guid CreatedBy { get; set; }

    }
}

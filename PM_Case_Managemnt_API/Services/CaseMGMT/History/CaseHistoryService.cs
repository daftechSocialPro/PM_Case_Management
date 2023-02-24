using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Case;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.History
{
    public class CaseHistoryService: ICaseHistoryService
    {
        private readonly DBContext _dbContext;

        public CaseHistoryService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCaseHistory(CaseHistoryPostDto caseHistoryPostDto)
        {
            try
            {
                CaseHistory history = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = caseHistoryPostDto.CreatedBy,
                    RowStatus = RowStatus.Active,
                    CaseId = caseHistoryPostDto.CaseId,
                    CaseTypeId = caseHistoryPostDto.CaseTypeId,
                    FromEmployeeId = caseHistoryPostDto.FromEmployeeId,
                    ToEmployeeId = caseHistoryPostDto.ToEmployeeId,
                    FromStructureId = caseHistoryPostDto.FromStructureId,
                    ToStructureId = caseHistoryPostDto.ToStructureId,
                    AffairHistoryStatus = caseHistoryPostDto.AffairHistoryStatus,
                    SeenDateTime = caseHistoryPostDto?.SeenDateTime,
                    TransferedDateTime = caseHistoryPostDto?.TransferedDateTime,
                    CompletedDateTime = caseHistoryPostDto?.CompletedDateTime,
                    RevertedAt = caseHistoryPostDto?.RevertedAt,
                    ReciverType = caseHistoryPostDto.ReciverType,
                    IsSmsSent = caseHistoryPostDto.IsSmsSent,
                    IsConfirmedBySeretery = caseHistoryPostDto.IsConfirmedBySeretery,
                    IsForwardedBySeretery = caseHistoryPostDto.IsConfirmedBySeretery,
                    SecreteryConfirmationDateTime = caseHistoryPostDto?.SecreteryConfirmationDateTime,
                    SecreteryId = caseHistoryPostDto?.SecreteryId,
                    ForwardedDateTime = caseHistoryPostDto?.ForwardedDateTime,
                    ForwardedById = caseHistoryPostDto?.ForwardedById,
                };

                await _dbContext.CaseHistories.AddAsync(history);
                await _dbContext.SaveChangesAsync();

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}

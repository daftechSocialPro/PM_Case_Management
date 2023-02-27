using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
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

        public async Task Add(CaseHistoryPostDto caseHistoryPostDto)
        {
            try
            {
                Case currCase = await _dbContext.Cases.SingleOrDefaultAsync(el => el.Id.Equals(caseHistoryPostDto.CaseId));

                if (currCase == null)
                    throw new Exception("Case Not found");


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
                    AffairHistoryStatus = AffairHistoryStatus.Waiting,
                    //SeenDateTime = caseHistoryPostDto?.SeenDateTime,
                    //TransferedDateTime = caseHistoryPostDto?.TransferedDateTime,
                    //CompletedDateTime = caseHistoryPostDto?.CompletedDateTime,
                    //RevertedAt = caseHistoryPostDto?.RevertedAt,
                    IsSmsSent = caseHistoryPostDto.IsSmsSent,
                    //IsConfirmedBySeretery = caseHistoryPostDto.IsConfirmedBySeretery,
                    //IsForwardedBySeretery = caseHistoryPostDto.IsConfirmedBySeretery,
                    //SecreteryConfirmationDateTime = caseHistoryPostDto?.SecreteryConfirmationDateTime,
                    SecreteryId = caseHistoryPostDto?.SecreteryId,
                    //ForwardedDateTime = caseHistoryPostDto?.ForwardedDateTime,
                    //ForwardedById = caseHistoryPostDto?.ForwardedById,
                };

                if (history.ToEmployeeId == currCase.EmployeeId)
                    history.ReciverType = ReciverType.Orginal;
                else
                    history.ReciverType = ReciverType.Cc;

                await _dbContext.CaseHistories.AddAsync(history);
                await _dbContext.SaveChangesAsync();

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task SetCaseSeen(CaseHistorySeenDto seenDto)
        {
            try
            {
                CaseHistory history = await CheckHistoryOwner(seenDto.CaseId, seenDto.SeenBy);

                history.SeenDateTime = DateTime.UtcNow;
                
                _dbContext.Entry(history).Property(history => history.SeenDateTime).IsModified = true;

                await _dbContext.SaveChangesAsync();

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CompleteCase(CaseHistoryCompleteDto completeDto)
        {
            try
            {
                CaseHistory history = await CheckHistoryOwner(completeDto.CaseId, completeDto.CompleatedBy);
                history.CompletedDateTime = DateTime.UtcNow;
                history.AffairHistoryStatus = AffairHistoryStatus.Completed;

                _dbContext.Entry(history).Property(history => history.CompletedDateTime).IsModified = true;

                Case currCase = await _dbContext.Cases.FindAsync(completeDto.CaseId);

                if (currCase == null)
                    throw new Exception("No Case with the given Id.");
                currCase.AffairStatus = AffairStatus.Completed;
                currCase.CompletedAt = DateTime.Now;

                _dbContext.Entry(currCase).Property(history => history.AffairStatus).IsModified = true;
                _dbContext.Entry(currCase).Property(history => history.CompletedAt).IsModified = true;

                await _dbContext.SaveChangesAsync();

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<CaseHistory> CheckHistoryOwner(Guid CaseId, Guid EmpId)
        {
            try
            {
                CaseHistory history = await _dbContext.CaseHistories.SingleOrDefaultAsync(history => history.CaseId.Equals(CaseId));

                if (history == null)
                    throw new Exception("No history found for the given Case.");

                if (EmpId != history.ToEmployeeId)
                    throw new Exception("Error! You can only alter Cases addressed to you.");

                return history;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Case;
using PM_Case_Managemnt_API.Models.CaseModel;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.CaseMessagesService
{
    public class CaseMessagesService : ICaseMessagesService
    {
        private readonly DBContext _dbContext;


        public CaseMessagesService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CaseMessagesPostDto caseMessagePost)
        {
            try
            {
                CaseMessages caseMessage = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = caseMessagePost.CreatedBy,
                    CaseId = caseMessagePost.CaseId,
                    MessageBody = caseMessagePost.MessageBody,
                    MessageFrom = caseMessagePost.MessageFrom,
                    Messagestatus = caseMessagePost.Messagestatus,
                    RowStatus = Models.Common.RowStatus.Active,
                };

                await _dbContext.CaseMessages.AddAsync(caseMessage);
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CaseMessages>> GetMany(bool messageStatus = false)
        {
            try
            {
                return await _dbContext.CaseMessages.Where(el => el.Messagestatus.Equals(messageStatus)).ToListAsync();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

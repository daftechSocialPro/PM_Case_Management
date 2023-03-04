using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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

        public async Task<List<CaseUnsentMessagesGetDto>> GetMany(bool messageStatus = false)
        {
            try
            {

                return await ( from  m in  _dbContext.CaseMessages.Include(x=>x.Case.Applicant).Include(x => x.Case.CaseType).Where(el => el.Messagestatus.Equals(messageStatus))
                               select new CaseUnsentMessagesGetDto
                               {
                                   ApplicantName = m.Case.Applicant.ApplicantName,
                                   LetterNumber= m.Case.LetterNumber,
                                   Subject = m.Case.LetterSubject,
                                   CaseTypeTitle = m.Case.CaseType.CaseTypeTitle,
                                   PhoneNumber = m.Case.Applicant.PhoneNumber,
                                   PhoneNumber2 = m.Case.PhoneNumber2,
                                   Message = m.MessageBody,
                                   MessageGroup = m.MessageFrom.ToString(),
                                   IsSmsSent = m.Messagestatus

    }).ToListAsync();


             
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

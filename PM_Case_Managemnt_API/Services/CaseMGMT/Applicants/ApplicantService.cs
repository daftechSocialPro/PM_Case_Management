using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.Applicants
{
    public class ApplicantService: IApplicantService
    {
        private readonly DBContext _dbContext;

        public ApplicantService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(ApplicantPostDto applicantPost)
        {
            try
            {
                Applicant applicant = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = applicantPost.CreatedBy,
                    ApplicantName = applicantPost.ApplicantName,
                    ApplicantType = applicantPost.ApplicantType,
                    CustomerIdentityNumber = applicantPost.CustomerIdentityNumber,
                    Email = applicantPost.Email,
                    PhoneNumber = applicantPost.PhoneNumber,
                    Remark = applicantPost.PhoneNumber,
                    RowStatus = RowStatus.Active
                };

                await _dbContext.Applicants.AddAsync(applicant);
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception("Error adding applicant");
            }
        }

        public async Task<List<ApplicantGetDto>> GetAll()
        {
            try
            {
                List<Applicant> applicants = await _dbContext.Applicants.ToListAsync();
                List<ApplicantGetDto> result = new();

                foreach(Applicant applicant in applicants)
                {
                    result.Add(new ApplicantGetDto()
                    {
                        Id = applicant.Id,
                        ApplicantName = applicant.ApplicantName,
                        ApplicantType = applicant.ApplicantType,
                        CreatedAt = applicant.CreatedAt,
                        CreatedBy = applicant.CreatedBy,
                        CustomerIdentityNumber = applicant.CustomerIdentityNumber,
                        Email = applicant.Email,
                        PhoneNumber = applicant.PhoneNumber,
                        RowStatus = applicant.RowStatus
                    });
                }

                return result;

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

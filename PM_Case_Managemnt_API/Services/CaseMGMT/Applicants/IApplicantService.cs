﻿using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.Applicants
{
    public interface IApplicantService
    {
        public Task Add(ApplicantPostDto applicant);
        public Task<List<ApplicantGetDto>> GetAll();
    }
}
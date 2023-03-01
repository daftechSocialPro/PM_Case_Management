﻿using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseService.Encode
{
    public interface ICaseEncodeService
    {
        public Task<string> Add(CaseEncodePostDto caseEncodePostDto);
        public Task<List<CaseEncodeGetDto>> GetAll(Guid userId);
        public Task AssignTask(CaseAssignDto caseAssignDto);
        public Task CompleteTask(CaseCompleteDto caseCompleteDto);
        public Task RevertTask(CaseRevertDto revertAffair);
        public Task TransferCase(CaseTransferDto caseTransferDto);
        public Task AddToWaiting(Guid caseId);

        public Task<string> getCaseNumber();



        public Task<List<CaseEncodeGetDto>> GetAllTransfred(Guid employeeId);
        public Task<List<CaseEncodeGetDto>> MyCaseList(Guid employeeId);

       



    }
}

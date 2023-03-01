using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseMGMT
{
    public interface ICaseProccessingService
    {

        public Task<int> ConfirmTranasaction(ConfirmTranscationDto confirmTranscationDto);
    }
}

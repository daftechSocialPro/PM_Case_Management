using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.FileInformationService
{
    public interface IFilesInformationService
    {
        public Task AddFileInformation(FilesInformationPostDto fileInformationPostDto);
    }
}

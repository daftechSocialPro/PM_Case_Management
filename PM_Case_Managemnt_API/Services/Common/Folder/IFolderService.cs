using PM_Case_Managemnt_API.DTOS.Common.Archive;

namespace PM_Case_Managemnt_API.Services.Common.FolderService
{
    public interface IFolderService
    {
        public Task Add(FolderPostDto folderPostDto);
        public Task<List<FolderGetDto>> GetAll();
    }
}

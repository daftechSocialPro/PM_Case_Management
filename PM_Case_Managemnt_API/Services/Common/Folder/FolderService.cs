using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common.Archive;
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Services.Common.FolderService
{
    public class FolderService: IFolderService
    {

        private readonly DBContext _dbContext;

        public FolderService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(FolderPostDto folderPostDto)
        {
            try
            {
                Folder newFolder = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    RowStatus = RowStatus.Active,
                    CreatedBy = folderPostDto.CreatedBy,
                    FolderName = folderPostDto.FolderName,
                    Remark = folderPostDto.Remark,
                    RowId = folderPostDto.RowId,
                    ShelfId = folderPostDto.ShelfId
                };

                await _dbContext.Folder.AddAsync(newFolder);
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
        public async Task<List<FolderGetDto>> GetAll()
        {
            try
            {
                return (await _dbContext.Folder.Include(x => x.Row).Include(x => x.Shelf).Select(x => new FolderGetDto()
                {
                    FolderName = x.FolderName,
                    Id = x.Id,
                    Remark = x.Remark,
                    RowId = x.RowId,
                    ShelfId = x.ShelfId,
                    RowNumber = x.Row.RowNumber,
                    ShelfNumber = x.Shelf.ShelfNumber
                }).ToListAsync());
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

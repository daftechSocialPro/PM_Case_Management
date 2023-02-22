namespace PM_Case_Managemnt_API.Models.Common
{
    public class Folder : CommonModel
    {
        public string FolderName { get; set; }

        public Guid ShelfId { get; set; }

        public virtual Shelf? Shelf { get; set; }

        public Guid RowId { get; set; }
        public virtual Row? Row { get; set; }
    }

}

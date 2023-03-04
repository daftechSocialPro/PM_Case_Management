namespace PM_Case_Managemnt_API.Models.Common
{
    public class Row : CommonModel
    {
        public string RowNumber { get; set; } = null!;
        public Guid ShelfId { get; set; }   
        public virtual Shelf Shelf { get; set; }
    }
}

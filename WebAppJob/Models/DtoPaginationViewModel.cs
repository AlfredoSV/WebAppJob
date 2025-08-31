namespace WebAppJob.Models
{
    public class DtoPaginationViewModel<T> where T : class
    {
        public DtoPaginationViewModel()
        {
            this.Data = new List<T>();
        }
        public PaginationViewModel PaginationViewModel { get; set; } = new PaginationViewModel();

        public List<T> Data { get; set; }
    }
}

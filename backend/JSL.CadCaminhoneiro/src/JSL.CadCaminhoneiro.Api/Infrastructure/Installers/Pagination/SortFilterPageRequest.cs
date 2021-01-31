
namespace JSL.CadCaminhoneiro.Api.Infrastructure.Installers.Pagination
{
    public class SortFilterPageRequest
    {
        public string Sort { get; set; }
        public string Filter { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }


        public SortFilterPageRequest()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public SortFilterPageRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}

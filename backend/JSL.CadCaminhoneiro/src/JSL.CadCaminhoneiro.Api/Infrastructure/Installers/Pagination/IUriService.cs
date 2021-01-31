using System;

namespace JSL.CadCaminhoneiro.Api.Infrastructure.Installers.Pagination
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}

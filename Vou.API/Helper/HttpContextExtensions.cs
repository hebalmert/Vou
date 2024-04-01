using Microsoft.EntityFrameworkCore;

namespace Vou.API.Helper
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParameterPaginationResponse<T>(this HttpContext context, IQueryable<T> queryable, int RegisterToShow)
        {
            if (context is null) { throw new ArgumentNullException(nameof(context)); }

            double conteo = await queryable.CountAsync();
            double totalPages = Math.Ceiling(conteo / RegisterToShow);
            context.Response.Headers.Append("conteo", conteo.ToString());
            context.Response.Headers.Append("Totalpages", totalPages.ToString());

        }
    }
}

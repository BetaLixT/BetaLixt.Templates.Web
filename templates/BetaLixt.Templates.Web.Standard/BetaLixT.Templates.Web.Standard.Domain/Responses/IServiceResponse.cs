using System.Threading.Tasks;

namespace BetaLixT.Templates.Web.Standard.Domain.Responses
{
    public interface IServiceResponse<T>
    {
        Task<List<TRes>> ToListAsync<TRes>(
            Func<T, TRes> selector,
            int countPerPage = 0,
            int pageNumber = 0
        );

        Task<TRes> FirstOrDefaultAsync<TRes>(
            Func<T, TRes> selector
        );
    }
}
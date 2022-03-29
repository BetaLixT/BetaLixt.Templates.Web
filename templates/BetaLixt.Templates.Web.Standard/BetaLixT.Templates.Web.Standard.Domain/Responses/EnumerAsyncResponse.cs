namespace BetaLixT.Templates.Web.Standard.Domain.Responses
{
    public class EnumerAsyncResponse<T> : IServiceResponse<T>
    {
        private readonly IAsyncEnumerable<T> _enumerable;
        public EnumerAsyncResponse(IAsyncEnumerable<T> enumerable)
        {
            this._enumerable = enumerable;
        }

        public async Task<List<TRes>> ToListAsync<TRes>(
            Func<T, TRes> selector,
            int countPerPage = 0,
            int pageNumber = 0
        )
        {
            var q = this._enumerable.Select(selector);
            if (countPerPage > 0)
            {
                q = q
                    .Skip(pageNumber * countPerPage)
                    .Take(countPerPage);
            }
            return await q.ToListAsync();
        }

        public async Task<TRes> FirstOrDefaultAsync<TRes>(
            Func<T, TRes> selector
        )
        {
            return await this._enumerable.Select(selector).FirstOrDefaultAsync();
        }
    }
}
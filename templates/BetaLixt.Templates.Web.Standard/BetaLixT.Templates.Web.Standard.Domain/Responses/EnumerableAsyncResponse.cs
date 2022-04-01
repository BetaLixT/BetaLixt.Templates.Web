using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;


namespace BetaLixT.Templates.Web.Standard.Domain.Responses
{
    public class EnumerableAsyncResponse<AccessObject> : IServiceListResponse<AccessObject>
    {
        private readonly IAsyncEnumerable<AccessObject> _enumerable;
        public EnumerableAsyncResponse(IAsyncEnumerable<AccessObject> enumerable)
        {
            this._enumerable = enumerable;
        }

        public async Task<List<TransferObject>> ToListAsync<TransferObject>(
            Func<AccessObject, TransferObject> selector,
            int countPerPage = 0,
            int pageNumber = 0
        ) where TransferObject : ITransferObject<AccessObject>
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
    }
}
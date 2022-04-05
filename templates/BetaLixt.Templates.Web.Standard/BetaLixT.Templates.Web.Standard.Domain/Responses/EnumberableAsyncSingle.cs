
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces;

namespace BetaLixT.Templates.Web.Standard.Domain.Responses
{
    public class EnumberableAsyncSingle<AccessObject> : IServiceResponse<AccessObject>
    {
        private readonly IAsyncEnumerable<AccessObject> _enumerable;
        public EnumberableAsyncSingle(IAsyncEnumerable<AccessObject> enumerable)
        {
            this._enumerable = enumerable;
        }

        public async Task<TransferObject?> GetOrDefaultAsync<TransferObject>(
            Func<AccessObject, TransferObject> selector
        ) where TransferObject : ITransferObject
        {
            return await this._enumerable
                .Select(selector)
                .FirstOrDefaultAsync();
        }


        public async Task<TransferObject?> GetOrDefaultGenericAsync<TransferObject>(
            Func<AccessObject, TransferObject> selector
        ) where TransferObject : ITransferObject
        {
            return await this._enumerable
                .Select(selector)
                .FirstOrDefaultAsync();
        }
    }
}

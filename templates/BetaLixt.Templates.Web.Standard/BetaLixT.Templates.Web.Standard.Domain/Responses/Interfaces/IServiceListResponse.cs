using System.Threading.Tasks;

namespace BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces
{
    public interface IServiceListResponse<AccessObject>
    {
        Task<List<TransferObject>> ToListAsync<TransferObject>(
            Func<AccessObject, TransferObject> selector,
            int countPerPage = 0,
            int pageNumber = 0
        ) where TransferObject : ITransferObject;
    }
}
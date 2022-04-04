using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaLixT.Templates.Web.Standard.Domain.Responses.Interfaces
{
    public interface ITransferObject
    {
        JsonTextWriter ToJson(JsonTextWriter writer);
    }
}

using BetaLixT.Templates.Web.Standard.Utility.Startup;
using Microsoft.Extensions.Logging;

namespace BetaLixT.Templates.Web.Standard.Utility.Helpers
{
    public class InitializerHelper
    {
        private IEnumerable<IInitializer> _initializers;
        private ILogger _logger;

        public InitializerHelper(
            IEnumerable<IInitializer> initializers,
            ILogger<InitializerHelper> logger)
        {
            this._initializers = initializers;
            this._logger = logger;
        }

        public void RunInitializers()
        { 
            foreach(var init in this._initializers)
            {
                try
                {
                    init.Initialize();
                }
                catch(Exception e)
                {
                    this._logger.LogError(e, "Exception while initializing {Type}", init.GetType());
                    if(init.IsRequired)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
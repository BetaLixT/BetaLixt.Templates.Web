using System;
using System.Collections.Generic;
using System.Text;

namespace BetaLixT.Templates.Web.Standard.Functionality.Interface.Scripts
{
    public interface IDatabase
    {
        /// <summary>
        /// Used to ensure that the underlying database is
        /// correctly configured and initialized
        /// </summary>
        public void Initialize();
    }
}

using System;

namespace BetaLixT.Templates.Web.Standard.Functionality.Interface.Exceptions
{
    public class EntityMissingException : Exception
    {
        public readonly int Code;
        public EntityMissingException(int code, string Message) : base(Message)
        {
            this.Code = code;
        }
    }
}

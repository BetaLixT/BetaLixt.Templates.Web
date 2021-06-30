using System;

namespace BetaLixT.Templates.Web.Standard.Functionality.Interface.Exceptions
{
    public class EntityForbiddenException : Exception
    {
        public readonly int Code;
        public EntityForbiddenException(int code, string Message) : base(Message)
        {
            this.Code = code;
        }
    }
}

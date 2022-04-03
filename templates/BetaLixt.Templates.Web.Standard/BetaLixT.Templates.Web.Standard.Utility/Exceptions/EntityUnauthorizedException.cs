namespace BetaLixT.Templates.Web.Standard.Utility.Exceptions
{
    public class EntityUnauthorizedException : BaseException
    { 
        public EntityUnauthorizedException(int code, string message, string? details = null)
		: base(code, message, details)
        {} 
    }
}
